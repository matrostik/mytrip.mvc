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

namespace Mytrip.Core.Controllers
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
        /// <param name="theme">Theme</param>
        /// <param name="path">Path</param>
        /// <returns>ActionResult</returns>
        public ActionResult Index(string theme, string path)
        {
            Session["theme"] = theme;
            HttpCookie cookie = new HttpCookie("myTripTheme", theme);
            cookie.Expires = DateTime.Now.AddYears(1);
            Response.Cookies.Add(cookie);
            path = path.Replace("(x)", "/");
            return Redirect(path);
        }
    }
}
