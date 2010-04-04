using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using Mytrip.Articles.Repository;

namespace Mytrip.Articles.ArticlesCache
{
    public class ArticleCache
    {
        IArticleRepository _repos;
        public IArticleRepository repos
        {
            get
            {
                if (_repos == null)
                    _repos = new IArticleRepository();
                return _repos;
            }
        }
        public string culture
        {
            get
            {
                return HttpContext.Current.Session["culture"].ToString();
            }
            set
            {
                HttpContext.Current.Session["culture"] = value;
            }
        }
        public string ArticleMenu()
        {
            if (ArticlesSetting.articles)
            {
                string urlPath = HttpContext.Current.Request.Path.ToString();
                int urlIndex = -1;
                string Controller = string.Empty;
                string Action = string.Empty;
                string id = "0";
                string param = string.Empty;
                urlPath = urlPath.Remove(0, 1);
                if (urlPath.IndexOf("/") != -1)
                    urlIndex = urlPath.IndexOf("/");
                if (urlIndex != -1)
                {
                    Controller = urlPath.Remove(urlIndex);
                    if (Controller == "Article")
                    {
                        urlPath = urlPath.Remove(0, urlIndex + 1);
                        if (urlPath.IndexOf("/") != -1)
                            urlIndex = urlPath.IndexOf("/");
                        if (urlIndex != -1)
                        {
                            Action = urlPath.Remove(urlIndex);
                            if (Action != "Profile")
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
                                            if (urlPath.IndexOf("/") != -1)
                                            {
                                                id = urlPath.Remove(urlIndex);
                                                param = urlPath.Remove(0, urlIndex + 1);
                                            }
                                            else
                                            { id = urlPath; }

                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                StringBuilder result = new StringBuilder();
                TagBuilder li_article = new TagBuilder("li");
                TagBuilder article = new TagBuilder("a");
                article.MergeAttribute("href", "/Article/Index/1/10/0/Articles");
                article.InnerHtml = ArticleLanguage.articles;
                TagBuilder right_menu = new TagBuilder("a");
                right_menu.AddCssClass("right_menu");
                TagBuilder ul = new TagBuilder("ul");
                TagBuilder li_category = new TagBuilder("li");
                StringBuilder _result = new StringBuilder();
                bool tab_category = false;
                foreach (var item in GetCategoriesNotInMenu())
                {
                    if (Controller == "Article")
                    {
                        if (item.CategoryId.ToString() == id)
                        { tab_category = true; }
                        else
                        {
                            foreach (var _item in item.mytrip_ArticlesCategory1)
                            {
                                if (_item.CategoryId.ToString() == id)
                                { tab_category = true; }
                            }
                        }
                    }
                    TagBuilder a = new TagBuilder("a");
                    a.MergeAttribute("href", "/Article/Index/1/10/" + item.CategoryId + "/" + item.Path);
                    a.InnerHtml = item.Title;
                    _result.AppendLine(a.ToString());
                }
                li_category.InnerHtml = _result.ToString();
                ul.InnerHtml = li_category.ToString();

                if (Controller == "Article")
                {
                    if (id == "0" && param == "Articles")
                        article.AddCssClass("menuvisible");
                    else if (tab_category)
                        article.AddCssClass("menuvisible");
                }
                if (ul.ToString() != "<ul><li></li></ul>")
                    li_article.InnerHtml = article.ToString() + right_menu.ToString() + ul.ToString();
                else
                    li_article.InnerHtml = article.ToString() + right_menu.ToString();
                result.AppendLine(li_article.ToString());
                return result.ToString();
            }
            else { return string.Empty; }
        }
        public string BlogMenu()
        {
            if (ArticlesSetting.blogs)
            {
                string urlPath = HttpContext.Current.Request.Path.ToString();
                int urlIndex = -1;
                string Controller = string.Empty;
                string Action = string.Empty;
                string id = "0";
                string param = string.Empty;
                urlPath = urlPath.Remove(0, 1);
                if (urlPath.IndexOf("/") != -1)
                    urlIndex = urlPath.IndexOf("/");
                if (urlIndex != -1)
                {
                    Controller = urlPath.Remove(urlIndex);
                    if (Controller == "Article")
                    {
                        urlPath = urlPath.Remove(0, urlIndex + 1);
                        if (urlPath.IndexOf("/") != -1)
                            urlIndex = urlPath.IndexOf("/");
                        if (urlIndex != -1)
                        {
                            Action = urlPath.Remove(urlIndex);
                            if (Action != "Profile")
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
                                            if (urlPath.IndexOf("/") != -1)
                                            {
                                                id = urlPath.Remove(urlIndex);
                                                param = urlPath.Remove(0, urlIndex + 1);
                                            }
                                            else
                                            { id = urlPath; }

                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                StringBuilder result = new StringBuilder();
                TagBuilder li_article = new TagBuilder("li");
                TagBuilder article = new TagBuilder("a");
                article.MergeAttribute("href", "/Article/Index/1/10/0/Blogs");
                article.InnerHtml = ArticleLanguage.blogs;
                TagBuilder right_menu = new TagBuilder("a");
                right_menu.AddCssClass("right_menu");
                TagBuilder ul = new TagBuilder("ul");
                TagBuilder li_category = new TagBuilder("li");
                StringBuilder _result = new StringBuilder();
                bool tab_category = false;
                foreach (var item in GetBlogs())
                {
                    if (Controller == "Article")
                    {
                        if (item.CategoryId.ToString() == id)
                        { tab_category = true; }
                        else
                        {
                            foreach (var _item in item.mytrip_ArticlesCategory1)
                            {
                                if (_item.CategoryId.ToString() == id)
                                { tab_category = true; }
                            }
                        }
                    }
                    TagBuilder a = new TagBuilder("a");
                    a.MergeAttribute("href", "/Article/Index/1/10/" + item.CategoryId + "/" + item.Path);
                    a.InnerHtml = item.Title;
                    _result.AppendLine(a.ToString());
                }
                li_category.InnerHtml = _result.ToString();
                ul.InnerHtml = li_category.ToString();

                if (Controller == "Article")
                {
                    if (id == "0" && param == "Blogs")
                        article.AddCssClass("menuvisible");
                    else if (tab_category)
                        article.AddCssClass("menuvisible");
                }
                if (ul.ToString() != "<ul><li></li></ul>")
                    li_article.InnerHtml = article.ToString() + right_menu.ToString() + ul.ToString();
                else
                    li_article.InnerHtml = article.ToString() + right_menu.ToString();
                result.AppendLine(li_article.ToString());
                return result.ToString();
            }
            else { return string.Empty; }
        }
        public string CategoryMenu()
        {
            if (ArticlesSetting.articles)
            {
                string urlPath = HttpContext.Current.Request.Path.ToString();
                int urlIndex = -1;
                string Controller = string.Empty;
                string Action = string.Empty;
                string id = "0";
                string param = string.Empty;
                urlPath = urlPath.Remove(0, 1);
                if (urlPath.IndexOf("/") != -1)
                    urlIndex = urlPath.IndexOf("/");
                if (urlIndex != -1)
                {
                    Controller = urlPath.Remove(urlIndex);
                    if (Controller == "Article")
                    {
                        urlPath = urlPath.Remove(0, urlIndex + 1);
                        if (urlPath.IndexOf("/") != -1)
                            urlIndex = urlPath.IndexOf("/");
                        if (urlIndex != -1)
                        {
                            Action = urlPath.Remove(urlIndex);
                            if (Action != "Profile")
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
                                            if (urlPath.IndexOf("/") != -1)
                                            {
                                                id = urlPath.Remove(urlIndex);
                                                param = urlPath.Remove(0, urlIndex + 1);
                                            }
                                            else
                                            { id = urlPath; }

                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                StringBuilder result = new StringBuilder();
                foreach (var __item in GetCategoriesForMenu())
                {
                    TagBuilder li_article = new TagBuilder("li");
                    TagBuilder article = new TagBuilder("a");
                    article.MergeAttribute("href", "/Article/Index/1/10/" + __item.CategoryId + "/" + __item.Path);
                    article.InnerHtml = __item.Title;
                    TagBuilder right_menu = new TagBuilder("a");
                    right_menu.AddCssClass("right_menu");
                    TagBuilder ul = new TagBuilder("ul");
                    TagBuilder li_category = new TagBuilder("li");
                    StringBuilder _result = new StringBuilder();
                    bool tab_category = false;
                    foreach (var item in __item.mytrip_ArticlesCategory1)
                    {
                        if (item.CategoryId != item.SubCategoryId && (item.AllCulture || item.Culture == HttpContext.Current.Session["culture"].ToString()))
                        {
                            if (Controller == "Article")
                            {
                                if (item.CategoryId.ToString() == id)
                                { tab_category = true; }
                                else
                                {
                                    foreach (var _item in item.mytrip_ArticlesCategory1)
                                    {
                                        if (_item.CategoryId.ToString() == id)
                                        { tab_category = true; }
                                    }
                                }
                            }
                            TagBuilder a = new TagBuilder("a");
                            a.MergeAttribute("href", "/Article/Index/1/10/" + item.CategoryId + "/" + item.Path);
                            a.InnerHtml = item.Title;
                            _result.AppendLine(a.ToString());
                        }
                    }
                    li_category.InnerHtml = _result.ToString();
                    ul.InnerHtml = li_category.ToString();

                    if (Controller == "Article")
                    {
                        if (__item.CategoryId.ToString() == id)
                            article.AddCssClass("menuvisible");
                        else if (tab_category)
                            article.AddCssClass("menuvisible");
                    }
                    if (ul.ToString() != "<ul><li></li></ul>")
                        li_article.InnerHtml = article.ToString() + right_menu.ToString() + ul.ToString();
                    else
                        li_article.InnerHtml = article.ToString() + right_menu.ToString();
                    result.AppendLine(li_article.ToString());
                }
                return result.ToString();
            }
            else { return string.Empty; }
        }
        public string AccordionArticle()
        {
            if (ArticlesSetting.articles)
            {
                StringBuilder result = new StringBuilder();
                TagBuilder div_accordion = new TagBuilder("div");
                div_accordion.AddCssClass("accordion");
                TagBuilder div_accordiontitle = new TagBuilder("div");
                div_accordiontitle.AddCssClass("accordiontitle");
                TagBuilder a_title = new TagBuilder("a");
                a_title.MergeAttribute("href", "/Article/Index/1/10/0/Articles");
                a_title.InnerHtml = ArticleLanguage.articles;
                div_accordiontitle.InnerHtml = a_title.ToString();
                TagBuilder div_accordioncontent = new TagBuilder("div");
                div_accordioncontent.AddCssClass("accordioncontent");
                TagBuilder ul = new TagBuilder("ul");
                StringBuilder _result = new StringBuilder();
                foreach (var item in GetCategoriesNotInMenu())
                {
                    TagBuilder li = new TagBuilder("li");
                    TagBuilder a = new TagBuilder("a");
                    a.MergeAttribute("href", "/Article/Index/1/10/" + item.CategoryId + "/" + item.Path);
                    a.InnerHtml = item.Title;
                    li.InnerHtml = a.ToString();
                    _result.AppendLine(li.ToString());
                }
                ul.InnerHtml = _result.ToString();
                div_accordioncontent.InnerHtml = ul.ToString();
                div_accordion.InnerHtml = div_accordiontitle.ToString() + div_accordioncontent.ToString();
                result.AppendLine(div_accordion.ToString());
                return result.ToString();
            }
            else { return string.Empty; }
        }
        public string AccordionBlogs()
        {
            if (ArticlesSetting.blogs)
            {
                StringBuilder result = new StringBuilder();
                TagBuilder div_accordion = new TagBuilder("div");
                div_accordion.AddCssClass("accordion");
                TagBuilder div_accordiontitle = new TagBuilder("div");
                div_accordiontitle.AddCssClass("accordiontitle");
                TagBuilder a_title = new TagBuilder("a");
                a_title.MergeAttribute("href", "/Article/Index/1/10/0/Blogs");
                a_title.InnerHtml = ArticleLanguage.blogs;
                div_accordiontitle.InnerHtml = a_title.ToString();
                TagBuilder div_accordioncontent = new TagBuilder("div");
                div_accordioncontent.AddCssClass("accordioncontent");
                TagBuilder ul = new TagBuilder("ul");
                StringBuilder _result = new StringBuilder();
                foreach (var item in GetBlogs())
                {
                    TagBuilder li = new TagBuilder("li");
                    TagBuilder a = new TagBuilder("a");
                    a.MergeAttribute("href", "/Article/Index/1/10/" + item.CategoryId + "/" + item.Path);
                    a.InnerHtml = item.Title;
                    li.InnerHtml = a.ToString();
                    _result.AppendLine(li.ToString());
                }
                ul.InnerHtml = _result.ToString();
                div_accordioncontent.InnerHtml = ul.ToString();
                div_accordion.InnerHtml = div_accordiontitle.ToString() + div_accordioncontent.ToString();
                result.AppendLine(div_accordion.ToString());
                return result.ToString();
            }
            else { return string.Empty; }
        }
        public string AccordionCategory()
        {
            if (ArticlesSetting.articles)
            {
                StringBuilder result = new StringBuilder();
                foreach (var _item in GetCategoriesForMenu())
                {
                    TagBuilder div_accordion = new TagBuilder("div");
                    div_accordion.AddCssClass("accordion");
                    TagBuilder div_accordiontitle = new TagBuilder("div");
                    div_accordiontitle.AddCssClass("accordiontitle");
                    TagBuilder a_title = new TagBuilder("a");
                    a_title.MergeAttribute("href", "/Article/Index/1/10/" + _item.CategoryId + "/" + _item.Path);
                    a_title.InnerHtml = _item.Title;
                    div_accordiontitle.InnerHtml = a_title.ToString();
                    TagBuilder div_accordioncontent = new TagBuilder("div");
                    div_accordioncontent.AddCssClass("accordioncontent");
                    TagBuilder ul = new TagBuilder("ul");
                    StringBuilder _result = new StringBuilder();
                    foreach (var item in _item.mytrip_ArticlesCategory1)
                    {
                        if (item.CategoryId != item.SubCategoryId && (item.AllCulture || item.Culture == HttpContext.Current.Session["culture"].ToString()))
                        {
                            TagBuilder li = new TagBuilder("li");
                            TagBuilder a = new TagBuilder("a");
                            a.MergeAttribute("href", "/Article/Index/1/10/" + item.CategoryId + "/" + item.Path);
                            a.InnerHtml = item.Title;
                            li.InnerHtml = a.ToString();
                            _result.AppendLine(li.ToString());
                        }
                    }
                    ul.InnerHtml = _result.ToString();
                    div_accordioncontent.InnerHtml = ul.ToString();
                    div_accordion.InnerHtml = div_accordiontitle.ToString() + div_accordioncontent.ToString();
                    result.AppendLine(div_accordion.ToString());
                }

                return result.ToString();
            }
            else { return string.Empty; }
        }
        public string AccordionTag()
        {
            if (ArticlesSetting.articles | ArticlesSetting.blogs)
            {
                StringBuilder result = new StringBuilder();
                TagBuilder div_accordion = new TagBuilder("div");
                div_accordion.AddCssClass("accordion");
                TagBuilder div_accordiontitle = new TagBuilder("div");
                div_accordiontitle.AddCssClass("accordiontitle");
                div_accordiontitle.InnerHtml = ArticleLanguage.tags1;
                TagBuilder div_accordioncontent = new TagBuilder("div");
                div_accordioncontent.AddCssClass("accordioncontent");
                div_accordioncontent.MergeAttribute("style", "padding:5px;");
                StringBuilder _result = new StringBuilder();
                bool tagstyle = true;
                string style = string.Empty;
                foreach (var item in GetAllTags())
                {
                    Random d = new Random();
                    int e = d.Next(10, 18);
                    if (tagstyle)
                    {
                        style = "font-size: " + e + "px; font-weight: bold;";
                        tagstyle = false;
                    }
                    else
                    {
                        style = "font-size: " + e + "px;";
                        tagstyle = true;
                    }
                    int count = item.mytrip_Articles.Where(x => x.Culture == HttpContext.Current.Session["culture"].ToString()).Count() + item.mytrip_Articles.Where(x => x.AllCulture == true).Count();
                    if (count > 0)
                    {
                        TagBuilder a = new TagBuilder("a");
                        a.MergeAttribute("href", "/Article/Index/1/10/" + item.TagId + "/" + item.Path);
                        a.MergeAttribute("style", style);
                        a.InnerHtml = item.TagName;
                        _result.AppendLine(a.ToString() + " ");
                    }
                }

                div_accordioncontent.InnerHtml = _result.ToString();
                div_accordion.InnerHtml = div_accordiontitle.ToString() + div_accordioncontent.ToString();
                result.AppendLine(div_accordion.ToString());
                return result.ToString();
            }
            else { return string.Empty; }
        }

        /****************************************/

        IQueryable<mytrip_ArticlesCategory> GetCategoriesNotInMenu()
        {

            return GetObjectFromCache<IQueryable<mytrip_ArticlesCategory>>(
               "GetCategoriesNotInMenu" + culture,
               () => repos.category.GetCategoriesNotInMenu(culture),
               (c, k, o) => c.Add(k, o, null, Cache.NoAbsoluteExpiration, TimeSpan.FromHours(24), CacheItemPriority.Normal, null)
            );
        }
        IQueryable<mytrip_ArticlesCategory> GetBlogs()
        {

            return GetObjectFromCache<IQueryable<mytrip_ArticlesCategory>>(
               "GetBlogs" + culture,
               () => repos.category.GetBlogs(culture),
               (c, k, o) => c.Add(k, o, null, Cache.NoAbsoluteExpiration, TimeSpan.FromHours(24), CacheItemPriority.Normal, null)
            );
        }
        IQueryable<mytrip_ArticlesCategory> GetCategoriesForMenu()
        {

            return GetObjectFromCache<IQueryable<mytrip_ArticlesCategory>>(
               "GetCategoriesForMenu" + culture,
               () => repos.category.GetCategoriesForMenu(culture),
               (c, k, o) => c.Add(k, o, null, Cache.NoAbsoluteExpiration, TimeSpan.FromHours(24), CacheItemPriority.Normal, null)
            );
        }
        IQueryable<mytrip_ArticlesTag> GetAllTags()
        {

            return GetObjectFromCache<IQueryable<mytrip_ArticlesTag>>(
               "GetAllTags",
               () => repos.article.GetAllTags(),
               (c, k, o) => c.Add(k, o, null, Cache.NoAbsoluteExpiration, TimeSpan.FromHours(24), CacheItemPriority.Normal, null)
            );
        }
        protected T GetObjectFromCache<T>(string key, Func<T> getObject, Action<Cache, string, T> addToCache) where T : class
        {
            T obj = HttpContext.Current.Cache.Get(key) as T;
            if (obj == null)
            {
                obj = getObject();
                addToCache(HttpContext.Current.Cache, key, obj);
            }
            return obj;
        }
    }
}
