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
using Mytrip.Mvc.Settings;
using Mytrip.Mvc.Helpers;

namespace Mytrip.Articles
{
    public static class Export
    {
        #region Top Menu
        public static HtmlString MenuArticle()
        {
            if (ModuleSetting.articles())
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
                article.InnerHtml = ModuleSetting.NameArticlesPage();
                IDictionary<int, string> _result = new Dictionary<int, string>();
                bool tab_category = false;
                int key = 1;
                foreach (var item in ar.category.GetCategories(false, LocalisationSetting.culture()))
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
                return new HtmlString(GeneralMethods.Menu(article.ToString(), _result, tab_category, false, true, drop));
            }
            else { return null; }
        }
        public static HtmlString MenuBlog()
        {
            if (ModuleSetting.blogs())
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
                article.InnerHtml = ModuleSetting.NameBlogsPage();
                IDictionary<int, string> _result = new Dictionary<int, string>();
                bool tab_category = false;
                int key = 1;
                foreach (var item in ar.category.GetBlogs(LocalisationSetting.culture()))
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
                return new HtmlString(GeneralMethods.Menu(article.ToString(), _result, tab_category, false, true, drop));
            }
            else { return null; }
        }
        public static HtmlString MenuCategory()
        {
            if (ModuleSetting.articles())
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
                foreach (var __item in ar.category.GetCategories(true, LocalisationSetting.culture()))
                {
                    TagBuilder article = new TagBuilder("a");
                    article.MergeAttribute("href", "/Article/Index/1/10/" + __item.CategoryId + "/" + __item.Path);
                    article.InnerHtml = __item.Title;
                    IDictionary<int, string> _result = new Dictionary<int, string>();
                    bool tab_category = false;
                    int key = 1;
                    foreach (var item in __item.mytrip_articlescategory1)
                    {
                        if (item.SubCategoryId != 0 && (item.AllCulture || item.Culture.ToLower() == LocalisationSetting.culture()))
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
                HtmlString htmlresult = new HtmlString(result.ToString());
                return htmlresult;
            }
            else { return null; }
        }
        #endregion

        #region SideBar
        public static HtmlString AccordionSearch()
        {
            TagBuilder form = new TagBuilder("form");
            string url = (HttpContext.Current.Request.Path.ToString() == "/") ? "/Home/Index" : HttpContext.Current.Request.Path.ToString();
            form.MergeAttribute("action", string.Concat("/Article/Search?url=",url));
            form.MergeAttribute("method", "post");
            form.InnerHtml = "<div class='search'><input  type='submit' value='' class='_search' ></input></div><input name='search' type='text' value='' class='search' />";
            return new HtmlString(GeneralMethods.Accordion(ModuleSetting.NameSearchPage(), form.ToString()));
        }
        public static HtmlString AccordionArticle()
        {
            if (ModuleSetting.articles())
            {
                TagBuilder _a = new TagBuilder("a");
                _a.MergeAttribute("href", "/Article/Index/1/10/0/Articles");
                _a.InnerHtml = ModuleSetting.NameArticlesPage();
                TagBuilder ul = new TagBuilder("ul");
                IArticleRepository ar = new IArticleRepository();
                StringBuilder _result = new StringBuilder();
                int count = 0;
                foreach (var item in ar.category.GetCategories(false, LocalisationSetting.culture()))
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
                return new HtmlString(GeneralMethods.Accordion(cssclacc, _a.ToString(), ul.ToString()));
            }
            else { return null; }
        }
        public static HtmlString AccordionBlogs()
        {
            if (ModuleSetting.blogs())
            {
                TagBuilder _a = new TagBuilder("a");
                _a.MergeAttribute("href", "/Article/Index/1/10/0/Blogs");
                _a.InnerHtml = ModuleSetting.NameBlogsPage();
                TagBuilder ul = new TagBuilder("ul");
                IArticleRepository ar = new IArticleRepository();
                StringBuilder _result = new StringBuilder();
                int count = 0;
                foreach (var item in ar.category.GetBlogs(LocalisationSetting.culture()))
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
                return new HtmlString(GeneralMethods.Accordion(cssclacc, _a.ToString(), ul.ToString()));
            }
            else { return null; }
        }
        public static HtmlString AccordionCategory()
        {
            if (ModuleSetting.articles())
            {
                IArticleRepository ar = new IArticleRepository();
                StringBuilder result = new StringBuilder();
                int category = 0;
                foreach (var _item in ar.category.GetCategories(true, LocalisationSetting.culture()))
                {
                    TagBuilder a_title = new TagBuilder("a");
                    a_title.MergeAttribute("href", "/Article/Index/1/10/" + _item.CategoryId + "/" + _item.Path);
                    a_title.InnerHtml = _item.Title;
                    TagBuilder ul = new TagBuilder("ul");
                    StringBuilder _result = new StringBuilder();
                    int count = 0;
                    foreach (var item in _item.mytrip_articlescategory1)
                    {
                        if (item.SubCategoryId != 0 && (item.AllCulture || item.Culture.ToLower() == LocalisationSetting.culture()))
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
                    result.AppendLine(GeneralMethods.Accordion(cssclacc, a_title.ToString(), ul.ToString()));
                    category++;
                }
                HtmlString htmlresult = new HtmlString(result.ToString());
                return htmlresult;
            }
            else { return null; }
        }
        public static HtmlString AccordionTag()
        {
            if (ModuleSetting.articles() | ModuleSetting.blogs())
            {
                IArticleRepository ar = new IArticleRepository();
                StringBuilder _result = new StringBuilder();
                string style = string.Empty;
                int _count = 0;
                foreach (var item in ar.article.GetAllTags())
                {
                    int count = item.mytrip_articles.Count(x => x.Culture.ToLower() == LocalisationSetting.culture()|| x.AllCulture == true);
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
                    return new HtmlString(GeneralMethods.Accordion(ModuleSetting.NameTagsPage(), _result.ToString()));
                }
                else { return null; }
            }
            else { return null; }
        }
        public static HtmlString AccordionArticlesActivity()
        {
            TagBuilder list = new TagBuilder("ol");
            list.AddCssClass("numbered");
            IArticleRepository ar = new IArticleRepository();
            TagBuilder title = new TagBuilder("center");
            title.InnerHtml = "<h4>" + ArticleLanguage.top_viewed+ "</h4>";
            foreach (var item in ar.article.GetArticles(LocalisationSetting.culture(),SortBy.Views, 5))
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
                 + "<input id='save' type='submit' name='option' value='" + ArticleLanguage.recent_articles + "' class=\"link\" />"
                  + "<input id='save' type='submit' name='option' value='" + ArticleLanguage.recent_comments + "' class=\"link\" /></center>";
            TagBuilder form = new TagBuilder("form");
            form.MergeAttribute("action", "/Article/ArticlesActivity");
            form.MergeAttribute("method", "post");
            form.MergeAttribute("onclick", "Sys.Mvc.AsyncForm.handleClick(this, new Sys.UI.DomEvent(event));");
            form.MergeAttribute("onsubmit", "Sys.Mvc.AsyncForm.handleSubmit(this, new Sys.UI.DomEvent(event), { insertionMode: Sys.Mvc.InsertionMode.replace , updateTargetId:\"populararticles\" });");
            form.InnerHtml = div.ToString();
            return new HtmlString(GeneralMethods.Accordion(string.Format(ArticleLanguage.articles_popular_recent, ModuleSetting.NameArticlesPage()), form.ToString()));
        }
        public static HtmlString AccordionBlogsActivity()
        {
            TagBuilder list = new TagBuilder("ol");
            list.AddCssClass("numbered");
            IArticleRepository ar = new IArticleRepository();
            TagBuilder title = new TagBuilder("center");
            title.InnerHtml = "<h4>" + ArticleLanguage.top_viewed + "</h4>";
            foreach (var item in ar.article.GetPostsPopular(LocalisationSetting.culture(), 5))
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
                 + "<input id='save' type='submit' name='option' value='" + ArticleLanguage.recent_posts + "' class=\"link\" />"
                  + "<input id='save' type='submit' name='option' value='" + ArticleLanguage.recent_comments + "' class=\"link\" /></center>";
            TagBuilder form = new TagBuilder("form");
            form.MergeAttribute("action", "/Article/PostsActivity");
            form.MergeAttribute("method", "post");
            form.MergeAttribute("onclick", "Sys.Mvc.AsyncForm.handleClick(this, new Sys.UI.DomEvent(event));");
            form.MergeAttribute("onsubmit", "Sys.Mvc.AsyncForm.handleSubmit(this, new Sys.UI.DomEvent(event), { insertionMode: Sys.Mvc.InsertionMode.replace, updateTargetId:\"popularblogs\" });");
            form.InnerHtml = div.ToString();
            return new HtmlString(GeneralMethods.Accordion(string.Format(ArticleLanguage.blogs_popular_recent, ModuleSetting.NameBlogsPage()), form.ToString()));
        }
        #endregion

        #region HomePage
        public static HtmlString HomePage(int categoryId, int line, int column, int content, int imgwidth, int style, bool viewtitle)
        {
            if (categoryId == -1)
                return new HtmlString(ArticlesHomeHelper._HP(false, line, column, content, imgwidth, style, viewtitle));
            else if (categoryId == -2)
                return new HtmlString(ArticlesHomeHelper._HP(true, line, column, content, imgwidth, style, viewtitle));
            else
            {
                IArticleRepository ar = new IArticleRepository();
                int take = line * column;
                IQueryable<mytrip_articles> articles = ar.article.GetArticles(categoryId,LocalisationSetting.culture(), take);
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
                    _content = ArticlesHomeHelper.articleContent(article, content, imgwidth);
                    
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
                    return new HtmlString(_CategoryName + start + table.ToString() + end);
                else
                    return null;
            }
        }
        #endregion

        #region Control Panel
        public static HtmlString Setting()
        {
            TagBuilder li = new TagBuilder("li");
            TagBuilder a = new TagBuilder("a");
            a.MergeAttribute("href", "/Article/Setting");
            a.InnerHtml = ArticleLanguage.article_setting;
            li.InnerHtml = a.ToString();
            HtmlString htmlresult = new HtmlString(li.ToString());
            return htmlresult;

        }
        public static HtmlString Manager()
        {
            /////
            TagBuilder li_archive = new TagBuilder("li");
            TagBuilder a_archive = new TagBuilder("a");
            a_archive.MergeAttribute("href", "/Article/Archive");
            a_archive.InnerHtml = ArticleLanguage.articles_manager;
            li_archive.InnerHtml = a_archive.ToString();
            /////
            TagBuilder li_jm = new TagBuilder("li");
            TagBuilder a_jm = new TagBuilder("a");
            a_jm.MergeAttribute("href", "/Article/Editors");
            a_jm.InnerHtml = ArticleLanguage.journalists_manager;
            li_jm.InnerHtml = a_jm.ToString();
            /////
            HtmlString htmlresult = new HtmlString(li_archive.ToString() + li_jm.ToString());
            return htmlresult;
        }
        #endregion

        #region Profile
        public static HtmlString Profile()
        {
            if (ModuleSetting.blogs() && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                IArticleRepository ar = new IArticleRepository();
                StringBuilder _result = new StringBuilder();
                TagBuilder ul = new TagBuilder("ul");
                int countcomment = ar.comment.GetCount(HttpContext.Current.User.Identity.Name);
                if ((!ModuleSetting.closecountCommentForBlogs()
                    && countcomment >= ModuleSetting.countCommentForBlogs()
                    && !MytripUser.UserInRole(ModuleSetting.roleBlogger()))
                    || MytripUser.UserInRole(ModuleSetting.roleBlogger()))
                {
                    if (ar.category.GetBlogsByUser(HttpContext.Current.User.Identity.Name, LocalisationSetting.culture()).Count() == 0)
                    {
                        TagBuilder li = new TagBuilder("li");
                        TagBuilder a_createblog = new TagBuilder("a");
                        a_createblog.MergeAttribute("href", "/Article/CreateBlog");
                        a_createblog.InnerHtml = ArticleLanguage.create_blog;
                        li.InnerHtml = a_createblog.ToString();
                        _result.AppendLine(li.ToString());
                    }
                    foreach (var item in ar.category.GetBlogsByUser(HttpContext.Current.User.Identity.Name))
                    {
                        TagBuilder li = new TagBuilder("li");
                        TagBuilder a_blog = new TagBuilder("a");
                        if (item.Culture == LocalisationSetting.culture())
                        {
                            a_blog.MergeAttribute("href", "/Article/Index/1/10/" + item.CategoryId + "/" + item.Path);
                            a_blog.InnerHtml = item.Title;
                        }
                        else
                        {
                            a_blog.MergeAttribute("href", "/MytripMvc/Language/" + item.Culture.ToLower() + "/(x)Article(x)Index(x)1(x)10(x)" + item.CategoryId + "(x)" + item.Path);
                            
                            TagBuilder flag = new TagBuilder("img");
                            flag.MergeAttribute("src", "/Theme/" + ThemeSetting.theme() + "/images/" + item.Culture.ToLower() + ".png");
                            flag.MergeAttribute("style", "width:14px");
                            flag.MergeAttribute("alt", item.Culture.ToLower());
                            flag.MergeAttribute("title", item.Culture.ToLower());
                            a_blog.InnerHtml = item.Title + " " + flag;
                        }
                        li.InnerHtml = a_blog.ToString();
                        _result.AppendLine(li.ToString());
                    }
                }
                else if (!ModuleSetting.closecountCommentForBlogs())
                {
                    TagBuilder li = new TagBuilder("li");
                    int count = ModuleSetting.countCommentForBlogs() - countcomment;
                    TagBuilder a_com = new TagBuilder("a");
                    a_com.InnerHtml = string.Format(ArticleLanguage.activation_blog, count);
                    li.InnerHtml = a_com.ToString();
                    _result.AppendLine(li.ToString());
                }
                ul.InnerHtml = _result.ToString();
                return new HtmlString(ul.ToString());
            }
            else { return null; }
        }

        public static List<LastActivity> LastActivity(string username)
        {
            var activities = ProfileHelpers.GetLastActivities(username, "Blogs");
            activities.AddRange(ProfileHelpers.GetLastActivities(username, "Articles"));
            if (HttpContext.Current.User.Identity.Name == username || MytripUser.UserInRole(ModuleSetting.roleChiefEditor()))
            {
                activities.AddRange(ProfileHelpers.GetLastActivities(username, "Unapproved"));
            }
            return activities;
        }
        #endregion

    }
}
