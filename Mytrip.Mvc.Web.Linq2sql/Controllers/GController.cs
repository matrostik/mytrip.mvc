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
    public class GController : Controller
    {
        /*  ����������� � ������  */

        /*-----------------------------------------------------*/
        IRepository _dc = new IRepository(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
        /*-----------------------------------------------------*/

        /*  �������� �������  */

       
        /*-----------------����/../..-------------------------*/
        [Authorize(Roles = "blogger, artycle_editor, chief_editor")]
        public ActionResult B(string a)
        {
            string dfg = HttpContext.User.Identity.Name;
            int dgh = a.IndexOf(dfg);
            if (dgh == -1)
            {
                return RedirectToAction("A", "A");
            }
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
            string dfg = HttpContext.User.Identity.Name;
            int dgh = a.IndexOf(dfg);
            if (dgh == -1)
            {
                return RedirectToAction("A", "A");
            }
            if (b == null)
                ModelState.AddModelError("b", "���� �� ��������.");
            if (ModelState.IsValid)
            {

                _dc.dm_file.Upload_file2(a,b);
                return RedirectToAction("B", new { a });
            }
          
            #region �������� �������

            ViewData["folder"] = _dc.dm_file.Get_folder_folder(a);
            ViewData["file"] = _dc.dm_file.Get_file_folder(a);
            ViewData["dir"] = a;
            #endregion

            return View();
        }

        /*-----------------������� ���� �� ����/../..------------------*/
        [Authorize(Roles = "blogger, artycle_editor, chief_editor")]
        public ActionResult C(string a)
        {
            string dfg = HttpContext.User.Identity.Name;
            int dgh = a.IndexOf(dfg);
            if (dgh == -1)
            {
                return RedirectToAction("A", "A");
            }
            _dc.dm_file.Delete_file(a);
            int d = a.LastIndexOf("()");
            string df = a.Remove(d);
            return RedirectToAction("B", new{a=df });
        }
      
        /*-----------------������� ����� � �a��/...------------------*/
        [Authorize(Roles = "blogger, artycle_editor, chief_editor")]
        public ActionResult H(string a,string b)
        {
            string dfg = HttpContext.User.Identity.Name;
            int dgh = a.IndexOf(dfg);
            if (dgh == -1)
            {
                return RedirectToAction("A", "A");
            }
            string c = a +"()" + b;
            _dc.dm_file.Create_folder(c);
            return RedirectToAction("B", new {a});
        }
        /*-----------------������� ����� �� �a��/...------------------*/
        [Authorize(Roles = "blogger, artycle_editor, chief_editor")]
        public ActionResult j(string a)
        {
            string dfg = HttpContext.User.Identity.Name;
            int dgh = a.IndexOf(dfg);
            if (dgh == -1)
            {
                return RedirectToAction("A", "A");
            }
            _dc.dm_file.Delete_folder(a);
            int d = a.LastIndexOf("()");
            string df = a.Remove(d);
            return RedirectToAction("B", new { a = df });
        }
    }
}
