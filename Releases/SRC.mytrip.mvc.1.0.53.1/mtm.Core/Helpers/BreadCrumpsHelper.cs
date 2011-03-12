using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using mtm.Core.Settings;

namespace mtm.Core.Helpers
{
    public static class BreadCrumpsHelper
    {
        public static HtmlString BreadCumps(this HtmlHelper html, string a,string[] b)
        {
            if (CoreSetting.Development())
                return null;
            StringBuilder result =new StringBuilder();
            result.Append("<div class=\"breadcrumbs\">");
            result.Append("<a href=\"/\">");
            result.Append(CoreSetting.NameHomePage());
            result.Append("</a>");
            foreach(var c in b){
                if(c!=null&&c.Length>0)
                    result.Append(" " + a + " " + c);
            }
            result.Append("</div>");
            return new HtmlString(result.ToString());
        }
        public static HtmlString BreadCumps(this HtmlHelper html, string a, string b)
        {
            if (CoreSetting.Development())
                return null;
            StringBuilder result = new StringBuilder();
            result.Append("<div class=\"breadcrumbs\">");
            result.Append("<a href=\"/\">");
            result.Append(CoreSetting.NameHomePage());
            result.Append("</a>");
            result.Append(" " + a + " " + b);
            result.Append("</div>");
            return new HtmlString(result.ToString());
        }
        public static HtmlString BreadCumps(this HtmlHelper html, string a)
        {
            if (CoreSetting.Development())
                return null;
            StringBuilder result = new StringBuilder();
            result.Append("<div class=\"breadcrumbs\">");
            result.Append("<a href=\"/\">");
            result.Append(CoreSetting.NameHomePage());
            result.Append("</a>");
            result.Append(" " + a);
            result.Append("</div>");
            return new HtmlString(result.ToString());
        }
    }
}
