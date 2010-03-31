using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Configuration;
using System.Web;

namespace Mytrip.Core.Helpers
{
    /// <summary>
    /// Menu Helper
    /// </summary>
    public static class MenuHelper
    {
        /// <summary>
        /// Mytrip Menu
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <returns></returns>
        public static string MytripMenu(this HtmlHelper html)
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
            TagBuilder li_home = new TagBuilder("li");
            TagBuilder li_about = new TagBuilder("li");
            TagBuilder home = new TagBuilder("a");
            TagBuilder about = new TagBuilder("a");
            TagBuilder a = new TagBuilder("a");
            a.AddCssClass("right_menu");
            home.MergeAttribute("href", "/");
            home.InnerHtml = CoreLanguage.home;
            about.MergeAttribute("href", "/Home/About");
            about.InnerHtml = CoreLanguage.about;
            if (String.IsNullOrEmpty(Controller) | Controller == "/")
                home.AddCssClass("menuvisible");
            if (Action == "About")
                about.AddCssClass("menuvisible");
            li_home.InnerHtml = home.ToString() + a.ToString();
            li_about.InnerHtml = about.ToString() + a.ToString();
            result.AppendLine(li_home.ToString());
            result.AppendLine(li_about.ToString());
            return result.ToString();
        }
        public static string MytripMenu(this HtmlHelper html, string menu1)
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
            TagBuilder li_home = new TagBuilder("li");
            TagBuilder li_about = new TagBuilder("li");
            TagBuilder home = new TagBuilder("a");
            TagBuilder about = new TagBuilder("a");
            TagBuilder a = new TagBuilder("a");
            a.AddCssClass("right_menu");
            home.MergeAttribute("href", "/");
            home.InnerHtml = CoreLanguage.home;
            about.MergeAttribute("href", "/Home/About");
            about.InnerHtml = CoreLanguage.about;
            if (String.IsNullOrEmpty(Controller) | Controller == "/")
                home.AddCssClass("menuvisible");
            if (Action == "About")
                about.AddCssClass("menuvisible");
            li_home.InnerHtml = home.ToString() + a.ToString();
            li_about.InnerHtml = about.ToString() + a.ToString();
            result.AppendLine(li_home.ToString());
            result.AppendLine(menu1);
            result.AppendLine(li_about.ToString());
            return result.ToString();
        }
        public static string MytripMenu(this HtmlHelper html, string menu1, string menu2)
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
            TagBuilder li_home = new TagBuilder("li");
            TagBuilder li_about = new TagBuilder("li");
            TagBuilder home = new TagBuilder("a");
            TagBuilder about = new TagBuilder("a");
            TagBuilder a = new TagBuilder("a");
            a.AddCssClass("right_menu");
            home.MergeAttribute("href", "/");
            home.InnerHtml = CoreLanguage.home;
            about.MergeAttribute("href", "/Home/About");
            about.InnerHtml = CoreLanguage.about;
            if (String.IsNullOrEmpty(Controller) | Controller == "/")
                home.AddCssClass("menuvisible");
            if (Action == "About")
                about.AddCssClass("menuvisible");
            li_home.InnerHtml = home.ToString() + a.ToString();
            li_about.InnerHtml = about.ToString() + a.ToString();
            result.AppendLine(li_home.ToString());
            result.AppendLine(menu1);
            result.AppendLine(menu2);
            result.AppendLine(li_about.ToString());
            return result.ToString();
        }
        public static string MytripMenu(this HtmlHelper html, string menu1, string menu2, string menu3)
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
            TagBuilder li_home = new TagBuilder("li");
            TagBuilder li_about = new TagBuilder("li");
            TagBuilder home = new TagBuilder("a");
            TagBuilder about = new TagBuilder("a");
            TagBuilder a = new TagBuilder("a");
            a.AddCssClass("right_menu");
            home.MergeAttribute("href", "/");
            home.InnerHtml = CoreLanguage.home;
            about.MergeAttribute("href", "/Home/About");
            about.InnerHtml = CoreLanguage.about;
            if (String.IsNullOrEmpty(Controller) | Controller == "/")
                home.AddCssClass("menuvisible");
            if (Action == "About")
                about.AddCssClass("menuvisible");
            li_home.InnerHtml = home.ToString() + a.ToString();
            li_about.InnerHtml = about.ToString() + a.ToString();
            result.AppendLine(li_home.ToString());
            result.AppendLine(menu1);
            result.AppendLine(menu2);
            result.AppendLine(menu3);
            result.AppendLine(li_about.ToString());
            return result.ToString();
        }
    }
}
