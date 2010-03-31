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
using System.Configuration;

namespace Mytrip.Core.Helpers
{
    /// <summary>
    /// Language Helper
    /// </summary>
    public static class LanguageHelper
    {
        public static string LanguageMenu(this HtmlHelper html)
        {
            if (LocalisationSetting.unlockAllCulture)
            {
                StringBuilder result = new StringBuilder();
                TagBuilder li_first = new TagBuilder("li");
                TagBuilder a_first = new TagBuilder("a");
                TagBuilder a1 = new TagBuilder("a");
                a1.AddCssClass("right_topmenu2");
                TagBuilder ul = new TagBuilder("ul");
                TagBuilder li_last = new TagBuilder("li");
                StringBuilder _result = new StringBuilder();
                foreach (string item in LocalisationSetting.allCultureMassive())
                {
                    string a = RedirectLanguage(item, string.Empty);
                    _result.AppendLine(a);
                }
                li_last.InnerHtml = _result.ToString();
                ul.InnerHtml = li_last.ToString();
                a_first.InnerHtml = "Language";
                li_first.InnerHtml = a_first.ToString()+a1.ToString() + ul.ToString();
                result.AppendLine(li_first.ToString());
                return result.ToString();

            }
            else { return string.Empty; }
        }
        public static string RedirectLanguage(string culture, string cssClass)
        {
            string path = HttpContext.Current.Request.Path.ToString();
            path = path.Replace("/", "(x)");
            StringBuilder result = new StringBuilder();
            TagBuilder _result = new TagBuilder("a");
            _result.MergeAttribute("href", "/Language/Index/" + culture + "/" + path);
            if (!String.IsNullOrEmpty(cssClass))
                _result.AddCssClass(cssClass);
            _result.InnerHtml = culture;
            result.AppendLine(_result.ToString());
            return result.ToString();
        }
    }
}
