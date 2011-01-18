using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mtm.Core;
using System.Web.Mvc;
using mtm.Articles.Repository;
using mtm.Core.Repository;
using mtm.Core.Repository.DataEntities;
using mtm.Articles.Repository.DataEntities;
using System.Web;
using mtm.Core.Settings;
using mtm.Core.Helpers;

namespace mtm.Articles.Helpers
{
    public static class ProfileHelpers
    {
        #region LastActitvity
        /// <summary>
        /// Get collection of last activities
        /// </summary>
        /// <param name="username">username</param>
        /// <param name="path">path</param>
        /// <returns></returns>
        public static List<LastActivity> GetLastActivities(string username, string path)
        {
            List<LastActivity> la = new List<LastActivity>();
            IArticleRepository ar = new IArticleRepository();
            if (path == "Articles" || path == "All")
            {
                var articles = ar.article.GetArticles(username, false);
                foreach (var a in articles)
                {
                    la.Add(new LastActivity(ArticleLanguage.articles, ActivityText(a), a.CreateDate));
                }
                var categories = ar.category.GetCategoriesByUser(username);
                foreach (var c in categories)
                {
                    la.Add(new LastActivity(ArticleLanguage.articles, ActivityText(c), c.CreateDate));
                }
                var comments = ar.comment.GetComments(username, CommentType.Articles);
                foreach (var c in comments)
                {
                    la.Add(new LastActivity(ArticleLanguage.articles, ActivityText(c), c.CreateDate));
                }
            }
            if (path == "Blogs" || path == "All")
            {
                var blogs = ar.category.GetBlogsByUser(username);
                foreach (var p in blogs)
                {
                    la.Add(new LastActivity(ArticleLanguage.blogs, ActivityText(p), p.CreateDate));
                }
                var posts = ar.article.GetArticles(username, true);
                foreach (var p in posts)
                {
                    la.Add(new LastActivity(ArticleLanguage.blogs, ActivityText(p), p.CreateDate));
                }
                var topics = ar.category.GetTopicsByUser(username);
                foreach (var t in topics)
                {
                    la.Add(new LastActivity(ArticleLanguage.blogs, ActivityText(t), t.CreateDate));
                }
                var bcomments = ar.comment.GetComments(username, CommentType.Blogs);
                foreach (var b in bcomments)
                {
                    la.Add(new LastActivity(ArticleLanguage.blogs, ActivityText(b), b.CreateDate));
                }
            }
            if (path == "Unapproved")
            {
                var bcomments = ar.comment.GetComments(username, CommentType.Unapproved);
                foreach (var b in bcomments)
                {
                    la.Add(new LastActivity(ArticleLanguage.unapproved_comments, ActivityText(b), b.CreateDate));
                }
            }
            if (path == "Editors")
            {
                var articles = ar.article.GetArticles(username, false);
                foreach (var a in articles)
                {
                    la.Add(new LastActivity("Article_" + a.ArticleId, ActivityText(a), a.CreateDate));
                }
                var categories = ar.category.GetCategoriesByUser(username);
                foreach (var c in categories)
                {
                    la.Add(new LastActivity("Category_" + c.CategoryId + "_" + c.SubCategoryId, ActivityText(c), c.CreateDate));
                }
            }
            return la;
        }
        /// <summary>
        /// ActivityText
        /// </summary>
        /// <param name="obj">object</param>
        /// <returns>string</returns>
        static string ActivityText(object obj)
        {
            string result = "";
            string flag = "";
            if (obj is mytrip_articlescategory)
            {
                mytrip_articlescategory category = obj as mytrip_articlescategory;
                flag = GeneralMethods.Flag(category.Culture);
                TagBuilder link = new TagBuilder("a");
                link.MergeAttribute("href", LangLink(category.Culture, "/Article/Index/1/10/" + category.CategoryId + "/" + category.Path));
                link.InnerHtml = category.Title;
                TagBuilder clink = new TagBuilder("a");
                if (category.SubCategoryId != 0)
                {
                    clink.MergeAttribute("href", LangLink(category.mytrip_articlescategory2.Culture, "/Article/Index/1/10/" + category.mytrip_articlescategory2.CategoryId + "/" + category.mytrip_articlescategory2.Path));
                    clink.InnerHtml = category.mytrip_articlescategory2.Title;
                    if (category.Blog)
                        result = ArticleLanguage.created_a_topic + " " + link + " " + ArticleLanguage.in_the + " " + clink;
                    else
                        result = ArticleLanguage.created_a_subcategory + " " + link + " " + ArticleLanguage.in_the + " " + clink;
                }
                else
                {
                    if (category.Blog)
                        result = ArticleLanguage.activated_a_blog + " " + link;
                    else
                        result = ArticleLanguage.created_a_category + " " + link;
                }
            }
            else if (obj is mytrip_articles)
            {
                mytrip_articles article = obj as mytrip_articles;
                flag = GeneralMethods.Flag(article.Culture);
                TagBuilder alink = new TagBuilder("a");
                alink.MergeAttribute("href", LangLink(article.Culture, "/Article/View/" + article.ArticleId + "/" + article.Path));
                // tooltip
                alink.MergeAttribute("rel", "Article," + article.ArticleId);
                alink.AddCssClass("mtPopupArticles");
                //
                alink.InnerHtml = article.Title;
                TagBuilder clink = new TagBuilder("a");
                clink.MergeAttribute("href", LangLink(article.mytrip_articlescategory.Culture, "/Article/Index/1/10/" + article.mytrip_articlescategory.CategoryId + "/" + article.mytrip_articlescategory.Path));
                clink.InnerHtml = article.mytrip_articlescategory.Title;
                if (!article.mytrip_articlescategory.Blog)
                    result = ArticleLanguage.created_an_article + " " + alink + " " + ArticleLanguage.in_the + " " + clink;
                else
                    result = ArticleLanguage.created_a_post + " " + alink + " " + ArticleLanguage.in_the + " " + clink;
            }
            else if (obj is mytrip_articlescomments)
            {
                mytrip_articlescomments comment = obj as mytrip_articlescomments;
                flag = GeneralMethods.Flag(comment.mytrip_articles.Culture);
                TagBuilder alink = new TagBuilder("a");
                alink.MergeAttribute("href", LangLink(comment.mytrip_articles.Culture, "/Article/View/" + comment.mytrip_articles.ArticleId + "/" + comment.mytrip_articles.Path + "#" + comment.CommentId));
                // tooltip
                alink.MergeAttribute("rel", "Comment," + comment.ArticleId);
                alink.AddCssClass("mtPopupArticles");
                //
                alink.InnerHtml = comment.mytrip_articles.Title;
                if (comment.IsApproved)
                    result = ArticleLanguage.added_a_comment + " " + ArticleLanguage.in_the + " " + alink;
                else
                    result = ArticleLanguage.comment + " " + ArticleLanguage.in_the + " " + alink;
            }
            return flag + " " + result;
        }
        #endregion

        #region Subscriptions

        public static HtmlString ArticlesProfile(this HtmlHelper html, string username, string path)
        {
            if (path == "Subscriptions")
            {
                return Subscriptions(html, username);
            }
            else if (path == "AwaitingModeration")
            {
                return AwaitingModeration(username);
            }
            return new MvcHtmlString("");
        }
        static HtmlString AwaitingModeration(string username)
        {
            TagBuilder table = new TagBuilder("table");
            table.MergeAttribute("class", "noborders");
            TagBuilder tr0 = new TagBuilder("tr");
            tr0.AddCssClass("profile2");
            TagBuilder td0 = new TagBuilder("td");
            td0.MergeAttribute("style", "width:35px;text-align:center;");
            TagBuilder td1 = new TagBuilder("td");
            td1.InnerHtml = ArticleLanguage.comment;
            TagBuilder td2 = new TagBuilder("td");
            td2.MergeAttribute("style", "width:75px;text-align:center;");
            td2.InnerHtml = ArticleLanguage.date;
            tr0.InnerHtml = td0.ToString() + td1.ToString() + td2.ToString();
            table.InnerHtml += tr0;
            int ctr = 0;
            IArticleRepository ar = new IArticleRepository();
            foreach (var c in ar.comment.GetComments(username))
            {
                TagBuilder tr1 = new TagBuilder("tr");
                if (ctr % 2 == 0)
                    tr1.AddCssClass("profile1");
                else
                    tr1.AddCssClass("profile2");
                TagBuilder td10 = new TagBuilder("td");
                td10.InnerHtml = GeneralMethods.ImageLink("approveComment_" + c.CommentId, "/Article/ApproveComment/" + c.CommentId + "/" + c.ArticleId + "/"
                   + HttpContext.Current.Request.Path.Replace("/", "(x)"), ArticleLanguage.approve_comment, "", "/images/approved.png", "Approve", 14)+" "+
                   GeneralMethods.ImageLink("deleteComment_" + c.CommentId, "/Article/DeleteComment/" + c.CommentId + "/" + c.ArticleId + "/"
                    + HttpContext.Current.Request.Path.Replace("/", "(x)"), ArticleLanguage.delete, "", "/images/delete.png", "Delete", 14); ;
                TagBuilder td11 = new TagBuilder("td");
                TagBuilder d1 = new TagBuilder("div");
                d1.InnerHtml = "<b>" + ArticleLanguage.author + "</b>: <a href='/Home/Profile/" + c.UserName
                   + "' title='" + ArticleLanguage.view_user_profile + "'>" + c.UserName + "</a>  | "
                   + c.mytrip_articles.CreateDate.ToString("dd MMMM yyyy HH:mm");
                TagBuilder d2 = new TagBuilder("div");
                TagBuilder article = new TagBuilder("a");
                article.InnerHtml = c.mytrip_articles.Title;
                article.MergeAttribute("href", "/Article/View/" + c.ArticleId + "/" + c.mytrip_articles.Path);
                TagBuilder cat = new TagBuilder("a");
                cat.InnerHtml = c.mytrip_articles.mytrip_articlescategory.Title;
                cat.MergeAttribute("href", "/Article/Index/1/10/" + c.mytrip_articles.CategoryId + "/" + c.mytrip_articles.mytrip_articlescategory.Path);
                if (c.mytrip_articles.mytrip_articlescategory.Blog)
                {
                    
                    d2.InnerHtml = "<b>" + ArticleLanguage.post + "</b>: " + article ;
                    if (c.mytrip_articles.mytrip_articlescategory.SubCategoryId == 0)
                        d2.InnerHtml += " | <b>" + ArticleLanguage.blog + "</b>: " + cat;
                    else
                    {
                        TagBuilder cat1 = new TagBuilder("a");
                        cat1.InnerHtml = c.mytrip_articles.mytrip_articlescategory.mytrip_articlescategory2.Title;
                        cat1.MergeAttribute("href", "/Article/Index/1/10/" + c.mytrip_articles.mytrip_articlescategory.SubCategoryId + "/" + c.mytrip_articles.mytrip_articlescategory.mytrip_articlescategory2.Path);
                        d2.InnerHtml += " | <b>" + ArticleLanguage.topic + "</b>: " + cat + " | <b>" + ArticleLanguage.blog + "</b>: " + cat1;
                    }
                }
                else
                {
                    d2.InnerHtml = "<b>" + ArticleLanguage.article + "</b>: " + article;
                    if (c.mytrip_articles.mytrip_articlescategory.SubCategoryId == 0)
                        d2.InnerHtml += " | <b>" + ArticleLanguage.category + "</b>: " + cat;
                    else
                    {
                        TagBuilder cat1 = new TagBuilder("a");
                        cat1.InnerHtml = c.mytrip_articles.mytrip_articlescategory.mytrip_articlescategory2.Title;
                        cat1.MergeAttribute("href", "/Article/Index/1/10/" + c.mytrip_articles.mytrip_articlescategory.SubCategoryId + "/" + c.mytrip_articles.mytrip_articlescategory.mytrip_articlescategory2.Path);
                        d2.InnerHtml += " | <b>" + ArticleLanguage.subcategory + "</b>: " + cat + " | <b>" + ArticleLanguage.category + "</b>: " + cat1;
                    }
                }
                td11.InnerHtml = c.Body +d1.ToString()+ d2.ToString();
                TagBuilder td12 = new TagBuilder("td");
                td12.InnerHtml = c.CreateDate.ToString("dd MMMM yyyy HH:mm");
                tr1.InnerHtml = td10.ToString() + td11.ToString() + td12.ToString();
                table.InnerHtml += tr1;
                ctr++;
            }
            return new MvcHtmlString(table.ToString());
        }
        public static HtmlString Subscriptions(this HtmlHelper html, string username)
        {
            IArticleRepository ar = new IArticleRepository();
            TagBuilder table = new TagBuilder("table");
            table.MergeAttribute("class", "noborders");
            TagBuilder tr0 = new TagBuilder("tr");
            tr0.AddCssClass("profile2");
            //tr0.MergeAttribute("style", "border-bottom:1px solid #000000;");
            TagBuilder th1 = new TagBuilder("td");
            th1.MergeAttribute("style", "width:25px;text-align:center;");
            TagBuilder th2 = new TagBuilder("td");
            TagBuilder th3 = new TagBuilder("td");
            th3.MergeAttribute("style", "width:50px;text-align:center;");
            th3.InnerHtml = ArticleLanguage.comments;
            TagBuilder th4 = new TagBuilder("td");
            th4.MergeAttribute("style", "width:50px;text-align:center;");
            th4.InnerHtml = ArticleLanguage.views;
            tr0.InnerHtml = th1.ToString() + th2.ToString() + th3.ToString() + th4.ToString();
            table.InnerHtml += tr0;
            int ctr = 0;
            foreach (var subs in ar.subscription.GetSubsciptions(username))
            {
                int commentsCount = subs.mytrip_articles.mytrip_articlescomments.Count;
                TagBuilder tr = new TagBuilder("tr");
                if (ctr % 2 == 0)
                    tr.AddCssClass("profile1");
                else
                    tr.AddCssClass("profile2");
                TagBuilder td1 = new TagBuilder("td");
                td1.MergeAttribute("style", "width:25px;text-align:center;");
                TagBuilder del = new TagBuilder("a");
                del.MergeAttribute("id", "delete");
                del.MergeAttribute("href", "/Article/DeleteSubscription/" + subs.ArticleId + "/");
                del.InnerHtml = "<img alt='delete' src='/Theme/" + ThemeSetting.theme() + "/images/noalert.png' title='"
                    + ArticleLanguage.unsubscribe_comments + "' style='height:20px;'/>";
                td1.InnerHtml = del.ToString();
                TagBuilder td2 = new TagBuilder("td");
                TagBuilder a = new TagBuilder("a");
                a.InnerHtml = subs.mytrip_articles.Title;
                a.MergeAttribute("href", "/Article/View/" + subs.ArticleId + "/" + subs.mytrip_articles.Path);
                TagBuilder d1 = new TagBuilder("div");
                d1.InnerHtml = subs.mytrip_articles.Abstract;
                TagBuilder d2 = new TagBuilder("div");
                d2.InnerHtml = "<b>" + ArticleLanguage.author + "</b>: <a href='/Home/Profile/" + subs.mytrip_articles.UserName
                    + "' title='" + ArticleLanguage.view_user_profile + "'>" + subs.mytrip_articles.UserName + "</a>  | "
                    + subs.mytrip_articles.CreateDate.ToString("dd MMMM yyyy HH:mm");
                TagBuilder d3 = new TagBuilder("div");
                if (commentsCount > 0)
                {
                    var c = subs.mytrip_articles.mytrip_articlescomments.OrderByDescending(x => x.CreateDate).First();
                    d3.InnerHtml = "<b>" + ArticleLanguage.last_comment + "</b>: <a href='/Home/Profile/" + c.UserName
                    + "' title='" + ArticleLanguage.view_user_profile + "'>" + c.UserName + "</a> |" + c.CreateDate.ToString("dd MMMM yyyy HH:mm");
                }
                else
                    d3.InnerHtml = "<b>" + ArticleLanguage.last_comment + "</b>: " + ArticleLanguage.no_comments_yet;
                TagBuilder d4 = new TagBuilder("div");
                if (subs.mytrip_articles.mytrip_articlescategory.SubCategoryId == 0)
                    d4.InnerHtml = "<b>" + ArticleLanguage.category + "</b>: <a href='/Article/Index/1/10/" + subs.mytrip_articles.CategoryId
                    + "' title='" + ArticleLanguage.category + "'>" + subs.mytrip_articles.mytrip_articlescategory.Title + "</a>";
                else
                    d4.InnerHtml = "<b>" + ArticleLanguage.subcategory + "</b>: <a href='/Article/Index/1/10/" + subs.mytrip_articles.CategoryId + "/"
                        + subs.mytrip_articles.mytrip_articlescategory.Path + "' title='" + ArticleLanguage.subcategory + "'>"
                        + subs.mytrip_articles.mytrip_articlescategory.Title + "</a>" + " | <b>" + ArticleLanguage.category + "</b>: <a href='/Article/Index/1/10/"
                        + subs.mytrip_articles.mytrip_articlescategory.SubCategoryId + "/" + subs.mytrip_articles.mytrip_articlescategory.mytrip_articlescategory2.Path
                    + "' title='" + ArticleLanguage.category + "'>" + subs.mytrip_articles.mytrip_articlescategory.mytrip_articlescategory2.Title + "</a>"
                        ;
                td2.InnerHtml = a.ToString() + d1.ToString() + d2.ToString() + d3.ToString() + d4.ToString();
                TagBuilder td3 = new TagBuilder("td");
                td3.MergeAttribute("style", "width:50px;text-align:center;");
                td3.InnerHtml = commentsCount.ToString();
                TagBuilder td4 = new TagBuilder("td");
                td4.MergeAttribute("style", "width:50px;text-align:center;");
                td4.InnerHtml = subs.mytrip_articles.Views.ToString();
                tr.InnerHtml = td1.ToString() + td2.ToString() + td3.ToString() + td4.ToString();
                table.InnerHtml += tr;
                ctr++;

            }
            return new MvcHtmlString(table.ToString());
        }
        #endregion

        #region Editors
        public static HtmlString EditorActivity(this HtmlHelper html, string username)
        {
            #region table rows
            string rows = string.Empty;
            var activities = GetLastActivities(username, "Editors");
            int ctr = 0;
            foreach (var item in activities)
            {
                TagBuilder tr1 = new TagBuilder("tr");
                if (ctr % 2 == 0)
                    tr1.AddCssClass("profile1");
                else
                    tr1.AddCssClass("profile2");
                TagBuilder td11 = new TagBuilder("td");
                td11.MergeAttribute("style", "text-align:center;width: 50px;");
                string imgEdit = GeneralMethods.ImageLink("modalJournalist_" + ctr, "#", ArticleLanguage.edit, item.Place, "/images/edite.png", "Edit", 15);
                td11.InnerHtml = imgEdit;
                tr1.InnerHtml = td11.ToString();
                TagBuilder td12 = new TagBuilder("td");
                td12.InnerHtml = item.Activity;
                tr1.InnerHtml += td12.ToString();
                TagBuilder td13 = new TagBuilder("td");
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
                rows += tr1.ToString();
                ctr++;
            }
            #endregion
            return new MvcHtmlString(rows.ToString());
        }
        #endregion

        #region Other helpers
        static string LangLink(string culture, string url)
        {
            culture = culture.ToLower();
            if (culture == LocalisationSetting.culture().ToLower())
                return url;
            return "/mtm/Language/" + culture + "/" + url.Replace("/", "(x)");
        }
        #endregion
    }
}
