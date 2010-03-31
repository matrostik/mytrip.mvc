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
using System.Web.Routing;

namespace Mytrip.Core.Helpers
{
    /// <summary>
    /// Mytrip HtmlHelper
    /// </summary>
    public static class MytripHtmlHelper
    {
        /// <summary>
        /// Mytrip LabelFor
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="target">Target control</param>
        /// <param name="text">Name to display</param>
        /// <returns></returns>
        public static string MytripLabelFor(this HtmlHelper html, string target, string text)
        {
            return String.Format("<label for='{0}'>{1}</label>", target, text);
        }
        /// <summary>
        /// Page Title
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="title">title</param>
        /// <param name="_char">char</param>
        /// <returns>static string</returns>
        public static string PageTitle(this HtmlHelper html, string title, string _char)
        {
            StringBuilder result = new StringBuilder();
            string _result = HttpContext.Current.Request.ServerVariables["HTTP_HOST"] + _char + title;
            result.Append(_result.ToString());
            return result.ToString();
        }
        /// <summary>
        /// Mytrip Input
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="_type">type</param>
        /// <param name="value">value</param>
        /// <param name="name">name</param>
        /// <param name="cssClass">cssClass</param>
        /// <returns>static string</returns>
        public static string MytripInput(this HtmlHelper html, string _type, string value, string name, string cssClass)
        {
            StringBuilder result = new StringBuilder();
            TagBuilder _result = new TagBuilder("input");
            _result.MergeAttribute("type", _type);
            if(!String.IsNullOrEmpty(name))
            _result.MergeAttribute("name", name);
            if (!String.IsNullOrEmpty(value))
            _result.MergeAttribute("value", value);
            if (!String.IsNullOrEmpty(cssClass))
                _result.AddCssClass(cssClass);
            result.Append(_result.ToString());
            return result.ToString();
        }
        /// <summary>
        /// Mytrip Input
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="_type">type</param>
        /// <param name="value">value</param>
        /// <param name="cssClass">cssClass</param>
        /// <returns>static string</returns>
        public static string MytripInput(this HtmlHelper html, string _type, string value, string cssClass)
        {
            StringBuilder result = new StringBuilder();
            TagBuilder _result = new TagBuilder("input");
            _result.MergeAttribute("type", _type);
            _result.MergeAttribute("value", value);
            if (!String.IsNullOrEmpty(cssClass))
                _result.AddCssClass(cssClass);
            result.Append(_result.ToString());
            return result.ToString();
        }
        /// <summary>
        /// Mytrip Input
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="_type">type</param>
        /// <param name="value">value</param>
        /// <returns>static string</returns>
        public static string MytripInput(this HtmlHelper html, string _type, string value)
        {
            StringBuilder result = new StringBuilder();
            TagBuilder _result = new TagBuilder("input");
            _result.MergeAttribute("type", _type);
            _result.MergeAttribute("value", value);
            result.Append(_result.ToString());
            return result.ToString();
        }
        /// <summary>
        /// Mytrip ActionLink
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="UrlAction">UrlAction</param>
        /// <param name="name">name</param>
        /// <returns>static string</returns>
        public static string MytripActionLink(this HtmlHelper html,string UrlAction,string name)
        {
            StringBuilder result = new StringBuilder();
            TagBuilder _result = new TagBuilder("a");
            _result.MergeAttribute("href", UrlAction);
            _result.InnerHtml = name;
            result.Append(_result.ToString());
            return result.ToString();
        }
        /// <summary>
        /// Mytrip ImageLink
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="UrlAction">UrlAction</param>
        /// <param name="url">url</param>
        /// <param name="alt">alt</param>
        /// <param name="width">width</param>
        /// <param name="height">height</param>
        /// <param name="border">border</param>
        /// <param name="onclick">onclick</param>
        /// <returns>static string</returns>
        public static string MytripImageLink(this HtmlHelper html, string UrlAction, string url, string alt, int width, int height, int border,string onclick)
        {
            StringBuilder result = new StringBuilder();
            TagBuilder _result = new TagBuilder("a");
            _result.MergeAttribute("href", UrlAction);
            _result.MergeAttribute("onclick", "return confirm ('" + onclick + "');");
            _result.InnerHtml = _Image(url,width,height,alt,border);
            result.Append(_result.ToString());
            return result.ToString();
        }
        /// <summary>
        /// Mytrip ImageLink
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="UrlAction">UrlAction</param>
        /// <param name="url">url</param>
        /// <param name="alt">alt</param>
        /// <param name="width">width</param>
        /// <param name="height">height</param>
        /// <param name="border">border</param>
        /// <returns>static string</returns>
        public static string MytripImageLink(this HtmlHelper html, string UrlAction, string url, string alt, int width, int height, int border)
        {
            StringBuilder result = new StringBuilder();
            TagBuilder _result = new TagBuilder("a");
            _result.MergeAttribute("href", UrlAction);
            _result.InnerHtml = _Image(url, width, height, alt, border);
            result.Append(_result.ToString());
            return result.ToString();
        }
        /// <summary>
        /// Mytrip ImageLink
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="UrlAction">UrlAction</param>
        /// <param name="url">url</param>
        /// <param name="alt">alt</param>
        /// <param name="cssClass">cssClass</param>
        /// <param name="onclick">onclick</param>
        /// <returns>static string</returns>
        public static string MytripImageLink(this HtmlHelper html, string UrlAction, string url, string alt, string cssClass, string onclick)
        {
            StringBuilder result = new StringBuilder();
            TagBuilder _result = new TagBuilder("a");
            _result.MergeAttribute("href", UrlAction);
            _result.MergeAttribute("onclick", "return confirm ('" + onclick + "');");
            if (!String.IsNullOrEmpty(cssClass))
            {
                _result.InnerHtml = _Image(url, alt, cssClass);
            }
            else { _result.InnerHtml = _Image(url, alt); }
            result.Append(_result.ToString());
            return result.ToString();
        }
        /// <summary>
        /// Mytrip ImageLink
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="UrlAction">UrlAction</param>
        /// <param name="url">url</param>
        /// <param name="alt">alt</param>
        /// <param name="cssClass">cssClass</param>
        /// <returns></returns>
        public static string MytripImageLink(this HtmlHelper html, string UrlAction, string url, string alt, string cssClass)
        {
            StringBuilder result = new StringBuilder();
            TagBuilder _result = new TagBuilder("a");
            _result.MergeAttribute("href", UrlAction);
            if (!String.IsNullOrEmpty(cssClass))
            {
                _result.InnerHtml = _Image(url, alt, cssClass);
            }
            else { _result.InnerHtml = _Image(url, alt); }
            result.Append(_result.ToString());
            return result.ToString();
        }
        /// <summary>
        /// Mytrip Image
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="url">url</param>
        /// <param name="alt">alt</param>
        /// <param name="cssClass">cssClass</param>
        /// <returns>static string</returns>
        public static string MytripImage(this HtmlHelper html,string url, string alt, string cssClass)
        {
            string result = string.Empty;
            if (!String.IsNullOrEmpty(cssClass))
            {
                result = _Image(url, alt, cssClass);
            }
            else { result = _Image(url, alt); }
            return result;
        }
        /// <summary>
        /// Mytrip Image
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="url">url</param>
        /// <param name="alt">alt</param>
        /// <param name="width">width</param>
        /// <param name="height">height</param>
        /// <param name="border">border</param>
        /// <returns>static string</returns>
        public static string MytripImage(this HtmlHelper html, string url, string alt, int width, int height, int border)
        {
            return _Image(url, width, height, alt, border);
        }
        /// <summary>
        /// _Image
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="width">width</param>
        /// <param name="height">height</param>
        /// <param name="alt">alt</param>
        /// <param name="border">border</param>
        /// <returns>static string</returns>
        private static string _Image(string url, int width, int height, string alt, int border)
        {
            string style = string.Empty;
            if (width > 0 && height > 0)
                style = "border-width: " + border + "px; width: " + width + "px; height: " + height + "px;";
            if(width == 0 && height > 0)
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
        /// <summary>
        /// _Image
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="alt">alt</param>
        /// <param name="cssClass">cssClass</param>
        /// <returns>static string</returns>
        private static string _Image(string url,string alt, string cssClass)
        {
            StringBuilder result = new StringBuilder();
            TagBuilder _result = new TagBuilder("img");
            _result.MergeAttribute("src", url);
            _result.MergeAttribute("alt", alt);
            _result.AddCssClass(cssClass);
            result.Append(_result.ToString());
            return result.ToString();
        }
        /// <summary>
        /// _Image
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="alt">alt</param>
        /// <returns>static string</returns>
        private static string _Image(string url, string alt)
        {
            StringBuilder result = new StringBuilder();
            TagBuilder _result = new TagBuilder("img");
            _result.MergeAttribute("src", url);
            _result.MergeAttribute("alt", alt);
            result.Append(_result.ToString());
            return result.ToString();
        }
    }
}
