using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading;
using System.Web.Mvc;

namespace Mytrip.Mvc.Web.Linq2sql.Models
{
    class LocalizationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string culture = (filterContext.HttpContext.Session["culture"] != null)
                                 ? filterContext.HttpContext.Session["culture"].ToString()
                                 : "en-us";

            CultureInfo cultureInfo = new CultureInfo(culture);

            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }
    }
}
