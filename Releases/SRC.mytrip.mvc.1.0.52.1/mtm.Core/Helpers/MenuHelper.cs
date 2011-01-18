/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about mytrip.mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using System.Web;
using System.Web.Routing;
using mtm.Core.Settings;
using mtm.Core.Repository;

namespace mtm.Core.Helpers
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
                string[] urlpath = GeneralMethods.UrlDictionary(HttpContext.Current.Request.Path);
                string Controller = string.Empty;
                string Action = string.Empty;
                string id = string.Empty;
                if (urlpath.Length >= 2)
                    Controller = urlpath[1];
                if (urlpath.Length >= 3)
                    Action = urlpath[2];
                if (urlpath.Length >= 4)
                    id = urlpath[3];
                StringBuilder result = new StringBuilder();
                TagBuilder home = new TagBuilder("a");
                home.MergeAttribute("href", "/");
                home.InnerHtml = CoreSetting.NameHomePage();
                if (String.IsNullOrEmpty(Controller) || Controller == "/" || (Controller == "Home" && Action == "Index"))
                    result.Append(GeneralMethods.Menu(home.ToString(), null, true, false, true, false));
                else
                    result.Append(GeneralMethods.Menu(home.ToString(), null, false, false, true, false));
                IDictionary<string, object> _menu = (menu == null ? new RouteValueDictionary() : new RouteValueDictionary(menu));
                foreach (string key in _menu.Keys)
                {
                    result.Append(_menu[key].ToString());
                }
                ICoreRepository core = new ICoreRepository();
                var x = core.corePageRepo.GetPagesForMenu(LocalisationSetting.culture());
                foreach (var _x in x)
                    {
                        IDictionary<int, string> _result = new Dictionary<int, string>();
                        bool drop = false;
                        if (_x.mytrip_corepages1 != null)
                        {
                            int key = 1;
                            foreach (var __x in _x.mytrip_corepages1)
                            {
                                if (__x.AddMenu == true)
                                {
                                    drop = true;
                                    TagBuilder a = new TagBuilder("a");
                                    a.MergeAttribute("href", "/Home/Page/" + __x.PageId + "/" + __x.Path);
                                    a.InnerHtml = __x.Title;
                                    _result.Add(key, a.ToString());
                                    key++;
                                }
                            }
                        }
                        TagBuilder _a = new TagBuilder("a");
                        _a.MergeAttribute("href", "/Home/Page/" + _x.PageId + "/" + _x.Path);
                        _a.InnerHtml = _x.Title;
                        if (Action == "Page" && id == _x.PageId.ToString())
                            result.Append(GeneralMethods.Menu(_a.ToString(), _result, true, false, true, drop));
                        else
                            result.Append(GeneralMethods.Menu(_a.ToString(), _result, false, false, true, drop));
                    }                
                HtmlString htmlresult = new HtmlString(result.ToString());
                return htmlresult;
            }
            else
            {
                return null;
            }
        }
        public static HtmlString AccordionPage(this HtmlHelper html)
        {
            ICoreRepository core = new ICoreRepository();
            var x = core.corePageRepo.GetPagesForMenu(LocalisationSetting.culture());
                StringBuilder result = new StringBuilder();
                int category = 0;
                foreach (var _item in x)
                {
                    TagBuilder a_title = new TagBuilder("a");
                    a_title.MergeAttribute("href", "/Home/Page/" + _item.PageId + "/" + _item.Path);
                    a_title.InnerHtml = _item.Title;
                    TagBuilder ul = new TagBuilder("ul");
                    StringBuilder _result = new StringBuilder();
                    int count = 0;
                    foreach (var item in _item.mytrip_corepages1)
                    {
                        if (item.SubPagesId != 0 && (item.AllCulture || item.Culture.ToLower() == LocalisationSetting.culture()))
                        {
                            TagBuilder li = new TagBuilder("li");
                            TagBuilder a = new TagBuilder("a");
                            a.MergeAttribute("href", "/Home/Page/" + item.PageId + "/" + item.Path);
                            a.InnerHtml = item.Title;
                            li.InnerHtml = a.ToString();
                            _result.AppendLine(li.ToString());
                            count++;
                        }
                    }
                    ul.InnerHtml = _result.ToString();
                    bool cssclacc = false;
                    if (count > 0)
                        cssclacc = true;
                    result.AppendLine(GeneralMethods.Accordion(cssclacc, a_title.ToString(), ul.ToString()));
                    category++;
                }
                HtmlString htmlresult = new HtmlString(result.ToString());
                return htmlresult;
        }
    }
}
