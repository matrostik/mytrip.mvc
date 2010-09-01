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

namespace Mytrip.Articles.Helpers
{
    public static class ProfileHelpers
    {
        /// <summary>
        /// Get collection of last activities
        /// </summary>
        /// <param name="username">username</param>
        /// <param name="path">path</param>
        /// <returns></returns>
        public static IQueryable<LastActivity> GetLastActivities(string username, string path)
        {
            List<LastActivity> la = new List<LastActivity>();
            IArticleRepository ar = new IArticleRepository();
            if (path == "Articles" || path == "All")
            {
                var articles = ar.article.GetArticles(username,false);
                foreach (var a in articles)
                {
                    la.Add(new LastActivity(ArticleLanguage.articles, ActivityText(a), a.CreateDate));
                }
                var categories = ar.category.GetCategoriesByUser(username);
                foreach (var c in categories)
                {
                    la.Add(new LastActivity(ArticleLanguage.articles, ActivityText(c), c.CreateDate));
                }
                var comments = ar.comment.GetComments(username,false);
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
                var bcomments = ar.comment.GetComments(username,true);
                foreach (var b in bcomments)
                {
                    la.Add(new LastActivity(ArticleLanguage.blogs, ActivityText(b), b.CreateDate));
                }
            }
            return la.AsQueryable().OrderByDescending(x => x.Date);
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
                flag = Flag(category.Culture, 14);
                TagBuilder link = new TagBuilder("a");
                link.MergeAttribute("href",LangLink(category.Culture, "/Article/Index/1/10/" + category.CategoryId + "/" + category.Path));
                link.InnerHtml = category.Title;
                TagBuilder clink = new TagBuilder("a");
                if (category.SubCategoryId != 0)
                {
                    clink.MergeAttribute("href", LangLink(category.mytrip_articlescategory2.Culture, "/Article/Index/1/10/" + category.mytrip_articlescategory2.CategoryId + "/" + category.mytrip_articlescategory2.Path));
                    clink.InnerHtml = category.mytrip_articlescategory2.Title;
                    if(category.Blog)
                        result = ArticleLanguage.created_a_topic + " " + link + " " + ArticleLanguage.in_the + " " + clink;
                    else
                        result = ArticleLanguage.created_a_subcategory + " " + link + " " + ArticleLanguage.in_the + " " + clink;
                }
                else
                {
                    if(category.Blog)
                        result = ArticleLanguage.activated_a_blog + " " + link;
                    else
                        result = ArticleLanguage.created_a_category + " " + link;
                }
            }
            else if (obj is mytrip_articles)
            {
                mytrip_articles article = obj as mytrip_articles;
                flag = Flag(article.Culture, 14);
                TagBuilder alink = new TagBuilder("a");
                alink.MergeAttribute("href", LangLink(article.Culture, "/Article/View/" + article.ArticleId + "/" + article.Path));
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
                flag = Flag(comment.mytrip_articles.Culture, 14);
                TagBuilder alink = new TagBuilder("a");
                alink.MergeAttribute("href", LangLink(comment.mytrip_articles.Culture, "/Article/View/" + comment.mytrip_articles.ArticleId + "/" + comment.mytrip_articles.Path+"#"+comment.CommentId));
                alink.InnerHtml = comment.mytrip_articles.Title;
                result = ArticleLanguage.added_a_comment + " " + ArticleLanguage.in_the + " " + alink;
            }
            return flag+" "+result;
        }

        #region Other helpers    
        static string LangLink(string culture, string url)
        {
            culture = culture.ToLower();
            if (culture == LocalisationSetting.culture().ToLower())
                return url;
            return "/MytripMvc/Language/" + culture + "/" + url.Replace("/", "(x)");
        }
        static string Flag(string culture, int width)
        {
            culture = culture.ToLower();
            TagBuilder img = new TagBuilder("img");
            img.MergeAttribute("src", "/Theme/" + ThemeSetting.theme() + "/images/" + culture + ".png");
            img.MergeAttribute("style", "border-width:0px;width:" + width + "px;");
            img.MergeAttribute("alt", culture);
            img.MergeAttribute("title", culture);
            return img.ToString();
        }
        #endregion
    }
    #region LastActivity Object
    public class LastActivity
    {
        private string place;
        private string activity;
        private DateTime date;
        public LastActivity()
        { }
        public LastActivity(string place, string activity, DateTime date)
        {
            Place = place;
            Activity = activity;
            Date = date;
        }
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
        public string Activity
        {
            get { return activity; }
            set { activity = value; }
        }
        public string Place
        {
            get { return place; }
            set { place = value; }
        }
    }
     #endregion
}
