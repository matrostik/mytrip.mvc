﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mytrip.Mvc;
using System.Web;
using Mytrip.Store.Repository;
using System.Web.Mvc;
using Mytrip.Mvc.Settings;

namespace Mytrip.Store
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
        public static HtmlString MenuStore()
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
                IDictionary<int, string> _result = new Dictionary<int, string>();
                bool tab_category = false;
                int key = 1;
                foreach (var item in ar.department.GetAllDepartment(LocalisationSetting.culture()))
                {
                    TagBuilder a = new TagBuilder("a");
                    a.MergeAttribute("href", "/Store/Index/1/10/" + item.DepartmentId + "/0/1/" + item.Path);
                    a.InnerHtml = item.Title;
                    _result.Add(key, a.ToString());
                    key++;
                }
                bool drop = true;
                if (key == 1)
                    drop = false;
                if (Controller == "Store")
                    tab_category = true;
                return new HtmlString(GeneralMethods.Menu(article.ToString(), _result, tab_category, false, true, drop));
            }
            else { return null; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static HtmlString AccordionStore()
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
                ul.InnerHtml = _result.ToString();bool cssclacc = false;
                if (count > 0)
                    cssclacc = true;
                return new HtmlString(GeneralMethods.Accordion(cssclacc, a_title.ToString(), ul.ToString()));
            }
            else { return null; }
        }
    }
}
