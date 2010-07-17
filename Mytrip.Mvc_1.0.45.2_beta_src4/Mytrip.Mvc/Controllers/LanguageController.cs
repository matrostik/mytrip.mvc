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
using Mytrip.Mvc.Models;

namespace Mytrip.Mvc.Controllers
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
        /// <param name="id">culture</param>
        /// <param name="id2">path</param>
        /// <returns>ActionResult</returns>
        public ActionResult Index(string id,string id2)
        {
            Session["culture"] = id;
            HttpCookie cookie = new HttpCookie("myTripCulture", id);
            cookie.Expires = DateTime.Now.AddYears(1);
            Response.Cookies.Add(cookie);
            id2 = id2.Replace("(x)", "/");
            return Redirect(id2);
        }
    }
}
