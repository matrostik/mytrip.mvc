/*   Mytrip.Mvc.Web.Linq2sql   Copyright ©  2009 Стюхин Олег Анатольевич   */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Mytrip.Mvc.Model.Linq2sql;
using System.Web.Security;
using System.ServiceModel.Syndication;
using Mytrip.Mvc.Model.Linq2sql.Rss;
using System.Configuration;

namespace Mytrip.Mvc.Web.Linq2sql.Controllers
{
    public class CController : Controller
    {
        /*  ПОДКЛЮЧЕНИЕ К МОДЕЛИ  */

        /*-----------------------------------------------------*/
        IRepository _dc = new IRepository(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
        /*-----------------------------------------------------*/

        /*  ОСНОВНОЙ КОНТЕНТ  */

        /*---------------------A - все статьи--------------------------------*/
        public ActionResult A(int a, int b, int c, string d)
        {
            #region меню и титл
            ViewData["menu_url"] = "artycles";
            ViewData["logon_url"] = "/C/A/" + a + "/" + b + "/" + c + "/" + d;
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion

            #region настройка сайта
            //количество комментов для активации блога
            ViewData["model_comment"] = _dc.dm_model.Get_model().CountComment;
            //bool разрешены блоги или нет
            ViewData["model_blog"] = _dc.dm_model.Get_model().Blog;
            //bool разрешены новости или нет
            ViewData["model_news"] = _dc.dm_model.Get_model().News;
            //bool разрешены статьи или нет
            ViewData["model_artycle"] = _dc.dm_model.Get_model().Artycle;
            
            #endregion

            #region правая колонка
            //меню пользователя
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                //все комментарии пользователя
                ViewData["comment_user"] = _dc.dm_artycle_comment.Get_сomments_start_user();
                //количество блогов пользователя
                ViewData["blog_user_count"] = _dc.dm_artycle_category.Get_blogs_user().Count();
                //все блдги пользователя
                ViewData["blog_user"] = _dc.dm_artycle_category.Get_blogs_user();
            }
            //рубрики новостей
            ViewData["news_category"] = _dc.dm_artycle_category.Get_category_news();
            //рубрики статей
            ViewData["artycle_category"] = _dc.dm_artycle_category.Get_categories();
            //блоги
            ViewData["blog"] = _dc.dm_artycle_category.Get_blogs();
            //теги
            ViewData["teg"] = _dc.dm_artycle.Get_tegs();
            #endregion

            #region основной контент
            ViewData["count_canegory"] = _dc.dm_artycle_category.Get_categories().Count();
            ViewData["artycle"] = _dc.dm_artycle.Get_artycles_date_true_page(b, c);
            ViewData["category_status"] = a;
            #endregion

            #region пейджинг страницы
            ViewData["content_count"] = _dc.dm_artycle.Get_artycles_date_true().Count();
            ViewData["content_url"] = "/C/A/";
            ViewData["content_int"] = c;
            ViewData["content_cat"] = a;
            ViewData["content_page"] = b;
            ViewData["content_path"] = "Artycles";
            #endregion

            return View();
        }
        [HttpPost]
        public ActionResult A(string search)
        {
            return RedirectToAction("A", "D", new { a = 0, b = 1, c = 10, d = search });
        }

        /*------------B - статьи, посты и новости в определенной рубрике или подрубрике---------------*/
        public ActionResult B(int a, int b, int c, string d)
        {
            #region меню и титл
            ViewData["logon_url"] = "/A/B/" + a + "/" + b + "/" + c + "/" + d;
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion

            #region настройка сайта
            //количество комментов для активации блога
            ViewData["model_comment"] = _dc.dm_model.Get_model().CountComment;
            //bool разрешены блоги или нет
            ViewData["model_blog"] = _dc.dm_model.Get_model().Blog;
            //bool разрешены новости или нет
            ViewData["model_news"] = _dc.dm_model.Get_model().News;
            //bool разрешены статьи или нет
            ViewData["model_artycle"] = _dc.dm_model.Get_model().Artycle;
            
            #endregion

            #region правая колонка
            //меню пользователя
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                //все комментарии пользователя
                ViewData["comment_user"] = _dc.dm_artycle_comment.Get_сomments_start_user();
                //количество блогов пользователя
                ViewData["blog_user_count"] = _dc.dm_artycle_category.Get_blogs_user().Count();
                //все блдги пользователя
                ViewData["blog_user"] = _dc.dm_artycle_category.Get_blogs_user();
            }
            //рубрики новостей
            ViewData["news_category"] = _dc.dm_artycle_category.Get_category_news();
            //рубрики статей
            ViewData["artycle_category"] = _dc.dm_artycle_category.Get_categories();
            //блоги
            ViewData["blog"] = _dc.dm_artycle_category.Get_blogs();
            //теги
            ViewData["teg"] = _dc.dm_artycle.Get_tegs();
            #endregion

            #region основной контент
            mt_artycle_category x = _dc.dm_artycle_category.Get_category(a);
            ViewData.Model = x;
            ViewData["artycle"] = _dc.dm_artycle.Get_artycles_from_category_date_true_page(a, b, c);
            if (x.Blog == true)
            {
                ViewData["menu_url"] = "blogs";
                _dc.dm_artycle_category.Views_blog(a);
            }
            if (x.News == true) { ViewData["menu_url"] = "news"; } else {
                if (x.Blog == false) { ViewData["menu_url"] = "artycles"; }
            }
            
            ViewData["category_status"] = b;
            #endregion

            #region пейджинг страницы
            ViewData["content_count"] = _dc.dm_artycle.Get_artycles_from_category_date_true(a).Count();
            ViewData["content_url"] = "/C/B/";
            ViewData["content_int"] = c;
            ViewData["content_cat"] = a;
            ViewData["content_page"] = b;
            ViewData["content_path"] = _dc.dm_artycle_category.Get_category(a).Path;
            #endregion

            return View();
        }
        [HttpPost]
        public ActionResult B(string search)
        {
            return RedirectToAction("A", "D", new { a = 0, b = 1, c = 10, d = search });
        }

        /*---------------------C - одна статья, новость или пост------------------------*/
        public ActionResult C(int a, string b)
        {

            #region меню и титл
            ViewData["logon_url"] = "/C/C/" + a + "/" + b;
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion

            #region настройка сайта
            //количество комментов для активации блога
            ViewData["model_comment"] = _dc.dm_model.Get_model().CountComment;
            //bool разрешены блоги или нет
            ViewData["model_blog"] = _dc.dm_model.Get_model().Blog;
            //bool разрешены новости или нет
            ViewData["model_news"] = _dc.dm_model.Get_model().News;
            //bool разрешены статьи или нет
            ViewData["model_artycle"] = _dc.dm_model.Get_model().Artycle;
            
            #endregion

            #region правая колонка
            //меню пользователя
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                //все комментарии пользователя
                ViewData["comment_user"] = _dc.dm_artycle_comment.Get_сomments_start_user();
                //количество блогов пользователя
                ViewData["blog_user_count"] = _dc.dm_artycle_category.Get_blogs_user().Count();
                //все блдги пользователя
                ViewData["blog_user"] = _dc.dm_artycle_category.Get_blogs_user();
            }
            //рубрики новостей
            ViewData["news_category"] = _dc.dm_artycle_category.Get_category_news();
            //рубрики статей
            ViewData["artycle_category"] = _dc.dm_artycle_category.Get_categories();
            //блоги
            ViewData["blog"] = _dc.dm_artycle_category.Get_blogs();
            //теги
            ViewData["teg"] = _dc.dm_artycle.Get_tegs();
            #endregion

            #region основной контент
            if (HttpContext.User.Identity.IsAuthenticated) { }
            else
            {
                if (_dc.dm_artycle.Get_artycle(a).RegistrUser == true)
                {
                    string urla = "/C/C/" + a + "/" + b;
                    return RedirectToAction("A", "B", new { returnUrl = urla });
                }
            }
            mt_artycle x = _dc.dm_artycle.Get_artycle(a);
            ViewData.Model = x;
            _dc.dm_artycle.Views_artycle(a);
            if (x.AddComment == true)
            {
                ViewData["artycle_comment"] = _dc.dm_artycle_comment.Get_сomments_from_artycle_start(a);
            }
            if (x.mt_artycle_category.Blog == true)
            {
                ViewData["menu_url"] = "blogs";
               
            }
            if (x.mt_artycle_category.News == true) { ViewData["menu_url"] = "news"; }
            else
            {
                if (x.mt_artycle_category.Blog == false) { ViewData["menu_url"] = "artycles"; }
            }
           
            #endregion

            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult C(int a, string b, string comment)
        {
            if (Request.IsAjaxRequest()) {
                if (comment == null)
                {
                    _dc.dm_artycle.Votes_artycle_plus(a);
                }
                else { _dc.dm_artycle.Votes_artycle_minus(a); }
                string votes = _dc.dm_artycle.Get_artycle(a).Votes.ToString();
                return Content(votes);
            }
            else
            {
                #region проверка данных
                if (comment == null)
                {
                    ModelState.AddModelError("comment", "Комментарий должен содержать не менее 2 символов.");
                }
                else
                {
                    if (comment.Length < 2)
                        ModelState.AddModelError("comment", "Комментарий должен содержать не менее 2 символов.");
                    if (comment.Length > 1000)
                        ModelState.AddModelError("comment", "Комментарий должен содержать не более 1000 символов.");
                    if (comment.IndexOf("script") != -1)
                        ModelState.AddModelError("comment", "Комментарий не может содержать: script");
                    if (comment.IndexOf("object") != -1)
                        ModelState.AddModelError("comment", "Комментарий не может содержать: object");
                    if (comment.IndexOf("applet") != -1)
                        ModelState.AddModelError("comment", "Комментарий не может содержать: applet");
                    if (comment.IndexOf("embed") != -1)
                        ModelState.AddModelError("comment", "Комментарий не может содержать: embed");
                    if (comment.IndexOf("<%") != -1)
                        ModelState.AddModelError("comment", "Комментарий не может содержать: <%%>");
                    if (comment.IndexOf("$") != -1)
                        ModelState.AddModelError("comment", "Комментарий не может содержать: $");
                }

                #endregion

                if (ModelState.IsValid)
                {
                    _dc.dm_artycle_comment.Create_сomment(a, comment);
                    return RedirectToAction("C", new { a, b });
                }
                #region меню и титл
                ViewData["logon_url"] = "/C/C/" + a + "/" + b;
                ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
                ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
                ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
                //bool включена captcha
                ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
                #endregion

                #region настройка сайта
                //количество комментов для активации блога
                ViewData["model_comment"] = _dc.dm_model.Get_model().CountComment;
                //bool разрешены блоги или нет
                ViewData["model_blog"] = _dc.dm_model.Get_model().Blog;
                //bool разрешены новости или нет
                ViewData["model_news"] = _dc.dm_model.Get_model().News;
                //bool разрешены статьи или нет
                ViewData["model_artycle"] = _dc.dm_model.Get_model().Artycle;

                #endregion

                #region правая колонка
                //меню пользователя
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    //все комментарии пользователя
                    ViewData["comment_user"] = _dc.dm_artycle_comment.Get_сomments_start_user();
                    //количество блогов пользователя
                    ViewData["blog_user_count"] = _dc.dm_artycle_category.Get_blogs_user().Count();
                    //все блдги пользователя
                    ViewData["blog_user"] = _dc.dm_artycle_category.Get_blogs_user();
                }
                //рубрики новостей
                ViewData["news_category"] = _dc.dm_artycle_category.Get_category_news();
                //рубрики статей
                ViewData["artycle_category"] = _dc.dm_artycle_category.Get_categories();
                //блоги
                ViewData["blog"] = _dc.dm_artycle_category.Get_blogs();
                //теги
                ViewData["teg"] = _dc.dm_artycle.Get_tegs();
                #endregion

                #region основной контент
                if (HttpContext.User.Identity.IsAuthenticated) { }
                else
                {
                    if (_dc.dm_artycle.Get_artycle(a).RegistrUser == true)
                    {
                        string urla = "/C/C/" + a + "/" + b;
                        return RedirectToAction("A", "B", new { returnUrl = urla });
                    }
                }
                mt_artycle x = _dc.dm_artycle.Get_artycle(a);
                ViewData.Model = x;
                _dc.dm_artycle.Views_artycle(a);
                if (x.AddComment == true)
                {
                    ViewData["artycle_comment"] = _dc.dm_artycle_comment.Get_сomments_from_artycle_start(a);
                }
                if (x.mt_artycle_category.Blog == true)
                {
                    ViewData["menu_url"] = "blogs";

                }
                if (x.mt_artycle_category.News == true) { ViewData["menu_url"] = "news"; }
                else
                {
                    if (x.mt_artycle_category.Blog == false) { ViewData["menu_url"] = "artycles"; }
                }
                
                #endregion
                return View();

            }
        }

        /*--------------------D - все блоги пользователей-----------------------*/
        public ActionResult D(int a, int b, int c, string d)
        {
            #region меню и титл
            ViewData["menu_url"] = "blogs";
            ViewData["logon_url"] = "/C/D/" + a + "/" + b + "/" + c + "/" + d;
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion

            #region настройка сайта
            //количество комментов для активации блога
            ViewData["model_comment"] = _dc.dm_model.Get_model().CountComment;
            //bool разрешены блоги или нет
            ViewData["model_blog"] = _dc.dm_model.Get_model().Blog;
            //bool разрешены новости или нет
            ViewData["model_news"] = _dc.dm_model.Get_model().News;
            //bool разрешены статьи или нет
            ViewData["model_artycle"] = _dc.dm_model.Get_model().Artycle;
            
            #endregion

            #region правая колонка
            //меню пользователя
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                //все комментарии пользователя
                ViewData["comment_user"] = _dc.dm_artycle_comment.Get_сomments_start_user();
                //количество блогов пользователя
                ViewData["blog_user_count"] = _dc.dm_artycle_category.Get_blogs_user().Count();
                //все блдги пользователя
                ViewData["blog_user"] = _dc.dm_artycle_category.Get_blogs_user();
            }
            //рубрики новостей
            ViewData["news_category"] = _dc.dm_artycle_category.Get_category_news();
            //рубрики статей
            ViewData["artycle_category"] = _dc.dm_artycle_category.Get_categories();
            //блоги
            ViewData["blog"] = _dc.dm_artycle_category.Get_blogs();
            //теги
            ViewData["teg"] = _dc.dm_artycle.Get_tegs();
            #endregion

            #region основной контент
            ViewData["artycle"] = _dc.dm_artycle_category.Get_blogs_page(b, c);
            ViewData["category_status"] = a;
            #endregion

            #region пейджинг страницы
            ViewData["content_count"] = _dc.dm_artycle_category.Get_blogs().Count();
            ViewData["content_url"] = "/C/D/";
            ViewData["content_int"] = c;
            ViewData["content_cat"] = a;
            ViewData["content_page"] = b;
            ViewData["content_path"] = "Blogs";
            #endregion

            return View();
        }
        [HttpPost]
        public ActionResult D(string search)
        {
            return RedirectToAction("A", "D", new { a = 0, b = 1, c = 10, d = search });
        }

        /*-----------------E - все статьи новости и блоги из определенного тега---------------------*/
        public ActionResult E(int a, int b, int c, string d)
        {
            #region меню и титл
            ViewData["logon_url"] = "/C/E/" + a + "/" + b + "/" + c + "/" + d;
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion

            #region настройка сайта
            ViewData["menu_url"] = "tegs";
            //количество комментов для активации блога
            ViewData["model_comment"] = _dc.dm_model.Get_model().CountComment;
            //bool разрешены блоги или нет
            ViewData["model_blog"] = _dc.dm_model.Get_model().Blog;
            //bool разрешены новости или нет
            ViewData["model_news"] = _dc.dm_model.Get_model().News;
            //bool разрешены статьи или нет
            ViewData["model_artycle"] = _dc.dm_model.Get_model().Artycle;
            
            #endregion

            #region правая колонка
            //меню пользователя
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                //все комментарии пользователя
                ViewData["comment_user"] = _dc.dm_artycle_comment.Get_сomments_start_user();
                //количество блогов пользователя
                ViewData["blog_user_count"] = _dc.dm_artycle_category.Get_blogs_user().Count();
                //все блдги пользователя
                ViewData["blog_user"] = _dc.dm_artycle_category.Get_blogs_user();
            }
            //рубрики новостей
            ViewData["news_category"] = _dc.dm_artycle_category.Get_category_news();
            //рубрики статей
            ViewData["artycle_category"] = _dc.dm_artycle_category.Get_categories();
            //блоги
            ViewData["blog"] = _dc.dm_artycle_category.Get_blogs();
            //теги
            ViewData["teg"] = _dc.dm_artycle.Get_tegs();
            #endregion

            #region основной контент
            mt_artycle_teg x = _dc.dm_artycle.Get_teg(a);
            ViewData.Model = x;
            if (_dc.dm_model.Get_model().Blog == true)
            {
                if (_dc.dm_model.Get_model().News == true)
                {
                    ViewData["artycle"] = _dc.dm_artycle.Get_artycles_blog_news_from_teg_date_true_page(a, b, c);
                    ViewData["content_count"] = _dc.dm_artycle.Get_artycles_blog_news_from_teg_date_true(a).Count();
                }
                else
                {
                    ViewData["artycle"] = _dc.dm_artycle.Get_artycles_blog_from_teg_date_true_page(a, b, c);
                    ViewData["content_count"] = _dc.dm_artycle.Get_artycles_blog_from_teg_date_true(a).Count();
                }
            }
            else
            {
                if (_dc.dm_model.Get_model().News == true)
                {
                    ViewData["artycle"] = _dc.dm_artycle.Get_artycles_news_from_teg_date_true_page(a, b, c);
                    ViewData["content_count"] = _dc.dm_artycle.Get_artycles_news_from_teg_date_true(a).Count();
                }
                else
                {
                    ViewData["artycle"] = _dc.dm_artycle.Get_artycles_from_teg_date_true_page(a, b, c);
                    ViewData["content_count"] = _dc.dm_artycle.Get_artycles_from_teg_date_true(a).Count();
                }

            }
            #endregion

            #region пейджинг страницы
            //ViewData["content_count"] = перенесено в основной контент
            ViewData["content_url"] = "/C/E/";
            ViewData["content_int"] = c;
            ViewData["content_cat"] = a;
            ViewData["content_page"] = b;
            ViewData["content_path"] = _dc.dm_artycle.Get_teg(a).Path;
            #endregion

            return View();
        }
        [HttpPost]
        public ActionResult E(string search)
        {
            return RedirectToAction("A", "D", new { a = 0, b = 1, c = 10, d = search });
        }

        /*-------------------F - все новости-----------------------*/
        public ActionResult F(int a, int b, int c, string d)
        {
            #region меню и титл
            ViewData["menu_url"] = "news";
            ViewData["logon_url"] = "/C/F/" + a + "/" + b + "/" + c + "/" + d;
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion

            #region настройка сайта
            //количество комментов для активации блога
            ViewData["model_comment"] = _dc.dm_model.Get_model().CountComment;
            //bool разрешены блоги или нет
            ViewData["model_blog"] = _dc.dm_model.Get_model().Blog;
            //bool разрешены новости или нет
            ViewData["model_news"] = _dc.dm_model.Get_model().News;
            //bool разрешены статьи или нет
            ViewData["model_artycle"] = _dc.dm_model.Get_model().Artycle;
            
            #endregion

            #region правая колонка
            //меню пользователя
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                //все комментарии пользователя
                ViewData["comment_user"] = _dc.dm_artycle_comment.Get_сomments_start_user();
                //количество блогов пользователя
                ViewData["blog_user_count"] = _dc.dm_artycle_category.Get_blogs_user().Count();
                //все блдги пользователя
                ViewData["blog_user"] = _dc.dm_artycle_category.Get_blogs_user();
            }
            //рубрики новостей
            ViewData["news_category"] = _dc.dm_artycle_category.Get_category_news();
            //рубрики статей
            ViewData["artycle_category"] = _dc.dm_artycle_category.Get_categories();
            //блоги
            ViewData["blog"] = _dc.dm_artycle_category.Get_blogs();
            //теги
            ViewData["teg"] = _dc.dm_artycle.Get_tegs();
            #endregion

            #region основной контент
            ViewData["count_canegory"] = _dc.dm_artycle_category.Get_category_news().Count();
            ViewData["artycle"] = _dc.dm_artycle.Get_news_date_true_page(b, c);
            ViewData["category_status"] = a;
            #endregion

            #region пейджинг страницы
            ViewData["content_count"] = _dc.dm_artycle.Get_news_date_true().Count();
            ViewData["content_url"] = "/C/F/";
            ViewData["content_int"] = c;
            ViewData["content_cat"] = a;
            ViewData["content_page"] = b;
            ViewData["content_path"] = "News";
            #endregion

            return View();
        }
        [HttpPost]
        public ActionResult F(string search)
        {
            return RedirectToAction("A", "D", new { a = 0, b = 1, c = 10, d = search });
        }
        /*-----------------------------------------------------*/

        /*  ДОБАВЛЕНИЕ, РЕДАКТИРОВАНИЕ, УДАЛЕНИЕ  */

        /*----------------YY – создать тему блога-----------------*/
        [Authorize(Roles = "blogger, chief_editor")]
        public ActionResult YY(int a)
        {
            #region меню и титл
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion
            #region настройка сайта
            ViewData["menu_url"] = "thema_blog";            
            //bool разрешены блоги или нет
            ViewData["model_blog"] = _dc.dm_model.Get_model().Blog;
            //bool разрешены новости или нет
            ViewData["model_news"] = _dc.dm_model.Get_model().News;
            //bool разрешены статьи или нет
            ViewData["model_artycle"] = _dc.dm_model.Get_model().Artycle;

            #endregion

            ViewData["url"] = "/C/A/0/1/10/Artycles";
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult YY(string title, int a)
        {
            #region проверка данных
            if (title == null)
            {
                ModelState.AddModelError("Title", "Название должно содержать не менее 3 символов.");
            }
            else
            {
                if (title.Length <= 2)
                    ModelState.AddModelError("Title", "Название должно содержать не менее 3 символов.");
                if (title.Length > 256)
                    ModelState.AddModelError("Title", "Название должно содержать не более 256 символов.");
                if (title.IndexOf("script") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: script");
                if (title.IndexOf("object") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: object");
                if (title.IndexOf("applet") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: applet");
                if (title.IndexOf("embed") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: embed");
                if (title.IndexOf("<%") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: <%%>");
                if (title.IndexOf("$") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: $");
            }

            #endregion

            if (ModelState.IsValid)
            {

                _dc.dm_artycle_category.Create_re_blog(title, a);
                int ad = _dc.dm_artycle_category.Get_blog_first().Id;
                string d = _dc.dm_artycle_category.Get_blog_first().Path;
                return RedirectToAction("B", new { a = ad, b = 1, c = 10, d });
            }
            #region меню и титл
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion
            #region настройка сайта
            ViewData["menu_url"] = "thema_blog";
            //bool разрешены блоги или нет
            ViewData["model_blog"] = _dc.dm_model.Get_model().Blog;
            //bool разрешены новости или нет
            ViewData["model_news"] = _dc.dm_model.Get_model().News;
            //bool разрешены статьи или нет
            ViewData["model_artycle"] = _dc.dm_model.Get_model().Artycle;

            #endregion
            ViewData["url"] = "/C/A/0/1/10/Artycles";
            return View();

        }

        /*----------------YZ – создать подрубрику новостей-----------------*/
        [Authorize(Roles = "artycle_editor, chief_editor")]
        public ActionResult YZ(int a)
        {
            #region меню и титл
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion
            #region настройка сайта
            ViewData["menu_url"] = "thema_news";
            //bool разрешены блоги или нет
            ViewData["model_blog"] = _dc.dm_model.Get_model().Blog;
            //bool разрешены новости или нет
            ViewData["model_news"] = _dc.dm_model.Get_model().News;
            //bool разрешены статьи или нет
            ViewData["model_artycle"] = _dc.dm_model.Get_model().Artycle;

            #endregion
            ViewData["url"] = "/C/A/0/1/10/Artycles";
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult YZ(string title, int a)
        {
            #region проверка данных
            if (title == null)
            {
                ModelState.AddModelError("Title", "Название должно содержать не менее 3 символов.");
            }
            else
            {
                if (title.Length <= 2)
                    ModelState.AddModelError("Title", "Название должно содержать не менее 3 символов.");
                if (title.Length > 256)
                    ModelState.AddModelError("Title", "Название должно содержать не более 256 символов.");
                if (title.IndexOf("script") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: script");
                if (title.IndexOf("object") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: object");
                if (title.IndexOf("applet") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: applet");
                if (title.IndexOf("embed") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: embed");
                if (title.IndexOf("<%") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: <%%>");
                if (title.IndexOf("$") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: $");
            }

            #endregion

            if (ModelState.IsValid)
            {

                _dc.dm_artycle_category.Create_re_category_news(title, a);
                int ad = _dc.dm_artycle_category.Get_blog_first().Id;
                string d = _dc.dm_artycle_category.Get_blog_first().Path;
                return RedirectToAction("B", new { a = ad, b = 1, c = 10, d });
            }
            #region меню и титл
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion
            #region настройка сайта
            ViewData["menu_url"] = "thema_news";
            //bool разрешены блоги или нет
            ViewData["model_blog"] = _dc.dm_model.Get_model().Blog;
            //bool разрешены новости или нет
            ViewData["model_news"] = _dc.dm_model.Get_model().News;
            //bool разрешены статьи или нет
            ViewData["model_artycle"] = _dc.dm_model.Get_model().Artycle;

            #endregion
            ViewData["url"] = "/C/A/0/1/10/Artycles";
            return View();

        }

        /*----------------ZA - создать рубрику статей---------------------*/
        [Authorize(Roles = "artycle_editor, chief_editor")]
        public ActionResult ZA()
        {
            #region меню и титл
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion
            #region настройка сайта
            ViewData["menu_url"] = "rub_artycle";
            //bool разрешены блоги или нет
            ViewData["model_blog"] = _dc.dm_model.Get_model().Blog;
            //bool разрешены новости или нет
            ViewData["model_news"] = _dc.dm_model.Get_model().News;
            //bool разрешены статьи или нет
            ViewData["model_artycle"] = _dc.dm_model.Get_model().Artycle;

            #endregion
            ViewData["url"] = "/C/A/0/1/10/Artycles";
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ZA(string title, bool addmenu)
        {
            #region проверка данных
            if (title == null)
            {
                ModelState.AddModelError("Title", "Название должно содержать не менее 3 символов.");
            }
            else
            {
                if (title.Length <= 2)
                    ModelState.AddModelError("Title", "Название должно содержать не менее 3 символов.");
                if (title.Length > 256)
                    ModelState.AddModelError("Title", "Название должно содержать не более 256 символов.");
                if (title.IndexOf("script") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: script");
                if (title.IndexOf("object") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: object");
                if (title.IndexOf("applet") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: applet");
                if (title.IndexOf("embed") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: embed");
                if (title.IndexOf("<%") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: <%%>");
                if (title.IndexOf("$") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: $");
            }

            #endregion

            if (ModelState.IsValid)
            {

                _dc.dm_artycle_category.Create_category(title, addmenu);
                int a = _dc.dm_artycle_category.Get_blog_first().Id;
                string d = _dc.dm_artycle_category.Get_blog_first().Path;
                return RedirectToAction("B", new { a, b = 1, c = 10, d });
            }
            #region меню и титл
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion
            #region настройка сайта
            ViewData["menu_url"] = "rub_artycle";
            //bool разрешены блоги или нет
            ViewData["model_blog"] = _dc.dm_model.Get_model().Blog;
            //bool разрешены новости или нет
            ViewData["model_news"] = _dc.dm_model.Get_model().News;
            //bool разрешены статьи или нет
            ViewData["model_artycle"] = _dc.dm_model.Get_model().Artycle;

            #endregion
            ViewData["url"] = "/C/A/0/1/10/Artycles";
            return View();

        }

        /*--------------------ZB - редактировать рубрику статей------------------------*/
        [Authorize(Roles = "artycle_editor, chief_editor")]
        public ActionResult ZB(int a)
        {
            #region меню и титл
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion
            #region настройка сайта
            ViewData["menu_url"] = "edit_rub_artycle";
            //bool разрешены блоги или нет
            ViewData["model_blog"] = _dc.dm_model.Get_model().Blog;
            //bool разрешены новости или нет
            ViewData["model_news"] = _dc.dm_model.Get_model().News;
            //bool разрешены статьи или нет
            ViewData["model_artycle"] = _dc.dm_model.Get_model().Artycle;

            #endregion

            mt_artycle_category x = _dc.dm_artycle_category.Get_category(a);
            ViewData.Model = x;
            ViewData["url"] = "/C/B/" + a + "/1/10/" + x.Path;
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ZB(int a, string title, bool addmenu)
        {
            #region проверка данных
            if (title == null)
            {
                ModelState.AddModelError("Title", "Название должно содержать не менее 3 символов.");
            }
            else
            {
                if (title.Length <= 2)
                    ModelState.AddModelError("Title", "Название должно содержать не менее 3 символов.");
                if (title.Length > 256)
                    ModelState.AddModelError("Title", "Название должно содержать не более 256 символов.");
                if (title.IndexOf("script") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: script");
                if (title.IndexOf("object") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: object");
                if (title.IndexOf("applet") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: applet");
                if (title.IndexOf("embed") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: embed");
                if (title.IndexOf("<%") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: <%%>");
                if (title.IndexOf("$") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: $");
            }

            #endregion

            if (ModelState.IsValid)
            {

                _dc.dm_artycle_category.Update_category(a, title, addmenu);
                string d = _dc.dm_artycle_category.Get_category(a).Path;
                return RedirectToAction("B", new { a, b = 1, c = 10, d });
            }
            #region меню и титл
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion
            #region настройка сайта
            ViewData["menu_url"] = "edit_rub_artycle";
            //bool разрешены блоги или нет
            ViewData["model_blog"] = _dc.dm_model.Get_model().Blog;
            //bool разрешены новости или нет
            ViewData["model_news"] = _dc.dm_model.Get_model().News;
            //bool разрешены статьи или нет
            ViewData["model_artycle"] = _dc.dm_model.Get_model().Artycle;

            #endregion
            mt_artycle_category x = _dc.dm_artycle_category.Get_category(a);
            ViewData.Model = x;
            ViewData["url"] = "/C/B/" + a + "/1/10/" + x.Path;
            return View();

        }

        /*--------------------ZC - создать подрубрику в рубрике статей---------------------*/
        [Authorize(Roles = "artycle_editor, chief_editor")]
        public ActionResult ZC(int a)
        {
            #region меню и титл
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion
            #region настройка сайта
            ViewData["menu_url"] = "thema_news";
            //bool разрешены блоги или нет
            ViewData["model_blog"] = _dc.dm_model.Get_model().Blog;
            //bool разрешены новости или нет
            ViewData["model_news"] = _dc.dm_model.Get_model().News;
            //bool разрешены статьи или нет
            ViewData["model_artycle"] = _dc.dm_model.Get_model().Artycle;

            #endregion
            ViewData["url"] = "/C/A/0/1/10/Artycles";
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ZC(string title, int a)
        {
            #region проверка данных
            if (title == null)
            {
                ModelState.AddModelError("Title", "Название должно содержать не менее 3 символов.");
            }
            else
            {
                if (title.Length <= 2)
                    ModelState.AddModelError("Title", "Название должно содержать не менее 3 символов.");
                if (title.Length > 256)
                    ModelState.AddModelError("Title", "Название должно содержать не более 256 символов.");
                if (title.IndexOf("script") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: script");
                if (title.IndexOf("object") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: object");
                if (title.IndexOf("applet") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: applet");
                if (title.IndexOf("embed") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: embed");
                if (title.IndexOf("<%") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: <%%>");
                if (title.IndexOf("$") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: $");
            }

            #endregion

            if (ModelState.IsValid)
            {

                _dc.dm_artycle_category.Create_re_category(title, a);
                int ad = _dc.dm_artycle_category.Get_blog_first().Id;
                string d = _dc.dm_artycle_category.Get_blog_first().Path;
                return RedirectToAction("B", new { a = ad, b = 1, c = 10, d });
            }
            #region меню и титл
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion
            #region настройка сайта
            ViewData["menu_url"] = "thema_news";
            //bool разрешены блоги или нет
            ViewData["model_blog"] = _dc.dm_model.Get_model().Blog;
            //bool разрешены новости или нет
            ViewData["model_news"] = _dc.dm_model.Get_model().News;
            //bool разрешены статьи или нет
            ViewData["model_artycle"] = _dc.dm_model.Get_model().Artycle;

            #endregion
            ViewData["url"] = "/C/A/0/1/10/Artycles";
            return View();

        }

        /*----------------------ZD - редактировать подрубрику--------------------------*/
        [Authorize(Roles = "artycle_editor, chief_editor")]
        public ActionResult ZD(int a)
        {
            #region меню и титл
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion
            #region настройка сайта
            ViewData["menu_url"] = "edit_thema_news";
            //bool разрешены блоги или нет
            ViewData["model_blog"] = _dc.dm_model.Get_model().Blog;
            //bool разрешены новости или нет
            ViewData["model_news"] = _dc.dm_model.Get_model().News;
            //bool разрешены статьи или нет
            ViewData["model_artycle"] = _dc.dm_model.Get_model().Artycle;

            #endregion

            mt_artycle_category x = _dc.dm_artycle_category.Get_category(a);
            ViewData.Model = x;
            ViewData["url"] = "/C/B/" + a + "/1/10/" + x.Path;
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ZD(int a, string title)
        {
            #region проверка данных
            if (title == null)
            {
                ModelState.AddModelError("Title", "Название должно содержать не менее 3 символов.");
            }
            else
            {
                if (title.Length <= 2)
                    ModelState.AddModelError("Title", "Название должно содержать не менее 3 символов.");
                if (title.Length > 256)
                    ModelState.AddModelError("Title", "Название должно содержать не более 256 символов.");
                if (title.IndexOf("script") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: script");
                if (title.IndexOf("object") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: object");
                if (title.IndexOf("applet") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: applet");
                if (title.IndexOf("embed") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: embed");
                if (title.IndexOf("<%") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: <%%>");
                if (title.IndexOf("$") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: $");
            }

            #endregion

            if (ModelState.IsValid)
            {

                _dc.dm_artycle_category.Update_blog(a, title);
                string d = _dc.dm_artycle_category.Get_category(a).Path;
                return RedirectToAction("B", new { a, b = 1, c = 10, d });
            }
            #region меню и титл
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion
            #region настройка сайта
            ViewData["menu_url"] = "edit_thema_news";
            //bool разрешены блоги или нет
            ViewData["model_blog"] = _dc.dm_model.Get_model().Blog;
            //bool разрешены новости или нет
            ViewData["model_news"] = _dc.dm_model.Get_model().News;
            //bool разрешены статьи или нет
            ViewData["model_artycle"] = _dc.dm_model.Get_model().Artycle;

            #endregion
            mt_artycle_category x = _dc.dm_artycle_category.Get_category(a);
            ViewData.Model = x;
            ViewData["url"] = "/C/B/" + a + "/1/10/" + x.Path;
            return View();

        }

        /*-----------------ZE - создать рубрику новостей---------------------*/
        [Authorize(Roles = "artycle_editor, chief_editor")]
        public ActionResult ZE()
        {
            #region меню и титл
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion
            #region настройка сайта
            ViewData["menu_url"] = "rub_artycle";
            //bool разрешены блоги или нет
            ViewData["model_blog"] = _dc.dm_model.Get_model().Blog;
            //bool разрешены новости или нет
            ViewData["model_news"] = _dc.dm_model.Get_model().News;
            //bool разрешены статьи или нет
            ViewData["model_artycle"] = _dc.dm_model.Get_model().Artycle;

            #endregion
            ViewData["url"] = "/C/F/0/1/10/News";
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ZE(string title, bool addmenu)
        {
            #region проверка данных
            if (title == null)
            {
                ModelState.AddModelError("Title", "Название должно содержать не менее 3 символов.");
            }
            else
            {
                if (title.Length <= 2)
                    ModelState.AddModelError("Title", "Название должно содержать не менее 3 символов.");
                if (title.Length > 256)
                    ModelState.AddModelError("Title", "Название должно содержать не более 256 символов.");
                if (title.IndexOf("script") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: script");
                if (title.IndexOf("object") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: object");
                if (title.IndexOf("applet") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: applet");
                if (title.IndexOf("embed") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: embed");
                if (title.IndexOf("<%") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: <%%>");
                if (title.IndexOf("$") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: $");
            }

            #endregion

            if (ModelState.IsValid)
            {

                _dc.dm_artycle_category.Create_category_news(title, addmenu);
                int a = _dc.dm_artycle_category.Get_blog_first().Id;
                string d = _dc.dm_artycle_category.Get_blog_first().Path;
                return RedirectToAction("B", new { a, b = 1, c = 10, d });
            }
            #region меню и титл
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion
            #region настройка сайта
            ViewData["menu_url"] = "rub_artycle";
            //bool разрешены блоги или нет
            ViewData["model_blog"] = _dc.dm_model.Get_model().Blog;
            //bool разрешены новости или нет
            ViewData["model_news"] = _dc.dm_model.Get_model().News;
            //bool разрешены статьи или нет
            ViewData["model_artycle"] = _dc.dm_model.Get_model().Artycle;

            #endregion
            ViewData["url"] = "/C/F/0/1/10/News";
            return View();

        }

        /*-----------------ZF - создать блог пользователя ---------------------*/
        [Authorize]
        public ActionResult ZF()
        {

            _dc.dm_artycle_category.Create_blog();
            if (HttpContext.User.IsInRole("blogger")) { }
            else
            {
                Roles.AddUserToRole(HttpContext.User.Identity.Name, "blogger");
            }
            int a = _dc.dm_artycle_category.Get_blog_first().Id;
            string d = _dc.dm_artycle_category.Get_blog_first().Path;
            return RedirectToAction("B", new { a, b = 1, c = 10, d });

        }

        /*-------------------ZG - редактировать блог----------------------*/
        [Authorize(Roles = "blogger, chief_editor")]
        public ActionResult ZG(int a)
        {
            #region меню и титл
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion
            #region настройка сайта
            ViewData["menu_url"] = "edit_blog";
            //bool разрешены блоги или нет
            ViewData["model_blog"] = _dc.dm_model.Get_model().Blog;
            //bool разрешены новости или нет
            ViewData["model_news"] = _dc.dm_model.Get_model().News;
            //bool разрешены статьи или нет
            ViewData["model_artycle"] = _dc.dm_model.Get_model().Artycle;

            #endregion

            mt_artycle_category x = _dc.dm_artycle_category.Get_category(a);
            ViewData.Model = x;
            ViewData["url"] = "/C/B/" + a + "/1/10/" + x.Path;
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ZG(int a, string title)
        {
            #region проверка данных
            if (title == null)
            {
                ModelState.AddModelError("Title", "Название должно содержать не менее 3 символов.");
            }
            else
            {
                if (title.Length <= 2)
                    ModelState.AddModelError("Title", "Название должно содержать не менее 3 символов.");
                if (title.Length > 256)
                    ModelState.AddModelError("Title", "Название должно содержать не более 256 символов.");
                if (title.IndexOf("script") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: script");
                if (title.IndexOf("object") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: object");
                if (title.IndexOf("applet") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: applet");
                if (title.IndexOf("embed") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: embed");
                if (title.IndexOf("<%") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: <%%>");
                if (title.IndexOf("$") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: $");
            }

            #endregion

            if (ModelState.IsValid)
            {

                _dc.dm_artycle_category.Update_blog(a, title);
                string d = _dc.dm_artycle_category.Get_category(a).Path;
                return RedirectToAction("B", new { a, b = 1, c = 10, d });
            }
            #region меню и титл
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion
            #region настройка сайта
            ViewData["menu_url"] = "edit_blog";
            //bool разрешены блоги или нет
            ViewData["model_blog"] = _dc.dm_model.Get_model().Blog;
            //bool разрешены новости или нет
            ViewData["model_news"] = _dc.dm_model.Get_model().News;
            //bool разрешены статьи или нет
            ViewData["model_artycle"] = _dc.dm_model.Get_model().Artycle;

            #endregion
            mt_artycle_category x = _dc.dm_artycle_category.Get_category(a);
            ViewData.Model = x;
            ViewData["url"] = "/C/B/" + a + "/1/10/" + x.Path;
            return View();

        }

        /*------------------ZH - удалить рубрику статей и новостей ---------------------*/
        [Authorize(Roles = "artycle_editor, chief_editor")]
        public ActionResult ZH(int a)
        {
            string def = _dc.dm_artycle_category.Get_category(a).AddedBy;
            if (HttpContext.User.IsInRole("artycle_editor"))
            {
                if (HttpContext.User.Identity.Name == def)
                {
                    _dc.dm_artycle_category.Delete_category(a);
                    return RedirectToAction("A", new { a = 0, b = 1, c = 10, d = "Artycles" });
                }

            }
            if (HttpContext.User.IsInRole("chief_editor"))
            {

                _dc.dm_artycle_category.Delete_category(a);
                return RedirectToAction("A", new { a = 0, b = 1, c = 10, d = "Artycles" });


            }
            return RedirectToAction("Error", "Home");

        }

        /*------------------ZI - удалить блог ----------------------*/
        [Authorize(Roles = "blogger, chief_editor")]
        public ActionResult ZI(int a)
        {
            string def = _dc.dm_artycle_category.Get_category(a).AddedBy;
            if (HttpContext.User.IsInRole("blogger"))
            {
                if (HttpContext.User.Identity.Name == def)
                {
                    _dc.dm_artycle_category.Delete_category(a);
                    return RedirectToAction("A", new { a = 0, b = 1, c = 10, d = "Artycles" });
                }

            }
            if (HttpContext.User.IsInRole("chief_editor"))
            {

                _dc.dm_artycle_category.Delete_category(a);
                return RedirectToAction("A", new { a = 0, b = 1, c = 10, d = "Artycles" });


            }
            return RedirectToAction("Error", "Home");

        }

        /*-----------------ZJ - создать статью ---------------------*/
        [Authorize(Roles = "artycle_editor, chief_editor")]
        public ActionResult ZJ(int a)
        {
            #region меню и титл
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion
            #region настройка сайта
            ViewData["menu_url"] = "create_artycle";
            //bool разрешены блоги или нет
            ViewData["model_blog"] = _dc.dm_model.Get_model().Blog;
            //bool разрешены новости или нет
            ViewData["model_news"] = _dc.dm_model.Get_model().News;
            //bool разрешены статьи или нет
            ViewData["model_artycle"] = _dc.dm_model.Get_model().Artycle;

            #endregion
            if (a == 0)
            {
                ViewData["Category"] = new SelectList(_dc.dm_artycle_category.Get_categories(), "Id", "Title");
                ViewData["url"] = "/C/A/0/1/10/Artycles";
            }
            else
            {
                if (_dc.dm_artycle_category.Get_category(a).CategoryId == 0)
                {
                    ViewData["Category"] = new SelectList(_dc.dm_artycle_category.Get_categories(), "Id", "Title", a);
                }
                else
                {
                    int fjk = _dc.dm_artycle_category.Get_category(a).mt_artycle_category1.Id;
                    ViewData["Category"] = new SelectList(_dc.dm_artycle_category.Get_re_categories(fjk), "Id", "Title", a);

                }
                string abc = _dc.dm_artycle_category.Get_category(a).Path;
                ViewData["url"] = "/C/B/" + a + "/1/10/" + abc;
            }

            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ZJ(int a, string title, string description,
            string body, bool addComment, string urlImageDescription, string urlImageBody, bool descriptionBody,
            bool registrUser, bool approvedVotes, System.DateTime closeDate)
        {
            #region проверка данных
            if (title == null)
            {
                ModelState.AddModelError("Title", "Название должно содержать не менее 3 символов.");
            }
            else
            {
                if (title.Length <= 2)
                    ModelState.AddModelError("Title", "Название должно содержать не менее 3 символов.");
                if (title.Length > 256)
                    ModelState.AddModelError("Title", "Название должно содержать не более 256 символов.");
                if (title.IndexOf("script") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: script");
                if (title.IndexOf("object") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: object");
                if (title.IndexOf("applet") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: applet");
                if (title.IndexOf("embed") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: embed");
                if (title.IndexOf("<%") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: <%%>");
                if (title.IndexOf("$") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: $");
            }
            if (description == null)
            {
                ModelState.AddModelError("Description", "Краткое содержание должно содержать не менее 3 символов.");
            }
            else
            {
                if (description.Length <= 2)
                    ModelState.AddModelError("Description", "Краткое содержание должно содержать не менее 3 символов.");
                if (description.Length > 4000)
                    ModelState.AddModelError("Description", "Краткое содержание должно содержать не более 4000 символов.");
                if (description.IndexOf("script") != -1)
                    ModelState.AddModelError("Description", "Краткое содержание не может содержать: script");
                if (description.IndexOf("object") != -1)
                    ModelState.AddModelError("Description", "Краткое содержание не может содержать: object");
                if (description.IndexOf("applet") != -1)
                    ModelState.AddModelError("Description", "Краткое содержание не может содержать: applet");
                if (description.IndexOf("embed") != -1)
                    ModelState.AddModelError("Description", "Краткое содержание не может содержать: embed");
                if (description.IndexOf("<%") != -1)
                    ModelState.AddModelError("Description", "Краткое содержание не может содержать: <%%>");
                if (description.IndexOf("$") != -1)
                    ModelState.AddModelError("Description", "Краткое содержание не может содержать: $");
            }
            if (body == null)
            {
                ModelState.AddModelError("Body", "Статья должна содержать не менее 100 символов.");
            }
            else
            {
                if (body.Length <= 99)
                    ModelState.AddModelError("Body", "Статья должна содержать не менее 100 символов.");
                if (body.IndexOf("script") != -1)
                    ModelState.AddModelError("Body", "Статья не может содержать: script");
                if (body.IndexOf("object") != -1)
                    ModelState.AddModelError("Body", "Статья не может содержать: object");
                if (body.IndexOf("applet") != -1)
                    ModelState.AddModelError("Body", "Статья не может содержать: applet");
                if (body.IndexOf("embed") != -1)
                    ModelState.AddModelError("Body", "Статья не может содержать: embed");
                if (body.IndexOf("<%") != -1)
                    ModelState.AddModelError("Body", "Статья не может содержать: <%%>");
                if (body.IndexOf("$") != -1)
                    ModelState.AddModelError("Body", "Статья не может содержать: $");
            }
            if (urlImageDescription == null)
            {
                ModelState.AddModelError("UrlImageDescription", "При отсутствии ссылки вставьте null.");
            }
            else
            {
                if (urlImageDescription.Length <= 1)
                    ModelState.AddModelError("UrlImageDescription", "При отсутствии ссылки вставьте null.");
                if (urlImageDescription.Length > 256)
                    ModelState.AddModelError("UrlImageDescription", "Ссылка должна содержать не более 256 символов.");
                if (urlImageDescription.IndexOf("script") != -1)
                    ModelState.AddModelError("UrlImageDescription", "Ссылка не может содержать: script");
                if (urlImageDescription.IndexOf("object") != -1)
                    ModelState.AddModelError("UrlImageDescription", "Ссылка не может содержать: object");
                if (urlImageDescription.IndexOf("applet") != -1)
                    ModelState.AddModelError("UrlImageDescription", "Ссылка не может содержать: applet");
                if (urlImageDescription.IndexOf("embed") != -1)
                    ModelState.AddModelError("UrlImageDescription", "Ссылка не может содержать: embed");
                if (urlImageDescription.IndexOf("<%") != -1)
                    ModelState.AddModelError("UrlImageDescription", "Ссылка не может содержать: <%%>");
                if (urlImageDescription.IndexOf("$") != -1)
                    ModelState.AddModelError("UrlImageDescription", "Ссылка не может содержать: $");
            }
            if (urlImageBody == null)
            {
                ModelState.AddModelError("UrlImageBody", "При отсутствии ссылки вставьте null.");
            }
            else
            {
                if (urlImageBody.Length <= 1)
                    ModelState.AddModelError("UrlImageBody", "При отсутствии ссылки вставьте null.");
                if (urlImageBody.Length > 256)
                    ModelState.AddModelError("UrlImageBody", "Ссылка должна содержать не более 256 символов.");
                if (urlImageBody.IndexOf("script") != -1)
                    ModelState.AddModelError("UrlImageBody", "Ссылка не может содержать: script");
                if (urlImageBody.IndexOf("object") != -1)
                    ModelState.AddModelError("UrlImageBody", "Ссылка не может содержать: object");
                if (urlImageBody.IndexOf("applet") != -1)
                    ModelState.AddModelError("UrlImageBody", "Ссылка не может содержать: applet");
                if (urlImageBody.IndexOf("embed") != -1)
                    ModelState.AddModelError("UrlImageBody", "Ссылка не может содержать: embed");
                if (urlImageBody.IndexOf("<%") != -1)
                    ModelState.AddModelError("UrlImageBody", "Ссылка не может содержать: <%%>");
                if (urlImageBody.IndexOf("$") != -1)
                    ModelState.AddModelError("UrlImageBody", "Ссылка не может содержать: $");
            }
            #endregion

            if (ModelState.IsValid)
            {

                _dc.dm_artycle.Create_artycle(a, title, description,
            body, addComment, urlImageDescription, urlImageBody, descriptionBody,
            registrUser, approvedVotes, closeDate);
                int abc = _dc.dm_artycle.Get_artycle_first().Id;
                return RedirectToAction("ZP", new { a = abc });

            }
            #region меню и титл
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion
            #region настройка сайта
            ViewData["menu_url"] = "create_artycle";
            //bool разрешены блоги или нет
            ViewData["model_blog"] = _dc.dm_model.Get_model().Blog;
            //bool разрешены новости или нет
            ViewData["model_news"] = _dc.dm_model.Get_model().News;
            //bool разрешены статьи или нет
            ViewData["model_artycle"] = _dc.dm_model.Get_model().Artycle;

            #endregion
            if (a == 0)
            {
                ViewData["Category"] = new SelectList(_dc.dm_artycle_category.Get_categories(), "Id", "Title");
                ViewData["url"] = "/C/A/0/1/10/Artycles";
            }
            else
            {
                if (_dc.dm_artycle_category.Get_category(a).CategoryId == 0)
                {
                    ViewData["Category"] = new SelectList(_dc.dm_artycle_category.Get_categories(), "Id", "Title", a);
                }
                else
                {
                    int fjk = _dc.dm_artycle_category.Get_category(a).mt_artycle_category1.Id;
                    ViewData["Category"] = new SelectList(_dc.dm_artycle_category.Get_re_categories(fjk), "Id", "Title", a);

                }
                string abc = _dc.dm_artycle_category.Get_category(a).Path;
                ViewData["url"] = "/C/B/" + a + "/1/10/" + abc;
            }

            return View();

        }

        /*------------------ZK - редактировать статью----------------------*/
        [Authorize(Roles = "artycle_editor, chief_editor")]
        public ActionResult ZK(int a, int b)
        {
            #region меню и титл
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion
            #region настройка сайта
            ViewData["menu_url"] = "edit_artycle";
            //bool разрешены блоги или нет
            ViewData["model_blog"] = _dc.dm_model.Get_model().Blog;
            //bool разрешены новости или нет
            ViewData["model_news"] = _dc.dm_model.Get_model().News;
            //bool разрешены статьи или нет
            ViewData["model_artycle"] = _dc.dm_model.Get_model().Artycle;

            #endregion
            mt_artycle x = _dc.dm_artycle.Get_artycle(a);
            ViewData.Model = x;

            if (_dc.dm_artycle_category.Get_category(x.CategoryId).CategoryId == 0)
            {
                ViewData["Category"] = new SelectList(_dc.dm_artycle_category.Get_categories(), "Id", "Title", x.CategoryId);
            }
            else
            {
                int fjk = _dc.dm_artycle_category.Get_category(x.CategoryId).mt_artycle_category1.Id;
                ViewData["Category"] = new SelectList(_dc.dm_artycle_category.Get_re_categories(fjk), "Id", "Title", x.CategoryId);

            }
            ViewData["url"] = "/C/C/" + a + "/1/10/" + x.Path;
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ZK(int a, int b, string title, string description,
            string body, bool addComment, string urlImageDescription, string urlImageBody, bool descriptionBody,
            bool registrUser, bool approvedVotes, System.DateTime closeDate)
        {
            #region проверка данных
            if (title == null)
            {
                ModelState.AddModelError("Title", "Название должно содержать не менее 3 символов.");
            }
            else
            {
                if (title.Length <= 2)
                    ModelState.AddModelError("Title", "Название должно содержать не менее 3 символов.");
                if (title.Length > 256)
                    ModelState.AddModelError("Title", "Название должно содержать не более 256 символов.");
                if (title.IndexOf("script") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: script");
                if (title.IndexOf("object") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: object");
                if (title.IndexOf("applet") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: applet");
                if (title.IndexOf("embed") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: embed");
                if (title.IndexOf("<%") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: <%%>");
                if (title.IndexOf("$") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: $");
            }
            if (description == null)
            {
                ModelState.AddModelError("Description", "Краткое содержание должно содержать не менее 3 символов.");
            }
            else
            {
                if (description.Length <= 2)
                    ModelState.AddModelError("Description", "Краткое содержание должно содержать не менее 3 символов.");
                if (description.Length > 4000)
                    ModelState.AddModelError("Description", "Краткое содержание должно содержать не более 4000 символов.");
                if (description.IndexOf("script") != -1)
                    ModelState.AddModelError("Description", "Краткое содержание не может содержать: script");
                if (description.IndexOf("object") != -1)
                    ModelState.AddModelError("Description", "Краткое содержание не может содержать: object");
                if (description.IndexOf("applet") != -1)
                    ModelState.AddModelError("Description", "Краткое содержание не может содержать: applet");
                if (description.IndexOf("embed") != -1)
                    ModelState.AddModelError("Description", "Краткое содержание не может содержать: embed");
                if (description.IndexOf("<%") != -1)
                    ModelState.AddModelError("Description", "Краткое содержание не может содержать: <%%>");
                if (description.IndexOf("$") != -1)
                    ModelState.AddModelError("Description", "Краткое содержание не может содержать: $");
            }
            if (body == null)
            {
                ModelState.AddModelError("Body", "Статья должна содержать не менее 100 символов.");
            }
            else
            {
                if (body.Length <= 99)
                    ModelState.AddModelError("Body", "Статья должна содержать не менее 100 символов.");
                if (body.IndexOf("script") != -1)
                    ModelState.AddModelError("Body", "Статья не может содержать: script");
                if (body.IndexOf("object") != -1)
                    ModelState.AddModelError("Body", "Статья не может содержать: object");
                if (body.IndexOf("applet") != -1)
                    ModelState.AddModelError("Body", "Статья не может содержать: applet");
                if (body.IndexOf("embed") != -1)
                    ModelState.AddModelError("Body", "Статья не может содержать: embed");
                if (body.IndexOf("<%") != -1)
                    ModelState.AddModelError("Body", "Статья не может содержать: <%%>");
                if (body.IndexOf("$") != -1)
                    ModelState.AddModelError("Body", "Статья не может содержать: $");
            }
            if (urlImageDescription == null)
            {
                ModelState.AddModelError("UrlImageDescription", "При отсутствии ссылки вставьте null.");
            }
            else
            {
                if (urlImageDescription.Length <= 1)
                    ModelState.AddModelError("UrlImageDescription", "При отсутствии ссылки вставьте null.");
                if (urlImageDescription.Length > 256)
                    ModelState.AddModelError("UrlImageDescription", "Ссылка должна содержать не более 256 символов.");
                if (urlImageDescription.IndexOf("script") != -1)
                    ModelState.AddModelError("UrlImageDescription", "Ссылка не может содержать: script");
                if (urlImageDescription.IndexOf("object") != -1)
                    ModelState.AddModelError("UrlImageDescription", "Ссылка не может содержать: object");
                if (urlImageDescription.IndexOf("applet") != -1)
                    ModelState.AddModelError("UrlImageDescription", "Ссылка не может содержать: applet");
                if (urlImageDescription.IndexOf("embed") != -1)
                    ModelState.AddModelError("UrlImageDescription", "Ссылка не может содержать: embed");
                if (urlImageDescription.IndexOf("<%") != -1)
                    ModelState.AddModelError("UrlImageDescription", "Ссылка не может содержать: <%%>");
                if (urlImageDescription.IndexOf("$") != -1)
                    ModelState.AddModelError("UrlImageDescription", "Ссылка не может содержать: $");
            }
            if (urlImageBody == null)
            {
                ModelState.AddModelError("UrlImageBody", "При отсутствии ссылки вставьте null.");
            }
            else
            {
                if (urlImageBody.Length <= 1)
                    ModelState.AddModelError("UrlImageBody", "При отсутствии ссылки вставьте null.");
                if (urlImageBody.Length > 256)
                    ModelState.AddModelError("UrlImageBody", "Ссылка должна содержать не более 256 символов.");
                if (urlImageBody.IndexOf("script") != -1)
                    ModelState.AddModelError("UrlImageBody", "Ссылка не может содержать: script");
                if (urlImageBody.IndexOf("object") != -1)
                    ModelState.AddModelError("UrlImageBody", "Ссылка не может содержать: object");
                if (urlImageBody.IndexOf("applet") != -1)
                    ModelState.AddModelError("UrlImageBody", "Ссылка не может содержать: applet");
                if (urlImageBody.IndexOf("embed") != -1)
                    ModelState.AddModelError("UrlImageBody", "Ссылка не может содержать: embed");
                if (urlImageBody.IndexOf("<%") != -1)
                    ModelState.AddModelError("UrlImageBody", "Ссылка не может содержать: <%%>");
                if (urlImageBody.IndexOf("$") != -1)
                    ModelState.AddModelError("UrlImageBody", "Ссылка не может содержать: $");
            }
            #endregion

            if (ModelState.IsValid)
            {

                _dc.dm_artycle.Update_artycle(a, b, title, description,
              body, addComment, urlImageDescription, urlImageBody, descriptionBody,
              registrUser, approvedVotes, closeDate);
                return RedirectToAction("ZP", new { a });


            }
            #region меню и титл
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion
            #region настройка сайта
            ViewData["menu_url"] = "edit_artycle";
            //bool разрешены блоги или нет
            ViewData["model_blog"] = _dc.dm_model.Get_model().Blog;
            //bool разрешены новости или нет
            ViewData["model_news"] = _dc.dm_model.Get_model().News;
            //bool разрешены статьи или нет
            ViewData["model_artycle"] = _dc.dm_model.Get_model().Artycle;

            #endregion
            mt_artycle x = _dc.dm_artycle.Get_artycle(a);
            ViewData.Model = x;
            if (_dc.dm_artycle_category.Get_category(x.CategoryId).CategoryId == 0)
            {
                ViewData["Category"] = new SelectList(_dc.dm_artycle_category.Get_categories(), "Id", "Title", x.CategoryId);
            }
            else
            {
                int fjk = _dc.dm_artycle_category.Get_category(x.CategoryId).mt_artycle_category1.Id;
                ViewData["Category"] = new SelectList(_dc.dm_artycle_category.Get_re_categories(fjk), "Id", "Title", x.CategoryId);

            }
            ViewData["url"] = "/C/C/" + a + "/1/10/" + x.Path;
            return View();

        }

        /*------------------ZL - удалить статью ---------------------*/
        [Authorize(Roles = "artycle_editor, chief_editor")]
        public ActionResult ZL(int a)
        {
            int abc = _dc.dm_artycle.Get_artycle(a).mt_artycle_category.Id;
            string bcd = _dc.dm_artycle.Get_artycle(a).mt_artycle_category.Path;
            string def = _dc.dm_artycle.Get_artycle(a).mt_artycle_category.AddedBy;
            if (HttpContext.User.IsInRole("artycle_editor"))
            {
                if (HttpContext.User.Identity.Name == def)
                {
                    _dc.dm_artycle.Delete_artycle(a);
                    return RedirectToAction("B", new { a = abc, b = 1, c = 10, d = bcd });
                }

            }
            if (HttpContext.User.IsInRole("chief_editor"))
            {

                _dc.dm_artycle.Delete_artycle(a);
                return RedirectToAction("B", new { a = abc, b = 1, c = 10, d = bcd });


            }
            return RedirectToAction("Error", "Home");
        }

        /*-----------------ZM - удалить пост -------------------*/
        [Authorize(Roles = "blogger, chief_editor")]
        public ActionResult ZM(int a)
        {
            int abc = _dc.dm_artycle.Get_artycle(a).mt_artycle_category.Id;
            string bcd = _dc.dm_artycle.Get_artycle(a).mt_artycle_category.Path;
            string def = _dc.dm_artycle.Get_artycle(a).AddedBy;
            if (HttpContext.User.IsInRole("blogger"))
            {
                if (HttpContext.User.Identity.Name == def)
                {
                    _dc.dm_artycle.Delete_artycle(a);
                    return RedirectToAction("B", new { a = abc, b = 1, c = 10, d = bcd });
                }

            }
            if (HttpContext.User.IsInRole("chief_editor"))
            {

                _dc.dm_artycle.Delete_artycle(a);
                return RedirectToAction("B", new { a = abc, b = 1, c = 10, d = bcd });


            }
            return RedirectToAction("Error", "Home");
        }      

        /*--------------ZP - добавить тег в статью и создать тег-----------------*/
        [Authorize(Roles = "blogger, artycle_editor, chief_editor")]
        public ActionResult ZP(int a)
        {
            #region меню и титл
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion
            #region настройка сайта
            ViewData["menu_url"] = "tegs";
            //bool разрешены блоги или нет
            ViewData["model_blog"] = _dc.dm_model.Get_model().Blog;
            //bool разрешены новости или нет
            ViewData["model_news"] = _dc.dm_model.Get_model().News;
            //bool разрешены статьи или нет
            ViewData["model_artycle"] = _dc.dm_model.Get_model().Artycle;

            #endregion
            ViewData["artycle"] = _dc.dm_artycle.Get_artycles();
            ViewData["artycleid"] = a;
            ViewData["categoryid"] = _dc.dm_artycle.Get_artycle(a).CategoryId;
            ViewData["blog_bool"] = _dc.dm_artycle.Get_artycle(a).mt_artycle_category.Blog;
            ViewData["news_bool"] = _dc.dm_artycle.Get_artycle(a).mt_artycle_category.News;
            ViewData["teg"] = _dc.dm_artycle.Get_tegs();
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ZP(int a, string title)
        {
            #region проверка данных
            if (title == null)
            {
                ModelState.AddModelError("Title", "Название должно содержать не менее 1 символов.");
            }
            else
            {
                if (title.Length < 1)
                    ModelState.AddModelError("Title", "Название должно содержать не менее 1 символов.");
                if (title.Length > 256)
                    ModelState.AddModelError("Title", "Название должно содержать не более 256 символов.");
                if (title.IndexOf("script") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: script");
                if (title.IndexOf("object") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: object");
                if (title.IndexOf("applet") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: applet");
                if (title.IndexOf("embed") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: embed");
                if (title.IndexOf("<%") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: <%%>");
                if (title.IndexOf("$") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: $");
            }

            #endregion

            if (ModelState.IsValid)
            {

                _dc.dm_artycle.Create_teg(title);
                return RedirectToAction("ZP", new { a });

            }
            #region меню и титл
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion
            #region настройка сайта
            ViewData["menu_url"] = "tegs";
            //bool разрешены блоги или нет
            ViewData["model_blog"] = _dc.dm_model.Get_model().Blog;
            //bool разрешены новости или нет
            ViewData["model_news"] = _dc.dm_model.Get_model().News;
            //bool разрешены статьи или нет
            ViewData["model_artycle"] = _dc.dm_model.Get_model().Artycle;

            #endregion
            ViewData["artycle"] = _dc.dm_artycle.Get_artycles();
            ViewData["artycleid"] = a;
            ViewData["categoryid"] = _dc.dm_artycle.Get_artycle(a).CategoryId;
            ViewData["blog_bool"] = _dc.dm_artycle.Get_artycle(a).mt_artycle_category.Blog;
            ViewData["news_bool"] = _dc.dm_artycle.Get_artycle(a).mt_artycle_category.News;
            ViewData["teg"] = _dc.dm_artycle.Get_tegs();
            return View();

        }

        /*------------------ZQ - добавить тег в статью -------------------*/
        [Authorize(Roles = "blogger, artycle_editor, chief_editor")]
        public ActionResult ZQ(int a, int b)
        {
            if (_dc.dm_artycle.Get_teg_artycle(a, b) == null)
            {
                _dc.dm_artycle.Add_teg_in_artycle(a, b);
            }
            return RedirectToAction("ZP", new { a });

        }

        /*--------------ZR - удалить тег из статьи -----------------*/
        [Authorize(Roles = "blogger, artycle_editor, chief_editor")]
        public ActionResult ZR(int a, int b)
        {

            _dc.dm_artycle.Delete_teg_in_artycle(a, b);
            return RedirectToAction("ZP", new { a });

        }

        /*----------------ZS - создать пост в блог------------------*/
        [Authorize(Roles = "blogger")]
        public ActionResult ZS(int a)
        {
            #region меню и титл
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion
            #region настройка сайта
            ViewData["menu_url"] = "create_post";
            //bool разрешены блоги или нет
            ViewData["model_blog"] = _dc.dm_model.Get_model().Blog;
            //bool разрешены новости или нет
            ViewData["model_news"] = _dc.dm_model.Get_model().News;
            //bool разрешены статьи или нет
            ViewData["model_artycle"] = _dc.dm_model.Get_model().Artycle;

            #endregion
            string dfg = _dc.dm_artycle_category.Get_category(a).Path;
            ViewData["url"] = "/C/B/" + a + "/1/10/" + dfg;
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ZS(int a, string title, string description,
            string body, string urlImageDescription, string urlImageBody, bool descriptionBody,
            bool registrUser)
        {
            #region проверка данных
            if (title == null)
            {
                ModelState.AddModelError("Title", "Название должно содержать не менее 3 символов.");
            }
            else
            {
                if (title.Length <= 2)
                    ModelState.AddModelError("Title", "Название должно содержать не менее 3 символов.");
                if (title.Length > 256)
                    ModelState.AddModelError("Title", "Название должно содержать не более 256 символов.");
                if (title.IndexOf("script") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: script");
                if (title.IndexOf("object") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: object");
                if (title.IndexOf("applet") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: applet");
                if (title.IndexOf("embed") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: embed");
                if (title.IndexOf("<%") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: <%%>");
                if (title.IndexOf("$") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: $");
            }
            if (description == null)
            {
                ModelState.AddModelError("Description", "Краткое содержание должно содержать не менее 3 символов.");
            }
            else
            {
                if (description.Length <= 2)
                    ModelState.AddModelError("Description", "Краткое содержание должно содержать не менее 3 символов.");
                if (description.Length > 4000)
                    ModelState.AddModelError("Description", "Краткое содержание должно содержать не более 4000 символов.");
                if (description.IndexOf("script") != -1)
                    ModelState.AddModelError("Description", "Краткое содержание не может содержать: script");
                if (description.IndexOf("object") != -1)
                    ModelState.AddModelError("Description", "Краткое содержание не может содержать: object");
                if (description.IndexOf("applet") != -1)
                    ModelState.AddModelError("Description", "Краткое содержание не может содержать: applet");
                if (description.IndexOf("embed") != -1)
                    ModelState.AddModelError("Description", "Краткое содержание не может содержать: embed");
                if (description.IndexOf("<%") != -1)
                    ModelState.AddModelError("Description", "Краткое содержание не может содержать: <%%>");
                if (description.IndexOf("$") != -1)
                    ModelState.AddModelError("Description", "Краткое содержание не может содержать: $");
            }
            if (body == null)
            {
                ModelState.AddModelError("Body", "Статья должна содержать не менее 100 символов.");
            }
            else
            {
                if (body.Length <= 99)
                    ModelState.AddModelError("Body", "Статья должна содержать не менее 100 символов.");
                if (body.IndexOf("script") != -1)
                    ModelState.AddModelError("Body", "Статья не может содержать: script");
                if (body.IndexOf("object") != -1)
                    ModelState.AddModelError("Body", "Статья не может содержать: object");
                if (body.IndexOf("applet") != -1)
                    ModelState.AddModelError("Body", "Статья не может содержать: applet");
                if (body.IndexOf("embed") != -1)
                    ModelState.AddModelError("Body", "Статья не может содержать: embed");
                if (body.IndexOf("<%") != -1)
                    ModelState.AddModelError("Body", "Статья не может содержать: <%%>");
                if (body.IndexOf("$") != -1)
                    ModelState.AddModelError("Body", "Статья не может содержать: $");
            }
            if (urlImageDescription == null)
            {
                ModelState.AddModelError("UrlImageDescription", "При отсутствии ссылки вставьте null.");
            }
            else
            {
                if (urlImageDescription.Length <= 1)
                    ModelState.AddModelError("UrlImageDescription", "При отсутствии ссылки вставьте null.");
                if (urlImageDescription.Length > 256)
                    ModelState.AddModelError("UrlImageDescription", "Ссылка должна содержать не более 256 символов.");
                if (urlImageDescription.IndexOf("script") != -1)
                    ModelState.AddModelError("UrlImageDescription", "Ссылка не может содержать: script");
                if (urlImageDescription.IndexOf("object") != -1)
                    ModelState.AddModelError("UrlImageDescription", "Ссылка не может содержать: object");
                if (urlImageDescription.IndexOf("applet") != -1)
                    ModelState.AddModelError("UrlImageDescription", "Ссылка не может содержать: applet");
                if (urlImageDescription.IndexOf("embed") != -1)
                    ModelState.AddModelError("UrlImageDescription", "Ссылка не может содержать: embed");
                if (urlImageDescription.IndexOf("<%") != -1)
                    ModelState.AddModelError("UrlImageDescription", "Ссылка не может содержать: <%%>");
                if (urlImageDescription.IndexOf("$") != -1)
                    ModelState.AddModelError("UrlImageDescription", "Ссылка не может содержать: $");
            }
            if (urlImageBody == null)
            {
                ModelState.AddModelError("UrlImageBody", "При отсутствии ссылки вставьте null.");
            }
            else
            {
                if (urlImageBody.Length <= 1)
                    ModelState.AddModelError("UrlImageBody", "При отсутствии ссылки вставьте null.");
                if (urlImageBody.Length > 256)
                    ModelState.AddModelError("UrlImageBody", "Ссылка должна содержать не более 256 символов.");
                if (urlImageBody.IndexOf("script") != -1)
                    ModelState.AddModelError("UrlImageBody", "Ссылка не может содержать: script");
                if (urlImageBody.IndexOf("object") != -1)
                    ModelState.AddModelError("UrlImageBody", "Ссылка не может содержать: object");
                if (urlImageBody.IndexOf("applet") != -1)
                    ModelState.AddModelError("UrlImageBody", "Ссылка не может содержать: applet");
                if (urlImageBody.IndexOf("embed") != -1)
                    ModelState.AddModelError("UrlImageBody", "Ссылка не может содержать: embed");
                if (urlImageBody.IndexOf("<%") != -1)
                    ModelState.AddModelError("UrlImageBody", "Ссылка не может содержать: <%%>");
                if (urlImageBody.IndexOf("$") != -1)
                    ModelState.AddModelError("UrlImageBody", "Ссылка не может содержать: $");
            }
            #endregion

            if (ModelState.IsValid)
            {

                _dc.dm_artycle.Create_post(a, title, description,
            body, urlImageDescription, urlImageBody, descriptionBody,
            registrUser);
                int abc = _dc.dm_artycle.Get_artycle_first().Id;
                return RedirectToAction("ZP", new { a = abc });

            }
            #region меню и титл
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion
            #region настройка сайта
            ViewData["menu_url"] = "create_post";
            //bool разрешены блоги или нет
            ViewData["model_blog"] = _dc.dm_model.Get_model().Blog;
            //bool разрешены новости или нет
            ViewData["model_news"] = _dc.dm_model.Get_model().News;
            //bool разрешены статьи или нет
            ViewData["model_artycle"] = _dc.dm_model.Get_model().Artycle;

            #endregion
            string dfg = _dc.dm_artycle_category.Get_category(a).Path;
            ViewData["url"] = "/C/B/" + a + "/1/10/" + dfg;


            return View();

        }

        /*----------------ZT - редактировать пост-------------------*/
        [Authorize(Roles = "artycle_editor, chief_editor")]
        public ActionResult ZT(int a)
        {
            #region меню и титл
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion
            #region настройка сайта
            ViewData["menu_url"] = "edit_post";
            //bool разрешены блоги или нет
            ViewData["model_blog"] = _dc.dm_model.Get_model().Blog;
            //bool разрешены новости или нет
            ViewData["model_news"] = _dc.dm_model.Get_model().News;
            //bool разрешены статьи или нет
            ViewData["model_artycle"] = _dc.dm_model.Get_model().Artycle;

            #endregion
            mt_artycle x = _dc.dm_artycle.Get_artycle(a);
            ViewData.Model = x;

            ViewData["url"] = "/C/C/" + a + "/1/10/" + x.Path;
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ZT(int a, string title, string description,
            string body, string urlImageDescription, string urlImageBody, bool descriptionBody,
            bool registrUser)
        {
            #region проверка данных
            if (title == null)
            {
                ModelState.AddModelError("Title", "Название должно содержать не менее 3 символов.");
            }
            else
            {
                if (title.Length <= 2)
                    ModelState.AddModelError("Title", "Название должно содержать не менее 3 символов.");
                if (title.Length > 256)
                    ModelState.AddModelError("Title", "Название должно содержать не более 256 символов.");
                if (title.IndexOf("script") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: script");
                if (title.IndexOf("object") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: object");
                if (title.IndexOf("applet") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: applet");
                if (title.IndexOf("embed") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: embed");
                if (title.IndexOf("<%") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: <%%>");
                if (title.IndexOf("$") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: $");
            }
            if (description == null)
            {
                ModelState.AddModelError("Description", "Краткое содержание должно содержать не менее 3 символов.");
            }
            else
            {
                if (description.Length <= 2)
                    ModelState.AddModelError("Description", "Краткое содержание должно содержать не менее 3 символов.");
                if (description.Length > 4000)
                    ModelState.AddModelError("Description", "Краткое содержание должно содержать не более 4000 символов.");
                if (description.IndexOf("script") != -1)
                    ModelState.AddModelError("Description", "Краткое содержание не может содержать: script");
                if (description.IndexOf("object") != -1)
                    ModelState.AddModelError("Description", "Краткое содержание не может содержать: object");
                if (description.IndexOf("applet") != -1)
                    ModelState.AddModelError("Description", "Краткое содержание не может содержать: applet");
                if (description.IndexOf("embed") != -1)
                    ModelState.AddModelError("Description", "Краткое содержание не может содержать: embed");
                if (description.IndexOf("<%") != -1)
                    ModelState.AddModelError("Description", "Краткое содержание не может содержать: <%%>");
                if (description.IndexOf("$") != -1)
                    ModelState.AddModelError("Description", "Краткое содержание не может содержать: $");
            }
            if (body == null)
            {
                ModelState.AddModelError("Body", "Статья должна содержать не менее 100 символов.");
            }
            else
            {
                if (body.Length <= 99)
                    ModelState.AddModelError("Body", "Статья должна содержать не менее 100 символов.");
                if (body.IndexOf("script") != -1)
                    ModelState.AddModelError("Body", "Статья не может содержать: script");
                if (body.IndexOf("object") != -1)
                    ModelState.AddModelError("Body", "Статья не может содержать: object");
                if (body.IndexOf("applet") != -1)
                    ModelState.AddModelError("Body", "Статья не может содержать: applet");
                if (body.IndexOf("embed") != -1)
                    ModelState.AddModelError("Body", "Статья не может содержать: embed");
                if (body.IndexOf("<%") != -1)
                    ModelState.AddModelError("Body", "Статья не может содержать: <%%>");
                if (body.IndexOf("$") != -1)
                    ModelState.AddModelError("Body", "Статья не может содержать: $");
            }
            if (urlImageDescription == null)
            {
                ModelState.AddModelError("UrlImageDescription", "При отсутствии ссылки вставьте null.");
            }
            else
            {
                if (urlImageDescription.Length <= 1)
                    ModelState.AddModelError("UrlImageDescription", "При отсутствии ссылки вставьте null.");
                if (urlImageDescription.Length > 256)
                    ModelState.AddModelError("UrlImageDescription", "Ссылка должна содержать не более 256 символов.");
                if (urlImageDescription.IndexOf("script") != -1)
                    ModelState.AddModelError("UrlImageDescription", "Ссылка не может содержать: script");
                if (urlImageDescription.IndexOf("object") != -1)
                    ModelState.AddModelError("UrlImageDescription", "Ссылка не может содержать: object");
                if (urlImageDescription.IndexOf("applet") != -1)
                    ModelState.AddModelError("UrlImageDescription", "Ссылка не может содержать: applet");
                if (urlImageDescription.IndexOf("embed") != -1)
                    ModelState.AddModelError("UrlImageDescription", "Ссылка не может содержать: embed");
                if (urlImageDescription.IndexOf("<%") != -1)
                    ModelState.AddModelError("UrlImageDescription", "Ссылка не может содержать: <%%>");
                if (urlImageDescription.IndexOf("$") != -1)
                    ModelState.AddModelError("UrlImageDescription", "Ссылка не может содержать: $");
            }
            if (urlImageBody == null)
            {
                ModelState.AddModelError("UrlImageBody", "При отсутствии ссылки вставьте null.");
            }
            else
            {
                if (urlImageBody.Length <= 1)
                    ModelState.AddModelError("UrlImageBody", "При отсутствии ссылки вставьте null.");
                if (urlImageBody.Length > 256)
                    ModelState.AddModelError("UrlImageBody", "Ссылка должна содержать не более 256 символов.");
                if (urlImageBody.IndexOf("script") != -1)
                    ModelState.AddModelError("UrlImageBody", "Ссылка не может содержать: script");
                if (urlImageBody.IndexOf("object") != -1)
                    ModelState.AddModelError("UrlImageBody", "Ссылка не может содержать: object");
                if (urlImageBody.IndexOf("applet") != -1)
                    ModelState.AddModelError("UrlImageBody", "Ссылка не может содержать: applet");
                if (urlImageBody.IndexOf("embed") != -1)
                    ModelState.AddModelError("UrlImageBody", "Ссылка не может содержать: embed");
                if (urlImageBody.IndexOf("<%") != -1)
                    ModelState.AddModelError("UrlImageBody", "Ссылка не может содержать: <%%>");
                if (urlImageBody.IndexOf("$") != -1)
                    ModelState.AddModelError("UrlImageBody", "Ссылка не может содержать: $");
            }
            #endregion

            if (ModelState.IsValid)
            {

                _dc.dm_artycle.Update_post(a, title, description,
              body, urlImageDescription, urlImageBody, descriptionBody,
              registrUser);
                return RedirectToAction("ZP", new { a });


            }
            #region меню и титл
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion
            #region настройка сайта
            ViewData["menu_url"] = "edit_post";
            //bool разрешены блоги или нет
            ViewData["model_blog"] = _dc.dm_model.Get_model().Blog;
            //bool разрешены новости или нет
            ViewData["model_news"] = _dc.dm_model.Get_model().News;
            //bool разрешены статьи или нет
            ViewData["model_artycle"] = _dc.dm_model.Get_model().Artycle;

            #endregion
            mt_artycle x = _dc.dm_artycle.Get_artycle(a);
            ViewData.Model = x;
            ViewData["url"] = "/C/C/" + a + "/1/10/" + x.Path;
            return View();

        }

        /*-----------------ZU - редактировать тег------------------*/
        [Authorize(Roles = "blogger, artycle_editor, chief_editor")]
        public ActionResult ZU(int a)
        {
            #region меню и титл
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion
            #region настройка сайта
            ViewData["menu_url"] = "tegs";
            //bool разрешены блоги или нет
            ViewData["model_blog"] = _dc.dm_model.Get_model().Blog;
            //bool разрешены новости или нет
            ViewData["model_news"] = _dc.dm_model.Get_model().News;
            //bool разрешены статьи или нет
            ViewData["model_artycle"] = _dc.dm_model.Get_model().Artycle;

            #endregion

            mt_artycle_teg x = _dc.dm_artycle.Get_teg(a);
            ViewData.Model = x;
            ViewData["url"] = "/C/E/" + a + "/1/10/" + x.Path;
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ZU(int a, string title)
        {
            #region проверка данных
            if (title == null)
            {
                ModelState.AddModelError("Title", "Название должно содержать не менее 1 символов.");
            }
            else
            {
                if (title.Length < 1)
                    ModelState.AddModelError("Title", "Название должно содержать не менее 1 символов.");
                if (title.Length > 256)
                    ModelState.AddModelError("Title", "Название должно содержать не более 256 символов.");
                if (title.IndexOf("script") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: script");
                if (title.IndexOf("object") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: object");
                if (title.IndexOf("applet") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: applet");
                if (title.IndexOf("embed") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: embed");
                if (title.IndexOf("<%") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: <%%>");
                if (title.IndexOf("$") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: $");
            }

            #endregion

            if (ModelState.IsValid)
            {

                _dc.dm_artycle.Update_teg(a, title);
                string d = _dc.dm_artycle.Get_teg(a).Path;
                return RedirectToAction("E", new { a, b = 1, c = 10, d });
            }
            #region меню и титл
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion
            #region настройка сайта
            ViewData["menu_url"] = "tegs";
            //bool разрешены блоги или нет
            ViewData["model_blog"] = _dc.dm_model.Get_model().Blog;
            //bool разрешены новости или нет
            ViewData["model_news"] = _dc.dm_model.Get_model().News;
            //bool разрешены статьи или нет
            ViewData["model_artycle"] = _dc.dm_model.Get_model().Artycle;

            #endregion
            mt_artycle_teg x = _dc.dm_artycle.Get_teg(a);
            ViewData.Model = x;
            ViewData["url"] = "/C/E/" + a + "/1/10/" + x.Path;
            return View();

        }

        /*-----------------ZV - удалить тег --------------------*/
        [Authorize(Roles = "blogger, artycle_editor, chief_editor")]
        public ActionResult ZV(int a)
        {
            string def = _dc.dm_artycle.Get_teg(a).AddedBy;
            if (HttpContext.User.IsInRole("blogger"))
            {
                if (HttpContext.User.Identity.Name == def)
                {
                    _dc.dm_artycle.Delete_teg(a);
                    return RedirectToAction("A", new { a = 0, b = 1, c = 10, d = "Artycles" });
                }

            }
            if (HttpContext.User.IsInRole("chief_editor"))
            {

                _dc.dm_artycle.Delete_teg(a);
                return RedirectToAction("A", new { a = 0, b = 1, c = 10, d = "Artycles" });


            }
            if (HttpContext.User.IsInRole("artycle_editor"))
            {
                if (HttpContext.User.Identity.Name == def)
                {
                    _dc.dm_artycle.Delete_teg(a);
                    return RedirectToAction("A", new { a = 0, b = 1, c = 10, d = "Artycles" });
                }

            }
            return RedirectToAction("Error", "Home");
        }

        /*-----------------ZW - создать новость---------------------*/
        [Authorize(Roles = "artycle_editor, chief_editor")]
        public ActionResult ZW(int a)
        {
            #region меню и титл
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion
            #region настройка сайта
            ViewData["menu_url"] = "create_news";
            //bool разрешены блоги или нет
            ViewData["model_blog"] = _dc.dm_model.Get_model().Blog;
            //bool разрешены новости или нет
            ViewData["model_news"] = _dc.dm_model.Get_model().News;
            //bool разрешены статьи или нет
            ViewData["model_artycle"] = _dc.dm_model.Get_model().Artycle;

            #endregion
            if (a == 0)
            {
                ViewData["Category"] = new SelectList(_dc.dm_artycle_category.Get_category_news(), "Id", "Title");
                ViewData["url"] = "/C/F/0/1/10/News";
            }
            else
            {
                if (_dc.dm_artycle_category.Get_category(a).CategoryId == 0)
                {
                    ViewData["Category"] = new SelectList(_dc.dm_artycle_category.Get_category_news(), "Id", "Title", a);
                }
                else
                {
                    int fjk = _dc.dm_artycle_category.Get_category(a).mt_artycle_category1.Id;
                    ViewData["Category"] = new SelectList(_dc.dm_artycle_category.Get_re_categories(fjk), "Id", "Title", a);

                }
                string abc = _dc.dm_artycle_category.Get_category(a).Path;
                ViewData["url"] = "/C/B/" + a + "/1/10/" + abc;
            }

            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ZW(int a, string title, string description,
            string body, bool addComment, string urlImageDescription, string urlImageBody, bool descriptionBody,
            bool registrUser, bool approvedVotes, System.DateTime closeDate, bool warning)
        {
            #region проверка данных
            if (title == null)
            {
                ModelState.AddModelError("Title", "Название должно содержать не менее 3 символов.");
            }
            else
            {
                if (title.Length <= 2)
                    ModelState.AddModelError("Title", "Название должно содержать не менее 3 символов.");
                if (title.Length > 256)
                    ModelState.AddModelError("Title", "Название должно содержать не более 256 символов.");
                if (title.IndexOf("script") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: script");
                if (title.IndexOf("object") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: object");
                if (title.IndexOf("applet") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: applet");
                if (title.IndexOf("embed") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: embed");
                if (title.IndexOf("<%") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: <%%>");
                if (title.IndexOf("$") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: $");
            }
            if (description == null)
            {
                ModelState.AddModelError("Description", "Краткое содержание должно содержать не менее 3 символов.");
            }
            else
            {
                if (description.Length <= 2)
                    ModelState.AddModelError("Description", "Краткое содержание должно содержать не менее 3 символов.");
                if (description.Length > 4000)
                    ModelState.AddModelError("Description", "Краткое содержание должно содержать не более 4000 символов.");
                if (description.IndexOf("script") != -1)
                    ModelState.AddModelError("Description", "Краткое содержание не может содержать: script");
                if (description.IndexOf("object") != -1)
                    ModelState.AddModelError("Description", "Краткое содержание не может содержать: object");
                if (description.IndexOf("applet") != -1)
                    ModelState.AddModelError("Description", "Краткое содержание не может содержать: applet");
                if (description.IndexOf("embed") != -1)
                    ModelState.AddModelError("Description", "Краткое содержание не может содержать: embed");
                if (description.IndexOf("<%") != -1)
                    ModelState.AddModelError("Description", "Краткое содержание не может содержать: <%%>");
                if (description.IndexOf("$") != -1)
                    ModelState.AddModelError("Description", "Краткое содержание не может содержать: $");
            }
            if (body == null)
            {
                ModelState.AddModelError("Body", "Статья должна содержать не менее 100 символов.");
            }
            else
            {
                if (body.Length <= 99)
                    ModelState.AddModelError("Body", "Статья должна содержать не менее 100 символов.");
                if (body.IndexOf("script") != -1)
                    ModelState.AddModelError("Body", "Статья не может содержать: script");
                if (body.IndexOf("object") != -1)
                    ModelState.AddModelError("Body", "Статья не может содержать: object");
                if (body.IndexOf("applet") != -1)
                    ModelState.AddModelError("Body", "Статья не может содержать: applet");
                if (body.IndexOf("embed") != -1)
                    ModelState.AddModelError("Body", "Статья не может содержать: embed");
                if (body.IndexOf("<%") != -1)
                    ModelState.AddModelError("Body", "Статья не может содержать: <%%>");
                if (body.IndexOf("$") != -1)
                    ModelState.AddModelError("Body", "Статья не может содержать: $");
            }
            if (urlImageDescription == null)
            {
                ModelState.AddModelError("UrlImageDescription", "При отсутствии ссылки вставьте null.");
            }
            else
            {
                if (urlImageDescription.Length <= 1)
                    ModelState.AddModelError("UrlImageDescription", "При отсутствии ссылки вставьте null.");
                if (urlImageDescription.Length > 256)
                    ModelState.AddModelError("UrlImageDescription", "Ссылка должна содержать не более 256 символов.");
                if (urlImageDescription.IndexOf("script") != -1)
                    ModelState.AddModelError("UrlImageDescription", "Ссылка не может содержать: script");
                if (urlImageDescription.IndexOf("object") != -1)
                    ModelState.AddModelError("UrlImageDescription", "Ссылка не может содержать: object");
                if (urlImageDescription.IndexOf("applet") != -1)
                    ModelState.AddModelError("UrlImageDescription", "Ссылка не может содержать: applet");
                if (urlImageDescription.IndexOf("embed") != -1)
                    ModelState.AddModelError("UrlImageDescription", "Ссылка не может содержать: embed");
                if (urlImageDescription.IndexOf("<%") != -1)
                    ModelState.AddModelError("UrlImageDescription", "Ссылка не может содержать: <%%>");
                if (urlImageDescription.IndexOf("$") != -1)
                    ModelState.AddModelError("UrlImageDescription", "Ссылка не может содержать: $");
            }
            if (urlImageBody == null)
            {
                ModelState.AddModelError("UrlImageBody", "При отсутствии ссылки вставьте null.");
            }
            else
            {
                if (urlImageBody.Length <= 1)
                    ModelState.AddModelError("UrlImageBody", "При отсутствии ссылки вставьте null.");
                if (urlImageBody.Length > 256)
                    ModelState.AddModelError("UrlImageBody", "Ссылка должна содержать не более 256 символов.");
                if (urlImageBody.IndexOf("script") != -1)
                    ModelState.AddModelError("UrlImageBody", "Ссылка не может содержать: script");
                if (urlImageBody.IndexOf("object") != -1)
                    ModelState.AddModelError("UrlImageBody", "Ссылка не может содержать: object");
                if (urlImageBody.IndexOf("applet") != -1)
                    ModelState.AddModelError("UrlImageBody", "Ссылка не может содержать: applet");
                if (urlImageBody.IndexOf("embed") != -1)
                    ModelState.AddModelError("UrlImageBody", "Ссылка не может содержать: embed");
                if (urlImageBody.IndexOf("<%") != -1)
                    ModelState.AddModelError("UrlImageBody", "Ссылка не может содержать: <%%>");
                if (urlImageBody.IndexOf("$") != -1)
                    ModelState.AddModelError("UrlImageBody", "Ссылка не может содержать: $");
            }
            #endregion

            if (ModelState.IsValid)
            {

                _dc.dm_artycle.Create_news(a, title, description,
            body, addComment, urlImageDescription, urlImageBody, descriptionBody,
            registrUser, approvedVotes, closeDate, warning);
                int abc = _dc.dm_artycle.Get_artycle_first().Id;
                return RedirectToAction("ZP", new { a = abc });

            }
            #region меню и титл
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion
            #region настройка сайта
            ViewData["menu_url"] = "create_news";
            //bool разрешены блоги или нет
            ViewData["model_blog"] = _dc.dm_model.Get_model().Blog;
            //bool разрешены новости или нет
            ViewData["model_news"] = _dc.dm_model.Get_model().News;
            //bool разрешены статьи или нет
            ViewData["model_artycle"] = _dc.dm_model.Get_model().Artycle;

            #endregion
            if (a == 0)
            {
                ViewData["Category"] = new SelectList(_dc.dm_artycle_category.Get_category_news(), "Id", "Title");
                ViewData["url"] = "/C/F/0/1/10/News";
            }
            else
            {
                if (_dc.dm_artycle_category.Get_category(a).CategoryId == 0)
                {
                    ViewData["Category"] = new SelectList(_dc.dm_artycle_category.Get_category_news(), "Id", "Title", a);
                }
                else
                {
                    int fjk = _dc.dm_artycle_category.Get_category(a).mt_artycle_category1.Id;
                    ViewData["Category"] = new SelectList(_dc.dm_artycle_category.Get_re_categories(fjk), "Id", "Title", a);

                }
                string abc = _dc.dm_artycle_category.Get_category(a).Path;
                ViewData["url"] = "/C/B/" + a + "/1/10/" + abc;
            }

            return View();

        }

        /*-----------------ZX - редактировать новость---------------------*/
        [Authorize(Roles = "artycle_editor, chief_editor")]
        public ActionResult ZX(int a, int b)
        {
            #region меню и титл
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion
            #region настройка сайта
            ViewData["menu_url"] = "edit_news";
            //bool разрешены блоги или нет
            ViewData["model_blog"] = _dc.dm_model.Get_model().Blog;
            //bool разрешены новости или нет
            ViewData["model_news"] = _dc.dm_model.Get_model().News;
            //bool разрешены статьи или нет
            ViewData["model_artycle"] = _dc.dm_model.Get_model().Artycle;

            #endregion
            mt_artycle x = _dc.dm_artycle.Get_artycle(a);
            ViewData.Model = x;
            if (_dc.dm_artycle_category.Get_category(x.CategoryId).CategoryId == 0)
            {
                ViewData["Category"] = new SelectList(_dc.dm_artycle_category.Get_category_news(), "Id", "Title", x.CategoryId);
            }
            else
            {
                int fjk = _dc.dm_artycle_category.Get_category(x.CategoryId).mt_artycle_category1.Id;
                ViewData["Category"] = new SelectList(_dc.dm_artycle_category.Get_re_categories(fjk), "Id", "Title", x.CategoryId);

            }
            ViewData["url"] = "/C/C/" + a + "/" + x.Path;
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ZX(int a, int b, string title, string description,
            string body, bool addComment, string urlImageDescription, string urlImageBody, bool descriptionBody,
            bool registrUser, bool approvedVotes, System.DateTime closeDate, bool warning)
        {
            #region проверка данных
            if (title == null)
            {
                ModelState.AddModelError("Title", "Название должно содержать не менее 3 символов.");
            }
            else
            {
                if (title.Length <= 2)
                    ModelState.AddModelError("Title", "Название должно содержать не менее 3 символов.");
                if (title.Length > 256)
                    ModelState.AddModelError("Title", "Название должно содержать не более 256 символов.");
                if (title.IndexOf("script") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: script");
                if (title.IndexOf("object") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: object");
                if (title.IndexOf("applet") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: applet");
                if (title.IndexOf("embed") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: embed");
                if (title.IndexOf("<%") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: <%%>");
                if (title.IndexOf("$") != -1)
                    ModelState.AddModelError("Title", "Название не может содержать: $");
            }
            if (description == null)
            {
                ModelState.AddModelError("Description", "Краткое содержание должно содержать не менее 3 символов.");
            }
            else
            {
                if (description.Length <= 2)
                    ModelState.AddModelError("Description", "Краткое содержание должно содержать не менее 3 символов.");
                if (description.Length > 4000)
                    ModelState.AddModelError("Description", "Краткое содержание должно содержать не более 4000 символов.");
                if (description.IndexOf("script") != -1)
                    ModelState.AddModelError("Description", "Краткое содержание не может содержать: script");
                if (description.IndexOf("object") != -1)
                    ModelState.AddModelError("Description", "Краткое содержание не может содержать: object");
                if (description.IndexOf("applet") != -1)
                    ModelState.AddModelError("Description", "Краткое содержание не может содержать: applet");
                if (description.IndexOf("embed") != -1)
                    ModelState.AddModelError("Description", "Краткое содержание не может содержать: embed");
                if (description.IndexOf("<%") != -1)
                    ModelState.AddModelError("Description", "Краткое содержание не может содержать: <%%>");
                if (description.IndexOf("$") != -1)
                    ModelState.AddModelError("Description", "Краткое содержание не может содержать: $");
            }
            if (body == null)
            {
                ModelState.AddModelError("Body", "Статья должна содержать не менее 100 символов.");
            }
            else
            {
                if (body.Length <= 99)
                    ModelState.AddModelError("Body", "Статья должна содержать не менее 100 символов.");
                if (body.IndexOf("script") != -1)
                    ModelState.AddModelError("Body", "Статья не может содержать: script");
                if (body.IndexOf("object") != -1)
                    ModelState.AddModelError("Body", "Статья не может содержать: object");
                if (body.IndexOf("applet") != -1)
                    ModelState.AddModelError("Body", "Статья не может содержать: applet");
                if (body.IndexOf("embed") != -1)
                    ModelState.AddModelError("Body", "Статья не может содержать: embed");
                if (body.IndexOf("<%") != -1)
                    ModelState.AddModelError("Body", "Статья не может содержать: <%%>");
                if (body.IndexOf("$") != -1)
                    ModelState.AddModelError("Body", "Статья не может содержать: $");
            }
            if (urlImageDescription == null)
            {
                ModelState.AddModelError("UrlImageDescription", "При отсутствии ссылки вставьте null.");
            }
            else
            {
                if (urlImageDescription.Length <= 1)
                    ModelState.AddModelError("UrlImageDescription", "При отсутствии ссылки вставьте null.");
                if (urlImageDescription.Length > 256)
                    ModelState.AddModelError("UrlImageDescription", "Ссылка должна содержать не более 256 символов.");
                if (urlImageDescription.IndexOf("script") != -1)
                    ModelState.AddModelError("UrlImageDescription", "Ссылка не может содержать: script");
                if (urlImageDescription.IndexOf("object") != -1)
                    ModelState.AddModelError("UrlImageDescription", "Ссылка не может содержать: object");
                if (urlImageDescription.IndexOf("applet") != -1)
                    ModelState.AddModelError("UrlImageDescription", "Ссылка не может содержать: applet");
                if (urlImageDescription.IndexOf("embed") != -1)
                    ModelState.AddModelError("UrlImageDescription", "Ссылка не может содержать: embed");
                if (urlImageDescription.IndexOf("<%") != -1)
                    ModelState.AddModelError("UrlImageDescription", "Ссылка не может содержать: <%%>");
                if (urlImageDescription.IndexOf("$") != -1)
                    ModelState.AddModelError("UrlImageDescription", "Ссылка не может содержать: $");
            }
            if (urlImageBody == null)
            {
                ModelState.AddModelError("UrlImageBody", "При отсутствии ссылки вставьте null.");
            }
            else
            {
                if (urlImageBody.Length <= 1)
                    ModelState.AddModelError("UrlImageBody", "При отсутствии ссылки вставьте null.");
                if (urlImageBody.Length > 256)
                    ModelState.AddModelError("UrlImageBody", "Ссылка должна содержать не более 256 символов.");
                if (urlImageBody.IndexOf("script") != -1)
                    ModelState.AddModelError("UrlImageBody", "Ссылка не может содержать: script");
                if (urlImageBody.IndexOf("object") != -1)
                    ModelState.AddModelError("UrlImageBody", "Ссылка не может содержать: object");
                if (urlImageBody.IndexOf("applet") != -1)
                    ModelState.AddModelError("UrlImageBody", "Ссылка не может содержать: applet");
                if (urlImageBody.IndexOf("embed") != -1)
                    ModelState.AddModelError("UrlImageBody", "Ссылка не может содержать: embed");
                if (urlImageBody.IndexOf("<%") != -1)
                    ModelState.AddModelError("UrlImageBody", "Ссылка не может содержать: <%%>");
                if (urlImageBody.IndexOf("$") != -1)
                    ModelState.AddModelError("UrlImageBody", "Ссылка не может содержать: $");
            }
            #endregion

            if (ModelState.IsValid)
            {

                _dc.dm_artycle.Update_news(a, b, title, description,
              body, addComment, urlImageDescription, urlImageBody, descriptionBody,
              registrUser, approvedVotes, closeDate, warning);
                return RedirectToAction("ZP", new { a });


            }
            #region меню и титл
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion
            #region настройка сайта
            ViewData["menu_url"] = "edit_news";
            //bool разрешены блоги или нет
            ViewData["model_blog"] = _dc.dm_model.Get_model().Blog;
            //bool разрешены новости или нет
            ViewData["model_news"] = _dc.dm_model.Get_model().News;
            //bool разрешены статьи или нет
            ViewData["model_artycle"] = _dc.dm_model.Get_model().Artycle;

            #endregion
            mt_artycle x = _dc.dm_artycle.Get_artycle(a);
            ViewData.Model = x;
            if (_dc.dm_artycle_category.Get_category(x.CategoryId).CategoryId == 0)
            {
                ViewData["Category"] = new SelectList(_dc.dm_artycle_category.Get_category_news(), "Id", "Title", x.CategoryId);
            }
            else
            {
                int fjk = _dc.dm_artycle_category.Get_category(x.CategoryId).mt_artycle_category1.Id;
                ViewData["Category"] = new SelectList(_dc.dm_artycle_category.Get_re_categories(fjk), "Id", "Title", x.CategoryId);

            }
            ViewData["url"] = "/C/C/" + a + "/1/10/" + x.Path;
            return View();

        }

        /*-----------------ZY - удалить комментарий---------------------*/
        [Authorize]
        public ActionResult ZY(int a)
        {
            string def = _dc.dm_artycle_comment.Get_сomment(a).AddedBy;
            string deef = _dc.dm_artycle_comment.Get_сomment(a).mt_artycle.AddedBy;
            int abc = _dc.dm_artycle_comment.Get_сomment(a).ArtycleId;
            string bcd = _dc.dm_artycle_comment.Get_сomment(a).mt_artycle.Path;
            if (HttpContext.User.Identity.Name == def)
            {

                _dc.dm_artycle_comment.Delete_сomment(a);
                return RedirectToAction("C", new { a = abc, b = bcd });
            }
            if (HttpContext.User.IsInRole("chief_editor"))
            {

                _dc.dm_artycle_comment.Delete_сomment(a);
                return RedirectToAction("C", new { a = abc, b = bcd });


            }
            if (HttpContext.User.IsInRole("artycle_editor"))
            {
                if (HttpContext.User.Identity.Name == deef)
                {
                    _dc.dm_artycle_comment.Delete_сomment(a);
                    return RedirectToAction("C", new { a = abc, b = bcd });
                }

            }
            if (HttpContext.User.IsInRole("blogger"))
            {
                if (HttpContext.User.Identity.Name == deef)
                {
                    _dc.dm_artycle_comment.Delete_сomment(a);
                    return RedirectToAction("C", new { a = abc, b = bcd });
                }

            }

            return RedirectToAction("Error", "Home");
        }

        /*----------------ZZ - редактировать комментарий-------------------*/
        [Authorize]
        public ActionResult ZZ(int a)
        {
            #region меню и титл
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion
            #region настройка сайта
            ViewData["menu_url"] = "edit_comm";
            //bool разрешены блоги или нет
            ViewData["model_blog"] = _dc.dm_model.Get_model().Blog;
            //bool разрешены новости или нет
            ViewData["model_news"] = _dc.dm_model.Get_model().News;
            //bool разрешены статьи или нет
            ViewData["model_artycle"] = _dc.dm_model.Get_model().Artycle;

            #endregion

            mt_artycle_comment x = _dc.dm_artycle_comment.Get_сomment(a);
            ViewData.Model = x;
            ViewData["url"] = "/C/C/" + x.mt_artycle.Id + "/" + x.mt_artycle.Path;
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ZZ(int a, string body)
        {
            #region проверка данных
            if (body == null)
            {
                ModelState.AddModelError("Body", "Комментарий должен содержать не менее 2 символов.");
            }
            else
            {
                if (body.Length < 2)
                    ModelState.AddModelError("Body", "Комментарий должен содержать не менее 2 символов.");
                if (body.Length > 1000)
                    ModelState.AddModelError("Body", "Комментарий должен содержать не более 1000 символов.");
                if (body.IndexOf("script") != -1)
                    ModelState.AddModelError("Body", "Комментарий не может содержать: script");
                if (body.IndexOf("object") != -1)
                    ModelState.AddModelError("Body", "Комментарий не может содержать: object");
                if (body.IndexOf("applet") != -1)
                    ModelState.AddModelError("Body", "Комментарий не может содержать: applet");
                if (body.IndexOf("embed") != -1)
                    ModelState.AddModelError("Body", "Комментарий не может содержать: embed");
                if (body.IndexOf("<%") != -1)
                    ModelState.AddModelError("Body", "Комментарий не может содержать: <%%>");
                if (body.IndexOf("$") != -1)
                    ModelState.AddModelError("Body", "Комментарий не может содержать: $");
            }

            #endregion

            if (ModelState.IsValid)
            {

                _dc.dm_artycle_comment.Update_сomment(a, body);
                int abc = _dc.dm_artycle_comment.Get_сomment(a).mt_artycle.Id;
                string dbc = _dc.dm_artycle_comment.Get_сomment(a).mt_artycle.Path;
                return RedirectToAction("C", new { a = abc, b = dbc });
            }
            #region меню и титл
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion
            #region настройка сайта
            ViewData["menu_url"] = "edit_comm";
            //bool разрешены блоги или нет
            ViewData["model_blog"] = _dc.dm_model.Get_model().Blog;
            //bool разрешены новости или нет
            ViewData["model_news"] = _dc.dm_model.Get_model().News;
            //bool разрешены статьи или нет
            ViewData["model_artycle"] = _dc.dm_model.Get_model().Artycle;

            #endregion
            mt_artycle_comment x = _dc.dm_artycle_comment.Get_сomment(a);
            ViewData.Model = x;
            ViewData["url"] = "/C/C/" + x.mt_artycle.Id + "/" + x.mt_artycle.Path;
            return View();

        }
        /*-----------------------------------------------------*/

        /*  RSS  */

        /*---------------Rss_A - rss поток из конкретной категории-------------------*/
        public ActionResult Rss_A(int a)
        {

            SyndicationFeed rss = new SyndicationFeed(_dc.dm_artycle_category.Get_category(a).Title, "News feed",
                new Uri("http://" + _dc.dm_model.Get_model().DomainName + "/C/B/" + a.ToString() + "/1/10/" + _dc.dm_artycle_category.Get_category(a).Path));
            rss.Items = (from x in this._dc.dm_artycle.Get_artycles_from_category_date_true(a)

                         select new SyndicationItem(x.Title, x.Description, new Uri("http://" + _dc.dm_model.Get_model().DomainName + "/C/C/" + x.Id.ToString())
                )).ToList();
            return new RssFeedResult(rss);
        }

        /*----------------Rss_B - rss поток всех статей--------------------*/
        public ActionResult Rss_B()
        {

            SyndicationFeed rss = new SyndicationFeed("Статьи", "News feed",
                new Uri("http://" + _dc.dm_model.Get_model().DomainName + "/C/A/0/1/10/Artycles"));
            rss.Items = (from x in this._dc.dm_artycle.Get_artycles_date_true()

                         select new SyndicationItem(x.Title, x.Description, new Uri("http://" + _dc.dm_model.Get_model().DomainName + "/C/C/" + x.Id.ToString())
                )).ToList();
            return new RssFeedResult(rss);
        }

        /*----------------Rss_C - rss поток всех новостей------------------*/
        public ActionResult Rss_C()
        {

            SyndicationFeed rss = new SyndicationFeed("Новости", "News feed",
                new Uri("http://" + _dc.dm_model.Get_model().DomainName + "/C/F/0/1/10/News"));
            rss.Items = (from x in this._dc.dm_artycle.Get_news_date_true()

                         select new SyndicationItem(x.Title, x.Description, new Uri("http://" + _dc.dm_model.Get_model().DomainName + "/C/C/" + x.Id.ToString())
                )).ToList();
            return new RssFeedResult(rss);
        }

        /*-----------------Rss_D - rss поток всех постов-------------------*/
        public ActionResult Rss_D()
        {

            SyndicationFeed rss = new SyndicationFeed("Посты", "News feed",
                new Uri("http://" + _dc.dm_model.Get_model().DomainName + "/C/D/0/1/10/Blogs"));
            rss.Items = (from x in this._dc.dm_artycle.Get_posts()

                         select new SyndicationItem(x.Title, x.Description, new Uri("http://" + _dc.dm_model.Get_model().DomainName + "/C/C/" + x.Id.ToString())
                )).ToList();
            return new RssFeedResult(rss);
        }
        /*-----------------------------------------------------*/
    }
}
