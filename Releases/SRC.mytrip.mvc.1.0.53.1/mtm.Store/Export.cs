using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mtm.Core;
using System.Web;
using mtm.Store.Repository;
using System.Web.Mvc;
using mtm.Core.Settings;
using mtm.Store.Helpers;
using mtm.Core.Helpers;

namespace mtm.Store
{
    /// <summary>
    /// 
    /// </summary>
    public static class Export
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<object> MenuStore()
        {
            if (ModuleSetting.unlockStore())
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
                IStoreRepository ar = new IStoreRepository();
                TagBuilder article = new TagBuilder("a");
                article.MergeAttribute("href", "/Store/Index/1/10/0/0/1/Department");
                article.InnerHtml = ModuleSetting.nameStore();
                string _result = null;
                bool tab_category = false;
                int key = 1;
                foreach (var item in ar.department.GetAllDepartment(LocalisationSetting.culture()))
                {
                    TagBuilder a = new TagBuilder("a");
                    a.MergeAttribute("href", "/Store/Index/1/10/" + item.DepartmentId + "/0/1/" + item.Path);
                    a.InnerHtml = item.Title;
                    if (_result == null)
                        _result = a.ToString();
                    else
                        _result += "|" + a;
                    key++;
                }
                bool drop = true;
                if (key == 1)
                    drop = false;
                if (Controller == "Store")
                    tab_category = true;
                List<object> _a =new List<object> { ModuleSetting.partialMenuLogon(), ModuleSetting.partialMenuLogonWrap(), article.ToString(), _result, tab_category, false, true, drop};
                return _a;
            }
            else { return null; }
        }

        #region HomePage
        public static HtmlString HomePage(int categoryId, int line, int column, int content, int imgwidth, int style, bool viewtitle)
        {
            IStoreRepository ar = new IStoreRepository();
                int take = line * column;
                var articles = ar.product.GetProductForStore(LocalisationSetting.culture(), take);
                int _count = articles.Count();
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
                string _content = string.Empty;
                int _line2 = 0;
                string finaltr = string.Empty;
                string start = string.Empty;
                string end = string.Empty;
                string styletable = string.Empty;
                foreach (var article in articles)
                {
                    _content = ProductHelpers.ViewProduct(article, content, imgwidth);

                    int tr2 = 0;
                    int _line3 = 0;
                    result.AppendLine(GeneralMethods.StyleTable(column, style, tr, width, _content,
                        count, _count2, _line, _line2, out tr2, out _line3, out finaltr, out start, out end, out styletable));
                    tr = tr2;
                    _line2 = _line3;
                    count++;
                }
                if (tr > 0 && tr % 2 != 0)
                    result.AppendLine(finaltr);
                table.AddCssClass(styletable);
                table.InnerHtml = result.ToString();
                string LangName = "<a href=\"/Store/Index/1/10/0/0/1/Department\">" + ModuleSetting.nameStore() + "</a>";
                string _CategoryName = string.Empty;
                if (viewtitle)
                    _CategoryName = "<h3 class=\"title\">" + LangName + "</h3>";
                if (column > 0)
                    return new HtmlString(_CategoryName + start + table.ToString() + end);
                else
                    return null;
            
        }
        #endregion


        public static HtmlString mtmStoreExportAccordionSearch(this HtmlHelper html)
        {
            TagBuilder form = new TagBuilder("form");
            string url = (HttpContext.Current.Request.Path.ToString() == "/") ? "/Home/Index" : HttpContext.Current.Request.Path.ToString();
            form.MergeAttribute("action", string.Concat("/Store/Search?url=", url));
            form.MergeAttribute("method", "post");
            form.InnerHtml = "<div class='search'><input  type='submit' value='' class='_search' ></input></div><input name='search' type='text' value='' class='search' />";
            return new HtmlString(GeneralMethods.Accordion(
                ModuleSetting.partialAccordion(), html, 
                ModuleSetting.NameSearchPage(), form.ToString()));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static HtmlString mtmStoreExportAccordionStore(this HtmlHelper html)
        {
            if (ModuleSetting.unlockStore())
            {
                TagBuilder a_title = new TagBuilder("a");
                a_title.MergeAttribute("href", "/Store/Index/1/10/0/0/1/Department");
                a_title.InnerHtml = ModuleSetting.nameStore();
                TagBuilder ul = new TagBuilder("ul");
                IStoreRepository ar = new IStoreRepository();
                StringBuilder _result = new StringBuilder();
                int count = 0;
                foreach (var item in ar.department.GetAllDepartment(LocalisationSetting.culture()))
                {
                    TagBuilder li = new TagBuilder("li");
                    TagBuilder a = new TagBuilder("a");
                    a.MergeAttribute("href", "/Store/Index/1/10/" + item.DepartmentId + "/0/1/" + item.Path);
                    a.InnerHtml = item.Title;
                    li.InnerHtml = a.ToString();
                    _result.AppendLine(li.ToString());
                    count++;
                }
                ul.InnerHtml = _result.ToString(); bool cssclacc = false;
                if (count > 0)
                    cssclacc = true;
                return new HtmlString(GeneralMethods.Accordion(
                    ModuleSetting.partialAccordion(),
                    ModuleSetting.partialNoAccordion(), html, cssclacc, a_title.ToString(), ul.ToString()));
            }
            else { return null; }
        }
        public static HtmlString mtmStoreExportAccordionProducer(this HtmlHelper html)
        {
            if (ModuleSetting.unlockStore())
            {
                TagBuilder a_title = new TagBuilder("a");
                a_title.MergeAttribute("href", "/Store/Index/1/10/0/0/1/Producer");
                a_title.InnerHtml = ModuleSetting.nameProducer();
                TagBuilder ul = new TagBuilder("ul");
                IStoreRepository ar = new IStoreRepository();
                StringBuilder _result = new StringBuilder();
                int count = 0;
                foreach (var item in ar.producer.GetAllProducer(LocalisationSetting.culture()))
                {
                    TagBuilder li = new TagBuilder("li");
                    TagBuilder a = new TagBuilder("a");
                    a.MergeAttribute("href", "/Store/Index/1/10/0/" + item.ProducerId + "/1/" + item.Path);
                    a.InnerHtml = item.Title;
                    li.InnerHtml = a.ToString();
                    _result.AppendLine(li.ToString());
                    count++;
                }
                ul.InnerHtml = _result.ToString(); bool cssclacc = false;
                if (count > 0)
                    cssclacc = true;
                return new HtmlString(GeneralMethods.Accordion(
                    ModuleSetting.partialAccordion(),
                    ModuleSetting.partialNoAccordion(), html, cssclacc, a_title.ToString(), ul.ToString()));
            }
            else { return null; }
        }
        public static HtmlString mtmStoreExportAccordionCart(this HtmlHelper html)
        {
            if (ModuleSetting.unlockStore())
            {
                TagBuilder a_title = new TagBuilder("a");
                a_title.MergeAttribute("href", "/Store/Cart");
                a_title.InnerHtml = StoreLanguage.mycarttitle;
                return new HtmlString(GeneralMethods.Accordion(
                    ModuleSetting.partialAccordion(),
                    ModuleSetting.partialNoAccordion(), html, false, a_title.ToString(), null));
            }
            else { return null; }
        }
        public static HtmlString AnnounceCart()
        {
            if (ModuleSetting.unlockStore())
            {
                TagBuilder a_title = new TagBuilder("a");
                a_title.MergeAttribute("href", "/Store/Cart");
                a_title.InnerHtml = StoreLanguage.mycarttitle;
                return new HtmlString(GeneralMethods.ImageLink(null, "/Store/Cart", StoreLanguage.mycarttitle, null, "/images/cart.png", StoreLanguage.mycarttitle,67));
            }
            else { return null; }
        }

        public static HtmlString Manager()
        {
            TagBuilder li_votes = new TagBuilder("li");
            TagBuilder a_archive = new TagBuilder("a");
            a_archive.MergeAttribute("href", "/Store/Manager");
            a_archive.InnerHtml = StoreLanguage.manager;
            li_votes.InnerHtml = a_archive.ToString();
            HtmlString htmlresult = new HtmlString(li_votes.ToString());
            return htmlresult;
        }
        public static HtmlString Setting()
        {
            TagBuilder li = new TagBuilder("li");
            TagBuilder a = new TagBuilder("a");
            a.MergeAttribute("href", "/Store/Setting");
            a.InnerHtml = StoreLanguage.storeSetting;
            li.InnerHtml = a.ToString();
            HtmlString htmlresult = new HtmlString(li.ToString());
            return htmlresult;

        }
        public static List<SearchQuestion> Search(string search)
        {
            return SearchHelpers.Search(search);
        }
    }
}
