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

namespace Mytrip.Mvc.Helpers
{
    /// <summary>
    /// Language Helper
    /// </summary>
    public static class LanguageHelper
    {
        public static string LanguageMenu(this HtmlHelper html)
        {
            LocalisationSetting loc = new LocalisationSetting();
            if (loc.unlockAllCulture())
            {
                TagBuilder a_first = new TagBuilder("a");
                IDictionary<int, string> _result = new Dictionary<int, string>();
                int key = 1;
                foreach (string item in loc.allCultureMassive())
                {
                    string a = RedirectLanguage(item, string.Empty);
                    _result.Add(key,a);
                    key++;
                }
                a_first.InnerHtml = "Language";
                return GeneralMethods.Menu(a_first.ToString(), _result, false, false, false,true);

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
