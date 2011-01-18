/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about mytrip.mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System;
using System.Web.Mvc;
using System.Text;
using System.Web;
using mtm.Core.Settings;


namespace mtm.Core.Helpers
{
    /// <summary>ХТМЛ Хелпер пейджинга
    /// </summary>
    public static class PagerHelper
    {
        /// <summary>ХТМЛ разметка пейджинга
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="defaultPageSize">количество на странице по умолчанию</param>
        /// <param name="total">общее количество контента</param>
        /// <param name="staticText">отображаемый статический текст</param>
        /// <returns>возвращает HtmlString</returns>
        public static HtmlString MytripPager(this HtmlHelper html, int defaultPageSize, int total, string staticText)
        {

            StringBuilder result = new StringBuilder();
            TagBuilder div = new TagBuilder("div");
            div.AddCssClass("pager");
            div.InnerHtml = Pager(defaultPageSize, total, staticText) + PagerCount(defaultPageSize, total);
            return new HtmlString(div.ToString());
        }

        /// <summary>ХМЛ Разметка параметра сортировки в таблице данных
        /// добавляет или удаляет "_" у параметра сортировки
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="nameSort">имя параметра сортировки</param>
        /// <returns>возвращает HtmlString</returns>
        public static HtmlString MytripSort(this HtmlHelper html, string nameSort)
        {
            string sort = string.Empty;
            string[] urlpath = GeneralMethods.UrlDictionary(HttpContext.Current.Request.Path);
            if (urlpath.Length >= 6)
                sort = urlpath[5];
            if (!String.IsNullOrEmpty(sort) && sort.Replace("_", "") == nameSort)
                sort = sort.Contains("_") ? sort.Replace("_", "") : ("_" + nameSort);
            else
                sort = nameSort;
            return new HtmlString(sort);

        }

        /// <summary>ХТМЛ разметка пейджинга
        /// </summary>
        /// <param name="defaultPageSize">количество на странице по умолчанию</param>
        /// <param name="total">общее количество контента</param>
        /// <param name="staticText">отображаемый статический текст</param>
        /// <returns>возвращает string</returns>
        private static string Pager(int defaultPageSize, int total, string staticText)
        {
            int pageIndex = 1;
            int pageSize = defaultPageSize;
            string sort = string.Empty;
            string[] urlpath = GeneralMethods.UrlDictionary(HttpContext.Current.Request.Path);
            string Controller = "/" + urlpath[1];
            if (urlpath.Length >= 3)
                Controller += "/" + urlpath[2] + "/";
            else
                Controller += "/Index/";
            if (urlpath.Length >= 4)
                int.TryParse(urlpath[3], out pageIndex);
            if (urlpath.Length >= 5)
                int.TryParse(urlpath[4], out pageSize);
            sort = "/" + pageSize;
            int id = 6;
            while (urlpath.Length >= id)
            {
                sort += "/" + urlpath[id - 1];
                id++;
            }
            int pageTotal = (int)Math.Ceiling((double)total / pageSize);
            StringBuilder result = new StringBuilder();
            result.AppendLine("<div class='left'>" + CoreLanguage.pagerPage + "</div>");
            if (pageIndex == 1)
                result.AppendLine(GeneralMethods.Pager(string.Empty, "1", true, "left"));
            else
                result.AppendLine(GeneralMethods.Pager(Controller + "1" + sort, "1", false, "left"));
            int _pageIndex = pageIndex;
            if (pageIndex == 1 || pageIndex == 3)
                _pageIndex = 2;
            else if (pageIndex > 3)
                _pageIndex = pageIndex - 2;
            int _page_Index;
            if (pageIndex <= 4)
            {
                for (_page_Index = _pageIndex; pageTotal >= _page_Index; _page_Index++)
                {
                    if (_page_Index == pageIndex)
                        result.AppendLine(GeneralMethods.Pager(string.Empty, _page_Index.ToString(), true, "left"));
                    else
                        result.AppendLine(GeneralMethods.Pager(Controller + _page_Index + sort, _page_Index.ToString(), false, "left"));
                    if (_page_Index == pageTotal - 1||_page_Index == pageIndex + 3)
                        break;
                } if (_page_Index <= pageIndex + 3 && _page_Index <= pageTotal - 3)
                    result.AppendLine(GeneralMethods.pagerStaticText(staticText));
            }
            else
            {
                if (pageIndex > 5)
                    result.AppendLine(GeneralMethods.pagerStaticText(staticText));
                for (_page_Index = _pageIndex - 1; pageTotal >= _page_Index; _page_Index++)
                {
                    if (_page_Index == pageIndex)
                        result.AppendLine(GeneralMethods.Pager(string.Empty, _page_Index.ToString(), true, "left"));
                    else
                        result.AppendLine(GeneralMethods.Pager(Controller + _page_Index + sort, _page_Index.ToString(), false, "left"));
                    if (_page_Index == pageTotal - 1||_page_Index == pageIndex + 3)
                        break;
                } if (_page_Index <= pageIndex + 3 && _page_Index <= pageTotal - 3)
                    result.AppendLine(GeneralMethods.pagerStaticText(staticText));
            }
            if (pageTotal > 5 && pageIndex == pageTotal - 5)
                result.AppendLine(GeneralMethods.pagerStaticText(staticText));
            if (pageTotal >= 3 && pageTotal == pageIndex)
                result.AppendLine(GeneralMethods.Pager(string.Empty, pageTotal.ToString(), true, "left"));
            else if (pageTotal >= 3 && pageTotal != pageIndex)
                result.AppendLine(GeneralMethods.Pager(Controller + pageTotal + sort, pageTotal.ToString(), false, "left"));
            return result.ToString();
        }

        /// <summary>ХТМЛ разметка выбора количества контента на странице 
        /// </summary>
        /// <param name="defaultPageSize">количество на странице по умолчанию</param>
        /// <param name="total">общее количество контента</param>
        /// <returns>возвращает string</returns>
        private static string PagerCount(int defaultPageSize, int total)
        {
            int pageIndex = 1;
            int pageSize = defaultPageSize;
            string sort = string.Empty;
            string[] urlpath = GeneralMethods.UrlDictionary(HttpContext.Current.Request.Path);
            string Controller = "/" + urlpath[1];
            if (urlpath.Length >= 3)
                Controller += "/" + urlpath[2] + "/";
            else
                Controller += "/Index/";
            if (urlpath.Length >= 4)
                int.TryParse(urlpath[3], out pageIndex);
            if (urlpath.Length >= 5)
                int.TryParse(urlpath[4], out pageSize);
            int id = 6;
            while (urlpath.Length >= id)
            {
                sort += "/" + urlpath[id - 1];
                id++;
            }
            StringBuilder result = new StringBuilder();
            if (total > 50 && pageSize == 100)
                result.AppendLine(GeneralMethods.Pager(string.Empty, "100", true, "right"));
            else if (total > 50 && pageSize != 100)
                result.AppendLine(GeneralMethods.Pager(Controller + "1/100" + sort, "100", false, "right"));
            if (total > 25 && pageSize == 50)
                result.AppendLine(GeneralMethods.Pager(string.Empty, "50", true, "right"));
            else if (total > 25 && pageSize != 50)
                result.AppendLine(GeneralMethods.Pager(Controller + "1/50" + sort, "50", false, "right"));
            if (total > 10 && pageSize == 25)
                result.AppendLine(GeneralMethods.Pager(string.Empty, "25", true, "right"));
            else if (total > 10 && pageSize != 25)
                result.AppendLine(GeneralMethods.Pager(Controller + "1/25" + sort, "25", false, "right"));
            if (pageSize == 10)
                result.AppendLine(GeneralMethods.Pager(string.Empty, "10", true, "right"));
            else
                result.AppendLine(GeneralMethods.Pager(Controller + "1/10" + sort, "10", false, "right"));
            if (pageSize == 5)
                result.AppendLine(GeneralMethods.Pager(string.Empty, "5", true, "right"));
            else
                result.AppendLine(GeneralMethods.Pager(Controller + "1/5" + sort, "5", false, "right"));
            result.AppendLine("<div class='right'>"+CoreLanguage.pagerCount+"</div>");
            return result.ToString();
        }
    }
}

