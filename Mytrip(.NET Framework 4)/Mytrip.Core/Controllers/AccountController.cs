//************************************************************ 
// Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich
// To learn more about Mytrip.Mvc.Entyty visit 
// http://starterkitmytripmvc.codeplex.com/
// mytripmvc@gmail.com
// license: Microsoft Public License (Ms-PL) 
// ***********************************************************
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Mytrip.Core.Models;

namespace Mytrip.Core.Controllers
{
    /// <summary>
    /// Account Controller
    /// </summary>
    [HandleError]
    [Localization]
    public class AccountController : Controller
    {
        /// <summary>
        /// IFormsAuthenticationService FormsService
        /// </summary>
        public IFormsAuthenticationService FormsService { get; set; }
        /// <summary>
        /// IMembershipService MembershipService
        /// </summary>
        public IMembershipService MembershipService { get; set; }
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="requestContext">RequestContext</param>
        protected override void Initialize(RequestContext requestContext)
        {
            if (FormsService == null) { FormsService = new FormsAuthenticationService(); }
            if (MembershipService == null) { MembershipService = new AccountMembershipService(); }
            base.Initialize(requestContext);
        }
        /// <summary>
        /// URL: /Account/LogOn
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult LogOn()
        {
            return View();
        }
        /// <summary>
        /// POST: /Account/LogOn
        /// </summary>
        /// <param name="model">LogOnModel</param>
        /// <param name="returnUrl">Return Url</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid & ValidateLogon(model.UserName,model.Password))
            {
                    FormsService.SignIn(model.UserName, model.RememberMe);
                    if (!String.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                
            }
            return View(model);
        }
        /// <summary>
        /// Validate Logon
        /// </summary>
        /// <param name="UserName">User Name</param>
        /// <param name="Password">Password</param>
        /// <returns>bool</returns>
        private bool ValidateLogon(string UserName,
                                 string Password)
        {
            if (!String.IsNullOrEmpty(UserName) && !String.IsNullOrEmpty(Password) && !Membership.ValidateUser(UserName, Password))
                ModelState.AddModelError("Password", CoreLanguage.logon_error_password);
            return ModelState.IsValid;
        }
        /// <summary>
        /// URL: /Account/LogOff
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult LogOff(string returnUrl)
        {
            FormsService.SignOut();
            if (!String.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        /// <summary>
        /// URL: /Account/Register
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult Register()
        {
            if(UsersSetting.unlockRegistration)
            return View();
            else
                return RedirectToAction("Index", "Home");
        }
        /// <summary>
        /// POST: /Account/Register
        /// </summary>
        /// <param name="model">RegisterModel</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid & ValidateRegistration(model.Password,model.ConfirmPassword))
            {
                MembershipCreateStatus createStatus = MembershipService.CreateUser(model.UserName, model.Password, model.Email);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    FormsService.SignIn(model.UserName, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", AccountValidation.ErrorCodeToString(createStatus));
                }
            }
            return View(model);
        }
        /// <summary>
        /// Validate Registration
        /// </summary>
        /// <param name="Password">Password</param>
        /// <param name="ConfirmPassword">Confirm Password</param>
        /// <returns></returns>
        public bool ValidateRegistration(string Password,
                                 string ConfirmPassword)
        {
            if (!String.IsNullOrEmpty(Password) && !String.IsNullOrEmpty(ConfirmPassword) && Password != ConfirmPassword)
            {
                ModelState.AddModelError("Password", CoreLanguage.register_password_confurmpassword);
                ModelState.AddModelError("ConfirmPassword", CoreLanguage.register_password_confurmpassword);
            }
            return ModelState.IsValid;
        }
        /// <summary>
        /// URL: /Account/ChangePassword
        /// </summary>
        /// <returns>ActionResult</returns>
        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }
        /// <summary>
        /// POST: /Account/ChangePassword
        /// </summary>
        /// <param name="model">ChangePasswordModel</param>
        /// <returns>ActionResult</returns>
        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid & ValidateChangePassword(model.NewPassword, model.ConfirmPassword))
            {
                if (MembershipService.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword))
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
            }
            return View(model);
        }
        /// <summary>
        /// Validate Change Password
        /// </summary>
        /// <param name="Password">Password</param>
        /// <param name="ConfirmPassword">Confirm Password</param>
        /// <returns>bool</returns>
        public bool ValidateChangePassword(string Password,
                                 string ConfirmPassword)
        {
            if (!String.IsNullOrEmpty(Password) && !String.IsNullOrEmpty(ConfirmPassword) && Password != ConfirmPassword)
            {
                ModelState.AddModelError("NewPassword", CoreLanguage.register_password_confurmpassword);
                ModelState.AddModelError("ConfirmPassword", CoreLanguage.register_password_confurmpassword);
            }
            return ModelState.IsValid;
        }
        /// <summary>
        /// URL: /Account/ChangePasswordSuccess
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

    }
}
