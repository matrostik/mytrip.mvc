using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;
using Mytrip.Mvc.Models;
using System.Collections;
using Mytrip.Mvc.Helpers;
using Mytrip.Articles.Repository;
using Mytrip.Mvc.Repository;
using Mytrip.Articles.Repository.DataEntities;
using Mytrip.Mvc;

namespace Mytrip.Articles.Helpers
{
    public static class ArticlesHelper
    {

        #region Show Edit Delete Rating Rss .......
        public static string ArticleRssLink(this HtmlHelper html, string title, string path, int id, int width)
        {
            if (path.StartsWith("(Search)"))
            { return string.Empty; }
            else
            {
                ThemeSetting theme = new ThemeSetting();
                StringBuilder result = new StringBuilder();
                TagBuilder img = new TagBuilder("img");
                img.MergeAttribute("src", "/Theme/" + theme.theme() + "/images/rss.png");
                img.MergeAttribute("alt", "Rss " + path);
                img.MergeAttribute("style", "width:" + width + "px;");
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
                else if (path.StartsWith("(Tag)")) {
                    string _title = GeneralMethods.DecodingSearch(title);
                    a.MergeAttribute("href", "/RssArticle/RssArticlesInTag/" + id + "/" + path + "/" + _title); 
                }
                else
                {
                    string _title = GeneralMethods.DecodingSearch(title);
                    a.MergeAttribute("href", "/RssArticle/RssArticlesInCategory/" + id + "/" + path + "/" + _title);
                }
                a.InnerHtml = img.ToString();
                result.AppendLine(a.ToString());
                return result.ToString();
            }
        }
        public static string _ArticleRssLink(string title, string path, int id, int width)
        {
            if (path.StartsWith("(Search)"))
            { return string.Empty; }
            else
            {
                ThemeSetting theme = new ThemeSetting();
                StringBuilder result = new StringBuilder();
                TagBuilder img = new TagBuilder("img");
                img.MergeAttribute("src", "/Theme/" + theme.theme() + "/images/rss.png");
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
                else if (path.StartsWith("(Tag)"))
                {
                    string _title = GeneralMethods.DecodingSearch(title);
                    a.MergeAttribute("href", "/RssArticle/RssArticlesInTag/" + id + "/" + path + "/" + _title);
                }
                else
                {
                    string _title = GeneralMethods.DecodingSearch(title);
                    a.MergeAttribute("href", "/RssArticle/RssArticlesInCategory/" + id + "/" + path + "/" + _title);
                }
                a.InnerHtml = img.ToString();
                result.AppendLine(a.ToString());
                return result.ToString();
            }
        }
        public static string EditorCategory(this HtmlHelper html, bool ShowAddCategory, bool ShowAddSubCategory, bool ShowAddArticle, bool ShowAddBlog, bool ShowAddPost, int id)
        {
            CategoryRepository cr = new CategoryRepository();
            mytrip_articlescategory category = cr.GetCategory(id);
            StringBuilder result = new StringBuilder();
            if (isUserHasRights(category, true))
            {
                ThemeSetting theme = new ThemeSetting();
                TagBuilder img = new TagBuilder("img");
                img.MergeAttribute("src", "/Theme/" + theme.theme() +"/images/add.png");
                img.MergeAttribute("style", "width:20px;");
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
                    if (id != 0)
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
                if (isUserHasRights(obj, false))
                {
                    ThemeSetting theme = new ThemeSetting();
                    TagBuilder imgEdit = new TagBuilder("img");
                    imgEdit.MergeAttribute("src", "/Theme/" + theme.theme() + "/images/edite.png");
                    imgEdit.MergeAttribute("title", ArticleLanguage.edit);
                    imgEdit.MergeAttribute("style", "width:14px");
                    TagBuilder imgDelete = new TagBuilder("img");
                    imgDelete.MergeAttribute("src", "/Theme/" + theme.theme() + "/images/delete.png");
                    imgDelete.MergeAttribute("title", ArticleLanguage.delete);
                    imgDelete.MergeAttribute("style", "width:14px;");
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

        public static string ParrentCategory(this HtmlHelper html, mytrip_articlescategory parentCat, string path)
        {
            StringBuilder result = new StringBuilder();
            if (parentCat.CategoryId != -1 && parentCat.Path != path)
            {
                TagBuilder h2 = new TagBuilder("h2");
                h2.AddCssClass("title");
                TagBuilder a = new TagBuilder("a");
                a.MergeAttribute("href", "/Article/Index/1/10/" + parentCat.CategoryId + "/" + parentCat.Path);
                a.InnerHtml = parentCat.Title;
                h2.InnerHtml = a.ToString();
                result.AppendLine(h2.ToString());
            }
            return result.ToString();
        }

        public static string ShowDetailsBlog(this HtmlHelper html, bool showDetailsBlog, mytrip_articlescategory parentCat)
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
                TagBuilder profile = new TagBuilder("a");
                profile.MergeAttribute("href", "/Home/Profile/" + parentCat.UserName);
                profile.MergeAttribute("title", ArticleLanguage.view_user_profile);
                profile.InnerHtml = parentCat.UserName;
                result.AppendLine(ArticleLanguage.author + ": " + profile + "<br/>");
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

        public static string ShowArticle(this HtmlHelper html, mytrip_articles article)
        {
            RoleRepository db = new RoleRepository();
            StringBuilder result = new StringBuilder();
            TagBuilder h2 = new TagBuilder("h2");
            h2.AddCssClass("title");
            if (isUserHasRights(article, false))
            {
                ThemeSetting theme = new ThemeSetting();
                TagBuilder imgEdit = new TagBuilder("img");
                imgEdit.MergeAttribute("src", "/Theme/" + theme.theme() + "/images/edite.png");
                imgEdit.MergeAttribute("title", ArticleLanguage.edit);
                imgEdit.MergeAttribute("style", "width:14px;");
                TagBuilder imgDelete = new TagBuilder("img");
                imgDelete.MergeAttribute("src", "/Theme/" + theme.theme() + "/images/delete.png");
                imgDelete.MergeAttribute("title", ArticleLanguage.delete);
                imgDelete.MergeAttribute("style", "width:14px;");
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
            string body = article.Body.Replace("[SilverWmvStart]", StartWmvPlaer());
            body = body.Replace("[SilverWmvEnd]", EndWmvPlaer());
            string start = "<div><div class=\"contenttopright\"></div><div class=\"contenttopleft\"></div><div class=\"contenttopcon\"></div></div><div class=\"content2\">";
            string end = "</div><div><div class=\"contentbottomright\"></div><div class=\"contentbottomleft\"></div><div class=\"contentbottomcon\"></div></div>";

            result.AppendLine(h2.ToString() + start + body + end + "<div class=\"acfooter\"></div>");
            return result.ToString();
        }

        public static string ShowArticleTags(this HtmlHelper html, mytrip_articles article)
        {
            StringBuilder result = new StringBuilder();
            int count = 0;
            foreach (var tag in article.mytrip_articlestag)
            {
                TagBuilder tagLink = new TagBuilder("a");
                tagLink.MergeAttribute("href", "/Article/Index/1/10/" + tag.TagId + "/" + tag.Path);
                tagLink.SetInnerText(tag.TagName);
                result.AppendLine(tagLink.ToString());
                count++;
            }
            if (count>0)
                return result.ToString() + "<br/>";
            else
                return string.Empty;
        }

        public static string ShowRelated(this HtmlHelper html, mytrip_articles article)
        {
            IArticleRepository ar = new IArticleRepository();
            TagBuilder list = new TagBuilder("ol");
            list.AddCssClass("numbered");
            foreach (var item in ar.article.GetArticlesSimilar(HttpContext.Current.Session["culture"].ToString(),article.mytrip_articlescategory.Blog, article.Title, 5))
            {
                TagBuilder li = new TagBuilder("li");
                TagBuilder a = new TagBuilder("a");
                a.MergeAttribute("href", "/Article/View/" + item.ArticleId + "/" + item.Path);
                a.MergeAttribute("title", item.Title);
                a.InnerHtml = item.Title;
                li.InnerHtml = a.ToString();
                list.InnerHtml += li.ToString();
            }
            return "<b><u>" + ArticleLanguage.related_links + "</b></u>" + list.ToString();
        }

        public static string ShowComments(this HtmlHelper html, mytrip_articles article)
        {
            ArticlesSetting artset = new ArticlesSetting();
            ThemeSetting theme = new ThemeSetting();
            StringBuilder result = new StringBuilder();
            #region Comments title
            bool blog = article.mytrip_articlescategory.Blog;
            TagBuilder tlegend = new TagBuilder("h3");
            tlegend.AddCssClass("title");
            if (!blog && isUserHasRights(article, false))
            {
                if (!article.ApprovedComment)
                {
                    TagBuilder imgOpen = new TagBuilder("img");
                    imgOpen.MergeAttribute("src", "/Theme/" + theme.theme() + "/images/approved.png");
                    imgOpen.MergeAttribute("title", ArticleLanguage.open_comments);
                    imgOpen.MergeAttribute("style", "width:14px;");
                    TagBuilder OpenComments = new TagBuilder("a");
                    OpenComments.MergeAttribute("href", "/Article/OpenComments/" + article.ArticleId + "/" + article.Path);
                    OpenComments.InnerHtml = imgOpen.ToString();
                    tlegend.InnerHtml = OpenComments.ToString();
                }
                else
                {
                    TagBuilder imgClose = new TagBuilder("img");
                    imgClose.MergeAttribute("src", "/Theme/" + theme.theme() + "/images/noapproved.png");
                    imgClose.MergeAttribute("title", ArticleLanguage.close_comments);
                    imgClose.MergeAttribute("style", "width:14px;");
                    TagBuilder CloseComments = new TagBuilder("a");
                    CloseComments.MergeAttribute("href", "/Article/CloseComments/" + article.ArticleId + "/" + article.Path);
                    CloseComments.InnerHtml = imgClose.ToString();
                    tlegend.InnerHtml = CloseComments.ToString();
                }
            }
            if (!article.ApprovedComment && artset.viewInfoClosedComments())
            { 
                tlegend.InnerHtml += " " + ArticleLanguage.comments_closed; 
            }
            else if (article.ApprovedComment&&!artset.replaceСommentsEmail())
            {
                tlegend.InnerHtml += " " + ArticleLanguage.comments;
                if (article.mytrip_articlescomments.Count == 0)
                    tlegend.InnerHtml += " ( " + ArticleLanguage.no_comments_yet + " ) ";
            }
            else if (article.ApprovedComment&&!blog&&artset.replaceСommentsEmail())
            {
                tlegend.InnerHtml += " " + ArticleLanguage.replace_comments_email;
            }
            result.AppendLine(tlegend.ToString());
            #endregion
            if (blog||!artset.replaceСommentsEmail())
            {
                #region Comments
                string start = "<div><div class=\"contenttopright\"></div><div class=\"contenttopleft\"></div><div class=\"contenttopcon\"></div></div><div class=\"content\">";
                string end = "</div><div><div class=\"contentbottomright\"></div><div class=\"contentbottomleft\"></div><div class=\"contentbottomcon\"></div></div>";
                //result.AppendLine(start);
                int count = 1;
                foreach (var comment in article.mytrip_articlescomments.OrderBy(x => x.CreateDate))
                {
                    TagBuilder fieldset = new TagBuilder("fieldset");
                    fieldset.MergeAttribute("style", "padding: 0;border:0;margin:0;");
                    fieldset.MergeAttribute("id", comment.CommentId.ToString());
                    TagBuilder legend = new TagBuilder("b");
                    legend.AddCssClass("title");
                    if (isUserHasRights(comment, false))
                    {
                        TagBuilder imgEdit = new TagBuilder("img");
                        imgEdit.MergeAttribute("src", "/Theme/" + theme.theme() + "/images/edite.png");
                        imgEdit.MergeAttribute("title", ArticleLanguage.edit_comment);
                        imgEdit.MergeAttribute("style", "width:14px;");
                        TagBuilder imgDelete = new TagBuilder("img");
                        imgDelete.MergeAttribute("src", "/Theme/" + theme.theme() + "/images/delete.png");
                        imgDelete.MergeAttribute("title", ArticleLanguage.delete);
                        imgDelete.MergeAttribute("style", "width:14px;");
                        TagBuilder editComment = new TagBuilder("a");
                        editComment.MergeAttribute("href", "/Article/EditComment/" + comment.CommentId + "/" + comment.mytrip_articles.Path);
                        editComment.InnerHtml = imgEdit.ToString();
                        TagBuilder deleteComment = new TagBuilder("a");
                        deleteComment.MergeAttribute("href", "/Article/DeleteComment/" + comment.CommentId + "/" + comment.mytrip_articles.Path);
                        deleteComment.MergeAttribute("onclick", "return confirm ('" + ArticleLanguage.are_you_sure + "');");
                        deleteComment.InnerHtml = imgDelete.ToString();
                        legend.InnerHtml += editComment.ToString() + " " + deleteComment.ToString() + " ";
                    }

                    TagBuilder profile = new TagBuilder("a");
                    if (comment.IsAnonym)
                        profile.SetInnerText(comment.UserName + "(" + ArticleLanguage.guest + ")");
                    else
                    {
                        profile.MergeAttribute("href", "/Home/Profile/" + comment.UserName);
                        profile.MergeAttribute("title", ArticleLanguage.view_user_profile);
                        profile.InnerHtml = comment.UserName;
                    }
                    legend.InnerHtml += "  #" + count + " " + profile + " " + comment.CreateDate.ToString("dd MMMM yy HH:mm");
                    TagBuilder divGravatar = new TagBuilder("div");
                    divGravatar.MergeAttribute("style", "position: relative; float: right");
                    divGravatar.InnerHtml = AvatarHelper.Avatar(html, comment.UserEmail, new { width = 50 });
                    string starttable = "<table style=\"border:0;padding: 0;border:0;margin:0;\"><tr><td style=\"border:0;padding: 0;border:0;margin:0;\">";
                    string endtable = "</td></tr></table>";
                    fieldset.InnerHtml = start + starttable + divGravatar.ToString() + legend.ToString() + "<br/>" + comment.Body + endtable + end + "<div class=\"acfooter\"></div>";
                    result.AppendLine(fieldset.ToString());
                    count++;
                }
                #endregion
            }
            return result.ToString();
        }

        public static string ListCategories(this HtmlHelper html, IQueryable<mytrip_articlescategory> categories, string path)
        {
            StringBuilder result = new StringBuilder();
            ThemeSetting theme = new ThemeSetting();
            foreach (var category in categories)
            {
                int count = CountPosts(category);
                if (category.Blog&&category.SubCategoryId==0)
                {
                    TagBuilder tr = new TagBuilder("tr");
                    TagBuilder td_first = new TagBuilder("td");
                    td_first.MergeAttribute("style", "border-top:0px;border-bottom:0px;border-right:0px;");
                    if (isUserHasRights(category, false))
                    {
                        TagBuilder imgEdit = new TagBuilder("img");
                        imgEdit.MergeAttribute("src", "/Theme/" + theme.theme() + "/images/edite.png");
                        imgEdit.MergeAttribute("title", ArticleLanguage.edit);
                        imgEdit.MergeAttribute("style", "width:14px;");
                        TagBuilder imgDelete = new TagBuilder("img");
                        imgDelete.MergeAttribute("src", "/Theme/" + theme.theme() + "/images/delete.png");
                        imgDelete.MergeAttribute("title", ArticleLanguage.delete);
                        imgDelete.MergeAttribute("style", "width:14px;");
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
                    TagBuilder profile = new TagBuilder("a");
                    profile.MergeAttribute("href", "/Home/Profile/" + category.UserName);
                    profile.MergeAttribute("title", ArticleLanguage.view_user_profile);
                    profile.InnerHtml = category.UserName;
                    TagBuilder td_last = new TagBuilder("td");
                    td_last.MergeAttribute("style", "width: 170px;border:0;");
                    td_last.InnerHtml += ArticleLanguage.author + ": " + profile + "<br/>";
                    td_last.InnerHtml += ArticleLanguage.views + ": " + category.Views + "<br/>";
                    td_last.InnerHtml += ArticleLanguage.posts + ": " + count + "<br/>";
                    td_last.InnerHtml += ArticleLanguage.create_date + ": " + String.Format("{0:dd MMMM yyyy}", category.CreateDate);
                    tr.InnerHtml = td_last.ToString() + td_first.ToString();
                    TagBuilder table = new TagBuilder("table");
                    table.AddCssClass("homepage");
                    table.InnerHtml = tr.ToString();
                    string start = "<div><div class=\"contenttopright\"></div><div class=\"contenttopleft\"></div><div class=\"contenttopcon\"></div></div><div class=\"content\">";
                    string end = "</div><div style=\"margin-bottom: 1px\"><div class=\"contentbottomright\"></div><div class=\"contentbottomleft\"></div><div class=\"contentbottomcon\"></div></div>";
            
                    result.AppendLine(start+table.ToString()+end);
                }
                else
                {
                    TagBuilder h4 = new TagBuilder("h4");
                    h4.AddCssClass("title");
                    if (isUserHasRights(category, false) || (category.mytrip_articlescategory2.Blog && isUserHasRights(category, false)))
                    {
                        TagBuilder imgEdit = new TagBuilder("img");
                        imgEdit.MergeAttribute("src", "/Theme/" + theme.theme() + "/images/edite.png");
                        imgEdit.MergeAttribute("title", ArticleLanguage.edit);
                        imgEdit.MergeAttribute("style", "width:14px;");
                        TagBuilder imgDelete = new TagBuilder("img");
                        imgDelete.MergeAttribute("src", "/Theme/" + theme.theme() + "/images/delete.png");
                        imgDelete.MergeAttribute("title", ArticleLanguage.delete);
                        imgDelete.MergeAttribute("style", "width:14px;");
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
                    if (category.mytrip_articlescategory2.Blog)
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
            return result.ToString();
        }

        public static string ArticleRating(this HtmlHelper html, bool enable, bool active, decimal totalvotes, int votescount)
        {
            string url = HttpContext.Current.Request.Url.AbsolutePath;
            return GeneralMethods.CoreRating(enable, active, (double)totalvotes, votescount);
        }

        public static string ArticleRating(this HtmlHelper html, bool enable, bool active, decimal totalvotes)
        {
            return GeneralMethods.CoreRating(enable, active, (double)totalvotes, -1);
        }

        public static string ArticleSpecification(this HtmlHelper html, mytrip_articles article, string path)
        {
            return ArticleInfo(path, article, true);
        }

        public static string ArticleSpecification(this HtmlHelper html, mytrip_articles article)
        {
            return ArticleInfo("Articles", article, true);
        }

        public static string ImageForAbstract(this HtmlHelper html, string image, int width)
        {
            if (image != null && image.IndexOf("src") != -1)
            {
                int q = image.IndexOf("src");
                image = image.Remove(0, q);
                int qq = image.IndexOf("\"");
                image = image.Remove(0, (qq + 1));
                int qqq = image.IndexOf("\"");
                image = image.Remove(qqq);
                TagBuilder img = new TagBuilder("img");
                img.MergeAttribute("src", image);
                img.MergeAttribute("alt", "");
                img.AddCssClass("imgabstract");
                img.MergeAttribute("style", "width:" + width + "px; border:0;");
                return img.ToString();
            }
            else
                return string.Empty;
        }
        #endregion

        #region Additional Methods
        /// <summary>
        /// Method to check rights of the current user
        /// </summary>
        /// <param name="obj">Rights for</param>
        /// <returns>Bool</returns>
        private static bool isUserHasRights(object obj, bool forAdd)
        {
            string categoryUserName = "";
            string category2UserName = "";
            string articleUserName = "";
            bool isBlog = true;
            RoleRepository db = new RoleRepository();
            ArticlesSetting artset = new ArticlesSetting();
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
                return false;
            if (db.IsUserInRoleOnline(artset.roleChiefEditor()))
                return true;
            else
            {
                if (obj is mytrip_articlescategory)
                {
                    mytrip_articlescategory category = obj as mytrip_articlescategory;
                    categoryUserName = category.UserName;
                    if (category.SubCategoryId != 0)
                    {
                        category2UserName = category.mytrip_articlescategory2.UserName;
                        isBlog = category.mytrip_articlescategory2.Blog|| category.Blog;
                    }
                    else
                    isBlog = category.Blog;
                }
                else if (obj is mytrip_articles)
                {
                    mytrip_articles article = obj as mytrip_articles;
                    articleUserName = article.UserName;
                    categoryUserName = article.mytrip_articlescategory.UserName;
                    category2UserName = article.mytrip_articlescategory.mytrip_articlescategory2.UserName;
                    isBlog = article.mytrip_articlescategory.mytrip_articlescategory2.Blog;
                }
                else if (obj is mytrip_articlescomments)
                {
                    mytrip_articlescomments comment = obj as mytrip_articlescomments;
                    if (!comment.IsAnonym && HttpContext.Current.User.Identity.Name == comment.UserName)
                        return true;
                    articleUserName = comment.mytrip_articles.UserName;
                    categoryUserName = comment.mytrip_articles.mytrip_articlescategory.UserName;
                    category2UserName = comment.mytrip_articles.mytrip_articlescategory.mytrip_articlescategory2.UserName;
                    isBlog = comment.mytrip_articles.mytrip_articlescategory.mytrip_articlescategory2.Blog;
                }
                else if (obj is mytrip_articlestag)
                {
                    forAdd = true;
                    isBlog = false;
                }
                bool articleEditor = db.IsUserInRoleOnline(artset.roleArticleEditor());
                if (db.IsUserInRoleOnline(artset.roleBlogger()) || articleEditor)
                {
                    if (HttpContext.Current.User.Identity.Name == articleUserName || HttpContext.Current.User.Identity.Name == categoryUserName || HttpContext.Current.User.Identity.Name == category2UserName)
                        return true;
                    if (forAdd && articleEditor && (!isBlog || obj == null))
                        return true;
                }
            }
            return false;
        }
        static int CountPosts(mytrip_articlescategory category)
        {
            int count = category.mytrip_articles.Count();
            foreach (mytrip_articlescategory cat in category.mytrip_articlescategory1)
            {
                if (cat.SubCategoryId != 0)
                {
                    count += cat.mytrip_articles.Count();
                }
            }
            return count;
        }
        #endregion

        /*PRIVATE*/
        #region Private Helpers
        private static string StartWmvPlaer()
        {
            UsersSetting userset = new UsersSetting();
            return "<div class='silverlightControlHost'>" +
            "<object data='data:application/x-silverlight,' type='application/x-silverlight' width='480px' height='360px'>" +
                "<param name='source' value='/Scripts/SLPlaerChrome.xap'/>" +
                "<param name='onerror' value='onSilverlightError' />" +
                "<param name='autoUpgrade' value='true' />" +
                "<param name='minRuntimeVersion' value='3.0.40624.0' />" +
                "<param name='enableHtmlAccess' value='true' />" +
                "<param name='enableGPUAcceleration' value='true' />" +
                "<param name='initparams' value='playerSettings = " +
                            "<Playlist>" +
                                "<AutoLoad>false</AutoLoad>" +
                                "<AutoPlay>false</AutoPlay>" +
                                "<DisplayTimeCode>false</DisplayTimeCode>" +
                                "<EnableCachedComposition>true</EnableCachedComposition>" +
                                "<EnableCaptions>true</EnableCaptions>" +
                                "<EnableOffline>true</EnableOffline>" +
                                "<EnablePopOut>true</EnablePopOut>" +
                                "<StartMuted>false</StartMuted>" +
                                "<StretchMode>None</StretchMode>" +
                                "<Items>" +
                                    "<PlaylistItem>" +
                                        "<AudioCodec>WmaProfessional</AudioCodec>" +
                                        "<IsAdaptiveStreaming>false</IsAdaptiveStreaming>" +
                                        "<MediaSource>http://" + userset.applicationName();
            //value + 

        }
        private static string EndWmvPlaer()
        {

            return "</MediaSource>" +
                                            "<VideoCodec>VC1</VideoCodec>" +
                                        "</PlaylistItem>" +
                                    "</Items>" +
                                "</Playlist>'/>" +
                     "<div onmouseover='highlightDownloadArea(true)' onmouseout='highlightDownloadArea(false)'>" +
                            "<img src='' style='position:absolute;width:100%;height:100%;border-style:none;' onerror='this.style.display='none''/>" +
                            "<img src='Preview.png' style='position:absolute;width:100%;height:100%;border-style:none;' onerror='this.style.display='none''/>" +
                            "<div id='overlay' class='fadeLots' style='position:absolute;width:100%;height:100%;border-style:none;background-color:white;'/></div>" +
                            "<table width='100%' height='100%' style='position:absolute;'><tr><td align='center' valign='middle'>" +
                           " <img src='http://go2.microsoft.com/fwlink/?LinkId=108181' alt='Get Microsoft Silverlight'>" +
                           " </td></tr></table> " +
                           " <a href='http://go2.microsoft.com/fwlink/?LinkID=124807 '>" +
                            "    <img src='' class='fadeCompletely' style='position:absolute;width:100%;height:100%;border-style:none;' alt='Get Microsoft Silverlight'/>" +
                            "</a>" +
                "</object> </div>";
        }
        /// <summary>
        /// Article Information (category, subcategory, author, comments and etc.)
        /// </summary>
        /// <param name="path">path</param>
        /// <param name="article">article</param>
        /// <param name="isDetailed">isDetailed</param>
        /// <returns>string</returns>
        public static string ArticleInfo(string path, mytrip_articles article, bool isDetailed)
        {
            ArticlesSetting artset = new ArticlesSetting();
            bool blog = article.mytrip_articlescategory.Blog;
            StringBuilder result = new StringBuilder();
            if (path == "Articles" | path.StartsWith("(Tag)") | path.StartsWith("(Search)"))
            {
                if (article.mytrip_articlescategory.SubCategoryId == 0)
                {
                    TagBuilder a_category = new TagBuilder("a");
                    a_category.MergeAttribute("href", "/Article/Index/1/10/" + article.CategoryId + "/" + article.mytrip_articlescategory.Path);
                    a_category.InnerHtml = article.mytrip_articlescategory.Title;
                    if (blog)
                        result.AppendLine(ArticleLanguage.blog + ": " + a_category + "<br/>");
                    else
                        result.AppendLine(ArticleLanguage.category + ": " + a_category + "<br/>");
                }
                else
                {
                    TagBuilder a_category = new TagBuilder("a");
                    a_category.MergeAttribute("href", "/Article/Index/1/10/" + article.mytrip_articlescategory.SubCategoryId
                        + "/" + article.mytrip_articlescategory.mytrip_articlescategory2.Path);
                    a_category.InnerHtml = article.mytrip_articlescategory.mytrip_articlescategory2.Title;
                    if (blog)
                        result.AppendLine(ArticleLanguage.blog + ": " + a_category + "<br/>");
                    else
                        result.AppendLine(ArticleLanguage.category + ": " + a_category + "<br/>");

                    TagBuilder a_subcategory = new TagBuilder("a");
                    a_subcategory.MergeAttribute("href", "/Article/Index/1/10/" + article.mytrip_articlescategory.CategoryId + "/" + article.mytrip_articlescategory.Path);
                    a_subcategory.InnerHtml = article.mytrip_articlescategory.Title;
                    if (blog)
                        result.AppendLine(ArticleLanguage.topic + ": " + a_subcategory + "<br/>");
                    else
                        result.AppendLine(ArticleLanguage.subcategory + ": " + a_subcategory + "<br/>");
                }
            }
            else
            {
                if (article.mytrip_articlescategory.Path != path)
                {
                    if (article.mytrip_articlescategory.SubCategoryId==0)
                        result.AppendLine(ArticleLanguage.category + ": ");
                    else if (blog)
                        result.AppendLine(ArticleLanguage.topic + ": ");
                    else
                        result.AppendLine(ArticleLanguage.subcategory + ": ");
                    TagBuilder a_2category = new TagBuilder("a");
                    a_2category.MergeAttribute("href", "/Article/Index/1/10/" + article.CategoryId + "/" + article.mytrip_articlescategory.Path);
                    a_2category.InnerHtml = article.mytrip_articlescategory.Title;
                    result.AppendLine(a_2category + "<br/>");
                }
            }
            if (isDetailed)
            {
                TagBuilder profile = new TagBuilder("a");
                profile.MergeAttribute("href", "/Home/Profile/" + article.UserName);
                profile.MergeAttribute("title", ArticleLanguage.view_user_profile);
                profile.InnerHtml = article.UserName;
                if(!blog && artset.viewInfoAuthorArticle())
                result.AppendLine(ArticleLanguage.author + ": " + profile + "<br/>");
                else if(blog)
                    result.AppendLine(ArticleLanguage.author + ": " + profile + "<br/>");
                if (!blog && artset.viewInfoViewsArticle())
                result.AppendLine(ArticleLanguage.views + ": " + article.Views + "<br/>");
                else if (blog)
                    result.AppendLine(ArticleLanguage.views + ": " + article.Views + "<br/>");
                if (article.ApprovedComment)
                    result.AppendLine(ArticleLanguage.comments + ": " + article.mytrip_articlescomments.Count + "<br/>");
                result.AppendLine(ArticleLanguage.create_date + ": " + String.Format("{0:dd MMMM yyyy}", article.CreateDate));
            }
            return result.ToString();
        }
        
        #endregion
    }
}
