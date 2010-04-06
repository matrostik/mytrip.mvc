using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mytrip.Core.Repository.MsSqlUsers;
using Mytrip.Core;
using System.Web.Mvc;
using Mytrip.Articles.Repository;

namespace Mytrip.Articles.Helpers
{
    public static class ProfileHelpers
    {
        public static string UserData(this HtmlHelper html, string username)
        {
            MsSqlMembershipRepository mmr = new MsSqlMembershipRepository();
            IArticleRepository ar = new IArticleRepository();
            mytrip_Users user = mmr.mssqlGetUserByUserName(username);
            #region table
            TagBuilder table = new TagBuilder("table");
            table.MergeAttribute("style", "border:0px;");
            TagBuilder tr1 = new TagBuilder("tr");
            tr1.MergeAttribute("style", "border:0px;border-bottom:1px solid grey");
            TagBuilder td11 = new TagBuilder("td");
            td11.MergeAttribute("style", "border:0px;");
            td11.InnerHtml = "<b>" + ArticleLanguage.member_since + ":</b> ";
            tr1.InnerHtml = td11.ToString();
            TagBuilder td12 = new TagBuilder("td");
            td12.MergeAttribute("style", "width:100px;border:0px;");
            td12.InnerHtml = user.mytrip_Membership.CreationDate.ToString("dd MMM, yyyy");
            tr1.InnerHtml += td12.ToString();
            TagBuilder tr2 = new TagBuilder("tr");
            tr2.MergeAttribute("style", "border:0px;border-bottom:1px solid grey");
            TagBuilder td21 = new TagBuilder("td");
            td21.MergeAttribute("style", "border:0px;");
            td21.InnerHtml = "<b>" + ArticleLanguage.last_visit + ":</b> ";
            tr2.InnerHtml = td21.ToString();
            TagBuilder td22 = new TagBuilder("td");
            td22.MergeAttribute("style", "border:0px;");
            td22.InnerHtml = user.LastActivityDate.ToString("dd MMM, yyyy HH:mm");
            tr2.InnerHtml += td22.ToString();
            table.InnerHtml = tr1.ToString() + tr2.ToString();
            #endregion
            TagBuilder div = new TagBuilder("div");
            div.MergeAttribute("style", "padding:0px 5px 5px 4px;");
            div.InnerHtml = table.ToString();
            return div.ToString();
        }

        public static string LastActivity(this HtmlHelper html, string username)
        {
            IArticleRepository ar = new IArticleRepository();
            #region table
            TagBuilder table = new TagBuilder("table");
            table.MergeAttribute("style", "border:0px;");
            var activities = GetLastActivities(username);
            foreach (var item in activities)
            {

                TagBuilder tr1 = new TagBuilder("tr");
                tr1.MergeAttribute("style", "border:0px;border-bottom:1px solid #CCCCCC");
                TagBuilder td11 = new TagBuilder("td");
                td11.MergeAttribute("style", "border:0px;width:100px");
                td11.InnerHtml = item.Place;
                tr1.InnerHtml = td11.ToString();
                TagBuilder td12 = new TagBuilder("td");
                td12.MergeAttribute("style", "border:0px;");
                td12.InnerHtml = item.Activity;
                tr1.InnerHtml += td12.ToString();
                TagBuilder td13 = new TagBuilder("td");
                td13.MergeAttribute("style", "border:0px;width:125px");
                if (item.Date.Date == DateTime.Today.Date && item.Date.Month == DateTime.Now.Month && item.Date.Year == DateTime.Now.Year)
                {
                    if (DateTime.Now.Hour - item.Date.Hour == 0)
                        td13.InnerHtml = (DateTime.Now.Minute - item.Date.Minute).ToString() + " minutes ago";
                    else
                        td13.InnerHtml = (DateTime.Now.Hour - item.Date.Hour).ToString() + " hours ago";
                }
                else
                    td13.InnerHtml = item.Date.ToString("dd MMM yyyy, HH:mm");
                tr1.InnerHtml += td13.ToString();
                table.InnerHtml += tr1.ToString();
            }
            #endregion
            TagBuilder div = new TagBuilder("div");
            div.MergeAttribute("style", "padding:0px 5px 5px 5px;");
            div.InnerHtml = table.ToString();
            return div.ToString();

        }
        private static IQueryable<LastActivity> GetLastActivities(string username)
        {
            List<LastActivity> la = new List<LastActivity>();
            IArticleRepository ar = new IArticleRepository();
            var articles = ar.article.GetAllArticlesByUsername(username);
            foreach (var a in articles)
            {
                LastActivity last = new LastActivity()
                {
                    Place = ArticleLanguage.articles,
                    Activity = ActivityText(a),
                    Date = a.CreateDate
                };
                la.Add(last);
            }
            var categories = ar.category.GetCategoriesByUser(username);
            foreach (var c in categories)
            {
                LastActivity last = new LastActivity()
                {
                    Place = ArticleLanguage.articles,
                    Activity = ActivityText(c),
                    Date = c.CreateDate
                };
                la.Add(last);
            }
            var blogs = ar.category.GetBlogsByUser(username);
            foreach (var p in blogs)
            {
                LastActivity last = new LastActivity()
                {
                    Place = ArticleLanguage.blogs,
                    Activity = ActivityText(p),
                    Date = p.CreateDate
                };
                la.Add(last);
            }
            var posts = ar.article.GetAllPostsByUsername(username);
            foreach (var p in posts)
            {
                LastActivity last = new LastActivity()
                {
                    Place = ArticleLanguage.blogs,
                    Activity = ActivityText(p),
                    Date = p.CreateDate
                };
                la.Add(last);
            }
            var topics = ar.category.GetTopicsByUser(username);
            foreach (var t in topics)
            {
                LastActivity last = new LastActivity()
                {
                    Place = ArticleLanguage.blogs,
                    Activity = ActivityText(t),
                    Date = t.CreateDate
                };
                la.Add(last);
            }
            var comments = ar.comment.GetCommentsArticlesByUser(username);
            foreach (var c in comments)
            {
                LastActivity last = new LastActivity()
                {
                    Place = ArticleLanguage.articles,
                    Activity = ActivityText(c),
                    Date = c.CreateDate
                };
                la.Add(last);
            }
            var bcomments = ar.comment.GetCommentsBlogsByUser(username);
            foreach (var b in bcomments)
            {
                LastActivity last = new LastActivity()
                {
                    Place = ArticleLanguage.blogs,
                    Activity = ActivityText(b),
                    Date = b.CreateDate
                };
                la.Add(last);
            }

            return la.AsQueryable().OrderByDescending(x => x.Date);
        }

        static string ActivityText(object obj)
        {
            string result = "";
            if (obj is mytrip_ArticlesCategory)
            {
                mytrip_ArticlesCategory category = obj as mytrip_ArticlesCategory;
                TagBuilder link = new TagBuilder("a");
                link.MergeAttribute("href", "/Article/Index/1/10/" + category.CategoryId + "/" + category.Path);
                link.InnerHtml = category.Title;
                TagBuilder clink = new TagBuilder("a");
                clink.MergeAttribute("href", "/Article/Index/1/10/" + category.mytrip_ArticlesCategory2.CategoryId + "/" + category.mytrip_ArticlesCategory2.Path);
                clink.InnerHtml = category.mytrip_ArticlesCategory2.Title;
                if (!category.mytrip_ArticlesCategory2.Blog)
                {
                    if (category.CategoryId == category.SubCategoryId)
                        result = ArticleLanguage.created_a_category + " " + link + " " + ArticleLanguage.in_the + " " + clink;
                    else
                        result = ArticleLanguage.created_a_subcategory + " " + link + " " + ArticleLanguage.in_the + " " + clink;
                }
                else
                {
                    if (category.CategoryId == category.SubCategoryId)
                        result = ArticleLanguage.activated_a_blog + " " + link ;
                    else
                    result = "Created a topic " + link + " " + ArticleLanguage.in_the + " " + clink;
                }
            }
            else if (obj is mytrip_Articles)
            {
                mytrip_Articles article = obj as mytrip_Articles;
                TagBuilder alink = new TagBuilder("a");
                alink.MergeAttribute("href", "/Article/View/" + article.ArticleId + "/" + article.Path);
                alink.InnerHtml = article.Title;
                TagBuilder clink = new TagBuilder("a");
                clink.MergeAttribute("href", "/Article/Index/1/10/" + article.mytrip_ArticlesCategory.CategoryId + "/" + article.mytrip_ArticlesCategory.Path);
                clink.InnerHtml = article.mytrip_ArticlesCategory.Title;
                if (!article.mytrip_ArticlesCategory.mytrip_ArticlesCategory2.Blog)
                    result = ArticleLanguage.created_an_article + " " + alink + " " + ArticleLanguage.in_the + " " + clink;
                else
                    result = ArticleLanguage.created_a_post + " " + alink + " " + ArticleLanguage.in_the + " " + clink;
            }
            else if (obj is mytrip_ArticlesComments)
            {
                mytrip_ArticlesComments comment = obj as mytrip_ArticlesComments;
                TagBuilder alink = new TagBuilder("a");
                alink.MergeAttribute("href", "/Article/View/" + comment.mytrip_Articles.ArticleId + "/" + comment.mytrip_Articles.Path);
                alink.InnerHtml = comment.mytrip_Articles.Title;
                result = ArticleLanguage.added_a_comment + " " + ArticleLanguage.in_the + " " + alink;
            }
            return result;
        }
        static string SubCatLink(mytrip_ArticlesCategory category)
        {
            if (category.CategoryId == category.SubCategoryId)
                return string.Empty;
            TagBuilder link = new TagBuilder("a");
            link.MergeAttribute("href", "/Article/Index/1/10/" + category.SubCategoryId + "/" + category.mytrip_ArticlesCategory2.Path);
            link.InnerHtml = category.mytrip_ArticlesCategory2.Title;
            return " " + ArticleLanguage.in_the + " " + link.ToString() + " ";
        }
    }
    class LastActivity
    {
        private string place;
        private string activity;
        private DateTime date;

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
}
