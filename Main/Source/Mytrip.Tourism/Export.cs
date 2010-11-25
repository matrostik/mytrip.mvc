using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mytrip.Mvc.Settings;
using System.Web;
using System.Web.Mvc;
using Mytrip.Tourism.Repository;
using Mytrip.Tourism.Helpers;

namespace Mytrip.Tourism
{
   public static class Export
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static HtmlString MenuTours()
        {
            if (ModuleSetting.unlockTours())
            {
                string[] urlpath = GeneralMethods.UrlDictionary(HttpContext.Current.Request.Path);
                string Controller = urlpath[1];
                string Action = "Index";
                if (urlpath.Length >= 3)
                    Action = urlpath[2];
                int id = -1;
                if (urlpath.Length >= 6)
                    int.TryParse(urlpath[5], out id);
                string param = string.Empty;
                if (urlpath.Length >= 7)
                    param = urlpath[6];
                IToursRepository ar = new IToursRepository();
                TagBuilder article = new TagBuilder("a");
                IDictionary<int, string> _result = new Dictionary<int, string>();
                bool tab_category = false;
                int key = 1;
                string merge = "/Tours/Index/1/10/0/Tours";
                string title = ModuleSetting.nameTours();
                foreach (var item in ar.category.GetAllCategory(LocalisationSetting.culture()))
                {
                    TagBuilder a = new TagBuilder("a");
                    a.MergeAttribute("href", "/Tours/Index/1/10/" + item.CategoryId + "/" + item.Path);
                    a.InnerHtml = item.Title;
                    _result.Add(key, a.ToString());
                    merge = "/Tours/Index/1/10/" + item.CategoryId + "/" + item.Path;
                    title = item.Title;
                    key++;
                }
                bool drop = true;
                if (key == 1)
                {
                    drop = false;
                    merge = "/Tours/Index/1/10/0/Tours";
                    title = ModuleSetting.nameTours();
                    article.MergeAttribute("href", merge);
                    article.InnerHtml = title;
                }
                else if (key == 2)
                {
                    drop = false;
                    _result = null;
                    article.MergeAttribute("href", merge);
                    article.InnerHtml = title;
                }
                else if (key > 2)
                {
                    merge = "/Tours/Index/1/10/0/Tours";
                    title = ModuleSetting.nameTours();
                }
                if (Controller == "Tours" && Action != "TourModule"&&Action!="OrderTour")
                    tab_category = true;
                return  new HtmlString(GeneralMethods.Menu(article.ToString(), _result, tab_category, false, true, drop));
            }
            else { return null; }
        }
        public static HtmlString MenuToursModule()
        {
            if (ModuleSetting.unlockTours())
            {
                string[] urlpath = GeneralMethods.UrlDictionary(HttpContext.Current.Request.Path);
                string Controller = urlpath[1];
                string Action = "Index";
                if (urlpath.Length >= 3)
                    Action = urlpath[2];
                int id = -1;
                if (urlpath.Length >= 6)
                    int.TryParse(urlpath[5], out id);
                string param = string.Empty;
                if (urlpath.Length >= 7)
                    param = urlpath[6];
                TagBuilder article = new TagBuilder("a");
                bool tab_category = false;
                string merge = "/Tours/TourModule";
                string title = ToursLanguage.searchtour;
                article.MergeAttribute("href", merge);
                article.InnerHtml = title;
                if (Controller == "Tours" && Action == "TourModule")
                    tab_category = true;
                return new HtmlString(GeneralMethods.Menu(article.ToString(), null, tab_category, false, true, false));
            }
            else { return null; }
        }
        public static HtmlString MenuToursOrder()
        {
            if (ModuleSetting.unlockTours())
            {
                string[] urlpath = GeneralMethods.UrlDictionary(HttpContext.Current.Request.Path);
                string Controller = urlpath[1];
                string Action = "Index";
                if (urlpath.Length >= 3)
                    Action = urlpath[2];
                int id = -1;
                if (urlpath.Length >= 6)
                    int.TryParse(urlpath[5], out id);
                string param = string.Empty;
                if (urlpath.Length >= 7)
                    param = urlpath[6];
                TagBuilder article = new TagBuilder("a");
                bool tab_category = false;
                string merge = "/Tours/OrderTour";
                string title = ToursLanguage.ordertour;
                article.MergeAttribute("href", merge);
                article.InnerHtml = title;
                if (Controller == "Tours" && Action == "OrderTour")
                    tab_category = true;
                return new HtmlString(GeneralMethods.Menu(article.ToString(), null, tab_category, false, true, false));
            }
            else { return null; }
        }
        #region HomePage
        public static HtmlString HomePage(int categoryId, int line, int column, int content, int imgwidth, int style, bool viewtitle)
        {
            IToursRepository ar = new IToursRepository();
            int take = line * column;
            var x = ar.tours.GetAllTours(LocalisationSetting.culture(),take);
            int _count = x.Count();
            if (column > _count)
                column = _count;
            int _count2 = 0;
            int _line = 1;
            if (_count > column)
            {
                Math.DivRem(_count, column, out _count2);
                _line = (int)Math.Ceiling((double)_count / column);
            }
            int count = 1;
            int tr = 0;
            int width = 100;
            if (column > 0)
                width = 100 / column;
            StringBuilder result = new StringBuilder();
            TagBuilder table = new TagBuilder("table");
            //string _content = string.Empty;
            int _line2 = 0;
            string finaltr = string.Empty;
            string start = string.Empty;
            string end = string.Empty;
            string styletable = string.Empty;
            foreach (var item in x)
            {
                StringBuilder _content = new StringBuilder();
                _content.Append(GeneralMethods.ImageForAbstract(item.Imige, ModuleSetting.widthImgTours()));
                _content.Append("<h3 class='title'><a href='/Tours/View/" + item.TourId + "/" + item.Path + "'>" + item.Title + "</a></h3>");
                var date = item.StopDate - item.StartDate;
                _content.Append("<b>" + string.Format(ToursLanguage.DateTour, string.Format("{0:dd MMMM yyyy}", item.StartDate), string.Format("{0:dd MMMM yyyy}", item.StopDate), date.Days + 1) + "</b><br/>");
                _content.Append("<b class='sale'>" + string.Format(ToursLanguage.priceMin, MoneyHelpers.ConvertMoney(item.MoneyId, item.MinPrice)) + "</b><br/>");
                if (ModuleSetting.viewDescription())
                    _content.Append(item.Body + "<br/>");
                if ( item.mytrip_tourscategory.SubCategoryId == 0)
                {
                    _content.Append(ToursLanguage.category + ": <a href='/Tours/Index/1/10/" + item.CategoryId + "/" + item.mytrip_tourscategory.Path + "'>" + item.mytrip_tourscategory.Title + "</a><br/>");
                }
                else if (item.mytrip_tourscategory.SubCategoryId > 0)
                {
                    _content.Append(ToursLanguage.category + ": <a href='/Tours/Index/1/10/" + item.mytrip_tourscategory.SubCategoryId + "/" + item.mytrip_tourscategory.mytrip_tourscategory2.Path + "'>" + item.mytrip_tourscategory.mytrip_tourscategory2.Title + "</a><br/>");
                    _content.Append(ToursLanguage.subcategory + ": <a href='/Tours/Index/1/10/" + item.CategoryId + "/" + item.mytrip_tourscategory.Path + "'>" + item.mytrip_tourscategory.Title + "</a><br/>");
                }
                int tr2 = 0;
                int _line3 = 0;
                result.AppendLine(GeneralMethods.StyleTable(column, ModuleSetting.styleTours(), tr, width, _content.ToString(),
                    count, _count2, _line, _line2, out tr2, out _line3, out finaltr, out start, out end, out styletable));
                tr = tr2;
                _line2 = _line3;
                count++;
            }
            if (tr > 0 && tr % 2 != 0)
                result.AppendLine(finaltr);
            table.AddCssClass(styletable);
            table.InnerHtml = result.ToString();
            string LangName = "<a href=\"/Tours/Index/1/10/0/Tours\">" + ModuleSetting.nameTours() + "</a>";
            string _CategoryName = string.Empty;
            if (viewtitle)
                _CategoryName = "<h3 class=\"title\">" + LangName + "</h3>";
            if (column > 0)
                return new HtmlString(_CategoryName + start + table.ToString() + end);
            else
                return null;

        }
        #endregion
    }
}
