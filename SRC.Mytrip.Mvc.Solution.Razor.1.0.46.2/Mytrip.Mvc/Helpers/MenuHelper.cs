/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about Mytrip.Mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using System.Web;
using System.Web.Routing;
using Mytrip.Mvc.Settings;

namespace Mytrip.Mvc.Helpers
{
    /// <summary>ХТМЛ Хелпер меню
    /// </summary>
    public static class MenuHelper
    {
        /// <summary>ХТМЛ разметка меню ядра
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="menu">набор меню модулей</param>
        /// <returns>возвращает HtmlString</returns>
        public static HtmlString MytripMenu(this HtmlHelper html, object menu)
        {
            if (!CoreSetting.Development())
            {
                string urlPath = HttpContext.Current.Request.Path.ToString();
                int urlIndex = -1;
                string Controller = string.Empty;
                string Action = string.Empty;
                urlPath = urlPath.Remove(0, 1);
                if (urlPath.IndexOf("/") != -1)
                    urlIndex = urlPath.IndexOf("/");
                if (urlIndex != -1)
                {
                    Controller = urlPath.Remove(urlIndex);
                    urlPath = urlPath.Remove(0, urlIndex + 1);
                    if (urlPath.IndexOf("/") != -1)
                        urlIndex = urlPath.IndexOf("/");
                    if (urlIndex != -1)
                    {
                        Action = urlPath;
                    }
                }
                else { Controller = urlPath; }
                StringBuilder result = new StringBuilder();
                TagBuilder home = new TagBuilder("a");
                TagBuilder about = new TagBuilder("a");
                home.MergeAttribute("href", "/");
                home.InnerHtml = CoreSetting.NameHomePage();
                about.MergeAttribute("href", "/Home/About");
                about.InnerHtml = CoreSetting.NameAboutPage();
                if (String.IsNullOrEmpty(Controller) | Controller == "/")
                    result.Append(GeneralMethods.Menu(home.ToString(), null, true, false, true, false));
                else
                    result.Append(GeneralMethods.Menu(home.ToString(), null, false, false, true, false));
                IDictionary<string, object> _menu = (menu == null ? new RouteValueDictionary() : new RouteValueDictionary(menu));
                foreach (string key in _menu.Keys)
                {
                    result.Append(_menu[key].ToString());
                }
                if (Action == "About")
                    result.Append(GeneralMethods.Menu(about.ToString(), null, true, false, true, false));
                else
                    result.Append(GeneralMethods.Menu(about.ToString(), null, false, false, true, false));
                HtmlString htmlresult = new HtmlString(result.ToString());
                return htmlresult;
            }
            else
            {
                return null;
            }
        }
    }
}
