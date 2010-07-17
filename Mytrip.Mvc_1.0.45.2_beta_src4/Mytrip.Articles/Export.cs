using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Mytrip.Articles.Repository;
using Mytrip.Articles.Helpers;
using System.Web;
using Mytrip.Mvc;
using Mytrip.Articles.Repository.DataEntities;
using Mytrip.Mvc.Repository;

namespace Mytrip.Articles
{
    public static class Export
    {
        #region Top Menu
        public static string MenuArticle()
        {
            ArticlesSetting artset = new ArticlesSetting();
            if (artset.articles())
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
                IArticleRepository ar = new IArticleRepository();
                TagBuilder article = new TagBuilder("a");
                article.MergeAttribute("href", "/Article/Index/1/10/0/Articles");
                article.InnerHtml = artset.NameArticlesPage();
                IDictionary<int, string> _result = new Dictionary<int, string>();
                bool tab_category = false;
                int key = 1;
                foreach (var item in ar.category.GetCategories(false, HttpContext.Current.Session["culture"].ToString()))
                {
                    if (Controller == "Article")
                    {
                        if (item.CategoryId == id)
                        { tab_category = true; }
                        else
                        {
                            foreach (var _item in item.mytrip_articlescategory1)
                            {
                                if (_item.CategoryId == id)
                                { tab_category = true; }
                            }
                        }
                    }
                    TagBuilder a = new TagBuilder("a");
                    a.MergeAttribute("href", "/Article/Index/1/10/" + item.CategoryId + "/" + item.Path);
                    a.InnerHtml = item.Title;
                    _result.Add(key, a.ToString());
                    key++;
                } bool drop = true;
                if (key == 1)
                    drop = false;
                if (Controller == "Article")
                {
                    if (id == 0 && param == "Articles")
                        tab_category = true;
                }
                return GeneralMethods.Menu(article.ToString(), _result, tab_category, false, true, drop);
            }
            else { return string.Empty; }
        }
        public static string MenuBlog()
        {
            ArticlesSetting artset = new ArticlesSetting();
            if (artset.blogs())
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
                IArticleRepository ar = new IArticleRepository();
                TagBuilder article = new TagBuilder("a");
                article.MergeAttribute("href", "/Article/Index/1/10/0/Blogs");
                article.InnerHtml = artset.NameBlogsPage();
                IDictionary<int, string> _result = new Dictionary<int, string>();
                bool tab_category = false;
                int key = 1;
                foreach (var item in ar.category.GetBlogs(HttpContext.Current.Session["culture"].ToString()))
                {
                    if (Controller == "Article")
                    {
                        if (item.CategoryId == id)
                        { tab_category = true; }
                        else
                        {
                            foreach (var _item in item.mytrip_articlescategory1)
                            {
                                if (_item.CategoryId == id)
                                { tab_category = true; }
                            }
                        }
                    }
                    TagBuilder a = new TagBuilder("a");
                    a.MergeAttribute("href", "/Article/Index/1/10/" + item.CategoryId + "/" + item.Path);
                    a.InnerHtml = item.Title;
                    _result.Add(key, a.ToString());
                    key++;
                } bool drop = true;
                if (key == 1)
                    drop = false;
                if (Controller == "Article")
                {
                    if (id == 0 && param == "Blogs")
                        tab_category = true;
                }
                return GeneralMethods.Menu(article.ToString(), _result, tab_category, false, true, drop);
            }
            else { return string.Empty; }
        }
        public static string MenuCategory()
        {
            ArticlesSetting artset = new ArticlesSetting();
            if (artset.articles())
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
                IArticleRepository ar = new IArticleRepository();
                StringBuilder result = new StringBuilder();
                foreach (var __item in ar.category.GetCategories(true, HttpContext.Current.Session["culture"].ToString()))
                {
                    TagBuilder article = new TagBuilder("a");
                    article.MergeAttribute("href", "/Article/Index/1/10/" + __item.CategoryId + "/" + __item.Path);
                    article.InnerHtml = __item.Title;
                    IDictionary<int, string> _result = new Dictionary<int, string>();
                    bool tab_category = false;
                    int key = 1;
                    foreach (var item in __item.mytrip_articlescategory1)
                    {
                        if (item.SubCategoryId != 0 && (item.AllCulture || item.Culture == HttpContext.Current.Session["culture"].ToString()))
                        {
                            if (Controller == "Article")
                            {
                                if (item.CategoryId == id)
                                { tab_category = true; }
                                else
                                {
                                    foreach (var _item in item.mytrip_articlescategory1)
                                    {
                                        if (_item.CategoryId == id)
                                        { tab_category = true; }
                                    }
                                }
                            }
                            TagBuilder a = new TagBuilder("a");
                            a.MergeAttribute("href", "/Article/Index/1/10/" + item.CategoryId + "/" + item.Path);
                            a.InnerHtml = item.Title;
                            _result.Add(key, a.ToString());
                            key++;
                        }
                    } bool drop = true;
                    if (key == 1)
                        drop = false;
                    if (Controller == "Article")
                    {
                        if (__item.CategoryId == id)
                            tab_category = true;
                    }
                    result.AppendLine(GeneralMethods.Menu(article.ToString(), _result, tab_category, false, true, drop));
                }
                return result.ToString();
            }
            else { return string.Empty; }
        }
        #endregion

        #region SideBar
        public static string AccordionSearch()
        {
            ArticlesSetting artset = new ArticlesSetting();
            TagBuilder form = new TagBuilder("form");
            form.MergeAttribute("action", "/Article?url=" + HttpContext.Current.Request.Path.ToString());
            form.MergeAttribute("method", "post");
            form.InnerHtml = "<table style=\"border:0;padding:0;\"><tr><td style=\"border:0;padding:0;\"><input id=\"search\" name=\"search\" type=\"text\" value=\"\" /></td><td style=\"border:0;padding:0;\"><input id=\"_search\" type=\"submit\" value=\"\"></input></td></tr></table>";

            return GeneralMethods.Accordion("accsearch", artset.NameSearchPage(), form.ToString());
        }
        public static string AccordionArticle()
        {
            ArticlesSetting artset = new ArticlesSetting();
            if (artset.articles())
            {
                TagBuilder _a = new TagBuilder("a");
                _a.MergeAttribute("href", "/Article/Index/1/10/0/Articles");
                _a.InnerHtml = artset.NameArticlesPage();
                TagBuilder ul = new TagBuilder("ul");
                IArticleRepository ar = new IArticleRepository();
                StringBuilder _result = new StringBuilder();
                int count = 0;
                foreach (var item in ar.category.GetCategories(false, HttpContext.Current.Session["culture"].ToString()))
                {
                    TagBuilder li = new TagBuilder("li");
                    TagBuilder a = new TagBuilder("a");
                    a.MergeAttribute("href", "/Article/Index/1/10/" + item.CategoryId + "/" + item.Path);
                    a.InnerHtml = item.Title;
                    li.InnerHtml = a.ToString();
                    _result.AppendLine(li.ToString());
                    count++;
                }
                ul.InnerHtml = _result.ToString();
                bool cssclacc = false;
                if (count > 0)
                    cssclacc = true;
                return GeneralMethods.Accordion2("accarticle", cssclacc, _a.ToString(), ul.ToString());
            }
            else { return string.Empty; }
        }
        public static string AccordionBlogs()
        {
            ArticlesSetting artset = new ArticlesSetting();
            if (artset.blogs())
            {
                TagBuilder _a = new TagBuilder("a");
                _a.MergeAttribute("href", "/Article/Index/1/10/0/Blogs");
                _a.InnerHtml = artset.NameBlogsPage();
                TagBuilder ul = new TagBuilder("ul");
                IArticleRepository ar = new IArticleRepository();
                StringBuilder _result = new StringBuilder();
                int count = 0;
                foreach (var item in ar.category.GetBlogs(HttpContext.Current.Session["culture"].ToString()))
                {
                    TagBuilder li = new TagBuilder("li");
                    TagBuilder a = new TagBuilder("a");
                    a.MergeAttribute("href", "/Article/Index/1/10/" + item.CategoryId + "/" + item.Path);
                    a.InnerHtml = item.Title;
                    li.InnerHtml = a.ToString();
                    _result.AppendLine(li.ToString());
                    count++;
                }///Article/Index/1/10/0/Blogs
                ul.InnerHtml = _result.ToString();
                bool cssclacc = false;
                if (count > 0)
                    cssclacc = true;
                return GeneralMethods.Accordion2("accblog", cssclacc, _a.ToString(), ul.ToString());
            }
            else { return string.Empty; }
        }
        public static string AccordionCategory()
        {
            ArticlesSetting artset = new ArticlesSetting();
            if (artset.articles())
            {
                IArticleRepository ar = new IArticleRepository();
                StringBuilder result = new StringBuilder();
                int category = 0;
                foreach (var _item in ar.category.GetCategories(true, HttpContext.Current.Session["culture"].ToString()))
                {
                    TagBuilder a_title = new TagBuilder("a");
                    a_title.MergeAttribute("href", "/Article/Index/1/10/" + _item.CategoryId + "/" + _item.Path);
                    a_title.InnerHtml = _item.Title;
                    TagBuilder ul = new TagBuilder("ul");
                    StringBuilder _result = new StringBuilder();
                    int count = 0;
                    foreach (var item in _item.mytrip_articlescategory1)
                    {
                        if (item.SubCategoryId != 0 && (item.AllCulture || item.Culture == HttpContext.Current.Session["culture"].ToString()))
                        {
                            TagBuilder li = new TagBuilder("li");
                            TagBuilder a = new TagBuilder("a");
                            a.MergeAttribute("href", "/Article/Index/1/10/" + item.CategoryId + "/" + item.Path);
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
                    result.AppendLine(GeneralMethods.Accordion2("acccat" + category, cssclacc, a_title.ToString(), ul.ToString()));
                    category++;
                }
                return result.ToString();
            }
            else { return string.Empty; }
        }
        public static string AccordionTag()
        {
            ArticlesSetting artset = new ArticlesSetting();
            if (artset.articles() | artset.blogs())
            {
                IArticleRepository ar = new IArticleRepository();
                StringBuilder _result = new StringBuilder();
                string style = string.Empty;
                int _count = 0;
                foreach (var item in ar.article.GetAllTags(true))
                {
                    int count = item.mytrip_articles.Where(x => x.Culture == HttpContext.Current.Session["culture"].ToString()).Count() + item.mytrip_articles.Where(x => x.AllCulture == true).Count();
                    if (count > 0)
                    {
                        int e = 10;
                        if (2 < count && count <= 4)
                            e = 11;
                        else if (4 < count && count <= 6)
                            e = 12;
                        else if (6 < count && count <= 8)
                            e = 13;
                        else if (8 < count && count <= 10)
                            e = 14;
                        else if (10 < count && count <= 15)
                            e = 15;
                        else if (15 < count && count <= 20)
                            e = 16;
                        else if (20 < count && count <= 25)
                            e = 17;
                        else if (25 < count && count <= 30)
                            e = 18;
                        else if (30 < count && count <= 40)
                            e = 19;
                        else if (40 < count)
                            e = 20;
                        if (e % 2 == 0)
                        {
                            style = "font-size: " + e + "px; font-weight: bold;";
                        }
                        else
                        {
                            style = "font-size: " + e + "px;";
                        }

                        TagBuilder a = new TagBuilder("a");
                        a.MergeAttribute("href", "/Article/Index/1/10/" + item.TagId + "/" + item.Path);
                        a.MergeAttribute("style", style);
                        a.InnerHtml = item.TagName + " (" + count + ")";
                        _result.AppendLine(a.ToString() + " ");
                        _count++;
                    }

                }
                if (_count > 0)
                {
                    return GeneralMethods.Accordion("acctag", artset.NameTagsPage(), _result.ToString());
                }
                else { return string.Empty; }
            }
            else { return string.Empty; }
        }
        public static string AccordionArticlesActivity()
        {
            TagBuilder list = new TagBuilder("ol");
            list.AddCssClass("numbered");
            IArticleRepository ar = new IArticleRepository();
            ArticlesSetting arset = new ArticlesSetting();
            TagBuilder title = new TagBuilder("center");
            title.InnerHtml = "<b><u>" + ArticleLanguage.top_viewed + "</b></u>";
            foreach (var item in ar.article.GetArticlesPopular(HttpContext.Current.Session["culture"].ToString(), 5))
            {
                TagBuilder li = new TagBuilder("li");
                TagBuilder a = new TagBuilder("a");
                a.MergeAttribute("href", "/Article/View/" + item.ArticleId + "/" + item.Path);
                a.MergeAttribute("title", item.Title);
                a.InnerHtml = item.Title;
                li.InnerHtml = "<b>" + a.ToString() + "</b><br/>" + ArticleLanguage.views + ": " + item.Views;
                list.InnerHtml += li.ToString();
            }
            TagBuilder div = new TagBuilder("div");
            div.MergeAttribute("id", "populararticles");
            div.InnerHtml = title + list.ToString()
                + "<center><input id='save' type='submit' name='option' value='" + ArticleLanguage.most_rated + "' class=\"link\" />"
                 + "<input id='save' type='submit' name='option' value='" + ArticleLanguage.recent + "' class=\"link\" />"
                  + "<input id='save' type='submit' name='option' value='" + ArticleLanguage.recent_comments + "' class=\"link\" /></center>";
            TagBuilder form = new TagBuilder("form");
            form.MergeAttribute("action", "/Article/ArticlesActivity");
            form.MergeAttribute("method", "post");
            form.MergeAttribute("onclick", "Sys.Mvc.AsyncForm.handleClick(this, new Sys.UI.DomEvent(event));");
            form.MergeAttribute("onsubmit", "Sys.Mvc.AsyncForm.handleSubmit(this, new Sys.UI.DomEvent(event), { insertionMode: Sys.Mvc.InsertionMode.replace , updateTargetId:\"populararticles\" });");
            form.InnerHtml = div.ToString();
            return GeneralMethods.Accordion2("accpopular", String.Format(ArticleLanguage.articles_popular_recent, arset.NameArticlesPage()), form.ToString());
        }
        public static string AccordionBlogsActivity()
        {
            TagBuilder list = new TagBuilder("ol");
            list.AddCssClass("numbered");
            IArticleRepository ar = new IArticleRepository();
            ArticlesSetting arset = new ArticlesSetting();
            TagBuilder title = new TagBuilder("center");
            title.InnerHtml = "<b><u>" + ArticleLanguage.top_viewed + "</b></u>";
            foreach (var item in ar.article.GetPostsPopular(HttpContext.Current.Session["culture"].ToString(), 5))
            {
                TagBuilder li = new TagBuilder("li");
                TagBuilder a = new TagBuilder("a");
                a.MergeAttribute("href", "/Article/View/" + item.ArticleId + "/" + item.Path);
                a.MergeAttribute("title", item.Title);
                a.InnerHtml = item.Title;
                li.InnerHtml = a.ToString() + "<br/>" + ArticleLanguage.views + ": " + item.Views;
                list.InnerHtml += li.ToString();
            }
            TagBuilder div = new TagBuilder("div");
            div.MergeAttribute("id", "popularblogs");
            div.InnerHtml = title + list.ToString()
                + "<center><input id='save' type='submit' name='option' value='" + ArticleLanguage.most_rated + "' class=\"link\" />"
                 + "<input id='save' type='submit' name='option' value='" + ArticleLanguage.recent + "' class=\"link\" />"
                  + "<input id='save' type='submit' name='option' value='" + ArticleLanguage.recent_comments + "' class=\"link\" /></center>";
            TagBuilder form = new TagBuilder("form");
            form.MergeAttribute("action", "/Article/PostsActivity");
            form.MergeAttribute("method", "post");
            form.MergeAttribute("onclick", "Sys.Mvc.AsyncForm.handleClick(this, new Sys.UI.DomEvent(event));");
            form.MergeAttribute("onsubmit", "Sys.Mvc.AsyncForm.handleSubmit(this, new Sys.UI.DomEvent(event), { insertionMode: Sys.Mvc.InsertionMode.replace, updateTargetId:\"popularblogs\" });");
            form.InnerHtml = div.ToString();
            return GeneralMethods.Accordion2("accpopular2", String.Format(ArticleLanguage.blogs_popular_recent, arset.NameBlogsPage()), form.ToString());
        }
        #endregion

        #region HomePage
        public static string HomePage(int categoryId, int line, int column, int content, int imgwidth, int style, bool viewtitle)
        {
            if (categoryId == -1)
                return ArticlesMenuHelper._HP(false, line, column, content, imgwidth, style, viewtitle);
            else if (categoryId == -2)
                return ArticlesMenuHelper._HP(true, line, column, content, imgwidth, style, viewtitle);
            else
            {
                IArticleRepository ar = new IArticleRepository();
                int take = line * column;
                IQueryable<mytrip_articles> articles = ar.article.GetArticles(categoryId, HttpContext.Current.Session["culture"].ToString(), take);
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
                string CategoryName = string.Empty;
                string CategoryPath = string.Empty;
                bool __cat = false;
                StringBuilder result = new StringBuilder();
                TagBuilder table = new TagBuilder("table");
                string _content = string.Empty;
                int _line2 = 0;
                string finaltr = string.Empty;
                string start = string.Empty;
                string end = string.Empty;
                string styletable = string.Empty;
                foreach (mytrip_articles article in articles)
                {
                    if (!__cat && article.mytrip_articlescategory.CategoryId == categoryId)
                    {
                        CategoryName = article.mytrip_articlescategory.Title;
                        CategoryPath = article.mytrip_articlescategory.Path;
                        __cat = true;
                    }
                    else if (!__cat && article.mytrip_articlescategory.SubCategoryId == categoryId)
                    {
                        CategoryName = article.mytrip_articlescategory.mytrip_articlescategory2.Title;
                        CategoryPath = article.mytrip_articlescategory.mytrip_articlescategory2.Path;
                        __cat = true;
                    }
                    if (imgwidth > 0)
                    {
                        _content = ArticlesMenuHelper.articleMax(article, content, imgwidth);
                    }
                    else
                    {
                        _content = ArticlesMenuHelper.articleMin(article, content);
                    }
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
                string LangName = "<a href=\"/Article/Index/1/10/" + categoryId + "/" + CategoryPath + "\">" + CategoryName + "</a>";
                string _CategoryName = string.Empty;
                if (viewtitle)
                    _CategoryName = "<h3 class=\"title\">" + LangName + " " + ArticlesHelper._ArticleRssLink(CategoryName, CategoryPath, categoryId, 14) + "</h3>";
                if (column > 0)
                    return _CategoryName + start + table.ToString() + end;
                else
                    return string.Empty;
            }
        }
        #endregion

        #region Control Panel
        public static string SettingArticle()
        {
            TagBuilder li = new TagBuilder("li");
            TagBuilder a = new TagBuilder("a");
            a.MergeAttribute("href", "/Article/ArticleSetting");
            a.InnerHtml = ArticleLanguage.article_setting;
            li.InnerHtml = a.ToString();
            return li.ToString();

        }
        public static string ManagerArticle()
        {
            /////
            TagBuilder li_archive = new TagBuilder("li");
            TagBuilder a_archive = new TagBuilder("a");
            a_archive.MergeAttribute("href", "/Article/Archive");
            a_archive.InnerHtml = ArticleLanguage.articles_manager;
            li_archive.InnerHtml = a_archive.ToString();
            /////
            return li_archive.ToString();
        }
        #endregion

        #region Profile
        public static string ProfileArticles()
        {
            RoleRepository db = new RoleRepository();
            ArticlesSetting artset = new ArticlesSetting();
            if (artset.blogs() && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                IArticleRepository ar = new IArticleRepository();
                StringBuilder _result = new StringBuilder();
                TagBuilder ul = new TagBuilder("ul");
                int countcomment = ar.comment.GetCommentsCount(HttpContext.Current.User.Identity.Name);
                if ((!artset.closecountCommentForBlogs()
                    && countcomment >= artset.countCommentForBlogs()
                    && !db.IsUserInRoleOnline(artset.roleBlogger()))
                    || db.IsUserInRoleOnline(artset.roleBlogger()))
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
                            ThemeSetting theme = new ThemeSetting();
                            TagBuilder flag = new TagBuilder("img");
                            flag.MergeAttribute("src", "/Theme/" + theme.theme() + "/images/" + item.Culture.ToLower() + ".png");
                            flag.MergeAttribute("style", "width:14px");
                            flag.MergeAttribute("alt", item.Culture.ToLower());
                            flag.MergeAttribute("title", item.Culture.ToLower());
                            a_blog.InnerHtml = item.Title + " " + flag;
                        }
                        li.InnerHtml = a_blog.ToString();
                        _result.AppendLine(li.ToString());
                    }
                }
                else if (!artset.closecountCommentForBlogs())
                {
                    TagBuilder li = new TagBuilder("li");
                    int count = artset.countCommentForBlogs() - countcomment;
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
        public static string LastActivity(string username)
        {
            IArticleRepository ar = new IArticleRepository();
            StringBuilder result = new StringBuilder();
            var activities = ProfileHelpers.GetLastActivities(username, "Blogs");
            int ctr = 0;
            if (activities.Count() > 0)
            {
                result.AppendLine("<h3 class=\"title\">" + ArticleLanguage.blogs + "</h3>");
                #region table
                TagBuilder table = new TagBuilder("table");
                table.MergeAttribute("style", "border:0px;");
                foreach (var item in activities)
                {
                    TagBuilder tr1 = new TagBuilder("tr");
                    if (ctr % 2 == 0)
                        tr1.AddCssClass("profile1");
                    else
                        tr1.AddCssClass("profile2");
                    TagBuilder td11 = new TagBuilder("td");
                    td11.MergeAttribute("style", "border:0px;width:100px");
                    td11.InnerHtml = item.Place;
                    tr1.InnerHtml = td11.ToString();
                    TagBuilder td12 = new TagBuilder("td");
                    td12.MergeAttribute("style", "border:0px;");
                    td12.InnerHtml = item.Activity;
                    tr1.InnerHtml += td12.ToString();
                    TagBuilder td13 = new TagBuilder("td");
                    td13.MergeAttribute("style", "border:0px;width:130px");
                    if (item.Date.Date == DateTime.Today.Date && item.Date.Month == DateTime.Now.Month && item.Date.Year == DateTime.Now.Year)
                    {
                        if (DateTime.Now.Hour - item.Date.Hour == 0)
                            td13.InnerHtml = (DateTime.Now.Minute - item.Date.Minute).ToString() + " " + ArticleLanguage.minutes_ago;
                        else
                            td13.InnerHtml = (DateTime.Now.Hour - item.Date.Hour).ToString() + " " + ArticleLanguage.hours_ago;
                    }
                    else
                        td13.InnerHtml = item.Date.ToString("dd MMM yyyy, HH:mm");
                    tr1.InnerHtml += td13.ToString();
                    table.InnerHtml += tr1.ToString();
                    ctr++;
                }
                #endregion
                result.AppendLine(table.ToString());
            }
            activities = ProfileHelpers.GetLastActivities(username, "Articles");
            if (activities.Count() > 0)
            {
                result.AppendLine("<h3 class=\"title\">" + ArticleLanguage.articles + "</h3>");
                #region table2
                TagBuilder table2 = new TagBuilder("table");
                table2.MergeAttribute("style", "border:0px;");

                ctr = 0;
                foreach (var item in activities)
                {
                    TagBuilder tr1 = new TagBuilder("tr");
                    if (ctr % 2 == 0)
                        tr1.AddCssClass("profile1");
                    else
                        tr1.AddCssClass("profile2");
                    TagBuilder td11 = new TagBuilder("td");
                    td11.MergeAttribute("style", "border:0px;width:100px");
                    td11.InnerHtml = item.Place;
                    tr1.InnerHtml = td11.ToString();
                    TagBuilder td12 = new TagBuilder("td");
                    td12.MergeAttribute("style", "border:0px;");
                    td12.InnerHtml = item.Activity;
                    tr1.InnerHtml += td12.ToString();
                    TagBuilder td13 = new TagBuilder("td");
                    td13.MergeAttribute("style", "border:0px;width:130px");
                    if (item.Date.Date == DateTime.Today.Date && item.Date.Month == DateTime.Now.Month && item.Date.Year == DateTime.Now.Year)
                    {
                        if (DateTime.Now.Hour - item.Date.Hour == 0)
                            td13.InnerHtml = (DateTime.Now.Minute - item.Date.Minute).ToString() + " " + ArticleLanguage.minutes_ago;
                        else
                            td13.InnerHtml = (DateTime.Now.Hour - item.Date.Hour).ToString() + " " + ArticleLanguage.hours_ago;
                    }
                    else
                        td13.InnerHtml = item.Date.ToString("dd MMM yyyy, HH:mm");
                    tr1.InnerHtml += td13.ToString();
                    table2.InnerHtml += tr1.ToString();
                    ctr++;
                }
                #endregion
                result.AppendLine(table2.ToString());
            }
            return result.ToString();
        }
        #endregion

    }
}
