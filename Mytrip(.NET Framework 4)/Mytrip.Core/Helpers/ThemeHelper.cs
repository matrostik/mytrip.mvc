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

namespace Mytrip.Core.Helpers
{
    /// <summary>
    /// Theme Helper
    /// </summary>
    public static class ThemeHelper
    {
        public static string ThemeMenu(this HtmlHelper html)
        {
            if (ThemeSetting.unlockTheme)
            {
                StringBuilder result = new StringBuilder();
                TagBuilder li_first = new TagBuilder("li");
                TagBuilder a_first = new TagBuilder("a");
                TagBuilder a1 = new TagBuilder("a");
                a1.AddCssClass("right_topmenu1");
                TagBuilder ul = new TagBuilder("ul");
                TagBuilder li_last = new TagBuilder("li");
                StringBuilder _result = new StringBuilder();
                foreach (string item in ThemeSetting.allThemeMassive())
                {
                    string a = RedirectTheme(item, string.Empty);
                    _result.AppendLine(a);
                }
                li_last.InnerHtml = _result.ToString();
                ul.InnerHtml = li_last.ToString();
                a_first.InnerHtml = CoreLanguage.theme;
                li_first.InnerHtml = a_first.ToString()+a1.ToString() + ul.ToString();
                result.AppendLine(li_first.ToString());
                return result.ToString();

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
        public static string CssLink(this HtmlHelper html) {
            StringBuilder result = new StringBuilder();
            TagBuilder link = new TagBuilder("link");
            link.MergeAttribute("href", "/Theme/"+ThemeSetting.theme()+"/Site.css");
            link.MergeAttribute("rel", "stylesheet");
            link.MergeAttribute("type", "text/css");
            result.AppendLine(link.ToString());
            TagBuilder _link = new TagBuilder("link");
            if (ThemeSetting.brouser == "IE")
            {
                if (ThemeSetting.majorversion < 8)
                     _link.MergeAttribute("href", "/Theme/" + ThemeSetting.theme() + "/IE7.css");
                else
                    _link.MergeAttribute("href", "/Theme/" + ThemeSetting.theme() + "/IE8.css");
            }
            else if (ThemeSetting.brouser == "Chrome")
                _link.MergeAttribute("href", "/Theme/" + ThemeSetting.theme() + "/Chrome.css");
            else if (ThemeSetting.brouser == "Firefox")
                _link.MergeAttribute("href", "/Theme/" + ThemeSetting.theme() + "/Firefox.css");
            else if (ThemeSetting.brouser == "Opera")
                _link.MergeAttribute("href", "/Theme/" + ThemeSetting.theme() + "/Opera.css");
            else if (ThemeSetting.brouser == "Safari")
                _link.MergeAttribute("href", "/Theme/" + ThemeSetting.theme() + "/Safari.css");
            else
                _link.MergeAttribute("href", "/Theme/" + ThemeSetting.theme() + "/IE8.css");
            _link.MergeAttribute("rel", "stylesheet");
            _link.MergeAttribute("type", "text/css");
            result.AppendLine(_link.ToString());
            return result.ToString();
        }
        public static string Logo(this HtmlHelper html)
        {
            TagBuilder a = new TagBuilder("a");
            a.MergeAttribute("href", "/");
            a.InnerHtml = "<img src='/Theme/" + ThemeSetting.theme() + "/images/logo.png' alt='logo' style='border:0'/>";
            return a.ToString();


        }
    }
}
