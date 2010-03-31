using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;
using Mytrip.Core.Models;
using System.Collections;
using Mytrip.Core.Helpers;
using Mytrip.Articles.Models;
using Mytrip.Core.Repository;

namespace Mytrip.Articles.Helpers
{
    public static class ArticlesHelper
    {
        public static string ArticleMenu(this HtmlHelper html)
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
                IArticleRepository ar = new IArticleRepository();
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
                foreach (var item in ar.category.GetCategoriesNotInMenu(HttpContext.Current.Session["culture"].ToString()))
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
        public static string BlogMenu(this HtmlHelper html)
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
                IArticleRepository ar = new IArticleRepository();
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
                foreach (var item in ar.category.GetBlogs(HttpContext.Current.Session["culture"].ToString()))
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
        public static string CategoryMenu(this HtmlHelper html)
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
                IArticleRepository ar = new IArticleRepository();
                StringBuilder result = new StringBuilder();
                foreach (var __item in ar.category.GetCategoriesForMenu(HttpContext.Current.Session["culture"].ToString()))
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
        public static string AccordionSearch(this HtmlHelper html, string text, string bottom)
        {
            StringBuilder result = new StringBuilder();
            TagBuilder div_accordion = new TagBuilder("div");
            div_accordion.AddCssClass("accordion");
            TagBuilder div_accordiontitle = new TagBuilder("div");
            div_accordiontitle.AddCssClass("accordiontitle");
            div_accordiontitle.InnerHtml = ArticleLanguage.search;
            TagBuilder div_accordioncontent = new TagBuilder("div");
            div_accordioncontent.AddCssClass("accordioncontent");
            div_accordioncontent.MergeAttribute("style", "padding:5px;");
            TagBuilder form = new TagBuilder("form");
            form.MergeAttribute("action", "/Article/Index");
            form.MergeAttribute("method", "post");
            form.InnerHtml = text + bottom;
            div_accordioncontent.InnerHtml = form.ToString();
            div_accordion.InnerHtml = div_accordiontitle.ToString() + div_accordioncontent.ToString();
            result.AppendLine(div_accordion.ToString());
            return result.ToString();
        }
        public static string AccordionSearch(this HtmlHelper html, string text, string bottom, string url)
        {
            StringBuilder result = new StringBuilder();
            TagBuilder div_accordion = new TagBuilder("div");
            div_accordion.AddCssClass("accordion");
            TagBuilder div_accordiontitle = new TagBuilder("div");
            div_accordiontitle.AddCssClass("accordiontitle");
            div_accordiontitle.InnerHtml = ArticleLanguage.search;
            TagBuilder div_accordioncontent = new TagBuilder("div");
            div_accordioncontent.AddCssClass("accordioncontent");
            div_accordioncontent.MergeAttribute("style", "padding:5px;");
            TagBuilder form = new TagBuilder("form");
            form.MergeAttribute("action", url);
            form.MergeAttribute("method", "post");
            form.InnerHtml = text + bottom;
            div_accordioncontent.InnerHtml = form.ToString();
            div_accordion.InnerHtml = div_accordiontitle.ToString() + div_accordioncontent.ToString();
            result.AppendLine(div_accordion.ToString());
            return result.ToString();
        }
        public static string AccordionArticle(this HtmlHelper html)
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
                IArticleRepository ar = new IArticleRepository();
                StringBuilder _result = new StringBuilder();
                foreach (var item in ar.category.GetCategoriesNotInMenu(HttpContext.Current.Session["culture"].ToString()))
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
        public static string AccordionBlogs(this HtmlHelper html)
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
                IArticleRepository ar = new IArticleRepository();
                StringBuilder _result = new StringBuilder();
                foreach (var item in ar.category.GetBlogs(HttpContext.Current.Session["culture"].ToString()))
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
        public static string AccordionCategory(this HtmlHelper html)
        {
            if (ArticlesSetting.articles)
            {
                IArticleRepository ar = new IArticleRepository();
                StringBuilder result = new StringBuilder();
                foreach (var _item in ar.category.GetCategoriesForMenu(HttpContext.Current.Session["culture"].ToString()))
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
        public static string AccordionTag(this HtmlHelper html)
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
                IArticleRepository ar = new IArticleRepository();
                StringBuilder _result = new StringBuilder();
                bool tagstyle = true;
                string style = string.Empty;
                foreach (var item in ar.article.GetAllTags())
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
        public static string ArticleRssLink(this HtmlHelper html, string title, string path, int id, int width)
        {
            if (path.StartsWith("(Search)"))
            { return string.Empty; }
            else
            {
                StringBuilder result = new StringBuilder();
                TagBuilder img = new TagBuilder("img");
                img.MergeAttribute("src", "/Content/images/rss.png");
                img.MergeAttribute("alt", "Rss " + path);
                img.MergeAttribute("style", "width:" + width + "px;border:0px;");
                TagBuilder a = new TagBuilder("a");
                if (id == 0)
                {
                    if (path == "Articles")
                    {
                        a.MergeAttribute("href", "/RssArticle/RssArticles");
                    }
                    else if (path == "Blogs")
                    {
                        a.MergeAttribute("href", "/RssArticle/RssBlogs");
                    }

                }
                else if (path.StartsWith("(Tag)")) { a.MergeAttribute("href", "/RssArticle/RssArticlesInTag/" + id + "/" + path + "/" + title); }
                else
                {
                    a.MergeAttribute("href", "/RssArticle/RssArticlesInCategory/" + id + "/" + path + "/" + title);
                }
                a.InnerHtml = img.ToString();
                result.AppendLine(a.ToString());
                return result.ToString();
            }
        }

        public static string EditorCategory(this HtmlHelper html, bool ShowAddCategory, bool ShowAddSubCategory, bool ShowAddArticle, bool ShowAddBlog, bool ShowAddPost, int id)
        {
            CategoryRepository cr = new CategoryRepository();
            mytrip_ArticlesCategory category = cr.GetCategory(id);
            StringBuilder result = new StringBuilder();
            if (isUserHasRights(category,true))
            {
                TagBuilder img = new TagBuilder("img");
                img.MergeAttribute("src", "/Content/images/add.png");
                img.MergeAttribute("style", "width:20px;border:0px;");
                if (ShowAddCategory)
                {
                    TagBuilder CreateCategoryP = new TagBuilder("div");
                    TagBuilder CreateCategory = new TagBuilder("a");
                    CreateCategory.MergeAttribute("href", "/Article/CreateCategory/0/Articles");
                    CreateCategory.InnerHtml = ArticleLanguage.add_category + " " + img.ToString();
                    CreateCategoryP.InnerHtml = CreateCategory.ToString();
                    result.AppendLine(CreateCategoryP.ToString());
                }
                if (ShowAddSubCategory)
                {
                    TagBuilder CreateSubCategoryP = new TagBuilder("div");
                    TagBuilder CreateSubCategory = new TagBuilder("a");
                    CreateSubCategory.MergeAttribute("href", "/Article/CreateCategory/" + id + "/" + category.Path);
                    CreateSubCategory.InnerHtml = ArticleLanguage.add_subcategory + " " + img.ToString();
                    CreateSubCategoryP.InnerHtml = CreateSubCategory.ToString();
                    result.AppendLine(CreateSubCategoryP.ToString());
                }
                if (ShowAddArticle)
                {
                    TagBuilder CreateArticleP = new TagBuilder("div");
                    TagBuilder CreateArticle = new TagBuilder("a");
                    if(id!=0)
                    CreateArticle.MergeAttribute("href", "/Article/Create/" + id + "/" + category.Path);
                    else
                        CreateArticle.MergeAttribute("href", "/Article/Create/0/Articles");
                    CreateArticle.InnerHtml = ArticleLanguage.add_article + " " + img.ToString();
                    CreateArticleP.InnerHtml = CreateArticle.ToString();
                    result.AppendLine(CreateArticleP.ToString());
                }
                if (ShowAddBlog)
                {
                    TagBuilder CreateSubCategoryP = new TagBuilder("div");
                    TagBuilder CreateSubCategory = new TagBuilder("a");
                    CreateSubCategory.MergeAttribute("href", "/Article/CreateCategory/" + id + "/" + category.Path);
                    CreateSubCategory.InnerHtml = ArticleLanguage.add_topic + " " + img.ToString();
                    CreateSubCategoryP.InnerHtml = CreateSubCategory.ToString();
                    result.AppendLine(CreateSubCategoryP.ToString());
                }
                if (ShowAddPost)
                {
                    TagBuilder CreateArticleP = new TagBuilder("div");
                    TagBuilder CreateArticle = new TagBuilder("a");
                    CreateArticle.MergeAttribute("href", "/Article/Create/" + id + "/" + category.Path);
                    CreateArticle.InnerHtml = ArticleLanguage.add_post + " " + img.ToString();
                    CreateArticleP.InnerHtml = CreateArticle.ToString();
                    result.AppendLine(CreateArticleP.ToString());
                }
            }
            return result.ToString();
        }

        public static string EditDeleteCategory(this HtmlHelper html, bool ShowEditDelete, bool ShowEditDeleteBlog, int id, string path)
        {
            var obj = new Object();
            if (path.StartsWith("(Tag)"))
            {
                ArticleRepository ar = new ArticleRepository();
                obj = ar.GetTag(id);
            }
            else
            {
                CategoryRepository cr = new CategoryRepository();
                obj = cr.GetCategory(id);
            }
            
            StringBuilder result = new StringBuilder();
            if (ShowEditDelete || ShowEditDeleteBlog)
            {
                if (isUserHasRights(obj,false))
                {
                    TagBuilder imgEdit = new TagBuilder("img");
                    imgEdit.MergeAttribute("src", "/Content/images/edite.png");
                    imgEdit.MergeAttribute("title", ArticleLanguage.edit);
                    imgEdit.MergeAttribute("style", "width:14px;border:0px;");
                    TagBuilder imgDelete = new TagBuilder("img");
                    imgDelete.MergeAttribute("src", "/Content/images/delete.png");
                    imgDelete.MergeAttribute("title", ArticleLanguage.delete);
                    imgDelete.MergeAttribute("style", "width:14px;border:0px;");
                    TagBuilder EditCategory = new TagBuilder("a");
                    EditCategory.MergeAttribute("href", "/Article/EditCategory/" + id + "/" + path);
                    EditCategory.InnerHtml = imgEdit.ToString();
                    result.AppendLine(EditCategory.ToString());
                    TagBuilder DeleteCategory = new TagBuilder("a");
                    DeleteCategory.MergeAttribute("href", "/Article/DeleteCategory/" + id + "/" + path);
                    DeleteCategory.MergeAttribute("onclick", "return confirm ('" + ArticleLanguage.are_you_sure + "');");
                    DeleteCategory.InnerHtml = imgDelete.ToString();
                    result.AppendLine(DeleteCategory.ToString());
                }
            }
            return result.ToString();
        }

        public static string ParrentCategory(this HtmlHelper html, mytrip_ArticlesCategory parentCat, string path)
        {
            StringBuilder result = new StringBuilder();
            if (parentCat.CategoryId != -1 && parentCat.Path != path)
            {
                TagBuilder h2 = new TagBuilder("h2");
                TagBuilder a = new TagBuilder("a");
                a.MergeAttribute("href", "/Article/Index/1/10/" + parentCat.CategoryId + "/" + parentCat.Path);
                a.InnerHtml = parentCat.Title;
                h2.InnerHtml = a.ToString();
                result.AppendLine(h2.ToString());
            }
            return result.ToString();
        }

        public static string ShowDetailsBlog(this HtmlHelper html, bool showDetailsBlog, mytrip_ArticlesCategory parentCat)
        {
            if (showDetailsBlog)
            {
                StringBuilder result = new StringBuilder();
                TagBuilder table = new TagBuilder("table");
                table.MergeAttribute("style", "border:0px;");
                TagBuilder tr = new TagBuilder("tr");
                tr.MergeAttribute("style", "border:0px;");
                TagBuilder td = new TagBuilder("td");
                td.MergeAttribute("style", "border:0px;");
                result.AppendLine("<div style='position: relative; float: right'>" + AvatarHelper.Avatar(html, parentCat.UserEmail) + "</div>");
                result.AppendLine(ArticleLanguage.author + ": " + parentCat.UserName + "<br/>");
                result.AppendLine(ArticleLanguage.views + ": " + parentCat.Views + "<br/>");
                result.AppendLine(ArticleLanguage.posts + ": " + CountPosts(parentCat) + "<br/>");
                result.AppendLine(ArticleLanguage.create_date + ": " + String.Format("{0:dd MMMM yy}", parentCat.CreateDate));
                td.InnerHtml = result.ToString();
                tr.InnerHtml = td.ToString();
                table.InnerHtml = tr.ToString();
                return table.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        public static int CountPosts(mytrip_ArticlesCategory category)
        {
            int count = category.mytrip_Articles.Count();
            foreach (mytrip_ArticlesCategory cat in category.mytrip_ArticlesCategory1)
            {
                if (cat.CategoryId != cat.SubCategoryId)
                {
                    count += cat.mytrip_Articles.Count();
                }
            }
            return count;
        }

        public static string ShowArticle(this HtmlHelper html, mytrip_Articles article)
        {
            RoleRepository db = new RoleRepository();
            StringBuilder result = new StringBuilder();
            TagBuilder h2 = new TagBuilder("h2");
            if (isUserHasRights(article,false))
            {
                TagBuilder imgEdit = new TagBuilder("img");
                imgEdit.MergeAttribute("src", "/Content/images/edite.png");
                imgEdit.MergeAttribute("title", ArticleLanguage.edit);
                imgEdit.MergeAttribute("style", "width:14px;border:0px;");
                TagBuilder imgDelete = new TagBuilder("img");
                imgDelete.MergeAttribute("src", "/Content/images/delete.png");
                imgDelete.MergeAttribute("title", ArticleLanguage.delete);
                imgDelete.MergeAttribute("style", "width:14px;border:0px;");
                TagBuilder EditCategory = new TagBuilder("a");
                EditCategory.MergeAttribute("href", "/Article/Edit/" + article.ArticleId + "/" + article.Path);
                EditCategory.InnerHtml = imgEdit.ToString();
                TagBuilder DeleteCategory = new TagBuilder("a");
                DeleteCategory.MergeAttribute("href", "/Article/Delete/" + article.ArticleId + "/" + article.Path);
                DeleteCategory.MergeAttribute("onclick", "return confirm ('" + ArticleLanguage.are_you_sure + "');");
                DeleteCategory.InnerHtml = imgDelete.ToString();
                h2.InnerHtml += EditCategory.ToString() + " " + DeleteCategory.ToString() + " ";
            }
            h2.InnerHtml += article.Title;
            result.AppendLine(h2.ToString() + article.Body);
            return result.ToString();
        }

        public static string ShowArticleTags(this HtmlHelper html, mytrip_Articles article)
        {
            StringBuilder result = new StringBuilder();
            foreach (var tag in article.mytrip_ArticlesTag)
            {
                TagBuilder tagLink = new TagBuilder("a");
                tagLink.MergeAttribute("href", "/Article/Index/1/10/" + tag.TagId + "/" + tag.Path);
                tagLink.SetInnerText(tag.TagName);
                result.AppendLine(tagLink.ToString());
            }
            return result.ToString();
        }

        public static string ShowComments(this HtmlHelper html, mytrip_Articles article, IQueryable<mytrip_ArticlesComments> comments)
        {
            StringBuilder result = new StringBuilder();
            #region Comments title
            if (!article.ApprovedComment)
            {
                if (isUserHasRights(article,false))
                {
                    TagBuilder imgOpen = new TagBuilder("img");
                    imgOpen.MergeAttribute("src", "/Content/images/approved.png");
                    imgOpen.MergeAttribute("title", ArticleLanguage.open_comments);
                    imgOpen.MergeAttribute("style", "width:14px;border:0px;");
                    TagBuilder OpenComments = new TagBuilder("a");
                    OpenComments.MergeAttribute("href", "/Article/OpenComments/" + article.ArticleId + "/" + article.Path);
                    OpenComments.InnerHtml = imgOpen.ToString();
                    result.AppendLine(OpenComments.ToString() + " ");
                }
                result.AppendLine(ArticleLanguage.comments_closed);
            }
            else if (comments.Count() != 0)
            {
                if (isUserHasRights(article,false))
                {
                    TagBuilder imgClose = new TagBuilder("img");
                    imgClose.MergeAttribute("src", "/Content/images/noapproved.png");
                    imgClose.MergeAttribute("title", ArticleLanguage.close_comments);
                    imgClose.MergeAttribute("style", "width:14px;border:0px;");
                    TagBuilder CloseComments = new TagBuilder("a");
                    CloseComments.MergeAttribute("href", "/Article/CloseComments/" + article.ArticleId + "/" + article.Path);
                    CloseComments.InnerHtml = imgClose.ToString();
                    result.AppendLine(CloseComments.ToString() + " ");
                }
                result.AppendLine(ArticleLanguage.comments);
            }
            else if (comments.Count() == 0)
            {
                result.AppendLine(ArticleLanguage.no_comments_yet + " ");
            }
            #endregion
            #region Comments
            int count = 1;
            foreach (var comment in comments)
            {
                TagBuilder fieldset = new TagBuilder("fieldset");
                fieldset.MergeAttribute("style", "padding: 2px");
                TagBuilder legend = new TagBuilder("legend");
                if (isUserHasRights(comment,false))
                {
                    TagBuilder imgEdit = new TagBuilder("img");
                    imgEdit.MergeAttribute("src", "/Content/images/edite.png");
                    imgEdit.MergeAttribute("title", ArticleLanguage.edit_comment);
                    imgEdit.MergeAttribute("style", "width:14px;border:0px;");
                    TagBuilder imgDelete = new TagBuilder("img");
                    imgDelete.MergeAttribute("src", "/Content/images/delete.png");
                    imgDelete.MergeAttribute("title", ArticleLanguage.delete);
                    imgDelete.MergeAttribute("style", "width:14px;border:0px;");
                    TagBuilder editComment = new TagBuilder("a");
                    editComment.MergeAttribute("href", "/Article/EditComment/" + comment.CommentId + "/" + comment.mytrip_Articles.Path);
                    editComment.InnerHtml = imgEdit.ToString();
                    TagBuilder deleteComment = new TagBuilder("a");
                    deleteComment.MergeAttribute("href", "/Article/DeleteComment/" + comment.CommentId + "/" + comment.mytrip_Articles.Path);
                    deleteComment.MergeAttribute("onclick", "return confirm ('" + ArticleLanguage.are_you_sure + "');");
                    deleteComment.InnerHtml = imgDelete.ToString();
                    legend.InnerHtml += editComment.ToString() + " " + deleteComment.ToString() + " ";
                }
                legend.InnerHtml += "  #" + count + " " + comment.UserName + " " + comment.CreateDate.ToString("dd MMMM yy HH:mm");
                TagBuilder divGravatar = new TagBuilder("div");
                divGravatar.MergeAttribute("style", "position: relative; margin-top: -10px; float: right");
                divGravatar.InnerHtml = AvatarHelper.Avatar(html, comment.UserEmail);
                fieldset.InnerHtml = legend.ToString() + comment.Body + divGravatar.ToString();
                result.AppendLine(fieldset.ToString());
                count++;
            }
            #endregion
            return result.ToString();
        }

        /// <summary>
        /// Method to check rights of the current user
        /// </summary>
        /// <param name="obj">Rights for</param>
        /// <returns>Bool</returns>
        private static bool isUserHasRights(object obj,bool forAdd)
        {
            string categoryUserName = "";
            string category2UserName = "";
            string articleUserName = "";
            bool isBlog=true;
            RoleRepository db = new RoleRepository();
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
                return false;
            if (db.IsUserInRoleOnline(ArticlesSetting.roleChiefEditor))
                return true;
            else
            {
                if (obj is mytrip_ArticlesCategory)
                {
                    mytrip_ArticlesCategory category = obj as mytrip_ArticlesCategory;
                    categoryUserName = category.UserName;
                    category2UserName = category.mytrip_ArticlesCategory2.UserName;
                    isBlog = category.mytrip_ArticlesCategory2.Blog;
                }
                else if (obj is mytrip_Articles)
                {
                    mytrip_Articles article = obj as mytrip_Articles;
                    articleUserName = article.UserName;
                    categoryUserName = article.mytrip_ArticlesCategory.UserName;
                    category2UserName = article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.UserName;
                    isBlog = article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog;
                }
                else if (obj is mytrip_ArticlesComments)
                {
                    mytrip_ArticlesComments comment = obj as mytrip_ArticlesComments;
                    if (!comment.IsAnonym && HttpContext.Current.User.Identity.Name == comment.UserName)
                        return true;
                    articleUserName = comment.mytrip_Articles.UserName;
                    categoryUserName = comment.mytrip_Articles.mytrip_ArticlesCategory.UserName;
                    category2UserName = comment.mytrip_Articles.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.UserName;
                    isBlog = comment.mytrip_Articles.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog;
                }
                else if (obj is mytrip_ArticlesTag)
                {
                    forAdd = true;
                    isBlog = false;
                }
                bool articleEditor = db.IsUserInRoleOnline(ArticlesSetting.roleArticleEditor);
                if (db.IsUserInRoleOnline(ArticlesSetting.roleBlogger) || articleEditor)
                {
                    if (HttpContext.Current.User.Identity.Name == articleUserName || HttpContext.Current.User.Identity.Name == categoryUserName || HttpContext.Current.User.Identity.Name == category2UserName)
                        return true;
                    if (forAdd && articleEditor&&(!isBlog || obj == null))
                        return true;
                }
            }
            return false;
        }

        public static string ListCategories(this HtmlHelper html, IQueryable<mytrip_ArticlesCategory> categories, string path)
        {
            StringBuilder result = new StringBuilder();
            foreach (var category in categories)
            {
                int count = CountPosts(category);
                if (category.Blog)
                {
                    TagBuilder tr = new TagBuilder("tr");
                    TagBuilder td_first = new TagBuilder("td");
                    if (isUserHasRights(category,false))
                    {
                        TagBuilder imgEdit = new TagBuilder("img");
                        imgEdit.MergeAttribute("src", "/Content/images/edite.png");
                        imgEdit.MergeAttribute("title", ArticleLanguage.edit);
                        imgEdit.MergeAttribute("style", "width:14px;border:0px;");
                        TagBuilder imgDelete = new TagBuilder("img");
                        imgDelete.MergeAttribute("src", "/Content/images/delete.png");
                        imgDelete.MergeAttribute("title", ArticleLanguage.delete);
                        imgDelete.MergeAttribute("style", "width:14px;border:0px;");
                        TagBuilder EditCategory = new TagBuilder("a");
                        EditCategory.MergeAttribute("href", "/Article/EditCategory/" + category.CategoryId + "/" + category.Path);
                        EditCategory.InnerHtml = imgEdit.ToString();
                        TagBuilder DeleteCategory = new TagBuilder("a");
                        DeleteCategory.MergeAttribute("href", "/Article/DeleteCategory/" + category.CategoryId + "/" + category.Path);
                        DeleteCategory.MergeAttribute("onclick", "return confirm ('" + ArticleLanguage.are_you_sure + "');");
                        DeleteCategory.InnerHtml = imgDelete.ToString();
                        td_first.InnerHtml = EditCategory.ToString() + " " + DeleteCategory.ToString() + " ";
                    }
                    TagBuilder a_category = new TagBuilder("a");
                    a_category.MergeAttribute("href", "/Article/Index/1/10/" + category.CategoryId + "/" + category.Path);
                    a_category.InnerHtml = category.Title;
                    td_first.InnerHtml += a_category.ToString();
                    string avatar = AvatarHelper.Avatar(html, category.UserEmail);
                    string avatar2 = string.Empty;
                    if (!String.IsNullOrEmpty(avatar))
                        avatar2 = "<div style='position: relative; float: right'>" + avatar + "</div>";
                    td_first.InnerHtml = avatar2 + "<h3>" + td_first.InnerHtml + "</h3>";
                    TagBuilder td_last = new TagBuilder("td");
                    td_last.MergeAttribute("style", "width:170px;");
                    td_last.InnerHtml += ArticleLanguage.author + ": " + category.UserName + "<br/>";
                    td_last.InnerHtml += ArticleLanguage.views + ": " + category.Views + "<br/>";
                    td_last.InnerHtml += ArticleLanguage.posts + ": " + count + "<br/>";
                    td_last.InnerHtml += ArticleLanguage.create_date + ": " + String.Format("{0:dd MMMM yyyy}", category.CreateDate);
                    tr.InnerHtml = td_last.ToString() + td_first.ToString();
                    result.AppendLine(tr.ToString());
                }
                else
                {
                    TagBuilder h4 = new TagBuilder("h4");
                    if (isUserHasRights(category,false) || (category.mytrip_ArticlesCategory2.Blog && isUserHasRights(category,false)))
                    {
                        TagBuilder imgEdit = new TagBuilder("img");
                        imgEdit.MergeAttribute("src", "/Content/images/edite.png");
                        imgEdit.MergeAttribute("title", ArticleLanguage.edit);
                        imgEdit.MergeAttribute("style", "width:14px;border:0px;");
                        TagBuilder imgDelete = new TagBuilder("img");
                        imgDelete.MergeAttribute("src", "/Content/images/delete.png");
                        imgDelete.MergeAttribute("title", ArticleLanguage.delete);
                        imgDelete.MergeAttribute("style", "width:14px;border:0px;");
                        TagBuilder EditCategory = new TagBuilder("a");
                        EditCategory.MergeAttribute("href", "/Article/EditCategory/" + category.CategoryId + "/" + category.Path);
                        EditCategory.InnerHtml = imgEdit.ToString();
                        TagBuilder DeleteCategory = new TagBuilder("a");
                        DeleteCategory.MergeAttribute("href", "/Article/DeleteCategory/" + category.CategoryId + "/" + category.Path);
                        DeleteCategory.MergeAttribute("onclick", "return confirm ('" + ArticleLanguage.are_you_sure + "');");
                        DeleteCategory.InnerHtml = imgDelete.ToString();
                        h4.InnerHtml = EditCategory.ToString() + " " + DeleteCategory.ToString() + " ";
                    }
                    TagBuilder a_category = new TagBuilder("a");
                    a_category.MergeAttribute("href", "/Article/Index/1/10/" + category.CategoryId + "/" + category.Path);
                    if (category.mytrip_ArticlesCategory2.Blog)
                    {
                        a_category.InnerHtml = "Topic: " + category.Title;
                        h4.InnerHtml += a_category.ToString();
                        string blogdetails = "<br/><b style='font-size: 12px; font-weight: normal;'>" + ArticleLanguage.create_date + ": " + String.Format("{0:dd MMMM yyyy}", category.CreateDate) +
                            ", " + ArticleLanguage.views + ": " + category.Views + "," + ArticleLanguage.posts + ": " + count + "</b>";
                        h4.InnerHtml += blogdetails;
                    }
                    else
                    {
                        a_category.InnerHtml = category.Title;
                        h4.InnerHtml += a_category.ToString();
                    }
                    result.AppendLine(h4.ToString());
                }
            }
            if (path == "Blogs")
            {
                result.Insert(result.Length, "</table>").Insert(0, "<table>");
            }
            return result.ToString();
        }

        public static string ArticleRating(this HtmlHelper html, mytrip_Articles article, bool active)
        {
            StringBuilder result = new StringBuilder();
            if (article.ApprovedComment)
            {
                result.AppendLine(ArticleLanguage.total_votes + ": " + article.mytrip_ArticlesVotes.Count() + ". ");
                int rate = 0;
                while (rate < 5)
                {
                    double totalVote = (double)article.TotalVotes;
                    double rate12 = rate + 0.125;
                    double rate37 = rate + 0.375;
                    double rate62 = rate + 0.625;
                    double rate87 = rate + 0.875;
                    if (active && HttpContext.Current.User.Identity.IsAuthenticated)
                    {
                        TagBuilder input = new TagBuilder("input");
                        input.MergeAttribute("type", "submit");
                        input.MergeAttribute("value", "");
                        input.MergeAttribute("name", "input" + rate);
                        input.MergeAttribute("title",(rate+1).ToString());
                        if (totalVote > rate12 && totalVote < rate37)
                            input.MergeAttribute("style", "background:url(/Content/images/star25.png);width: 15px; height: 15px; border-width: 0px;cursor: hand");
                        if (totalVote > rate37 && totalVote < rate62)
                            input.MergeAttribute("style", "background:url(/Content/images/star50.png);width: 15px; height: 15px; border-width: 0px;cursor: hand");
                        if (totalVote > rate62 && totalVote < rate87)
                            input.MergeAttribute("style", "background:url(/Content/images/star75.png);width: 15px; height: 15px; border-width: 0px;cursor: hand");
                        if (totalVote < rate87)
                            input.MergeAttribute("style", "background:url(/Content/images/star100.png);width: 15px; height: 15px; border-width: 0px;cursor: hand");
                        if (totalVote > rate87)
                            input.MergeAttribute("style", "background:url(/Content/images/star.png);width: 15px; height: 15px; border-width: 0px;cursor: hand");
                        rate++;
                        result.AppendLine(input.ToString());
                    }
                    else
                    {
                        TagBuilder input = new TagBuilder("img");
                        if (totalVote > rate12 && totalVote < rate37)
                            input.MergeAttribute("src", "/Content/images/star25.png");
                        if (totalVote > rate37 && totalVote < rate62)
                            input.MergeAttribute("src", "/Content/images/star50.png");
                        if (totalVote > rate62 && totalVote < rate87)
                            input.MergeAttribute("src", "/Content/images/star75.png");
                        if (totalVote < rate87)
                            input.MergeAttribute("src", "/Content/images/star100.png");
                        if (totalVote > rate87)
                            input.MergeAttribute("src", "/Content/images/star.png");
                        rate++;
                        input.MergeAttribute("style", "width: 15px; height: 15px; border-width: 0px;");
                        result.AppendLine(input.ToString());
                    }
                }
            }
            return result.ToString();
        }

        public static string ArticleSpecification(this HtmlHelper html, mytrip_Articles article)
        {
            string path = article.Path;
            StringBuilder result = new StringBuilder();
            if (path == "Articles" | path.StartsWith("(Tag)") | path.StartsWith("(Search)"))
            {
                if (article.CategoryId == article.mytrip_ArticlesCategory.SubCategoryId)
                {
                    TagBuilder a_category = new TagBuilder("a");
                    a_category.MergeAttribute("href", "/Article/Index/1/10/" + article.CategoryId + "/" + article.mytrip_ArticlesCategory.Path);
                    a_category.InnerHtml = article.mytrip_ArticlesCategory.Title;
                    if (article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog)
                    {
                        result.AppendLine(ArticleLanguage.blog + ": " + a_category + "<br/>");
                    }
                    else
                    {
                        result.AppendLine(ArticleLanguage.category + ": " + a_category + "<br/>");
                    }
                }
                else
                {
                    TagBuilder a_category = new TagBuilder("a");
                    a_category.MergeAttribute("href", "/Article/Index/1/10/" + article.mytrip_ArticlesCategory.SubCategoryId + "/" + article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Path);
                    a_category.InnerHtml = article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Title;
                    if (article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog)
                    {
                        result.AppendLine(ArticleLanguage.blog + ": " + a_category + "<br/>");
                    }
                    else
                    {
                        result.AppendLine(ArticleLanguage.category + ": " + a_category + "<br/>");
                    }

                    TagBuilder a_subcategory = new TagBuilder("a");
                    a_subcategory.MergeAttribute("href", "/Article/Index/1/10/" + article.CategoryId + "/" + article.mytrip_ArticlesCategory.Path);
                    a_subcategory.InnerHtml = article.mytrip_ArticlesCategory.Title;
                    if (article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog)
                    {
                        result.AppendLine(ArticleLanguage.topic + ": " + a_subcategory + "<br/>");
                    }
                    else
                    {
                        result.AppendLine(ArticleLanguage.subcategory + ": " + a_subcategory + "<br/>");
                    }
                }
            }
            else
            {
                if (article.mytrip_ArticlesCategory.Path != path)
                {
                    if (article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.SeparateBlock)
                    {
                        result.AppendLine(ArticleLanguage.category + ": ");
                    }
                    else if (article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog)
                    {
                        result.AppendLine(ArticleLanguage.topic + ": ");
                    }
                    else { result.AppendLine(ArticleLanguage.subcategory + ": "); }
                    TagBuilder a_2category = new TagBuilder("a");
                    a_2category.MergeAttribute("href", "/Article/Index/1/10/" + article.CategoryId + "/" + article.mytrip_ArticlesCategory.Path);
                    a_2category.InnerHtml = article.mytrip_ArticlesCategory.Title;
                    result.AppendLine(a_2category + "<br/>");
                }

            }
            result.AppendLine(ArticleLanguage.author + ": " + article.UserName + "<br/>");
            result.AppendLine(ArticleLanguage.views + ": " + article.Views + "<br/>");
            if (article.ApprovedComment)
                result.AppendLine(ArticleLanguage.comments + ": " + article.mytrip_ArticlesComments.Count + "<br/>");
            result.AppendLine(ArticleLanguage.create_date + ": " + String.Format("{0:dd MMMM yyyy}", article.CreateDate));
            return result.ToString();
        }

        public static string ImageForAbstract(this HtmlHelper html, string image, int width)
        {
            if (image != null)
            {
                string old = "BORDER-BOTTOM: 0px; BORDER-LEFT: 0px; BORDER-TOP: 0px; BORDER-RIGHT: 0px";
                string _new = "border:0px;width:" + width + "px;position:relative;float:right;margin-left:5px;";
                image = image.Replace(old, _new);
                return image;
            }
            else
            {
                return string.Empty;
            }
        }
        public static string ArticleHomeContent(this HtmlHelper html, string param, int take, int content)
        {
            #region AsArticles
            if (param == "AsArticles")
            {
                StringBuilder result = new StringBuilder();
                TagBuilder table = new TagBuilder("table");
                IArticleRepository ar = new IArticleRepository();
                foreach (mytrip_Articles article in ar.article.GetArticlesOpenedNoMenu(take, HttpContext.Current.Session["culture"].ToString()))
                {
                    TagBuilder tr = new TagBuilder("tr");
                    TagBuilder td1 = new TagBuilder("td");
                    td1.MergeAttribute("style", "width: 170px;");
                    td1.InnerHtml = privateArticleSpecification("Articles", article.CategoryId, article.mytrip_ArticlesCategory.SubCategoryId,
                                          article.mytrip_ArticlesCategory.Title, article.mytrip_ArticlesCategory.Path,
                                          article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Title,
                                          article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Path,
                                          article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.SeparateBlock,
                                          article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog,
                                          article.UserName, article.Views,
                                          article.ApprovedComment,
                                          article.mytrip_ArticlesComments.Count(), article.CreateDate);
                    TagBuilder td2 = new TagBuilder("td");
                    TagBuilder div = new TagBuilder("div");
                    div.MergeAttribute("style", "position: relative; float: right");
                    div.InnerHtml = privateArticleRating((double)article.TotalVotes, article.ApprovedVotes, false, article.mytrip_ArticlesVotes.Count());
                    TagBuilder b = new TagBuilder("b");
                    TagBuilder a_title = new TagBuilder("a");
                    a_title.MergeAttribute("href", "/Article/View/" + article.ArticleId + "/" + article.Path);
                    a_title.InnerHtml = article.Title;
                    if (article.OnlyForRegisterUser)
                        b.InnerHtml = a_title + _Image("/Content/images/Keys.png", 20, 0, "", 0);
                    else
                        b.InnerHtml = a_title.ToString();
                    TagBuilder p = new TagBuilder("p");
                    string imageAbstract = privateImageForAbstract(article.ImageForAbstract, 150);
                    StringBuilder tag = new StringBuilder();
                    foreach (var t in article.mytrip_ArticlesTag)
                    {
                        TagBuilder a_tag = new TagBuilder("a");
                        a_tag.MergeAttribute("href", "/Article/Index/1/10/" + t.TagId + "/" + t.Path);
                        a_tag.InnerHtml = t.TagName;
                        tag.AppendLine(a_tag.ToString());
                    }
                    string abstracts = string.Empty;
                    if (article.Abstract.Length <= content)
                        abstracts = article.Abstract;
                    else
                        abstracts = article.Abstract.Remove(content) + "...";
                    td2.InnerHtml = div.ToString() + b.ToString() + p.ToString() + imageAbstract + abstracts + "<br/>" + tag.ToString();
                    tr.InnerHtml = td1.ToString() + td2.ToString();
                    result.AppendLine(tr.ToString());
                }
                table.InnerHtml = result.ToString();
                return table.ToString();
            }
            #endregion
            #region small1column
            else if (param == "small1column")
            {
                StringBuilder result = new StringBuilder();
                TagBuilder table = new TagBuilder("table");
                IArticleRepository ar = new IArticleRepository();
                TagBuilder tr = new TagBuilder("tr");
                TagBuilder td1 = new TagBuilder("td");
                foreach (mytrip_Articles article in ar.article.GetArticlesOpenedNoMenu(take, HttpContext.Current.Session["culture"].ToString()))
                {


                    //td1.MergeAttribute("style", "width: 50%;");
                    string specification = privateArticleSpecification("Articles", article.CategoryId, article.mytrip_ArticlesCategory.SubCategoryId,
                                          article.mytrip_ArticlesCategory.Title, article.mytrip_ArticlesCategory.Path,
                                          article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Title,
                                          article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Path,
                                          article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.SeparateBlock,
                                          article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog);
                    string rating = privateArticleRating2((double)article.TotalVotes, article.ApprovedVotes, false, article.mytrip_ArticlesVotes.Count());

                    TagBuilder b = new TagBuilder("b");
                    TagBuilder a_title = new TagBuilder("a");
                    a_title.MergeAttribute("href", "/Article/View/" + article.ArticleId + "/" + article.Path);
                    a_title.InnerHtml = article.Title;
                    if (article.OnlyForRegisterUser)
                        b.InnerHtml = a_title + _Image("/Content/images/Keys.png", 20, 0, "", 0);
                    else
                        b.InnerHtml = a_title.ToString();
                    StringBuilder tag = new StringBuilder();
                    foreach (var t in article.mytrip_ArticlesTag)
                    {
                        TagBuilder a_tag = new TagBuilder("a");
                        a_tag.MergeAttribute("href", "/Article/Index/1/10/" + t.TagId + "/" + t.Path);
                        a_tag.InnerHtml = t.TagName;
                        tag.AppendLine(a_tag.ToString());
                    }
                    string abstracts = string.Empty;
                    if (article.Abstract.Length <= content)
                        abstracts = article.Abstract;
                    else
                        abstracts = article.Abstract.Remove(content) + "...";
                    td1.InnerHtml = b.ToString() + "<br/>" + abstracts + "<br/>" + rating + "<br/>" + specification + tag.ToString();



                    tr.InnerHtml = td1.ToString();
                    result.AppendLine(tr.ToString());
                }
                table.InnerHtml = result.ToString();
                return table.ToString();
            }
            #endregion
            #region 1column
            else if (param == "1column")
            {
                StringBuilder result = new StringBuilder();
                TagBuilder table = new TagBuilder("table");
                IArticleRepository ar = new IArticleRepository();
                TagBuilder tr = new TagBuilder("tr");
                TagBuilder td1 = new TagBuilder("td");
                foreach (mytrip_Articles article in ar.article.GetArticlesOpenedNoMenu(take, HttpContext.Current.Session["culture"].ToString()))
                {


                    //td1.MergeAttribute("style", "width: 50%;");
                    string specification = privateArticleSpecification("Articles", article.CategoryId, article.mytrip_ArticlesCategory.SubCategoryId,
                                          article.mytrip_ArticlesCategory.Title, article.mytrip_ArticlesCategory.Path,
                                          article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Title,
                                          article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Path,
                                          article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.SeparateBlock,
                                          article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog);
                    string rating = privateArticleRating2((double)article.TotalVotes, article.ApprovedVotes, false, article.mytrip_ArticlesVotes.Count());

                    TagBuilder b = new TagBuilder("b");
                    TagBuilder a_title = new TagBuilder("a");
                    a_title.MergeAttribute("href", "/Article/View/" + article.ArticleId + "/" + article.Path);
                    a_title.InnerHtml = article.Title;
                    if (article.OnlyForRegisterUser)
                        b.InnerHtml = a_title + _Image("/Content/images/Keys.png", 20, 0, "", 0);
                    else
                        b.InnerHtml = a_title.ToString();
                    TagBuilder p = new TagBuilder("p");
                    string imageAbstract = privateImageForAbstract(article.ImageForAbstract, 150);
                    StringBuilder tag = new StringBuilder();
                    foreach (var t in article.mytrip_ArticlesTag)
                    {
                        TagBuilder a_tag = new TagBuilder("a");
                        a_tag.MergeAttribute("href", "/Article/Index/1/10/" + t.TagId + "/" + t.Path);
                        a_tag.InnerHtml = t.TagName;
                        tag.AppendLine(a_tag.ToString());
                    }
                    string abstracts = string.Empty;
                    if (article.Abstract.Length <= content)
                        abstracts = article.Abstract;
                    else
                        abstracts = article.Abstract.Remove(content) + "...";
                    td1.InnerHtml = b.ToString() + p.ToString() + imageAbstract + abstracts + "<br/>" + rating + "<br/>" + specification + tag.ToString();



                    tr.InnerHtml = td1.ToString();
                    result.AppendLine(tr.ToString());
                }
                table.InnerHtml = result.ToString();
                return table.ToString();
            }
            #endregion
            #region 2column
            else if (param == "2column")
            {
                StringBuilder result = new StringBuilder();
                TagBuilder table = new TagBuilder("table");
                IArticleRepository ar = new IArticleRepository();
                int artcount = ar.article.GetArticlesOpenedNoMenu((take * 2), HttpContext.Current.Session["culture"].ToString()).Count();
                if ((int)Math.Ceiling((double)artcount / 2) < take)
                    take = (int)Math.Ceiling((double)artcount / 2);
                int _count = 0;
                string path = string.Empty;
                while (_count < take)
                {
                    TagBuilder tr = new TagBuilder("tr");
                    TagBuilder td1 = new TagBuilder("td");
                    TagBuilder td2 = new TagBuilder("td");
                    foreach (mytrip_Articles article in ar.article.GetArticlesOpenedNoMenu((take * 2), HttpContext.Current.Session["culture"].ToString()))
                    {
                        if (path.IndexOf("art" + article.ArticleId.ToString() + "end") == -1)
                        {
                            path += "art" + article.ArticleId.ToString() + "end";

                            td1.MergeAttribute("style", "width: 50%;");
                            string specification = privateArticleSpecification("Articles", article.CategoryId, article.mytrip_ArticlesCategory.SubCategoryId,
                                                  article.mytrip_ArticlesCategory.Title, article.mytrip_ArticlesCategory.Path,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Title,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Path,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.SeparateBlock,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog);
                            string rating = privateArticleRating2((double)article.TotalVotes, article.ApprovedVotes, false, article.mytrip_ArticlesVotes.Count());

                            TagBuilder b = new TagBuilder("b");
                            TagBuilder a_title = new TagBuilder("a");
                            a_title.MergeAttribute("href", "/Article/View/" + article.ArticleId + "/" + article.Path);
                            a_title.InnerHtml = article.Title;
                            if (article.OnlyForRegisterUser)
                                b.InnerHtml = a_title + _Image("/Content/images/Keys.png", 20, 0, "", 0);
                            else
                                b.InnerHtml = a_title.ToString();
                            TagBuilder p = new TagBuilder("p");
                            string imageAbstract = privateImageForAbstract(article.ImageForAbstract, 150);
                            StringBuilder tag = new StringBuilder();
                            foreach (var t in article.mytrip_ArticlesTag)
                            {
                                TagBuilder a_tag = new TagBuilder("a");
                                a_tag.MergeAttribute("href", "/Article/Index/1/10/" + t.TagId + "/" + t.Path);
                                a_tag.InnerHtml = t.TagName;
                                tag.AppendLine(a_tag.ToString());
                            }
                            string abstracts = string.Empty;
                            if (article.Abstract.Length <= content)
                                abstracts = article.Abstract;
                            else
                                abstracts = article.Abstract.Remove(content) + "...";
                            td1.InnerHtml = b.ToString() + p.ToString() + imageAbstract + abstracts + "<br/>" + rating + "<br/>" + specification + tag.ToString();
                            break;
                        }
                    }
                    foreach (mytrip_Articles article in ar.article.GetArticlesOpenedNoMenu((take * 2), HttpContext.Current.Session["culture"].ToString()))
                    {
                        if (path.IndexOf("art" + article.ArticleId.ToString() + "end") == -1)
                        {
                            path += "art" + article.ArticleId.ToString() + "end";

                            //td2.MergeAttribute("style", "width: 170px;");
                            string specification = privateArticleSpecification("Articles", article.CategoryId, article.mytrip_ArticlesCategory.SubCategoryId,
                                                  article.mytrip_ArticlesCategory.Title, article.mytrip_ArticlesCategory.Path,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Title,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Path,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.SeparateBlock,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog);
                            string rating = privateArticleRating2((double)article.TotalVotes, article.ApprovedVotes, false, article.mytrip_ArticlesVotes.Count());

                            TagBuilder b = new TagBuilder("b");
                            TagBuilder a_title = new TagBuilder("a");
                            a_title.MergeAttribute("href", "/Article/View/" + article.ArticleId + "/" + article.Path);
                            a_title.InnerHtml = article.Title;
                            if (article.OnlyForRegisterUser)
                                b.InnerHtml = a_title + _Image("/Content/images/Keys.png", 20, 0, "", 0);
                            else
                                b.InnerHtml = a_title.ToString();
                            TagBuilder p = new TagBuilder("p");
                            string imageAbstract = privateImageForAbstract(article.ImageForAbstract, 150);
                            StringBuilder tag = new StringBuilder();
                            foreach (var t in article.mytrip_ArticlesTag)
                            {
                                TagBuilder a_tag = new TagBuilder("a");
                                a_tag.MergeAttribute("href", "/Article/Index/1/10/" + t.TagId + "/" + t.Path);
                                a_tag.InnerHtml = t.TagName;
                                tag.AppendLine(a_tag.ToString());
                            }
                            string abstracts = string.Empty;
                            if (article.Abstract.Length <= content)
                                abstracts = article.Abstract;
                            else
                                abstracts = article.Abstract.Remove(content) + "...";
                            td2.InnerHtml = b.ToString() + p.ToString() + imageAbstract + abstracts + "<br/>" + rating + "<br/>" + specification + tag.ToString();
                            break;
                        }
                    }
                    tr.InnerHtml = td1.ToString() + td2.ToString();
                    result.AppendLine(tr.ToString());
                    _count++;
                }
                table.InnerHtml = result.ToString();
                return table.ToString();
            }
            #endregion
            #region small2column
            else if (param == "small2column")
            {
                StringBuilder result = new StringBuilder();
                TagBuilder table = new TagBuilder("table");
                IArticleRepository ar = new IArticleRepository();
                int artcount = ar.article.GetArticlesOpenedNoMenu((take * 2), HttpContext.Current.Session["culture"].ToString()).Count();
                if ((int)Math.Ceiling((double)artcount / 2) < take)
                    take = (int)Math.Ceiling((double)artcount / 2);
                int _count = 0;
                string path = string.Empty;
                while (_count < take)
                {
                    TagBuilder tr = new TagBuilder("tr");
                    TagBuilder td1 = new TagBuilder("td");
                    TagBuilder td2 = new TagBuilder("td");
                    foreach (mytrip_Articles article in ar.article.GetArticlesOpenedNoMenu((take * 2), HttpContext.Current.Session["culture"].ToString()))
                    {
                        if (path.IndexOf("art" + article.ArticleId.ToString() + "end") == -1)
                        {
                            path += "art" + article.ArticleId.ToString() + "end";

                            td1.MergeAttribute("style", "width: 50%;");
                            string specification = privateArticleSpecification("Articles", article.CategoryId, article.mytrip_ArticlesCategory.SubCategoryId,
                                                  article.mytrip_ArticlesCategory.Title, article.mytrip_ArticlesCategory.Path,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Title,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Path,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.SeparateBlock,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog);
                            string rating = privateArticleRating2((double)article.TotalVotes, article.ApprovedVotes, false, article.mytrip_ArticlesVotes.Count());

                            TagBuilder b = new TagBuilder("b");
                            TagBuilder a_title = new TagBuilder("a");
                            a_title.MergeAttribute("href", "/Article/View/" + article.ArticleId + "/" + article.Path);
                            a_title.InnerHtml = article.Title;
                            if (article.OnlyForRegisterUser)
                                b.InnerHtml = a_title + _Image("/Content/images/Keys.png", 20, 0, "", 0);
                            else
                                b.InnerHtml = a_title.ToString();
                            StringBuilder tag = new StringBuilder();
                            foreach (var t in article.mytrip_ArticlesTag)
                            {
                                TagBuilder a_tag = new TagBuilder("a");
                                a_tag.MergeAttribute("href", "/Article/Index/1/10/" + t.TagId + "/" + t.Path);
                                a_tag.InnerHtml = t.TagName;
                                tag.AppendLine(a_tag.ToString());
                            }
                            string abstracts = string.Empty;
                            if (article.Abstract.Length <= content)
                                abstracts = article.Abstract;
                            else
                                abstracts = article.Abstract.Remove(content) + "...";
                            td1.InnerHtml = b.ToString() + "<br/>" + abstracts + "<br/>" + rating + "<br/>" + specification + tag.ToString();
                            break;
                        }
                    }
                    foreach (mytrip_Articles article in ar.article.GetArticlesOpenedNoMenu((take * 2), HttpContext.Current.Session["culture"].ToString()))
                    {
                        if (path.IndexOf("art" + article.ArticleId.ToString() + "end") == -1)
                        {
                            path += "art" + article.ArticleId.ToString() + "end";

                            //td2.MergeAttribute("style", "width: 170px;");
                            string specification = privateArticleSpecification("Articles", article.CategoryId, article.mytrip_ArticlesCategory.SubCategoryId,
                                                  article.mytrip_ArticlesCategory.Title, article.mytrip_ArticlesCategory.Path,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Title,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Path,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.SeparateBlock,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog);
                            string rating = privateArticleRating2((double)article.TotalVotes, article.ApprovedVotes, false, article.mytrip_ArticlesVotes.Count());

                            TagBuilder b = new TagBuilder("b");
                            TagBuilder a_title = new TagBuilder("a");
                            a_title.MergeAttribute("href", "/Article/View/" + article.ArticleId + "/" + article.Path);
                            a_title.InnerHtml = article.Title;
                            if (article.OnlyForRegisterUser)
                                b.InnerHtml = a_title + _Image("/Content/images/Keys.png", 20, 0, "", 0);
                            else
                                b.InnerHtml = a_title.ToString();
                            StringBuilder tag = new StringBuilder();
                            foreach (var t in article.mytrip_ArticlesTag)
                            {
                                TagBuilder a_tag = new TagBuilder("a");
                                a_tag.MergeAttribute("href", "/Article/Index/1/10/" + t.TagId + "/" + t.Path);
                                a_tag.InnerHtml = t.TagName;
                                tag.AppendLine(a_tag.ToString());
                            }
                            string abstracts = string.Empty;
                            if (article.Abstract.Length <= content)
                                abstracts = article.Abstract;
                            else
                                abstracts = article.Abstract.Remove(content) + "...";
                            td2.InnerHtml = b.ToString() + "<br/>" + abstracts + "<br/>" + rating + "<br/>" + specification + tag.ToString();
                            break;
                        }
                    }
                    tr.InnerHtml = td1.ToString() + td2.ToString();
                    result.AppendLine(tr.ToString());
                    _count++;
                }
                table.InnerHtml = result.ToString();
                return table.ToString();
            }
            #endregion
            #region 3column
            else if (param == "3column")
            {
                StringBuilder result = new StringBuilder();
                TagBuilder table = new TagBuilder("table");
                IArticleRepository ar = new IArticleRepository();
                int artcount = ar.article.GetArticlesOpenedNoMenu((take * 3), HttpContext.Current.Session["culture"].ToString()).Count();
                if ((int)Math.Ceiling((double)artcount / 3) < take)
                    take = (int)Math.Ceiling((double)artcount / 3);
                int _count = 0;
                string path = string.Empty;
                while (_count < take)
                {
                    TagBuilder tr = new TagBuilder("tr");
                    TagBuilder td1 = new TagBuilder("td");
                    TagBuilder td2 = new TagBuilder("td");
                    TagBuilder td3 = new TagBuilder("td");
                    foreach (mytrip_Articles article in ar.article.GetArticlesOpenedNoMenu((take * 3), HttpContext.Current.Session["culture"].ToString()))
                    {
                        if (path.IndexOf("art" + article.ArticleId.ToString() + "end") == -1)
                        {
                            path += "art" + article.ArticleId.ToString() + "end";

                            td1.MergeAttribute("style", "width: 33%;");
                            string specification = privateArticleSpecification("Articles", article.CategoryId, article.mytrip_ArticlesCategory.SubCategoryId,
                                                  article.mytrip_ArticlesCategory.Title, article.mytrip_ArticlesCategory.Path,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Title,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Path,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.SeparateBlock,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog);
                            string rating = privateArticleRating2((double)article.TotalVotes, article.ApprovedVotes, false, article.mytrip_ArticlesVotes.Count());

                            TagBuilder b = new TagBuilder("b");
                            TagBuilder a_title = new TagBuilder("a");
                            a_title.MergeAttribute("href", "/Article/View/" + article.ArticleId + "/" + article.Path);
                            a_title.InnerHtml = article.Title;
                            if (article.OnlyForRegisterUser)
                                b.InnerHtml = a_title + _Image("/Content/images/Keys.png", 20, 0, "", 0);
                            else
                                b.InnerHtml = a_title.ToString();
                            TagBuilder p = new TagBuilder("p");
                            string imageAbstract = privateImageForAbstract(article.ImageForAbstract, 80);
                            StringBuilder tag = new StringBuilder();
                            foreach (var t in article.mytrip_ArticlesTag)
                            {
                                TagBuilder a_tag = new TagBuilder("a");
                                a_tag.MergeAttribute("href", "/Article/Index/1/10/" + t.TagId + "/" + t.Path);
                                a_tag.InnerHtml = t.TagName;
                                tag.AppendLine(a_tag.ToString());
                            }
                            string abstracts = string.Empty;
                            if (article.Abstract.Length <= content)
                                abstracts = article.Abstract;
                            else
                                abstracts = article.Abstract.Remove(content) + "...";
                            td1.InnerHtml = b.ToString() + p.ToString() + imageAbstract + abstracts + "<br/>" + rating + "<br/>" + specification + tag.ToString();
                            break;
                        }
                    }
                    foreach (mytrip_Articles article in ar.article.GetArticlesOpenedNoMenu((take * 3), HttpContext.Current.Session["culture"].ToString()))
                    {
                        if (path.IndexOf("art" + article.ArticleId.ToString() + "end") == -1)
                        {
                            path += "art" + article.ArticleId.ToString() + "end";

                            //td2.MergeAttribute("style", "width: 170px;");
                            string specification = privateArticleSpecification("Articles", article.CategoryId, article.mytrip_ArticlesCategory.SubCategoryId,
                                                  article.mytrip_ArticlesCategory.Title, article.mytrip_ArticlesCategory.Path,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Title,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Path,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.SeparateBlock,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog);
                            string rating = privateArticleRating2((double)article.TotalVotes, article.ApprovedVotes, false, article.mytrip_ArticlesVotes.Count());

                            TagBuilder b = new TagBuilder("b");
                            TagBuilder a_title = new TagBuilder("a");
                            a_title.MergeAttribute("href", "/Article/View/" + article.ArticleId + "/" + article.Path);
                            a_title.InnerHtml = article.Title;
                            if (article.OnlyForRegisterUser)
                                b.InnerHtml = a_title + _Image("/Content/images/Keys.png", 20, 0, "", 0);
                            else
                                b.InnerHtml = a_title.ToString();
                            TagBuilder p = new TagBuilder("p");
                            string imageAbstract = privateImageForAbstract(article.ImageForAbstract, 80);
                            StringBuilder tag = new StringBuilder();
                            foreach (var t in article.mytrip_ArticlesTag)
                            {
                                TagBuilder a_tag = new TagBuilder("a");
                                a_tag.MergeAttribute("href", "/Article/Index/1/10/" + t.TagId + "/" + t.Path);
                                a_tag.InnerHtml = t.TagName;
                                tag.AppendLine(a_tag.ToString());
                            }
                            string abstracts = string.Empty;
                            if (article.Abstract.Length <= content)
                                abstracts = article.Abstract;
                            else
                                abstracts = article.Abstract.Remove(content) + "...";
                            td2.InnerHtml = b.ToString() + p.ToString() + imageAbstract + abstracts + "<br/>" + rating + "<br/>" + specification + tag.ToString();
                            break;
                        }
                    }
                    foreach (mytrip_Articles article in ar.article.GetArticlesOpenedNoMenu((take * 3), HttpContext.Current.Session["culture"].ToString()))
                    {
                        if (path.IndexOf("art" + article.ArticleId.ToString() + "end") == -1)
                        {
                            path += "art" + article.ArticleId.ToString() + "end";

                            td3.MergeAttribute("style", "width: 33%;");
                            string specification = privateArticleSpecification("Articles", article.CategoryId, article.mytrip_ArticlesCategory.SubCategoryId,
                                                  article.mytrip_ArticlesCategory.Title, article.mytrip_ArticlesCategory.Path,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Title,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Path,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.SeparateBlock,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog);
                            string rating = privateArticleRating2((double)article.TotalVotes, article.ApprovedVotes, false, article.mytrip_ArticlesVotes.Count());

                            TagBuilder b = new TagBuilder("b");
                            TagBuilder a_title = new TagBuilder("a");
                            a_title.MergeAttribute("href", "/Article/View/" + article.ArticleId + "/" + article.Path);
                            a_title.InnerHtml = article.Title;
                            if (article.OnlyForRegisterUser)
                                b.InnerHtml = a_title + _Image("/Content/images/Keys.png", 20, 0, "", 0);
                            else
                                b.InnerHtml = a_title.ToString();
                            TagBuilder p = new TagBuilder("p");
                            string imageAbstract = privateImageForAbstract(article.ImageForAbstract, 80);
                            StringBuilder tag = new StringBuilder();
                            foreach (var t in article.mytrip_ArticlesTag)
                            {
                                TagBuilder a_tag = new TagBuilder("a");
                                a_tag.MergeAttribute("href", "/Article/Index/1/10/" + t.TagId + "/" + t.Path);
                                a_tag.InnerHtml = t.TagName;
                                tag.AppendLine(a_tag.ToString());
                            }
                            string abstracts = string.Empty;
                            if (article.Abstract.Length <= content)
                                abstracts = article.Abstract;
                            else
                                abstracts = article.Abstract.Remove(content) + "...";
                            td3.InnerHtml = b.ToString() + p.ToString() + imageAbstract + abstracts + "<br/>" + rating + "<br/>" + specification + tag.ToString();
                            break;
                        }
                    }
                    tr.InnerHtml = td1.ToString() + td2.ToString() + td3.ToString();
                    result.AppendLine(tr.ToString());
                    _count++;
                }
                table.InnerHtml = result.ToString();
                return table.ToString();
            }
            #endregion
            #region small3column
            else if (param == "small3column")
            {
                StringBuilder result = new StringBuilder();
                TagBuilder table = new TagBuilder("table");
                IArticleRepository ar = new IArticleRepository();
                int artcount = ar.article.GetArticlesOpenedNoMenu((take * 3), HttpContext.Current.Session["culture"].ToString()).Count();
                if ((int)Math.Ceiling((double)artcount / 3) < take)
                    take = (int)Math.Ceiling((double)artcount / 3);
                int _count = 0;
                string path = string.Empty;
                while (_count < take)
                {
                    TagBuilder tr = new TagBuilder("tr");
                    TagBuilder td1 = new TagBuilder("td");
                    TagBuilder td2 = new TagBuilder("td");
                    TagBuilder td3 = new TagBuilder("td");
                    foreach (mytrip_Articles article in ar.article.GetArticlesOpenedNoMenu((take * 3), HttpContext.Current.Session["culture"].ToString()))
                    {
                        if (path.IndexOf("art" + article.ArticleId.ToString() + "end") == -1)
                        {
                            path += "art" + article.ArticleId.ToString() + "end";

                            td1.MergeAttribute("style", "width: 33%;");
                            string specification = privateArticleSpecification("Articles", article.CategoryId, article.mytrip_ArticlesCategory.SubCategoryId,
                                                  article.mytrip_ArticlesCategory.Title, article.mytrip_ArticlesCategory.Path,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Title,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Path,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.SeparateBlock,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog);
                            string rating = privateArticleRating2((double)article.TotalVotes, article.ApprovedVotes, false, article.mytrip_ArticlesVotes.Count());

                            TagBuilder b = new TagBuilder("b");
                            TagBuilder a_title = new TagBuilder("a");
                            a_title.MergeAttribute("href", "/Article/View/" + article.ArticleId + "/" + article.Path);
                            a_title.InnerHtml = article.Title;
                            if (article.OnlyForRegisterUser)
                                b.InnerHtml = a_title + _Image("/Content/images/Keys.png", 20, 0, "", 0);
                            else
                                b.InnerHtml = a_title.ToString();
                            StringBuilder tag = new StringBuilder();
                            foreach (var t in article.mytrip_ArticlesTag)
                            {
                                TagBuilder a_tag = new TagBuilder("a");
                                a_tag.MergeAttribute("href", "/Article/Index/1/10/" + t.TagId + "/" + t.Path);
                                a_tag.InnerHtml = t.TagName;
                                tag.AppendLine(a_tag.ToString());
                            }
                            string abstracts = string.Empty;
                            if (article.Abstract.Length <= content)
                                abstracts = article.Abstract;
                            else
                                abstracts = article.Abstract.Remove(content) + "...";
                            td1.InnerHtml = b.ToString() + "<br/>" + abstracts + "<br/>" + rating + "<br/>" + specification + tag.ToString();
                            break;
                        }
                    }
                    foreach (mytrip_Articles article in ar.article.GetArticlesOpenedNoMenu((take * 3), HttpContext.Current.Session["culture"].ToString()))
                    {
                        if (path.IndexOf("art" + article.ArticleId.ToString() + "end") == -1)
                        {
                            path += "art" + article.ArticleId.ToString() + "end";

                            //td2.MergeAttribute("style", "width: 170px;");
                            string specification = privateArticleSpecification("Articles", article.CategoryId, article.mytrip_ArticlesCategory.SubCategoryId,
                                                  article.mytrip_ArticlesCategory.Title, article.mytrip_ArticlesCategory.Path,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Title,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Path,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.SeparateBlock,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog);
                            string rating = privateArticleRating2((double)article.TotalVotes, article.ApprovedVotes, false, article.mytrip_ArticlesVotes.Count());

                            TagBuilder b = new TagBuilder("b");
                            TagBuilder a_title = new TagBuilder("a");
                            a_title.MergeAttribute("href", "/Article/View/" + article.ArticleId + "/" + article.Path);
                            a_title.InnerHtml = article.Title;
                            if (article.OnlyForRegisterUser)
                                b.InnerHtml = a_title + _Image("/Content/images/Keys.png", 20, 0, "", 0);
                            else
                                b.InnerHtml = a_title.ToString();
                            StringBuilder tag = new StringBuilder();
                            foreach (var t in article.mytrip_ArticlesTag)
                            {
                                TagBuilder a_tag = new TagBuilder("a");
                                a_tag.MergeAttribute("href", "/Article/Index/1/10/" + t.TagId + "/" + t.Path);
                                a_tag.InnerHtml = t.TagName;
                                tag.AppendLine(a_tag.ToString());
                            }
                            string abstracts = string.Empty;
                            if (article.Abstract.Length <= content)
                                abstracts = article.Abstract;
                            else
                                abstracts = article.Abstract.Remove(content) + "...";
                            td2.InnerHtml = b.ToString() + "<br/>" + abstracts + "<br/>" + rating + "<br/>" + specification + tag.ToString();
                            break;
                        }
                    }
                    foreach (mytrip_Articles article in ar.article.GetArticlesOpenedNoMenu((take * 3), HttpContext.Current.Session["culture"].ToString()))
                    {
                        if (path.IndexOf("art" + article.ArticleId.ToString() + "end") == -1)
                        {
                            path += "art" + article.ArticleId.ToString() + "end";

                            td3.MergeAttribute("style", "width: 33%;");
                            string specification = privateArticleSpecification("Articles", article.CategoryId, article.mytrip_ArticlesCategory.SubCategoryId,
                                                  article.mytrip_ArticlesCategory.Title, article.mytrip_ArticlesCategory.Path,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Title,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Path,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.SeparateBlock,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog);
                            string rating = privateArticleRating2((double)article.TotalVotes, article.ApprovedVotes, false, article.mytrip_ArticlesVotes.Count());

                            TagBuilder b = new TagBuilder("b");
                            TagBuilder a_title = new TagBuilder("a");
                            a_title.MergeAttribute("href", "/Article/View/" + article.ArticleId + "/" + article.Path);
                            a_title.InnerHtml = article.Title;
                            if (article.OnlyForRegisterUser)
                                b.InnerHtml = a_title + _Image("/Content/images/Keys.png", 20, 0, "", 0);
                            else
                                b.InnerHtml = a_title.ToString();
                            StringBuilder tag = new StringBuilder();
                            foreach (var t in article.mytrip_ArticlesTag)
                            {
                                TagBuilder a_tag = new TagBuilder("a");
                                a_tag.MergeAttribute("href", "/Article/Index/1/10/" + t.TagId + "/" + t.Path);
                                a_tag.InnerHtml = t.TagName;
                                tag.AppendLine(a_tag.ToString());
                            }
                            string abstracts = string.Empty;
                            if (article.Abstract.Length <= content)
                                abstracts = article.Abstract;
                            else
                                abstracts = article.Abstract.Remove(content) + "...";
                            td3.InnerHtml = b.ToString() + "<br/>" + abstracts + "<br/>" + rating + "<br/>" + specification + tag.ToString();
                            break;
                        }
                    }
                    tr.InnerHtml = td1.ToString() + td2.ToString() + td3.ToString();
                    result.AppendLine(tr.ToString());
                    _count++;
                }
                table.InnerHtml = result.ToString();
                return table.ToString();
            }
            #endregion
            else
            {
                return string.Empty;
            }
        }
        public static string PostsHomeContent(this HtmlHelper html, string param, int count, int content)
        {
            #region AsArticles
            if (param == "AsArticles")
            {
                StringBuilder result = new StringBuilder();
                TagBuilder table = new TagBuilder("table");
                IArticleRepository ar = new IArticleRepository();
                int _count = 0;
                foreach (mytrip_Articles article in ar.article.GetPosts(HttpContext.Current.Session["culture"].ToString()))
                {
                    TagBuilder tr = new TagBuilder("tr");
                    TagBuilder td1 = new TagBuilder("td");
                    td1.MergeAttribute("style", "width: 170px;");
                    td1.InnerHtml = privateArticleSpecification("Articles", article.CategoryId, article.mytrip_ArticlesCategory.SubCategoryId,
                                          article.mytrip_ArticlesCategory.Title, article.mytrip_ArticlesCategory.Path,
                                          article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Title,
                                          article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Path,
                                          article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.SeparateBlock,
                                          article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog,
                                          article.UserName, article.Views,
                                          article.ApprovedComment,
                                          article.mytrip_ArticlesComments.Count(), article.CreateDate);
                    TagBuilder td2 = new TagBuilder("td");
                    TagBuilder div = new TagBuilder("div");
                    div.MergeAttribute("style", "position: relative; float: right");
                    div.InnerHtml = privateArticleRating((double)article.TotalVotes, article.ApprovedVotes, false, article.mytrip_ArticlesVotes.Count());
                    TagBuilder b = new TagBuilder("b");
                    TagBuilder a_title = new TagBuilder("a");
                    a_title.MergeAttribute("href", "/Article/View/" + article.ArticleId + "/" + article.Path);
                    a_title.InnerHtml = article.Title;
                    if (article.OnlyForRegisterUser)
                        b.InnerHtml = a_title + _Image("/Content/images/Keys.png", 20, 0, "", 0);
                    else
                        b.InnerHtml = a_title.ToString();
                    TagBuilder p = new TagBuilder("p");
                    string imageAbstract = privateImageForAbstract(article.ImageForAbstract, 150);
                    StringBuilder tag = new StringBuilder();
                    foreach (var t in article.mytrip_ArticlesTag)
                    {
                        TagBuilder a_tag = new TagBuilder("a");
                        a_tag.MergeAttribute("href", "/Article/Index/1/10/" + t.TagId + "/" + t.Path);
                        a_tag.InnerHtml = t.TagName;
                        tag.AppendLine(a_tag.ToString());
                    }
                    string abstracts = string.Empty;
                    if (article.Abstract.Length <= content)
                        abstracts = article.Abstract;
                    else
                        abstracts = article.Abstract.Remove(content) + "...";
                    td2.InnerHtml = div.ToString() + b.ToString() + p.ToString() + imageAbstract + abstracts + "<br/>" + tag.ToString();
                    tr.InnerHtml = td1.ToString() + td2.ToString();
                    result.AppendLine(tr.ToString());
                    _count++;
                    if (_count >= count)
                        break;
                }
                table.InnerHtml = result.ToString();
                return table.ToString();
            }
            #endregion
            #region small1column
            else if (param == "small1column")
            {
                StringBuilder result = new StringBuilder();
                TagBuilder table = new TagBuilder("table");
                IArticleRepository ar = new IArticleRepository();
                int artcount = ar.article.GetPosts(HttpContext.Current.Session["culture"].ToString()).Count();
                int _count = 0;
                TagBuilder tr = new TagBuilder("tr");
                TagBuilder td1 = new TagBuilder("td");
                foreach (mytrip_Articles article in ar.article.GetPosts(HttpContext.Current.Session["culture"].ToString()))
                {


                    //td1.MergeAttribute("style", "width: 50%;");
                    string specification = privateArticleSpecification("Articles", article.CategoryId, article.mytrip_ArticlesCategory.SubCategoryId,
                                          article.mytrip_ArticlesCategory.Title, article.mytrip_ArticlesCategory.Path,
                                          article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Title,
                                          article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Path,
                                          article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.SeparateBlock,
                                          article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog);
                    string rating = privateArticleRating2((double)article.TotalVotes, article.ApprovedVotes, false, article.mytrip_ArticlesVotes.Count());

                    TagBuilder b = new TagBuilder("b");
                    TagBuilder a_title = new TagBuilder("a");
                    a_title.MergeAttribute("href", "/Article/View/" + article.ArticleId + "/" + article.Path);
                    a_title.InnerHtml = article.Title;
                    if (article.OnlyForRegisterUser)
                        b.InnerHtml = a_title + _Image("/Content/images/Keys.png", 20, 0, "", 0);
                    else
                        b.InnerHtml = a_title.ToString();
                    StringBuilder tag = new StringBuilder();
                    foreach (var t in article.mytrip_ArticlesTag)
                    {
                        TagBuilder a_tag = new TagBuilder("a");
                        a_tag.MergeAttribute("href", "/Article/Index/1/10/" + t.TagId + "/" + t.Path);
                        a_tag.InnerHtml = t.TagName;
                        tag.AppendLine(a_tag.ToString());
                    }
                    string abstracts = string.Empty;
                    if (article.Abstract.Length <= content)
                        abstracts = article.Abstract;
                    else
                        abstracts = article.Abstract.Remove(content) + "...";
                    td1.InnerHtml = b.ToString() + "<br/>" + abstracts + "<br/>" + rating + "<br/>" + specification + tag.ToString();



                    tr.InnerHtml = td1.ToString();
                    result.AppendLine(tr.ToString());
                    _count++;
                    if (_count >= count)
                        break;
                }
                table.InnerHtml = result.ToString();
                return table.ToString();
            }
            #endregion
            #region 1column
            else if (param == "1column")
            {
                StringBuilder result = new StringBuilder();
                TagBuilder table = new TagBuilder("table");
                IArticleRepository ar = new IArticleRepository();
                int artcount = ar.article.GetPosts(HttpContext.Current.Session["culture"].ToString()).Count();
                int _count = 0;
                TagBuilder tr = new TagBuilder("tr");
                TagBuilder td1 = new TagBuilder("td");
                foreach (mytrip_Articles article in ar.article.GetPosts(HttpContext.Current.Session["culture"].ToString()))
                {


                    //td1.MergeAttribute("style", "width: 50%;");
                    string specification = privateArticleSpecification("Articles", article.CategoryId, article.mytrip_ArticlesCategory.SubCategoryId,
                                          article.mytrip_ArticlesCategory.Title, article.mytrip_ArticlesCategory.Path,
                                          article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Title,
                                          article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Path,
                                          article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.SeparateBlock,
                                          article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog);
                    string rating = privateArticleRating2((double)article.TotalVotes, article.ApprovedVotes, false, article.mytrip_ArticlesVotes.Count());

                    TagBuilder b = new TagBuilder("b");
                    TagBuilder a_title = new TagBuilder("a");
                    a_title.MergeAttribute("href", "/Article/View/" + article.ArticleId + "/" + article.Path);
                    a_title.InnerHtml = article.Title;
                    if (article.OnlyForRegisterUser)
                        b.InnerHtml = a_title + _Image("/Content/images/Keys.png", 20, 0, "", 0);
                    else
                        b.InnerHtml = a_title.ToString();
                    TagBuilder p = new TagBuilder("p");
                    string imageAbstract = privateImageForAbstract(article.ImageForAbstract, 150);
                    StringBuilder tag = new StringBuilder();
                    foreach (var t in article.mytrip_ArticlesTag)
                    {
                        TagBuilder a_tag = new TagBuilder("a");
                        a_tag.MergeAttribute("href", "/Article/Index/1/10/" + t.TagId + "/" + t.Path);
                        a_tag.InnerHtml = t.TagName;
                        tag.AppendLine(a_tag.ToString());
                    }
                    string abstracts = string.Empty;
                    if (article.Abstract.Length <= content)
                        abstracts = article.Abstract;
                    else
                        abstracts = article.Abstract.Remove(content) + "...";
                    td1.InnerHtml = b.ToString() + p.ToString() + imageAbstract + abstracts + "<br/>" + rating + "<br/>" + specification + tag.ToString();



                    tr.InnerHtml = td1.ToString();
                    result.AppendLine(tr.ToString());
                    _count++;
                    if (_count >= count)
                        break;
                }
                table.InnerHtml = result.ToString();
                return table.ToString();
            }
            #endregion
            #region 2column
            else if (param == "2column")
            {
                StringBuilder result = new StringBuilder();
                TagBuilder table = new TagBuilder("table");
                IArticleRepository ar = new IArticleRepository();
                int artcount = ar.article.GetPosts(HttpContext.Current.Session["culture"].ToString()).Count();
                if ((int)Math.Ceiling((double)artcount / 2) < count)
                    count = (int)Math.Ceiling((double)artcount / 2);
                int _count = 0;
                string path = string.Empty;
                while (_count < count)
                {
                    TagBuilder tr = new TagBuilder("tr");
                    TagBuilder td1 = new TagBuilder("td");
                    TagBuilder td2 = new TagBuilder("td");
                    foreach (mytrip_Articles article in ar.article.GetPosts(HttpContext.Current.Session["culture"].ToString()))
                    {
                        if (path.IndexOf("art" + article.ArticleId.ToString() + "end") == -1)
                        {
                            path += "art" + article.ArticleId.ToString() + "end";

                            td1.MergeAttribute("style", "width: 50%;");
                            string specification = privateArticleSpecification("Articles", article.CategoryId, article.mytrip_ArticlesCategory.SubCategoryId,
                                                  article.mytrip_ArticlesCategory.Title, article.mytrip_ArticlesCategory.Path,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Title,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Path,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.SeparateBlock,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog);
                            string rating = privateArticleRating2((double)article.TotalVotes, article.ApprovedVotes, false, article.mytrip_ArticlesVotes.Count());

                            TagBuilder b = new TagBuilder("b");
                            TagBuilder a_title = new TagBuilder("a");
                            a_title.MergeAttribute("href", "/Article/View/" + article.ArticleId + "/" + article.Path);
                            a_title.InnerHtml = article.Title;
                            if (article.OnlyForRegisterUser)
                                b.InnerHtml = a_title + _Image("/Content/images/Keys.png", 20, 0, "", 0);
                            else
                                b.InnerHtml = a_title.ToString();
                            TagBuilder p = new TagBuilder("p");
                            string imageAbstract = privateImageForAbstract(article.ImageForAbstract, 150);
                            StringBuilder tag = new StringBuilder();
                            foreach (var t in article.mytrip_ArticlesTag)
                            {
                                TagBuilder a_tag = new TagBuilder("a");
                                a_tag.MergeAttribute("href", "/Article/Index/1/10/" + t.TagId + "/" + t.Path);
                                a_tag.InnerHtml = t.TagName;
                                tag.AppendLine(a_tag.ToString());
                            }
                            string abstracts = string.Empty;
                            if (article.Abstract.Length <= content)
                                abstracts = article.Abstract;
                            else
                                abstracts = article.Abstract.Remove(content) + "...";
                            td1.InnerHtml = b.ToString() + p.ToString() + imageAbstract + abstracts + "<br/>" + rating + "<br/>" + specification + tag.ToString();
                            break;
                        }
                    }
                    foreach (mytrip_Articles article in ar.article.GetPosts(HttpContext.Current.Session["culture"].ToString()))
                    {
                        if (path.IndexOf("art" + article.ArticleId.ToString() + "end") == -1)
                        {
                            path += "art" + article.ArticleId.ToString() + "end";

                            //td2.MergeAttribute("style", "width: 170px;");
                            string specification = privateArticleSpecification("Articles", article.CategoryId, article.mytrip_ArticlesCategory.SubCategoryId,
                                                  article.mytrip_ArticlesCategory.Title, article.mytrip_ArticlesCategory.Path,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Title,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Path,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.SeparateBlock,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog);
                            string rating = privateArticleRating2((double)article.TotalVotes, article.ApprovedVotes, false, article.mytrip_ArticlesVotes.Count());

                            TagBuilder b = new TagBuilder("b");
                            TagBuilder a_title = new TagBuilder("a");
                            a_title.MergeAttribute("href", "/Article/View/" + article.ArticleId + "/" + article.Path);
                            a_title.InnerHtml = article.Title;
                            if (article.OnlyForRegisterUser)
                                b.InnerHtml = a_title + _Image("/Content/images/Keys.png", 20, 0, "", 0);
                            else
                                b.InnerHtml = a_title.ToString();
                            TagBuilder p = new TagBuilder("p");
                            string imageAbstract = privateImageForAbstract(article.ImageForAbstract, 150);
                            StringBuilder tag = new StringBuilder();
                            foreach (var t in article.mytrip_ArticlesTag)
                            {
                                TagBuilder a_tag = new TagBuilder("a");
                                a_tag.MergeAttribute("href", "/Article/Index/1/10/" + t.TagId + "/" + t.Path);
                                a_tag.InnerHtml = t.TagName;
                                tag.AppendLine(a_tag.ToString());
                            }
                            string abstracts = string.Empty;
                            if (article.Abstract.Length <= content)
                                abstracts = article.Abstract;
                            else
                                abstracts = article.Abstract.Remove(content) + "...";
                            td2.InnerHtml = b.ToString() + p.ToString() + imageAbstract + abstracts + "<br/>" + rating + "<br/>" + specification + tag.ToString();
                            break;
                        }
                    }
                    tr.InnerHtml = td1.ToString() + td2.ToString();
                    result.AppendLine(tr.ToString());
                    _count++;
                }
                table.InnerHtml = result.ToString();
                return table.ToString();
            }
            #endregion
            #region small2column
            else if (param == "small2column")
            {
                StringBuilder result = new StringBuilder();
                TagBuilder table = new TagBuilder("table");
                IArticleRepository ar = new IArticleRepository();
                int artcount = ar.article.GetPosts(HttpContext.Current.Session["culture"].ToString()).Count();
                if ((int)Math.Ceiling((double)artcount / 2) < count)
                    count = (int)Math.Ceiling((double)artcount / 2);
                int _count = 0;
                string path = string.Empty;
                while (_count < count)
                {
                    TagBuilder tr = new TagBuilder("tr");
                    TagBuilder td1 = new TagBuilder("td");
                    TagBuilder td2 = new TagBuilder("td");
                    foreach (mytrip_Articles article in ar.article.GetPosts(HttpContext.Current.Session["culture"].ToString()))
                    {
                        if (path.IndexOf("art" + article.ArticleId.ToString() + "end") == -1)
                        {
                            path += "art" + article.ArticleId.ToString() + "end";

                            td1.MergeAttribute("style", "width: 50%;");
                            string specification = privateArticleSpecification("Articles", article.CategoryId, article.mytrip_ArticlesCategory.SubCategoryId,
                                                  article.mytrip_ArticlesCategory.Title, article.mytrip_ArticlesCategory.Path,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Title,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Path,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.SeparateBlock,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog);
                            string rating = privateArticleRating2((double)article.TotalVotes, article.ApprovedVotes, false, article.mytrip_ArticlesVotes.Count());

                            TagBuilder b = new TagBuilder("b");
                            TagBuilder a_title = new TagBuilder("a");
                            a_title.MergeAttribute("href", "/Article/View/" + article.ArticleId + "/" + article.Path);
                            a_title.InnerHtml = article.Title;
                            if (article.OnlyForRegisterUser)
                                b.InnerHtml = a_title + _Image("/Content/images/Keys.png", 20, 0, "", 0);
                            else
                                b.InnerHtml = a_title.ToString();
                            StringBuilder tag = new StringBuilder();
                            foreach (var t in article.mytrip_ArticlesTag)
                            {
                                TagBuilder a_tag = new TagBuilder("a");
                                a_tag.MergeAttribute("href", "/Article/Index/1/10/" + t.TagId + "/" + t.Path);
                                a_tag.InnerHtml = t.TagName;
                                tag.AppendLine(a_tag.ToString());
                            }
                            string abstracts = string.Empty;
                            if (article.Abstract.Length <= content)
                                abstracts = article.Abstract;
                            else
                                abstracts = article.Abstract.Remove(content) + "...";
                            td1.InnerHtml = b.ToString() + "<br/>" + abstracts + "<br/>" + rating + "<br/>" + specification + tag.ToString();
                            break;
                        }
                    }
                    foreach (mytrip_Articles article in ar.article.GetPosts(HttpContext.Current.Session["culture"].ToString()))
                    {
                        if (path.IndexOf("art" + article.ArticleId.ToString() + "end") == -1)
                        {
                            path += "art" + article.ArticleId.ToString() + "end";

                            //td2.MergeAttribute("style", "width: 170px;");
                            string specification = privateArticleSpecification("Articles", article.CategoryId, article.mytrip_ArticlesCategory.SubCategoryId,
                                                  article.mytrip_ArticlesCategory.Title, article.mytrip_ArticlesCategory.Path,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Title,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Path,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.SeparateBlock,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog);
                            string rating = privateArticleRating2((double)article.TotalVotes, article.ApprovedVotes, false, article.mytrip_ArticlesVotes.Count());

                            TagBuilder b = new TagBuilder("b");
                            TagBuilder a_title = new TagBuilder("a");
                            a_title.MergeAttribute("href", "/Article/View/" + article.ArticleId + "/" + article.Path);
                            a_title.InnerHtml = article.Title;
                            if (article.OnlyForRegisterUser)
                                b.InnerHtml = a_title + _Image("/Content/images/Keys.png", 20, 0, "", 0);
                            else
                                b.InnerHtml = a_title.ToString();
                            StringBuilder tag = new StringBuilder();
                            foreach (var t in article.mytrip_ArticlesTag)
                            {
                                TagBuilder a_tag = new TagBuilder("a");
                                a_tag.MergeAttribute("href", "/Article/Index/1/10/" + t.TagId + "/" + t.Path);
                                a_tag.InnerHtml = t.TagName;
                                tag.AppendLine(a_tag.ToString());
                            }
                            string abstracts = string.Empty;
                            if (article.Abstract.Length <= content)
                                abstracts = article.Abstract;
                            else
                                abstracts = article.Abstract.Remove(content) + "...";
                            td2.InnerHtml = b.ToString() + "<br/>" + abstracts + "<br/>" + rating + "<br/>" + specification + tag.ToString();
                            break;
                        }
                    }
                    tr.InnerHtml = td1.ToString() + td2.ToString();
                    result.AppendLine(tr.ToString());
                    _count++;
                }
                table.InnerHtml = result.ToString();
                return table.ToString();
            }
            #endregion
            #region 3column
            else if (param == "3column")
            {
                StringBuilder result = new StringBuilder();
                TagBuilder table = new TagBuilder("table");
                IArticleRepository ar = new IArticleRepository();
                int artcount = ar.article.GetPosts(HttpContext.Current.Session["culture"].ToString()).Count();
                if ((int)Math.Ceiling((double)artcount / 3) < count)
                    count = (int)Math.Ceiling((double)artcount / 3);
                int _count = 0;
                string path = string.Empty;
                while (_count < count)
                {
                    TagBuilder tr = new TagBuilder("tr");
                    TagBuilder td1 = new TagBuilder("td");
                    TagBuilder td2 = new TagBuilder("td");
                    TagBuilder td3 = new TagBuilder("td");
                    foreach (mytrip_Articles article in ar.article.GetPosts(HttpContext.Current.Session["culture"].ToString()))
                    {
                        if (path.IndexOf("art" + article.ArticleId.ToString() + "end") == -1)
                        {
                            path += "art" + article.ArticleId.ToString() + "end";

                            td1.MergeAttribute("style", "width: 33%;");
                            string specification = privateArticleSpecification("Articles", article.CategoryId, article.mytrip_ArticlesCategory.SubCategoryId,
                                                  article.mytrip_ArticlesCategory.Title, article.mytrip_ArticlesCategory.Path,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Title,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Path,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.SeparateBlock,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog);
                            string rating = privateArticleRating2((double)article.TotalVotes, article.ApprovedVotes, false, article.mytrip_ArticlesVotes.Count());

                            TagBuilder b = new TagBuilder("b");
                            TagBuilder a_title = new TagBuilder("a");
                            a_title.MergeAttribute("href", "/Article/View/" + article.ArticleId + "/" + article.Path);
                            a_title.InnerHtml = article.Title;
                            if (article.OnlyForRegisterUser)
                                b.InnerHtml = a_title + _Image("/Content/images/Keys.png", 20, 0, "", 0);
                            else
                                b.InnerHtml = a_title.ToString();
                            TagBuilder p = new TagBuilder("p");
                            string imageAbstract = privateImageForAbstract(article.ImageForAbstract, 80);
                            StringBuilder tag = new StringBuilder();
                            foreach (var t in article.mytrip_ArticlesTag)
                            {
                                TagBuilder a_tag = new TagBuilder("a");
                                a_tag.MergeAttribute("href", "/Article/Index/1/10/" + t.TagId + "/" + t.Path);
                                a_tag.InnerHtml = t.TagName;
                                tag.AppendLine(a_tag.ToString());
                            }
                            string abstracts = string.Empty;
                            if (article.Abstract.Length <= content)
                                abstracts = article.Abstract;
                            else
                                abstracts = article.Abstract.Remove(content) + "...";
                            td1.InnerHtml = b.ToString() + p.ToString() + imageAbstract + abstracts + "<br/>" + rating + "<br/>" + specification + tag.ToString();
                            break;
                        }
                    }
                    foreach (mytrip_Articles article in ar.article.GetPosts(HttpContext.Current.Session["culture"].ToString()))
                    {
                        if (path.IndexOf("art" + article.ArticleId.ToString() + "end") == -1)
                        {
                            path += "art" + article.ArticleId.ToString() + "end";

                            //td2.MergeAttribute("style", "width: 170px;");
                            string specification = privateArticleSpecification("Articles", article.CategoryId, article.mytrip_ArticlesCategory.SubCategoryId,
                                                  article.mytrip_ArticlesCategory.Title, article.mytrip_ArticlesCategory.Path,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Title,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Path,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.SeparateBlock,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog);
                            string rating = privateArticleRating2((double)article.TotalVotes, article.ApprovedVotes, false, article.mytrip_ArticlesVotes.Count());

                            TagBuilder b = new TagBuilder("b");
                            TagBuilder a_title = new TagBuilder("a");
                            a_title.MergeAttribute("href", "/Article/View/" + article.ArticleId + "/" + article.Path);
                            a_title.InnerHtml = article.Title;
                            if (article.OnlyForRegisterUser)
                                b.InnerHtml = a_title + _Image("/Content/images/Keys.png", 20, 0, "", 0);
                            else
                                b.InnerHtml = a_title.ToString();
                            TagBuilder p = new TagBuilder("p");
                            string imageAbstract = privateImageForAbstract(article.ImageForAbstract, 80);
                            StringBuilder tag = new StringBuilder();
                            foreach (var t in article.mytrip_ArticlesTag)
                            {
                                TagBuilder a_tag = new TagBuilder("a");
                                a_tag.MergeAttribute("href", "/Article/Index/1/10/" + t.TagId + "/" + t.Path);
                                a_tag.InnerHtml = t.TagName;
                                tag.AppendLine(a_tag.ToString());
                            }
                            string abstracts = string.Empty;
                            if (article.Abstract.Length <= content)
                                abstracts = article.Abstract;
                            else
                                abstracts = article.Abstract.Remove(content) + "...";
                            td2.InnerHtml = b.ToString() + p.ToString() + imageAbstract + abstracts + "<br/>" + rating + "<br/>" + specification + tag.ToString();
                            break;
                        }
                    }
                    foreach (mytrip_Articles article in ar.article.GetPosts(HttpContext.Current.Session["culture"].ToString()))
                    {
                        if (path.IndexOf("art" + article.ArticleId.ToString() + "end") == -1)
                        {
                            path += "art" + article.ArticleId.ToString() + "end";

                            td3.MergeAttribute("style", "width: 33%;");
                            string specification = privateArticleSpecification("Articles", article.CategoryId, article.mytrip_ArticlesCategory.SubCategoryId,
                                                  article.mytrip_ArticlesCategory.Title, article.mytrip_ArticlesCategory.Path,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Title,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Path,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.SeparateBlock,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog);
                            string rating = privateArticleRating2((double)article.TotalVotes, article.ApprovedVotes, false, article.mytrip_ArticlesVotes.Count());

                            TagBuilder b = new TagBuilder("b");
                            TagBuilder a_title = new TagBuilder("a");
                            a_title.MergeAttribute("href", "/Article/View/" + article.ArticleId + "/" + article.Path);
                            a_title.InnerHtml = article.Title;
                            if (article.OnlyForRegisterUser)
                                b.InnerHtml = a_title + _Image("/Content/images/Keys.png", 20, 0, "", 0);
                            else
                                b.InnerHtml = a_title.ToString();
                            TagBuilder p = new TagBuilder("p");
                            string imageAbstract = privateImageForAbstract(article.ImageForAbstract, 80);
                            StringBuilder tag = new StringBuilder();
                            foreach (var t in article.mytrip_ArticlesTag)
                            {
                                TagBuilder a_tag = new TagBuilder("a");
                                a_tag.MergeAttribute("href", "/Article/Index/1/10/" + t.TagId + "/" + t.Path);
                                a_tag.InnerHtml = t.TagName;
                                tag.AppendLine(a_tag.ToString());
                            }
                            string abstracts = string.Empty;
                            if (article.Abstract.Length <= content)
                                abstracts = article.Abstract;
                            else
                                abstracts = article.Abstract.Remove(content) + "...";
                            td3.InnerHtml = b.ToString() + p.ToString() + imageAbstract + abstracts + "<br/>" + rating + "<br/>" + specification + tag.ToString();
                            break;
                        }
                    }
                    tr.InnerHtml = td1.ToString() + td2.ToString() + td3.ToString();
                    result.AppendLine(tr.ToString());
                    _count++;
                }
                table.InnerHtml = result.ToString();
                return table.ToString();
            }
            #endregion
            #region small3column
            else if (param == "small3column")
            {
                StringBuilder result = new StringBuilder();
                TagBuilder table = new TagBuilder("table");
                IArticleRepository ar = new IArticleRepository();
                int artcount = ar.article.GetPosts(HttpContext.Current.Session["culture"].ToString()).Count();
                if ((int)Math.Ceiling((double)artcount / 3) < count)
                    count = (int)Math.Ceiling((double)artcount / 3);
                int _count = 0;
                string path = string.Empty;
                while (_count < count)
                {
                    TagBuilder tr = new TagBuilder("tr");
                    TagBuilder td1 = new TagBuilder("td");
                    TagBuilder td2 = new TagBuilder("td");
                    TagBuilder td3 = new TagBuilder("td");
                    foreach (mytrip_Articles article in ar.article.GetPosts(HttpContext.Current.Session["culture"].ToString()))
                    {
                        if (path.IndexOf("art" + article.ArticleId.ToString() + "end") == -1)
                        {
                            path += "art" + article.ArticleId.ToString() + "end";

                            td1.MergeAttribute("style", "width: 33%;");
                            string specification = privateArticleSpecification("Articles", article.CategoryId, article.mytrip_ArticlesCategory.SubCategoryId,
                                                  article.mytrip_ArticlesCategory.Title, article.mytrip_ArticlesCategory.Path,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Title,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Path,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.SeparateBlock,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog);
                            string rating = privateArticleRating2((double)article.TotalVotes, article.ApprovedVotes, false, article.mytrip_ArticlesVotes.Count());

                            TagBuilder b = new TagBuilder("b");
                            TagBuilder a_title = new TagBuilder("a");
                            a_title.MergeAttribute("href", "/Article/View/" + article.ArticleId + "/" + article.Path);
                            a_title.InnerHtml = article.Title;
                            if (article.OnlyForRegisterUser)
                                b.InnerHtml = a_title + _Image("/Content/images/Keys.png", 20, 0, "", 0);
                            else
                                b.InnerHtml = a_title.ToString();
                            StringBuilder tag = new StringBuilder();
                            foreach (var t in article.mytrip_ArticlesTag)
                            {
                                TagBuilder a_tag = new TagBuilder("a");
                                a_tag.MergeAttribute("href", "/Article/Index/1/10/" + t.TagId + "/" + t.Path);
                                a_tag.InnerHtml = t.TagName;
                                tag.AppendLine(a_tag.ToString());
                            }
                            string abstracts = string.Empty;
                            if (article.Abstract.Length <= content)
                                abstracts = article.Abstract;
                            else
                                abstracts = article.Abstract.Remove(content) + "...";
                            td1.InnerHtml = b.ToString() + "<br/>" + abstracts + "<br/>" + rating + "<br/>" + specification + tag.ToString();
                            break;
                        }
                    }
                    foreach (mytrip_Articles article in ar.article.GetPosts(HttpContext.Current.Session["culture"].ToString()))
                    {
                        if (path.IndexOf("art" + article.ArticleId.ToString() + "end") == -1)
                        {
                            path += "art" + article.ArticleId.ToString() + "end";

                            //td2.MergeAttribute("style", "width: 170px;");
                            string specification = privateArticleSpecification("Articles", article.CategoryId, article.mytrip_ArticlesCategory.SubCategoryId,
                                                  article.mytrip_ArticlesCategory.Title, article.mytrip_ArticlesCategory.Path,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Title,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Path,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.SeparateBlock,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog);
                            string rating = privateArticleRating2((double)article.TotalVotes, article.ApprovedVotes, false, article.mytrip_ArticlesVotes.Count());

                            TagBuilder b = new TagBuilder("b");
                            TagBuilder a_title = new TagBuilder("a");
                            a_title.MergeAttribute("href", "/Article/View/" + article.ArticleId + "/" + article.Path);
                            a_title.InnerHtml = article.Title;
                            if (article.OnlyForRegisterUser)
                                b.InnerHtml = a_title + _Image("/Content/images/Keys.png", 20, 0, "", 0);
                            else
                                b.InnerHtml = a_title.ToString();
                            StringBuilder tag = new StringBuilder();
                            foreach (var t in article.mytrip_ArticlesTag)
                            {
                                TagBuilder a_tag = new TagBuilder("a");
                                a_tag.MergeAttribute("href", "/Article/Index/1/10/" + t.TagId + "/" + t.Path);
                                a_tag.InnerHtml = t.TagName;
                                tag.AppendLine(a_tag.ToString());
                            }
                            string abstracts = string.Empty;
                            if (article.Abstract.Length <= content)
                                abstracts = article.Abstract;
                            else
                                abstracts = article.Abstract.Remove(content) + "...";
                            td2.InnerHtml = b.ToString() + "<br/>" + abstracts + "<br/>" + rating + "<br/>" + specification + tag.ToString();
                            break;
                        }
                    }
                    foreach (mytrip_Articles article in ar.article.GetPosts(HttpContext.Current.Session["culture"].ToString()))
                    {
                        if (path.IndexOf("art" + article.ArticleId.ToString() + "end") == -1)
                        {
                            path += "art" + article.ArticleId.ToString() + "end";

                            td3.MergeAttribute("style", "width: 33%;");
                            string specification = privateArticleSpecification("Articles", article.CategoryId, article.mytrip_ArticlesCategory.SubCategoryId,
                                                  article.mytrip_ArticlesCategory.Title, article.mytrip_ArticlesCategory.Path,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Title,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Path,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.SeparateBlock,
                                                  article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog);
                            string rating = privateArticleRating2((double)article.TotalVotes, article.ApprovedVotes, false, article.mytrip_ArticlesVotes.Count());

                            TagBuilder b = new TagBuilder("b");
                            TagBuilder a_title = new TagBuilder("a");
                            a_title.MergeAttribute("href", "/Article/View/" + article.ArticleId + "/" + article.Path);
                            a_title.InnerHtml = article.Title;
                            if (article.OnlyForRegisterUser)
                                b.InnerHtml = a_title + _Image("/Content/images/Keys.png", 20, 0, "", 0);
                            else
                                b.InnerHtml = a_title.ToString();
                            StringBuilder tag = new StringBuilder();
                            foreach (var t in article.mytrip_ArticlesTag)
                            {
                                TagBuilder a_tag = new TagBuilder("a");
                                a_tag.MergeAttribute("href", "/Article/Index/1/10/" + t.TagId + "/" + t.Path);
                                a_tag.InnerHtml = t.TagName;
                                tag.AppendLine(a_tag.ToString());
                            }
                            string abstracts = string.Empty;
                            if (article.Abstract.Length <= content)
                                abstracts = article.Abstract;
                            else
                                abstracts = article.Abstract.Remove(content) + "...";
                            td3.InnerHtml = b.ToString() + "<br/>" + abstracts + "<br/>" + rating + "<br/>" + specification + tag.ToString();
                            break;
                        }
                    }
                    tr.InnerHtml = td1.ToString() + td2.ToString() + td3.ToString();
                    result.AppendLine(tr.ToString());
                    _count++;
                }
                table.InnerHtml = result.ToString();
                return table.ToString();
            }
            #endregion
            else
            {
                return string.Empty;
            }
        }
        public static string ArticleFromCategoryHomeContent(this HtmlHelper html, string param, int count, int content, int categoryId, bool culture)
        {
            IArticleRepository ar = new IArticleRepository();
            bool allculture = ar.category.GetCategory(categoryId).AllCulture;
            string _culture = ar.category.GetCategory(categoryId).Culture;
            if (culture && !allculture && _culture != HttpContext.Current.Session["culture"].ToString())
            { return string.Empty; }
            else
            {
                #region AsArticles
                if (param == "AsArticles")
                {
                    StringBuilder result = new StringBuilder();
                    TagBuilder table = new TagBuilder("table");
                    int _count = 0;
                    foreach (mytrip_Articles article in ar.article.GetArticlesOpenedByCategory(categoryId))
                    {
                        TagBuilder tr = new TagBuilder("tr");
                        TagBuilder td1 = new TagBuilder("td");
                        td1.MergeAttribute("style", "width: 170px;");
                        td1.InnerHtml = privateArticleSpecification("Articles", article.CategoryId, article.mytrip_ArticlesCategory.SubCategoryId,
                                              article.mytrip_ArticlesCategory.Title, article.mytrip_ArticlesCategory.Path,
                                              article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Title,
                                              article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Path,
                                              article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.SeparateBlock,
                                              article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog,
                                              article.UserName, article.Views,
                                              article.ApprovedComment,
                                              article.mytrip_ArticlesComments.Count(), article.CreateDate);
                        TagBuilder td2 = new TagBuilder("td");
                        TagBuilder div = new TagBuilder("div");
                        div.MergeAttribute("style", "position: relative; float: right");
                        div.InnerHtml = privateArticleRating((double)article.TotalVotes, article.ApprovedVotes, false, article.mytrip_ArticlesVotes.Count());
                        TagBuilder b = new TagBuilder("b");
                        TagBuilder a_title = new TagBuilder("a");
                        a_title.MergeAttribute("href", "/Article/View/" + article.ArticleId + "/" + article.Path);
                        a_title.InnerHtml = article.Title;
                        if (article.OnlyForRegisterUser)
                            b.InnerHtml = a_title + _Image("/Content/images/Keys.png", 20, 0, "", 0);
                        else
                            b.InnerHtml = a_title.ToString();
                        TagBuilder p = new TagBuilder("p");
                        string imageAbstract = privateImageForAbstract(article.ImageForAbstract, 150);
                        StringBuilder tag = new StringBuilder();
                        foreach (var t in article.mytrip_ArticlesTag)
                        {
                            TagBuilder a_tag = new TagBuilder("a");
                            a_tag.MergeAttribute("href", "/Article/Index/1/10/" + t.TagId + "/" + t.Path);
                            a_tag.InnerHtml = t.TagName;
                            tag.AppendLine(a_tag.ToString());
                        }
                        string abstracts = string.Empty;
                        if (article.Abstract.Length <= content)
                            abstracts = article.Abstract;
                        else
                            abstracts = article.Abstract.Remove(content) + "...";
                        td2.InnerHtml = div.ToString() + b.ToString() + p.ToString() + imageAbstract + abstracts + "<br/>" + tag.ToString();
                        tr.InnerHtml = td1.ToString() + td2.ToString();
                        result.AppendLine(tr.ToString());
                        _count++;
                        if (_count >= count)
                            break;
                    }
                    table.InnerHtml = result.ToString();
                    return table.ToString();
                }
                #endregion
                #region small1column
                else if (param == "small1column")
                {
                    StringBuilder result = new StringBuilder();
                    TagBuilder table = new TagBuilder("table");
                    int artcount = ar.article.GetArticlesOpenedByCategory(categoryId).Count();
                    int _count = 0;
                    TagBuilder tr = new TagBuilder("tr");
                    TagBuilder td1 = new TagBuilder("td");
                    foreach (mytrip_Articles article in ar.article.GetArticlesOpenedByCategory(categoryId))
                    {


                        //td1.MergeAttribute("style", "width: 50%;");
                        string specification = privateArticleSpecification("Articles", article.CategoryId, article.mytrip_ArticlesCategory.SubCategoryId,
                                              article.mytrip_ArticlesCategory.Title, article.mytrip_ArticlesCategory.Path,
                                              article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Title,
                                              article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Path,
                                              article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.SeparateBlock,
                                              article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog);
                        string rating = privateArticleRating2((double)article.TotalVotes, article.ApprovedVotes, false, article.mytrip_ArticlesVotes.Count());

                        TagBuilder b = new TagBuilder("b");
                        TagBuilder a_title = new TagBuilder("a");
                        a_title.MergeAttribute("href", "/Article/View/" + article.ArticleId + "/" + article.Path);
                        a_title.InnerHtml = article.Title;
                        if (article.OnlyForRegisterUser)
                            b.InnerHtml = a_title + _Image("/Content/images/Keys.png", 20, 0, "", 0);
                        else
                            b.InnerHtml = a_title.ToString();
                        StringBuilder tag = new StringBuilder();
                        foreach (var t in article.mytrip_ArticlesTag)
                        {
                            TagBuilder a_tag = new TagBuilder("a");
                            a_tag.MergeAttribute("href", "/Article/Index/1/10/" + t.TagId + "/" + t.Path);
                            a_tag.InnerHtml = t.TagName;
                            tag.AppendLine(a_tag.ToString());
                        }
                        string abstracts = string.Empty;
                        if (article.Abstract.Length <= content)
                            abstracts = article.Abstract;
                        else
                            abstracts = article.Abstract.Remove(content) + "...";
                        td1.InnerHtml = b.ToString() + "<br/>" + abstracts + "<br/>" + rating + "<br/>" + specification + tag.ToString();



                        tr.InnerHtml = td1.ToString();
                        result.AppendLine(tr.ToString());
                        _count++;
                        if (_count >= count)
                            break;
                    }
                    table.InnerHtml = result.ToString();
                    return table.ToString();
                }
                #endregion
                #region 1column
                else if (param == "1column")
                {
                    StringBuilder result = new StringBuilder();
                    TagBuilder table = new TagBuilder("table");
                    int artcount = ar.article.GetArticlesOpenedByCategory(categoryId).Count();
                    int _count = 0;
                    TagBuilder tr = new TagBuilder("tr");
                    TagBuilder td1 = new TagBuilder("td");
                    foreach (mytrip_Articles article in ar.article.GetArticlesOpenedByCategory(categoryId))
                    {


                        //td1.MergeAttribute("style", "width: 50%;");
                        string specification = privateArticleSpecification("Articles", article.CategoryId, article.mytrip_ArticlesCategory.SubCategoryId,
                                              article.mytrip_ArticlesCategory.Title, article.mytrip_ArticlesCategory.Path,
                                              article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Title,
                                              article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Path,
                                              article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.SeparateBlock,
                                              article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog);
                        string rating = privateArticleRating2((double)article.TotalVotes, article.ApprovedVotes, false, article.mytrip_ArticlesVotes.Count());

                        TagBuilder b = new TagBuilder("b");
                        TagBuilder a_title = new TagBuilder("a");
                        a_title.MergeAttribute("href", "/Article/View/" + article.ArticleId + "/" + article.Path);
                        a_title.InnerHtml = article.Title;
                        if (article.OnlyForRegisterUser)
                            b.InnerHtml = a_title + _Image("/Content/images/Keys.png", 20, 0, "", 0);
                        else
                            b.InnerHtml = a_title.ToString();
                        TagBuilder p = new TagBuilder("p");
                        string imageAbstract = privateImageForAbstract(article.ImageForAbstract, 150);
                        StringBuilder tag = new StringBuilder();
                        foreach (var t in article.mytrip_ArticlesTag)
                        {
                            TagBuilder a_tag = new TagBuilder("a");
                            a_tag.MergeAttribute("href", "/Article/Index/1/10/" + t.TagId + "/" + t.Path);
                            a_tag.InnerHtml = t.TagName;
                            tag.AppendLine(a_tag.ToString());
                        }
                        string abstracts = string.Empty;
                        if (article.Abstract.Length <= content)
                            abstracts = article.Abstract;
                        else
                            abstracts = article.Abstract.Remove(content) + "...";
                        td1.InnerHtml = b.ToString() + p.ToString() + imageAbstract + abstracts + "<br/>" + rating + "<br/>" + specification + tag.ToString();



                        tr.InnerHtml = td1.ToString();
                        result.AppendLine(tr.ToString());
                        _count++;
                        if (_count >= count)
                            break;
                    }
                    table.InnerHtml = result.ToString();
                    return table.ToString();
                }
                #endregion
                #region 2column
                else if (param == "2column")
                {
                    StringBuilder result = new StringBuilder();
                    TagBuilder table = new TagBuilder("table");
                    int artcount = ar.article.GetArticlesOpenedByCategory(categoryId).Count();
                    if ((int)Math.Ceiling((double)artcount / 2) < count)
                        count = (int)Math.Ceiling((double)artcount / 2);
                    int _count = 0;
                    string path = string.Empty;
                    while (_count < count)
                    {
                        TagBuilder tr = new TagBuilder("tr");
                        TagBuilder td1 = new TagBuilder("td");
                        TagBuilder td2 = new TagBuilder("td");
                        foreach (mytrip_Articles article in ar.article.GetArticlesOpenedByCategory(categoryId))
                        {
                            if (path.IndexOf("art" + article.ArticleId.ToString() + "end") == -1)
                            {
                                path += "art" + article.ArticleId.ToString() + "end";

                                td1.MergeAttribute("style", "width: 50%;");
                                string specification = privateArticleSpecification("Articles", article.CategoryId, article.mytrip_ArticlesCategory.SubCategoryId,
                                                      article.mytrip_ArticlesCategory.Title, article.mytrip_ArticlesCategory.Path,
                                                      article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Title,
                                                      article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Path,
                                                      article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.SeparateBlock,
                                                      article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog);
                                string rating = privateArticleRating2((double)article.TotalVotes, article.ApprovedVotes, false, article.mytrip_ArticlesVotes.Count());

                                TagBuilder b = new TagBuilder("b");
                                TagBuilder a_title = new TagBuilder("a");
                                a_title.MergeAttribute("href", "/Article/View/" + article.ArticleId + "/" + article.Path);
                                a_title.InnerHtml = article.Title;
                                if (article.OnlyForRegisterUser)
                                    b.InnerHtml = a_title + _Image("/Content/images/Keys.png", 20, 0, "", 0);
                                else
                                    b.InnerHtml = a_title.ToString();
                                TagBuilder p = new TagBuilder("p");
                                string imageAbstract = privateImageForAbstract(article.ImageForAbstract, 150);
                                StringBuilder tag = new StringBuilder();
                                foreach (var t in article.mytrip_ArticlesTag)
                                {
                                    TagBuilder a_tag = new TagBuilder("a");
                                    a_tag.MergeAttribute("href", "/Article/Index/1/10/" + t.TagId + "/" + t.Path);
                                    a_tag.InnerHtml = t.TagName;
                                    tag.AppendLine(a_tag.ToString());
                                }
                                string abstracts = string.Empty;
                                if (article.Abstract.Length <= content)
                                    abstracts = article.Abstract;
                                else
                                    abstracts = article.Abstract.Remove(content) + "...";
                                td1.InnerHtml = b.ToString() + p.ToString() + imageAbstract + abstracts + "<br/>" + rating + "<br/>" + specification + tag.ToString();
                                break;
                            }
                        }
                        foreach (mytrip_Articles article in ar.article.GetArticlesOpenedByCategory(categoryId))
                        {
                            if (path.IndexOf("art" + article.ArticleId.ToString() + "end") == -1)
                            {
                                path += "art" + article.ArticleId.ToString() + "end";

                                //td2.MergeAttribute("style", "width: 170px;");
                                string specification = privateArticleSpecification("Articles", article.CategoryId, article.mytrip_ArticlesCategory.SubCategoryId,
                                                      article.mytrip_ArticlesCategory.Title, article.mytrip_ArticlesCategory.Path,
                                                      article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Title,
                                                      article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Path,
                                                      article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.SeparateBlock,
                                                      article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog);
                                string rating = privateArticleRating2((double)article.TotalVotes, article.ApprovedVotes, false, article.mytrip_ArticlesVotes.Count());

                                TagBuilder b = new TagBuilder("b");
                                TagBuilder a_title = new TagBuilder("a");
                                a_title.MergeAttribute("href", "/Article/View/" + article.ArticleId + "/" + article.Path);
                                a_title.InnerHtml = article.Title;
                                if (article.OnlyForRegisterUser)
                                    b.InnerHtml = a_title + _Image("/Content/images/Keys.png", 20, 0, "", 0);
                                else
                                    b.InnerHtml = a_title.ToString();
                                TagBuilder p = new TagBuilder("p");
                                string imageAbstract = privateImageForAbstract(article.ImageForAbstract, 150);
                                StringBuilder tag = new StringBuilder();
                                foreach (var t in article.mytrip_ArticlesTag)
                                {
                                    TagBuilder a_tag = new TagBuilder("a");
                                    a_tag.MergeAttribute("href", "/Article/Index/1/10/" + t.TagId + "/" + t.Path);
                                    a_tag.InnerHtml = t.TagName;
                                    tag.AppendLine(a_tag.ToString());
                                }
                                string abstracts = string.Empty;
                                if (article.Abstract.Length <= content)
                                    abstracts = article.Abstract;
                                else
                                    abstracts = article.Abstract.Remove(content) + "...";
                                td2.InnerHtml = b.ToString() + p.ToString() + imageAbstract + abstracts + "<br/>" + rating + "<br/>" + specification + tag.ToString();
                                break;
                            }
                        }
                        tr.InnerHtml = td1.ToString() + td2.ToString();
                        result.AppendLine(tr.ToString());
                        _count++;
                    }
                    table.InnerHtml = result.ToString();
                    return table.ToString();
                }
                #endregion
                #region small2column
                else if (param == "small2column")
                {
                    StringBuilder result = new StringBuilder();
                    TagBuilder table = new TagBuilder("table");
                    int artcount = ar.article.GetArticlesOpenedByCategory(categoryId).Count();
                    if ((int)Math.Ceiling((double)artcount / 2) < count)
                        count = (int)Math.Ceiling((double)artcount / 2);
                    int _count = 0;
                    string path = string.Empty;
                    while (_count < count)
                    {
                        TagBuilder tr = new TagBuilder("tr");
                        TagBuilder td1 = new TagBuilder("td");
                        TagBuilder td2 = new TagBuilder("td");
                        foreach (mytrip_Articles article in ar.article.GetArticlesOpenedByCategory(categoryId))
                        {
                            if (path.IndexOf("art" + article.ArticleId.ToString() + "end") == -1)
                            {
                                path += "art" + article.ArticleId.ToString() + "end";

                                td1.MergeAttribute("style", "width: 50%;");
                                string specification = privateArticleSpecification("Articles", article.CategoryId, article.mytrip_ArticlesCategory.SubCategoryId,
                                                      article.mytrip_ArticlesCategory.Title, article.mytrip_ArticlesCategory.Path,
                                                      article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Title,
                                                      article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Path,
                                                      article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.SeparateBlock,
                                                      article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog);
                                string rating = privateArticleRating2((double)article.TotalVotes, article.ApprovedVotes, false, article.mytrip_ArticlesVotes.Count());

                                TagBuilder b = new TagBuilder("b");
                                TagBuilder a_title = new TagBuilder("a");
                                a_title.MergeAttribute("href", "/Article/View/" + article.ArticleId + "/" + article.Path);
                                a_title.InnerHtml = article.Title;
                                if (article.OnlyForRegisterUser)
                                    b.InnerHtml = a_title + _Image("/Content/images/Keys.png", 20, 0, "", 0);
                                else
                                    b.InnerHtml = a_title.ToString();
                                StringBuilder tag = new StringBuilder();
                                foreach (var t in article.mytrip_ArticlesTag)
                                {
                                    TagBuilder a_tag = new TagBuilder("a");
                                    a_tag.MergeAttribute("href", "/Article/Index/1/10/" + t.TagId + "/" + t.Path);
                                    a_tag.InnerHtml = t.TagName;
                                    tag.AppendLine(a_tag.ToString());
                                }
                                string abstracts = string.Empty;
                                if (article.Abstract.Length <= content)
                                    abstracts = article.Abstract;
                                else
                                    abstracts = article.Abstract.Remove(content) + "...";
                                td1.InnerHtml = b.ToString() + "<br/>" + abstracts + "<br/>" + rating + "<br/>" + specification + tag.ToString();
                                break;
                            }
                        }
                        foreach (mytrip_Articles article in ar.article.GetArticlesOpenedByCategory(categoryId))
                        {
                            if (path.IndexOf("art" + article.ArticleId.ToString() + "end") == -1)
                            {
                                path += "art" + article.ArticleId.ToString() + "end";

                                //td2.MergeAttribute("style", "width: 170px;");
                                string specification = privateArticleSpecification("Articles", article.CategoryId, article.mytrip_ArticlesCategory.SubCategoryId,
                                                      article.mytrip_ArticlesCategory.Title, article.mytrip_ArticlesCategory.Path,
                                                      article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Title,
                                                      article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Path,
                                                      article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.SeparateBlock,
                                                      article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog);
                                string rating = privateArticleRating2((double)article.TotalVotes, article.ApprovedVotes, false, article.mytrip_ArticlesVotes.Count());

                                TagBuilder b = new TagBuilder("b");
                                TagBuilder a_title = new TagBuilder("a");
                                a_title.MergeAttribute("href", "/Article/View/" + article.ArticleId + "/" + article.Path);
                                a_title.InnerHtml = article.Title;
                                if (article.OnlyForRegisterUser)
                                    b.InnerHtml = a_title + _Image("/Content/images/Keys.png", 20, 0, "", 0);
                                else
                                    b.InnerHtml = a_title.ToString();
                                StringBuilder tag = new StringBuilder();
                                foreach (var t in article.mytrip_ArticlesTag)
                                {
                                    TagBuilder a_tag = new TagBuilder("a");
                                    a_tag.MergeAttribute("href", "/Article/Index/1/10/" + t.TagId + "/" + t.Path);
                                    a_tag.InnerHtml = t.TagName;
                                    tag.AppendLine(a_tag.ToString());
                                }
                                string abstracts = string.Empty;
                                if (article.Abstract.Length <= content)
                                    abstracts = article.Abstract;
                                else
                                    abstracts = article.Abstract.Remove(content) + "...";
                                td2.InnerHtml = b.ToString() + "<br/>" + abstracts + "<br/>" + rating + "<br/>" + specification + tag.ToString();
                                break;
                            }
                        }
                        tr.InnerHtml = td1.ToString() + td2.ToString();
                        result.AppendLine(tr.ToString());
                        _count++;
                    }
                    table.InnerHtml = result.ToString();
                    return table.ToString();
                }
                #endregion
                #region 3column
                else if (param == "3column")
                {
                    StringBuilder result = new StringBuilder();
                    TagBuilder table = new TagBuilder("table");
                    int artcount = ar.article.GetArticlesOpenedByCategory(categoryId).Count();
                    if ((int)Math.Ceiling((double)artcount / 3) < count)
                        count = (int)Math.Ceiling((double)artcount / 3);
                    int _count = 0;
                    string path = string.Empty;
                    while (_count < count)
                    {
                        TagBuilder tr = new TagBuilder("tr");
                        TagBuilder td1 = new TagBuilder("td");
                        TagBuilder td2 = new TagBuilder("td");
                        TagBuilder td3 = new TagBuilder("td");
                        foreach (mytrip_Articles article in ar.article.GetArticlesOpenedByCategory(categoryId))
                        {
                            if (path.IndexOf("art" + article.ArticleId.ToString() + "end") == -1)
                            {
                                path += "art" + article.ArticleId.ToString() + "end";

                                td1.MergeAttribute("style", "width: 33%;");
                                string specification = privateArticleSpecification("Articles", article.CategoryId, article.mytrip_ArticlesCategory.SubCategoryId,
                                                      article.mytrip_ArticlesCategory.Title, article.mytrip_ArticlesCategory.Path,
                                                      article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Title,
                                                      article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Path,
                                                      article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.SeparateBlock,
                                                      article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog);
                                string rating = privateArticleRating2((double)article.TotalVotes, article.ApprovedVotes, false, article.mytrip_ArticlesVotes.Count());

                                TagBuilder b = new TagBuilder("b");
                                TagBuilder a_title = new TagBuilder("a");
                                a_title.MergeAttribute("href", "/Article/View/" + article.ArticleId + "/" + article.Path);
                                a_title.InnerHtml = article.Title;
                                if (article.OnlyForRegisterUser)
                                    b.InnerHtml = a_title + _Image("/Content/images/Keys.png", 20, 0, "", 0);
                                else
                                    b.InnerHtml = a_title.ToString();
                                TagBuilder p = new TagBuilder("p");
                                string imageAbstract = privateImageForAbstract(article.ImageForAbstract, 80);
                                StringBuilder tag = new StringBuilder();
                                foreach (var t in article.mytrip_ArticlesTag)
                                {
                                    TagBuilder a_tag = new TagBuilder("a");
                                    a_tag.MergeAttribute("href", "/Article/Index/1/10/" + t.TagId + "/" + t.Path);
                                    a_tag.InnerHtml = t.TagName;
                                    tag.AppendLine(a_tag.ToString());
                                }
                                string abstracts = string.Empty;
                                if (article.Abstract.Length <= content)
                                    abstracts = article.Abstract;
                                else
                                    abstracts = article.Abstract.Remove(content) + "...";
                                td1.InnerHtml = b.ToString() + p.ToString() + imageAbstract + abstracts + "<br/>" + rating + "<br/>" + specification + tag.ToString();
                                break;
                            }
                        }
                        foreach (mytrip_Articles article in ar.article.GetArticlesOpenedByCategory(categoryId))
                        {
                            if (path.IndexOf("art" + article.ArticleId.ToString() + "end") == -1)
                            {
                                path += "art" + article.ArticleId.ToString() + "end";

                                //td2.MergeAttribute("style", "width: 170px;");
                                string specification = privateArticleSpecification("Articles", article.CategoryId, article.mytrip_ArticlesCategory.SubCategoryId,
                                                      article.mytrip_ArticlesCategory.Title, article.mytrip_ArticlesCategory.Path,
                                                      article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Title,
                                                      article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Path,
                                                      article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.SeparateBlock,
                                                      article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog);
                                string rating = privateArticleRating2((double)article.TotalVotes, article.ApprovedVotes, false, article.mytrip_ArticlesVotes.Count());

                                TagBuilder b = new TagBuilder("b");
                                TagBuilder a_title = new TagBuilder("a");
                                a_title.MergeAttribute("href", "/Article/View/" + article.ArticleId + "/" + article.Path);
                                a_title.InnerHtml = article.Title;
                                if (article.OnlyForRegisterUser)
                                    b.InnerHtml = a_title + _Image("/Content/images/Keys.png", 20, 0, "", 0);
                                else
                                    b.InnerHtml = a_title.ToString();
                                TagBuilder p = new TagBuilder("p");
                                string imageAbstract = privateImageForAbstract(article.ImageForAbstract, 80);
                                StringBuilder tag = new StringBuilder();
                                foreach (var t in article.mytrip_ArticlesTag)
                                {
                                    TagBuilder a_tag = new TagBuilder("a");
                                    a_tag.MergeAttribute("href", "/Article/Index/1/10/" + t.TagId + "/" + t.Path);
                                    a_tag.InnerHtml = t.TagName;
                                    tag.AppendLine(a_tag.ToString());
                                }
                                string abstracts = string.Empty;
                                if (article.Abstract.Length <= content)
                                    abstracts = article.Abstract;
                                else
                                    abstracts = article.Abstract.Remove(content) + "...";
                                td2.InnerHtml = b.ToString() + p.ToString() + imageAbstract + abstracts + "<br/>" + rating + "<br/>" + specification + tag.ToString();
                                break;
                            }
                        }
                        foreach (mytrip_Articles article in ar.article.GetArticlesOpenedByCategory(categoryId))
                        {
                            if (path.IndexOf("art" + article.ArticleId.ToString() + "end") == -1)
                            {
                                path += "art" + article.ArticleId.ToString() + "end";

                                td3.MergeAttribute("style", "width: 33%;");
                                string specification = privateArticleSpecification("Articles", article.CategoryId, article.mytrip_ArticlesCategory.SubCategoryId,
                                                      article.mytrip_ArticlesCategory.Title, article.mytrip_ArticlesCategory.Path,
                                                      article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Title,
                                                      article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Path,
                                                      article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.SeparateBlock,
                                                      article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog);
                                string rating = privateArticleRating2((double)article.TotalVotes, article.ApprovedVotes, false, article.mytrip_ArticlesVotes.Count());

                                TagBuilder b = new TagBuilder("b");
                                TagBuilder a_title = new TagBuilder("a");
                                a_title.MergeAttribute("href", "/Article/View/" + article.ArticleId + "/" + article.Path);
                                a_title.InnerHtml = article.Title;
                                if (article.OnlyForRegisterUser)
                                    b.InnerHtml = a_title + _Image("/Content/images/Keys.png", 20, 0, "", 0);
                                else
                                    b.InnerHtml = a_title.ToString();
                                TagBuilder p = new TagBuilder("p");
                                string imageAbstract = privateImageForAbstract(article.ImageForAbstract, 80);
                                StringBuilder tag = new StringBuilder();
                                foreach (var t in article.mytrip_ArticlesTag)
                                {
                                    TagBuilder a_tag = new TagBuilder("a");
                                    a_tag.MergeAttribute("href", "/Article/Index/1/10/" + t.TagId + "/" + t.Path);
                                    a_tag.InnerHtml = t.TagName;
                                    tag.AppendLine(a_tag.ToString());
                                }
                                string abstracts = string.Empty;
                                if (article.Abstract.Length <= content)
                                    abstracts = article.Abstract;
                                else
                                    abstracts = article.Abstract.Remove(content) + "...";
                                td3.InnerHtml = b.ToString() + p.ToString() + imageAbstract + abstracts + "<br/>" + rating + "<br/>" + specification + tag.ToString();
                                break;
                            }
                        }
                        tr.InnerHtml = td1.ToString() + td2.ToString() + td3.ToString();
                        result.AppendLine(tr.ToString());
                        _count++;
                    }
                    table.InnerHtml = result.ToString();
                    return table.ToString();
                }
                #endregion
                #region small3column
                else if (param == "small3column")
                {
                    StringBuilder result = new StringBuilder();
                    TagBuilder table = new TagBuilder("table");
                    int artcount = ar.article.GetArticlesOpenedByCategory(categoryId).Count();
                    if ((int)Math.Ceiling((double)artcount / 3) < count)
                        count = (int)Math.Ceiling((double)artcount / 3);
                    int _count = 0;
                    string path = string.Empty;
                    while (_count < count)
                    {
                        TagBuilder tr = new TagBuilder("tr");
                        TagBuilder td1 = new TagBuilder("td");
                        TagBuilder td2 = new TagBuilder("td");
                        TagBuilder td3 = new TagBuilder("td");
                        foreach (mytrip_Articles article in ar.article.GetArticlesOpenedByCategory(categoryId))
                        {
                            if (path.IndexOf("art" + article.ArticleId.ToString() + "end") == -1)
                            {
                                path += "art" + article.ArticleId.ToString() + "end";

                                td1.MergeAttribute("style", "width: 33%;");
                                string specification = privateArticleSpecification("Articles", article.CategoryId, article.mytrip_ArticlesCategory.SubCategoryId,
                                                      article.mytrip_ArticlesCategory.Title, article.mytrip_ArticlesCategory.Path,
                                                      article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Title,
                                                      article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Path,
                                                      article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.SeparateBlock,
                                                      article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog);
                                string rating = privateArticleRating2((double)article.TotalVotes, article.ApprovedVotes, false, article.mytrip_ArticlesVotes.Count());

                                TagBuilder b = new TagBuilder("b");
                                TagBuilder a_title = new TagBuilder("a");
                                a_title.MergeAttribute("href", "/Article/View/" + article.ArticleId + "/" + article.Path);
                                a_title.InnerHtml = article.Title;
                                if (article.OnlyForRegisterUser)
                                    b.InnerHtml = a_title + _Image("/Content/images/Keys.png", 20, 0, "", 0);
                                else
                                    b.InnerHtml = a_title.ToString();
                                StringBuilder tag = new StringBuilder();
                                foreach (var t in article.mytrip_ArticlesTag)
                                {
                                    TagBuilder a_tag = new TagBuilder("a");
                                    a_tag.MergeAttribute("href", "/Article/Index/1/10/" + t.TagId + "/" + t.Path);
                                    a_tag.InnerHtml = t.TagName;
                                    tag.AppendLine(a_tag.ToString());
                                }
                                string abstracts = string.Empty;
                                if (article.Abstract.Length <= content)
                                    abstracts = article.Abstract;
                                else
                                    abstracts = article.Abstract.Remove(content) + "...";
                                td1.InnerHtml = b.ToString() + "<br/>" + abstracts + "<br/>" + rating + "<br/>" + specification + tag.ToString();
                                break;
                            }
                        }
                        foreach (mytrip_Articles article in ar.article.GetArticlesOpenedByCategory(categoryId))
                        {
                            if (path.IndexOf("art" + article.ArticleId.ToString() + "end") == -1)
                            {
                                path += "art" + article.ArticleId.ToString() + "end";

                                //td2.MergeAttribute("style", "width: 170px;");
                                string specification = privateArticleSpecification("Articles", article.CategoryId, article.mytrip_ArticlesCategory.SubCategoryId,
                                                      article.mytrip_ArticlesCategory.Title, article.mytrip_ArticlesCategory.Path,
                                                      article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Title,
                                                      article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Path,
                                                      article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.SeparateBlock,
                                                      article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog);
                                string rating = privateArticleRating2((double)article.TotalVotes, article.ApprovedVotes, false, article.mytrip_ArticlesVotes.Count());

                                TagBuilder b = new TagBuilder("b");
                                TagBuilder a_title = new TagBuilder("a");
                                a_title.MergeAttribute("href", "/Article/View/" + article.ArticleId + "/" + article.Path);
                                a_title.InnerHtml = article.Title;
                                if (article.OnlyForRegisterUser)
                                    b.InnerHtml = a_title + _Image("/Content/images/Keys.png", 20, 0, "", 0);
                                else
                                    b.InnerHtml = a_title.ToString();
                                StringBuilder tag = new StringBuilder();
                                foreach (var t in article.mytrip_ArticlesTag)
                                {
                                    TagBuilder a_tag = new TagBuilder("a");
                                    a_tag.MergeAttribute("href", "/Article/Index/1/10/" + t.TagId + "/" + t.Path);
                                    a_tag.InnerHtml = t.TagName;
                                    tag.AppendLine(a_tag.ToString());
                                }
                                string abstracts = string.Empty;
                                if (article.Abstract.Length <= content)
                                    abstracts = article.Abstract;
                                else
                                    abstracts = article.Abstract.Remove(content) + "...";
                                td2.InnerHtml = b.ToString() + "<br/>" + abstracts + "<br/>" + rating + "<br/>" + specification + tag.ToString();
                                break;
                            }
                        }
                        foreach (mytrip_Articles article in ar.article.GetArticlesOpenedByCategory(categoryId))
                        {
                            if (path.IndexOf("art" + article.ArticleId.ToString() + "end") == -1)
                            {
                                path += "art" + article.ArticleId.ToString() + "end";

                                td3.MergeAttribute("style", "width: 33%;");
                                string specification = privateArticleSpecification("Articles", article.CategoryId, article.mytrip_ArticlesCategory.SubCategoryId,
                                                      article.mytrip_ArticlesCategory.Title, article.mytrip_ArticlesCategory.Path,
                                                      article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Title,
                                                      article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Path,
                                                      article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.SeparateBlock,
                                                      article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog);
                                string rating = privateArticleRating2((double)article.TotalVotes, article.ApprovedVotes, false, article.mytrip_ArticlesVotes.Count());

                                TagBuilder b = new TagBuilder("b");
                                TagBuilder a_title = new TagBuilder("a");
                                a_title.MergeAttribute("href", "/Article/View/" + article.ArticleId + "/" + article.Path);
                                a_title.InnerHtml = article.Title;
                                if (article.OnlyForRegisterUser)
                                    b.InnerHtml = a_title + _Image("/Content/images/Keys.png", 20, 0, "", 0);
                                else
                                    b.InnerHtml = a_title.ToString();
                                StringBuilder tag = new StringBuilder();
                                foreach (var t in article.mytrip_ArticlesTag)
                                {
                                    TagBuilder a_tag = new TagBuilder("a");
                                    a_tag.MergeAttribute("href", "/Article/Index/1/10/" + t.TagId + "/" + t.Path);
                                    a_tag.InnerHtml = t.TagName;
                                    tag.AppendLine(a_tag.ToString());
                                }
                                string abstracts = string.Empty;
                                if (article.Abstract.Length <= content)
                                    abstracts = article.Abstract;
                                else
                                    abstracts = article.Abstract.Remove(content) + "...";
                                td3.InnerHtml = b.ToString() + "<br/>" + abstracts + "<br/>" + rating + "<br/>" + specification + tag.ToString();
                                break;
                            }
                        }
                        tr.InnerHtml = td1.ToString() + td2.ToString() + td3.ToString();
                        result.AppendLine(tr.ToString());
                        _count++;
                    }
                    table.InnerHtml = result.ToString();
                    return table.ToString();
                }
                #endregion
                else
                {
                    return string.Empty;
                }
            }
        }
        public static string ArticlesUserProfile(this HtmlHelper html)
        {
            if ((ArticlesSetting.articles || ArticlesSetting.blogs) && HttpContext.Current.User.Identity.IsAuthenticated)
            {

                IArticleRepository ar = new IArticleRepository();
                StringBuilder _result = new StringBuilder();
                TagBuilder ul = new TagBuilder("ul");
                int countcomment = ar.comment.GetCountCommentsByUser(HttpContext.Current.User.Identity.Name);
                if (countcomment >= ArticlesSetting.countCommentForBlogs)
                {
                    if (ar.category.GetBlogsByUser(HttpContext.Current.User.Identity.Name, HttpContext.Current.Session["culture"].ToString()).Count() == 0)
                    {
                        TagBuilder li = new TagBuilder("li");
                        TagBuilder a_createblog = new TagBuilder("a");
                        a_createblog.MergeAttribute("href", "/ArticleUser/CreateBlog");
                        a_createblog.InnerHtml = ArticleLanguage.create_blog;
                        li.InnerHtml = a_createblog.ToString();
                        _result.AppendLine(li.ToString());
                    }
                    foreach (var item in ar.category.GetBlogsByUser(HttpContext.Current.User.Identity.Name))
                    {
                        TagBuilder li = new TagBuilder("li");
                        TagBuilder a_blog = new TagBuilder("a");
                        if (item.Culture == HttpContext.Current.Session["culture"].ToString())
                        {
                            a_blog.MergeAttribute("href", "/Article/Index/1/10/" + item.CategoryId + "/" + item.Path);
                            a_blog.InnerHtml = item.Title;
                        }
                        else
                        {
                            a_blog.MergeAttribute("href", "/Language/Index/" + item.Culture.ToLower() + "/(x)Article(x)Index(x)1(x)10(x)" + item.CategoryId + "(x)" + item.Path);
                            TagBuilder flag = new TagBuilder("img");
                            flag.MergeAttribute("src", "/Content/images/" + item.Culture.ToLower() + ".png");
                            flag.MergeAttribute("style", "border-width:0px;width:14px");
                            flag.MergeAttribute("alt", item.Culture.ToLower());
                            flag.MergeAttribute("title", item.Culture.ToLower());
                            a_blog.InnerHtml = item.Title + " " + flag ;
                        }
                        li.InnerHtml = a_blog.ToString();
                        _result.AppendLine(li.ToString());
                    }
                }
                else
                {
                    TagBuilder li = new TagBuilder("li");
                    int count = ArticlesSetting.countCommentForBlogs - countcomment;
                    TagBuilder a_com = new TagBuilder("a");
                    a_com.InnerHtml = String.Format(ArticleLanguage.activation_blog, count);
                    li.InnerHtml = a_com.ToString();
                    _result.AppendLine(li.ToString());
                }
                ul.InnerHtml = _result.ToString();
                return ul.ToString();
            }
            else { return string.Empty; }
        }
        /*PRIVATE*/
        private static string privateArticleSpecification(string path, int categoryId, int subcategoryId, string categoryTitle,
           string categoryPath, string categoryTitle2, string categoryPath2, bool separateBlok, bool blog, string usrname, int views,
           bool approvedcomment, int commentsCount, DateTime createDate)
        {
            StringBuilder result = new StringBuilder();
            if (path == "Articles" | path.StartsWith("(Tag)") | path.StartsWith("(Search)"))
            {
                if (categoryId == subcategoryId)
                {
                    TagBuilder a_category = new TagBuilder("a");
                    a_category.MergeAttribute("href", "/Article/Index/1/10/" + categoryId + "/" + categoryPath);
                    a_category.InnerHtml = categoryTitle;
                    if (blog)
                    {
                        result.AppendLine(ArticleLanguage.blog + ": " + a_category + "<br/>");
                    }
                    else
                    {
                        result.AppendLine(ArticleLanguage.category + ": " + a_category + "<br/>");
                    }
                }
                else
                {
                    TagBuilder a_category = new TagBuilder("a");
                    a_category.MergeAttribute("href", "/Article/Index/1/10/" + subcategoryId + "/" + categoryPath2);
                    a_category.InnerHtml = categoryTitle2;
                    if (blog)
                    {
                        result.AppendLine(ArticleLanguage.blog + ": " + a_category + "<br/>");
                    }
                    else
                    {
                        result.AppendLine(ArticleLanguage.category + ": " + a_category + "<br/>");
                    }

                    TagBuilder a_subcategory = new TagBuilder("a");
                    a_subcategory.MergeAttribute("href", "/Article/Index/1/10/" + categoryId + "/" + categoryPath);
                    a_subcategory.InnerHtml = categoryTitle;
                    if (blog)
                    {
                        result.AppendLine(ArticleLanguage.topic + ": " + a_subcategory + "<br/>");
                    }
                    else
                    {
                        result.AppendLine(ArticleLanguage.subcategory + ": " + a_subcategory + "<br/>");
                    }
                }
            }
            else
            {
                if (categoryPath != path)
                {
                    if (separateBlok)
                    {
                        result.AppendLine(ArticleLanguage.category + ": ");
                    }
                    else if (blog)
                    {
                        result.AppendLine(ArticleLanguage.topic + ": ");
                    }
                    else { result.AppendLine(ArticleLanguage.subcategory + ": "); }
                    TagBuilder a_2category = new TagBuilder("a");
                    a_2category.MergeAttribute("href", "/Article/Index/1/10/" + categoryId + "/" + categoryPath);
                    a_2category.InnerHtml = categoryTitle;
                    result.AppendLine(a_2category + "<br/>");
                }

            }
            result.AppendLine(ArticleLanguage.author + ": " + usrname + "<br/>");
            result.AppendLine(ArticleLanguage.views + ": " + views + "<br/>");
            if (approvedcomment)
                result.AppendLine(ArticleLanguage.comments + ": " + commentsCount + "<br/>");
            result.AppendLine(ArticleLanguage.create_date + ": " + String.Format("{0:dd MMMM yyyy}", createDate));
            return result.ToString();
        }
        private static string privateArticleSpecification(string path, int categoryId, int subcategoryId, string categoryTitle,
           string categoryPath, string categoryTitle2, string categoryPath2, bool separateBlok, bool blog)
        {
            StringBuilder result = new StringBuilder();
            if (path == "Articles" | path.StartsWith("(Tag)") | path.StartsWith("(Search)"))
            {
                if (categoryId == subcategoryId)
                {
                    TagBuilder a_category = new TagBuilder("a");
                    a_category.MergeAttribute("href", "/Article/Index/1/10/" + categoryId + "/" + categoryPath);
                    a_category.InnerHtml = categoryTitle;
                    if (blog)
                    {
                        result.AppendLine(ArticleLanguage.blog + ": " + a_category + "<br/>");
                    }
                    else
                    {
                        result.AppendLine(ArticleLanguage.category + ": " + a_category + "<br/>");
                    }
                }
                else
                {
                    TagBuilder a_category = new TagBuilder("a");
                    a_category.MergeAttribute("href", "/Article/Index/1/10/" + subcategoryId + "/" + categoryPath2);
                    a_category.InnerHtml = categoryTitle2;
                    if (blog)
                    {
                        result.AppendLine(ArticleLanguage.blog + ": " + a_category + "<br/>");
                    }
                    else
                    {
                        result.AppendLine(ArticleLanguage.category + ": " + a_category + "<br/>");
                    }

                    TagBuilder a_subcategory = new TagBuilder("a");
                    a_subcategory.MergeAttribute("href", "/Article/Index/1/10/" + categoryId + "/" + categoryPath);
                    a_subcategory.InnerHtml = categoryTitle;
                    if (blog)
                    {
                        result.AppendLine(ArticleLanguage.topic + ": " + a_subcategory + "<br/>");
                    }
                    else
                    {
                        result.AppendLine(ArticleLanguage.subcategory + ": " + a_subcategory + "<br/>");
                    }
                }
            }
            else
            {
                if (categoryPath != path)
                {
                    if (separateBlok)
                    {
                        result.AppendLine(ArticleLanguage.category + ": ");
                    }
                    else if (blog)
                    {
                        result.AppendLine(ArticleLanguage.topic + ": ");
                    }
                    else { result.AppendLine(ArticleLanguage.subcategory + ": "); }
                    TagBuilder a_2category = new TagBuilder("a");
                    a_2category.MergeAttribute("href", "/Article/Index/1/10/" + categoryId + "/" + categoryPath);
                    a_2category.InnerHtml = categoryTitle;
                    result.AppendLine(a_2category + "<br/>");
                }

            }
            return result.ToString();
        }
        private static string privateArticleRating(double totalvote, bool approvedvotes, bool active, int countvotes)
        {
            StringBuilder result = new StringBuilder();
            if (approvedvotes)
            {
                result.AppendLine(ArticleLanguage.total_votes + ": " + countvotes + ". ");
                int rate = 0;
                while (rate < 5)
                {
                    double rate12 = rate + 0.125;
                    double rate37 = rate + 0.375;
                    double rate62 = rate + 0.625;
                    double rate87 = rate + 0.875;
                    if (active && HttpContext.Current.User.Identity.IsAuthenticated)
                    {
                        TagBuilder input = new TagBuilder("input");
                        input.MergeAttribute("type", "submit");
                        input.MergeAttribute("value", "");
                        input.MergeAttribute("name", "input" + rate);
                        if (totalvote > rate12 && totalvote < rate37)
                            input.MergeAttribute("style", "background:url(/Content/images/star25.png);width: 15px; height: 15px; border-width: 0px;");
                        if (totalvote > rate37 && totalvote < rate62)
                            input.MergeAttribute("style", "background:url(/Content/images/star50.png);width: 15px; height: 15px; border-width: 0px;");
                        if (totalvote > rate62 && totalvote < rate87)
                            input.MergeAttribute("style", "background:url(/Content/images/star75.png);width: 15px; height: 15px; border-width: 0px;");
                        if (totalvote < rate87)
                            input.MergeAttribute("style", "background:url(/Content/images/star100.png);width: 15px; height: 15px; border-width: 0px;");
                        if (totalvote > rate87)
                            input.MergeAttribute("style", "background:url(/Content/images/star.png);width: 15px; height: 15px; border-width: 0px;");
                        rate++;
                        result.AppendLine(input.ToString());

                    }
                    else
                    {
                        TagBuilder input = new TagBuilder("img");
                        if (totalvote > rate12 && totalvote < rate37)
                            input.MergeAttribute("src", "/Content/images/star25.png");
                        if (totalvote > rate37 && totalvote < rate62)
                            input.MergeAttribute("src", "/Content/images/star50.png");
                        if (totalvote > rate62 && totalvote < rate87)
                            input.MergeAttribute("src", "/Content/images/star75.png");
                        if (totalvote < rate87)
                            input.MergeAttribute("src", "/Content/images/star100.png");
                        if (totalvote > rate87)
                            input.MergeAttribute("src", "/Content/images/star.png");
                        rate++;
                        input.MergeAttribute("style", "width: 15px; height: 15px; border-width: 0px;");
                        result.AppendLine(input.ToString());
                    }

                }
            }
            return result.ToString();
        }
        private static string privateArticleRating2(double totalvote, bool approvedvotes, bool active, int countvotes)
        {
            StringBuilder result = new StringBuilder();
            if (approvedvotes)
            {
                int rate = 0;
                while (rate < 5)
                {
                    double rate12 = rate + 0.125;
                    double rate37 = rate + 0.375;
                    double rate62 = rate + 0.625;
                    double rate87 = rate + 0.875;
                    if (active && HttpContext.Current.User.Identity.IsAuthenticated)
                    {
                        TagBuilder input = new TagBuilder("input");
                        input.MergeAttribute("type", "submit");
                        input.MergeAttribute("value", "");
                        input.MergeAttribute("name", "input" + rate);
                        if (totalvote > rate12 && totalvote < rate37)
                            input.MergeAttribute("style", "background:url(/Content/images/star25.png);width: 15px; height: 15px; border-width: 0px;");
                        if (totalvote > rate37 && totalvote < rate62)
                            input.MergeAttribute("style", "background:url(/Content/images/star50.png);width: 15px; height: 15px; border-width: 0px;");
                        if (totalvote > rate62 && totalvote < rate87)
                            input.MergeAttribute("style", "background:url(/Content/images/star75.png);width: 15px; height: 15px; border-width: 0px;");
                        if (totalvote < rate87)
                            input.MergeAttribute("style", "background:url(/Content/images/star100.png);width: 15px; height: 15px; border-width: 0px;");
                        if (totalvote > rate87)
                            input.MergeAttribute("style", "background:url(/Content/images/star.png);width: 15px; height: 15px; border-width: 0px;");
                        rate++;
                        result.AppendLine(input.ToString());

                    }
                    else
                    {
                        TagBuilder input = new TagBuilder("img");
                        if (totalvote > rate12 && totalvote < rate37)
                            input.MergeAttribute("src", "/Content/images/star25.png");
                        if (totalvote > rate37 && totalvote < rate62)
                            input.MergeAttribute("src", "/Content/images/star50.png");
                        if (totalvote > rate62 && totalvote < rate87)
                            input.MergeAttribute("src", "/Content/images/star75.png");
                        if (totalvote < rate87)
                            input.MergeAttribute("src", "/Content/images/star100.png");
                        if (totalvote > rate87)
                            input.MergeAttribute("src", "/Content/images/star.png");
                        rate++;
                        input.MergeAttribute("style", "width: 15px; height: 15px; border-width: 0px;");
                        result.AppendLine(input.ToString());
                    }

                }
            }
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
        private static string privateImageForAbstract(string image, int width)
        {
            if (image != null)
            {
                string old = "BORDER-BOTTOM: 0px; BORDER-LEFT: 0px; BORDER-TOP: 0px; BORDER-RIGHT: 0px";
                string _new = "border:0px;width:" + width + "px;position:relative;float:right;margin-left:5px;";
                image = image.Replace(old, _new);
                return image;
            }
            else
            {
                return string.Empty;
            }
        }

    }
}
