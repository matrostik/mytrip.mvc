/*   Mytrip.Mvc.Web.Linq2sql   Copyright ©  2009 Стюхин Олег Анатольевич   */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Mytrip.Mvc.Model.Linq2sql;
using System.Configuration;

namespace Mytrip.Mvc.Web.Linq2sql.Controllers
{
    public class DController : Controller
    {
        /*  ПОДКЛЮЧЕНИЕ К МОДЕЛИ  */

        /*-----------------------------------------------------*/
        IRepository _dc = new IRepository(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
        /*-----------------------------------------------------*/

        /*  ОСНОВНОЙ КОНТЕНТ  */

        /*------------A - результаты поиска статей -------------------*/
        public ActionResult A(int a, int b, int c, string d)
        {
            #region меню и титл
            ViewData["logon_url"] = "/D/A/" + a + "/" + b + "/" + c + "/" + d;
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool включена captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion

            #region настройка сайта
            ViewData["menu_url"] = "search";
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
            if (_dc.dm_model.Get_model().Blog == false)
            {
                if (_dc.dm_model.Get_model().News == false)
                {
                    ViewData["artycle"] = _dc.dm_search.Search_artycles_only_page(b, c, d);
                    ViewData["content_count"] = _dc.dm_search.Search_artycles_only(d).Count();
                }
                else
                {
                    ViewData["artycle"] = _dc.dm_search.Search_artycles_news_page(b, c, d);
                    ViewData["content_count"] = _dc.dm_search.Search_artycles_news(d).Count();
                }
            }
            else
            {
                if (_dc.dm_model.Get_model().News == false)
                {
                    ViewData["artycle"] = _dc.dm_search.Search_artycles_blog_page(b, c, d);
                    ViewData["content_count"] = _dc.dm_search.Search_artycles_blog(d).Count();
                }
                else
                {
                    ViewData["artycle"] = _dc.dm_search.Search_artycles_page(b, c, d);
                    ViewData["content_count"] = _dc.dm_search.Search_artycles(d).Count();
                }
            }
            ViewData["category_status"] = a;
            ViewData["search"] = d;
            #endregion

            #region пейджинг страницы
            //ViewData["content_count"] = _dc.dm_search.Search_artycles(d).Count();
            ViewData["content_url"] = "/D/A/";
            ViewData["content_int"] = c;
            ViewData["content_cat"] = a;
            ViewData["content_page"] = b;
            ViewData["content_path"] = d;
            #endregion

            return View();
        }
        [HttpPost]
        public ActionResult A(string search)
        {
            return RedirectToAction("A", "D", new { a = 0, b = 1, c = 10, d = search });
        }
        /*-----------------------------------------------------*/
    }
}