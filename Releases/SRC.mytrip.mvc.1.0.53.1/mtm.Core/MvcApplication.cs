using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using mtm.Core.Settings;
using mtm.Core.Models;

namespace mtm.Core
{
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
            
            string name = "mtm";
            int namber = 0;
            foreach (var item in MytripRewrite.GetMytripRewrite())
            {
                routes.MapRoute(name + namber, item.Key, item.Value);
                namber++;
            }
            routes.MapRoute(
                "Default10",
                "{controller}/{action}/{id}/{id2}/{id3}/{id4}/{id5}/{id6}/{id7}/{id8}/{id9}/{id10}",
                new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional,
                    id2 = UrlParameter.Optional,
                    id3 = UrlParameter.Optional,
                    id4 = UrlParameter.Optional,
                    id5 = UrlParameter.Optional,
                    id6 = UrlParameter.Optional,
                    id7 = UrlParameter.Optional,
                    id8 = UrlParameter.Optional,
                    id9 = UrlParameter.Optional,
                    id10 = UrlParameter.Optional
                });
            routes.MapRoute(
                "Default9",
                "{controller}/{action}/{id}/{id2}/{id3}/{id4}/{id5}/{id6}/{id7}/{id8}/{id9}",
                new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional,
                    id2 = UrlParameter.Optional,
                    id3 = UrlParameter.Optional,
                    id4 = UrlParameter.Optional,
                    id5 = UrlParameter.Optional,
                    id6 = UrlParameter.Optional,
                    id7 = UrlParameter.Optional,
                    id8 = UrlParameter.Optional,
                    id9 = UrlParameter.Optional
                });
            routes.MapRoute(
                "Default8",
                "{controller}/{action}/{id}/{id2}/{id3}/{id4}/{id5}/{id6}/{id7}/{id8}",
                new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional,
                    id2 = UrlParameter.Optional,
                    id3 = UrlParameter.Optional,
                    id4 = UrlParameter.Optional,
                    id5 = UrlParameter.Optional,
                    id6 = UrlParameter.Optional,
                    id7 = UrlParameter.Optional,
                    id8 = UrlParameter.Optional
                });
            routes.MapRoute(
                "Default7",
                "{controller}/{action}/{id}/{id2}/{id3}/{id4}/{id5}/{id6}/{id7}",
                new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional,
                    id2 = UrlParameter.Optional,
                    id3 = UrlParameter.Optional,
                    id4 = UrlParameter.Optional,
                    id5 = UrlParameter.Optional,
                    id6 = UrlParameter.Optional,
                    id7 = UrlParameter.Optional
                });
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
        }
    }
}