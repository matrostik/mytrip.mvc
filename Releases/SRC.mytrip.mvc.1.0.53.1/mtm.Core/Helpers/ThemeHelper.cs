/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about mytrip.mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;
using mtm.Core.Settings;
using System.IO;

namespace mtm.Core.Helpers
{
    /// <summary>ХТМЛ Хелпер тем сайта
    /// </summary>
    public static class ThemeHelper
    {
        /// <summary>ХТМЛ разметка меню тем
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <returns>HtmlString</returns>
        public static string ThemeMenu(HtmlHelper html)
        {
            if (ThemeSetting.unlockTheme())
            {
                TagBuilder a_first = new TagBuilder("a");
                string _result = null;
                foreach (string item in ThemeSetting.allThemeMassive())
                {
                    string a = RedirectTheme(item, string.Empty);
                    if (_result == null)
                        _result = a;
                    else
                        _result += "|" + a;
                }

                a_first.InnerHtml = CoreLanguage.theme;
                return GeneralMethods.Menu(html,a_first.ToString(), _result, false, false, false, true);

            }
            else { return null; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="theme"></param>
        /// <param name="cssClass"></param>
        /// <returns></returns>
        public static string RedirectTheme(string theme, string cssClass)
        {
            string path = HttpContext.Current.Request.Path.ToString();
            path = path.Replace("/", "(x)");
            StringBuilder result = new StringBuilder();
            TagBuilder _result = new TagBuilder("a");
            _result.MergeAttribute("href",string.Format("/mtm/Theme/{0}/{1}",theme, path));
            if (!String.IsNullOrEmpty(cssClass))
                _result.AddCssClass(cssClass);
            _result.InnerHtml = theme;
            result.AppendLine(_result.ToString());
            return result.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static HtmlString CssLink(this HtmlHelper html)
        {
            TagBuilder link = new TagBuilder("link");
            link.MergeAttribute("href", "/Theme/" + ThemeSetting.theme() + "/Site.css");
            link.MergeAttribute("rel", "stylesheet");
            link.MergeAttribute("type", "text/css");
            HtmlString _result = new HtmlString(link.ToString());
            return _result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static HtmlString CssLink(this HtmlHelper html, string filename)
        {
            string[] _filename = filename.Split(',');
            StringBuilder result = new StringBuilder();
            foreach (string x in _filename)
            {
                TagBuilder link = new TagBuilder("link");
                link.MergeAttribute("href", "/Theme/" + ThemeSetting.theme() + "/" + x);
                link.MergeAttribute("rel", "stylesheet");
                link.MergeAttribute("type", "text/css");
                result.Append(link.ToString());
            }
            HtmlString _result = new HtmlString(result.ToString());
            return _result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static HtmlString ScriptLink(this HtmlHelper html, string filename)
        {
            
            string folder = (ThemeSetting.theme().ToLower().Contains("Views")) ? "/" : "/Scripts/";
            string[] _filename = filename.Split(',');
            StringBuilder result = new StringBuilder();
            foreach (string x in _filename)
            {
                string _x = x.Trim();
                TagBuilder link = new TagBuilder("script");
                link.MergeAttribute("src", folder + _x);
                link.MergeAttribute("type", "text/javascript");
                result.Append(link.ToString());
            }
            HtmlString _result = new HtmlString(result.ToString());
            return _result;
        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static HtmlString Logo(this HtmlHelper html)
        {
            TagBuilder a = new TagBuilder("a");
            a.MergeAttribute("href", "/");
            string absolutDirectory = HttpContext.Current.Server.MapPath("/Theme/" + ThemeSetting.theme() + "/logo");
            DirectoryInfo _absolutDirectory = new DirectoryInfo(absolutDirectory);
            if (!_absolutDirectory.Exists)
                a.InnerHtml = CoreSetting.nameLogo();
            else
            a.InnerHtml = "<img src='/Theme/" + ThemeSetting.theme() + "/logo/logo.png' alt='" + HttpContext.Current.Request.ServerVariables["HTTP_HOST"] + "' class='logo'/>";
            HtmlString _result = new HtmlString(a.ToString());
            return _result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static HtmlString FooterLogo(this HtmlHelper html)
        {
            TagBuilder a = new TagBuilder("a");
            a.MergeAttribute("href", "/");
            string absolutDirectory = HttpContext.Current.Server.MapPath("/Theme/" + ThemeSetting.theme() + "/logo");
            DirectoryInfo _absolutDirectory = new DirectoryInfo(absolutDirectory);
            if (!_absolutDirectory.Exists)
                return new HtmlString("");
            else
                a.InnerHtml = "<img src='/Theme/" + ThemeSetting.theme() + "/logo/footerlogo.png' alt='" + HttpContext.Current.Request.ServerVariables["HTTP_HOST"] + "' class='footerlogo'/>";
            HtmlString _result = new HtmlString(a.ToString());
            return _result;
        }
    }
}
