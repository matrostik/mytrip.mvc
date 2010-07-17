//************************************************************ 
// Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich
// To learn more about Mytrip.Mvc.Entyty visit 
// http://starterkitmytripmvc.codeplex.com/
// mytripmvc@gmail.com
// license: Microsoft Public License (Ms-PL) 
// ***********************************************************
using System.Web.Mvc;
using System.Globalization;
using System.Threading;

namespace Mytrip.Mvc.Models
{
    /// <summary>
    /// LocalizationAttribute
    /// </summary>
    public class LocalizationAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// On Action Executing
        /// </summary>
        /// <param name="filterContext">ActionExecutingContext</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            LocalisationSetting loc = new LocalisationSetting();
            string culture = loc.defaultCulture();
            if (filterContext.HttpContext.Session["culture"] != null)
                culture = filterContext.HttpContext.Session["culture"].ToString();
            if (filterContext.HttpContext.Request.UserLanguages[0] != null)
                culture = filterContext.HttpContext.Request.UserLanguages[0];
            if (filterContext.HttpContext.Request.Cookies["myTripCulture"] != null)
                culture = filterContext.HttpContext.Request.Cookies["myTripCulture"].Value;
            if (loc.allCulture().IndexOf(culture) == -1)
                culture = loc.defaultCulture();
            if (!loc.unlockAllCulture())
                culture = loc.defaultCulture();
            CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture(culture);
            filterContext.HttpContext.Session["culture"] = cultureInfo.Name;
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }
    }
}
