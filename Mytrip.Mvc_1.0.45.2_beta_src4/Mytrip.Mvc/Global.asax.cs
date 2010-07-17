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
using System.Web.Routing;
using System.Data.Common;

namespace Mytrip.Mvc
{
    /// <summary>
    /// Global Mvc Application
    /// </summary>
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Register Routes
        /// </summary>
        /// <param name="routes">Route Collection</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                "Default6",
                "{controller}/{action}/{id}/{id2}/{id3}/{id4}/{id5}/{id6}",
                new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional,
                    id2 = UrlParameter.Optional,
                    id3 = UrlParameter.Optional,
                    id4 = UrlParameter.Optional,
                    id5 = UrlParameter.Optional,
                    id6 = UrlParameter.Optional
                });
            routes.MapRoute(
                "Default5",
                "{controller}/{action}/{id}/{id2}/{id3}/{id4}/{id5}",
                new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional,
                    id2 = UrlParameter.Optional,
                    id3 = UrlParameter.Optional,
                    id4 = UrlParameter.Optional,
                    id5 = UrlParameter.Optional
                });
            routes.MapRoute(
                "Default4",
                "{controller}/{action}/{id}/{id2}/{id3}/{id4}",
                new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional,
                    id2 = UrlParameter.Optional,
                    id3 = UrlParameter.Optional,
                    id4 = UrlParameter.Optional
                });
            routes.MapRoute(
                "Default3",
                "{controller}/{action}/{id}/{id2}/{id3}",
                new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional,
                    id2 = UrlParameter.Optional,
                    id3 = UrlParameter.Optional
                });
            routes.MapRoute(
                "Default2",
                "{controller}/{action}/{id}/{id2}",
                new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional,
                    id2 = UrlParameter.Optional
                });
            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new 
                { 
                    controller = "Home", 
                    action = "Index", 
                    id = UrlParameter.Optional
                });
        }
        /// <summary>
        /// Application Start
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);
        }
    }
}