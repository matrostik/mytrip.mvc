using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mtm.Core.Settings;
using System.Web;
using System.Web.Mvc;
using mtm.Tourism.Repository;
using mtm.Tourism.Helpers;
using mtm.Core.Helpers;

namespace mtm.Tourism
{
   public static class Export
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<object> MenuTours()
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
                int _id = -1;
                if (urlpath.Length >= 7)
                    int.TryParse(urlpath[6], out _id);
                string param = string.Empty;
                if (urlpath.Length >= 8)
                    param = urlpath[7];
                IToursRepository ar = new IToursRepository();
                TagBuilder article = new TagBuilder("a");
                string _result = null;
                bool tab_category = false;
                int key = 1;
                string merge = "/Tours/Index/1/10/0/0/Tours";
                string title = ModuleSetting.nameTours();
                foreach (var item in ar.category.GetAllCategory(LocalisationSetting.culture()))
                {
                    TagBuilder a = new TagBuilder("a");
                    a.MergeAttribute("href", "/Tours/Index/1/10/" + item.CategoryId + "/0/" + item.Path);
                    a.InnerHtml = item.Title;
                    if (_result == null)
                        _result = a.ToString();
                    else
                        _result += "|" + a;
                    merge = "/Tours/Index/1/10/" + item.CategoryId + "/0/" + item.Path;
                    title = item.Title;
                    key++;
                }
                bool drop = true;
                if (key == 1)
                {
                    drop = false;
                    merge = "/Tours/Index/1/10/0/0/Tours";
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
                    merge = "/Tours/Index/1/10/0/0/Tours";
                    title = ModuleSetting.nameTours();
                    article.MergeAttribute("href", merge);
                    article.InnerHtml = title;
                }
                
                if (Controller == "Tours" && (param=="Tours"||id>0) && Action != "OrderTour")
                    tab_category = true;
                List<object> _a =new List<object> { ModuleSetting.partialMenuLogon(), ModuleSetting.partialMenuLogonWrap(), article.ToString(), _result, tab_category, false, true, drop};
                return _a;
            }
            else { return null; }
        }
        public static List<object> MenuToursOrder()
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
                string title = ModuleSetting.nameOrderTours();
                article.MergeAttribute("href", merge);
                article.InnerHtml = title;
                if (Controller == "Tours" && Action == "OrderTour")
                    tab_category = true;
                List<object> _a =new List<object> { ModuleSetting.partialMenuLogon(), ModuleSetting.partialMenuLogonWrap(), article.ToString(), null, tab_category, false, true, false};
                return _a;
            }
            else { return null; }
        }
        public static List<object> MenuToursCountry()
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
                int _id = -1;
                if (urlpath.Length >= 7)
                    int.TryParse(urlpath[6], out _id);
                string param = string.Empty;
                if (urlpath.Length >= 8)
                    param = urlpath[7];
                IToursRepository ar = new IToursRepository();
                TagBuilder article = new TagBuilder("a");
                string _result = null;
                bool tab_category = false;
                string merge = "/Tours/Index/1/10/0/0/Country";
                string title = ModuleSetting.nameCountry();
                article.MergeAttribute("href", merge);
                article.InnerHtml = title;
                bool drop = false;
                foreach (var item in ar.category.GetAllCountry(LocalisationSetting.culture()))
                {
                    TagBuilder a = new TagBuilder("a");
                    a.MergeAttribute("href", "/Tours/Index/1/10/0/" + item.CountryId + "/" + item.Path);
                    a.InnerHtml = item.Title;
                    if (_result == null)
                        _result = a.ToString();
                    else
                        _result += "|" + a; 
                    drop = true;
                }

                if (Controller == "Tours" && (param == "Country" || (id == 0 && _id > 0)) && Action != "OrderTour")
                    tab_category = true;
                List<object> _a = new List<object> { ModuleSetting.partialMenuLogon(), ModuleSetting.partialMenuLogonWrap(), article.ToString(), _result, tab_category, false, true, drop };
                return _a;
            }
            else { return null; }
        }
        public static HtmlString mtmTourismExportAccordionOrder(this HtmlHelper html)
        {
            if (ModuleSetting.unlockTours())
            {
                TagBuilder a_title = new TagBuilder("a");
                a_title.MergeAttribute("href", "/Tours/OrderTour");
                a_title.InnerHtml = ModuleSetting.nameOrderTours();
                return new HtmlString(GeneralMethods.Accordion(
                    ModuleSetting.partialAccordion(),
                    ModuleSetting.partialNoAccordion(), html, false, a_title.ToString(), null));
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
                _content.Append("<h3 class='title'><a href='/Tours/View/" + item.TourId + "/" + item.Path + "'>" + item.Title + "</a> (<a href='/Tours/Index/1/10/0/" + item.CountryId + "/" + item.mytrip_tourscountry.Path + "'>" + item.mytrip_tourscountry.Title + "</a>)</h3>");
                var date = item.StopDate - item.StartDate;
                _content.Append("<b>" + string.Format(ToursLanguage.DateTour, string.Format("{0:dd MMMM yyyy}", item.StartDate), string.Format("{0:dd MMMM yyyy}", item.StopDate), date.Days + 1) + "</b><br/>");
                _content.Append("<b class='sale'>" + string.Format(ToursLanguage.priceMin, MoneyHelpers.ConvertMoney(item.MoneyId, item.MinPrice)) + "</b><br/>");
                if (ModuleSetting.viewDescription())
                    _content.Append(item.Body + "<br/>");
                if ( item.mytrip_tourscategory.SubCategoryId == 0)
                {
                    _content.Append(ToursLanguage.category + ": <a href='/Tours/Index/1/10/" + item.CategoryId + "/0/" + item.mytrip_tourscategory.Path + "'>" + item.mytrip_tourscategory.Title + "</a><br/>");
                }
                else if (item.mytrip_tourscategory.SubCategoryId > 0)
                {
                    _content.Append(ToursLanguage.category + ": <a href='/Tours/Index/1/10/" + item.mytrip_tourscategory.SubCategoryId + "/0/" + item.mytrip_tourscategory.mytrip_tourscategory2.Path + "'>" + item.mytrip_tourscategory.mytrip_tourscategory2.Title + "</a><br/>");
                    _content.Append(ToursLanguage.subcategory + ": <a href='/Tours/Index/1/10/" + item.CategoryId + "/0/" + item.mytrip_tourscategory.Path + "'>" + item.mytrip_tourscategory.Title + "</a><br/>");
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
            string LangName = "<a href=\"/Tours/Index/1/10/0/0/Tours\">" + ModuleSetting.nameTours() + "</a>";
            string _CategoryName = string.Empty;
            if (viewtitle)
                _CategoryName = "<h3 class=\"title\">" + LangName + "</h3>";
            if (column > 0)
                return new HtmlString(_CategoryName + start + table.ToString() + end);
            else
                return null;

        }
        #endregion
        public static List<SearchQuestion> Search(string search)
        {
            return SearchHelpers.Search(search);
        }
        public static HtmlString Setting()
        {
            TagBuilder li = new TagBuilder("li");
            TagBuilder a = new TagBuilder("a");
            a.MergeAttribute("href", "/Tours/Setting");
            a.InnerHtml = ToursLanguage.setting;
            li.InnerHtml = a.ToString();
            HtmlString htmlresult = new HtmlString(li.ToString());
            return htmlresult;

        }
        /// <summary>
        /// Control panel link for Votes Manager
        /// </summary>
        /// <returns>HtmlString</returns>
        public static HtmlString Manager()
        {
            TagBuilder li_votes = new TagBuilder("li");
            TagBuilder a_archive = new TagBuilder("a");
            a_archive.MergeAttribute("href", "/Tours/Arhiv/1/10/0/0/Tours");
            a_archive.InnerHtml = ToursLanguage.arhiv;
            li_votes.InnerHtml = a_archive.ToString();
            HtmlString htmlresult = new HtmlString(li_votes.ToString());
            return htmlresult;
        }
    }
}
