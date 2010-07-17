//************************************************************ 
// Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich
// To learn more about Mytrip.Mvc.Entyty visit 
// http://starterkitmytripmvc.codeplex.com/
// mytripmvc@gmail.com
// license: Microsoft Public License (Ms-PL) 
// ***********************************************************
using System;
using System.Web.Mvc;
using System.Text;
using System.Web;


namespace Mytrip.Mvc.Helpers
{
    /// <summary>
    /// Pager Helper
    /// </summary>
    public static class PagerHelper
    {
        /// <summary>
        /// Mytrip Pager
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="defaultPageSize">defaultPageSize</param>
        /// <param name="total">total</param>
        /// <param name="staticText">staticText</param>
        /// <param name="cssClass">cssClass</param>
        /// <param name="cssClassActiv">cssClassActiv</param>
        /// <param name="cssClassStatic">cssClassStatic</param>
        /// <returns>static string</returns>
        public static string MytripPager(this HtmlHelper html, int defaultPageSize, int total, string staticText, string cssClass, string cssClassActiv, string cssClassStatic)
        {
           
            StringBuilder result = new StringBuilder();
            TagBuilder table_td_pager = new TagBuilder("td");
            table_td_pager.MergeAttribute("style", "text-align: left;padding: 0px; margin: 0px;border: 0px;");
            table_td_pager.InnerHtml = Pager(defaultPageSize, total, staticText,
                cssClass,cssClassActiv,cssClassStatic);
            TagBuilder table_td_count = new TagBuilder("td");
            table_td_count.MergeAttribute("style", "text-align: right;padding: 0px; margin: 0px;border: 0px;");
            table_td_count.InnerHtml = PagerCount(defaultPageSize, total, cssClass, cssClassActiv);
            TagBuilder table_tr = new TagBuilder("tr");
            table_tr.InnerHtml = table_td_pager.ToString() + table_td_count.ToString();
            TagBuilder table = new TagBuilder("table");
            table.MergeAttribute("style", "width: 100%;padding: 0px; margin: 0px;border: 0px;");
            table.InnerHtml = table_tr.ToString();
            result.AppendLine(table.ToString());
            return result.ToString();
        }
        /// <summary>
        /// Mytrip Pager
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="defaultPageSize">defaultPageSize</param>
        /// <param name="total">total</param>
        /// <param name="staticText">staticText</param>
        /// <returns>static string</returns>
        public static string MytripPager(this HtmlHelper html, int defaultPageSize, int total, string staticText)
        {
           
            StringBuilder result = new StringBuilder();
            TagBuilder table_td_pager = new TagBuilder("td");
            table_td_pager.MergeAttribute("style", "text-align: left; padding: 0px; margin: 0px;border: 0px;");
            table_td_pager.InnerHtml = Pager(defaultPageSize, total, staticText,
                string.Empty, string.Empty, string.Empty);
            TagBuilder table_td_count = new TagBuilder("td");
            table_td_count.MergeAttribute("style", "text-align: right;padding: 0px; margin: 0px;border: 0px;");
            table_td_count.InnerHtml = PagerCount(defaultPageSize, total, string.Empty, string.Empty);
            TagBuilder table_tr = new TagBuilder("tr");
            table_tr.InnerHtml = table_td_pager.ToString() + table_td_count.ToString();
            TagBuilder table = new TagBuilder("table");
            table.MergeAttribute("style", "width: 100%;padding: 0px; margin: 0px;border: 0px;");
            table.InnerHtml = table_tr.ToString();
            result.AppendLine(table.ToString());
            return result.ToString();
        }
        /// <summary>
        /// Mytrip Sort
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="nameSort">nameSort</param>
        /// <returns>static string</returns>
        public static string MytripSort(this HtmlHelper html,string nameSort )
        {
            string urlPath = HttpContext.Current.Request.Path.ToString();
            int urlIndex = -1;
            string sort = nameSort;
            urlPath = urlPath.Remove(0, 1);
            if (urlPath.IndexOf("/") != -1)
                urlIndex = urlPath.IndexOf("/");
            if (urlIndex != -1)
            {
                urlPath = urlPath.Remove(0, urlIndex + 1);
                if (urlPath.IndexOf("/") != -1)
                    urlIndex = urlPath.IndexOf("/");
                if (urlIndex != -1)
                {
                    urlPath = urlPath.Remove(0, urlIndex + 1);
                    if (urlPath.IndexOf("/") != -1)
                        urlIndex = urlPath.IndexOf("/");
                    if (urlIndex != -1)
                    {
                        urlPath = urlPath.Remove(0, urlIndex + 1);
                        if (urlPath.IndexOf("/") != -1)
                            urlIndex = urlPath.IndexOf("/");
                        if (urlIndex != -1)
                        {
                            sort = urlPath.Remove(0, urlIndex + 1);
                        }
                    }
                }
            }
            if (!String.IsNullOrEmpty(sort) && sort.Replace("_", "") == nameSort)
            {
                if (sort.IndexOf("_") != -1)
                {
                    sort = sort.Replace("_", "");
                }
                else { sort = "_" + nameSort; }
            }
            else {sort=nameSort; }
            return sort;

        }
        /// <summary>
        /// TagA
        /// </summary>
        /// <param name="Text">Text</param>
        /// <param name="url">url</param>
        /// <param name="cssClass">cssClass</param>
        /// <returns>static string</returns>
        private static string TagA(string Text, string url, string cssClass)
        {
            StringBuilder result = new StringBuilder();
            TagBuilder _result = new TagBuilder("a");
            if (!String.IsNullOrEmpty(url))
                _result.MergeAttribute("href", url);
            if (!String.IsNullOrEmpty(cssClass))
                _result.AddCssClass(cssClass);
            _result.InnerHtml = Text;
            result.AppendLine(_result.ToString());
            return result.ToString();
        }
        /// <summary>
        /// Pager
        /// </summary>
        /// <param name="defaultPageSize">defaultPageSize</param>
        /// <param name="total">total</param>
        /// <param name="staticText">staticText</param>
        /// <param name="cssClass">cssClass</param>
        /// <param name="cssClassActiv">cssClassActiv</param>
        /// <param name="cssClassStatic">cssClassStatic</param>
        /// <returns>static string</returns>
        private static string Pager(int defaultPageSize, int total, string staticText, string cssClass, string cssClassActiv, string cssClassStatic)
        {
            string urlPath = HttpContext.Current.Request.Path.ToString();
            int urlIndex = -1;
            string Controller = string.Empty;
            string Action = "Index";
            int pageIndex = 1;
            int pageSize = defaultPageSize;
            string sort = string.Empty;
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
                    Action = urlPath.Remove(urlIndex);
                    urlPath = urlPath.Remove(0, urlIndex + 1);
                    if (urlPath.IndexOf("/") != -1)
                        urlIndex = urlPath.IndexOf("/");
                    if (urlIndex != -1)
                    {
                        pageIndex = int.Parse(urlPath.Remove(urlIndex));
                        urlPath = urlPath.Remove(0, urlIndex + 1);
                        if (urlPath.IndexOf("/") != -1)
                            urlIndex = urlPath.IndexOf("/");
                        if (urlIndex != -1)
                        {
                            pageSize = int.Parse(urlPath.Remove(urlIndex));
                            sort = urlPath.Remove(0, urlIndex + 1);
                        }
                    }
                }
            }
            else
            {
                Controller = urlPath;
            }
            int pageTotal = (int)Math.Ceiling((double)total / pageSize);
            StringBuilder result = new StringBuilder();
            if (pageIndex == 1) 
            {
               result.AppendLine(TagA("1", string.Empty, cssClassActiv)); 
            }
            else
            {
                result.AppendLine(TagA("1", "/" + Controller + "/" + Action + "/1/" + pageSize + "/" + sort, cssClass)); 
            }
            int _pageIndex = pageIndex;
            if (pageIndex == 1)
                _pageIndex = 2; 
            if (pageIndex == 3) 
                _pageIndex = 2;
            if (pageIndex > 3)
                _pageIndex = pageIndex - 2;
            int _page_Index; 
            if (pageIndex <= 4)
            {
                for (_page_Index = _pageIndex; pageTotal >= _page_Index; _page_Index++)
                {
                    if (_page_Index == pageIndex) 
                    {
                        result.AppendLine(TagA(_page_Index.ToString(), string.Empty, cssClassActiv)); 
                    }
                    else
                    {
                        result.AppendLine(TagA(_page_Index.ToString(), "/" + Controller + "/" + Action + "/" + _page_Index + "/" + pageSize + "/" + sort, cssClass)); 
                    }
                    if (_page_Index == pageTotal - 1)
                        break;
                    if (_page_Index == pageIndex + 3)
                        break;
                } if (_page_Index <= pageIndex + 3 && _page_Index <= pageTotal - 3)
                {
                    result.AppendLine(TagA(staticText, string.Empty, cssClassStatic));
                }
            }
            else
            {
                if (pageIndex > 5)
                {
                    result.AppendLine(TagA(staticText, string.Empty, cssClassStatic));
                }
                for (_page_Index = _pageIndex - 1; pageTotal >= _page_Index; _page_Index++)
                {
                    if (_page_Index == pageIndex)
                    {
                        result.AppendLine(TagA(_page_Index.ToString(), string.Empty, cssClassActiv));
                    }
                    else
                    {
                        result.AppendLine(TagA(_page_Index.ToString(), "/" + Controller + "/" + Action + "/" + _page_Index + "/" + pageSize + "/" + sort, cssClass)); 
                    }
                    if (_page_Index == pageTotal - 1)
                        break;
                    if (_page_Index == pageIndex + 3)
                        break;
                } if (_page_Index <= pageIndex + 3 && _page_Index <= pageTotal - 3)
                {
                    result.AppendLine(TagA(staticText, string.Empty, cssClassStatic));
                }
            }
            if (pageTotal > 5 && pageIndex == pageTotal - 5)
            {
                result.AppendLine(TagA(staticText, string.Empty, cssClassStatic));
            }
            if (pageTotal >= 3)
            {
                if (pageTotal == pageIndex)
                {
                    result.AppendLine(TagA(pageTotal.ToString(), string.Empty, cssClassActiv));
                }
                else
                {
                    result.AppendLine(TagA(pageTotal.ToString(), "/" + Controller + "/" + Action + "/" + pageTotal + "/" + pageSize + "/" + sort, cssClass));
                }
            }
            return result.ToString();
        } 
        /// <summary>
        /// Pager Count
        /// </summary>
        /// <param name="defaultPageSize">defaultPageSize</param>
        /// <param name="total">total</param>
        /// <param name="cssClass">cssClass</param>
        /// <param name="cssClassActiv">cssClassActiv</param>
        /// <returns>static string</returns>
        private static string PagerCount(int defaultPageSize, int total, string cssClass, string cssClassActiv)
        {
            string urlPath = HttpContext.Current.Request.Path.ToString();
            int urlIndex = -1;
            string Controller = string.Empty;
            string Action = "Index";
            int pageIndex = 1;
            int pageSize = defaultPageSize;
            string sort = string.Empty;
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
                    Action = urlPath.Remove(urlIndex);
                    urlPath = urlPath.Remove(0, urlIndex + 1);
                    if (urlPath.IndexOf("/") != -1)
                        urlIndex = urlPath.IndexOf("/");
                    if (urlIndex != -1)
                    {
                        pageIndex = int.Parse(urlPath.Remove(urlIndex));
                        urlPath = urlPath.Remove(0, urlIndex + 1);
                        if (urlPath.IndexOf("/") != -1)
                            urlIndex = urlPath.IndexOf("/");
                        if (urlIndex != -1)
                        {
                            pageSize = int.Parse(urlPath.Remove(urlIndex));
                            sort = urlPath.Remove(0, urlIndex + 1);
                        }
                    }
                }
            }
            else
            {
                Controller = urlPath;
            }
            StringBuilder result = new StringBuilder();
            if (pageSize == 5)
            {
                result.AppendLine(TagA("5", string.Empty, cssClassActiv));
            }
            else
            {
                result.AppendLine(TagA("5", "/" + Controller + "/" + Action + "/1/5/" + sort, cssClass));
            }

            if (pageSize == 10)
            {
                result.AppendLine(TagA("10", string.Empty, cssClassActiv));
            }
            else
            {
                result.AppendLine(TagA("10", "/" + Controller + "/" + Action + "/1/10/" + sort, cssClass));
            }
            if (total > 10)
            {
                if (pageSize == 25)
                {
                    result.AppendLine(TagA("25", string.Empty, cssClassActiv));
                }
                else
                {
                    result.AppendLine(TagA("25", "/" + Controller + "/" + Action + "/1/25/" + sort, cssClass));
                }
            } if (total > 25)
            {
                if (pageSize == 50)
                {
                    result.AppendLine(TagA("50", string.Empty, cssClassActiv));
                }
                else
                {
                    result.AppendLine(TagA("50", "/" + Controller + "/" + Action + "/1/50/" + sort, cssClass));
                }
            } if (total > 50)
            {
                if (pageSize == 100)
                {
                    result.AppendLine(TagA("100", string.Empty, cssClassActiv));
                }
                else
                {
                    result.AppendLine(TagA("100", "/" + Controller + "/" + Action + "/1/100/" + sort, cssClass));
                }
            }
            return result.ToString();
        }
    }
}

