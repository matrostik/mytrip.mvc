using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mtm.Core.Settings;
using System.Xml.Linq;
using System.Web.Mvc;
using System.Text;

namespace mtm.Core.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public static class RewriteHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static HtmlString GetAllRewrite()
        {
            string _absolutDirectory = GeneralMethods.MytripConfigurationDirectory("mtm.Rewrite");
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var x = _doc.Root.Elements("add");
            StringBuilder result = new StringBuilder();
            TagBuilder img = new TagBuilder("img");
            img.MergeAttribute("src", "/Theme/" + ThemeSetting.theme() + "/images/add.png");
            img.MergeAttribute("style", "height:14px;");
            result.Append("<div class='right'>" + GeneralMethods.ImgInput("/images/plus.png", "", "add", 16)+"</div>");
            result.Append("<table class='noborders'>");
            result.Append("<tr><td>");
            result.Append("<h3>"+CoreLanguage.linkrewrite1+"</h3>");
            result.Append("</td><td>");
            result.Append("<h3>" + CoreLanguage.linkrewrite2 + "</h3>");
            result.Append("</td></tr>");
            int ctr = 0;
            foreach (var item in x)
            {
                 string a = item.Attribute("url").Value;
                string b = item.Attribute("rewrite").Value;
                string edit = GeneralMethods.ImgInput("/images/edite.png", a, "rename", 14,b) +
                               " " + GeneralMethods.ImgInput("/images/delete.png", "/Core/DleteRewrite/" + a.Replace("/","()"), "delete", 14);
              
                if (ctr % 2 == 0)
                   result.Append("<tr class='profile1'><td>");
                else
                    result.Append("<tr class='profile2'><td>");
                result.Append("<a href='/" + a + "'>" + a + "</a>");
                result.Append("</td><td>");
                result.Append("<a href='/" + b + "'>" + b + "</a>");
                result.Append("</td><td>"+edit);
                result.Append("</td></tr>");
            }
            result.Append("</table>");
            return new HtmlString(result.ToString());

        }
    }
}