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
            #region USERS
            routes.MapRoute(
               "UsersDeleteRole",
               "Users/DeleteRole/{roleName}",
               new
               {
                   controller = "Users",
                   action = "DeleteRole",
                   roleName = UrlParameter.Optional
               });
            routes.MapRoute(
               "UsersIsApproved",
               "Users/IsApproved/{userName}",
               new
               {
                   controller = "Users",
                   action = "IsApproved",
                   userName = UrlParameter.Optional
               });
            routes.MapRoute(
                "UsersDelete",
                "Users/Delete/{userName}",
                new
                {
                    controller = "Users",
                    action = "Delete",
                    userName = UrlParameter.Optional
                });
            routes.MapRoute(
                "UsersDetails",
                "Users/Details/{userName}",
                new
                {
                    controller = "Users",
                    action = "Details",
                    userName = UrlParameter.Optional
                });
            routes.MapRoute(
                "UsersIsUserInRole",
                "Users/IsUserInRole/{userName}/{roleName}",
                new
                {
                    controller = "Users",
                    action = "IsUserInRole",
                    userName = UrlParameter.Optional,
                    roleName = UrlParameter.Optional
                });
            routes.MapRoute(
                "Users",
                "Users/{action}/{pageIndex}/{pageSize}/{sorting}",
                new
                {
                    controller = "Users",
                    action = "Index",
                    pageIndex = UrlParameter.Optional,
                    pageSize = UrlParameter.Optional,
                    sorting = UrlParameter.Optional
                });
            #endregion
            #region CAPTCHA
            routes.MapRoute(
                "Captcha",
                "Captcha/{action}/{ImageWidth}/{ImageHeight}/{fontFamily}",
                new
                {
                    controller = "Captcha",
                    action = "Index",
                    ImageWidth = UrlParameter.Optional,
                    ImageHeight = UrlParameter.Optional,
                    fontFamily = UrlParameter.Optional
                });
            #endregion
            #region FILE
            routes.MapRoute(
                "FileDetais",
                "File/{action}/{directory}/{param}",
                new
                {
                    controller = "File",
                    action = "Index",
                    directory = UrlParameter.Optional,
                    param = UrlParameter.Optional
                });
            routes.MapRoute(
                "File",
                "File/{action}/{directory}",
                new
                {
                    controller = "File",
                    action = "Index",
                    directory = UrlParameter.Optional
                });
            #endregion
            #region ArticleFile
            routes.MapRoute(
                "ArticleFileDetais2",
                "ArticleFile/{action}/{directory}/{param}/{param2}",
                new
                {
                    controller = "ArticleFile",
                    action = "Index",
                    directory = UrlParameter.Optional,
                    param = UrlParameter.Optional,
                    param2 = UrlParameter.Optional
                });
            routes.MapRoute(
                "ArticleFileDetais",
                "ArticleFile/{action}/{directory}/{param}",
                new
                {
                    controller = "ArticleFile",
                    action = "Index",
                    directory = UrlParameter.Optional,
                    param = UrlParameter.Optional
                });
            routes.MapRoute(
                "ArticleFile",
                "ArticleFile/{action}/{directory}",
                new
                {
                    controller = "ArticleFile",
                    action = "Index",
                    directory = UrlParameter.Optional
                });
            #endregion
            #region THEME
            routes.MapRoute(
                "Theme",
                "Theme/{action}/{theme}/{path}",
                new 
                { 
                    controller = "Theme", 
                    action = "Index", 
                    theme = UrlParameter.Optional, 
                    path = UrlParameter.Optional 
                });
            #endregion
            #region LANGUAGE
            routes.MapRoute(
                "Language",
                "Language/{action}/{culture}/{path}",
                new 
                { 
                    controller = "Language", 
                    action = "Index", 
                    culture = UrlParameter.Optional, 
                    path = UrlParameter.Optional 
                });
            #endregion
            #region ARTICLE
            routes.MapRoute(
                 "ArticleIndex",
                 "Article/Index/{pageIndex}/{pageSize}/{id}/{Path}",
                 new
                 {
                     controller = "Article",
                     action = "Index",
                     pageIndex = UrlParameter.Optional,
                     pageSize = UrlParameter.Optional,
                     id = UrlParameter.Optional,
                     Path = UrlParameter.Optional
                 });
            routes.MapRoute(
               "ArticleProfile",
               "Article/Profile/{username}/",
               new
               {
                   controller = "Article",
                   action = "Profile",
                   username = UrlParameter.Optional,
               });
            routes.MapRoute(
                 "ArticleActions",
                 "Article/{action}/{id}/{Path}/{url}",
                 new
                 {
                     controller = "Article",
                     action = "View",
                     id = UrlParameter.Optional,
                     Path = UrlParameter.Optional,
                     url = UrlParameter.Optional
                 });

            #endregion
            #region ARTICLE ARCHIVE
            routes.MapRoute(
                "Details",
                "ArticleArchive/Details/{path}/{culture}/",
                new
                {
                    controller = "ArticleArchive",
                    action = "Details",
                    path = UrlParameter.Optional,
                    culture = UrlParameter.Optional
                });
            routes.MapRoute(
                 "ArticleArchive",
                 "ArticleArchive/{action}/{count}/",
                 new
                 {
                     controller = "ArticleArchive",
                     action = "Index",
                     count = UrlParameter.Optional
                 });

            routes.MapRoute(
                 "ArticleArchive1",
                 "ArticleArchive/{action}/",
                 new
                 {
                     controller = "ArticleArchive",
                     action = "Index",
                 });
            #endregion
            #region RSS
            routes.MapRoute(
                 "RssIndex",
                 "RssArticle/{action}/{id}/{path}/{title}",
                 new
                 {
                     controller = "RssArticle",
                     action = "Index",
                     id = UrlParameter.Optional,
                     path = UrlParameter.Optional,
                     title = UrlParameter.Optional
                 });
            #endregion
            #region DEFAULT
            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new 
                { 
                    controller = "Home", 
                    action = "Index", 
                    id = UrlParameter.Optional
                });
            #endregion
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