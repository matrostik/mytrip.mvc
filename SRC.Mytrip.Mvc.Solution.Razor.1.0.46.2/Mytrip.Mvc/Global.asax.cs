/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about Mytrip.Mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System.Web.Mvc;
using System.Web.Routing;
using Mytrip.Mvc.Models;
using Mytrip.Mvc.Settings;

namespace Mytrip.Mvc
{
    /// <summary>
    /// 
    /// </summary>
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filters"></param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new LanguageAttribute());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="routes"></param>
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
        /// 
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            foreach (var x in CustomNamespace.Namespace())
            {
                Microsoft.WebPages.Compilation.CodeGeneratorSettings.AddGlobalImport(x.Value);
            }
        }
    }
}