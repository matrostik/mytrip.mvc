/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about mytrip.mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using System.Web;
using mtm.Core.Settings;

namespace mtm.Core.Helpers
{
    /// <summary>ХТМЛ Хелпер для языковой панели
    /// </summary>
    public static class LanguageHelper
    {
        /// <summary>Языковая панель меню
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <returns>возвращает HtmlString</returns>
        public static string LanguageMenu()
        {
            if (LocalisationSetting.unlockAllCulture())
            {
                TagBuilder a_first = new TagBuilder("a");
                IDictionary<int, string> _result = new Dictionary<int, string>();
                int count = 0;
                foreach (string item in LocalisationSetting.allCultureMassive())
                {
                    
                    string a = RedirectLanguage(item, string.Empty);
                    _result.Add(count,a);
                    count++;
                }
                a_first.InnerHtml = "Language";
                return GeneralMethods.Menu(a_first.ToString(), _result, false, false, false, true);
                
            }
            else { return null; }
        }

        /// <summary>Ссылка для возвращаения пользователя
        /// после смены культуры
        /// </summary>
        /// <param name="culture">выбранная культура</param>
        /// <param name="cssClass">css класс</param>
        /// <returns>возвращает string</returns>
        private static string RedirectLanguage(string culture, string cssClass)
        {
            string path = HttpContext.Current.Request.Path.ToString();
            path = path.Replace("/", "(x)");
            StringBuilder result = new StringBuilder();
            TagBuilder _result = new TagBuilder("a");
            _result.MergeAttribute("href", string.Format("/mtm/Language/{0}/{1}",culture, path));
            if (!String.IsNullOrEmpty(cssClass))
                _result.AddCssClass(cssClass);
            _result.InnerHtml = culture;
            result.AppendLine(_result.ToString());
            return result.ToString();
        }
    }
}
