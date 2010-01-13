/*   Mytrip.Mvc.Web.Linq2sql   Copyright ©  2009 Стюхин Олег Анатольевич   */
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Mytrip.Mvc.Web.Linq2sql.Models;
using Mytrip.Mvc.Model.Linq2sql;
using Mytrip.Mvc.Web.Linq2sql.HtmlHelpers;
using System.Text.RegularExpressions;
using System.Configuration;

namespace Mytrip.Mvc.Web.Linq2sql.Controllers
{

    [HandleError]
    [Localization]
    public class BController : Controller
    {
        #region настройка контроллера
        // This constructor is used by the MVC framework to instantiate the controller using
        // the default forms authentication and membership providers.
        IRepository _dc = new IRepository(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
        // This constructor is used by the MVC framework to instantiate the controller using
        // the default forms authentication and membership providers.

        public BController()
            : this(null, null)
        {
        }

        // This constructor is not used by the MVC framework but is instead provided for ease
        // of unit testing this type. See the comments in AccountModels.cs for more information.
        public BController(IFormsAuthenticationService formsService, IMembershipService membershipService)
        {            
            FormsService = formsService ?? new FormsAuthenticationService();
            MembershipService = membershipService ?? new AccountMembershipService();
        }

        public IFormsAuthenticationService FormsService
        {
            get;
            private set;
        }

        public IMembershipService MembershipService
        {
            get;
            private set;
        }

        protected override void Initialize(RequestContext requestContext)
        {
            if (requestContext.HttpContext.User.Identity is WindowsIdentity)
            {
                throw new InvalidOperationException("Windows authentication is not supported.");
            }
            else
            {
                base.Initialize(requestContext);
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;

            base.OnActionExecuting(filterContext);
        }
        #endregion

        /* ВХОД */
        /*-----------A - вход------------------*/
        public ActionResult A()
        {
            #region меню и титл
            ViewData["logon_url"] = "/A/A";
            ViewData["language_url"] = "/B/A";
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion
            #region настройка сайта
            ViewData["menu_url"] = "logon";
            //языковая панель
            ViewData["lang_panel"] = _dc.dm_model.Get_model().Language_approved;
            //bool разрешены блоги или нет
            ViewData["model_blog"] = _dc.dm_model.Get_model().Blog;
            //bool разрешены новости или нет
            ViewData["model_news"] = _dc.dm_model.Get_model().News;
            //bool разрешены статьи или нет
            ViewData["model_artycle"] = _dc.dm_model.Get_model().Artycle;
            if (Session["culture"] == null)
            {
                Session["culture"] = _dc.dm_model.Get_model().mt_language.param;
            }
            ViewData["language"] = new SelectList(_dc.dm_model.Get_languages(), "param", "name");
            #endregion
            return View();
        }

        [HttpPost]
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings",
            Justification = "Needs to take same parameter type as Controller.Redirect()")]
        public ActionResult A(LogOnModel model, bool rememberMe, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (MembershipService.ValidateUser(model.UserName, model.Password))
                {
                    FormsService.SignIn(model.UserName, rememberMe);
                    if (!String.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("A", "A");
                    }
                }
                else
                {

                    ModelState.AddModelError("", "Неправильно введено Логин или пароль");
                }
            }
            #region меню и титл
            ViewData["logon_url"] = "/A/A";
            ViewData["language_url"] = "/B/A";
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion
            #region настройка сайта            
            ViewData["menu_url"] = "logon";
            //языковая панель
            ViewData["lang_panel"] = _dc.dm_model.Get_model().Language_approved;
            //bool разрешены блоги или нет
            ViewData["model_blog"] = _dc.dm_model.Get_model().Blog;
            //bool разрешены новости или нет
            ViewData["model_news"] = _dc.dm_model.Get_model().News;
            //bool разрешены статьи или нет
            ViewData["model_artycle"] = _dc.dm_model.Get_model().Artycle;
            if (Session["culture"] == null)
            {
                Session["culture"] = _dc.dm_model.Get_model().mt_language.param;
            }
            ViewData["language"] = new SelectList(_dc.dm_model.Get_languages(), "param", "name");
            #endregion
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        /*-------------B - регистрация без captcha-------------------*/
        public ActionResult B()
        {
            #region меню и титл
            ViewData["logon_url"] = "/A/A";
            ViewData["language_url"] = "/B/B";
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion
            #region настройка сайта            
            ViewData["menu_url"] = "registr";
            //языковая панель
            ViewData["lang_panel"] = _dc.dm_model.Get_model().Language_approved;
            //bool разрешены блоги или нет
            ViewData["model_blog"] = _dc.dm_model.Get_model().Blog;
            //bool разрешены новости или нет
            ViewData["model_news"] = _dc.dm_model.Get_model().News;
            //bool разрешены статьи или нет
            ViewData["model_artycle"] = _dc.dm_model.Get_model().Artycle;
            if (Session["culture"] == null)
            {
                Session["culture"] = _dc.dm_model.Get_model().mt_language.param;
            }
            ViewData["language"] = new SelectList(_dc.dm_model.Get_languages(), "param", "name");
            #endregion
            return View();
        }        
        [HttpPost]
        public ActionResult B(RegisterModel model)
        {
            if (ModelState.IsValid)
            {

                // Attempt to register the user
                MembershipCreateStatus createStatus = MembershipService.CreateUser(model.UserName, model.Password, model.Email);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    FormsService.SignIn(model.UserName, false /* createPersistentCookie */);
                    if (_dc.dm_model.Get_model().Email_approved == true)
                    {
                        return RedirectToAction("G");
                    }
                    else { return RedirectToAction("A", "A"); }
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }

            }
            #region меню и титл
            ViewData["logon_url"] = "/A/A";
            ViewData["language_url"] = "/B/B";
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion
            #region настройка сайта            
            ViewData["menu_url"] = "registr";
            //языковая панель
            ViewData["lang_panel"] = _dc.dm_model.Get_model().Language_approved;
            //bool разрешены блоги или нет
            ViewData["model_blog"] = _dc.dm_model.Get_model().Blog;
            //bool разрешены новости или нет
            ViewData["model_news"] = _dc.dm_model.Get_model().News;
            //bool разрешены статьи или нет
            ViewData["model_artycle"] = _dc.dm_model.Get_model().Artycle;
            if (Session["culture"] == null)
            {
                Session["culture"] = _dc.dm_model.Get_model().mt_language.param;
            }
            ViewData["language"] = new SelectList(_dc.dm_model.Get_languages(), "param", "name");
            #endregion
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        /*-------------C - сменить пароль-------------------*/
        [Authorize]
        public ActionResult C()
        {
            #region меню и титл
            ViewData["language_url"] = "/B/C";
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion
            #region настройка сайта            
            ViewData["menu_url"] = "change_password";
            //языковая панель
            ViewData["lang_panel"] = _dc.dm_model.Get_model().Language_approved;
            //bool разрешены блоги или нет
            ViewData["model_blog"] = _dc.dm_model.Get_model().Blog;
            //bool разрешены новости или нет
            ViewData["model_news"] = _dc.dm_model.Get_model().News;
            //bool разрешены статьи или нет
            ViewData["model_artycle"] = _dc.dm_model.Get_model().Artycle;
            if (Session["culture"] == null)
            {
                Session["culture"] = _dc.dm_model.Get_model().mt_language.param;
            }
            ViewData["language"] = new SelectList(_dc.dm_model.Get_languages(), "param", "name");
            #endregion

            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult C(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                if (MembershipService.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword))
                {
                    return RedirectToAction("D");
                }
                else
                {

                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }
            #region меню и титл
            ViewData["language_url"] = "/B/C";
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion
            #region настройка сайта
            ViewData["menu_url"] = "change_password";
            //языковая панель
            ViewData["lang_panel"] = _dc.dm_model.Get_model().Language_approved;
            //bool разрешены блоги или нет
            ViewData["model_blog"] = _dc.dm_model.Get_model().Blog;
            //bool разрешены новости или нет
            ViewData["model_news"] = _dc.dm_model.Get_model().News;
            //bool разрешены статьи или нет
            ViewData["model_artycle"] = _dc.dm_model.Get_model().Artycle;
            if (Session["culture"] == null)
            {
                Session["culture"] = _dc.dm_model.Get_model().mt_language.param;
            }
            ViewData["language"] = new SelectList(_dc.dm_model.Get_languages(), "param", "name");
            #endregion
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        /*----------D - успешная смена пароля-----------------*/
        public ActionResult D()
        {
            #region меню и титл
            ViewData["logon_url"] = "/A/A";
            ViewData["language_url"] = "/B/D";
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion
            #region настройка сайта
            ViewData["menu_url"] = "change_password";
            //языковая панель
            ViewData["lang_panel"] = _dc.dm_model.Get_model().Language_approved;
            //bool разрешены блоги или нет
            ViewData["model_blog"] = _dc.dm_model.Get_model().Blog;
            //bool разрешены новости или нет
            ViewData["model_news"] = _dc.dm_model.Get_model().News;
            //bool разрешены статьи или нет
            ViewData["model_artycle"] = _dc.dm_model.Get_model().Artycle;
            if (Session["culture"] == null)
            {
                Session["culture"] = _dc.dm_model.Get_model().mt_language.param;
            }
            ViewData["language"] = new SelectList(_dc.dm_model.Get_languages(), "param", "name");
            #endregion
            return View();
        }

        /*--------------F - выход     --------------------*/
        public ActionResult F()
        {
            FormsService.SignOut();

            return RedirectToAction("A", "A");
        }

        /*-----E - регистрация администратора сайта (появляется один раз при первом входе на сайт)----*/
        public ActionResult E()
        {
            ViewData["logon_url"] = "/A/A";
            ViewData["language_url"] = "/B/E";
            //языковая панель
            ViewData["lang_panel"] = true;
            if (_dc.dm_model.Get_models().Count() != 0)
            {
                return RedirectToAction("A", "A");
            }
            if (Session["culture"] == null)
            {
                Session["culture"] = "en-us";
            }
            ViewData["language"] = new SelectList(_dc.dm_model.Get_languages(), "param", "name");
            return View();
        }

        [HttpPost]
        public ActionResult E(RegisterModel model)
        {
            if (ModelState.IsValid)
            {

                // Attempt to register the user
                MembershipCreateStatus createStatus = MembershipService.CreateUser(model.UserName, model.Password, model.Email);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    FormsService.SignIn(model.UserName, false /* createPersistentCookie */);
                    return RedirectToAction("G", "A");
                }
                else
                {
                    ViewData["logon_url"] = "/A/A";
                    ViewData["language_url"] = "/B/E";
                    //языковая панель
                    ViewData["lang_panel"] = true;
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                    if (Session["culture"] == null)
                    {
                        Session["culture"] = "en-us";
                    }
                    ViewData["language"] = new SelectList(_dc.dm_model.Get_languages(), "param", "name");
                }

            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        /*----------G - отправка почты-----------------*/
        [Authorize]
        public ActionResult G()
        {
            #region меню и титл
            ViewData["language_url"] = "/B/G";
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion
            #region настройка сайта
            ViewData["menu_url"] = "registr";
            //языковая панель
            ViewData["lang_panel"] = _dc.dm_model.Get_model().Language_approved;
            //bool разрешены блоги или нет
            ViewData["model_blog"] = _dc.dm_model.Get_model().Blog;
            //bool разрешены новости или нет
            ViewData["model_news"] = _dc.dm_model.Get_model().News;
            //bool разрешены статьи или нет
            ViewData["model_artycle"] = _dc.dm_model.Get_model().Artycle;
            if (Session["culture"] == null)
            {
                Session["culture"] = _dc.dm_model.Get_model().mt_language.param;
            }
            ViewData["language"] = new SelectList(_dc.dm_model.Get_languages(), "param", "name");
            #endregion
            _dc.dm_mail.Send_mail_registration();
            _dc.dm_model.Close_user();
            FormsService.SignOut();
            return View();
        }

        /*-------------H - регистрация c captcha-------------------*/
        public ActionResult H()
        {
            #region меню и титл
            ViewData["logon_url"] = "/A/A";
            ViewData["language_url"] = "/B/H";
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion
            #region настройка сайта
            ViewData["menu_url"] = "registr";
            //языковая панель
            ViewData["lang_panel"] = _dc.dm_model.Get_model().Language_approved;
            //bool разрешены блоги или нет
            ViewData["model_blog"] = _dc.dm_model.Get_model().Blog;
            //bool разрешены новости или нет
            ViewData["model_news"] = _dc.dm_model.Get_model().News;
            //bool разрешены статьи или нет
            ViewData["model_artycle"] = _dc.dm_model.Get_model().Artycle;
            if (Session["culture"] == null)
            {
                Session["culture"] = _dc.dm_model.Get_model().mt_language.param;
            }
            ViewData["language"] = new SelectList(_dc.dm_model.Get_languages(), "param", "name");
            #endregion

            return View();
        }
        [CaptchaValidator]
        [HttpPost]
        public ActionResult H(RegisterModel model, bool captchaValid)
        {


            if (ModelState.IsValid)
            {
                if (!captchaValid)
                {
                    ModelState.AddModelError("_FORM", "Неправильно введено слово проверки.");
                }
                else
                {

                    // Attempt to register the user
                    MembershipCreateStatus createStatus = MembershipService.CreateUser(model.UserName, model.Password, model.Email);

                    if (createStatus == MembershipCreateStatus.Success)
                    {
                        FormsService.SignIn(model.UserName, false /* createPersistentCookie */);
                        if (_dc.dm_model.Get_model().Email_approved == true)
                        {
                            return RedirectToAction("G");
                        }
                        else { return RedirectToAction("A", "A"); }
                    }
                    else
                    {
                        ModelState.AddModelError("", ErrorCodeToString(createStatus));
                    }

                }
            }
            #region меню и титл
            ViewData["logon_url"] = "/A/A";
            ViewData["language_url"] = "/B/H";
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion
            #region настройка сайта
            ViewData["menu_url"] = "registr";
            //языковая панель
            ViewData["lang_panel"] = _dc.dm_model.Get_model().Language_approved;
            //bool разрешены блоги или нет
            ViewData["model_blog"] = _dc.dm_model.Get_model().Blog;
            //bool разрешены новости или нет
            ViewData["model_news"] = _dc.dm_model.Get_model().News;
            //bool разрешены статьи или нет
            ViewData["model_artycle"] = _dc.dm_model.Get_model().Artycle;
            if (Session["culture"] == null)
            {
                Session["culture"] = _dc.dm_model.Get_model().mt_language.param;
            }
            ViewData["language"] = new SelectList(_dc.dm_model.Get_languages(), "param", "name");
            #endregion

            return View();
        }
        /*--------------I - одобрение пользователя  --------------------*/
        public ActionResult I(Guid a)
        {
            _dc.dm_model.Approved_user(a);

            return RedirectToAction("A", "A");
        }
        /*---------------------------------------------------*/

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://msdn.microsoft.com/en-us/library/system.web.security.membershipcreatestatus.aspx for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "Username already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A username for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }

    }
}
