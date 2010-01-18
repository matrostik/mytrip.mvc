using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading;
using System.Web.Mvc;
using Mytrip.Mvc.Model.Linq2sql;
using System.Configuration;

namespace Mytrip.Mvc.Web.Linq2sql.Models
{
    class LocalizationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Задаем культуру по умолчанию
            //string culture = "ru-RU";
            string culture = "";
            //Берем культуру из сессии
            if (filterContext.HttpContext.Session["culture"] != null)
                culture = filterContext.HttpContext.Session["culture"].ToString();
            //Берем культуру из куки
            else if (filterContext.HttpContext.Request.Cookies["myTripCulture"] != null)
                culture = filterContext.HttpContext.Request.Cookies["myTripCulture"].Value;
            //Берем культуру из браузера
            else if (filterContext.HttpContext.Request.UserLanguages[0] != null)
                culture = filterContext.HttpContext.Request.UserLanguages[0];
            //Задаем культуру по умолчанию
            else
                culture = "en-us";
            if (culture.Length < 5)
            {
                culture += "-" + culture;
            }
            filterContext.HttpContext.Session["culture"] = culture;
            
            
            CultureInfo cultureInfo = new CultureInfo(culture);

            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }
    }
}
