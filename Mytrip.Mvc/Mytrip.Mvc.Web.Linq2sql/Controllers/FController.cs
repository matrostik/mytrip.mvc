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
    public class FController : Controller
    {
        /*  ����������� � ������  */

        /*-----------------------------------------------------*/
        IRepository _dc = new IRepository(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
        /*-----------------------------------------------------*/

        /*  �������� �������  */

        /*------------------����-------------------*/
        [Authorize(Roles = "admin")]
        public ActionResult F()
        {            

            #region ���� � ����
            ViewData["logon_url"] = "/F/F";
            ViewData["language_url"] = "/F/F";
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool �������� captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion       
            #region ��������� �����
            ViewData["menu_url"] = "file";
            //�������� ������
            ViewData["lang_panel"] = _dc.dm_model.Get_model().Language_approved;
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
            

            #region �������� �������
        
            ViewData["folder"] = _dc.dm_file.Get_folder_site();
            ViewData["file"] = _dc.dm_file.Get_file_site();
           
            #endregion

            return View();
        }
        [HttpPost]
        public ActionResult F(HttpPostedFileBase a)
        {
            if(a==null)
                ModelState.AddModelError("a", "���� �� ��������.");
            if (ModelState.IsValid)
            {

                _dc.dm_file.Upload_file(a);
                return RedirectToAction("F");
            }
            #region ���� � ����
            ViewData["logon_url"] = "/F/F";
            ViewData["language_url"] = "/F/F";
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool �������� captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion
            #region ��������� �����
            ViewData["menu_url"] = "file";
            //�������� ������
            ViewData["lang_panel"] = _dc.dm_model.Get_model().Language_approved;
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



            #region �������� �������
          
            ViewData["folder"] = _dc.dm_file.Get_folder_site();
            ViewData["file"] = _dc.dm_file.Get_file_site();

            #endregion

            return View();
        }

        /*-----------------����/../..-------------------------*/
        [Authorize(Roles = "admin")]
        public ActionResult B(string a)
        {

            #region ���� � ����
            ViewData["logon_url"] = "/F/B"+a;
            ViewData["language_url"] = "/F/B" + a;
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool �������� captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion
            #region ��������� �����
            ViewData["menu_url"] = "file";
            //�������� ������
            ViewData["lang_panel"] = _dc.dm_model.Get_model().Language_approved;
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



            #region �������� �������
            
            ViewData["folder"] = _dc.dm_file.Get_folder_folder(a);
            ViewData["file"] = _dc.dm_file.Get_file_folder(a);
            ViewData["dir"] = a;
            #endregion

            return View();
        }
        [HttpPost]
        public ActionResult B(string a, HttpPostedFileBase b)
        {
            if (b == null)
                ModelState.AddModelError("b", "���� �� ��������.");
            if (ModelState.IsValid)
            {

                _dc.dm_file.Upload_file2(a,b);
                return RedirectToAction("B", new { a });
            }
            #region ���� � ����
            ViewData["logon_url"] = "/F/B" + a;
            ViewData["language_url"] = "/F/B" + a;
            ViewData["model_domain"] = _dc.dm_model.Get_model().DomainName;
            ViewData["artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true();
            ViewData["news_category_menu"] = _dc.dm_artycle_category.Get_categories_news_addmenu_true();
            //bool �������� captcha
            ViewData["captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            #endregion

            #region ��������� �����
            ViewData["menu_url"] = "file";
            //�������� ������
            ViewData["lang_panel"] = _dc.dm_model.Get_model().Language_approved;
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


            #region �������� �������

            ViewData["folder"] = _dc.dm_file.Get_folder_folder(a);
            ViewData["file"] = _dc.dm_file.Get_file_folder(a);
            ViewData["dir"] = a;
            #endregion

            return View();
        }

        /*-----------------������� ���� �� ����/../..------------------*/
        [Authorize(Roles = "admin")]
        public ActionResult C(string a)
        {
            _dc.dm_file.Delete_file(a);
            int d = a.LastIndexOf("()");
            string df = a.Remove(d);
            return RedirectToAction("B", new{a=df });
        }
        /*-----------------������� ���� �� ����------------------*/
        [Authorize(Roles = "admin")]
        public ActionResult D(string a)
        {
            _dc.dm_file.Delete_file(a);            
            return RedirectToAction("F");
        }
        /*-----------------������� �����------------------*/
        [Authorize(Roles = "admin")]
        public ActionResult E(string a)
        {
            a = "()" + a;
            _dc.dm_file.Create_folder(a);
            return RedirectToAction("F");
        }
        /*-----------------������� �����------------------*/
        [Authorize(Roles = "admin")]
        public ActionResult G(string a)
        {           
            _dc.dm_file.Delete_folder(a);
            return RedirectToAction("F");
        }
        /*-----------------������� ����� � �a��/...------------------*/
        [Authorize(Roles = "admin")]
        public ActionResult H(string a,string b)
        {
            string c = a +"()" + b;
            _dc.dm_file.Create_folder(c);
            return RedirectToAction("B", new {a});
        }
        /*-----------------������� ����� �� �a��/...------------------*/
        [Authorize(Roles = "admin")]
        public ActionResult j(string a)
        {
            _dc.dm_file.Delete_folder(a);
            int d = a.LastIndexOf("()");
            string df = a.Remove(d);
            return RedirectToAction("B", new { a = df });
        }
    }
}
