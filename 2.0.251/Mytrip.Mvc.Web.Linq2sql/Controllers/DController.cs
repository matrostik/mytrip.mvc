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
using Mytrip.Mvc.Language;

namespace Mytrip.Mvc.Web.Linq2sql.Controllers
{
    [HandleError]
    [Localization]
    public class DController : HelperController
    {
        /*  ОСНОВНОЙ КОНТЕНТ  */

        /*------------A - результаты поиска статей -------------------*/
        public ActionResult A(int a, int b, int c, string d)
        {
            Mytrip_Mvc_Heder("/D/A/" + a + "/" + b + "/" + c + "/" + d);
            Mytrip_Mvc_Right_Column();
            Mytrip_Mvc_Move_Content();
            Mytrip_Mvc_Menu_Active("search");
            //////string culture = Session["culture"].ToString();
            #region основной контент
            if (_dc.dm_model.Get_model().Blog == false)
            {
                if (_dc.dm_model.Get_model().News == false)
                {
                    ViewData["artycle"] = _dc.dm_search.Search_artycles_only_page(b, c, d,culture);
                    ViewData["content_count"] = _dc.dm_search.Search_artycles_only(d,culture).Count();
                }
                else
                {
                    ViewData["artycle"] = _dc.dm_search.Search_artycles_news_page(b, c, d,culture);
                    ViewData["content_count"] = _dc.dm_search.Search_artycles_news(d,culture).Count();
                }
            }
            else
            {
                if (_dc.dm_model.Get_model().News == false)
                {
                    ViewData["artycle"] = _dc.dm_search.Search_artycles_blog_page(b, c, d,culture);
                    ViewData["content_count"] = _dc.dm_search.Search_artycles_blog(d,culture).Count();
                }
                else
                {
                    ViewData["artycle"] = _dc.dm_search.Search_artycles_page(b, c, d,culture);
                    ViewData["content_count"] = _dc.dm_search.Search_artycles(d, culture).Count();
                }
            }
            Mytrip_Mvc_Category_Status(a);
            ViewData["search"] = d;
            #endregion
            Mytrip_Mvc_Pager_Site("/D/A/", c, a, b, d);
            return View();
        }
        [HttpPost]
        public ActionResult A(string search)
        {
            if (search == null)
                search = Mytrip_Mvc_Language.error;
            if(search.Length < 3)
                search = Mytrip_Mvc_Language.error;
            return RedirectToAction("A", "D", new { a = 0, b = 1, c = 10, d = search });
        }
        /*-----------------------------------------------------*/
    }
}