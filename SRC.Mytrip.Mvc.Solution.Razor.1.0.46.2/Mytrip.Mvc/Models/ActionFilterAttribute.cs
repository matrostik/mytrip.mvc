/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about Mytrip.Mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Mytrip.Mvc.Repository;
using Mytrip.Mvc.Settings;

namespace Mytrip.Mvc.Models
{
    #region Атрибут для определения текущей культуры
    // **********************************************
    // Атрибут для определения текущей культуры
    // **********************************************

    /// <summary>
    /// Атрибут для определения текущей культуры
    /// </summary>
    public class LanguageAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Фильтр для определения текущей культуры
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture(LocalisationSetting.culture());
            filterContext.HttpContext.Session["culture"] = cultureInfo.Name;
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }
    }

    //****************** E N D **********************
    #endregion

    #region Атрибут на принадлежность пользователя к роли администратора сайта
    // **********************************************
    // Атрибут на принадлежность пользователя к роли 
    // администратора сайта
    // **********************************************

    /// <summary>
    /// Атрибут на принадлежность пользователя к роли 
    /// администратора сайта
    /// </summary>
    public class RoleAdminAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Фильтр на принадлежность пользователя к роли 
        /// администратора сайта
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!MytripUser.UserInRole(UsersSetting.roleAdmin()))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                      new { controller = "Account", action = "LogOn", returnUrl = HttpContext.Current.Request.Path.ToString() }));
            }
        }
    }

    //****************** E N D **********************
    #endregion

    #region Атрибут на принадлежность пользователя к роли администратора сайта или главного редактора
    // **********************************************
    // Атрибут на принадлежность пользователя к роли 
    // администратора сайта или главного редактора
    // **********************************************

    /// <summary>
    /// Атрибут на принадлежность пользователя к роли 
    /// администратора сайта или главного редактора
    /// </summary>
    public class RoleAdminAndEditorAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Фильтр на принадлежность пользователя к роли 
        /// администратора сайта или главного редактора
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (MytripUser.UserInRole(UsersSetting.roleAdmin()) || MytripUser.UserInRole(UsersSetting.roleChiefEditor()))
            {}else{
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                      new { controller = "Account", action = "LogOn", returnUrl = HttpContext.Current.Request.Path.ToString() }));
            }
        }
    }

    //****************** E N D **********************
    #endregion

    #region Атрибут на первое включение
    // **********************************************
    // Атрибут на первое включение
    // **********************************************

    /// <summary>
    /// Атрибут на первое включение
    /// </summary>
    public class StartSettingAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Фильтр на первое включение
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!CoreSetting.Development())
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                      new { controller = "Home", action = "Index" }));
            }
        }
    }

    //****************** E N D **********************
    #endregion

}
