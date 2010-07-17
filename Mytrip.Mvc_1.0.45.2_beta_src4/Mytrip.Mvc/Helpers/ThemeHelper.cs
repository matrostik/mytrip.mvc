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
using System.Text;
using System.Web.Mvc;
using System.Web;

namespace Mytrip.Mvc.Helpers
{
    /// <summary>
    /// Theme Helper
    /// </summary>
    public static class ThemeHelper
    {
        public static string ThemeMenu(this HtmlHelper html)
        {
            ThemeSetting theme = new ThemeSetting();
            if (theme.unlockTheme())
            {
                TagBuilder a_first = new TagBuilder("a");
                IDictionary<int, string> _result = new Dictionary<int, string>();
                int key = 1;
                foreach (string item in theme.allThemeMassive())
                {
                    string a = RedirectTheme(item, string.Empty);
                    _result.Add(key, a);
                    key++;
                }

                a_first.InnerHtml = CoreLanguage.theme;
                return GeneralMethods.Menu(a_first.ToString(), _result, false, false, false, true);

            }
            else { return string.Empty; }
        }
        public static string RedirectTheme(string theme, string cssClass)
        {
            string path = HttpContext.Current.Request.Path.ToString();
            path = path.Replace("/", "(x)");
            StringBuilder result = new StringBuilder();
            TagBuilder _result = new TagBuilder("a");
            _result.MergeAttribute("href", "/Theme/Index/" + theme + "/" + path);
            if (!String.IsNullOrEmpty(cssClass))
                _result.AddCssClass(cssClass);
            _result.InnerHtml = theme;
            result.AppendLine(_result.ToString());
            return result.ToString();
        }
        public static string CssLink(this HtmlHelper html)
        {
            ThemeSetting theme = new ThemeSetting();
            StringBuilder result = new StringBuilder();
            TagBuilder link = new TagBuilder("link");
            link.MergeAttribute("href", "/Theme/" + theme.theme() + "/Site.css");
            link.MergeAttribute("rel", "stylesheet");
            link.MergeAttribute("type", "text/css");
            result.AppendLine(link.ToString());
            return result.ToString();
        }
        public static string Logo(this HtmlHelper html)
        {
            ThemeSetting theme = new ThemeSetting();
            TagBuilder a = new TagBuilder("a");
            a.MergeAttribute("href", "/");
            a.InnerHtml = "<img src='/Theme/" + theme.theme() + "/images/logo.png' alt='" + HttpContext.Current.Request.ServerVariables["HTTP_HOST"] + "' class='logo'/>";
            return a.ToString();
        }
        public static string FooterLogo(this HtmlHelper html)
        {
            ThemeSetting theme = new ThemeSetting();
            TagBuilder a = new TagBuilder("a");
            a.MergeAttribute("href", "/");
            a.InnerHtml = "<img src='/Theme/" + theme.theme() + "/images/footerlogo.png' alt='" + HttpContext.Current.Request.ServerVariables["HTTP_HOST"] + "' class='footerlogo'/>";
            return a.ToString();
        }
    }
}
