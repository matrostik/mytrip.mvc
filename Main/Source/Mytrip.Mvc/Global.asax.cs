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
            string name = "Mytrip";
            int namber = 0;
            foreach (var item in MytripRewrite.GetMytripRewrite())
            {
                routes.MapRoute(name+namber,item.Key,item.Value);
                namber++;
            }
            routes.MapRoute(
                "Default",
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