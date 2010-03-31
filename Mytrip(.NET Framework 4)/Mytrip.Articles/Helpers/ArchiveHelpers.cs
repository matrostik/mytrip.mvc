using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Mytrip.Core;
using Mytrip.Articles.Models;
using System.Web;

namespace Mytrip.Articles.Helpers
{
    public static class ArchiveHelpers
    {
        public static string ArchiveStatistic(this HtmlHelper html)
        {
            IArticleRepository ar = new IArticleRepository();
            StringBuilder result = new StringBuilder();
            #region table
            TagBuilder table = new TagBuilder("table");
            table.MergeAttribute("style", "text-align:center");
            //table.MergeAttribute("style", "border:0px;");
            #region Header row
            TagBuilder tr1 = new TagBuilder("tr");
            tr1.MergeAttribute("style", "border:0px;");
            TagBuilder th11 = new TagBuilder("th");
            th11.MergeAttribute("style", "text-align:center");
            tr1.InnerHtml = th11.ToString();
            foreach (string culture in LocalisationSetting.allCultureMassive())
            {
                TagBuilder th12 = new TagBuilder("th");
                th12.MergeAttribute("style", "text-align:center");
                th12.InnerHtml = Flag(culture, 18);
                tr1.InnerHtml += th12.ToString();
            }
            TagBuilder th13 = new TagBuilder("th");
            th13.MergeAttribute("style", "text-align:center");
            th13.InnerHtml=Globe(true,18);
            tr1.InnerHtml += th13.ToString();
            #endregion
            #region Categories or Subcategories row
            TagBuilder tr2 = new TagBuilder("tr");
            TagBuilder td21 = new TagBuilder("td");
            TagBuilder a2 = new TagBuilder("a");
            a2.MergeAttribute("href", "/ArticleArchive/Details/Categories/");
            a2.InnerHtml = "Categories or Subcategories";
            td21.InnerHtml = a2.ToString();
            td21.MergeAttribute("style", "width:200px");
            tr2.InnerHtml = td21.ToString();
            int total = 0;
            foreach (string culture in LocalisationSetting.allCultureMassive())
            {
                TagBuilder td22 = new TagBuilder("td");
                int count = ar.category.GetCategoriesCount(culture);
                td22.SetInnerText(count.ToString());
                tr2.InnerHtml += td22.ToString();
                total += count;
            }
            TagBuilder td23 = new TagBuilder("td");
            td23.SetInnerText(total.ToString());
            tr2.InnerHtml += td23.ToString();
            #endregion
            #region Articles row
            TagBuilder tr3 = new TagBuilder("tr");
            TagBuilder td31 = new TagBuilder("td");
            TagBuilder a3 = new TagBuilder("a");
            a3.MergeAttribute("href", "/ArticleArchive/Details/Articles/");
            a3.InnerHtml = "Articles";
            td31.InnerHtml = a3.ToString();
            td31.MergeAttribute("style", "width:200px");
            tr3.InnerHtml = td31.ToString();
            total = 0;
            foreach (string culture in LocalisationSetting.allCultureMassive())
            {
                TagBuilder td32 = new TagBuilder("td");
                int count = ar.article.GetAllArticlesCount(culture);
                td32.SetInnerText(count.ToString());
                tr3.InnerHtml += td32.ToString();
                total += count;
            }
            TagBuilder td33 = new TagBuilder("td");
            td33.SetInnerText(total.ToString());
            tr3.InnerHtml += td33.ToString();
            #endregion
            #region Comments row
            TagBuilder tr4 = new TagBuilder("tr");
            TagBuilder td41 = new TagBuilder("td");
            TagBuilder a4 = new TagBuilder("a");
            a4.MergeAttribute("href", "/ArticleArchive/Details/Comments/");
            a4.InnerHtml = "Comments";
            td41.InnerHtml = a4.ToString();
            td41.MergeAttribute("style", "width:200px");
            tr4.InnerHtml = td41.ToString();
            total = 0;
            foreach (string culture in LocalisationSetting.allCultureMassive())
            {
                TagBuilder td42 = new TagBuilder("td");
                int count = ar.comment.GetCommentsCount(culture,false);
                td42.SetInnerText(count.ToString());
                tr4.InnerHtml += td42.ToString();
                total += count;
            }
            TagBuilder td43 = new TagBuilder("td");
            td43.SetInnerText(total.ToString());
            tr4.InnerHtml += td43.ToString();
            #endregion
            #region Blogs Header row
            TagBuilder tr5 = new TagBuilder("tr");
            tr5.MergeAttribute("style", "border:0px;");
            TagBuilder th51 = new TagBuilder("th");
            th51.MergeAttribute("style", "text-align:center");
            th51.MergeAttribute("colspan", LocalisationSetting.allCultureMassive().Count().ToString()+2);
            tr5.InnerHtml = th51.ToString();
            #endregion
            #region Blogs row
            TagBuilder tr6 = new TagBuilder("tr");
            TagBuilder td61 = new TagBuilder("td");
            TagBuilder a6 = new TagBuilder("a");
            a6.MergeAttribute("href", "/ArticleArchive/Details/Blogs/");
            a6.InnerHtml = "Blogs";
            td61.InnerHtml = a6.ToString();
            td61.MergeAttribute("style", "width:200px");
            tr6.InnerHtml = td61.ToString();
            total = 0;
            foreach (string culture in LocalisationSetting.allCultureMassive())
            {
                TagBuilder td62 = new TagBuilder("td");
                int count = ar.category.GetBlogsCount(culture);
                td62.SetInnerText(count.ToString());
                tr6.InnerHtml += td62.ToString();
                total += count;
            }
            TagBuilder td63 = new TagBuilder("td");
            td63.SetInnerText(total.ToString());
            tr6.InnerHtml += td63.ToString();
            #endregion          
            #region Topics row
            TagBuilder tr7 = new TagBuilder("tr");
            TagBuilder td71 = new TagBuilder("td");
            TagBuilder a7 = new TagBuilder("a");
            a7.MergeAttribute("href", "/ArticleArchive/Details/Topics/");
            a7.InnerHtml = "Topics";
            td71.InnerHtml = a7.ToString();
            td71.MergeAttribute("style", "width:200px");
            tr7.InnerHtml = td71.ToString();
            total = 0;
            foreach (string culture in LocalisationSetting.allCultureMassive())
            {
                TagBuilder td72 = new TagBuilder("td");
                int count = ar.category.GetTopicsCount(culture);
                td72.SetInnerText(count.ToString());
                tr7.InnerHtml += td72.ToString();
                total += count;
            }
            TagBuilder td73 = new TagBuilder("td");
            td73.SetInnerText(total.ToString());
            tr7.InnerHtml += td73.ToString();
            #endregion
            #region Posts row
            TagBuilder tr8 = new TagBuilder("tr");
            TagBuilder td81 = new TagBuilder("td");
            TagBuilder a8 = new TagBuilder("a");
            a8.MergeAttribute("href", "/ArticleArchive/Details/Posts/");
            a8.InnerHtml = "Posts";
            td81.InnerHtml = a8.ToString();
            td81.MergeAttribute("style", "width:200px");
            tr8.InnerHtml = td81.ToString();
            total = 0;
            foreach (string culture in LocalisationSetting.allCultureMassive())
            {
                TagBuilder td82 = new TagBuilder("td");
                int count = ar.article.GetAllPostsCount(culture);
                td82.SetInnerText(count.ToString());
                tr8.InnerHtml += td82.ToString();
                total += count;
            }
            TagBuilder td83 = new TagBuilder("td");
            td83.SetInnerText(total.ToString());
            tr8.InnerHtml += td83.ToString();
            #endregion
            #region Blogs Comments row
            TagBuilder tr9 = new TagBuilder("tr");
            TagBuilder td91 = new TagBuilder("td");
            TagBuilder a9 = new TagBuilder("a");
            a9.MergeAttribute("href", "/ArticleArchive/Details/BlogsComments/");
            a9.InnerHtml = "BlogsComments";
            td91.InnerHtml = a9.ToString();
            td91.MergeAttribute("style", "width:200px");
            tr9.InnerHtml = td91.ToString();
            total = 0;
            foreach (string culture in LocalisationSetting.allCultureMassive())
            {
                TagBuilder td92 = new TagBuilder("td");
                int count = ar.comment.GetCommentsCount(culture,true);
                td92.SetInnerText(count.ToString());
                tr9.InnerHtml += td92.ToString();
                total += count;
            }
            TagBuilder td93 = new TagBuilder("td");
            td93.SetInnerText(total.ToString());
            tr9.InnerHtml += td93.ToString();
            #endregion
            #region Other Header row
            TagBuilder tr10 = new TagBuilder("tr");
            tr10.MergeAttribute("style", "border:0px;");
            TagBuilder th101 = new TagBuilder("th");
            th101.MergeAttribute("style", "text-align:center");
            th101.MergeAttribute("colspan", LocalisationSetting.allCultureMassive().Count().ToString() + 2);
            tr10.InnerHtml = th101.ToString();
            #endregion
            #region Tags row
            TagBuilder tr11 = new TagBuilder("tr");
            TagBuilder td111 = new TagBuilder("td");
            TagBuilder a11 = new TagBuilder("a");
            a11.MergeAttribute("href", "/ArticleArchive/Details/Tags/");
            a11.InnerHtml = "Tags";
            td111.InnerHtml = a11.ToString();
            td111.MergeAttribute("style", "width:200px");
            tr11.InnerHtml = td111.ToString();
            TagBuilder td112 = new TagBuilder("td");
            td112.MergeAttribute("colspan", LocalisationSetting.allCultureMassive().Count().ToString());
            td112.MergeAttribute("style", "background-color:#F7F5F5;");
            tr11.InnerHtml += td112.ToString();
            TagBuilder td113 = new TagBuilder("td");
            td113.SetInnerText(ar.article.GetTagsCount().ToString());
            tr11.InnerHtml += td113.ToString();
            #endregion
            #region Closed Articles row
            TagBuilder tr12 = new TagBuilder("tr");
            TagBuilder td121 = new TagBuilder("td");
            TagBuilder a12 = new TagBuilder("a");
            a12.MergeAttribute("href", "/ArticleArchive/Details/ClosedArticles/");
            a12.InnerHtml = "Closed Articles";
            td121.InnerHtml = a12.ToString();
            td121.MergeAttribute("style", "width:200px");
            tr12.InnerHtml = td121.ToString();
            total = 0;
            foreach (string culture in LocalisationSetting.allCultureMassive())
            {
                TagBuilder td122 = new TagBuilder("td");
                int count = ar.article.GetClosedArticlesCount(culture);
                td122.SetInnerText(count.ToString());
                tr12.InnerHtml += td122.ToString();
                total += count;
            }
            TagBuilder td123 = new TagBuilder("td");
            td123.SetInnerText(total.ToString());
            tr12.InnerHtml += td123.ToString();
            #endregion
            
            //Insert rows into table
            table.InnerHtml = tr1.ToString() + tr2.ToString() + tr3.ToString() + tr4.ToString() + tr5.ToString() + tr6.ToString()
                + tr7.ToString() + tr8.ToString() + tr9.ToString() + tr10.ToString() + tr11.ToString() + tr12.ToString();
            #endregion
            result.AppendLine(table.ToString());
            return result.ToString();
        }

        public static string LatestUpdates(this HtmlHelper html, int count)
        {
            IArticleRepository ar = new IArticleRepository();
            StringBuilder result = new StringBuilder();
            #region table
            TagBuilder table = new TagBuilder("table");
            #region Articles Header row
            TagBuilder tr1 = new TagBuilder("tr");
            tr1.MergeAttribute("style", "border:0px;");
            TagBuilder th11 = new TagBuilder("th");
            th11.MergeAttribute("style", "text-align:center;width:200px;");
            th11.SetInnerText("Articles");
            TagBuilder th12 = new TagBuilder("th");
            th12.MergeAttribute("style", "text-align:center");
            tr1.InnerHtml +=th11.ToString()+ th12.ToString();
            #endregion
            #region Categories or Subcategories row
            TagBuilder tr2 = new TagBuilder("tr");
            TagBuilder td21 = new TagBuilder("td");
            TagBuilder a2 = new TagBuilder("a");
            a2.MergeAttribute("href", "/ArticleArchive/Details/Categories/");
            a2.InnerHtml = "Categories or Subcategories";
            td21.InnerHtml = a2.ToString();
            td21.MergeAttribute("style", "text-align:center");
            tr2.InnerHtml = td21.ToString();
            TagBuilder td22 = new TagBuilder("td");
            int ctr = 0;
            foreach (var cat in ar.category.GetLastCategories(count))
            {
                TagBuilder div = new TagBuilder("div");
                if (ctr % 2 == 0)
                    div.MergeAttribute("style", "background-color:#F7F5F5;");
                else
                    div.MergeAttribute("style", "background-color:#EDEBEB;");
                TagBuilder link = new TagBuilder("a");
                link.MergeAttribute("href", LangLink(cat.Culture,"/Article/Index/1/10/" + cat.CategoryId + "/" + cat.Path));
                link.InnerHtml = cat.Title;
                div.InnerHtml += "<b>" + link + " " + Globe(cat.AllCulture, 14) + " " + Flag(cat.Culture, 14)
                    + "</b><br/>"+cat.CreateDate.ToString("dd MMMM yyyy HH:mm") + SubCatLink(cat) + " added by " + cat.UserName;
                td22.InnerHtml += div;
                ctr++;
            }
            tr2.InnerHtml += td22.ToString();
            #endregion        
            #region Articles row
            TagBuilder tr3 = new TagBuilder("tr");
            TagBuilder td31 = new TagBuilder("td");
            TagBuilder a3 = new TagBuilder("a");
            a3.MergeAttribute("href", "/ArticleArchive/Details/Articles/");
            a3.InnerHtml = "Articles";
            td31.InnerHtml = a3.ToString();
            td31.MergeAttribute("style", "text-align:center");
            tr3.InnerHtml = td31.ToString();
            TagBuilder td32 = new TagBuilder("td");
            ctr = 0;
            foreach (var article in ar.article.GetLastArticles(count))
            {
                TagBuilder div = new TagBuilder("div");
                if (ctr % 2 == 0)
                    div.MergeAttribute("style", "background-color:#F7F5F5;");
                else
                    div.MergeAttribute("style", "background-color:#EDEBEB;");
                TagBuilder alink = new TagBuilder("a");
                alink.MergeAttribute("href", LangLink(article.Culture,"/Article/View/" + article.ArticleId + "/" + article.Path));
                alink.InnerHtml = article.Title;
                TagBuilder clink = new TagBuilder("a");
                clink.MergeAttribute("href", LangLink(article.Culture,"/Article/Index/1/10/" + article.mytrip_ArticlesCategory.CategoryId + "/" + article.mytrip_ArticlesCategory.Path));
                clink.InnerHtml = article.mytrip_ArticlesCategory.Title;
                div.InnerHtml = "<b>" + alink + " " + Keys(article.OnlyForRegisterUser, 14) + " " + Globe(article.AllCulture, 14) + " " + Flag(article.Culture, 14)
                    + "</b><br/>" + article.CreateDate.ToString("dd MMMM yyyy HH:mm") + " in " + clink + " by " + article.UserName;
                td32.InnerHtml += div;
                ctr++;
            }
            tr3.InnerHtml += td32.ToString();
            #endregion
            #region Comments row
            TagBuilder tr4 = new TagBuilder("tr");
            TagBuilder td41 = new TagBuilder("td");
            TagBuilder a4 = new TagBuilder("a");
            a4.MergeAttribute("href", "/ArticleArchive/Details/Comments/");
            a4.InnerHtml = "Comments";
            td41.InnerHtml = a4.ToString();
            td41.MergeAttribute("style", "text-align:center");
            tr4.InnerHtml = td41.ToString();
            TagBuilder td42 = new TagBuilder("td");
            ctr = 0;
            foreach (var comment in ar.comment.GetLastComments(count,false))
            {
                TagBuilder div = new TagBuilder("div");
                if (ctr % 2 == 0)
                    div.MergeAttribute("style", "background-color:#F7F5F5;");
                else
                    div.MergeAttribute("style", "background-color:#EDEBEB;");
                TagBuilder link = new TagBuilder("a");
                link.MergeAttribute("href", LangLink(comment.mytrip_Articles.Culture, "/Article/View/" + comment.mytrip_Articles.ArticleId + "/" + comment.mytrip_Articles.Path));
                link.InnerHtml = comment.mytrip_Articles.Title;
                div.InnerHtml += comment.CreateDate.ToString("dd MMMM yyyy HH:mm:ss") + " in <b>" + link + " " + Globe(comment.mytrip_Articles.AllCulture, 14) + " "
                    + Flag(comment.mytrip_Articles.Culture, 14) + "</b> " + "added by " + comment.UserName;
                td42.InnerHtml += div;
                ctr++;
            }
            tr4.InnerHtml += td42.ToString();
            #endregion
            #region Blogs Header row
            TagBuilder tr5 = new TagBuilder("tr");
            tr5.MergeAttribute("style", "border:0px;");
            TagBuilder th51 = new TagBuilder("th");
            th51.MergeAttribute("style", "text-align:center;width:200px;");
            th51.SetInnerText("Blogs");
            TagBuilder th52 = new TagBuilder("th");
            th52.MergeAttribute("style", "text-align:center");
            tr5.InnerHtml += th51.ToString() + th52.ToString();
            #endregion
            #region Blogs row
            TagBuilder tr6 = new TagBuilder("tr");
            TagBuilder td61 = new TagBuilder("td");
            TagBuilder a6 = new TagBuilder("a");
            a6.MergeAttribute("href", "/ArticleArchive/Details/Blogs/");
            a6.InnerHtml = "Blogs";
            td61.InnerHtml = a6.ToString();
            td61.MergeAttribute("style", "text-align:center");
            tr6.InnerHtml = td61.ToString();
            TagBuilder td62 = new TagBuilder("td");
            ctr = 0;
            foreach (var blog in ar.category.GetLastBlogs(count))
            {
                TagBuilder div = new TagBuilder("div");
                if (ctr % 2 == 0)
                    div.MergeAttribute("style", "background-color:#F7F5F5;");
                else
                    div.MergeAttribute("style", "background-color:#EDEBEB;");
                TagBuilder link = new TagBuilder("a");
                link.MergeAttribute("href", LangLink(blog.Culture, "/Article/Index/1/10/" + blog.CategoryId + "/" + blog.Path));
                link.InnerHtml = blog.Title;
                div.InnerHtml += "<b>" + link + " " + Flag(blog.Culture, 14) + "</b><br/>" + blog.CreateDate.ToString("dd MMMM yyyy HH:mm") + " by " + blog.UserName;
                td62.InnerHtml += div;
                ctr++;
            }
            tr6.InnerHtml += td62.ToString();
            #endregion
            #region Topics row
            TagBuilder tr7 = new TagBuilder("tr");
            TagBuilder td71 = new TagBuilder("td");
            TagBuilder a7 = new TagBuilder("a");
            a7.MergeAttribute("href", "/ArticleArchive/Details/Topics/");
            a7.InnerHtml = "Topics";
            td71.InnerHtml = a7.ToString();
            td71.MergeAttribute("style", "text-align:center");
            tr7.InnerHtml = td71.ToString();
            TagBuilder td72 = new TagBuilder("td");
            ctr = 0;
            foreach (var topic in ar.category.GetLastTopics(count))
            {
                TagBuilder div = new TagBuilder("div");
                if (ctr % 2 == 0)
                    div.MergeAttribute("style", "background-color:#F7F5F5;");
                else
                    div.MergeAttribute("style", "background-color:#EDEBEB;");
                TagBuilder link = new TagBuilder("a");
                link.MergeAttribute("href", LangLink(topic.Culture, "/Article/Index/1/10/" + topic.CategoryId + "/" + topic.Path));
                link.InnerHtml = topic.Title;
                div.InnerHtml += "<b>" + link + " " + Globe(topic.AllCulture, 14) + " " + Flag(topic.Culture, 14)
                    + "</b><br/>" + topic.CreateDate.ToString("dd MMMM yyyy HH:mm") + SubCatLink(topic) + " added by " + topic.UserName;
                td72.InnerHtml += div;
                ctr++;
            }
            tr7.InnerHtml += td72.ToString();
            #endregion        
            #region Posts row
            TagBuilder tr8 = new TagBuilder("tr");
            TagBuilder td81 = new TagBuilder("td");
            TagBuilder a8 = new TagBuilder("a");
            a8.MergeAttribute("href", "/ArticleArchive/Details/Posts/");
            a8.InnerHtml = "Posts";
            td81.InnerHtml = a8.ToString();
            td81.MergeAttribute("style", "text-align:center");
            tr8.InnerHtml = td81.ToString();
            TagBuilder td82 = new TagBuilder("td");
            ctr = 0;
            foreach (var post in ar.article.GetLastPosts(count))
            {
                TagBuilder div = new TagBuilder("div");
                if (ctr % 2 == 0)
                    div.MergeAttribute("style", "background-color:#F7F5F5;");
                else
                    div.MergeAttribute("style", "background-color:#EDEBEB;");
                TagBuilder link = new TagBuilder("a");
                link.MergeAttribute("href", LangLink(post.Culture,"/Article/View/" + post.CategoryId + "/" + post.Path));
                link.InnerHtml = post.Title;
                TagBuilder clink = new TagBuilder("a");
                clink.MergeAttribute("href", LangLink(post.Culture,"/Article/Index/1/10/" + post.mytrip_ArticlesCategory.CategoryId + "/" + post.mytrip_ArticlesCategory.Path));
                clink.InnerHtml = post.mytrip_ArticlesCategory.Title;
                div.InnerHtml += "<b>" + link + " " + Flag(post.Culture, 14) + "</b><br/>" + post.CreateDate.ToString("dd MMMM yyyy HH:mm") + " in " + clink + " by " + post.UserName;
                td82.InnerHtml += div;
                ctr++;
            }
            tr8.InnerHtml += td82.ToString();
            #endregion
            #region Comments row
            TagBuilder tr9 = new TagBuilder("tr");
            TagBuilder td91 = new TagBuilder("td");
            TagBuilder a9 = new TagBuilder("a");
            a9.MergeAttribute("href", "/ArticleArchive/Details/BlogsComments/");
            a9.InnerHtml = "Comments";
            td91.InnerHtml = a9.ToString();
            td91.MergeAttribute("style", "text-align:center");
            tr9.InnerHtml = td91.ToString();
            TagBuilder td92 = new TagBuilder("td");
            ctr = 0;
            foreach (var comment in ar.comment.GetLastComments(count,true))
            {
                TagBuilder div = new TagBuilder("div");
                if (ctr % 2 == 0)
                    div.MergeAttribute("style", "background-color:#F7F5F5;");
                else
                    div.MergeAttribute("style", "background-color:#EDEBEB;");
                TagBuilder link = new TagBuilder("a");
                link.MergeAttribute("href", LangLink(comment.mytrip_Articles.Culture, "/Article/View/" + comment.mytrip_Articles.ArticleId + "/" + comment.mytrip_Articles.Path));
                link.InnerHtml = comment.mytrip_Articles.Title;
                div.InnerHtml += comment.CreateDate.ToString("dd MMMM yyyy HH:mm:ss") + " in <b>" + link + " " + Globe(comment.mytrip_Articles.AllCulture, 14) + " "
                    + Flag(comment.mytrip_Articles.Culture, 14) + "</b> " + "added by " + comment.UserName;
                td92.InnerHtml += div;
                ctr++;
            }
            tr9.InnerHtml += td92.ToString();
            #endregion
            #region Other Header row
            TagBuilder tr10 = new TagBuilder("tr");
            tr10.MergeAttribute("style", "border:0px;");
            TagBuilder th101 = new TagBuilder("th");
            th101.MergeAttribute("style", "text-align:center;width:200px;");
            th101.SetInnerText("Other");
            TagBuilder th102 = new TagBuilder("th");
            th102.MergeAttribute("style", "text-align:center");
            tr10.InnerHtml += th101.ToString() + th102.ToString();
            #endregion
            #region Tags row
            TagBuilder tr11 = new TagBuilder("tr");
            TagBuilder td111 = new TagBuilder("td");
            TagBuilder a11 = new TagBuilder("a");
            a11.MergeAttribute("href", "/ArticleArchive/Details/Tags/");
            a11.InnerHtml = "Tags";
            td111.InnerHtml = a11.ToString();
            td111.MergeAttribute("style", "text-align:center");
            tr11.InnerHtml = td111.ToString();
            TagBuilder td112 = new TagBuilder("td");
            ctr = 0;
            foreach (string culture in LocalisationSetting.allCultureMassive())
            {
                TagBuilder div = new TagBuilder("div");
                if (ctr % 2 == 0)
                    div.MergeAttribute("style", "background-color:#F7F5F5;");
                else
                    div.MergeAttribute("style", "background-color:#EDEBEB;");
                div.InnerHtml += Flag(culture, 14);
                foreach (var tag in ar.article.GetAllTags())
                {
                    TagBuilder link = new TagBuilder("a");
                    link.MergeAttribute("href", LangLink(culture,"/Article/Index/1/10/" + tag.TagId + "/" + tag.Path));
                    link.InnerHtml = tag.TagName;
                    div.InnerHtml += " <b>" + link + "</b>(" + ar.article.GetArticlesInTagsCount(tag.TagId, culture) + ") ";
                }
                td112.InnerHtml += div;
                ctr++;
            }
            tr11.InnerHtml += td112.ToString();
            #endregion
            table.InnerHtml = tr1.ToString() + tr2.ToString() + tr3.ToString() + tr4.ToString() + tr5.ToString() + tr6.ToString()
                + tr7.ToString() + tr8.ToString() + tr9.ToString() + tr10.ToString() + tr11.ToString();
            #endregion
            result.AppendLine(table.ToString());
            return result.ToString();
        }

        public static string ClosedArticles(this HtmlHelper html, int count)
        {
            IArticleRepository ar = new IArticleRepository();
            StringBuilder result = new StringBuilder();
            #region table
            TagBuilder table = new TagBuilder("table");
            #region Header row
            TagBuilder tr1 = new TagBuilder("tr");
            tr1.MergeAttribute("style", "border:0px;");
            TagBuilder th11 = new TagBuilder("th");
            th11.MergeAttribute("style", "text-align:center");
            tr1.InnerHtml = th11.ToString();
            #endregion
            #region Closed Articles row
            TagBuilder tr2 = new TagBuilder("tr");
            TagBuilder td21 = new TagBuilder("td");
            int ctr = 0;
            foreach (var article in ar.article.GetClosedArticles(count))
            {
                TagBuilder div = new TagBuilder("div");
                if (ctr % 2 == 0)
                    div.MergeAttribute("style", "background-color:#F7F5F5;");
                else
                    div.MergeAttribute("style", "background-color:#EDEBEB;");
                TagBuilder alink = new TagBuilder("a");
                alink.MergeAttribute("href",LangLink(article.Culture, "/Article/View/" + article.ArticleId + "/" + article.Path));
                alink.InnerHtml = article.Title;
                TagBuilder clink = new TagBuilder("a");
                clink.MergeAttribute("href", LangLink(article.Culture,"/Article/Index/1/10/" + article.mytrip_ArticlesCategory.CategoryId + "/" + article.mytrip_ArticlesCategory.Path));
                clink.InnerHtml = article.mytrip_ArticlesCategory.Title;
                div.InnerHtml += "<b>" + alink + " " + Globe(article.AllCulture, 14) + " " + Flag(article.Culture, 14)
                    + "</b> closed " + article.CloseDate.ToString("dd MMMM yyyy HH:mm") + " in " + clink + " added by " + article.UserName;
                td21.InnerHtml += div;
                ctr++;
            }
            tr2.InnerHtml += td21.ToString();
            #endregion
            table.InnerHtml = tr1.ToString() + tr2.ToString();
            #endregion
            result.AppendLine(table.ToString());
            return result.ToString();
        }

        public static string CountPager(this HtmlHelper html, int count)
        {
            StringBuilder result = new StringBuilder();
            TagBuilder div = new TagBuilder("div");
            div.MergeAttribute("style", "float:right;margin-right:25px");
            TagBuilder a5 = new TagBuilder("a");
            a5.MergeAttribute("href", "/ArticleArchive/Index/5/");
            a5.InnerHtml = "5";
            TagBuilder a10 = new TagBuilder("a");
            a10.MergeAttribute("href", "/ArticleArchive/Index/10/");
            a10.InnerHtml = "10";
            TagBuilder a15 = new TagBuilder("a");
            a15.MergeAttribute("href", "/ArticleArchive/Index/15/");
            a15.InnerHtml = "15";
            if (count == 5)
                div.InnerHtml = "5 " + a10.ToString() + " " + a15.ToString();
            else if (count == 10)
                div.InnerHtml = a5.ToString() + " 10 " + " " + a15.ToString();
            else
                div.InnerHtml = a5.ToString() + " " + a10.ToString() + " 15";
            result.AppendLine(div.ToString());
            return result.ToString();
        }

        public static string CulturePager(this HtmlHelper html, string path)
        {
            if (path == "Tags")
                return string.Empty;
            StringBuilder result = new StringBuilder();
            TagBuilder div = new TagBuilder("div");
            div.MergeAttribute("style", "float:right;margin-right:25px");
            TagBuilder a = new TagBuilder("a");
            a.MergeAttribute("href", "/ArticleArchive/Details/"+path+"/");
            a.InnerHtml =Globe(true, 20);
            div.InnerHtml = a.ToString()+"  ";
            foreach (string culture in LocalisationSetting.allCultureMassive())
            {
                TagBuilder img = new TagBuilder("img");
                img.MergeAttribute("src", "/Content/images/" + culture.ToLower() + ".png");
                img.MergeAttribute("style", "border-width:0px;width:20px");
                img.MergeAttribute("alt", culture.ToLower());
                img.MergeAttribute("title", culture.ToLower());
                TagBuilder a1 = new TagBuilder("a");
                a1.MergeAttribute("href", "/ArticleArchive/Details/"+path+"/"+culture.ToLower()+"/");
                a1.InnerHtml = img.ToString();
                div.InnerHtml += a1.ToString()+"  ";
            }
            result.AppendLine(div.ToString());
            return result.ToString(); ;
        }

        public static string ShowDetails(this HtmlHelper html, string path,string culture)
        {
            string details = "";
            if (path == "Articles")
                details = ArticlesDetails(culture);
            else if (path == "Categories")
                details = CategoriesDetails(culture);
            else if (path == "Comments")
                details = CommentsDetails(culture);
            else if (path == "Blogs")
                details = BlogsDetails(culture);
            else if (path == "Topics")
                details = TopicsDetails(culture);
            else if (path == "Posts")
                details = PostsDetails(culture);
            else if (path == "BlogsComments")
                details = BlogsCommentsDetails(culture);
            else if (path == "Tags")
                details = TagsDetails();
            else if (path == "ClosedArticles")
                details = ClosedArticlesDetails(culture);
            return details;
        }

        public static string ArticlesDetails(string culture)
        {
            IArticleRepository ar = new IArticleRepository();
            StringBuilder result = new StringBuilder();
            #region table
            TagBuilder table = new TagBuilder("table");
            #region Days of week
            string week = "";
            string dayOfWeek = DateTime.Now.DayOfWeek.ToString();
            if (dayOfWeek == "Monday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.article.GetLastArticlesByDate(-2,culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.article.GetLastArticlesByDate(-3, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.article.GetLastArticlesByDate(-4, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-5).ToString("dddd")), ar.article.GetLastArticlesByDate(-5, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-6).ToString("dddd")), ar.article.GetLastArticlesByDate(-6, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-7).ToString("dddd")), ar.article.GetLastArticlesByDate(-7, culture));
                week += BuildRow("Two weeks ago", ar.article.GetLastArticlesByDate(-8, 7, culture));
                week += BuildRow("Three weeks ago", ar.article.GetLastArticlesByDate(-15, 7, culture));
                week += BuildRow("Four weeks ago", ar.article.GetLastArticlesByDate(-22, 7, culture));
                week += BuildRow("Month ago", ar.article.GetLastArticlesByDate(-29, 30, culture));
                week += BuildRow("Two months ago", ar.article.GetLastArticlesByDate(-59, 30, culture));
                week += BuildRow("Three months ago", ar.article.GetLastArticlesByDate(-89, 30, culture));
                week += BuildRow("Older", ar.article.GetLastArticlesByDate(-119, 99999, culture));
            }
            else if (dayOfWeek == "Tuesday")
            {
                week = BuildRow("Week ago", ar.article.GetLastArticlesByDate(-2, 7, culture));
                week += BuildRow("Two weeks ago", ar.article.GetLastArticlesByDate(-9, 7, culture));
                week += BuildRow("Three weeks ago", ar.article.GetLastArticlesByDate(-16, 7, culture));
                week += BuildRow("Four weeks ago", ar.article.GetLastArticlesByDate(-23, 7,culture));
                week += BuildRow("Month ago", ar.article.GetLastArticlesByDate(-30, 30,culture));
                week += BuildRow("Two months ago", ar.article.GetLastArticlesByDate(-60, 30,culture));
                week += BuildRow("Three months ago", ar.article.GetLastArticlesByDate(-90, 30,culture));
                week += BuildRow("Older", ar.article.GetLastArticlesByDate(-120, 99999,culture));
            }
            else if (dayOfWeek == "Wednesday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.article.GetLastArticlesByDate(-2,culture));
                week += BuildRow("Week ago", ar.article.GetLastArticlesByDate(-3, 7,culture));
                week += BuildRow("Two weeks ago", ar.article.GetLastArticlesByDate(-10, 7,culture));
                week += BuildRow("Three weeks ago", ar.article.GetLastArticlesByDate(-17, 7,culture));
                week += BuildRow("Four weeks ago", ar.article.GetLastArticlesByDate(-24, 7,culture));
                week += BuildRow("Month ago", ar.article.GetLastArticlesByDate(-31, 30,culture));
                week += BuildRow("Two months ago", ar.article.GetLastArticlesByDate(-61, 30,culture));
                week += BuildRow("Three months ago", ar.article.GetLastArticlesByDate(-91, 30,culture));
            }
            else if (dayOfWeek == "Thursday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.article.GetLastArticlesByDate(-2,culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.article.GetLastArticlesByDate(-3,culture));
                week += BuildRow("Week ago", ar.article.GetLastArticlesByDate(-4, 7,culture));
                week += BuildRow("Two weeks ago", ar.article.GetLastArticlesByDate(-11, 7,culture));
                week += BuildRow("Three weeks ago", ar.article.GetLastArticlesByDate(-18, 7,culture));
                week += BuildRow("Four weeks ago", ar.article.GetLastArticlesByDate(-25, 7,culture));
                week += BuildRow("Month ago", ar.article.GetLastArticlesByDate(-32, 30,culture));
                week += BuildRow("Two months ago", ar.article.GetLastArticlesByDate(-62, 30,culture));
                week += BuildRow("Three months ago", ar.article.GetLastArticlesByDate(-92, 30,culture));
                week += BuildRow("Older", ar.article.GetLastArticlesByDate(-122, 99999,culture));
            }
            else if (dayOfWeek == "Friday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.article.GetLastArticlesByDate(-2,culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.article.GetLastArticlesByDate(-3,culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.article.GetLastArticlesByDate(-4,culture));
                week += BuildRow("Week ago", ar.article.GetLastArticlesByDate(-5, 7,culture));
                week += BuildRow("Two weeks ago", ar.article.GetLastArticlesByDate(-12, 7,culture));
                week += BuildRow("Three weeks ago", ar.article.GetLastArticlesByDate(-19, 7,culture));
                week += BuildRow("Four weeks ago", ar.article.GetLastArticlesByDate(-26, 7,culture));
                week += BuildRow("Month ago", ar.article.GetLastArticlesByDate(-33, 30,culture));
                week += BuildRow("Two months ago", ar.article.GetLastArticlesByDate(-63, 30,culture));
                week += BuildRow("Three months ago", ar.article.GetLastArticlesByDate(-93, 30,culture));
                week += BuildRow("Older", ar.article.GetLastArticlesByDate(-123, 99999,culture));
            }
            else if (dayOfWeek == "Saturday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.article.GetLastArticlesByDate(-2,culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.article.GetLastArticlesByDate(-3,culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.article.GetLastArticlesByDate(-4,culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-5).ToString("dddd")), ar.article.GetLastArticlesByDate(-5,culture));
                week += BuildRow("Week ago", ar.article.GetLastArticlesByDate(-6, 7,culture));
                week += BuildRow("Two weeks ago", ar.article.GetLastArticlesByDate(-13, 7,culture));
                week += BuildRow("Three weeks ago", ar.article.GetLastArticlesByDate(-20, 7,culture));
                week += BuildRow("Four weeks ago", ar.article.GetLastArticlesByDate(-27, 7,culture));
                week += BuildRow("Month ago", ar.article.GetLastArticlesByDate(-34, 30,culture));
                week += BuildRow("Two months ago", ar.article.GetLastArticlesByDate(-64, 30,culture));
                week += BuildRow("Three months ago", ar.article.GetLastArticlesByDate(-94, 30,culture));
                week += BuildRow("Older", ar.article.GetLastArticlesByDate(-124, 99999,culture));
            }
            else if (dayOfWeek == "Sunday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.article.GetLastArticlesByDate(-2,culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.article.GetLastArticlesByDate(-3,culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.article.GetLastArticlesByDate(-4,culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-5).ToString("dddd")), ar.article.GetLastArticlesByDate(-5,culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-6).ToString("dddd")), ar.article.GetLastArticlesByDate(-6,culture));
                week += BuildRow("Week ago", ar.article.GetLastArticlesByDate(-7, 7,culture));
                week += BuildRow("Two weeks ago", ar.article.GetLastArticlesByDate(-14, 7,culture));
                week += BuildRow("Three weeks ago", ar.article.GetLastArticlesByDate(-21, 7,culture));
                week += BuildRow("Month ago", ar.article.GetLastArticlesByDate(-28, 30,culture));
                week += BuildRow("Two months ago", ar.article.GetLastArticlesByDate(-58, 30,culture));
                week += BuildRow("Three months ago", ar.article.GetLastArticlesByDate(-88, 30,culture));
                week += BuildRow("Older", ar.article.GetLastArticlesByDate(-118, 99999,culture));
            }
            #endregion
            table.InnerHtml = BuildRow("Today", ar.article.GetLastArticlesByDate(0,culture))
                + BuildRow("Yesterday", ar.article.GetLastArticlesByDate(-1,culture)) + week;
            #endregion
            result.AppendLine(table.ToString());
            return result.ToString();
        }
        public static string ClosedArticlesDetails(string culture)
        {
            IArticleRepository ar = new IArticleRepository();
            StringBuilder result = new StringBuilder();
            #region table
            TagBuilder table = new TagBuilder("table");
            #region Days of week
            string week = "";
            string dayOfWeek = DateTime.Now.DayOfWeek.ToString();
            if (dayOfWeek == "Monday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.article.GetClosedArticlesByDate(-2,culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.article.GetClosedArticlesByDate(-3,culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.article.GetClosedArticlesByDate(-4,culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-5).ToString("dddd")), ar.article.GetClosedArticlesByDate(-5,culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-6).ToString("dddd")), ar.article.GetClosedArticlesByDate(-6,culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-7).ToString("dddd")), ar.article.GetClosedArticlesByDate(-7,culture));
                week += BuildRow("Two weeks ago", ar.article.GetClosedArticlesByDate(-8, 7,culture));
                week += BuildRow("Three weeks ago", ar.article.GetClosedArticlesByDate(-15, 7,culture));
                week += BuildRow("Four weeks ago", ar.article.GetClosedArticlesByDate(-22, 7,culture));
                week += BuildRow("Month ago", ar.article.GetClosedArticlesByDate(-29, 30,culture));
                week += BuildRow("Two months ago", ar.article.GetClosedArticlesByDate(-59, 30,culture));
                week += BuildRow("Three months ago", ar.article.GetClosedArticlesByDate(-89, 30,culture));
                week += BuildRow("Older", ar.article.GetClosedArticlesByDate(-119, 99999,culture));
            }
            else if (dayOfWeek == "Tuesday")
            {
                week = BuildRow("Week ago", ar.article.GetClosedArticlesByDate(-2, 7,culture));
                week += BuildRow("Two weeks ago", ar.article.GetClosedArticlesByDate(-9, 7,culture));
                week += BuildRow("Three weeks ago", ar.article.GetClosedArticlesByDate(-16, 7,culture));
                week += BuildRow("Four weeks ago", ar.article.GetClosedArticlesByDate(-23, 7,culture));
                week += BuildRow("Month ago", ar.article.GetClosedArticlesByDate(-30, 30,culture));
                week += BuildRow("Two months ago", ar.article.GetClosedArticlesByDate(-60, 30,culture));
                week += BuildRow("Three months ago", ar.article.GetClosedArticlesByDate(-90, 30,culture));
                week += BuildRow("Older", ar.article.GetClosedArticlesByDate(-120, 99999,culture));
            }
            else if (dayOfWeek == "Wednesday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.article.GetClosedArticlesByDate(-2,culture));
                week += BuildRow("Week ago", ar.article.GetClosedArticlesByDate(-3, 7,culture));
                week += BuildRow("Two weeks ago", ar.article.GetClosedArticlesByDate(-10, 7,culture));
                week += BuildRow("Three weeks ago", ar.article.GetClosedArticlesByDate(-17, 7,culture));
                week += BuildRow("Four weeks ago", ar.article.GetClosedArticlesByDate(-24, 7,culture));
                week += BuildRow("Month ago", ar.article.GetClosedArticlesByDate(-31, 30,culture));
                week += BuildRow("Two months ago", ar.article.GetClosedArticlesByDate(-61, 30,culture));
                week += BuildRow("Three months ago", ar.article.GetClosedArticlesByDate(-91, 30,culture));
            }
            else if (dayOfWeek == "Thursday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.article.GetClosedArticlesByDate(-2,culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.article.GetClosedArticlesByDate(-3,culture));
                week += BuildRow("Week ago", ar.article.GetClosedArticlesByDate(-4, 7,culture));
                week += BuildRow("Two weeks ago", ar.article.GetClosedArticlesByDate(-11, 7,culture));
                week += BuildRow("Three weeks ago", ar.article.GetClosedArticlesByDate(-18, 7,culture));
                week += BuildRow("Four weeks ago", ar.article.GetClosedArticlesByDate(-25, 7,culture));
                week += BuildRow("Month ago", ar.article.GetClosedArticlesByDate(-32, 30,culture));
                week += BuildRow("Two months ago", ar.article.GetClosedArticlesByDate(-62, 30,culture));
                week += BuildRow("Three months ago", ar.article.GetClosedArticlesByDate(-92, 30,culture));
                week += BuildRow("Older", ar.article.GetClosedArticlesByDate(-122, 99999,culture));
            }
            else if (dayOfWeek == "Friday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.article.GetClosedArticlesByDate(-2,culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.article.GetClosedArticlesByDate(-3,culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.article.GetClosedArticlesByDate(-4,culture));
                week += BuildRow("Week ago", ar.article.GetClosedArticlesByDate(-5, 7,culture));
                week += BuildRow("Two weeks ago", ar.article.GetClosedArticlesByDate(-12, 7,culture));
                week += BuildRow("Three weeks ago", ar.article.GetClosedArticlesByDate(-19, 7,culture));
                week += BuildRow("Four weeks ago", ar.article.GetClosedArticlesByDate(-26, 7,culture));
                week += BuildRow("Month ago", ar.article.GetClosedArticlesByDate(-33, 30,culture));
                week += BuildRow("Two months ago", ar.article.GetClosedArticlesByDate(-63, 30,culture));
                week += BuildRow("Three months ago", ar.article.GetClosedArticlesByDate(-93, 30,culture));
                week += BuildRow("Older", ar.article.GetClosedArticlesByDate(-123, 99999,culture));
            }
            else if (dayOfWeek == "Saturday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.article.GetClosedArticlesByDate(-2,culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.article.GetClosedArticlesByDate(-3,culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.article.GetClosedArticlesByDate(-4,culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-5).ToString("dddd")), ar.article.GetClosedArticlesByDate(-5,culture));
                week += BuildRow("Week ago", ar.article.GetClosedArticlesByDate(-6, 7,culture));
                week += BuildRow("Two weeks ago", ar.article.GetClosedArticlesByDate(-13, 7,culture));
                week += BuildRow("Three weeks ago", ar.article.GetClosedArticlesByDate(-20, 7,culture));
                week += BuildRow("Four weeks ago", ar.article.GetClosedArticlesByDate(-27, 7,culture));
                week += BuildRow("Month ago", ar.article.GetClosedArticlesByDate(-34, 30,culture));
                week += BuildRow("Two months ago", ar.article.GetClosedArticlesByDate(-64, 30,culture));
                week += BuildRow("Three months ago", ar.article.GetClosedArticlesByDate(-94, 30,culture));
                week += BuildRow("Older", ar.article.GetClosedArticlesByDate(-124, 99999,culture));
            }
            else if (dayOfWeek == "Sunday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.article.GetClosedArticlesByDate(-2,culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.article.GetClosedArticlesByDate(-3,culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.article.GetClosedArticlesByDate(-4,culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-5).ToString("dddd")), ar.article.GetClosedArticlesByDate(-5,culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-6).ToString("dddd")), ar.article.GetClosedArticlesByDate(-6,culture));
                week += BuildRow("Week ago", ar.article.GetClosedArticlesByDate(-7, 7,culture));
                week += BuildRow("Two weeks ago", ar.article.GetClosedArticlesByDate(-14, 7,culture));
                week += BuildRow("Three weeks ago", ar.article.GetClosedArticlesByDate(-21, 7,culture));
                week += BuildRow("Month ago", ar.article.GetClosedArticlesByDate(-28, 30,culture));
                week += BuildRow("Two months ago", ar.article.GetClosedArticlesByDate(-58, 30,culture));
                week += BuildRow("Three months ago", ar.article.GetClosedArticlesByDate(-88, 30,culture));
                week += BuildRow("Older", ar.article.GetClosedArticlesByDate(-118, 99999,culture));
            }
            #endregion
            table.InnerHtml = BuildRow("Today", ar.article.GetClosedArticlesByDate(0,culture))
                + BuildRow("Yesterday", ar.article.GetClosedArticlesByDate(-1,culture)) + week;
            #endregion
            result.AppendLine(table.ToString());
            return result.ToString();
        }
        public static string PostsDetails(string culture)
        {
            IArticleRepository ar = new IArticleRepository();
            StringBuilder result = new StringBuilder();
            #region table
            TagBuilder table = new TagBuilder("table");
            #region Days of week
            string week = "";
            string dayOfWeek = DateTime.Now.DayOfWeek.ToString();
            if (dayOfWeek == "Monday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.article.GetLastPostsByDate(-2, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.article.GetLastPostsByDate(-3, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.article.GetLastPostsByDate(-4, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-5).ToString("dddd")), ar.article.GetLastPostsByDate(-5, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-6).ToString("dddd")), ar.article.GetLastPostsByDate(-6, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-7).ToString("dddd")), ar.article.GetLastPostsByDate(-7, culture));
                week += BuildRow("Two weeks ago", ar.article.GetLastPostsByDate(-8, 7,culture));
                week += BuildRow("Three weeks ago", ar.article.GetLastPostsByDate(-15, 7, culture));
                week += BuildRow("Four weeks ago", ar.article.GetLastPostsByDate(-22, 7, culture));
                week += BuildRow("Month ago", ar.article.GetLastPostsByDate(-29, 30, culture));
                week += BuildRow("Two months ago", ar.article.GetLastPostsByDate(-59, 30, culture));
                week += BuildRow("Three months ago", ar.article.GetLastPostsByDate(-89, 30, culture));
                week += BuildRow("Older", ar.article.GetLastPostsByDate(-119, 99999, culture));
            }
            else if (dayOfWeek == "Tuesday")
            {
                week = BuildRow("Week ago", ar.article.GetLastPostsByDate(-2, 7, culture));
                week += BuildRow("Two weeks ago", ar.article.GetLastPostsByDate(-9, 7, culture));
                week += BuildRow("Three weeks ago", ar.article.GetLastPostsByDate(-16, 7, culture));
                week += BuildRow("Four weeks ago", ar.article.GetLastPostsByDate(-23, 7, culture));
                week += BuildRow("Month ago", ar.article.GetLastPostsByDate(-30, 30, culture));
                week += BuildRow("Two months ago", ar.article.GetLastPostsByDate(-60, 30, culture));
                week += BuildRow("Three months ago", ar.article.GetLastPostsByDate(-90, 30, culture));
                week += BuildRow("Older", ar.article.GetLastPostsByDate(-120, 99999, culture));
            }
            else if (dayOfWeek == "Wednesday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.article.GetLastPostsByDate(-2, culture));
                week += BuildRow("Week ago", ar.article.GetLastPostsByDate(-3, 7, culture));
                week += BuildRow("Two weeks ago", ar.article.GetLastPostsByDate(-10, 7, culture));
                week += BuildRow("Three weeks ago", ar.article.GetLastPostsByDate(-17, 7, culture));
                week += BuildRow("Four weeks ago", ar.article.GetLastPostsByDate(-24, 7, culture));
                week += BuildRow("Month ago", ar.article.GetLastPostsByDate(-31, 30, culture));
                week += BuildRow("Two months ago", ar.article.GetLastPostsByDate(-61, 30, culture));
                week += BuildRow("Three months ago", ar.article.GetLastPostsByDate(-91, 30, culture));
            }
            else if (dayOfWeek == "Thursday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.article.GetLastPostsByDate(-2, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.article.GetLastPostsByDate(-3, culture));
                week += BuildRow("Week ago", ar.article.GetLastPostsByDate(-4, 7, culture));
                week += BuildRow("Two weeks ago", ar.article.GetLastPostsByDate(-11, 7, culture));
                week += BuildRow("Three weeks ago", ar.article.GetLastPostsByDate(-18, 7, culture));
                week += BuildRow("Four weeks ago", ar.article.GetLastPostsByDate(-25, 7, culture));
                week += BuildRow("Month ago", ar.article.GetLastPostsByDate(-32, 30, culture));
                week += BuildRow("Two months ago", ar.article.GetLastPostsByDate(-62, 30, culture));
                week += BuildRow("Three months ago", ar.article.GetLastPostsByDate(-92, 30, culture));
                week += BuildRow("Older", ar.article.GetLastPostsByDate(-122, 99999, culture));
            }
            else if (dayOfWeek == "Friday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.article.GetLastPostsByDate(-2, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.article.GetLastPostsByDate(-3, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.article.GetLastPostsByDate(-4, culture));
                week += BuildRow("Week ago", ar.article.GetLastPostsByDate(-5, 7, culture));
                week += BuildRow("Two weeks ago", ar.article.GetLastPostsByDate(-12, 7, culture));
                week += BuildRow("Three weeks ago", ar.article.GetLastPostsByDate(-19, 7, culture));
                week += BuildRow("Four weeks ago", ar.article.GetLastPostsByDate(-26, 7, culture));
                week += BuildRow("Month ago", ar.article.GetLastPostsByDate(-33, 30, culture));
                week += BuildRow("Two months ago", ar.article.GetLastPostsByDate(-63, 30, culture));
                week += BuildRow("Three months ago", ar.article.GetLastPostsByDate(-93, 30, culture));
                week += BuildRow("Older", ar.article.GetLastPostsByDate(-123, 99999, culture));
            }
            else if (dayOfWeek == "Saturday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.article.GetLastPostsByDate(-2, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.article.GetLastPostsByDate(-3, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.article.GetLastPostsByDate(-4, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-5).ToString("dddd")), ar.article.GetLastPostsByDate(-5, culture));
                week += BuildRow("Week ago", ar.article.GetLastPostsByDate(-6, 7, culture));
                week += BuildRow("Two weeks ago", ar.article.GetLastPostsByDate(-13, 7, culture));
                week += BuildRow("Three weeks ago", ar.article.GetLastPostsByDate(-20, 7, culture));
                week += BuildRow("Four weeks ago", ar.article.GetLastPostsByDate(-27, 7, culture));
                week += BuildRow("Month ago", ar.article.GetLastPostsByDate(-34, 30, culture));
                week += BuildRow("Two months ago", ar.article.GetLastPostsByDate(-64, 30, culture));
                week += BuildRow("Three months ago", ar.article.GetLastPostsByDate(-94, 30, culture));
                week += BuildRow("Older", ar.article.GetLastPostsByDate(-124, 99999, culture));
            }
            else if (dayOfWeek == "Sunday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.article.GetLastPostsByDate(-2, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.article.GetLastPostsByDate(-3, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.article.GetLastPostsByDate(-4, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-5).ToString("dddd")), ar.article.GetLastPostsByDate(-5, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-6).ToString("dddd")), ar.article.GetLastPostsByDate(-6, culture));
                week += BuildRow("Week ago", ar.article.GetLastPostsByDate(-7, 7, culture));
                week += BuildRow("Two weeks ago", ar.article.GetLastPostsByDate(-14, 7, culture));
                week += BuildRow("Three weeks ago", ar.article.GetLastPostsByDate(-21, 7, culture));
                week += BuildRow("Month ago", ar.article.GetLastPostsByDate(-28, 30, culture));
                week += BuildRow("Two months ago", ar.article.GetLastPostsByDate(-58, 30, culture));
                week += BuildRow("Three months ago", ar.article.GetLastPostsByDate(-88, 30, culture));
                week += BuildRow("Older", ar.article.GetLastPostsByDate(-118, 99999, culture));
            }
            #endregion
            table.InnerHtml = BuildRow("Today", ar.article.GetLastPostsByDate(0, culture))
                + BuildRow("Yesterday", ar.article.GetLastPostsByDate(-1, culture)) + week;
            #endregion
            result.AppendLine(table.ToString());
            return result.ToString();
        }
        public static string CategoriesDetails(string culture)
        {
            IArticleRepository ar = new IArticleRepository();
            StringBuilder result = new StringBuilder();
            #region table
            TagBuilder table = new TagBuilder("table");
            #region Days of week
            string week = "";
            string dayOfWeek = DateTime.Now.DayOfWeek.ToString();
            if (dayOfWeek == "Monday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.category.GetLastCategoriesByDate(-2,culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.category.GetLastCategoriesByDate(-3,culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.category.GetLastCategoriesByDate(-4,culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-5).ToString("dddd")), ar.category.GetLastCategoriesByDate(-5,culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-6).ToString("dddd")), ar.category.GetLastCategoriesByDate(-6,culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-7).ToString("dddd")), ar.category.GetLastCategoriesByDate(-7,culture));
                week += BuildRow("Two weeks ago", ar.category.GetLastCategoriesByDate(-8, 7,culture));
                week += BuildRow("Three weeks ago", ar.category.GetLastCategoriesByDate(-15, 7,culture));
                week += BuildRow("Four weeks ago", ar.category.GetLastCategoriesByDate(-22, 7,culture));
                week += BuildRow("Month ago", ar.category.GetLastCategoriesByDate(-29, 30,culture));
                week += BuildRow("Two months ago", ar.category.GetLastCategoriesByDate(-59, 30,culture));
                week += BuildRow("Three months ago", ar.category.GetLastCategoriesByDate(-89, 30,culture));
                week += BuildRow("Older", ar.category.GetLastCategoriesByDate(-119, 99999,culture));
            }
            else if (dayOfWeek == "Tuesday")
            {
                week = BuildRow("Week ago", ar.category.GetLastCategoriesByDate(-2, 7,culture));
                week += BuildRow("Two weeks ago", ar.category.GetLastCategoriesByDate(-9, 7,culture));
                week += BuildRow("Three weeks ago", ar.category.GetLastCategoriesByDate(-16, 7,culture));
                week += BuildRow("Four weeks ago", ar.category.GetLastCategoriesByDate(-23, 7,culture));
                week += BuildRow("Month ago", ar.category.GetLastCategoriesByDate(-30, 30,culture));
                week += BuildRow("Two months ago", ar.category.GetLastCategoriesByDate(-60, 30,culture));
                week += BuildRow("Three months ago", ar.category.GetLastCategoriesByDate(-90, 30,culture));
                week += BuildRow("Older", ar.category.GetLastCategoriesByDate(-120, 99999,culture));
            }
            else if (dayOfWeek == "Wednesday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.category.GetLastCategoriesByDate(-2,culture));
                week += BuildRow("Week ago", ar.category.GetLastCategoriesByDate(-3, 7,culture));
                week += BuildRow("Two weeks ago", ar.category.GetLastCategoriesByDate(-10, 7,culture));
                week += BuildRow("Three weeks ago", ar.category.GetLastCategoriesByDate(-17, 7,culture));
                week += BuildRow("Four weeks ago", ar.category.GetLastCategoriesByDate(-24, 7,culture));
                week += BuildRow("Month ago", ar.category.GetLastCategoriesByDate(-31, 30,culture));
                week += BuildRow("Two months ago", ar.category.GetLastCategoriesByDate(-61, 30,culture));
                week += BuildRow("Three months ago", ar.category.GetLastCategoriesByDate(-91, 30,culture));
            }
            else if (dayOfWeek == "Thursday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.category.GetLastCategoriesByDate(-2,culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.category.GetLastCategoriesByDate(-3,culture));
                week += BuildRow("Week ago", ar.category.GetLastCategoriesByDate(-4, 7,culture));
                week += BuildRow("Two weeks ago", ar.category.GetLastCategoriesByDate(-11, 7,culture));
                week += BuildRow("Three weeks ago", ar.category.GetLastCategoriesByDate(-18, 7,culture));
                week += BuildRow("Four weeks ago", ar.category.GetLastCategoriesByDate(-25, 7,culture));
                week += BuildRow("Month ago", ar.category.GetLastCategoriesByDate(-32, 30,culture));
                week += BuildRow("Two months ago", ar.category.GetLastCategoriesByDate(-62, 30,culture));
                week += BuildRow("Three months ago", ar.category.GetLastCategoriesByDate(-92, 30,culture));
                week += BuildRow("Older", ar.category.GetLastCategoriesByDate(-122, 99999,culture));
            }
            else if (dayOfWeek == "Friday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.category.GetLastCategoriesByDate(-2,culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.category.GetLastCategoriesByDate(-3,culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.category.GetLastCategoriesByDate(-4,culture));
                week += BuildRow("Week ago", ar.category.GetLastCategoriesByDate(-5, 7,culture));
                week += BuildRow("Two weeks ago", ar.category.GetLastCategoriesByDate(-12, 7,culture));
                week += BuildRow("Three weeks ago", ar.category.GetLastCategoriesByDate(-19, 7,culture));
                week += BuildRow("Four weeks ago", ar.category.GetLastCategoriesByDate(-26, 7,culture));
                week += BuildRow("Month ago", ar.category.GetLastCategoriesByDate(-33, 30,culture));
                week += BuildRow("Two months ago", ar.category.GetLastCategoriesByDate(-63, 30,culture));
                week += BuildRow("Three months ago", ar.category.GetLastCategoriesByDate(-93, 30,culture));
                week += BuildRow("Older", ar.category.GetLastCategoriesByDate(-123, 99999,culture));
            }
            else if (dayOfWeek == "Saturday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.category.GetLastCategoriesByDate(-2,culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.category.GetLastCategoriesByDate(-3,culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.category.GetLastCategoriesByDate(-4,culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-5).ToString("dddd")), ar.category.GetLastCategoriesByDate(-5,culture));
                week += BuildRow("Week ago", ar.category.GetLastCategoriesByDate(-6, 7,culture));
                week += BuildRow("Two weeks ago", ar.category.GetLastCategoriesByDate(-13, 7,culture));
                week += BuildRow("Three weeks ago", ar.category.GetLastCategoriesByDate(-20, 7,culture));
                week += BuildRow("Four weeks ago", ar.category.GetLastCategoriesByDate(-27, 7,culture));
                week += BuildRow("Month ago", ar.category.GetLastCategoriesByDate(-34, 30,culture));
                week += BuildRow("Two months ago", ar.category.GetLastCategoriesByDate(-64, 30,culture));
                week += BuildRow("Three months ago", ar.category.GetLastCategoriesByDate(-94, 30,culture));
                week += BuildRow("Older", ar.category.GetLastCategoriesByDate(-124, 99999,culture));
            }
            else if (dayOfWeek == "Sunday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.category.GetLastCategoriesByDate(-2,culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.category.GetLastCategoriesByDate(-3,culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.category.GetLastCategoriesByDate(-4,culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-5).ToString("dddd")), ar.category.GetLastCategoriesByDate(-5,culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-6).ToString("dddd")), ar.category.GetLastCategoriesByDate(-6,culture));
                week += BuildRow("Week ago", ar.category.GetLastCategoriesByDate(-7, 7,culture));
                week += BuildRow("Two weeks ago", ar.category.GetLastCategoriesByDate(-14, 7,culture));
                week += BuildRow("Three weeks ago", ar.category.GetLastCategoriesByDate(-21, 7,culture));
                week += BuildRow("Month ago", ar.category.GetLastCategoriesByDate(-28, 30,culture));
                week += BuildRow("Two months ago", ar.category.GetLastCategoriesByDate(-58, 30,culture));
                week += BuildRow("Three months ago", ar.category.GetLastCategoriesByDate(-88, 30,culture));
                week += BuildRow("Older", ar.category.GetLastCategoriesByDate(-118, 99999,culture));
            }
            #endregion
            table.InnerHtml = BuildRow("Today", ar.category.GetLastCategoriesByDate(0, culture))
                + BuildRow("Yesterday", ar.category.GetLastCategoriesByDate(-1, culture)) + week;
            #endregion
            result.AppendLine(table.ToString());
            return result.ToString();
        }
        public static string BlogsDetails(string culture)
        {
            IArticleRepository ar = new IArticleRepository();
            StringBuilder result = new StringBuilder();
            #region table
            TagBuilder table = new TagBuilder("table");
            #region Days of week
            string week = "";
            string dayOfWeek = DateTime.Now.DayOfWeek.ToString();
            if (dayOfWeek == "Monday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.category.GetLastBlogsByDate(-2,culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.category.GetLastBlogsByDate(-3,culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.category.GetLastBlogsByDate(-4,culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-5).ToString("dddd")), ar.category.GetLastBlogsByDate(-5,culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-6).ToString("dddd")), ar.category.GetLastBlogsByDate(-6,culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-7).ToString("dddd")), ar.category.GetLastBlogsByDate(-7,culture));
                week += BuildRow("Two weeks ago", ar.category.GetLastBlogsByDate(-8, 7,culture));
                week += BuildRow("Three weeks ago", ar.category.GetLastBlogsByDate(-15, 7,culture));
                week += BuildRow("Four weeks ago", ar.category.GetLastBlogsByDate(-22, 7,culture));
                week += BuildRow("Month ago", ar.category.GetLastBlogsByDate(-29, 30,culture));
                week += BuildRow("Two months ago", ar.category.GetLastBlogsByDate(-59, 30,culture));
                week += BuildRow("Three months ago", ar.category.GetLastBlogsByDate(-89, 30,culture));
                week += BuildRow("Older", ar.category.GetLastBlogsByDate(-119, 99999,culture));
            }
            else if (dayOfWeek == "Tuesday")
            {
                week = BuildRow("Week ago", ar.category.GetLastBlogsByDate(-2, 7,culture));
                week += BuildRow("Two weeks ago", ar.category.GetLastBlogsByDate(-9, 7,culture));
                week += BuildRow("Three weeks ago", ar.category.GetLastBlogsByDate(-16, 7,culture));
                week += BuildRow("Four weeks ago", ar.category.GetLastBlogsByDate(-23, 7,culture));
                week += BuildRow("Month ago", ar.category.GetLastBlogsByDate(-30, 30,culture));
                week += BuildRow("Two months ago", ar.category.GetLastBlogsByDate(-60, 30,culture));
                week += BuildRow("Three months ago", ar.category.GetLastBlogsByDate(-90, 30,culture));
                week += BuildRow("Older", ar.category.GetLastBlogsByDate(-120, 99999,culture));
            }
            else if (dayOfWeek == "Wednesday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.category.GetLastBlogsByDate(-2,culture));
                week += BuildRow("Week ago", ar.category.GetLastBlogsByDate(-3, 7,culture));
                week += BuildRow("Two weeks ago", ar.category.GetLastBlogsByDate(-10, 7,culture));
                week += BuildRow("Three weeks ago", ar.category.GetLastBlogsByDate(-17, 7,culture));
                week += BuildRow("Four weeks ago", ar.category.GetLastBlogsByDate(-24, 7,culture));
                week += BuildRow("Month ago", ar.category.GetLastBlogsByDate(-31, 30,culture));
                week += BuildRow("Two months ago", ar.category.GetLastBlogsByDate(-61, 30,culture));
                week += BuildRow("Three months ago", ar.category.GetLastBlogsByDate(-91, 30,culture));
            }
            else if (dayOfWeek == "Thursday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.category.GetLastBlogsByDate(-2,culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.category.GetLastBlogsByDate(-3,culture));
                week += BuildRow("Week ago", ar.category.GetLastBlogsByDate(-4, 7,culture));
                week += BuildRow("Two weeks ago", ar.category.GetLastBlogsByDate(-11, 7,culture));
                week += BuildRow("Three weeks ago", ar.category.GetLastBlogsByDate(-18, 7,culture));
                week += BuildRow("Four weeks ago", ar.category.GetLastBlogsByDate(-25, 7,culture));
                week += BuildRow("Month ago", ar.category.GetLastBlogsByDate(-32, 30,culture));
                week += BuildRow("Two months ago", ar.category.GetLastBlogsByDate(-62, 30,culture));
                week += BuildRow("Three months ago", ar.category.GetLastBlogsByDate(-92, 30,culture));
                week += BuildRow("Older", ar.category.GetLastBlogsByDate(-122, 99999,culture));
            }
            else if (dayOfWeek == "Friday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.category.GetLastBlogsByDate(-2,culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.category.GetLastBlogsByDate(-3,culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.category.GetLastBlogsByDate(-4,culture));
                week += BuildRow("Week ago", ar.category.GetLastBlogsByDate(-5, 7,culture));
                week += BuildRow("Two weeks ago", ar.category.GetLastBlogsByDate(-12, 7,culture));
                week += BuildRow("Three weeks ago", ar.category.GetLastBlogsByDate(-19, 7,culture));
                week += BuildRow("Four weeks ago", ar.category.GetLastBlogsByDate(-26, 7,culture));
                week += BuildRow("Month ago", ar.category.GetLastBlogsByDate(-33, 30,culture));
                week += BuildRow("Two months ago", ar.category.GetLastBlogsByDate(-63, 30,culture));
                week += BuildRow("Three months ago", ar.category.GetLastBlogsByDate(-93, 30,culture));
                week += BuildRow("Older", ar.category.GetLastBlogsByDate(-123, 99999,culture));
            }
            else if (dayOfWeek == "Saturday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.category.GetLastBlogsByDate(-2,culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.category.GetLastBlogsByDate(-3,culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.category.GetLastBlogsByDate(-4,culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-5).ToString("dddd")), ar.category.GetLastBlogsByDate(-5,culture));
                week += BuildRow("Week ago", ar.category.GetLastBlogsByDate(-6, 7,culture));
                week += BuildRow("Two weeks ago", ar.category.GetLastBlogsByDate(-13, 7,culture));
                week += BuildRow("Three weeks ago", ar.category.GetLastBlogsByDate(-20, 7,culture));
                week += BuildRow("Four weeks ago", ar.category.GetLastBlogsByDate(-27, 7,culture));
                week += BuildRow("Month ago", ar.category.GetLastBlogsByDate(-34, 30,culture));
                week += BuildRow("Two months ago", ar.category.GetLastBlogsByDate(-64, 30,culture));
                week += BuildRow("Three months ago", ar.category.GetLastBlogsByDate(-94, 30,culture));
                week += BuildRow("Older", ar.category.GetLastBlogsByDate(-124, 99999,culture));
            }
            else if (dayOfWeek == "Sunday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.category.GetLastBlogsByDate(-2,culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.category.GetLastBlogsByDate(-3,culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.category.GetLastBlogsByDate(-4,culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-5).ToString("dddd")), ar.category.GetLastBlogsByDate(-5,culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-6).ToString("dddd")), ar.category.GetLastBlogsByDate(-6,culture));
                week += BuildRow("Week ago", ar.category.GetLastBlogsByDate(-7, 7,culture));
                week += BuildRow("Two weeks ago", ar.category.GetLastBlogsByDate(-14, 7,culture));
                week += BuildRow("Three weeks ago", ar.category.GetLastBlogsByDate(-21, 7,culture));
                week += BuildRow("Month ago", ar.category.GetLastBlogsByDate(-28, 30,culture));
                week += BuildRow("Two months ago", ar.category.GetLastBlogsByDate(-58, 30,culture));
                week += BuildRow("Three months ago", ar.category.GetLastBlogsByDate(-88, 30,culture));
                week += BuildRow("Older", ar.category.GetLastBlogsByDate(-118, 99999,culture));
            }
            #endregion
            table.InnerHtml = BuildRow("Today", ar.category.GetLastBlogsByDate(0,culture))
                + BuildRow("Yesterday", ar.category.GetLastBlogsByDate(-1,culture)) + week;
            #endregion
            result.AppendLine(table.ToString());
            return result.ToString();
        }
        public static string TopicsDetails(string culture)
        {
            IArticleRepository ar = new IArticleRepository();
            StringBuilder result = new StringBuilder();
            #region table
            TagBuilder table = new TagBuilder("table");
            #region Days of week
            string week = "";
            string dayOfWeek = DateTime.Now.DayOfWeek.ToString();
            if (dayOfWeek == "Monday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.category.GetLastTopicsByDate(-2, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.category.GetLastTopicsByDate(-3, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.category.GetLastTopicsByDate(-4, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-5).ToString("dddd")), ar.category.GetLastTopicsByDate(-5, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-6).ToString("dddd")), ar.category.GetLastTopicsByDate(-6, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-7).ToString("dddd")), ar.category.GetLastTopicsByDate(-7, culture));
                week += BuildRow("Two weeks ago", ar.category.GetLastTopicsByDate(-8, 7, culture));
                week += BuildRow("Three weeks ago", ar.category.GetLastTopicsByDate(-15, 7, culture));
                week += BuildRow("Four weeks ago", ar.category.GetLastTopicsByDate(-22, 7, culture));
                week += BuildRow("Month ago", ar.category.GetLastTopicsByDate(-29, 30, culture));
                week += BuildRow("Two months ago", ar.category.GetLastTopicsByDate(-59, 30, culture));
                week += BuildRow("Three months ago", ar.category.GetLastTopicsByDate(-89, 30, culture));
                week += BuildRow("Older", ar.category.GetLastTopicsByDate(-119, 99999, culture));
            }
            else if (dayOfWeek == "Tuesday")
            {
                week = BuildRow("Week ago", ar.category.GetLastTopicsByDate(-2, 7, culture));
                week += BuildRow("Two weeks ago", ar.category.GetLastTopicsByDate(-9, 7, culture));
                week += BuildRow("Three weeks ago", ar.category.GetLastTopicsByDate(-16, 7, culture));
                week += BuildRow("Four weeks ago", ar.category.GetLastTopicsByDate(-23, 7, culture));
                week += BuildRow("Month ago", ar.category.GetLastTopicsByDate(-30, 30, culture));
                week += BuildRow("Two months ago", ar.category.GetLastTopicsByDate(-60, 30, culture));
                week += BuildRow("Three months ago", ar.category.GetLastTopicsByDate(-90, 30, culture));
                week += BuildRow("Older", ar.category.GetLastTopicsByDate(-120, 99999, culture));
            }
            else if (dayOfWeek == "Wednesday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.category.GetLastTopicsByDate(-2, culture));
                week += BuildRow("Week ago", ar.category.GetLastTopicsByDate(-3, 7, culture));
                week += BuildRow("Two weeks ago", ar.category.GetLastTopicsByDate(-10, 7, culture));
                week += BuildRow("Three weeks ago", ar.category.GetLastTopicsByDate(-17, 7, culture));
                week += BuildRow("Four weeks ago", ar.category.GetLastTopicsByDate(-24, 7, culture));
                week += BuildRow("Month ago", ar.category.GetLastTopicsByDate(-31, 30, culture));
                week += BuildRow("Two months ago", ar.category.GetLastTopicsByDate(-61, 30, culture));
                week += BuildRow("Three months ago", ar.category.GetLastTopicsByDate(-91, 30, culture));
            }
            else if (dayOfWeek == "Thursday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.category.GetLastTopicsByDate(-2, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.category.GetLastTopicsByDate(-3, culture));
                week += BuildRow("Week ago", ar.category.GetLastTopicsByDate(-4, 7, culture));
                week += BuildRow("Two weeks ago", ar.category.GetLastTopicsByDate(-11, 7, culture));
                week += BuildRow("Three weeks ago", ar.category.GetLastTopicsByDate(-18, 7, culture));
                week += BuildRow("Four weeks ago", ar.category.GetLastTopicsByDate(-25, 7, culture));
                week += BuildRow("Month ago", ar.category.GetLastTopicsByDate(-32, 30, culture));
                week += BuildRow("Two months ago", ar.category.GetLastTopicsByDate(-62, 30, culture));
                week += BuildRow("Three months ago", ar.category.GetLastTopicsByDate(-92, 30, culture));
                week += BuildRow("Older", ar.category.GetLastTopicsByDate(-122, 99999, culture));
            }
            else if (dayOfWeek == "Friday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.category.GetLastTopicsByDate(-2, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.category.GetLastTopicsByDate(-3, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.category.GetLastTopicsByDate(-4, culture));
                week += BuildRow("Week ago", ar.category.GetLastTopicsByDate(-5, 7, culture));
                week += BuildRow("Two weeks ago", ar.category.GetLastTopicsByDate(-12, 7, culture));
                week += BuildRow("Three weeks ago", ar.category.GetLastTopicsByDate(-19, 7, culture));
                week += BuildRow("Four weeks ago", ar.category.GetLastTopicsByDate(-26, 7, culture));
                week += BuildRow("Month ago", ar.category.GetLastTopicsByDate(-33, 30, culture));
                week += BuildRow("Two months ago", ar.category.GetLastTopicsByDate(-63, 30, culture));
                week += BuildRow("Three months ago", ar.category.GetLastTopicsByDate(-93, 30, culture));
                week += BuildRow("Older", ar.category.GetLastTopicsByDate(-123, 99999, culture));
            }
            else if (dayOfWeek == "Saturday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.category.GetLastTopicsByDate(-2, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.category.GetLastTopicsByDate(-3, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.category.GetLastTopicsByDate(-4, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-5).ToString("dddd")), ar.category.GetLastTopicsByDate(-5, culture));
                week += BuildRow("Week ago", ar.category.GetLastTopicsByDate(-6, 7, culture));
                week += BuildRow("Two weeks ago", ar.category.GetLastTopicsByDate(-13, 7, culture));
                week += BuildRow("Three weeks ago", ar.category.GetLastTopicsByDate(-20, 7, culture));
                week += BuildRow("Four weeks ago", ar.category.GetLastTopicsByDate(-27, 7, culture));
                week += BuildRow("Month ago", ar.category.GetLastTopicsByDate(-34, 30, culture));
                week += BuildRow("Two months ago", ar.category.GetLastTopicsByDate(-64, 30, culture));
                week += BuildRow("Three months ago", ar.category.GetLastTopicsByDate(-94, 30, culture));
                week += BuildRow("Older", ar.category.GetLastTopicsByDate(-124, 99999, culture));
            }
            else if (dayOfWeek == "Sunday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.category.GetLastTopicsByDate(-2, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.category.GetLastTopicsByDate(-3, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.category.GetLastTopicsByDate(-4, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-5).ToString("dddd")), ar.category.GetLastTopicsByDate(-5, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-6).ToString("dddd")), ar.category.GetLastTopicsByDate(-6, culture));
                week += BuildRow("Week ago", ar.category.GetLastTopicsByDate(-7, 7, culture));
                week += BuildRow("Two weeks ago", ar.category.GetLastTopicsByDate(-14, 7, culture));
                week += BuildRow("Three weeks ago", ar.category.GetLastTopicsByDate(-21, 7, culture));
                week += BuildRow("Month ago", ar.category.GetLastTopicsByDate(-28, 30, culture));
                week += BuildRow("Two months ago", ar.category.GetLastTopicsByDate(-58, 30, culture));
                week += BuildRow("Three months ago", ar.category.GetLastTopicsByDate(-88, 30, culture));
                week += BuildRow("Older", ar.category.GetLastTopicsByDate(-118, 99999, culture));
            }
            #endregion
            table.InnerHtml = BuildRow("Today", ar.category.GetLastTopicsByDate(0, culture))
                + BuildRow("Yesterday", ar.category.GetLastTopicsByDate(-1, culture)) + week;
            #endregion
            result.AppendLine(table.ToString());
            return result.ToString();
        }
        public static string CommentsDetails(string culture)
        {
            IArticleRepository ar = new IArticleRepository();
            StringBuilder result = new StringBuilder();
            #region table
            TagBuilder table = new TagBuilder("table");
            #region Days of week
            string week = "";
            string dayOfWeek = DateTime.Now.DayOfWeek.ToString();
            if (dayOfWeek == "Monday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.comment.GetLastCommentsByDate(-2,culture,false));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.comment.GetLastCommentsByDate(-3,culture,false));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.comment.GetLastCommentsByDate(-4,culture,false));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-5).ToString("dddd")), ar.comment.GetLastCommentsByDate(-5,culture,false));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-6).ToString("dddd")), ar.comment.GetLastCommentsByDate(-6,culture,false));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-7).ToString("dddd")), ar.comment.GetLastCommentsByDate(-7,culture,false));
                week += BuildRow("Two weeks ago", ar.comment.GetLastCommentsByDate(-8, 7,culture,false));
                week += BuildRow("Three weeks ago", ar.comment.GetLastCommentsByDate(-15, 7,culture,false));
                week += BuildRow("Four weeks ago", ar.comment.GetLastCommentsByDate(-22, 7,culture,false));
                week += BuildRow("Month ago", ar.comment.GetLastCommentsByDate(-29, 30,culture,false));
                week += BuildRow("Two months ago", ar.comment.GetLastCommentsByDate(-59, 30,culture,false));
                week += BuildRow("Three months ago", ar.comment.GetLastCommentsByDate(-89, 30,culture,false));
                week += BuildRow("Older", ar.comment.GetLastCommentsByDate(-119, 99999,culture,false));
            }
            else if (dayOfWeek == "Tuesday")
            {
                week = BuildRow("Week ago", ar.comment.GetLastCommentsByDate(-2, 7,culture,false));
                week += BuildRow("Two weeks ago", ar.comment.GetLastCommentsByDate(-9, 7,culture,false));
                week += BuildRow("Three weeks ago", ar.comment.GetLastCommentsByDate(-16, 7,culture,false));
                week += BuildRow("Four weeks ago", ar.comment.GetLastCommentsByDate(-23, 7,culture,false));
                week += BuildRow("Month ago", ar.comment.GetLastCommentsByDate(-30, 30,culture,false));
                week += BuildRow("Two months ago", ar.comment.GetLastCommentsByDate(-60, 30,culture,false));
                week += BuildRow("Three months ago", ar.comment.GetLastCommentsByDate(-90, 30,culture,false));
                week += BuildRow("Older", ar.comment.GetLastCommentsByDate(-120, 99999,culture,false));
            }
            else if (dayOfWeek == "Wednesday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.comment.GetLastCommentsByDate(-2,culture,false));
                week += BuildRow("Week ago", ar.comment.GetLastCommentsByDate(-3, 7,culture,false));
                week += BuildRow("Two weeks ago", ar.comment.GetLastCommentsByDate(-10, 7,culture,false));
                week += BuildRow("Three weeks ago", ar.comment.GetLastCommentsByDate(-17, 7,culture,false));
                week += BuildRow("Four weeks ago", ar.comment.GetLastCommentsByDate(-24, 7,culture,false));
                week += BuildRow("Month ago", ar.comment.GetLastCommentsByDate(-31, 30,culture,false));
                week += BuildRow("Two months ago", ar.comment.GetLastCommentsByDate(-61, 30,culture,false));
                week += BuildRow("Three months ago", ar.comment.GetLastCommentsByDate(-91, 30,culture,false));
            }
            else if (dayOfWeek == "Thursday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.comment.GetLastCommentsByDate(-2,culture,false));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.comment.GetLastCommentsByDate(-3,culture,false));
                week += BuildRow("Week ago", ar.comment.GetLastCommentsByDate(-4, 7,culture,false));
                week += BuildRow("Two weeks ago", ar.comment.GetLastCommentsByDate(-11, 7,culture,false));
                week += BuildRow("Three weeks ago", ar.comment.GetLastCommentsByDate(-18, 7,culture,false));
                week += BuildRow("Four weeks ago", ar.comment.GetLastCommentsByDate(-25, 7,culture,false));
                week += BuildRow("Month ago", ar.comment.GetLastCommentsByDate(-32, 30,culture,false));
                week += BuildRow("Two months ago", ar.comment.GetLastCommentsByDate(-62, 30,culture,false));
                week += BuildRow("Three months ago", ar.comment.GetLastCommentsByDate(-92, 30,culture,false));
                week += BuildRow("Older", ar.comment.GetLastCommentsByDate(-122, 99999,culture,false));
            }
            else if (dayOfWeek == "Friday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.comment.GetLastCommentsByDate(-2,culture,false));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.comment.GetLastCommentsByDate(-3,culture,false));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.comment.GetLastCommentsByDate(-4,culture,false));
                week += BuildRow("Week ago", ar.comment.GetLastCommentsByDate(-5, 7,culture,false));
                week += BuildRow("Two weeks ago", ar.comment.GetLastCommentsByDate(-12, 7,culture,false));
                week += BuildRow("Three weeks ago", ar.comment.GetLastCommentsByDate(-19, 7,culture,false));
                week += BuildRow("Four weeks ago", ar.comment.GetLastCommentsByDate(-26, 7,culture,false));
                week += BuildRow("Month ago", ar.comment.GetLastCommentsByDate(-33, 30,culture,false));
                week += BuildRow("Two months ago", ar.comment.GetLastCommentsByDate(-63, 30,culture,false));
                week += BuildRow("Three months ago", ar.comment.GetLastCommentsByDate(-93, 30,culture,false));
                week += BuildRow("Older", ar.comment.GetLastCommentsByDate(-123, 99999,culture,false));
            }
            else if (dayOfWeek == "Saturday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.comment.GetLastCommentsByDate(-2,culture,false));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.comment.GetLastCommentsByDate(-3,culture,false));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.comment.GetLastCommentsByDate(-4,culture,false));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-5).ToString("dddd")), ar.comment.GetLastCommentsByDate(-5,culture,false));
                week += BuildRow("Week ago", ar.comment.GetLastCommentsByDate(-6, 7,culture,false));
                week += BuildRow("Two weeks ago", ar.comment.GetLastCommentsByDate(-13, 7,culture,false));
                week += BuildRow("Three weeks ago", ar.comment.GetLastCommentsByDate(-20, 7,culture,false));
                week += BuildRow("Four weeks ago", ar.comment.GetLastCommentsByDate(-27, 7,culture,false));
                week += BuildRow("Month ago", ar.comment.GetLastCommentsByDate(-34, 30,culture,false));
                week += BuildRow("Two months ago", ar.comment.GetLastCommentsByDate(-64, 30,culture,false));
                week += BuildRow("Three months ago", ar.comment.GetLastCommentsByDate(-94, 30,culture,false));
                week += BuildRow("Older", ar.comment.GetLastCommentsByDate(-124, 99999,culture,false));
            }
            else if (dayOfWeek == "Sunday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.comment.GetLastCommentsByDate(-2,culture,false));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.comment.GetLastCommentsByDate(-3,culture,false));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.comment.GetLastCommentsByDate(-4,culture,false));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-5).ToString("dddd")), ar.comment.GetLastCommentsByDate(-5,culture,false));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-6).ToString("dddd")), ar.comment.GetLastCommentsByDate(-6,culture,false));
                week += BuildRow("Week ago", ar.comment.GetLastCommentsByDate(-7, 7,culture,false));
                week += BuildRow("Two weeks ago", ar.comment.GetLastCommentsByDate(-14, 7,culture,false));
                week += BuildRow("Three weeks ago", ar.comment.GetLastCommentsByDate(-21, 7,culture,false));
                week += BuildRow("Month ago", ar.comment.GetLastCommentsByDate(-28, 30,culture,false));
                week += BuildRow("Two months ago", ar.comment.GetLastCommentsByDate(-58, 30,culture,false));
                week += BuildRow("Three months ago", ar.comment.GetLastCommentsByDate(-88, 30,culture,false));
                week += BuildRow("Older", ar.comment.GetLastCommentsByDate(-118, 99999,culture,false));
            }
            #endregion
            table.InnerHtml = BuildRow("Today", ar.comment.GetLastCommentsByDate(0,culture,false))
                + BuildRow("Yesterday", ar.comment.GetLastCommentsByDate(-1,culture,false)) + week;
            #endregion
            result.AppendLine(table.ToString());
            return result.ToString();
        }
        public static string BlogsCommentsDetails(string culture)
        {
            IArticleRepository ar = new IArticleRepository();
            StringBuilder result = new StringBuilder();
            #region table
            TagBuilder table = new TagBuilder("table");
            #region Days of week
            string week = "";
            string dayOfWeek = DateTime.Now.DayOfWeek.ToString();
            if (dayOfWeek == "Monday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.comment.GetLastCommentsByDate(-2, culture, true));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.comment.GetLastCommentsByDate(-3, culture, true));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.comment.GetLastCommentsByDate(-4, culture, true));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-5).ToString("dddd")), ar.comment.GetLastCommentsByDate(-5, culture, true));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-6).ToString("dddd")), ar.comment.GetLastCommentsByDate(-6, culture, true));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-7).ToString("dddd")), ar.comment.GetLastCommentsByDate(-7, culture, true));
                week += BuildRow("Two weeks ago", ar.comment.GetLastCommentsByDate(-8, 7, culture, true));
                week += BuildRow("Three weeks ago", ar.comment.GetLastCommentsByDate(-15, 7, culture, true));
                week += BuildRow("Four weeks ago", ar.comment.GetLastCommentsByDate(-22, 7, culture, true));
                week += BuildRow("Month ago", ar.comment.GetLastCommentsByDate(-29, 30, culture, true));
                week += BuildRow("Two months ago", ar.comment.GetLastCommentsByDate(-59, 30, culture, true));
                week += BuildRow("Three months ago", ar.comment.GetLastCommentsByDate(-89, 30, culture, true));
                week += BuildRow("Older", ar.comment.GetLastCommentsByDate(-119, 99999, culture, true));
            }
            else if (dayOfWeek == "Tuesday")
            {
                week = BuildRow("Week ago", ar.comment.GetLastCommentsByDate(-2, 7, culture, true));
                week += BuildRow("Two weeks ago", ar.comment.GetLastCommentsByDate(-9, 7, culture, true));
                week += BuildRow("Three weeks ago", ar.comment.GetLastCommentsByDate(-16, 7, culture, true));
                week += BuildRow("Four weeks ago", ar.comment.GetLastCommentsByDate(-23, 7, culture, true));
                week += BuildRow("Month ago", ar.comment.GetLastCommentsByDate(-30, 30, culture, true));
                week += BuildRow("Two months ago", ar.comment.GetLastCommentsByDate(-60, 30, culture, true));
                week += BuildRow("Three months ago", ar.comment.GetLastCommentsByDate(-90, 30, culture, true));
                week += BuildRow("Older", ar.comment.GetLastCommentsByDate(-120, 99999, culture, true));
            }
            else if (dayOfWeek == "Wednesday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.comment.GetLastCommentsByDate(-2, culture, true));
                week += BuildRow("Week ago", ar.comment.GetLastCommentsByDate(-3, 7, culture, true));
                week += BuildRow("Two weeks ago", ar.comment.GetLastCommentsByDate(-10, 7, culture, true));
                week += BuildRow("Three weeks ago", ar.comment.GetLastCommentsByDate(-17, 7, culture, true));
                week += BuildRow("Four weeks ago", ar.comment.GetLastCommentsByDate(-24, 7, culture, true));
                week += BuildRow("Month ago", ar.comment.GetLastCommentsByDate(-31, 30, culture, true));
                week += BuildRow("Two months ago", ar.comment.GetLastCommentsByDate(-61, 30, culture, true));
                week += BuildRow("Three months ago", ar.comment.GetLastCommentsByDate(-91, 30, culture, true));
            }
            else if (dayOfWeek == "Thursday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.comment.GetLastCommentsByDate(-2, culture, true));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.comment.GetLastCommentsByDate(-3, culture, true));
                week += BuildRow("Week ago", ar.comment.GetLastCommentsByDate(-4, 7, culture, true));
                week += BuildRow("Two weeks ago", ar.comment.GetLastCommentsByDate(-11, 7, culture, true));
                week += BuildRow("Three weeks ago", ar.comment.GetLastCommentsByDate(-18, 7, culture, true));
                week += BuildRow("Four weeks ago", ar.comment.GetLastCommentsByDate(-25, 7, culture, true));
                week += BuildRow("Month ago", ar.comment.GetLastCommentsByDate(-32, 30, culture, true));
                week += BuildRow("Two months ago", ar.comment.GetLastCommentsByDate(-62, 30, culture, true));
                week += BuildRow("Three months ago", ar.comment.GetLastCommentsByDate(-92, 30, culture, true));
                week += BuildRow("Older", ar.comment.GetLastCommentsByDate(-122, 99999, culture, true));
            }
            else if (dayOfWeek == "Friday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.comment.GetLastCommentsByDate(-2, culture, true));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.comment.GetLastCommentsByDate(-3, culture, true));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.comment.GetLastCommentsByDate(-4, culture, true));
                week += BuildRow("Week ago", ar.comment.GetLastCommentsByDate(-5, 7, culture, true));
                week += BuildRow("Two weeks ago", ar.comment.GetLastCommentsByDate(-12, 7, culture, true));
                week += BuildRow("Three weeks ago", ar.comment.GetLastCommentsByDate(-19, 7, culture, true));
                week += BuildRow("Four weeks ago", ar.comment.GetLastCommentsByDate(-26, 7, culture, true));
                week += BuildRow("Month ago", ar.comment.GetLastCommentsByDate(-33, 30, culture, true));
                week += BuildRow("Two months ago", ar.comment.GetLastCommentsByDate(-63, 30, culture, true));
                week += BuildRow("Three months ago", ar.comment.GetLastCommentsByDate(-93, 30, culture, true));
                week += BuildRow("Older", ar.comment.GetLastCommentsByDate(-123, 99999, culture, true));
            }
            else if (dayOfWeek == "Saturday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.comment.GetLastCommentsByDate(-2, culture, true));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.comment.GetLastCommentsByDate(-3, culture, true));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.comment.GetLastCommentsByDate(-4, culture, true));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-5).ToString("dddd")), ar.comment.GetLastCommentsByDate(-5, culture, true));
                week += BuildRow("Week ago", ar.comment.GetLastCommentsByDate(-6, 7, culture, true));
                week += BuildRow("Two weeks ago", ar.comment.GetLastCommentsByDate(-13, 7, culture, true));
                week += BuildRow("Three weeks ago", ar.comment.GetLastCommentsByDate(-20, 7, culture, true));
                week += BuildRow("Four weeks ago", ar.comment.GetLastCommentsByDate(-27, 7, culture, true));
                week += BuildRow("Month ago", ar.comment.GetLastCommentsByDate(-34, 30, culture, true));
                week += BuildRow("Two months ago", ar.comment.GetLastCommentsByDate(-64, 30, culture, true));
                week += BuildRow("Three months ago", ar.comment.GetLastCommentsByDate(-94, 30, culture, true));
                week += BuildRow("Older", ar.comment.GetLastCommentsByDate(-124, 99999, culture, true));
            }
            else if (dayOfWeek == "Sunday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.comment.GetLastCommentsByDate(-2, culture, true));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.comment.GetLastCommentsByDate(-3, culture, true));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.comment.GetLastCommentsByDate(-4, culture, true));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-5).ToString("dddd")), ar.comment.GetLastCommentsByDate(-5, culture, true));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-6).ToString("dddd")), ar.comment.GetLastCommentsByDate(-6, culture, true));
                week += BuildRow("Week ago", ar.comment.GetLastCommentsByDate(-7, 7, culture, true));
                week += BuildRow("Two weeks ago", ar.comment.GetLastCommentsByDate(-14, 7, culture, true));
                week += BuildRow("Three weeks ago", ar.comment.GetLastCommentsByDate(-21, 7, culture, true));
                week += BuildRow("Month ago", ar.comment.GetLastCommentsByDate(-28, 30, culture, true));
                week += BuildRow("Two months ago", ar.comment.GetLastCommentsByDate(-58, 30, culture, true));
                week += BuildRow("Three months ago", ar.comment.GetLastCommentsByDate(-88, 30, culture, true));
                week += BuildRow("Older", ar.comment.GetLastCommentsByDate(-118, 99999, culture, true));
            }
            #endregion
            table.InnerHtml = BuildRow("Today", ar.comment.GetLastCommentsByDate(0, culture, true))
                + BuildRow("Yesterday", ar.comment.GetLastCommentsByDate(-1, culture, true)) + week;
            #endregion
            result.AppendLine(table.ToString());
            return result.ToString();
        }
        public static string TagsDetails()
        {
            IArticleRepository ar = new IArticleRepository();
            StringBuilder result = new StringBuilder();
            #region table
            TagBuilder table = new TagBuilder("table");
            foreach (string culture in LocalisationSetting.allCultureMassive())
            {
                table.InnerHtml += BuildRow(culture, ar.article.GetAllTags());
            }

            #endregion
            result.AppendLine(table.ToString());
            return result.ToString();
        }

        static string BuildRow(string title, IQueryable<mytrip_Articles> articles)
        {
            if (articles.ToList().Count() == 0)
                return string.Empty;
            TagBuilder tr = new TagBuilder("tr");
            TagBuilder td = new TagBuilder("td");
            TagBuilder accordion2 = new TagBuilder("div");
            accordion2.AddCssClass("accordion2");
            TagBuilder accordionTitle2 = new TagBuilder("div");
            accordionTitle2.AddCssClass("accordiontitle2");
            accordionTitle2.InnerHtml = "<b>" + title + "</b>";
            TagBuilder accordionContent2 = new TagBuilder("div");
            accordionContent2.AddCssClass("accordioncontent2");
            int ctr = 0;
            foreach (var article in articles)
            {
                TagBuilder div = new TagBuilder("div");
                if (ctr % 2 == 0)
                    div.MergeAttribute("style", "background-color:#F7F5F5;");
                else
                    div.MergeAttribute("style", "background-color:#EDEBEB;");
                TagBuilder alink = new TagBuilder("a");
                alink.MergeAttribute("href", LangLink(article.Culture,"/Article/View/" + article.ArticleId + "/" + article.Path));
                alink.InnerHtml = article.Title;
                TagBuilder clink = new TagBuilder("a");
                clink.MergeAttribute("href", LangLink(article.Culture,"/Article/Index/1/10/" + article.mytrip_ArticlesCategory.CategoryId + "/" + article.mytrip_ArticlesCategory.Path));
                clink.InnerHtml = article.mytrip_ArticlesCategory.Title;
                div.InnerHtml = EditDelete(article)+" <b>" + alink + " " + Keys(article.OnlyForRegisterUser, 14) + " " + Globe(article.AllCulture, 14) + " " + Flag(article.Culture, 14)
                    + "</b><br/>" + article.CreateDate.ToString("dd MMMM yyyy HH:mm") + " in " + clink + " by " + article.UserName;
                accordionContent2.InnerHtml += div;
                ctr++;
            }
            accordion2.InnerHtml = accordionTitle2.ToString() + accordionContent2.ToString();
            td.InnerHtml = accordion2.ToString();
            tr.InnerHtml += td.ToString();
            return tr.ToString();
        }
        static string BuildRow(string title, IQueryable<mytrip_ArticlesCategory> categories)
        {
            if (categories.ToList().Count() == 0)
                return string.Empty;
            TagBuilder tr = new TagBuilder("tr");
            TagBuilder td = new TagBuilder("td");
            TagBuilder accordion2 = new TagBuilder("div");
            accordion2.AddCssClass("accordion2");
            TagBuilder accordionTitle2 = new TagBuilder("div");
            accordionTitle2.AddCssClass("accordiontitle2");
            accordionTitle2.InnerHtml = "<b>" + title + "</b>";
            TagBuilder accordionContent2 = new TagBuilder("div");
            accordionContent2.AddCssClass("accordioncontent2");
            int ctr = 0;
            foreach (var category in categories)
            {
                TagBuilder div = new TagBuilder("div");
                if (ctr % 2 == 0)
                    div.MergeAttribute("style", "background-color:#F7F5F5;");
                else
                    div.MergeAttribute("style", "background-color:#EDEBEB;");
                TagBuilder link = new TagBuilder("a");
                link.MergeAttribute("href", LangLink(category.Culture,"/Article/Index/1/10/" + category.CategoryId + "/" + category.Path));
                link.InnerHtml = category.Title;
                div.InnerHtml += EditDelete(category) + " <b>" + link + " " + Flag(category.Culture, 14) + "</b><br/>" + category.CreateDate.ToString("dd MMMM yyyy HH:mm")
                    +SubCatLink(category)+" by " + category.UserName;
                accordionContent2.InnerHtml += div;
                ctr++;
            }
            accordion2.InnerHtml = accordionTitle2.ToString() + accordionContent2.ToString();
            td.InnerHtml = accordion2.ToString();
            tr.InnerHtml += td.ToString();
            return tr.ToString();
        }
        static string BuildRow(string title, IQueryable<mytrip_ArticlesComments> comments)
        {
            if (comments.ToList().Count() == 0)
                return string.Empty;
            TagBuilder tr = new TagBuilder("tr");
            TagBuilder td = new TagBuilder("td");
            TagBuilder accordion2 = new TagBuilder("div");
            accordion2.AddCssClass("accordion2");
            TagBuilder accordionTitle2 = new TagBuilder("div");
            accordionTitle2.AddCssClass("accordiontitle2");
            accordionTitle2.InnerHtml = "<b>" + title + "</b>";
            TagBuilder accordionContent2 = new TagBuilder("div");
            accordionContent2.AddCssClass("accordioncontent2");
            int ctr = 0;
            foreach (var comment in comments)
            {
                TagBuilder div = new TagBuilder("div");
                if (ctr % 2 == 0)
                    div.MergeAttribute("style", "background-color:#F7F5F5;");
                else
                    div.MergeAttribute("style", "background-color:#EDEBEB;");
                TagBuilder link = new TagBuilder("a");
                link.MergeAttribute("href", LangLink(comment.mytrip_Articles.Culture,"/Article/View/" + comment.mytrip_Articles.ArticleId + "/" + comment.mytrip_Articles.Path));
                link.InnerHtml = comment.mytrip_Articles.Title;
                div.InnerHtml += EditDelete(comment) + comment.CreateDate.ToString("dd MMMM yyyy HH:mm:ss") + " added by " + comment.UserName + " in <b>" + link + " " 
                    + Globe(comment.mytrip_Articles.AllCulture, 14) + " " + Flag(comment.mytrip_Articles.Culture, 14) + "</b>";
                accordionContent2.InnerHtml += div;
                ctr++;
            }
            accordion2.InnerHtml = accordionTitle2.ToString() + accordionContent2.ToString();
            td.InnerHtml = accordion2.ToString();
            tr.InnerHtml += td.ToString();
            return tr.ToString();
        }
        static string BuildRow(string title, IQueryable<mytrip_ArticlesTag> tags)
        {
            if (tags.ToList().Count() == 0)
                return string.Empty;
            IArticleRepository ar = new IArticleRepository();
            TagBuilder tr = new TagBuilder("tr");
            TagBuilder td = new TagBuilder("td");
            TagBuilder accordion2 = new TagBuilder("div");
            accordion2.AddCssClass("accordion2");
            TagBuilder accordionTitle2 = new TagBuilder("div");
            accordionTitle2.AddCssClass("accordiontitle2");
            accordionTitle2.InnerHtml = Flag(title.ToLower(), 15);
            TagBuilder accordionContent2 = new TagBuilder("div");
            accordionContent2.AddCssClass("accordioncontent2");
            TagBuilder div = new TagBuilder("div");
            div.MergeAttribute("style", "background-color:#F7F5F5;");
            foreach (var tag in tags)
            {
                TagBuilder link = new TagBuilder("a");
                link.MergeAttribute("href", LangLink(title,"/Article/Index/1/10/" + tag.TagId + "/" + tag.Path));
                link.InnerHtml = tag.TagName;
                div.InnerHtml += EditDelete(tag) + "  <b>" + link + "</b>(" + ar.article.GetArticlesInTagsCount(tag.TagId, title.ToLower()) + ")   ";
            }
            accordionContent2.InnerHtml += div;
            accordion2.InnerHtml = accordionTitle2.ToString() + accordionContent2.ToString();
            td.InnerHtml = accordion2.ToString();
            tr.InnerHtml += td.ToString();
            return tr.ToString();
        }
        static string LangLink(string culture, string url)
        {
            culture = culture.ToLower();
            if (culture == HttpContext.Current.Session["culture"].ToString().ToLower())
            return url;
            return "/Language/Index/" + culture +"/"+url.Replace("/","(x)");
        }
        static string SubCatLink(mytrip_ArticlesCategory category)
        {
            if (category.CategoryId == category.SubCategoryId)
                return string.Empty;
            TagBuilder link = new TagBuilder("a");
            link.MergeAttribute("href", LangLink(category.Culture, "/Article/Index/1/10/" + category.SubCategoryId + "/" + category.mytrip_ArticlesCategory2.Path));
            link.InnerHtml = category.mytrip_ArticlesCategory2.Title;
            return " in "+link.ToString()+" ";
        }
        static string UppercaseFirst(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }

        static string Flag(string culture, int width)
        {
            culture = culture.ToLower();
            TagBuilder img = new TagBuilder("img");
            img.MergeAttribute("src", "/Content/images/" + culture + ".png");
            img.MergeAttribute("style", "border-width:0px;width:" + width + "px");
            img.MergeAttribute("alt", culture);
            img.MergeAttribute("title", culture);
            return img.ToString();
        }
        static string Globe(bool show, int width)
        {
            if (!show)
                return string.Empty;
            TagBuilder img = new TagBuilder("img");
            img.MergeAttribute("src", "/Content/images/globe.png");
            img.MergeAttribute("style", "border-width:0px;width:" + width + "px");
            img.MergeAttribute("alt", "All Languages");
            img.MergeAttribute("title", "All Languages");
            return img.ToString();
        }
        static string Keys(bool show, int width)
        {
            if (!show)
                return string.Empty;
            TagBuilder img = new TagBuilder("img");
            img.MergeAttribute("src", "/Content/images/Keys.png");
            img.MergeAttribute("style", "border-width:0px;width:" + width + "px");
            img.MergeAttribute("alt", "Only for registered");
            img.MergeAttribute("title", "Only for registered");
            return img.ToString();
        }
        static string EditDelete(object obj)
        {
            string linkEdit = "";
            string linkDelete = "";
            if (obj is mytrip_ArticlesCategory)  
            {
                mytrip_ArticlesCategory category = obj as mytrip_ArticlesCategory;
                linkEdit = "/Article/EditCategory/" + category.CategoryId + "/Archive/";
                linkDelete = "/Article/DeleteCategory/" + category.CategoryId + "/Archive/";
            }
            else if (obj is mytrip_Articles)
            {
                mytrip_Articles article = obj as mytrip_Articles;
                linkEdit = "/Article/Edit/" + article.ArticleId + "/Archive/";
                linkDelete = "/Article/Delete/" + article.ArticleId + "/Archive/";
            }
            else if (obj is mytrip_ArticlesComments)
            {
                mytrip_ArticlesComments comment = obj as mytrip_ArticlesComments;
                linkEdit = "/Article/EditComment/" + comment.CommentId + "/Archive/";
                linkDelete = "/Article/DeleteComment/" + comment.CommentId + "/Archive/";
            }
            else if (obj is mytrip_ArticlesTag)
            {
                mytrip_ArticlesTag tag = obj as mytrip_ArticlesTag;
                linkEdit = "/Article/EditCategory/" + tag.TagId + "/(Tag)Archive/";
                linkDelete = "/Article/DeleteCategory/" + tag.TagId + "/(Tag)Archive/";
            }
            linkEdit += HttpContext.Current.Request.Path.Replace("/", "(x)");
            linkDelete += HttpContext.Current.Request.Path.Replace("/", "(x)");
            TagBuilder imgEdit = new TagBuilder("img");
            imgEdit.MergeAttribute("src", "/Content/images/edite.png");
            imgEdit.MergeAttribute("title", ArticleLanguage.edit);
            imgEdit.MergeAttribute("style", "width:14px;border:0px;");
            TagBuilder imgDelete = new TagBuilder("img");
            imgDelete.MergeAttribute("src", "/Content/images/delete.png");
            imgDelete.MergeAttribute("title", ArticleLanguage.delete);
            imgDelete.MergeAttribute("style", "width:14px;border:0px;");
            TagBuilder EditCategory = new TagBuilder("a");
            EditCategory.MergeAttribute("href", linkEdit);
            EditCategory.InnerHtml = imgEdit.ToString();
            TagBuilder DeleteCategory = new TagBuilder("a");
            DeleteCategory.MergeAttribute("href", linkDelete);
            DeleteCategory.MergeAttribute("onclick", "return confirm ('" + ArticleLanguage.are_you_sure + "');");
            DeleteCategory.InnerHtml = imgDelete.ToString();

            return EditCategory.ToString() +" "+ DeleteCategory.ToString();
        }
    }
}
