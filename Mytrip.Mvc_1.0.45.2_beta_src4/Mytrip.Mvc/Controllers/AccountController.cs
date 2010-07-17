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
using Mytrip.Mvc.Models;
using Mytrip.Mvc.Repository;

namespace Mytrip.Mvc.Controllers
{
    /// <summary>
    /// Account Controller
    /// </summary>
    [HandleError]
    [Localization]
    public class AccountController : Controller
    {
        #region Properties
        CoreSetting _core;
        public CoreSetting __core
        {
            get
            {
                if (_core == null)
                    _core = new CoreSetting();
                return _core;
            }
        }
        UsersSetting _userset;
        public UsersSetting userset
        {
            get
            {
                if (_userset == null)
                    _userset = new UsersSetting();
                return _userset;
            }
        }
        ICoreRepository _coreRepo;
        public ICoreRepository coreRepo
        {
            get
            {
                if (_coreRepo == null)
                    _coreRepo = new ICoreRepository();
                return _coreRepo;
            }
        }
        /// <summary>
        /// IFormsAuthenticationService FormsService
        /// </summary>
        public IFormsAuthenticationService FormsService { get; set; }
        /// <summary>
        /// IMembershipService MembershipService
        /// </summary>
        public IMembershipService MembershipService { get; set; }
        #endregion

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
            LogOnModel model = new LogOnModel();
            model.unlockRegistration = userset.unlockRegistration();
            return View(model);
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
                        return Redirect(returnUrl);
                    else
                        return RedirectToAction("Index", "Home");
                
            }
            model.unlockRegistration = userset.unlockRegistration();
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
                return Redirect(returnUrl);
            else
                return RedirectToAction("Index", "Home");
        }
        /// <summary>
        /// URL: /Account/Register
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult Register()
        {
            if (userset.unlockRegistration())
            {
                RegisterModel model = new RegisterModel();
                if (__core.Development())
                    model.unlockCaptcha = false;
                else
                model.unlockCaptcha = userset.unlockCaptcha();
                model.minRequiredPasswordLength = userset.minRequiredPasswordLength();
                return View(model);
            }
            else
                return RedirectToAction("Index", "Home");
        }
        /// <summary>
        /// POST: /Account/Register
        /// </summary>
        /// <param name="model">RegisterModel</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        public ActionResult Register(RegisterModel model, string returnUrl)
        {
            if (ModelState.IsValid & ValidateRegistration(model.Password,model.ConfirmPassword))
            {
                MembershipCreateStatus createStatus = MembershipService.CreateUser(model.UserName, model.Password, model.Email);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    if (!__core.Development()&& userset.unlockSendEmail() && userset.unlockApprovedEmail())
                    {
                        string userId=coreRepo.membershipRepo.BlokUserEmail(model.UserName);
                        string subject = string.Format(CoreLanguage.subjectEmail, userset.applicationName());
                        string body = string.Format(CoreLanguage.bodyEmail, userset.applicationName(), userId);
                        coreRepo.emailRepo.SendEmail(model.Email, subject, body);
                        return RedirectToAction("Email");
                    }
                    else
                    {
                        FormsService.SignIn(model.UserName, false);
                        if (__core.Development())
                            return RedirectToAction("CreateAdmin", "Core");
                        else
                        {
                            if (string.IsNullOrEmpty(returnUrl))
                                return RedirectToAction("Index", "Home");
                            else
                                return Redirect(returnUrl);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", AccountValidation.ErrorCodeToString(createStatus));
                }
            }
            model.unlockCaptcha = userset.unlockCaptcha();
            model.minRequiredPasswordLength = userset.minRequiredPasswordLength();
            return View(model);
        }
        public ActionResult Email()
        { return View(); }
        public ActionResult ApprovedAccount(string id)
        { 
          string userName = coreRepo.membershipRepo.ApprovedUserEmail(id);
          FormsService.SignIn(userName, false);
          return RedirectToAction("Index", "Home");
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
            ChangePasswordModel model = new ChangePasswordModel();
            model.minRequiredPasswordLength = userset.minRequiredPasswordLength();
            return View(model);
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
            model.minRequiredPasswordLength = userset.minRequiredPasswordLength();
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
