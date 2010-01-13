/*   Mytrip.Mvc.Web.Linq2sql   Copyright �  2009 ������ ���� �����������   */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Mytrip.Mvc.Model.Linq2sql;
using System.Configuration;
using Mytrip.Mvc.Web.Linq2sql.Models;

namespace Mytrip.Mvc.Web.Linq2sql.Controllers
{
    [HandleError]
    [Localization]
    public class DController : Controller
    {
        /*  ����������� � ������  */

        /*-----------------------------------------------------*/
        IRepository _dc = new IRepository(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
        /*-----------------------------------------------------*/

        /*  �������� �������  */

        /*------------A - ���������� ������ ������ -------------------*/
        public ActionResult A(int a, int b, int c, string d)
        {
            #region ���� � ����
            ViewData["logon_url"] = "/D/A/" + a + "/" + b + "/" + c + "/" + d;
            ViewData["language_url"] = "/D/A/" + a + "/" + b + "/" + c + "/" + d;
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool �������� captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion

            #region ��������� �����
            ViewData["menu_url"] = "search";
            //�������� ������
            ViewData["lang_panel"] = _dc.dm_model.Get_model().Language_approved;
            //���������� ��������� ��� ��������� �����
            ViewData["model_comment"] = _dc.dm_model.Get_model().CountComment;
            //������������� �� email
            ViewData["email_approved"] = _dc.dm_model.Get_model().Email_approved;
            //bool ��������� ����� ��� ���
            ViewData["model_blog"] = _dc.dm_model.Get_model().Blog;
            //bool ��������� ������� ��� ���
            ViewData["model_news"] = _dc.dm_model.Get_model().News;
            //bool ��������� ������ ��� ���
            ViewData["model_artycle"] = _dc.dm_model.Get_model().Artycle;
            if (Session["culture"] == null)
            {
                Session["culture"] = _dc.dm_model.Get_model().mt_language.param;
            }
            ViewData["language"] = new SelectList(_dc.dm_model.Get_languages(), "param", "name");
            #endregion
            string culture = Session["culture"].ToString();
            #region ������ �������
            //���� ������������
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                //��� ����������� ������������
                ViewData["comment_user"] = _dc.dm_artycle_comment.Get_�omments_start_user();
                //���������� ������ ������������
                ViewData["blog_user_count"] = _dc.dm_artycle_category.Get_blogs_user(culture).Count();
                //��� ����� ������������
                ViewData["blog_user"] = _dc.dm_artycle_category.Get_blogs_user(culture);
            }
            //������� ��������
            ViewData["news_category"] = _dc.dm_artycle_category.Get_category_news(culture);
            //������� ������
            ViewData["artycle_category"] = _dc.dm_artycle_category.Get_categories(culture);
            //�����
            ViewData["blog"] = _dc.dm_artycle_category.Get_blogs(culture);
            //����
            ViewData["teg"] = _dc.dm_artycle.Get_tegs(culture);
            #endregion

            #region �������� �������
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

            #region �������� ��������
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