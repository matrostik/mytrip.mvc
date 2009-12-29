using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace Mytrip.Mvc.Web.Linq2sql.HtmlHelpers
{
    public static class TegHelper
    {
        public static string Tegs(this HtmlHelper html, int a, string b, string c)
        {
            StringBuilder result = new StringBuilder();
            Random d = new Random();
            int e = d.Next(10, 18);
            if (e == 10)
            {
                TagBuilder a_1 = new TagBuilder("a");
                a_1.MergeAttribute("href", "/C/E/" + a + "/1/10/" + c);
                a_1.MergeAttribute("style", "font-size: 10px");
                a_1.InnerHtml = b;
                result.AppendLine(a_1.ToString());

            } if (e == 11)
            {
                TagBuilder a_1 = new TagBuilder("a");
                a_1.MergeAttribute("href", "/C/E/" + a + "/1/10/" + c);
                a_1.MergeAttribute("style", "font-size: 10px; font-weight: bold;");
                a_1.InnerHtml = b;
                result.AppendLine(a_1.ToString());
                
            } if (e == 12)
            {
                TagBuilder a_1 = new TagBuilder("a");
                a_1.MergeAttribute("href", "/C/E/" + a + "/1/10/" + c);
                a_1.MergeAttribute("style", "font-size: 12px");
                a_1.InnerHtml = b;
                result.AppendLine(a_1.ToString());

            } if (e == 13)
            {
                TagBuilder a_1 = new TagBuilder("a");
                a_1.MergeAttribute("href", "/C/E/" + a + "/1/10/" + c);
                a_1.MergeAttribute("style", "font-size: 12px; font-weight: bold;");
                a_1.InnerHtml = b;
                result.AppendLine(a_1.ToString());

            } if (e == 14)
            {
                TagBuilder a_1 = new TagBuilder("a");
                a_1.MergeAttribute("href", "/C/E/" + a + "/1/10/" + c);
                a_1.MergeAttribute("style", "font-size: 14px");
                a_1.InnerHtml = b;
                result.AppendLine(a_1.ToString());

            } if (e == 15)
            {
                TagBuilder a_1 = new TagBuilder("a");
                a_1.MergeAttribute("href", "/C/E/" + a + "/1/10/" + c);
                a_1.MergeAttribute("style", "font-size: 14px; font-weight: bold;");
                a_1.InnerHtml = b;
                result.AppendLine(a_1.ToString());

            } if (e == 16)
            {
                TagBuilder a_1 = new TagBuilder("a");
                a_1.MergeAttribute("href", "/C/E/" + a + "/1/10/" + c);
                a_1.MergeAttribute("style", "font-size: 16px");
                a_1.InnerHtml = b;
                result.AppendLine(a_1.ToString());

            } if (e == 17)
            {
                TagBuilder a_1 = new TagBuilder("a");
                a_1.MergeAttribute("href", "/C/E/" + a + "/1/10/" + c);
                a_1.MergeAttribute("style", "font-size: 16px; font-weight: bold;");
                a_1.InnerHtml = b;
                result.AppendLine(a_1.ToString());

            } if (e == 18)
            {
                TagBuilder a_1 = new TagBuilder("a");
                a_1.MergeAttribute("href", "/C/E/" + a + "/1/10/" + c);
                a_1.MergeAttribute("style", "font-size: 18px");
                a_1.InnerHtml = b;
                result.AppendLine(a_1.ToString());
            }
            return result.ToString();

        }
    }
}
