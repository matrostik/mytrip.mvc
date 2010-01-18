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
using Mytrip.Mvc.Web.Linq2sql.Models;
using Mytrip.Mvc.Language;

namespace Mytrip.Mvc.Web.Linq2sql.Controllers
{
    [HandleError]
    [Localization]
    public class CController : HelperController
    {
        /*  ОСНОВНОЙ КОНТЕНТ  */

        /*---------------------A - все статьи--------------------------------*/
        public ActionResult A(int a, int b, string c)
        {
            if (c == null) {
                return RedirectToAction("A", "A");
            }
            Mytrip_Mvc_Heder("/C/A/" + a + "/" + b + "/" + c);
            Mytrip_Mvc_Right_Column();
            Mytrip_Mvc_Move_Content();
            Mytrip_Mvc_Category_Status(0);
            //////string culture = Session["culture"].ToString();
            if(c=="Articles"){
                Mytrip_Mvc_Menu_Active("artycles");
            #region основной контент
            ViewData["count_canegory"] = _dc.dm_artycle_category.Get_categories(culture).Count();
            ViewData["artycle"] = _dc.dm_artycle.Get_artycles_date_true_home_page(a, b, culture);            
            #endregion            
            ViewData["content_count"] = _dc.dm_artycle.Get_artycles_date_true_home(culture).Count();
            }
            if (c == "News") {
                Mytrip_Mvc_Menu_Active("news");
                #region основной контент
                ViewData["count_canegory"] = _dc.dm_artycle_category.Get_category_news(culture).Count();
                ViewData["artycle"] = _dc.dm_artycle.Get_news_date_true_page(a, b, culture);
                #endregion
                ViewData["content_count"] = _dc.dm_artycle.Get_news_date_true(culture).Count();
            }
            if (c == "Blogs") {
                Mytrip_Mvc_Menu_Active("blogs");
                #region основной контент
                ViewData["artycle"] = _dc.dm_artycle_category.Get_blogs_page(a, b, culture);
                #endregion
                ViewData["content_count"] = _dc.dm_artycle_category.Get_blogs(culture).Count();
            }
            Mytrip_Mvc_Pager_Site("/C/A/", b, 0, a, c);
            return View();
        }
        /*------------B - статьи, посты и новости в определенной рубрике или подрубрике---------------*/
        public ActionResult B(int a, int b, int c, string d)
        {
            Mytrip_Mvc_Heder("/C/B/" + a + "/" + b + "/" + c + "/" + d);
            Mytrip_Mvc_Right_Column();
            Mytrip_Mvc_Move_Content();
            Mytrip_Mvc_Category_Status(b);
            #region основной контент
            mt_artycle_category x = _dc.dm_artycle_category.Get_category(a);
            ViewData.Model = x;
            ViewData["artycle"] = _dc.dm_artycle.Get_artycles_from_category_date_true_page(a, b, c);
            ViewData["content_count"] = _dc.dm_artycle.Get_artycles_from_category_date_true(a).Count();
            if (x.Blog == true)
            {
                Mytrip_Mvc_Menu_Active("blogs");
                _dc.dm_artycle_category.Views_blog(a);
            }
            if (x.News == true) { Mytrip_Mvc_Menu_Active("news"); }
            else
            {
                if (x.Blog == false) {
                    if (x.CategoryId == 0)
                    {
                        if (x.AddMenu == true)
                        {
                            Mytrip_Mvc_Menu_Active(x.Path);
                        }
                        else
                        {
                            Mytrip_Mvc_Menu_Active("artycles");
                        }
                    }
                    else
                    {
                        if (x.mt_artycle_category1.AddMenu == true)
                        {
                            Mytrip_Mvc_Menu_Active(x.mt_artycle_category1.Path);
                        }
                        else
                        {
                            Mytrip_Mvc_Menu_Active("artycles");

                        }
                    }
                }
            }
            #endregion
            Mytrip_Mvc_Pager_Site("/C/B/",c,a,b,d);
            return View();
        }
        /*---------------------C - одна статья, новость или пост------------------------*/
        public ActionResult C(int a, string b)
        {
            Mytrip_Mvc_Heder("/C/C/" + a + "/" + b);
            Mytrip_Mvc_Right_Column();
            Mytrip_Mvc_Move_Content();
            #region основной контент
            if (!HttpContext.User.Identity.IsAuthenticated)
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
                Mytrip_Mvc_Menu_Active("blogs");               
            }
            if (x.mt_artycle_category.News == true) { Mytrip_Mvc_Menu_Active("news"); }
            else
            {
                if (x.mt_artycle_category.Blog == false)
                {
                    if (x.mt_artycle_category.CategoryId == 0)
                    {
                        if (x.mt_artycle_category.AddMenu == true)
                        {
                            Mytrip_Mvc_Menu_Active(x.mt_artycle_category.Path);
                        }
                        else
                        {
                            Mytrip_Mvc_Menu_Active("artycles");
                        }
                    }
                    else
                    {
                        if (x.mt_artycle_category.mt_artycle_category1.AddMenu == true)
                        {
                            Mytrip_Mvc_Menu_Active(x.mt_artycle_category.mt_artycle_category1.Path);
                        }
                        else
                        {
                            Mytrip_Mvc_Menu_Active("artycles");

                        }
                    }
                }
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
                    ModelState.AddModelError("comment", Mytrip_Mvc_Language.comment_valid_2_simbol);
                }
                else
                {
                    if (comment.Length < 2)
                        ModelState.AddModelError("comment", Mytrip_Mvc_Language.comment_valid_2_simbol);
                    if (comment.Length > 1000)
                        ModelState.AddModelError("comment", Mytrip_Mvc_Language.comment_valid_1000_simbol);
                    if (comment.IndexOf("script") != -1)
                        ModelState.AddModelError("comment", Mytrip_Mvc_Language.comment_valid_script);
                    if (comment.IndexOf("object") != -1)
                        ModelState.AddModelError("comment", Mytrip_Mvc_Language.comment_valid_object);
                    if (comment.IndexOf("applet") != -1)
                        ModelState.AddModelError("comment", Mytrip_Mvc_Language.comment_valid_applet);
                    if (comment.IndexOf("embed") != -1)
                        ModelState.AddModelError("comment", Mytrip_Mvc_Language.comment_valid_embed);
                    if (comment.IndexOf("<%") != -1)
                        ModelState.AddModelError("comment", Mytrip_Mvc_Language.comment_valid_procent);
                    if (comment.IndexOf("$") != -1)
                        ModelState.AddModelError("comment", Mytrip_Mvc_Language.comment_valid_baks);
                }

                #endregion

                if (ModelState.IsValid)
                {
                    _dc.dm_artycle_comment.Create_сomment(a, comment);
                    string c = _dc.dm_artycle.Get_artycle(a).Path;
                    return RedirectToAction("C", new { a, b=c });
                }
                Mytrip_Mvc_Heder("/C/C/" + a + "/" + b);
                Mytrip_Mvc_Right_Column();
                Mytrip_Mvc_Move_Content();
                #region основной контент
                if (!HttpContext.User.Identity.IsAuthenticated)
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
                    Mytrip_Mvc_Menu_Active("blogs");
                }
                if (x.mt_artycle_category.News == true) { Mytrip_Mvc_Menu_Active("news"); }
                else
                {
                    if (x.mt_artycle_category.Blog == false)
                    {
                        if (x.mt_artycle_category.CategoryId == 0)
                        {
                            if (x.mt_artycle_category.AddMenu == true)
                            {
                                Mytrip_Mvc_Menu_Active(x.mt_artycle_category.Path);
                            }
                            else
                            {
                                Mytrip_Mvc_Menu_Active("artycles");
                            }
                        }
                        else
                        {
                            if (x.mt_artycle_category.mt_artycle_category1.AddMenu == true)
                            {
                                Mytrip_Mvc_Menu_Active(x.mt_artycle_category.mt_artycle_category1.Path);
                            }
                            else
                            {
                                Mytrip_Mvc_Menu_Active("artycles");

                            }
                        }
                    }
                }

                #endregion
                return View();

            }
        }
        /*-----------------E - все статьи новости и блоги из определенного тега---------------------*/
        public ActionResult E(int a, int b, int c, string d)
        {
            Mytrip_Mvc_Heder("/C/E/" + a + "/" + b + "/" + c + "/" + d);
            Mytrip_Mvc_Right_Column();
            Mytrip_Mvc_Move_Content();
            Mytrip_Mvc_Menu_Active("tegs");
            //////string culture = Session["culture"].ToString();
            #region основной контент
            mt_artycle_teg x = _dc.dm_artycle.Get_teg(a);
            ViewData.Model = x;
            if (_dc.dm_model.Get_model().Blog == true)
            {
                if (_dc.dm_model.Get_model().News == true)
                {
                    ViewData["artycle"] = _dc.dm_artycle.Get_artycles_blog_news_from_teg_date_true_page(a, b, c, culture);
                    ViewData["content_count"] = _dc.dm_artycle.Get_artycles_blog_news_from_teg_date_true(a, culture).Count();
                }
                else
                {
                    ViewData["artycle"] = _dc.dm_artycle.Get_artycles_blog_from_teg_date_true_page(a, b, c, culture);
                    ViewData["content_count"] = _dc.dm_artycle.Get_artycles_blog_from_teg_date_true(a, culture).Count();
                }
            }
            else
            {
                if (_dc.dm_model.Get_model().News == true)
                {
                    ViewData["artycle"] = _dc.dm_artycle.Get_artycles_news_from_teg_date_true_page(a, b, c, culture);
                    ViewData["content_count"] = _dc.dm_artycle.Get_artycles_news_from_teg_date_true(a, culture).Count();
                }
                else
                {
                    ViewData["artycle"] = _dc.dm_artycle.Get_artycles_from_teg_date_true_page(a, b, c, culture);
                    ViewData["content_count"] = _dc.dm_artycle.Get_artycles_from_teg_date_true(a, culture).Count();
                }

            }
            ViewData["helper_category_status"] = 0;
            #endregion
            Mytrip_Mvc_Pager_Site("/C/E/", c, a, b, d);
            return View();
        }
        /*-----------------------------------------------------*/

        /*  ДОБАВЛЕНИЕ, РЕДАКТИРОВАНИЕ, УДАЛЕНИЕ  */

        /*----------------ZA - создать рубрику, подрубрику или тему блога ---------------------*/
        [Authorize(Roles = "artycle_editor, chief_editor, blogger")]
        public ActionResult ZA(int a, string b)
        {
            if (b == null)
            {
                return RedirectToAction("A", "A");
            }
            Mytrip_Mvc_Heder("/C/ZA/" + a + "/" + b);
           /////string culture = Session["culture"].ToString();
            #region общий контент
            ViewData["create"] = b;
            #endregion
            #region создать рубрику статей
            if (b == "Create_article_heding")
            {
                if (!HttpContext.User.IsInRole("artycle_editor"))
                {
                    if (!HttpContext.User.IsInRole("chief_editor"))
                    {
                        return RedirectToAction("A", "A");
                    }
                }
                ViewData["create_heding"] = true;
                Mytrip_Mvc_Menu_Active("rub_artycle");
                Mytrip_Mvc_Back_Url("/C/A/1/10/Articles");
            }
            #endregion
            #region создать рубрику новостей
            if (b == "Create_news_heding") 
            {
                if (!HttpContext.User.IsInRole("artycle_editor"))
                {
                    if (!HttpContext.User.IsInRole("chief_editor"))
                    {
                        return RedirectToAction("A", "A");
                    }
                }
                ViewData["create_heding"] = false;
                Mytrip_Mvc_Menu_Active("rub_news");
                Mytrip_Mvc_Back_Url("/C/A/1/10/News");
            }
            #endregion
            #region создать подрубрику в рубрике статей
            if (b == "Create_article_subheding") {
                if (!HttpContext.User.IsInRole("artycle_editor"))
                {
                    if (!HttpContext.User.IsInRole("chief_editor"))
                    {
                        return RedirectToAction("A", "A");
                    }
                }
                ViewData["create_heding"] = false;
                Mytrip_Mvc_Menu_Active("thema_article");                
                string c = _dc.dm_artycle_category.Get_category(a).Path;
                Mytrip_Mvc_Back_Url("/C/B/" + a + "/1/10/" + c);
            }
            #endregion
            #region создать подрубрику в рубрике новостей
            if (b == "Create_news_subheding") {
                if (!HttpContext.User.IsInRole("artycle_editor"))
                {
                    if (!HttpContext.User.IsInRole("chief_editor"))
                    {
                        return RedirectToAction("A", "A");
                    }
                }
                ViewData["create_heding"] = false;
                Mytrip_Mvc_Menu_Active("thema_news");
                string c = _dc.dm_artycle_category.Get_category(a).Path;
                Mytrip_Mvc_Back_Url("/C/B/" + a + "/1/10/" + c);
            }
            #endregion
            #region создать тему блога
            if (b == "Create_blog_heading") {
                if (HttpContext.User.IsInRole("blogger"))
                {
                    if (_dc.dm_artycle_category.Get_category(a).AddedBy == HttpContext.User.Identity.Name)
                    {
                        ViewData["create_heding"] = false;
                        Mytrip_Mvc_Menu_Active("thema_blog");
                        string c = _dc.dm_artycle_category.Get_category(a).Path;
                        Mytrip_Mvc_Back_Url("/C/B/" + a + "/1/10/" + c);
                    }
                    else
                    {
                        return RedirectToAction("A", "A");
                    }
                }
                else
                {
                    return RedirectToAction("A", "A");
                }
            }
            #endregion
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ZA(int a,string b, string title, bool? addmenu, bool? allCulture)
        {
            //////string culture = Session["culture"].ToString();
            if (allCulture == null)
            allCulture = false;
            #region проверка данных
            if (title == null)
            {
                ModelState.AddModelError("Title", Mytrip_Mvc_Language.title_valid_3_simbols);
            }
            else
            {
                if (title.Length <= 2)
                    ModelState.AddModelError("Title", Mytrip_Mvc_Language.title_valid_3_simbols);
                if (title.Length > 256)
                    ModelState.AddModelError("Title", Mytrip_Mvc_Language.title_valid_256_simbols);
                if (title.IndexOf("script") != -1)
                    ModelState.AddModelError("Title", Mytrip_Mvc_Language.title_valid_script);
                if (title.IndexOf("object") != -1)
                    ModelState.AddModelError("Title", Mytrip_Mvc_Language.title_valid_object);
                if (title.IndexOf("applet") != -1)
                    ModelState.AddModelError("Title", Mytrip_Mvc_Language.title_valid_applet);
                if (title.IndexOf("embed") != -1)
                    ModelState.AddModelError("Title", Mytrip_Mvc_Language.title_valid_embed);
                if (title.IndexOf("<%") != -1)
                    ModelState.AddModelError("Title", Mytrip_Mvc_Language.title_valid_procent);
                if (title.IndexOf("$") != -1)
                    ModelState.AddModelError("Title", Mytrip_Mvc_Language.title_valid_baks);
            }

            #endregion
            #region сохранение данных
            if (ModelState.IsValid)
            {
                if (b == "Create_article_heding")
                {
                    _dc.dm_artycle_category.Create_category(title, (bool)addmenu,(bool)allCulture,culture);
                } if (b == "Create_news_heding")
                {
                    _dc.dm_artycle_category.Create_category_news(title, (bool)allCulture, culture);
                }
                if (b == "Create_article_subheding")
                {
                    _dc.dm_artycle_category.Create_re_category(title, a);
                }
                if (b == "Create_news_subheding")
                {
                    _dc.dm_artycle_category.Create_re_category_news(title, a);
                }
                if (b == "Create_blog_heading")
                {
                    _dc.dm_artycle_category.Create_re_blog(title, a);
                }
                int aa = _dc.dm_artycle_category.Get_blog_first().Id;
                string d = _dc.dm_artycle_category.Get_blog_first().Path;
                return RedirectToAction("B", new { a=aa, b = 1, c = 10, d });
            }
            #endregion
            Mytrip_Mvc_Heder("/C/ZA/" + a + "/" + b);
            #region общий контент
            ViewData["create"] = b;
            #endregion
            #region создать рубрику статей
            if (b == "Create_article_heding")
            {
                if (!HttpContext.User.IsInRole("artycle_editor"))
                {
                    if (!HttpContext.User.IsInRole("chief_editor"))
                    {
                        return RedirectToAction("A", "A");
                    }
                }
                ViewData["create_heding"] = true;
                Mytrip_Mvc_Menu_Active("rub_artycle");
                Mytrip_Mvc_Back_Url("/C/A/1/10/Articles");
            }
            #endregion
            #region создать рубрику новостей
            if (b == "Create_news_heding")
            {
                if (!HttpContext.User.IsInRole("artycle_editor"))
                {
                    if (!HttpContext.User.IsInRole("chief_editor"))
                    {
                        return RedirectToAction("A", "A");
                    }
                }
                ViewData["create_heding"] = false;
                Mytrip_Mvc_Menu_Active("rub_news");
                Mytrip_Mvc_Back_Url("/C/A/1/10/News");
            }
            #endregion
            #region создать подрубрику в рубрике статей
            if (b == "Create_article_subheding")
            {
                if (!HttpContext.User.IsInRole("artycle_editor"))
                {
                    if (!HttpContext.User.IsInRole("chief_editor"))
                    {
                        return RedirectToAction("A", "A");
                    }
                }
                ViewData["create_heding"] = false;
                Mytrip_Mvc_Menu_Active("thema_article");
                string c = _dc.dm_artycle_category.Get_category(a).Path;
                Mytrip_Mvc_Back_Url("/C/B/" + a + "/1/10/" + c);
            }
            #endregion
            #region создать подрубрику в рубрике новостей
            if (b == "Create_news_subheding")
            {
                if (!HttpContext.User.IsInRole("artycle_editor"))
                {
                    if (!HttpContext.User.IsInRole("chief_editor"))
                    {
                        return RedirectToAction("A", "A");
                    }
                }
                ViewData["create_heding"] = false;
                Mytrip_Mvc_Menu_Active("thema_news");
                string c = _dc.dm_artycle_category.Get_category(a).Path;
                Mytrip_Mvc_Back_Url("/C/B/" + a + "/1/10/" + c);
            }
            #endregion
            #region создать тему блога
            if (b == "Create_blog_heading")
            {
                if (HttpContext.User.IsInRole("blogger"))
                {
                    if (_dc.dm_artycle_category.Get_category(a).AddedBy == HttpContext.User.Identity.Name)
                    {
                        ViewData["create_heding"] = false;
                        Mytrip_Mvc_Menu_Active("thema_blog");
                        string c = _dc.dm_artycle_category.Get_category(a).Path;
                        Mytrip_Mvc_Back_Url("/C/B/" + a + "/1/10/" + c);
                    }
                    else
                    {
                        return RedirectToAction("A", "A");
                    }
                }
                else
                {
                    return RedirectToAction("A", "A");
                }
            }
            #endregion
            return View();

        }
        /*--------------------ZB - редактировать рубрику------------------------*/
        [Authorize(Roles = "artycle_editor, chief_editor, blogger")]
        public ActionResult ZB(int a, string b)
        {
            if (b == null)
            {
                return RedirectToAction("A", "A");
            }
            Mytrip_Mvc_Heder("/C/ZB/" + a + "/" + b);
            ///string culture = Session["culture"].ToString();
            
            #region общий контент
            ViewData["edit"] = b;
            mt_artycle_category x = _dc.dm_artycle_category.Get_category(a);
            ViewData.Model = x;
            Mytrip_Mvc_Back_Url("/C/B/" + a + "/1/10/" + x.Path);
            #endregion
            #region редактировать рубрику статей
            if (b == "Edit_article_heding")
            {
                if (!HttpContext.User.IsInRole("artycle_editor"))
                {
                    if (!HttpContext.User.IsInRole("chief_editor"))
                    {
                        return RedirectToAction("A", "A");
                    }
                }
                ViewData["create_heding"] = true;
                Mytrip_Mvc_Menu_Active("edit_rub_artycle");
            }
            #endregion
            #region редактировать рубрику новостей
            if (b == "Edit_news_heding")
            {
                if (!HttpContext.User.IsInRole("artycle_editor"))
                {
                    if (!HttpContext.User.IsInRole("chief_editor"))
                    {
                        return RedirectToAction("A", "A");
                    }
                }
                ViewData["create_heding"] = false;
                Mytrip_Mvc_Menu_Active("edit_rub_news");
            }
            #endregion
            #region редактировать блог
            if (b == "Edit_blog")
            {
                if (HttpContext.User.IsInRole("blogger"))
                {
                    if (_dc.dm_artycle_category.Get_category(a).AddedBy == HttpContext.User.Identity.Name)
                    {
                        ViewData["create_heding"] = false;
                        Mytrip_Mvc_Menu_Active("edit_blog"); 
                    }
                    else
                    {
                        if (HttpContext.User.IsInRole("chief_editor"))
                        {
                            if (_dc.dm_artycle_category.Get_category(a).AddedBy == HttpContext.User.Identity.Name)
                            {
                                ViewData["create_heding"] = false;
                                Mytrip_Mvc_Menu_Active("edit_blog"); 
                            }
                            else { return RedirectToAction("A", "A"); }
                        }
                        else { return RedirectToAction("A", "A"); }
                    }
                }
                else
                {
                    return RedirectToAction("A", "A");
                }

            }
            #endregion
            #region редактировать подрубрику в рубрике статей
            if (b == "Edit_article_subheding")
            {
                if (!HttpContext.User.IsInRole("artycle_editor"))
                {
                    if (!HttpContext.User.IsInRole("chief_editor"))
                    {
                        return RedirectToAction("A", "A");
                    }
                }
                ViewData["create_heding"] = false;
                Mytrip_Mvc_Menu_Active("edit_thema_article");
            }
            #endregion
            #region редактировать подрубрику в рубрике новостей
            if (b == "Edit_news_subheding")
            {
                if (!HttpContext.User.IsInRole("artycle_editor"))
                {
                    if (!HttpContext.User.IsInRole("chief_editor"))
                    {
                        return RedirectToAction("A", "A");
                    }
                }
                ViewData["create_heding"] = false;
                Mytrip_Mvc_Menu_Active("edit_thema_news");
            }
            #endregion
            #region редактировать тему блога
            if (b == "Edit_blog_heding")
            {
                if (HttpContext.User.IsInRole("blogger"))
                {
                    if (_dc.dm_artycle_category.Get_category(a).AddedBy == HttpContext.User.Identity.Name)
                    {
                        ViewData["create_heding"] = false;
                        Mytrip_Mvc_Menu_Active("edit_thema_blog");
                    }
                    else
                    {
                        if (HttpContext.User.IsInRole("chief_editor"))
                        {
                            if (_dc.dm_artycle_category.Get_category(a).AddedBy == HttpContext.User.Identity.Name)
                            {
                                ViewData["create_heding"] = false;
                                Mytrip_Mvc_Menu_Active("edit_thema_blog");
                            }
                            else { return RedirectToAction("A", "A"); }
                        }
                        else { return RedirectToAction("A", "A"); }
                    }
                }
                else
                {
                    return RedirectToAction("A", "A");
                }

            }
            #endregion
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ZB(int a, string b, string title, bool? addmenu, bool? allCulture)
        {
            if (allCulture == null)
                allCulture = false;
            #region проверка данных
            if (title == null)
            {
                ModelState.AddModelError("Title", Mytrip_Mvc_Language.title_valid_3_simbols);
            }
            else
            {
                if (title.Length <= 2)
                    ModelState.AddModelError("Title", Mytrip_Mvc_Language.title_valid_3_simbols);
                if (title.Length > 256)
                    ModelState.AddModelError("Title", Mytrip_Mvc_Language.title_valid_256_simbols);
                if (title.IndexOf("script") != -1)
                    ModelState.AddModelError("Title", Mytrip_Mvc_Language.title_valid_script);
                if (title.IndexOf("object") != -1)
                    ModelState.AddModelError("Title", Mytrip_Mvc_Language.title_valid_object);
                if (title.IndexOf("applet") != -1)
                    ModelState.AddModelError("Title", Mytrip_Mvc_Language.title_valid_applet);
                if (title.IndexOf("embed") != -1)
                    ModelState.AddModelError("Title", Mytrip_Mvc_Language.title_valid_embed);
                if (title.IndexOf("<%") != -1)
                    ModelState.AddModelError("Title", Mytrip_Mvc_Language.title_valid_procent);
                if (title.IndexOf("$") != -1)
                    ModelState.AddModelError("Title", Mytrip_Mvc_Language.title_valid_baks);
            }

            #endregion
            if (ModelState.IsValid)
            {
                if (b == "Edit_article_heding")
                { _dc.dm_artycle_category.Update_category(a, title, (bool)addmenu, (bool)allCulture); }
                if (b == "Edit_news_heding")
                { _dc.dm_artycle_category.Update_blog(a, title); }
                if (b == "Edit_blog")
                { _dc.dm_artycle_category.Update_blog(a, title); }
                if (b == "Edit_article_subheding")
                { _dc.dm_artycle_category.Update_blog(a, title); }
                if (b == "Edit_news_subheding")
                { _dc.dm_artycle_category.Update_blog(a, title); }
                if (b == "Edit_blog_heding")
                { _dc.dm_artycle_category.Update_blog(a, title); }                
                string d = _dc.dm_artycle_category.Get_category(a).Path;
                return RedirectToAction("B", new { a, b = 1, c = 10, d });
            }
            Mytrip_Mvc_Heder("/C/ZB/" + a + "/" + b);
            //////string culture = Session["culture"].ToString();

            #region общий контент
            ViewData["edit"] = b;
            mt_artycle_category x = _dc.dm_artycle_category.Get_category(a);
            ViewData.Model = x;
            Mytrip_Mvc_Back_Url("/C/B/" + a + "/1/10/" + x.Path);
            #endregion
            #region редактировать рубрику статей
            if (b == "Edit_article_heding")
            {
                if (!HttpContext.User.IsInRole("artycle_editor"))
                {
                    if (!HttpContext.User.IsInRole("chief_editor"))
                    {
                        return RedirectToAction("A", "A");
                    }
                }
                ViewData["create_heding"] = true;
                Mytrip_Mvc_Menu_Active("edit_rub_artycle");
            }
            #endregion
            #region редактировать рубрику новостей
            if (b == "Edit_news_heding")
            {
                if (!HttpContext.User.IsInRole("artycle_editor"))
                {
                    if (!HttpContext.User.IsInRole("chief_editor"))
                    {
                        return RedirectToAction("A", "A");
                    }
                }
                ViewData["create_heding"] = false;
                Mytrip_Mvc_Menu_Active("edit_rub_news");
            }
            #endregion
            #region редактировать блог
            if (b == "Edit_blog")
            {
                if (HttpContext.User.IsInRole("blogger"))
                {
                    if (_dc.dm_artycle_category.Get_category(a).AddedBy == HttpContext.User.Identity.Name)
                    {
                        ViewData["create_heding"] = false;
                        Mytrip_Mvc_Menu_Active("edit_blog");
                    }
                    else
                    {
                        if (HttpContext.User.IsInRole("chief_editor"))
                        {
                            if (_dc.dm_artycle_category.Get_category(a).AddedBy == HttpContext.User.Identity.Name)
                            {
                                ViewData["create_heding"] = false;
                                Mytrip_Mvc_Menu_Active("edit_blog");
                            }
                            else { return RedirectToAction("A", "A"); }
                        }
                        else { return RedirectToAction("A", "A"); }
                    }
                }
                else
                {
                    return RedirectToAction("A", "A");
                }

            }
            #endregion
            #region редактировать подрубрику в рубрике статей
            if (b == "Edit_article_subheding")
            {
                if (!HttpContext.User.IsInRole("artycle_editor"))
                {
                    if (!HttpContext.User.IsInRole("chief_editor"))
                    {
                        return RedirectToAction("A", "A");
                    }
                }
                ViewData["create_heding"] = false;
                Mytrip_Mvc_Menu_Active("edit_thema_article");
            }
            #endregion
            #region редактировать подрубрику в рубрике новостей
            if (b == "Edit_news_subheding")
            {
                if (!HttpContext.User.IsInRole("artycle_editor"))
                {
                    if (!HttpContext.User.IsInRole("chief_editor"))
                    {
                        return RedirectToAction("A", "A");
                    }
                }
                ViewData["create_heding"] = false;
                Mytrip_Mvc_Menu_Active("edit_thema_news");
            }
            #endregion
            #region редактировать тему блога
            if (b == "Edit_blog_heding")
            {
                if (HttpContext.User.IsInRole("blogger"))
                {
                    if (_dc.dm_artycle_category.Get_category(a).AddedBy == HttpContext.User.Identity.Name)
                    {
                        ViewData["create_heding"] = false;
                        Mytrip_Mvc_Menu_Active("edit_thema_blog");
                    }
                    else
                    {
                        if (HttpContext.User.IsInRole("chief_editor"))
                        {
                            if (_dc.dm_artycle_category.Get_category(a).AddedBy == HttpContext.User.Identity.Name)
                            {
                                ViewData["create_heding"] = false;
                                Mytrip_Mvc_Menu_Active("edit_thema_blog");
                            }
                            else { return RedirectToAction("A", "A"); }
                        }
                        else { return RedirectToAction("A", "A"); }
                    }
                }
                else
                {
                    return RedirectToAction("A", "A");
                }

            }
            #endregion
            return View();

        }
        
        /*-----------------ZF - создать блог пользователя ---------------------*/
        [Authorize]
        public ActionResult ZF()
        {
            //////string culture = Session["culture"].ToString();            
            _dc.dm_artycle_category.Create_blog(culture);
            if (HttpContext.User.IsInRole("blogger")) { }
            else
            {
                Roles.AddUserToRole(HttpContext.User.Identity.Name, "blogger");
            }
            int a = _dc.dm_artycle_category.Get_blog_first().Id;
            string d = _dc.dm_artycle_category.Get_blog_first().Path;
            return RedirectToAction("B", new { a, b = 1, c = 10, d });

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
                    return RedirectToAction("A", new { a = 1, b = 10, c = "Articles" });
                }

            }
            if (HttpContext.User.IsInRole("chief_editor"))
            {

                _dc.dm_artycle_category.Delete_category(a);
                return RedirectToAction("A", new { a = 1, b = 10, c = "Articles" });


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
                    return RedirectToAction("A", new { a = 1, b = 10, c = "Articles" });
                }

            }
            if (HttpContext.User.IsInRole("chief_editor"))
            {

                _dc.dm_artycle_category.Delete_category(a);
                return RedirectToAction("A", new { a = 1, b = 10, c = "Articles" });


            }
            return RedirectToAction("Error", "Home");

        }
        /*----------------- ZJ - создать статью, новость или пост ---------------------*/
        [Authorize(Roles = "artycle_editor, chief_editor, blogger")]
        public ActionResult ZJ(int a, string b)
        {
            if (b == null)
            {
                return RedirectToAction("A", "A");
            }
            Mytrip_Mvc_Heder("/C/ZJ/" + a + "/" + b);
            string culture = Session["culture"].ToString();
            
            #region общий контент
            ViewData["create"] = b;
            #endregion
            #region создать статью
            if (b == "Create_article")
                {
                    if (!HttpContext.User.IsInRole("artycle_editor"))
                    {
                        if (!HttpContext.User.IsInRole("chief_editor"))
                        {
                            return RedirectToAction("A", "A");                        
                        }
                    }
                    Mytrip_Mvc_Menu_Active("create_artycle");

                    if (a == 0)
                    {
                        ViewData["Category"] = new SelectList(_dc.dm_artycle_category.Get_categories(culture), "Id", "Title");
                        Mytrip_Mvc_Back_Url("/C/A/1/10/Articles");
                        ViewData["category_allculture"] = true;
                    }
                    else
                    {
                        if (_dc.dm_artycle_category.Get_category(a).CategoryId == 0)
                        {
                            ViewData["Category"] = new SelectList(_dc.dm_artycle_category.Get_categories(culture), "Id", "Title", a);
                        }
                        else
                        {
                            int fjk = _dc.dm_artycle_category.Get_category(a).mt_artycle_category1.Id;
                            ViewData["Category"] = new SelectList(_dc.dm_artycle_category.Get_re_categories(fjk), "Id", "Title", a);

                        }
                        string abc = _dc.dm_artycle_category.Get_category(a).Path;
                        Mytrip_Mvc_Back_Url("/C/B/" + a + "/1/10/" + abc);
                        ViewData["category_allculture"] = _dc.dm_artycle_category.Get_category(a).AllCulture;
                    }
                }
                #endregion
            #region создать новость
                if (b == "Create_news")
                {
                    if (!HttpContext.User.IsInRole("artycle_editor"))
                    {
                        if (!HttpContext.User.IsInRole("chief_editor"))
                        {
                            return RedirectToAction("A", "A");
                        }
                    }
                    Mytrip_Mvc_Menu_Active("create_news");

                    if (a == 0)
                    {
                        ViewData["Category"] = new SelectList(_dc.dm_artycle_category.Get_category_news(culture), "Id", "Title");
                        Mytrip_Mvc_Back_Url("/C/A/1/10/News");
                        ViewData["category_allculture"] = true;
                    }
                    else
                    {
                        if (_dc.dm_artycle_category.Get_category(a).CategoryId == 0)
                        {
                            ViewData["Category"] = new SelectList(_dc.dm_artycle_category.Get_category_news(culture), "Id", "Title", a);
                        }
                        else
                        {
                            int fjk = _dc.dm_artycle_category.Get_category(a).mt_artycle_category1.Id;
                            ViewData["Category"] = new SelectList(_dc.dm_artycle_category.Get_re_categories(fjk), "Id", "Title", a);

                        }
                        string abc = _dc.dm_artycle_category.Get_category(a).Path;
                        Mytrip_Mvc_Back_Url("/C/B/" + a + "/1/10/" + abc);
                        ViewData["category_allculture"] = _dc.dm_artycle_category.Get_category(a).AllCulture;
                    }
                }
                #endregion
            #region создать пост
            if (b == "Create_post")
            {
                if (HttpContext.User.IsInRole("blogger"))
                {
                    if (_dc.dm_artycle_category.Get_category(a).AddedBy == HttpContext.User.Identity.Name)
                    {
                        Mytrip_Mvc_Menu_Active("create_post");
                        string dfg = _dc.dm_artycle_category.Get_category(a).Path;
                        Mytrip_Mvc_Back_Url("/C/B/" + a + "/1/10/" + dfg);
                    }
                    else
                    {
                        return RedirectToAction("A", "A");
                    }
                }
                else
                {
                    return RedirectToAction("A", "A");
                }
            }
            #endregion
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ZJ(int a, string b, string title, string description,
            string body, bool? addComment, string urlImageDescription, string urlImageBody, bool descriptionBody,
            bool registrUser, bool? approvedVotes, System.DateTime? closeDate, bool? warning, bool? allCulture)
        {
            #region задать культуру
            /////string culture = Session["culture"].ToString();
            if (allCulture == null)
                allCulture = false;
            #endregion
            #region проверка данных
            if (title == null)
            {
                ModelState.AddModelError("Title", Mytrip_Mvc_Language.title_valid_3_simbols);
            }
            else
            {
                if (title.Length <= 2)
                    ModelState.AddModelError("Title", Mytrip_Mvc_Language.title_valid_3_simbols);
                if (title.Length > 256)
                    ModelState.AddModelError("Title", Mytrip_Mvc_Language.title_valid_256_simbols);
                if (title.IndexOf("script") != -1)
                    ModelState.AddModelError("Title", Mytrip_Mvc_Language.title_valid_script);
                if (title.IndexOf("object") != -1)
                    ModelState.AddModelError("Title", Mytrip_Mvc_Language.title_valid_object);
                if (title.IndexOf("applet") != -1)
                    ModelState.AddModelError("Title", Mytrip_Mvc_Language.title_valid_applet);
                if (title.IndexOf("embed") != -1)
                    ModelState.AddModelError("Title", Mytrip_Mvc_Language.title_valid_embed);
                if (title.IndexOf("<%") != -1)
                    ModelState.AddModelError("Title", Mytrip_Mvc_Language.title_valid_procent);
                if (title.IndexOf("$") != -1)
                    ModelState.AddModelError("Title", Mytrip_Mvc_Language.title_valid_baks);
            }

            if (description == null)
            {
                ModelState.AddModelError("Description", Mytrip_Mvc_Language.Description_valid_3_simbols);
            }
            else
            {
                if (description.Length <= 2)
                    ModelState.AddModelError("Description", Mytrip_Mvc_Language.Description_valid_3_simbols);
                if (description.Length > 4000)
                    ModelState.AddModelError("Description", Mytrip_Mvc_Language.Description_valid_4000_simbols);
                if (description.IndexOf("script") != -1)
                    ModelState.AddModelError("Description", Mytrip_Mvc_Language.Description_valid_script);
                if (description.IndexOf("object") != -1)
                    ModelState.AddModelError("Description", Mytrip_Mvc_Language.Description_valid_object);
                if (description.IndexOf("applet") != -1)
                    ModelState.AddModelError("Description", Mytrip_Mvc_Language.Description_valid_applet);
                if (description.IndexOf("embed") != -1)
                    ModelState.AddModelError("Description", Mytrip_Mvc_Language.Description_valid_embed);
                if (description.IndexOf("<%") != -1)
                    ModelState.AddModelError("Description", Mytrip_Mvc_Language.Description_valid_procent);
                if (description.IndexOf("$") != -1)
                    ModelState.AddModelError("Description", Mytrip_Mvc_Language.Description_valid_baks);
            }
            if (body == null)
            {
                ModelState.AddModelError("Body", Mytrip_Mvc_Language.Artycles_valid_100_simbols);
            }
            else
            {
                if (body.Length <= 99)
                    ModelState.AddModelError("Body", Mytrip_Mvc_Language.Artycles_valid_100_simbols);
                if (body.IndexOf("script") != -1)
                    ModelState.AddModelError("Body", Mytrip_Mvc_Language.Artycles_valid_script);
                if (body.IndexOf("object") != -1)
                    ModelState.AddModelError("Body", Mytrip_Mvc_Language.Artycles_valid_object);
                if (body.IndexOf("applet") != -1)
                    ModelState.AddModelError("Body", Mytrip_Mvc_Language.Artycles_valid_applet);
                if (body.IndexOf("embed") != -1)
                    ModelState.AddModelError("Body", Mytrip_Mvc_Language.Artycles_valid_embed);
                if (body.IndexOf("<%") != -1)
                    ModelState.AddModelError("Body", Mytrip_Mvc_Language.Artycles_valid_procent);
                if (body.IndexOf("$") != -1)
                    ModelState.AddModelError("Body", Mytrip_Mvc_Language.Artycles_valid_baks);
            }
            if (urlImageDescription == null)
            {
                ModelState.AddModelError("UrlImageDescription", Mytrip_Mvc_Language.image_valid_null);
            }
            else
            {
                if (urlImageDescription.Length <= 1)
                    ModelState.AddModelError("UrlImageDescription", Mytrip_Mvc_Language.image_valid_null);
                if (urlImageDescription.Length > 256)
                    ModelState.AddModelError("UrlImageDescription", Mytrip_Mvc_Language.image_valid_256_simbols);
                if (urlImageDescription.IndexOf("script") != -1)
                    ModelState.AddModelError("UrlImageDescription", Mytrip_Mvc_Language.image_valid_script);
                if (urlImageDescription.IndexOf("object") != -1)
                    ModelState.AddModelError("UrlImageDescription", Mytrip_Mvc_Language.image_valid_object);
                if (urlImageDescription.IndexOf("applet") != -1)
                    ModelState.AddModelError("UrlImageDescription", Mytrip_Mvc_Language.image_valid_applet);
                if (urlImageDescription.IndexOf("embed") != -1)
                    ModelState.AddModelError("UrlImageDescription", Mytrip_Mvc_Language.image_valid_embed);
                if (urlImageDescription.IndexOf("<%") != -1)
                    ModelState.AddModelError("UrlImageDescription", Mytrip_Mvc_Language.image_valid_procent);
                if (urlImageDescription.IndexOf("$") != -1)
                    ModelState.AddModelError("UrlImageDescription", Mytrip_Mvc_Language.image_valid_baks);
            }
            if (urlImageBody == null)
            {
                ModelState.AddModelError("UrlImageBody", Mytrip_Mvc_Language.image_valid_null);
            }
            else
            {
                if (urlImageBody.Length <= 1)
                    ModelState.AddModelError("UrlImageBody", Mytrip_Mvc_Language.image_valid_null);
                if (urlImageBody.Length > 256)
                    ModelState.AddModelError("UrlImageBody", Mytrip_Mvc_Language.image_valid_256_simbols);
                if (urlImageBody.IndexOf("script") != -1)
                    ModelState.AddModelError("UrlImageBody", Mytrip_Mvc_Language.image_valid_script);
                if (urlImageBody.IndexOf("object") != -1)
                    ModelState.AddModelError("UrlImageBody", Mytrip_Mvc_Language.image_valid_object);
                if (urlImageBody.IndexOf("applet") != -1)
                    ModelState.AddModelError("UrlImageBody", Mytrip_Mvc_Language.image_valid_applet);
                if (urlImageBody.IndexOf("embed") != -1)
                    ModelState.AddModelError("UrlImageBody", Mytrip_Mvc_Language.image_valid_embed);
                if (urlImageBody.IndexOf("<%") != -1)
                    ModelState.AddModelError("UrlImageBody", Mytrip_Mvc_Language.image_valid_procent);
                if (urlImageBody.IndexOf("$") != -1)
                    ModelState.AddModelError("UrlImageBody", Mytrip_Mvc_Language.image_valid_baks);
            }
            #endregion
            #region сохранить данные
            if (ModelState.IsValid)
            {
                if (b == "Create_article")
                {
                    _dc.dm_artycle.Create_artycle(a, title, description,
                body, (bool)addComment, urlImageDescription, urlImageBody, descriptionBody,
                registrUser, (bool)approvedVotes, (System.DateTime)closeDate, (bool)allCulture, culture);
                } 
                if (b == "Create_news")
                {
                    _dc.dm_artycle.Create_news(a, title, description,
                body, (bool)addComment, urlImageDescription, urlImageBody, descriptionBody,
                registrUser, (bool)approvedVotes, (System.DateTime)closeDate, (bool)warning, (bool)allCulture, culture);
                }
                if (b == "Create_post")
                {
                    _dc.dm_artycle.Create_post(a, title, description,
            body, urlImageDescription, urlImageBody, descriptionBody,
            registrUser);
                }
                int abc = _dc.dm_artycle.Get_artycle_first().Id;
                return RedirectToAction("ZP", new { a = abc });

            }
            #endregion
            Mytrip_Mvc_Heder("/C/ZJ/" + a + "/" + b);
            #region общий контент
            ViewData["create"] = b;
            #endregion
            #region создать статью
            if (b == "Create_article")
            {
                if (!HttpContext.User.IsInRole("artycle_editor"))
                {
                    if (!HttpContext.User.IsInRole("chief_editor"))
                    {
                        return RedirectToAction("A", "A");
                    }
                }
                Mytrip_Mvc_Menu_Active("create_artycle");

                if (a == 0)
                {
                    ViewData["Category"] = new SelectList(_dc.dm_artycle_category.Get_categories(culture), "Id", "Title");
                    Mytrip_Mvc_Back_Url("/C/A/1/10/Articles");
                    ViewData["category_allculture"] = true;
                }
                else
                {
                    if (_dc.dm_artycle_category.Get_category(a).CategoryId == 0)
                    {
                        ViewData["Category"] = new SelectList(_dc.dm_artycle_category.Get_categories(culture), "Id", "Title", a);
                    }
                    else
                    {
                        int fjk = _dc.dm_artycle_category.Get_category(a).mt_artycle_category1.Id;
                        ViewData["Category"] = new SelectList(_dc.dm_artycle_category.Get_re_categories(fjk), "Id", "Title", a);

                    }
                    string abc = _dc.dm_artycle_category.Get_category(a).Path;
                    Mytrip_Mvc_Back_Url("/C/B/" + a + "/1/10/" + abc);
                    ViewData["category_allculture"] = _dc.dm_artycle_category.Get_category(a).AllCulture;
                }
            }
            #endregion
            #region создать новость
            if (b == "Create_news")
            {
                if (!HttpContext.User.IsInRole("artycle_editor"))
                {
                    if (!HttpContext.User.IsInRole("chief_editor"))
                    {
                        return RedirectToAction("A", "A");
                    }
                }
                Mytrip_Mvc_Menu_Active("create_news");

                if (a == 0)
                {
                    ViewData["Category"] = new SelectList(_dc.dm_artycle_category.Get_category_news(culture), "Id", "Title");
                    Mytrip_Mvc_Back_Url("/C/A/1/10/News");
                    ViewData["category_allculture"] = true;
                }
                else
                {
                    if (_dc.dm_artycle_category.Get_category(a).CategoryId == 0)
                    {
                        ViewData["Category"] = new SelectList(_dc.dm_artycle_category.Get_category_news(culture), "Id", "Title", a);
                    }
                    else
                    {
                        int fjk = _dc.dm_artycle_category.Get_category(a).mt_artycle_category1.Id;
                        ViewData["Category"] = new SelectList(_dc.dm_artycle_category.Get_re_categories(fjk), "Id", "Title", a);

                    }
                    string abc = _dc.dm_artycle_category.Get_category(a).Path;
                    Mytrip_Mvc_Back_Url("/C/B/" + a + "/1/10/" + abc);
                    ViewData["category_allculture"] = _dc.dm_artycle_category.Get_category(a).AllCulture;
                }
            }
            #endregion
            #region создать пост
            if (b == "Create_post")
            {
                if (HttpContext.User.IsInRole("blogger"))
                {
                    if (_dc.dm_artycle_category.Get_category(a).AddedBy == HttpContext.User.Identity.Name)
                    {
                        Mytrip_Mvc_Menu_Active("create_post");
                        string dfg = _dc.dm_artycle_category.Get_category(a).Path;
                        Mytrip_Mvc_Back_Url("/C/B/" + a + "/1/10/" + dfg);
                    }
                    else
                    {
                        return RedirectToAction("A", "A");
                    }
                }
                else
                {
                    return RedirectToAction("A", "A");
                }
            }
            #endregion
            return View();
        }
        /*------------------ZK - редактировать статью----------------------*/
        [Authorize(Roles = "artycle_editor, chief_editor, blogger")]
        public ActionResult ZK(int a, string b)
        {
            Mytrip_Mvc_Heder("/C/ZK/" + a + "/" + b);
            //////string culture = Session["culture"].ToString();
            #region общий контент
            ViewData["edit"] = b;
            mt_artycle x = _dc.dm_artycle.Get_artycle(a);
            ViewData.Model = x;
            #endregion
            #region редактировать статью
            if (b == "Edit_article")
            {
                if (!HttpContext.User.IsInRole("artycle_editor"))
                {
                    if (!HttpContext.User.IsInRole("chief_editor"))
                    {
                        return RedirectToAction("A", "A");
                    }
                }
                Mytrip_Mvc_Menu_Active("edit_artycle");
                if (_dc.dm_artycle_category.Get_category(x.CategoryId).CategoryId == 0)
                {
                    ViewData["Category"] = new SelectList(_dc.dm_artycle_category.Get_categories(culture), "Id", "Title", x.CategoryId);
                }
                else
                {
                    int fjk = _dc.dm_artycle_category.Get_category(x.CategoryId).mt_artycle_category1.Id;
                    ViewData["Category"] = new SelectList(_dc.dm_artycle_category.Get_re_categories(fjk), "Id", "Title", x.CategoryId);

                }
                Mytrip_Mvc_Back_Url("/C/C/" + a + "/1/10/" + x.Path);
                ViewData["category_allculture"] = _dc.dm_artycle_category.Get_category(x.CategoryId).AllCulture;
            }
            #endregion
            #region редактирование новости
            if (b == "Edit_news") {
                if (!HttpContext.User.IsInRole("artycle_editor"))
                {
                    if (!HttpContext.User.IsInRole("chief_editor"))
                    {
                        return RedirectToAction("A", "A");
                    }
                }
                Mytrip_Mvc_Menu_Active("edit_news");
                if (_dc.dm_artycle_category.Get_category(x.CategoryId).CategoryId == 0)
                {
                    ViewData["Category"] = new SelectList(_dc.dm_artycle_category.Get_category_news(culture), "Id", "Title", x.CategoryId);
                }
                else
                {
                    int fjk = _dc.dm_artycle_category.Get_category(x.CategoryId).mt_artycle_category1.Id;
                    ViewData["Category"] = new SelectList(_dc.dm_artycle_category.Get_re_categories(fjk), "Id", "Title", x.CategoryId);

                }
                Mytrip_Mvc_Back_Url("/C/C/" + a + "/" + x.Path);
            }
            #endregion
            #region редактирование поста
            if (b == "Edit_post") {
                 if (HttpContext.User.IsInRole("blogger"))
                {
                    if (_dc.dm_artycle_category.Get_category(x.CategoryId).AddedBy == HttpContext.User.Identity.Name)
                    {
                        Mytrip_Mvc_Menu_Active("edit_post");
                        Mytrip_Mvc_Back_Url("/C/C/" + a + "/1/10/" + x.Path);
                    }
                    else
                    {
                        if (HttpContext.User.IsInRole("chief_editor"))
                        {
                            if (_dc.dm_artycle_category.Get_category(x.CategoryId).AddedBy == HttpContext.User.Identity.Name)
                            {
                                Mytrip_Mvc_Menu_Active("edit_post");
                                Mytrip_Mvc_Back_Url("/C/C/" + a + "/1/10/" + x.Path);
                            }
                            else { return RedirectToAction("A", "A"); }
                        }
                        else { return RedirectToAction("A", "A"); }
                    }
                }
                 else
                 {
                     return RedirectToAction("A", "A");
                 }
            }
            #endregion
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ZK(int a, int? c, string b, string title, string description,
            string body, bool? addComment, string urlImageDescription, string urlImageBody, bool descriptionBody,
            bool registrUser, bool? approvedVotes, System.DateTime? closeDate, bool? warning, bool? allCulture)
        {
            if (allCulture == null)
                allCulture = false;
            #region проверка данных
            if (title == null)
            {
                ModelState.AddModelError("Title", Mytrip_Mvc_Language.title_valid_3_simbols);
            }
            else
            {
                if (title.Length <= 2)
                    ModelState.AddModelError("Title", Mytrip_Mvc_Language.title_valid_3_simbols);
                if (title.Length > 256)
                    ModelState.AddModelError("Title", Mytrip_Mvc_Language.title_valid_256_simbols);
                if (title.IndexOf("script") != -1)
                    ModelState.AddModelError("Title", Mytrip_Mvc_Language.title_valid_script);
                if (title.IndexOf("object") != -1)
                    ModelState.AddModelError("Title", Mytrip_Mvc_Language.title_valid_object);
                if (title.IndexOf("applet") != -1)
                    ModelState.AddModelError("Title", Mytrip_Mvc_Language.title_valid_applet);
                if (title.IndexOf("embed") != -1)
                    ModelState.AddModelError("Title", Mytrip_Mvc_Language.title_valid_embed);
                if (title.IndexOf("<%") != -1)
                    ModelState.AddModelError("Title", Mytrip_Mvc_Language.title_valid_procent);
                if (title.IndexOf("$") != -1)
                    ModelState.AddModelError("Title", Mytrip_Mvc_Language.title_valid_baks);
            }

            if (description == null)
            {
                ModelState.AddModelError("Description", Mytrip_Mvc_Language.Description_valid_3_simbols);
            }
            else
            {
                if (description.Length <= 2)
                    ModelState.AddModelError("Description", Mytrip_Mvc_Language.Description_valid_3_simbols);
                if (description.Length > 4000)
                    ModelState.AddModelError("Description", Mytrip_Mvc_Language.Description_valid_4000_simbols);
                if (description.IndexOf("script") != -1)
                    ModelState.AddModelError("Description", Mytrip_Mvc_Language.Description_valid_script);
                if (description.IndexOf("object") != -1)
                    ModelState.AddModelError("Description", Mytrip_Mvc_Language.Description_valid_object);
                if (description.IndexOf("applet") != -1)
                    ModelState.AddModelError("Description", Mytrip_Mvc_Language.Description_valid_applet);
                if (description.IndexOf("embed") != -1)
                    ModelState.AddModelError("Description", Mytrip_Mvc_Language.Description_valid_embed);
                if (description.IndexOf("<%") != -1)
                    ModelState.AddModelError("Description", Mytrip_Mvc_Language.Description_valid_procent);
                if (description.IndexOf("$") != -1)
                    ModelState.AddModelError("Description", Mytrip_Mvc_Language.Description_valid_baks);
            }
            if (body == null)
            {
                ModelState.AddModelError("Body", Mytrip_Mvc_Language.Artycles_valid_100_simbols);
            }
            else
            {
                if (body.Length <= 99)
                    ModelState.AddModelError("Body", Mytrip_Mvc_Language.Artycles_valid_100_simbols);
                if (body.IndexOf("script") != -1)
                    ModelState.AddModelError("Body", Mytrip_Mvc_Language.Artycles_valid_script);
                if (body.IndexOf("object") != -1)
                    ModelState.AddModelError("Body", Mytrip_Mvc_Language.Artycles_valid_object);
                if (body.IndexOf("applet") != -1)
                    ModelState.AddModelError("Body", Mytrip_Mvc_Language.Artycles_valid_applet);
                if (body.IndexOf("embed") != -1)
                    ModelState.AddModelError("Body", Mytrip_Mvc_Language.Artycles_valid_embed);
                if (body.IndexOf("<%") != -1)
                    ModelState.AddModelError("Body", Mytrip_Mvc_Language.Artycles_valid_procent);
                if (body.IndexOf("$") != -1)
                    ModelState.AddModelError("Body", Mytrip_Mvc_Language.Artycles_valid_baks);
            }
            if (urlImageDescription == null)
            {
                ModelState.AddModelError("UrlImageDescription", Mytrip_Mvc_Language.image_valid_null);
            }
            else
            {
                if (urlImageDescription.Length <= 1)
                    ModelState.AddModelError("UrlImageDescription", Mytrip_Mvc_Language.image_valid_null);
                if (urlImageDescription.Length > 256)
                    ModelState.AddModelError("UrlImageDescription", Mytrip_Mvc_Language.image_valid_256_simbols);
                if (urlImageDescription.IndexOf("script") != -1)
                    ModelState.AddModelError("UrlImageDescription", Mytrip_Mvc_Language.image_valid_script);
                if (urlImageDescription.IndexOf("object") != -1)
                    ModelState.AddModelError("UrlImageDescription", Mytrip_Mvc_Language.image_valid_object);
                if (urlImageDescription.IndexOf("applet") != -1)
                    ModelState.AddModelError("UrlImageDescription", Mytrip_Mvc_Language.image_valid_applet);
                if (urlImageDescription.IndexOf("embed") != -1)
                    ModelState.AddModelError("UrlImageDescription", Mytrip_Mvc_Language.image_valid_embed);
                if (urlImageDescription.IndexOf("<%") != -1)
                    ModelState.AddModelError("UrlImageDescription", Mytrip_Mvc_Language.image_valid_procent);
                if (urlImageDescription.IndexOf("$") != -1)
                    ModelState.AddModelError("UrlImageDescription", Mytrip_Mvc_Language.image_valid_baks);
            }
            if (urlImageBody == null)
            {
                ModelState.AddModelError("UrlImageBody", Mytrip_Mvc_Language.image_valid_null);
            }
            else
            {
                if (urlImageBody.Length <= 1)
                    ModelState.AddModelError("UrlImageBody", Mytrip_Mvc_Language.image_valid_null);
                if (urlImageBody.Length > 256)
                    ModelState.AddModelError("UrlImageBody", Mytrip_Mvc_Language.image_valid_256_simbols);
                if (urlImageBody.IndexOf("script") != -1)
                    ModelState.AddModelError("UrlImageBody", Mytrip_Mvc_Language.image_valid_script);
                if (urlImageBody.IndexOf("object") != -1)
                    ModelState.AddModelError("UrlImageBody", Mytrip_Mvc_Language.image_valid_object);
                if (urlImageBody.IndexOf("applet") != -1)
                    ModelState.AddModelError("UrlImageBody", Mytrip_Mvc_Language.image_valid_applet);
                if (urlImageBody.IndexOf("embed") != -1)
                    ModelState.AddModelError("UrlImageBody", Mytrip_Mvc_Language.image_valid_embed);
                if (urlImageBody.IndexOf("<%") != -1)
                    ModelState.AddModelError("UrlImageBody", Mytrip_Mvc_Language.image_valid_procent);
                if (urlImageBody.IndexOf("$") != -1)
                    ModelState.AddModelError("UrlImageBody", Mytrip_Mvc_Language.image_valid_baks);
            }
            #endregion
            #region сохранение данных
            if (ModelState.IsValid)
            {
                if (b == "Edit_article")
                {

                    _dc.dm_artycle.Update_artycle(a, (int)c, title, description,
                  body, (bool)addComment, urlImageDescription, urlImageBody, descriptionBody,
                  registrUser, (bool)approvedVotes, (System.DateTime)closeDate, (bool)allCulture);
                    
                }
                if (b == "Edit_news") {
                    _dc.dm_artycle.Update_news(a, (int)c, title, description,
                  body, (bool)addComment, urlImageDescription, urlImageBody, descriptionBody,
                  registrUser, (bool)approvedVotes, (System.DateTime)closeDate, (bool)warning, (bool)allCulture);
                    
                }
                if (b == "Edit_post") {
                    _dc.dm_artycle.Update_post(a, title, description,
                 body, urlImageDescription, urlImageBody, descriptionBody,
                 registrUser);
                   
                }
                return RedirectToAction("ZP", new { a });

            }
            #endregion
            Mytrip_Mvc_Heder("/C/ZK/" + a + "/" + b);
            string culture = Session["culture"].ToString();
            #region общий контент
            ViewData["edit"] = b;
            mt_artycle x = _dc.dm_artycle.Get_artycle(a);
            ViewData.Model = x;
            #endregion
            #region редактировать статью
            if (b == "Edit_article")
            {
                if (!HttpContext.User.IsInRole("artycle_editor"))
                {
                    if (!HttpContext.User.IsInRole("chief_editor"))
                    {
                        return RedirectToAction("A", "A");
                    }
                }
                Mytrip_Mvc_Menu_Active("edit_artycle");
                if (_dc.dm_artycle_category.Get_category(x.CategoryId).CategoryId == 0)
                {
                    ViewData["Category"] = new SelectList(_dc.dm_artycle_category.Get_categories(culture), "Id", "Title", x.CategoryId);
                }
                else
                {
                    int fjk = _dc.dm_artycle_category.Get_category(x.CategoryId).mt_artycle_category1.Id;
                    ViewData["Category"] = new SelectList(_dc.dm_artycle_category.Get_re_categories(fjk), "Id", "Title", x.CategoryId);

                }
                Mytrip_Mvc_Back_Url("/C/C/" + a + "/1/10/" + x.Path);
                ViewData["category_allculture"] = _dc.dm_artycle_category.Get_category(x.CategoryId).AllCulture;
            }
            #endregion
            #region редактирование новости
            if (b == "Edit_news")
            {
                if (!HttpContext.User.IsInRole("artycle_editor"))
                {
                    if (!HttpContext.User.IsInRole("chief_editor"))
                    {
                        return RedirectToAction("A", "A");
                    }
                }
                Mytrip_Mvc_Menu_Active("edit_news");
                if (_dc.dm_artycle_category.Get_category(x.CategoryId).CategoryId == 0)
                {
                    ViewData["Category"] = new SelectList(_dc.dm_artycle_category.Get_category_news(culture), "Id", "Title", x.CategoryId);
                }
                else
                {
                    int fjk = _dc.dm_artycle_category.Get_category(x.CategoryId).mt_artycle_category1.Id;
                    ViewData["Category"] = new SelectList(_dc.dm_artycle_category.Get_re_categories(fjk), "Id", "Title", x.CategoryId);

                }
                Mytrip_Mvc_Back_Url("/C/C/" + a + "/" + x.Path);
            }
            #endregion
            #region редактирование поста
            if (b == "Edit_post")
            {
                if (HttpContext.User.IsInRole("blogger"))
                {
                    if (_dc.dm_artycle_category.Get_category(x.CategoryId).AddedBy == HttpContext.User.Identity.Name)
                    {
                        Mytrip_Mvc_Menu_Active("edit_post");
                        Mytrip_Mvc_Back_Url("/C/C/" + a + "/1/10/" + x.Path);
                    }
                    else
                    {
                        if (HttpContext.User.IsInRole("chief_editor"))
                        {
                            if (_dc.dm_artycle_category.Get_category(x.CategoryId).AddedBy == HttpContext.User.Identity.Name)
                            {
                                Mytrip_Mvc_Menu_Active("edit_post");
                                Mytrip_Mvc_Back_Url("/C/C/" + a + "/1/10/" + x.Path);
                            }
                            else { return RedirectToAction("A", "A"); }
                        }
                        else { return RedirectToAction("A", "A"); }
                    }
                }
                else
                {
                    return RedirectToAction("A", "A");
                }
            }
            #endregion
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
            Mytrip_Mvc_Heder("/C/ZP/" + a);
            Mytrip_Mvc_Menu_Active("tegs");
            ViewData["artycle"] = _dc.dm_artycle.Get_artycles();
            ViewData["artycleid"] = a;
            ViewData["categoryid"] = _dc.dm_artycle.Get_artycle(a).CategoryId;
            ViewData["blog_bool"] = _dc.dm_artycle.Get_artycle(a).mt_artycle_category.Blog;
            ViewData["news_bool"] = _dc.dm_artycle.Get_artycle(a).mt_artycle_category.News;
            ViewData["helper_teg"] = _dc.dm_artycle.Get_tegs_all();
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ZP(int a, string title)
        {
            #region проверка данных
            if (title == null)
            {
                ModelState.AddModelError("Title", Mytrip_Mvc_Language.title_valid_3_simbols);
            }
            else
            {
                if (title.Length <= 2)
                    ModelState.AddModelError("Title", Mytrip_Mvc_Language.title_valid_3_simbols);
                if (title.Length > 256)
                    ModelState.AddModelError("Title", Mytrip_Mvc_Language.title_valid_256_simbols);
                if (title.IndexOf("script") != -1)
                    ModelState.AddModelError("Title", Mytrip_Mvc_Language.title_valid_script);
                if (title.IndexOf("object") != -1)
                    ModelState.AddModelError("Title", Mytrip_Mvc_Language.title_valid_object);
                if (title.IndexOf("applet") != -1)
                    ModelState.AddModelError("Title", Mytrip_Mvc_Language.title_valid_applet);
                if (title.IndexOf("embed") != -1)
                    ModelState.AddModelError("Title", Mytrip_Mvc_Language.title_valid_embed);
                if (title.IndexOf("<%") != -1)
                    ModelState.AddModelError("Title", Mytrip_Mvc_Language.title_valid_procent);
                if (title.IndexOf("$") != -1)
                    ModelState.AddModelError("Title", Mytrip_Mvc_Language.title_valid_baks);
            }

            #endregion
            
            if (ModelState.IsValid)
            {

                _dc.dm_artycle.Create_teg(title);
                return RedirectToAction("ZP", new { a });

            }
            Mytrip_Mvc_Heder("/C/ZP/" + a);
            Mytrip_Mvc_Menu_Active("tegs");
            ViewData["artycle"] = _dc.dm_artycle.Get_artycles();
            ViewData["artycleid"] = a;
            ViewData["categoryid"] = _dc.dm_artycle.Get_artycle(a).CategoryId;
            ViewData["blog_bool"] = _dc.dm_artycle.Get_artycle(a).mt_artycle_category.Blog;
            ViewData["news_bool"] = _dc.dm_artycle.Get_artycle(a).mt_artycle_category.News;
            ViewData["helper_teg"] = _dc.dm_artycle.Get_tegs_all();
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
       
        /*-----------------ZU - редактировать тег------------------*/
        [Authorize(Roles = "blogger, artycle_editor, chief_editor")]
        public ActionResult ZU(int a)
        {
            Mytrip_Mvc_Heder("/C/ZU/" + a);
            Mytrip_Mvc_Menu_Active("tegs");
            mt_artycle_teg x = _dc.dm_artycle.Get_teg(a);
            ViewData.Model = x;
            Mytrip_Mvc_Back_Url("/C/E/" + a + "/1/10/" + x.Path);
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ZU(int a, string title)
        {
            #region проверка данных
            if (title == null)
            {
                ModelState.AddModelError("Title", Mytrip_Mvc_Language.title_valid_3_simbols);
            }
            else
            {
                if (title.Length <= 2)
                    ModelState.AddModelError("Title", Mytrip_Mvc_Language.title_valid_3_simbols);
                if (title.Length > 256)
                    ModelState.AddModelError("Title", Mytrip_Mvc_Language.title_valid_256_simbols);
                if (title.IndexOf("script") != -1)
                    ModelState.AddModelError("Title", Mytrip_Mvc_Language.title_valid_script);
                if (title.IndexOf("object") != -1)
                    ModelState.AddModelError("Title", Mytrip_Mvc_Language.title_valid_object);
                if (title.IndexOf("applet") != -1)
                    ModelState.AddModelError("Title", Mytrip_Mvc_Language.title_valid_applet);
                if (title.IndexOf("embed") != -1)
                    ModelState.AddModelError("Title", Mytrip_Mvc_Language.title_valid_embed);
                if (title.IndexOf("<%") != -1)
                    ModelState.AddModelError("Title", Mytrip_Mvc_Language.title_valid_procent);
                if (title.IndexOf("$") != -1)
                    ModelState.AddModelError("Title", Mytrip_Mvc_Language.title_valid_baks);
            }

            #endregion

            if (ModelState.IsValid)
            {

                _dc.dm_artycle.Update_teg(a, title);
                string d = _dc.dm_artycle.Get_teg(a).Path;
                return RedirectToAction("E", new { a, b = 1, c = 10, d });
            }
            Mytrip_Mvc_Heder("/C/ZU/" + a);
            Mytrip_Mvc_Menu_Active("tegs");
            mt_artycle_teg x = _dc.dm_artycle.Get_teg(a);
            ViewData.Model = x;
            Mytrip_Mvc_Back_Url("/C/E/" + a + "/1/10/" + x.Path);
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
                    return RedirectToAction("A", new { a = 1, b = 10, c = "Articles" });
                }

            }
            if (HttpContext.User.IsInRole("chief_editor"))
            {

                _dc.dm_artycle.Delete_teg(a);
                return RedirectToAction("A", new { a = 1, b = 10, c = "Articles" });


            }
            if (HttpContext.User.IsInRole("artycle_editor"))
            {
                if (HttpContext.User.Identity.Name == def)
                {
                    _dc.dm_artycle.Delete_teg(a);
                    return RedirectToAction("A", new { a = 1, b = 10, c = "Articles" });
                }

            }
            return RedirectToAction("Error", "Home");
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
            Mytrip_Mvc_Heder("/C/ZZ/" + a);
            Mytrip_Mvc_Menu_Active("edit_comm");
            mt_artycle_comment x = _dc.dm_artycle_comment.Get_сomment(a);
            ViewData.Model = x;
            Mytrip_Mvc_Back_Url("/C/C/" + x.mt_artycle.Id + "/" + x.mt_artycle.Path);
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ZZ(int a, string body)
        {
            #region проверка данных
            if (body == null)
            {
                ModelState.AddModelError("Body", Mytrip_Mvc_Language.comment_valid_2_simbol);
            }
            else
            {
                if (body.Length < 2)
                    ModelState.AddModelError("Body", Mytrip_Mvc_Language.comment_valid_2_simbol);
                if (body.Length > 1000)
                    ModelState.AddModelError("Body", Mytrip_Mvc_Language.comment_valid_1000_simbol);
                if (body.IndexOf("script") != -1)
                    ModelState.AddModelError("Body", Mytrip_Mvc_Language.comment_valid_script);
                if (body.IndexOf("object") != -1)
                    ModelState.AddModelError("Body", Mytrip_Mvc_Language.comment_valid_object);
                if (body.IndexOf("applet") != -1)
                    ModelState.AddModelError("Body", Mytrip_Mvc_Language.comment_valid_applet);
                if (body.IndexOf("embed") != -1)
                    ModelState.AddModelError("Body", Mytrip_Mvc_Language.comment_valid_embed);
                if (body.IndexOf("<%") != -1)
                    ModelState.AddModelError("Body", Mytrip_Mvc_Language.comment_valid_procent);
                if (body.IndexOf("$") != -1)
                    ModelState.AddModelError("Body", Mytrip_Mvc_Language.comment_valid_baks);
            }

            #endregion

            if (ModelState.IsValid)
            {

                _dc.dm_artycle_comment.Update_сomment(a, body);
                int abc = _dc.dm_artycle_comment.Get_сomment(a).mt_artycle.Id;
                string dbc = _dc.dm_artycle_comment.Get_сomment(a).mt_artycle.Path;
                return RedirectToAction("C", new { a = abc, b = dbc });
            }
            Mytrip_Mvc_Heder("/C/ZZ/" + a);
            Mytrip_Mvc_Menu_Active("edit_comm");
            mt_artycle_comment x = _dc.dm_artycle_comment.Get_сomment(a);
            ViewData.Model = x;
            Mytrip_Mvc_Back_Url("/C/C/" + x.mt_artycle.Id + "/" + x.mt_artycle.Path);
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

                         select new SyndicationItem(x.Title, x.Description, new Uri("http://" + _dc.dm_model.Get_model().DomainName + "/C/C/" + x.Id.ToString() + "/" + x.Path)
                )).ToList();
            return new RssFeedResult(rss);
        }
        /*----------------Rss_B - rss поток всех статей--------------------*/
        public ActionResult Rss_B()
        {
            //////string culture = Session["culture"].ToString();
            SyndicationFeed rss = new SyndicationFeed(Mytrip_Mvc_Language.menu_artycles, "News feed",
                new Uri("http://" + _dc.dm_model.Get_model().DomainName + "/C/A/1/10/Articles"));
            rss.Items = (from x in this._dc.dm_artycle.Get_artycles_date_true(culture)

                         select new SyndicationItem(x.Title, x.Description, new Uri("http://" + _dc.dm_model.Get_model().DomainName + "/C/C/" + x.Id.ToString() + "/" + x.Path)
                )).ToList();
            return new RssFeedResult(rss);
        }
        /*----------------Rss_C - rss поток всех новостей------------------*/
        public ActionResult Rss_C()
        {
            //////string culture = Session["culture"].ToString();
            SyndicationFeed rss = new SyndicationFeed(Mytrip_Mvc_Language.menu_news, "News feed",
                new Uri("http://" + _dc.dm_model.Get_model().DomainName + "/C/A/1/10/News"));
            rss.Items = (from x in this._dc.dm_artycle.Get_news_date_true(culture)

                         select new SyndicationItem(x.Title, x.Description, new Uri("http://" + _dc.dm_model.Get_model().DomainName + "/C/C/" + x.Id.ToString() + "/" + x.Path)
                )).ToList();
            return new RssFeedResult(rss);
        }
        /*-----------------Rss_D - rss поток всех постов-------------------*/
        public ActionResult Rss_D()
        {
            ///////string culture = Session["culture"].ToString();
            SyndicationFeed rss = new SyndicationFeed(Mytrip_Mvc_Language.menu_blogs, "News feed",
                new Uri("http://" + _dc.dm_model.Get_model().DomainName + "/C/A/1/10/Blogs"));
            rss.Items = (from x in this._dc.dm_artycle.Get_posts(culture)

                         select new SyndicationItem(x.Title, x.Description, new Uri("http://" + _dc.dm_model.Get_model().DomainName + "/C/C/" + x.Id.ToString()+"/"+x.Path)
                )).ToList();
            return new RssFeedResult(rss);
        }
        /*---------------Rss_E - rss поток комментариев из статьи-------------------*/
        public ActionResult Rss_E(int a)
        {

            SyndicationFeed rss = new SyndicationFeed(_dc.dm_artycle.Get_artycle(a).Title, "News feed",
                new Uri("http://" + _dc.dm_model.Get_model().DomainName + "/C/C/" + a.ToString() + "/" + _dc.dm_artycle.Get_artycle(a).Path));
            rss.Items = (from x in this._dc.dm_artycle_comment.Get_сomments_from_artycle_end(a)

                         select new SyndicationItem(x.AddedBy, x.Body, new Uri("http://" + _dc.dm_model.Get_model().DomainName + "/C/C/" + x.ArtycleId.ToString()+"/"+x.mt_artycle.Path)
                )).ToList();
            return new RssFeedResult(rss);
        }
        /*-----------------------------------------------------*/
    }
}
