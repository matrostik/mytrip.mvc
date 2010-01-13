using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Mytrip.Mvc.Web.Linq2sql
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            #region {resource}.axd/{*pathInfo}
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            #endregion

            #region {controller}/{action}/{a}/{b}/{c}/{d}
            routes.MapRoute(
              "abcd",                                              // Route name
              "{controller}/{action}/{a}/{b}/{c}/{d}",                           // URL with parameters
              new { controller = "A", action = "A", a = "", b = "", c = "", d = "" });// Parameter defaults
            #endregion

            #region {controller}/{action}/{a}/{b}/{c}
            routes.MapRoute(
              "abc",                                              // Route name
              "{controller}/{action}/{a}/{b}/{c}",                           // URL with parameters
              new { controller = "A", action = "A", a = "", b = "", c = "" });// Parameter defaults
            #endregion

            #region {controller}/{action}/{a}/{b}
            routes.MapRoute(
               "ab",                                              // Route name
               "{controller}/{action}/{a}/{b}",                           // URL with parameters
               new { controller = "A", action = "A", a = "", b = "" });// Parameter defaults
            #endregion

            #region {controller}/{action}/{a}
            routes.MapRoute(
               "Default",                                              // Route name
               "{controller}/{action}/{a}",                           // URL with parameters
               new { controller = "A", action = "A", a = "" }  // Parameter defaults
           );
            #endregion

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);         
        }
    }
}