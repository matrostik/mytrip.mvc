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
using Mytrip.Core.Models;

namespace Mytrip.Core.Controllers
{
    /// <summary>
    /// Language Controller
    /// </summary>
    [HandleError]
    [Localization]
    public class LanguageController:Controller
    {
        /// <summary>
        /// URL: /Language/Index/culture/path
        /// </summary>
        /// <param name="culture">culture</param>
        /// <param name="path">path</param>
        /// <returns>ActionResult</returns>
        public ActionResult Index(string culture,string path)
        {
            Session["culture"] = culture;
            HttpCookie cookie = new HttpCookie("myTripCulture", culture);
            cookie.Expires = DateTime.Now.AddYears(1);
            Response.Cookies.Add(cookie);
            path = path.Replace("(x)", "/");
            return Redirect(path);
        }
    }
}
