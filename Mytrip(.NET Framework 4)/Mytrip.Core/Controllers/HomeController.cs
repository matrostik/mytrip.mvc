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
using System.Web;
using System.Web.Mvc;
using Mytrip.Core.Models;
using System.Configuration;
using System.Web.Configuration;
using Mytrip.Core.Repository.XmlUsers;

namespace Mytrip.Core.Controllers
{
    /// <summary>
    /// Home Controller
    /// </summary>
    [HandleError]
    [Localization]
    [SqlSetting]
    public class HomeController : Controller
    {
        /// <summary>
        /// URL: /Home/Index
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult Index()
        {
            ViewData["Message"] = "Welcome to Mytrip.Mvc.Entity!";
            return View();
        }        
        /// <summary>
        /// URL: /Home/About
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult About()
        {
            
            return View();
        }
    }
}
