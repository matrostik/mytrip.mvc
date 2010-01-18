/*   Mytrip.Mvc.Web.Linq2sql   Copyright ©  2009 Стюхин Олег Анатольевич   */
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
    public class FController : HelperController
    {
        /*  ОСНОВНОЙ КОНТЕНТ  */

        /*------------------Сайт-------------------*/
        [Authorize(Roles = "admin")]
        public ActionResult F()
        {
            Mytrip_Mvc_Heder("/F/F");
            Mytrip_Mvc_Menu_Active("file");
            #region основной контент
            ViewData["folder"] = _dc.dm_file.Get_folder_site();
            ViewData["file"] = _dc.dm_file.Get_file_site();           
            #endregion

            return View();
        }
        [HttpPost]
        public ActionResult F(HttpPostedFileBase a)
        {
            if(a==null)
                ModelState.AddModelError("a", "Файл не загружен.");
            if (ModelState.IsValid)
            {

                _dc.dm_file.Upload_file(a);
                return RedirectToAction("F");
            }
            Mytrip_Mvc_Heder("/F/F");
            Mytrip_Mvc_Menu_Active("file");
            #region основной контент
            ViewData["folder"] = _dc.dm_file.Get_folder_site();
            ViewData["file"] = _dc.dm_file.Get_file_site();
            #endregion
            return View();
        }

        /*-----------------Сайт/../..-------------------------*/
        [Authorize(Roles = "admin")]
        public ActionResult B(string a)
        {
            Mytrip_Mvc_Heder("/F/B" + a);
            Mytrip_Mvc_Menu_Active("file");
            #region основной контент
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
                ModelState.AddModelError("b", "Файл не загружен.");
            if (ModelState.IsValid)
            {

                _dc.dm_file.Upload_file2(a,b);
                return RedirectToAction("B", new { a });
            }
            Mytrip_Mvc_Heder("/F/B" + a);
            Mytrip_Mvc_Menu_Active("file");
            #region основной контент
            ViewData["folder"] = _dc.dm_file.Get_folder_folder(a);
            ViewData["file"] = _dc.dm_file.Get_file_folder(a);
            ViewData["dir"] = a;
            #endregion
            return View();
        }

        /*-----------------Удалить файл из Сайт/../..------------------*/
        [Authorize(Roles = "admin")]
        public ActionResult C(string a)
        {
            _dc.dm_file.Delete_file(a);
            int d = a.LastIndexOf("()");
            string df = a.Remove(d);
            return RedirectToAction("B", new{a=df });
        }
        /*-----------------Удалить файл из Сайт------------------*/
        [Authorize(Roles = "admin")]
        public ActionResult D(string a)
        {
            _dc.dm_file.Delete_file(a);            
            return RedirectToAction("F");
        }
        /*-----------------Создать папку------------------*/
        [Authorize(Roles = "admin")]
        public ActionResult E(string a)
        {
            a = "()" + a;
            _dc.dm_file.Create_folder(a);
            return RedirectToAction("F");
        }
        /*-----------------Удалить папку------------------*/
        [Authorize(Roles = "admin")]
        public ActionResult G(string a)
        {           
            _dc.dm_file.Delete_folder(a);
            return RedirectToAction("F");
        }
        /*-----------------Создать папку в Сaйт/...------------------*/
        [Authorize(Roles = "admin")]
        public ActionResult H(string a,string b)
        {
            string c = a +"()" + b;
            _dc.dm_file.Create_folder(c);
            return RedirectToAction("B", new {a});
        }
        /*-----------------Удалить папку из Сaйт/...------------------*/
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
