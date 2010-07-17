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
    /// File Helper
    /// </summary>
    public static class FileHelper
    {
       
        public static string MytripDirectory(this HtmlHelper html, string path)
        {
            string result = string.Empty;
            if (!String.IsNullOrEmpty(path))
            {
                string[] directory = path.Remove(0, 2).Replace("()", "/").Split('/');
                string _path = string.Empty;
                foreach (string item in directory)
                {
                    if (item == directory[directory.Length - 1])
                    {
                        result += " / " + item;
                    }
                    else
                    {
                        _path += "()" + item;
                        TagBuilder _result = new TagBuilder("a");
                        _result.MergeAttribute("href", "/File/Index/" + _path);
                        _result.InnerHtml = item;
                        result += " / " + _result;
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// Mytrip Mim
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="directory">directory</param>
        /// <param name="name">name</param>
        /// <param name="extension">extension</param>
        /// <returns></returns>
        public static string MytripMim(this HtmlHelper html, string directory, string name, string extension)
        {
            string result = (directory).Replace("()", "/");
            if (extension != ".ico" && extension != ".png" && extension != ".jpg" && extension != ".gif")
             result = "/content/files/" + extension + ".png";
            return result;
        }
        public static string FileImageLink(this HtmlHelper html,string alt, int width, int height, int border,string directory, string name, string extension)
        {
            string UrlAction = string.Empty;
            if (extension == ".ico" || extension == ".png" || extension == ".jpg" || extension == ".gif")
                UrlAction = (directory).Replace("()", "/");
            else if(extension==".Master"||extension==".master"||extension==".cs"||extension==".css"||extension==".aspx"||extension==".xml"
                || extension == ".txt" || extension == ".config" || extension == ".ascx" || extension == ".js")
                UrlAction ="/File/Page/"+ (directory).Replace("/", "()");
            StringBuilder result = new StringBuilder();
            TagBuilder _result = new TagBuilder("a");
            _result.MergeAttribute("href", UrlAction);
            _result.InnerHtml = _Image(MytripMim(html,directory,name,extension), width, height, alt, border);
            result.Append(_result.ToString());
            return result.ToString();
        }
        public static string FileActionLink(this HtmlHelper html, string directory, string name, string extension)
        {
            string UrlAction = string.Empty;
            if (extension == ".ico" || extension == ".png" || extension == ".jpg" || extension == ".gif")
                UrlAction = directory.Replace("()", "/");
            else if (extension == ".Master" || extension == ".master" || extension == ".cs" || extension == ".css" || extension == ".aspx" || extension == ".xml"
                || extension == ".txt" || extension == ".config" || extension == ".ascx" || extension == ".js")
            {
                directory = directory.Replace("/", "()");
                directory = directory.Replace(".", "(x)");
                UrlAction = "/File/Page/" + directory;
            }
            StringBuilder result = new StringBuilder();
            TagBuilder _result = new TagBuilder("a");
            _result.MergeAttribute("href", UrlAction);
            _result.InnerHtml = name;
            result.Append(_result.ToString());
            return result.ToString();
        }
        private static string _Image(string url, int width, int height, string alt, int border)
        {
            string style = string.Empty;
            if (width > 0 && height > 0)
                style = "border-width: " + border + "px; width: " + width + "px; height: " + height + "px;";
            if (width == 0 && height > 0)
                style = "border-width: " + border + "px; height: " + height + "px;";
            if (width > 0 && height == 0)
                style = "border-width: " + border + "px; width: " + width + "px;";
            if (width == 0 && height == 0)
                style = "border-width: " + border + "px;";
            StringBuilder result = new StringBuilder();
            TagBuilder _result = new TagBuilder("img");
            _result.MergeAttribute("src", url);
            _result.MergeAttribute("style", style);
            _result.MergeAttribute("alt", alt);
            result.Append(_result.ToString());
            return result.ToString();
        }
       
    }
}
