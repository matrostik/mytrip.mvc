using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mytrip.Mvc;
using System.Web.Mvc;
using Mytrip.Articles.Repository;
using Mytrip.Mvc.Repository;
using Mytrip.Mvc.Repository.DataEntities;
using Mytrip.Articles.Repository.DataEntities;
using System.Web;
using Mytrip.Mvc.Settings;
using Mytrip.Mvc.Helpers;

namespace Mytrip.Articles.Helpers
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
                if(comment.IsApproved)
                result = ArticleLanguage.added_a_comment + " " + ArticleLanguage.in_the + " " + alink;
                else
                    result = ArticleLanguage.comment + " " + ArticleLanguage.in_the + " " + alink;
            }
            return flag + " " + result;
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
                    tr1.MergeAttribute("style", "border-bottom:1px solid #CCCCCC;background-color:#F7F5F5");
                else
                    tr1.MergeAttribute("style", "border-bottom:1px solid #CCCCCC;background-color:#EDEBEB");
                TagBuilder td11 = new TagBuilder("td");
                td11.MergeAttribute("style", "text-align:center");
                string imgEdit = GeneralMethods.ImgInput("/images/edite.png", item.Place, "rename", 15);
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
            return "/MytripMvc/Language/" + culture + "/" + url.Replace("/", "(x)");
        }
        #endregion
    }
}
