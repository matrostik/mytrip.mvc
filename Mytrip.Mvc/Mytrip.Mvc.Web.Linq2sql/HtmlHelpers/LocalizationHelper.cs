using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Compilation;
using System.Globalization;

namespace Mytrip.Mvc.Web.Linq2sql.HtmlHelpers
{
    public static class LocalizationHelper
    {
       
        public static string Language(this HtmlHelper html, string a, string b) {
            return String.Format(a,b);

        }
    }
}
