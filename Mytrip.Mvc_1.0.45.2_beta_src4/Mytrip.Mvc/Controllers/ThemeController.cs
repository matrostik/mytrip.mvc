//************************************************************ 
// Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich
// To learn more about Mytrip.Mvc.Entyty visit 
// http://starterkitmytripmvc.codeplex.com/
// mytripmvc@gmail.com
// license: Microsoft Public License (Ms-PL) 
// ***********************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;

namespace Mytrip.Mvc.Controllers
{
    /// <summary>
    /// Theme Controller
    /// </summary>
    [HandleError]
    public class ThemeController : Controller
    { 
        /// <summary>
        /// URL: /Theme/Index/theme/path
        /// </summary>
        /// <param name="id">Theme</param>
        /// <param name="id2">Path</param>
        /// <returns>ActionResult</returns>
        public ActionResult Index(string id, string id2)
        {
            Session["theme"] = id;
            HttpCookie cookie = new HttpCookie("myTripTheme", id);
            cookie.Expires = DateTime.Now.AddYears(1);
            Response.Cookies.Add(cookie);
            id2 = id2.Replace("(x)", "/");
            return Redirect(id2);
        }
    }
}
