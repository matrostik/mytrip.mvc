/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about Mytrip.Mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using DotNetOpenAuth.OpenId.RelyingParty;
using Mytrip.Mvc.Models;
using Mytrip.Mvc.Settings;
using System.Web;
using Mytrip.Mvc.Repository;

namespace Mytrip.Mvc.Controllers
{
    /// <summary>Контроллер входа существующих пользователей
    /// и регистрации новых пользователей
    /// </summary>
    public class AccountController : ICoreController
    {
        #region Вход через провайдера сайта и выход
        // **********************************************
        // Вход через провайдера сайта и выход
        // **********************************************

        /// <summary>GET: /Account/LogOn
        /// Страница входа существующих пользователей
        /// </summary>
        /// <param name="returnUrl">url для возврата пользователя</param>
        /// <param name="error">ошибка openId</param>
        /// <returns>ActionResult</returns>
        public ActionResult LogOn(string returnUrl, string error)
        {
            LogOnModel model = new LogOnModel();
            model.unlockRegistration = UsersSetting.unlockRegistration();
            model.unlockOpenId = UsersSetting.unlockOpenId();
            model.unlockSiteId = UsersSetting.unlockSiteId();
            model.errorOpenId = error;
            model.returnUrl = returnUrl;
            model.tableWidth = (model.unlockOpenId&&model.unlockSiteId)?"width:50%;":"width:100%;";
            return View(model);
        }

        /// <summary>POST: /Account/LogOn
        /// Обработка данных полученных со страницы
        /// входа при использовании провайдера сайта
        /// </summary>
        /// <param name="model">LogOnModel</param>
        /// <param name="returnUrl">url для возврата пользователя</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (UsersSetting.unlockSiteId() && ModelState.IsValid & ValidateLogon(model.UserName, model.Password))
            {
                coreRepo.formsService.SignIn(model.UserName, model.RememberMe);
                    if (!String.IsNullOrEmpty(returnUrl)&&!returnUrl.Contains("Password"))
                        return Redirect(returnUrl);
                    else
                        return RedirectToAction("Index", "Home");

            }
            model.unlockRegistration = UsersSetting.unlockRegistration();
            model.unlockOpenId = UsersSetting.unlockOpenId();
            model.unlockSiteId = UsersSetting.unlockSiteId();
            model.returnUrl = returnUrl;
            model.tableWidth = (model.unlockOpenId && model.unlockSiteId) ? "width:50%;" : "width:100%;";
            return View(model);
        }

        /// <summary>GET: /Account/LogOff
        /// Выход пользователя (актуален для всех провайдеров)
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult LogOff(string returnUrl)
        {
            coreRepo.formsService.SignOut();
            if (!String.IsNullOrEmpty(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction("Index", "Home");
        }

        //****************** E N D **********************
        #endregion

        #region Регистрация через провайдер сайта
        // **********************************************
        // Регистрация через провайдера сайта
        // **********************************************

        /// <summary>GET: /Account/Register
        /// регистрация нового пользователя
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult Register()
        {
            if (UsersSetting.unlockSiteId() && UsersSetting.unlockRegistration())
            {
                RegisterModel model = new RegisterModel();
                model.unlockCaptcha = CoreSetting.Development() ? false : UsersSetting.unlockCaptcha();
                model.minRequiredPasswordLength = UsersSetting.minRequiredPasswordLength();
                model.sendemail = false;
                return View(model);
            }
            else
                return RedirectToAction("Index", "Home");
        }

        /// <summary>POST: /Account/Register
        /// обработка данных для регистрации нового пользователя
        /// </summary>
        /// <param name="model">RegisterModel</param>
        /// <param name="returnUrl">url для возврата пользователя</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        public ActionResult Register(RegisterModel model, string returnUrl)
        {
            if (UsersSetting.unlockSiteId() && UsersSetting.unlockRegistration() && (ModelState.IsValid & ValidateConfirmPassword(model.Password, model.ConfirmPassword, "Password")))
            {
                MembershipCreateStatus createStatus = coreRepo.membershipService.CreateUser(model.UserName, model.Password, model.Email);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    GeneralMethods.MytripCacheRemove("cacherole");
                    if (!CoreSetting.Development() && EmailSetting.unlockSendEmail() && UsersSetting.unlockApprovedEmail())
                    {
                        string userId = coreRepo.membershipRepo.BlokUserEmail(model.UserName);
                        string linksite="<a href='http://"+UsersSetting.applicationName()+"'>"+UsersSetting.applicationName()+"</a>";
                        string appremail = "<a href='http://" + UsersSetting.applicationName() + "/Account/ApprovedAccount?_id=" + userId + "&returnUrl=" + returnUrl + "'>" + UsersSetting.applicationName() + "/Account/ApprovedAccount</a>";
                        
                        string subject = string.Format(CoreLanguage.subjectEmail, UsersSetting.applicationName());
                        string body = string.Format(CoreLanguage.bodyEmail, linksite, appremail);
                        coreRepo.emailRepo.SendEmail(model.Email, subject, body);
                        model.sendemail = true;
                    }
                    else
                    {
                        coreRepo.formsService.SignIn(model.UserName, false);
                        if (CoreSetting.Development())
                            return RedirectToAction("CreateAdmin", "Core");
                        else if (string.IsNullOrEmpty(returnUrl))
                            return RedirectToAction("Index", "Home");
                        else
                            return Redirect(returnUrl);
                        
                    }
                }
                else
                {
                    ModelState.AddModelError("", AccountValidation.ErrorCodeToString(createStatus));
                }
            }
            model.unlockCaptcha = CoreSetting.Development() ? false : UsersSetting.unlockCaptcha();
            model.minRequiredPasswordLength = UsersSetting.minRequiredPasswordLength();
            return View(model);
        }

        /// <summary>GET: /Account/ApprovedAccount
        /// Потверждение и одобрение пользователя по Email
        /// </summary>
        /// <param name="_id">индентификатор пользователя</param>
        /// <returns>ActionResult</returns>
        public ActionResult ApprovedAccount(string _id, string returnUrl)
        { 
          string userName = coreRepo.membershipRepo.ApprovedUserEmail(_id);
          coreRepo.formsService.SignIn(userName, false);

          if (string.IsNullOrEmpty(returnUrl))
              return RedirectToAction("Index", "Home");
          else
              return Redirect(returnUrl);
        }

        //****************** E N D **********************
        #endregion

        #region Восстановление и смена пароля, смена email
        // **********************************************
        // Восстановление и смена пароля, смена email
        // **********************************************

        /// <summary>GET: /Account/ChangePassword
        /// Смена пароля
        /// </summary>
        /// <returns>ActionResult</returns>
        [Authorize]
        public ActionResult ChangePassword()
        {
            ChangePasswordModel model = new ChangePasswordModel();
            model.minRequiredPasswordLength = UsersSetting.minRequiredPasswordLength();
            model.Change = false;
            TempData["useremail"] = MytripUser.UserEmail(User.Identity.Name);
            TempData["username"] = User.Identity.Name;
            return View(model);
        }
        
        /// <summary>POST: /Account/ChangePassword
        /// Обработка данных для смены пароля
        /// </summary>
        /// <param name="model">ChangePasswordModel</param>
        /// <returns>ActionResult</returns>
        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid & ValidateConfirmPassword(model.NewPassword, model.ConfirmPassword, "NewPassword"))
            {
                if (coreRepo.membershipService.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword))
                {
                    model.Change = true;
                }
            }
            TempData["useremail"] = MytripUser.UserEmail(User.Identity.Name);
            TempData["username"] = User.Identity.Name;
            model.minRequiredPasswordLength = UsersSetting.minRequiredPasswordLength();
            return View(model);
        }
        /// <summary>GET: /Account/ChangeEmail
        /// Change Email
        /// </summary>
        /// <returns>ActionResult</returns>
        [Authorize]
        public ActionResult ChangeEmail()
        {
            ChangeEmailModel model = new ChangeEmailModel();
            model.Change = false;
            TempData["useremail"] = model.Email = MytripUser.UserEmail(User.Identity.Name);
            TempData["username"] = User.Identity.Name;
            return View(model);
        }
        /// <summary>POST: /Account/ChangePassword
        /// Обработка данных для смены пароля
        /// </summary>
        /// <param name="model">ChangePasswordModel</param>
        /// <returns>ActionResult</returns>
        [Authorize]
        [HttpPost]
        public ActionResult ChangeEmail(ChangeEmailModel model)
        {
            if (ModelState.IsValid)
            {
                coreRepo.membershipRepo.ChangeEmail(model.Email);
                model.Change = true;
            }
            TempData["useremail"] = model.Email = MytripUser.UserEmail(User.Identity.Name);
            TempData["username"] = User.Identity.Name;
            return View(model);
        }

        public ActionResult ForgotPassword(bool _id,string returnUrl)
        {
            ForgotPasswordModel model = new ForgotPasswordModel();
            model.sendemail = false;
           if(!_id)
               model.message = new HtmlString("<div class='noappr'>" +CoreLanguage.ForgotDateError+ "</div><div class='last'></div>");
          
            return View(model);
        }
        [HttpPost]
        public ActionResult ForgotPassword(string returnUrl,ForgotPasswordModel model)
        {
            model.sendemail = false;
            if (ModelState.IsValid) 
            {
                var x = coreRepo.membershipRepo.GetMembershipForForgotPassword(model.Email);
                foreach (var _x in x)
                    {
                        if (_x.Password == _x.PasswordSalt)
                        {
                            if (!model.sendemail)
                            {
                                model.sendemail = true;
                                model.message = new HtmlString(CoreLanguage.forgotOpenid);
                            }
                        }
                        else
                        {
                            coreRepo.membershipRepo.CreateDateCangePassword(_x);
                            string userId = _x.UserId;
                            string username = _x.mytrip_users.UserName;
                            string linksite = "<a href='http://" + UsersSetting.applicationName() + "'>" + UsersSetting.applicationName() + "</a>";
                            string appremail = "<a href='http://" + UsersSetting.applicationName() + "/Account/RecoveryPassword?_id=" + userId + "&returnUrl=" + returnUrl + "'>" + UsersSetting.applicationName() + "/Account/RecoveryPassword</a>";
                        
                            string subject = string.Format(CoreLanguage.recovryPassword, UsersSetting.applicationName());
                            string body = string.Format(CoreLanguage.bodyForgot, linksite, appremail,username);
                            coreRepo.emailRepo.SendEmail(model.Email, subject, body);
                            model.message = new HtmlString(CoreLanguage.Email_forgot_body);
                            model.sendemail = true;
                        }
                    }
                coreRepo.membershipRepo.SaveChanges();
                    
            }
            return View(model);
        }
        public ActionResult RecoveryPassword(string _id, string returnUrl)
        {
            bool a = coreRepo.membershipRepo.DateCangePassword(_id);
            if (!a)
                return RedirectToAction("ForgotPassword", new { _id=a,returnUrl});
            RecoveryPasswordModel model = new RecoveryPasswordModel();
            model.sendemail = false;
            return View(model);
        }
        [HttpPost]
        public ActionResult RecoveryPassword(string _id, string returnUrl, RecoveryPasswordModel model)
        {
            if (ModelState.IsValid & ValidateConfirmPassword(model.Password, model.ConfirmPassword, "Password"))
            {
                string userName=coreRepo.membershipRepo.RecoveryPassword(_id, model.Password);
                coreRepo.formsService.SignIn(userName, false);
                model.sendemail = true;
                model.returnUrl = returnUrl;
            }
            return View(model);
        }
        //****************** E N D **********************
        #endregion

        #region Вход и регистрация через OpenId
        // **********************************************
        // Вход и регистрация через OpenId
        // **********************************************

        /// <summary>GET: /Account/OpenIdLogin
        /// Вход пользователей через OpenId
        /// </summary>
        /// <param name="openid">провайдер OpenId</param>
        /// <param name="returnUrl">url для возврата пользователя</param>
        /// <returns>ActionResult</returns>
        public ActionResult OpenIdLogin(string openid,string returnUrl)
        {            
            OpenIdRelyingParty _openid = new OpenIdRelyingParty();
            IAuthenticationResponse response = _openid.GetResponse();
            if (UsersSetting.unlockOpenId() && response == null)
            {
                Identifier _id;
                if (Identifier.TryParse(openid, out _id))
                {
                    try
                    {
                        IAuthenticationRequest req = _openid.CreateRequest(openid);

                        var fetch = new FetchRequest();
                        var item = new AttributeRequest(WellKnownAttributes.Contact.Email);
                        item.IsRequired = true;
                        fetch.Attributes.Add(item);
                        req.AddExtension(fetch);
                        return req.RedirectingResponse.AsActionResult();
                    }
                    catch
                    {
                        return RedirectToAction("Logon", new { returnUrl, error = CoreLanguage.openiderror });
                    }
                }
                else
                {
                    return RedirectToAction("Logon", new { returnUrl, error = CoreLanguage.openiderror });
                }
            }
            else if (UsersSetting.unlockOpenId())
            {
                switch (response.Status)
                {
                    case AuthenticationStatus.Authenticated:
                        string email = "z";
                        var fetch = response.GetExtension<FetchResponse>();
                        string provider = response.FriendlyIdentifierForDisplay;
                        if (fetch != null)
                        {
                            IList<string> emailAddresses = fetch.Attributes[WellKnownAttributes.Contact.Email].Values;
                            email = emailAddresses.Count > 0 ? emailAddresses[0] : null;
                        }
                        //if (email != "z" && UsersSetting.requiresUniqueEmail() && !coreRepo.membershipRepo.mtAccessibleEmail(email))
                        //{
                        //    return RedirectToAction("Logon", new { returnUrl, error = CoreLanguage.register_duplicate_email }); 
                        //}
                        provider = "[" + provider + "][" + email + "]";
                        bool approved = false;
                        bool yes = false;
                        string userName = coreRepo.membershipRepo.UserNameOpenId(provider, out approved, out yes);

                        if (!approved && yes)
                        {
                            return RedirectToAction("Logon", new { returnUrl, error = CoreLanguage.account_block_username });
                        }
                        else if (!yes)
                        {
                            provider = provider.Replace(":", "(x)").Replace("/", "()");
                            return RedirectToAction("OpenIdRegister", new { id = email, id2 = provider, returnUrl = returnUrl });
                        }
                        else if (approved && yes)
                            FormsAuthentication.SetAuthCookie(userName, false);
                        if (!string.IsNullOrEmpty(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    case AuthenticationStatus.Canceled:
                        return RedirectToAction("Logon", new { returnUrl, error = CoreLanguage.cancel });
                    case AuthenticationStatus.Failed:
                        return RedirectToAction("Logon", new { returnUrl, error = CoreLanguage.openiderror });
                }
            }
            else
                return RedirectToAction("Logon", new { returnUrl });
            return new EmptyResult();

        }

        /// <summary>GET: /Account/OpenIdRegister
        /// Регистрация нового пользователя авторизовавшегося
        /// через OpenId
        /// </summary>
        /// <param name="id">Email</param>
        /// <param name="id2">Имя пользователя</param>
        /// <param name="returnUrl">url для возврата пользователя</param>
        /// <returns>ActionResult</returns>
        public ActionResult OpenIdRegister(string id, string id2, string returnUrl)
        {
            OpenIdRegisterModel model = new OpenIdRegisterModel();
            if (id == "z")
            {
                string username = "";
                if(id2.Contains("openid.yandex.ru"))
                    username = id2.Remove(0, (id2.IndexOf(')') + 1));
                else if (id2.Contains("id.rambler.ru"))
                {
                    username = id2.Remove(0, (id2.IndexOf(')') + 1));
                    username = username.Remove(0, (username.IndexOf(')') + 1));
                }
                else if (id2.Contains("claimid.com"))
                {
                    username = id2.Remove(0, (id2.IndexOf(')') + 1));
                }
                else
                    username = id2.Substring(0, id2.IndexOf('.'));
                model.UserName = username;
                model.Email = string.Empty;
                model.display = "show";
            }
            else
            {
                model.UserName = id.Substring(0, id.IndexOf('@'));
                model.Email = id; 
                model.display = "show";
            }
            return View(model);
        }

        /// <summary>POST: /Account/OpenIdRegister
        /// Обработка данных полученных при регистрации
        /// нового пользователя авторизовавшегося
        /// через OpenId
        /// </summary>
        /// <param name="model">OpenIdRegisterModel</param>
        /// <param name="id2">Имя пользователя</param>
        /// <param name="returnUrl">url для возврата пользователя</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        public ActionResult OpenIdRegister(OpenIdRegisterModel model,string id2, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                id2 = id2.Replace("(x)",":").Replace("()","/");
                coreRepo.membershipRepo.CreateUserOpenId(model.UserName,model.Email,id2,true);
                FormsAuthentication.SetAuthCookie(model.UserName, false);
                GeneralMethods.MytripCacheRemove("cacherole");
                if (!string.IsNullOrEmpty(returnUrl))
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

        //****************** E N D **********************
        #endregion

        #region Валидация данных
        // **********************************************
        // Валидация данных
        // **********************************************

        /// <summary>Проверка пользователя на
        /// корректность внесенных данных
        /// </summary>
        /// <param name="UserName">Имя пользователя</param>
        /// <param name="Password">Пароль</param>
        /// <returns>возвращает bool</returns>
        private bool ValidateLogon(string UserName, string Password)
        {
            if (!String.IsNullOrEmpty(UserName) && !String.IsNullOrEmpty(Password) && !Membership.ValidateUser(UserName, Password))
                ModelState.AddModelError("Password", CoreLanguage.logon_error_password);
            return ModelState.IsValid;
        }

        /// <summary>Проверка корректного ввода повтора пароля
        /// </summary>
        /// <param name="Password">Пароль</param>
        /// <param name="ConfirmPassword">Повтор пароля</param>
        /// <param name="namePassword">Название поля пароля для возврата ошибки</param>
        /// <returns>возвращает bool</returns>
        private bool ValidateConfirmPassword(string Password, string ConfirmPassword, string namePassword)
        {
            if (!String.IsNullOrEmpty(Password) && !String.IsNullOrEmpty(ConfirmPassword) && Password != ConfirmPassword)
            {
                ModelState.AddModelError(namePassword, CoreLanguage.register_password_confurmpassword);
                ModelState.AddModelError("ConfirmPassword", CoreLanguage.register_password_confurmpassword);
            }
            return ModelState.IsValid;
        }

        //****************** E N D **********************
        #endregion
    }
}
