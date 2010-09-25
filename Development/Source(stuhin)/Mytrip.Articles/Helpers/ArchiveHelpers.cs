using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Mytrip.Mvc;
using Mytrip.Articles.Repository;
using System.Web;
using Mytrip.Articles.Repository.DataEntities;
using Mytrip.Mvc.Settings;

namespace Mytrip.Articles.Helpers
{
    public static class ArchiveHelpers
    {
        #region Index page helpers
        public static HtmlString ArchiveStatistic(this HtmlHelper html)
        {
            IArticleRepository ar = new IArticleRepository();
            StringBuilder result = new StringBuilder();
            #region table
            TagBuilder table = new TagBuilder("table");
            table.MergeAttribute("style", "text-align:center");
            #region Header row
            TagBuilder tr1 = new TagBuilder("tr");
            TagBuilder th11 = new TagBuilder("th");
            th11.MergeAttribute("style", "text-align:center;width:15%");
            th11.SetInnerText(ArticleLanguage.statistic);
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
            th13.InnerHtml = Globe(true, 18);
            tr1.InnerHtml += th13.ToString();
            #endregion
            #region Categories or Subcategories row
            TagBuilder tr2 = new TagBuilder("tr");
            TagBuilder td21 = new TagBuilder("td");
            TagBuilder a2 = new TagBuilder("a");
            a2.MergeAttribute("href", "/Article/ArchiveDetails/Categories/");
            a2.InnerHtml = ArticleLanguage.categories_and_subcategories;
            td21.InnerHtml = a2.ToString();
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
            a3.MergeAttribute("href", "/Article/ArchiveDetails/Articles/");
            a3.InnerHtml = ArticleLanguage.articles;
            td31.InnerHtml = a3.ToString();
            tr3.InnerHtml = td31.ToString();
            total = 0;
            foreach (string culture in LocalisationSetting.allCultureMassive())
            {
                TagBuilder td32 = new TagBuilder("td");
                int count = ar.article.GetCount(culture, false);
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
            a4.MergeAttribute("href", "/Article/ArchiveDetails/Comments/");
            a4.InnerHtml = ArticleLanguage.comments;
            td41.InnerHtml = a4.ToString();
            tr4.InnerHtml = td41.ToString();
            total = 0;
            foreach (string culture in LocalisationSetting.allCultureMassive())
            {
                TagBuilder td42 = new TagBuilder("td");
                int count = ar.comment.GetCount(culture, CommentType.Articles);
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
            TagBuilder th51 = new TagBuilder("th");
            th51.MergeAttribute("colspan", LocalisationSetting.allCultureMassive().Count().ToString() + 2);
            tr5.InnerHtml = th51.ToString();
            #endregion
            #region Blogs row
            TagBuilder tr6 = new TagBuilder("tr");
            TagBuilder td61 = new TagBuilder("td");
            TagBuilder a6 = new TagBuilder("a");
            a6.MergeAttribute("href", "/Article/ArchiveDetails/Blogs/");
            a6.InnerHtml = ArticleLanguage.blogs;
            td61.InnerHtml = a6.ToString();
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
            a7.MergeAttribute("href", "/Article/ArchiveDetails/Topics/");
            a7.InnerHtml = ArticleLanguage.topics;
            td71.InnerHtml = a7.ToString();
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
            a8.MergeAttribute("href", "/Article/ArchiveDetails/Posts/");
            a8.InnerHtml = ArticleLanguage.posts;
            td81.InnerHtml = a8.ToString();
            tr8.InnerHtml = td81.ToString();
            total = 0;
            foreach (string culture in LocalisationSetting.allCultureMassive())
            {
                TagBuilder td82 = new TagBuilder("td");
                int count = ar.article.GetCount(culture, true);
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
            a9.MergeAttribute("href", "/Article/ArchiveDetails/BlogsComments/");
            a9.InnerHtml = ArticleLanguage.comments_in_blogs;
            td91.InnerHtml = a9.ToString();
            tr9.InnerHtml = td91.ToString();
            total = 0;
            foreach (string culture in LocalisationSetting.allCultureMassive())
            {
                TagBuilder td92 = new TagBuilder("td");
                int count = ar.comment.GetCount(culture, CommentType.Blogs);
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
            TagBuilder th101 = new TagBuilder("th");
            th101.MergeAttribute("colspan", LocalisationSetting.allCultureMassive().Count().ToString() + 2);
            tr10.InnerHtml = th101.ToString();
            #endregion
            #region Tags row
            TagBuilder tr11 = new TagBuilder("tr");
            TagBuilder td111 = new TagBuilder("td");
            TagBuilder a11 = new TagBuilder("a");
            a11.MergeAttribute("href", "/Article/ArchiveDetails/Tags/");
            a11.InnerHtml = ArticleLanguage.tags1;
            td111.InnerHtml = a11.ToString();
            tr11.InnerHtml = td111.ToString();
            TagBuilder td112 = new TagBuilder("td");
            td112.MergeAttribute("colspan", LocalisationSetting.allCultureMassive().Count().ToString());
            td112.AddCssClass("profile1");
            tr11.InnerHtml += td112.ToString();
            TagBuilder td113 = new TagBuilder("td");
            td113.SetInnerText(ar.article.GetTagsCount().ToString());
            tr11.InnerHtml += td113.ToString();
            #endregion
            #region Unapproved Comments row
            TagBuilder tr12 = new TagBuilder("tr");
            TagBuilder td121 = new TagBuilder("td");
            TagBuilder a12 = new TagBuilder("a");
            a12.MergeAttribute("href", "/Article/ArchiveDetails/UnapprovedComments/");
            a12.InnerHtml = ArticleLanguage.unapproved_comments;
            td121.InnerHtml = a12.ToString();
            tr12.InnerHtml = td121.ToString();
            total = 0;
            foreach (string culture in LocalisationSetting.allCultureMassive())
            {
                TagBuilder td122 = new TagBuilder("td");
                int count = ar.comment.GetCount(culture, CommentType.Unapproved);
                td122.SetInnerText(count.ToString());
                tr12.InnerHtml += td122.ToString();
                total += count;
            }
            TagBuilder td123 = new TagBuilder("td");
            td123.SetInnerText(total.ToString());
            tr12.InnerHtml += td123.ToString();
            #endregion
            #region Closed Articles row
            TagBuilder tr13 = new TagBuilder("tr");
            TagBuilder td131 = new TagBuilder("td");
            TagBuilder a13 = new TagBuilder("a");
            a13.MergeAttribute("href", "/Article/ArchiveDetails/ClosedArticles/");
            a13.InnerHtml = ArticleLanguage.closed_articles;
            td131.InnerHtml = a13.ToString();
            tr13.InnerHtml = td131.ToString();
            total = 0;
            foreach (string culture in LocalisationSetting.allCultureMassive())
            {
                TagBuilder td132 = new TagBuilder("td");
                int count = ar.article.GetClosedCount(culture);
                td132.SetInnerText(count.ToString());
                tr13.InnerHtml += td132.ToString();
                total += count;
            }
            TagBuilder td133 = new TagBuilder("td");
            td133.SetInnerText(total.ToString());
            tr13.InnerHtml += td133.ToString();
            #endregion
            //Insert rows into table
            table.InnerHtml = tr1.ToString() + tr2.ToString() + tr3.ToString() + tr4.ToString() + tr5.ToString() + tr6.ToString()
                + tr7.ToString() + tr8.ToString() + tr9.ToString() + tr10.ToString() + tr11.ToString() + tr12.ToString() + tr13.ToString();
            #endregion
            result.AppendLine(table.ToString());
            return new HtmlString(result.ToString());
        }
        public static HtmlString LatestUpdates(this HtmlHelper html, int count)
        {
            IArticleRepository ar = new IArticleRepository();
            StringBuilder result = new StringBuilder();
            #region table
            TagBuilder table = new TagBuilder("table");
            #region Articles Header row
            TagBuilder tr1 = new TagBuilder("tr");
            TagBuilder th11 = new TagBuilder("th");
            th11.MergeAttribute("style", "text-align:center;width:15%;");
            th11.SetInnerText(ArticleLanguage.articles);
            TagBuilder th12 = new TagBuilder("th");
            th12.MergeAttribute("style", "text-align:center");
            tr1.InnerHtml += th11.ToString() + th12.ToString();
            #endregion
            #region Categories or Subcategories row
            TagBuilder tr2 = new TagBuilder("tr");
            TagBuilder td21 = new TagBuilder("td");
            TagBuilder a2 = new TagBuilder("a");
            a2.MergeAttribute("href", "/Article/ArchiveDetails/Categories/");
            a2.InnerHtml = ArticleLanguage.categories_and_subcategories;
            td21.InnerHtml = a2.ToString();
            td21.MergeAttribute("style", "text-align:center");
            tr2.InnerHtml = td21.ToString();
            TagBuilder td22 = new TagBuilder("td");
            int ctr = 0;
            foreach (var cat in ar.category.GetLastCategories(count))
            {
                TagBuilder div = new TagBuilder("div");
                if (ctr % 2 == 0)
                    div.AddCssClass("profile1");
                else
                    div.AddCssClass("profile2");
                TagBuilder link = new TagBuilder("a");
                link.MergeAttribute("href", LangLink(cat.Culture, "/Article/Index/1/10/" + cat.CategoryId + "/" + cat.Path));
                // tooltip
                link.MergeAttribute("rel", "Category," + cat.CategoryId);
                link.AddCssClass("mtPopupTrigger");
                //
                link.InnerHtml = cat.Title;
                div.InnerHtml += "<b>" + link + " " + Globe(cat.AllCulture, 14) + " " + Flag(cat.Culture, 14)
                    + "</b><br/>" + cat.CreateDate.ToString("dd MMMM yyyy HH:mm") + SubCatLink(cat) + " " + ArticleLanguage.added_by + " " + ProfileLink(cat.UserName);
                td22.InnerHtml += div;
                ctr++;
            }
            tr2.InnerHtml += td22.ToString();
            #endregion
            #region Articles row
            TagBuilder tr3 = new TagBuilder("tr");
            TagBuilder td31 = new TagBuilder("td");
            TagBuilder a3 = new TagBuilder("a");
            a3.MergeAttribute("href", "/Article/ArchiveDetails/Articles/");
            a3.InnerHtml = ArticleLanguage.articles;
            td31.InnerHtml = a3.ToString();
            td31.MergeAttribute("style", "text-align:center");
            tr3.InnerHtml = td31.ToString();
            TagBuilder td32 = new TagBuilder("td");
            ctr = 0;
            foreach (var article in ar.article.GetLastArticles(count))
            {
                TagBuilder div = new TagBuilder("div");
                if (ctr % 2 == 0)
                    div.AddCssClass("profile1");
                else
                    div.AddCssClass("profile2");
                TagBuilder alink = new TagBuilder("a");
                alink.MergeAttribute("href", LangLink(article.Culture, "/Article/View/" + article.ArticleId + "/" + article.Path));
                // tooltip
                alink.MergeAttribute("rel","Article,"+article.ArticleId);
                alink.AddCssClass("mtPopupTrigger");
                //
                alink.InnerHtml = article.Title;
                TagBuilder clink = new TagBuilder("a");
                clink.MergeAttribute("href", LangLink(article.Culture, "/Article/Index/1/10/" + article.mytrip_articlescategory.CategoryId 
                    + "/" + article.mytrip_articlescategory.Path));
                // tooltip
                clink.MergeAttribute("rel", "Category," + article.mytrip_articlescategory.CategoryId);
                clink.AddCssClass("mtPopupTrigger");
                //
                clink.InnerHtml = article.mytrip_articlescategory.Title;
                div.InnerHtml = "<b>" + alink + " " + Keys(article.OnlyForRegisterUser, 14) + " " + Globe(article.AllCulture, 14) + " " + Flag(article.Culture, 14)
                    + "</b><br/>" + article.CreateDate.ToString("dd MMMM yyyy HH:mm") + " " + ArticleLanguage.in_the + " " + clink + " " + ArticleLanguage.by 
                    + " " + ProfileLink(article.UserName);
                td32.InnerHtml += div;
                ctr++;
            }
            tr3.InnerHtml += td32.ToString();
            #endregion
            #region Comments row
            TagBuilder tr4 = new TagBuilder("tr");
            TagBuilder td41 = new TagBuilder("td");
            TagBuilder a4 = new TagBuilder("a");
            a4.MergeAttribute("href", "/Article/ArchiveDetails/Comments/");
            a4.InnerHtml = ArticleLanguage.comments;
            td41.InnerHtml = a4.ToString();
            td41.MergeAttribute("style", "text-align:center");
            tr4.InnerHtml = td41.ToString();
            TagBuilder td42 = new TagBuilder("td");
            ctr = 0;
            foreach (var comment in ar.comment.GetComments(CommentType.Articles, count))
            {
                TagBuilder div = new TagBuilder("div");
                if (ctr % 2 == 0)
                    div.AddCssClass("profile1");
                else
                    div.AddCssClass("profile2");
                TagBuilder link = new TagBuilder("a");
                link.MergeAttribute("href", LangLink(comment.mytrip_articles.Culture, "/Article/View/" + comment.mytrip_articles.ArticleId 
                    + "/" + comment.mytrip_articles.Path + "#" + comment.CommentId));
                // tooltip
                link.MergeAttribute("rel", "Comment," + comment.CommentId);
                link.AddCssClass("mtPopupTrigger");
                //
                link.InnerHtml = comment.mytrip_articles.Title;
                div.InnerHtml += comment.CreateDate.ToString("dd MMMM yyyy HH:mm:ss") + " " + ArticleLanguage.in_the + " <b>" + link + " "
                    + Globe(comment.mytrip_articles.AllCulture, 14) + " " + Flag(comment.mytrip_articles.Culture, 14) + "</b> " 
                    + ArticleLanguage.added_by + " " + ProfileLink(comment.UserName, comment.IsAnonym);
                td42.InnerHtml += div;
                ctr++;
            }
            tr4.InnerHtml += td42.ToString();
            #endregion
            #region Blogs Header row
            TagBuilder tr5 = new TagBuilder("tr");
            TagBuilder th51 = new TagBuilder("th");
            th51.MergeAttribute("style", "text-align:center;");
            th51.SetInnerText(ArticleLanguage.blogs);
            TagBuilder th52 = new TagBuilder("th");
            th52.MergeAttribute("style", "text-align:center");
            tr5.InnerHtml += th51.ToString() + th52.ToString();
            #endregion
            #region Blogs row
            TagBuilder tr6 = new TagBuilder("tr");
            TagBuilder td61 = new TagBuilder("td");
            TagBuilder a6 = new TagBuilder("a");
            a6.MergeAttribute("href", "/Article/ArchiveDetails/Blogs/");
            a6.InnerHtml = ArticleLanguage.blogs;
            td61.InnerHtml = a6.ToString();
            td61.MergeAttribute("style", "text-align:center");
            tr6.InnerHtml = td61.ToString();
            TagBuilder td62 = new TagBuilder("td");
            ctr = 0;
            foreach (var blog in ar.category.GetLastBlogs(count))
            {
                TagBuilder div = new TagBuilder("div");
                if (ctr % 2 == 0)
                    div.AddCssClass("profile1");
                else
                    div.AddCssClass("profile2");
                TagBuilder link = new TagBuilder("a");
                link.MergeAttribute("href", LangLink(blog.Culture, "/Article/Index/1/10/" + blog.CategoryId + "/" + blog.Path));
                // tooltip
                link.MergeAttribute("rel", "Category," + blog.CategoryId);
                link.AddCssClass("mtPopupTrigger");
                //
                link.InnerHtml = blog.Title;
                div.InnerHtml += "<b>" + link + " " + Flag(blog.Culture, 14) + "</b><br/>" + blog.CreateDate.ToString("dd MMMM yyyy HH:mm")
                    + " " + ArticleLanguage.by + " " + ProfileLink(blog.UserName);
                td62.InnerHtml += div;
                ctr++;
            }
            tr6.InnerHtml += td62.ToString();
            #endregion
            #region Topics row
            TagBuilder tr7 = new TagBuilder("tr");
            TagBuilder td71 = new TagBuilder("td");
            TagBuilder a7 = new TagBuilder("a");
            a7.MergeAttribute("href", "/Article/ArchiveDetails/Topics/");
            a7.InnerHtml = ArticleLanguage.topics;
            td71.InnerHtml = a7.ToString();
            td71.MergeAttribute("style", "text-align:center");
            tr7.InnerHtml = td71.ToString();
            TagBuilder td72 = new TagBuilder("td");
            ctr = 0;
            foreach (var topic in ar.category.GetLastTopics(count))
            {
                TagBuilder div = new TagBuilder("div");
                if (ctr % 2 == 0)
                    div.AddCssClass("profile1");
                else
                    div.AddCssClass("profile2");
                TagBuilder link = new TagBuilder("a");
                link.MergeAttribute("href", LangLink(topic.Culture, "/Article/Index/1/10/" + topic.CategoryId + "/" + topic.Path));
                // tooltip
                link.MergeAttribute("rel", "Category," + topic.CategoryId);
                link.AddCssClass("mtPopupTrigger");
                //
                link.InnerHtml = topic.Title;
                div.InnerHtml += "<b>" + link + " " + Globe(topic.AllCulture, 14) + " " + Flag(topic.Culture, 14)
                    + "</b><br/>" + topic.CreateDate.ToString("dd MMMM yyyy HH:mm") + SubCatLink(topic) + " " 
                    + ArticleLanguage.added_by + " " + ProfileLink(topic.UserName);
                td72.InnerHtml += div;
                ctr++;
            }
            tr7.InnerHtml += td72.ToString();
            #endregion
            #region Posts row
            TagBuilder tr8 = new TagBuilder("tr");
            TagBuilder td81 = new TagBuilder("td");
            TagBuilder a8 = new TagBuilder("a");
            a8.MergeAttribute("href", "/Article/ArchiveDetails/Posts/");
            a8.InnerHtml = ArticleLanguage.posts;
            td81.InnerHtml = a8.ToString();
            td81.MergeAttribute("style", "text-align:center");
            tr8.InnerHtml = td81.ToString();
            TagBuilder td82 = new TagBuilder("td");
            ctr = 0;
            foreach (var post in ar.article.GetLastPosts(count))
            {
                TagBuilder div = new TagBuilder("div");
                if (ctr % 2 == 0)
                    div.AddCssClass("profile1");
                else
                    div.AddCssClass("profile2");
                TagBuilder link = new TagBuilder("a");
                link.MergeAttribute("href", LangLink(post.Culture, "/Article/View/" + post.ArticleId + "/" + post.Path));
                // tooltip
                link.MergeAttribute("rel", "Article," + post.ArticleId);
                link.AddCssClass("mtPopupTrigger");
                //
                link.InnerHtml = post.Title;
                TagBuilder clink = new TagBuilder("a");
                clink.MergeAttribute("href", LangLink(post.Culture, "/Article/Index/1/10/" + post.mytrip_articlescategory.CategoryId
                    + "/" + post.mytrip_articlescategory.Path));
                // tooltip
                clink.MergeAttribute("rel", "Category," + post.mytrip_articlescategory.CategoryId);
                clink.AddCssClass("mtPopupTrigger");
                //
                clink.InnerHtml = post.mytrip_articlescategory.Title;
                div.InnerHtml += "<b>" + link + " " + Flag(post.Culture, 14) + "</b><br/>" + post.CreateDate.ToString("dd MMMM yyyy HH:mm") + " " + ArticleLanguage.in_the + " " + clink
                    + " " + ArticleLanguage.by + " " + ProfileLink(post.UserName);
                td82.InnerHtml += div;
                ctr++;
            }
            tr8.InnerHtml += td82.ToString();
            #endregion
            #region Comments row
            TagBuilder tr9 = new TagBuilder("tr");
            TagBuilder td91 = new TagBuilder("td");
            TagBuilder a9 = new TagBuilder("a");
            a9.MergeAttribute("href", "/Article/ArchiveDetails/BlogsComments/");
            a9.InnerHtml = ArticleLanguage.comments_in_blogs;
            td91.InnerHtml = a9.ToString();
            td91.MergeAttribute("style", "text-align:center");
            tr9.InnerHtml = td91.ToString();
            TagBuilder td92 = new TagBuilder("td");
            ctr = 0;
            foreach (var comment in ar.comment.GetComments(CommentType.Blogs, count))
            {
                TagBuilder div = new TagBuilder("div");
                if (ctr % 2 == 0)
                    div.AddCssClass("profile1");
                else
                    div.AddCssClass("profile2");
                TagBuilder link = new TagBuilder("a");
                link.MergeAttribute("href", LangLink(comment.mytrip_articles.Culture, "/Article/View/" + comment.mytrip_articles.ArticleId 
                    + "/" + comment.mytrip_articles.Path + "#" + comment.CommentId));
                // tooltip
                link.MergeAttribute("rel", "Comment," + comment.CommentId);
                link.AddCssClass("mtPopupTrigger");
                //
                link.InnerHtml = comment.mytrip_articles.Title;
                div.InnerHtml += comment.CreateDate.ToString("dd MMMM yyyy HH:mm:ss") + " " + ArticleLanguage.in_the + " <b>" + link + " "
                    + Globe(comment.mytrip_articles.AllCulture, 14) + " " + Flag(comment.mytrip_articles.Culture, 14) + "</b> "
                    + ArticleLanguage.added_by + " " + ProfileLink(comment.UserName, comment.IsAnonym);
                td92.InnerHtml += div;
                ctr++;
            }
            tr9.InnerHtml += td92.ToString();
            #endregion
            #region Other Header row
            TagBuilder tr10 = new TagBuilder("tr");
            TagBuilder th101 = new TagBuilder("th");
            th101.MergeAttribute("style", "text-align:center;");
            th101.SetInnerText(ArticleLanguage.other);
            TagBuilder th102 = new TagBuilder("th");
            th102.MergeAttribute("style", "text-align:center");
            tr10.InnerHtml += th101.ToString() + th102.ToString();
            #endregion
            #region Tags row
            TagBuilder tr11 = new TagBuilder("tr");
            TagBuilder td111 = new TagBuilder("td");
            TagBuilder a11 = new TagBuilder("a");
            a11.MergeAttribute("href", "/Article/ArchiveDetails/Tags/");
            a11.InnerHtml = ArticleLanguage.tags1;
            td111.InnerHtml = a11.ToString();
            td111.MergeAttribute("style", "text-align:center");
            tr11.InnerHtml = td111.ToString();
            TagBuilder td112 = new TagBuilder("td");
            ctr = 0;
            foreach (string culture in LocalisationSetting.allCultureMassive())
            {
                TagBuilder div = new TagBuilder("div");
                if (ctr % 2 == 0)
                    div.AddCssClass("profile1");
                else
                    div.AddCssClass("profile2");
                div.InnerHtml += Flag(culture, 14);
                foreach (var tag in ar.article.GetAllTags().ToList())
                {
                    TagBuilder link = new TagBuilder("a");
                    link.MergeAttribute("href", LangLink(culture, "/Article/Index/1/10/" + tag.TagId + "/" + tag.Path));
                    link.InnerHtml = tag.TagName;
                    div.InnerHtml += " <b>" + link + "</b>(" + ar.article.GetInTagCount(culture, tag.TagId) + ") ";
                }
                td112.InnerHtml += div;
                ctr++;
            }
            tr11.InnerHtml += td112.ToString();
            #endregion
            #region Unapproved Comments row
            TagBuilder tr12 = new TagBuilder("tr");
            TagBuilder td121 = new TagBuilder("td");
            TagBuilder a12 = new TagBuilder("a");
            a12.MergeAttribute("href", "/Article/ArchiveDetails/UnapprovedComments/");
            a12.InnerHtml = ArticleLanguage.unapproved_comments;
            td121.InnerHtml = a12.ToString();
            td121.MergeAttribute("style", "text-align:center");
            tr12.InnerHtml = td121.ToString();
            TagBuilder td122 = new TagBuilder("td");
            ctr = 0;
            foreach (var comment in ar.comment.GetComments(CommentType.Unapproved, count))
            {
                TagBuilder div = new TagBuilder("div");
                if (ctr % 2 == 0)
                    div.AddCssClass("profile1");
                else
                    div.AddCssClass("profile2");
                TagBuilder link = new TagBuilder("a");
                link.MergeAttribute("href", LangLink(comment.mytrip_articles.Culture, "/Article/View/" + comment.mytrip_articles.ArticleId
                    + "/" + comment.mytrip_articles.Path + "#" + comment.CommentId));
                // tooltip
                link.MergeAttribute("rel", "Comment," + comment.CommentId);
                link.AddCssClass("mtPopupTrigger");
                //
                link.InnerHtml = comment.mytrip_articles.Title;
                div.InnerHtml += comment.CreateDate.ToString("dd MMMM yyyy HH:mm:ss") + " " + ArticleLanguage.in_the + " <b>" + link + " "
                    + Globe(comment.mytrip_articles.AllCulture, 14) + " " + Flag(comment.mytrip_articles.Culture, 14) + "</b> " 
                    + ArticleLanguage.added_by + " " + ProfileLink(comment.UserName, comment.IsAnonym);
                td122.InnerHtml += div;
                ctr++;
            }
            tr12.InnerHtml += td122.ToString();
            #endregion
            table.InnerHtml = tr1.ToString() + tr2.ToString() + tr3.ToString() + tr4.ToString() + tr5.ToString() + tr6.ToString()
                + tr7.ToString() + tr8.ToString() + tr9.ToString() + tr10.ToString() + tr11.ToString() + tr12.ToString();
            #endregion
            result.AppendLine(table.ToString());
            return new HtmlString(result.ToString());
        }
        public static HtmlString ClosedArticles(this HtmlHelper html, int count)
        {
            IArticleRepository ar = new IArticleRepository();
            StringBuilder result = new StringBuilder();
            #region table
            TagBuilder table = new TagBuilder("table");
            #region Header row
            TagBuilder tr1 = new TagBuilder("tr");
            TagBuilder th11 = new TagBuilder("th");
            th11.MergeAttribute("style", "text-align:center");
            tr1.InnerHtml = th11.ToString();
            #endregion
            #region Closed Articles row
            TagBuilder tr2 = new TagBuilder("tr");
            TagBuilder td21 = new TagBuilder("td");
            int ctr = 0;
            foreach (var article in ar.article.GetArticlesClosed(count))
            {
                TagBuilder div = new TagBuilder("div");
                if (ctr % 2 == 0)
                    div.AddCssClass("profile1");
                else
                    div.AddCssClass("profile2");
                TagBuilder alink = new TagBuilder("a");
                alink.MergeAttribute("href", LangLink(article.Culture, "/Article/View/" + article.ArticleId + "/" + article.Path));
                // tooltip
                alink.MergeAttribute("rel", "Article," + article.ArticleId);
                alink.AddCssClass("mtPopupTrigger");
                //
                alink.InnerHtml = article.Title;
                TagBuilder clink = new TagBuilder("a");
                clink.MergeAttribute("href", LangLink(article.Culture, "/Article/Index/1/10/" + article.mytrip_articlescategory.CategoryId 
                    + "/" + article.mytrip_articlescategory.Path));
                // tooltip
                clink.MergeAttribute("rel", "Category," + article.mytrip_articlescategory.CategoryId);
                clink.AddCssClass("mtPopupTrigger");
                //
                clink.InnerHtml = article.mytrip_articlescategory.Title;
                div.InnerHtml += "<b>" + alink + " " + Globe(article.AllCulture, 14) + " " + Flag(article.Culture, 14)
                    + "</b> " + ArticleLanguage.closed + " " + article.CloseDate.ToString("dd MMMM yyyy HH:mm") + " " 
                    + ArticleLanguage.in_the + " " + clink + " " + ArticleLanguage.added_by + " " + ProfileLink(article.UserName);
                td21.InnerHtml += div;
                ctr++;
            }
            tr2.InnerHtml += td21.ToString();
            #endregion
            table.InnerHtml = tr1.ToString() + tr2.ToString();
            #endregion
            result.AppendLine(table.ToString());
            return new HtmlString(result.ToString());
        }
        public static HtmlString CountPager(this HtmlHelper html, int count)
        {
            StringBuilder result = new StringBuilder();
            TagBuilder div = new TagBuilder("div");
            div.MergeAttribute("style", "float:right;margin-right:25px;font-weight:bold");
            TagBuilder a5 = new TagBuilder("a");
            a5.MergeAttribute("href", "/Article/Archive/5/");
            a5.InnerHtml = "5";
            TagBuilder a10 = new TagBuilder("a");
            a10.MergeAttribute("href", "/Article/Archive/10/");
            a10.InnerHtml = "10";
            TagBuilder a15 = new TagBuilder("a");
            a15.MergeAttribute("href", "/Article/Archive/15/");
            a15.InnerHtml = "15";
            if (count == 5)
                div.InnerHtml = "5 " + a10.ToString() + " " + a15.ToString();
            else if (count == 10)
                div.InnerHtml = a5.ToString() + " 10 " + " " + a15.ToString();
            else
                div.InnerHtml = a5.ToString() + " " + a10.ToString() + " 15";
            result.AppendLine(div.ToString());
            return new HtmlString(result.ToString());
        }
        #endregion

        #region Details helpers
        public static HtmlString CulturePager(this HtmlHelper html, string path)
        {
            if (path == "Tags")
                return null;
            StringBuilder result = new StringBuilder();
            TagBuilder div = new TagBuilder("div");
            div.MergeAttribute("style", "float:right;margin-right:25px");
            TagBuilder a = new TagBuilder("a");
            a.MergeAttribute("href", "/Article/ArchiveDetails/" + path + "/");
            a.InnerHtml = Globe(true, 20);
            div.InnerHtml = a.ToString() + "  ";
            foreach (string culture in LocalisationSetting.allCultureMassive())
            {
                TagBuilder a1 = new TagBuilder("a");
                a1.MergeAttribute("href", "/Article/ArchiveDetails/" + path + "/" + culture.ToLower() + "/");
                a1.InnerHtml = Flag(culture, 20);
                div.InnerHtml += a1.ToString() + "  ";
            }
            result.AppendLine(div.ToString());
            return new HtmlString(result.ToString());
        }
        public static HtmlString ShowDetails(this HtmlHelper html, string path, string culture)
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
            else if (path == "UnapprovedComments")
                details = UnapprovedCommentsDetails(culture);
            else if (path == "ClosedArticles")
                details = ClosedArticlesDetails(culture);
            return new HtmlString(details);
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
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.article.GetLastArticles(-2, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.article.GetLastArticles(-3, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.article.GetLastArticles(-4, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-5).ToString("dddd")), ar.article.GetLastArticles(-5, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-6).ToString("dddd")), ar.article.GetLastArticles(-6, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-7).ToString("dddd")), ar.article.GetLastArticles(-7, culture));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.article.GetLastArticles(-8, 7, culture));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.article.GetLastArticles(-15, 7, culture));
                week += BuildRow(ArticleLanguage.four_weeks_ago, ar.article.GetLastArticles(-22, 7, culture));
                week += BuildRow(ArticleLanguage.month_ago, ar.article.GetLastArticles(-29, 30, culture));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.article.GetLastArticles(-59, 30, culture));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.article.GetLastArticles(-89, 30, culture));
                week += BuildRow(ArticleLanguage.older, ar.article.GetLastArticles(-119, 99999, culture));
            }
            else if (dayOfWeek == "Tuesday")
            {
                week = BuildRow(ArticleLanguage.week_ago, ar.article.GetLastArticles(-2, 7, culture));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.article.GetLastArticles(-9, 7, culture));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.article.GetLastArticles(-16, 7, culture));
                week += BuildRow(ArticleLanguage.four_weeks_ago, ar.article.GetLastArticles(-23, 7, culture));
                week += BuildRow(ArticleLanguage.month_ago, ar.article.GetLastArticles(-30, 30, culture));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.article.GetLastArticles(-60, 30, culture));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.article.GetLastArticles(-90, 30, culture));
                week += BuildRow(ArticleLanguage.older, ar.article.GetLastArticles(-120, 99999, culture));
            }
            else if (dayOfWeek == "Wednesday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.article.GetLastArticles(-2, culture));
                week += BuildRow(ArticleLanguage.week_ago, ar.article.GetLastArticles(-3, 7, culture));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.article.GetLastArticles(-10, 7, culture));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.article.GetLastArticles(-17, 7, culture));
                week += BuildRow(ArticleLanguage.four_weeks_ago, ar.article.GetLastArticles(-24, 7, culture));
                week += BuildRow(ArticleLanguage.month_ago, ar.article.GetLastArticles(-31, 30, culture));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.article.GetLastArticles(-61, 30, culture));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.article.GetLastArticles(-91, 30, culture));
            }
            else if (dayOfWeek == "Thursday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.article.GetLastArticles(-2, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.article.GetLastArticles(-3, culture));
                week += BuildRow(ArticleLanguage.week_ago, ar.article.GetLastArticles(-4, 7, culture));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.article.GetLastArticles(-11, 7, culture));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.article.GetLastArticles(-18, 7, culture));
                week += BuildRow(ArticleLanguage.four_weeks_ago, ar.article.GetLastArticles(-25, 7, culture));
                week += BuildRow(ArticleLanguage.month_ago, ar.article.GetLastArticles(-32, 30, culture));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.article.GetLastArticles(-62, 30, culture));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.article.GetLastArticles(-92, 30, culture));
                week += BuildRow(ArticleLanguage.older, ar.article.GetLastArticles(-122, 99999, culture));
            }
            else if (dayOfWeek == "Friday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.article.GetLastArticles(-2, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.article.GetLastArticles(-3, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.article.GetLastArticles(-4, culture));
                week += BuildRow(ArticleLanguage.week_ago, ar.article.GetLastArticles(-5, 7, culture));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.article.GetLastArticles(-12, 7, culture));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.article.GetLastArticles(-19, 7, culture));
                week += BuildRow(ArticleLanguage.four_weeks_ago, ar.article.GetLastArticles(-26, 7, culture));
                week += BuildRow(ArticleLanguage.month_ago, ar.article.GetLastArticles(-33, 30, culture));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.article.GetLastArticles(-63, 30, culture));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.article.GetLastArticles(-93, 30, culture));
                week += BuildRow(ArticleLanguage.older, ar.article.GetLastArticles(-123, 99999, culture));
            }
            else if (dayOfWeek == "Saturday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.article.GetLastArticles(-2, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.article.GetLastArticles(-3, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.article.GetLastArticles(-4, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-5).ToString("dddd")), ar.article.GetLastArticles(-5, culture));
                week += BuildRow(ArticleLanguage.week_ago, ar.article.GetLastArticles(-6, 7, culture));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.article.GetLastArticles(-13, 7, culture));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.article.GetLastArticles(-20, 7, culture));
                week += BuildRow(ArticleLanguage.four_weeks_ago, ar.article.GetLastArticles(-27, 7, culture));
                week += BuildRow(ArticleLanguage.month_ago, ar.article.GetLastArticles(-34, 30, culture));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.article.GetLastArticles(-64, 30, culture));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.article.GetLastArticles(-94, 30, culture));
                week += BuildRow(ArticleLanguage.older, ar.article.GetLastArticles(-124, 99999, culture));
            }
            else if (dayOfWeek == "Sunday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.article.GetLastArticles(-2, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.article.GetLastArticles(-3, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.article.GetLastArticles(-4, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-5).ToString("dddd")), ar.article.GetLastArticles(-5, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-6).ToString("dddd")), ar.article.GetLastArticles(-6, culture));
                week += BuildRow(ArticleLanguage.week_ago, ar.article.GetLastArticles(-7, 7, culture));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.article.GetLastArticles(-14, 7, culture));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.article.GetLastArticles(-21, 7, culture));
                week += BuildRow(ArticleLanguage.month_ago, ar.article.GetLastArticles(-28, 30, culture));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.article.GetLastArticles(-58, 30, culture));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.article.GetLastArticles(-88, 30, culture));
                week += BuildRow(ArticleLanguage.older, ar.article.GetLastArticles(-118, 99999, culture));
            }
            #endregion
            table.InnerHtml = BuildRow(ArticleLanguage.today, ar.article.GetLastArticles(0, culture))
                + BuildRow(ArticleLanguage.yesterday, ar.article.GetLastArticles(-1, culture)) + week;
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
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.article.GetArticlesClosed(-2, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.article.GetArticlesClosed(-3, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.article.GetArticlesClosed(-4, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-5).ToString("dddd")), ar.article.GetArticlesClosed(-5, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-6).ToString("dddd")), ar.article.GetArticlesClosed(-6, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-7).ToString("dddd")), ar.article.GetArticlesClosed(-7, culture));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.article.GetArticlesClosed(-8, 7, culture));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.article.GetArticlesClosed(-15, 7, culture));
                week += BuildRow(ArticleLanguage.four_weeks_ago, ar.article.GetArticlesClosed(-22, 7, culture));
                week += BuildRow(ArticleLanguage.month_ago, ar.article.GetArticlesClosed(-29, 30, culture));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.article.GetArticlesClosed(-59, 30, culture));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.article.GetArticlesClosed(-89, 30, culture));
                week += BuildRow(ArticleLanguage.older, ar.article.GetArticlesClosed(-119, 99999, culture));
            }
            else if (dayOfWeek == "Tuesday")
            {
                week = BuildRow(ArticleLanguage.week_ago, ar.article.GetArticlesClosed(-2, 7, culture));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.article.GetArticlesClosed(-9, 7, culture));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.article.GetArticlesClosed(-16, 7, culture));
                week += BuildRow(ArticleLanguage.four_weeks_ago, ar.article.GetArticlesClosed(-23, 7, culture));
                week += BuildRow(ArticleLanguage.month_ago, ar.article.GetArticlesClosed(-30, 30, culture));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.article.GetArticlesClosed(-60, 30, culture));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.article.GetArticlesClosed(-90, 30, culture));
                week += BuildRow(ArticleLanguage.older, ar.article.GetArticlesClosed(-120, 99999, culture));
            }
            else if (dayOfWeek == "Wednesday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.article.GetArticlesClosed(-2, culture));
                week += BuildRow(ArticleLanguage.week_ago, ar.article.GetArticlesClosed(-3, 7, culture));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.article.GetArticlesClosed(-10, 7, culture));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.article.GetArticlesClosed(-17, 7, culture));
                week += BuildRow(ArticleLanguage.four_weeks_ago, ar.article.GetArticlesClosed(-24, 7, culture));
                week += BuildRow(ArticleLanguage.month_ago, ar.article.GetArticlesClosed(-31, 30, culture));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.article.GetArticlesClosed(-61, 30, culture));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.article.GetArticlesClosed(-91, 30, culture));
            }
            else if (dayOfWeek == "Thursday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.article.GetArticlesClosed(-2, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.article.GetArticlesClosed(-3, culture));
                week += BuildRow(ArticleLanguage.week_ago, ar.article.GetArticlesClosed(-4, 7, culture));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.article.GetArticlesClosed(-11, 7, culture));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.article.GetArticlesClosed(-18, 7, culture));
                week += BuildRow(ArticleLanguage.four_weeks_ago, ar.article.GetArticlesClosed(-25, 7, culture));
                week += BuildRow(ArticleLanguage.month_ago, ar.article.GetArticlesClosed(-32, 30, culture));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.article.GetArticlesClosed(-62, 30, culture));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.article.GetArticlesClosed(-92, 30, culture));
                week += BuildRow(ArticleLanguage.older, ar.article.GetArticlesClosed(-122, 99999, culture));
            }
            else if (dayOfWeek == "Friday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.article.GetArticlesClosed(-2, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.article.GetArticlesClosed(-3, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.article.GetArticlesClosed(-4, culture));
                week += BuildRow(ArticleLanguage.week_ago, ar.article.GetArticlesClosed(-5, 7, culture));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.article.GetArticlesClosed(-12, 7, culture));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.article.GetArticlesClosed(-19, 7, culture));
                week += BuildRow(ArticleLanguage.four_weeks_ago, ar.article.GetArticlesClosed(-26, 7, culture));
                week += BuildRow(ArticleLanguage.month_ago, ar.article.GetArticlesClosed(-33, 30, culture));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.article.GetArticlesClosed(-63, 30, culture));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.article.GetArticlesClosed(-93, 30, culture));
                week += BuildRow(ArticleLanguage.older, ar.article.GetArticlesClosed(-123, 99999, culture));
            }
            else if (dayOfWeek == "Saturday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.article.GetArticlesClosed(-2, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.article.GetArticlesClosed(-3, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.article.GetArticlesClosed(-4, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-5).ToString("dddd")), ar.article.GetArticlesClosed(-5, culture));
                week += BuildRow(ArticleLanguage.week_ago, ar.article.GetArticlesClosed(-6, 7, culture));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.article.GetArticlesClosed(-13, 7, culture));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.article.GetArticlesClosed(-20, 7, culture));
                week += BuildRow(ArticleLanguage.four_weeks_ago, ar.article.GetArticlesClosed(-27, 7, culture));
                week += BuildRow(ArticleLanguage.month_ago, ar.article.GetArticlesClosed(-34, 30, culture));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.article.GetArticlesClosed(-64, 30, culture));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.article.GetArticlesClosed(-94, 30, culture));
                week += BuildRow(ArticleLanguage.older, ar.article.GetArticlesClosed(-124, 99999, culture));
            }
            else if (dayOfWeek == "Sunday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.article.GetArticlesClosed(-2, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.article.GetArticlesClosed(-3, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.article.GetArticlesClosed(-4, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-5).ToString("dddd")), ar.article.GetArticlesClosed(-5, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-6).ToString("dddd")), ar.article.GetArticlesClosed(-6, culture));
                week += BuildRow(ArticleLanguage.week_ago, ar.article.GetArticlesClosed(-7, 7, culture));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.article.GetArticlesClosed(-14, 7, culture));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.article.GetArticlesClosed(-21, 7, culture));
                week += BuildRow(ArticleLanguage.month_ago, ar.article.GetArticlesClosed(-28, 30, culture));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.article.GetArticlesClosed(-58, 30, culture));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.article.GetArticlesClosed(-88, 30, culture));
                week += BuildRow(ArticleLanguage.older, ar.article.GetArticlesClosed(-118, 99999, culture));
            }
            #endregion
            table.InnerHtml = BuildRow(ArticleLanguage.today, ar.article.GetArticlesClosed(0, culture))
                + BuildRow(ArticleLanguage.yesterday, ar.article.GetArticlesClosed(-1, culture)) + week;
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
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.article.GetLastPosts(-2, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.article.GetLastPosts(-3, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.article.GetLastPosts(-4, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-5).ToString("dddd")), ar.article.GetLastPosts(-5, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-6).ToString("dddd")), ar.article.GetLastPosts(-6, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-7).ToString("dddd")), ar.article.GetLastPosts(-7, culture));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.article.GetLastPosts(-8, 7, culture));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.article.GetLastPosts(-15, 7, culture));
                week += BuildRow(ArticleLanguage.four_weeks_ago, ar.article.GetLastPosts(-22, 7, culture));
                week += BuildRow(ArticleLanguage.month_ago, ar.article.GetLastPosts(-29, 30, culture));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.article.GetLastPosts(-59, 30, culture));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.article.GetLastPosts(-89, 30, culture));
                week += BuildRow(ArticleLanguage.older, ar.article.GetLastPosts(-119, 99999, culture));
            }
            else if (dayOfWeek == "Tuesday")
            {
                week = BuildRow(ArticleLanguage.week_ago, ar.article.GetLastPosts(-2, 7, culture));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.article.GetLastPosts(-9, 7, culture));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.article.GetLastPosts(-16, 7, culture));
                week += BuildRow(ArticleLanguage.four_weeks_ago, ar.article.GetLastPosts(-23, 7, culture));
                week += BuildRow(ArticleLanguage.month_ago, ar.article.GetLastPosts(-30, 30, culture));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.article.GetLastPosts(-60, 30, culture));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.article.GetLastPosts(-90, 30, culture));
                week += BuildRow(ArticleLanguage.older, ar.article.GetLastPosts(-120, 99999, culture));
            }
            else if (dayOfWeek == "Wednesday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.article.GetLastPosts(-2, culture));
                week += BuildRow(ArticleLanguage.week_ago, ar.article.GetLastPosts(-3, 7, culture));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.article.GetLastPosts(-10, 7, culture));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.article.GetLastPosts(-17, 7, culture));
                week += BuildRow(ArticleLanguage.four_weeks_ago, ar.article.GetLastPosts(-24, 7, culture));
                week += BuildRow(ArticleLanguage.month_ago, ar.article.GetLastPosts(-31, 30, culture));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.article.GetLastPosts(-61, 30, culture));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.article.GetLastPosts(-91, 30, culture));
            }
            else if (dayOfWeek == "Thursday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.article.GetLastPosts(-2, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.article.GetLastPosts(-3, culture));
                week += BuildRow(ArticleLanguage.week_ago, ar.article.GetLastPosts(-4, 7, culture));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.article.GetLastPosts(-11, 7, culture));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.article.GetLastPosts(-18, 7, culture));
                week += BuildRow(ArticleLanguage.four_weeks_ago, ar.article.GetLastPosts(-25, 7, culture));
                week += BuildRow(ArticleLanguage.month_ago, ar.article.GetLastPosts(-32, 30, culture));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.article.GetLastPosts(-62, 30, culture));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.article.GetLastPosts(-92, 30, culture));
                week += BuildRow(ArticleLanguage.older, ar.article.GetLastPosts(-122, 99999, culture));
            }
            else if (dayOfWeek == "Friday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.article.GetLastPosts(-2, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.article.GetLastPosts(-3, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.article.GetLastPosts(-4, culture));
                week += BuildRow(ArticleLanguage.week_ago, ar.article.GetLastPosts(-5, 7, culture));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.article.GetLastPosts(-12, 7, culture));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.article.GetLastPosts(-19, 7, culture));
                week += BuildRow(ArticleLanguage.four_weeks_ago, ar.article.GetLastPosts(-26, 7, culture));
                week += BuildRow(ArticleLanguage.month_ago, ar.article.GetLastPosts(-33, 30, culture));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.article.GetLastPosts(-63, 30, culture));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.article.GetLastPosts(-93, 30, culture));
                week += BuildRow(ArticleLanguage.older, ar.article.GetLastPosts(-123, 99999, culture));
            }
            else if (dayOfWeek == "Saturday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.article.GetLastPosts(-2, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.article.GetLastPosts(-3, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.article.GetLastPosts(-4, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-5).ToString("dddd")), ar.article.GetLastPosts(-5, culture));
                week += BuildRow(ArticleLanguage.week_ago, ar.article.GetLastPosts(-6, 7, culture));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.article.GetLastPosts(-13, 7, culture));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.article.GetLastPosts(-20, 7, culture));
                week += BuildRow(ArticleLanguage.four_weeks_ago, ar.article.GetLastPosts(-27, 7, culture));
                week += BuildRow(ArticleLanguage.month_ago, ar.article.GetLastPosts(-34, 30, culture));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.article.GetLastPosts(-64, 30, culture));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.article.GetLastPosts(-94, 30, culture));
                week += BuildRow(ArticleLanguage.older, ar.article.GetLastPosts(-124, 99999, culture));
            }
            else if (dayOfWeek == "Sunday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.article.GetLastPosts(-2, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.article.GetLastPosts(-3, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.article.GetLastPosts(-4, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-5).ToString("dddd")), ar.article.GetLastPosts(-5, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-6).ToString("dddd")), ar.article.GetLastPosts(-6, culture));
                week += BuildRow(ArticleLanguage.week_ago, ar.article.GetLastPosts(-7, 7, culture));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.article.GetLastPosts(-14, 7, culture));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.article.GetLastPosts(-21, 7, culture));
                week += BuildRow(ArticleLanguage.month_ago, ar.article.GetLastPosts(-28, 30, culture));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.article.GetLastPosts(-58, 30, culture));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.article.GetLastPosts(-88, 30, culture));
                week += BuildRow(ArticleLanguage.older, ar.article.GetLastPosts(-118, 99999, culture));
            }
            #endregion
            table.InnerHtml = BuildRow(ArticleLanguage.today, ar.article.GetLastPosts(0, culture))
                + BuildRow(ArticleLanguage.yesterday, ar.article.GetLastPosts(-1, culture)) + week;
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
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.category.GetLastCategories(-2, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.category.GetLastCategories(-3, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.category.GetLastCategories(-4, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-5).ToString("dddd")), ar.category.GetLastCategories(-5, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-6).ToString("dddd")), ar.category.GetLastCategories(-6, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-7).ToString("dddd")), ar.category.GetLastCategories(-7, culture));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.category.GetLastCategories(-8, 7, culture));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.category.GetLastCategories(-15, 7, culture));
                week += BuildRow(ArticleLanguage.four_weeks_ago, ar.category.GetLastCategories(-22, 7, culture));
                week += BuildRow(ArticleLanguage.month_ago, ar.category.GetLastCategories(-29, 30, culture));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.category.GetLastCategories(-59, 30, culture));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.category.GetLastCategories(-89, 30, culture));
                week += BuildRow(ArticleLanguage.older, ar.category.GetLastCategories(-119, 99999, culture));
            }
            else if (dayOfWeek == "Tuesday")
            {
                week = BuildRow(ArticleLanguage.week_ago, ar.category.GetLastCategories(-2, 7, culture));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.category.GetLastCategories(-9, 7, culture));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.category.GetLastCategories(-16, 7, culture));
                week += BuildRow(ArticleLanguage.four_weeks_ago, ar.category.GetLastCategories(-23, 7, culture));
                week += BuildRow(ArticleLanguage.month_ago, ar.category.GetLastCategories(-30, 30, culture));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.category.GetLastCategories(-60, 30, culture));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.category.GetLastCategories(-90, 30, culture));
                week += BuildRow(ArticleLanguage.older, ar.category.GetLastCategories(-120, 99999, culture));
            }
            else if (dayOfWeek == "Wednesday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.category.GetLastCategories(-2, culture));
                week += BuildRow(ArticleLanguage.week_ago, ar.category.GetLastCategories(-3, 7, culture));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.category.GetLastCategories(-10, 7, culture));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.category.GetLastCategories(-17, 7, culture));
                week += BuildRow(ArticleLanguage.four_weeks_ago, ar.category.GetLastCategories(-24, 7, culture));
                week += BuildRow(ArticleLanguage.month_ago, ar.category.GetLastCategories(-31, 30, culture));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.category.GetLastCategories(-61, 30, culture));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.category.GetLastCategories(-91, 30, culture));
            }
            else if (dayOfWeek == "Thursday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.category.GetLastCategories(-2, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.category.GetLastCategories(-3, culture));
                week += BuildRow(ArticleLanguage.week_ago, ar.category.GetLastCategories(-4, 7, culture));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.category.GetLastCategories(-11, 7, culture));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.category.GetLastCategories(-18, 7, culture));
                week += BuildRow(ArticleLanguage.four_weeks_ago, ar.category.GetLastCategories(-25, 7, culture));
                week += BuildRow(ArticleLanguage.month_ago, ar.category.GetLastCategories(-32, 30, culture));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.category.GetLastCategories(-62, 30, culture));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.category.GetLastCategories(-92, 30, culture));
                week += BuildRow(ArticleLanguage.older, ar.category.GetLastCategories(-122, 99999, culture));
            }
            else if (dayOfWeek == "Friday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.category.GetLastCategories(-2, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.category.GetLastCategories(-3, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.category.GetLastCategories(-4, culture));
                week += BuildRow(ArticleLanguage.week_ago, ar.category.GetLastCategories(-5, 7, culture));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.category.GetLastCategories(-12, 7, culture));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.category.GetLastCategories(-19, 7, culture));
                week += BuildRow(ArticleLanguage.four_weeks_ago, ar.category.GetLastCategories(-26, 7, culture));
                week += BuildRow(ArticleLanguage.month_ago, ar.category.GetLastCategories(-33, 30, culture));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.category.GetLastCategories(-63, 30, culture));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.category.GetLastCategories(-93, 30, culture));
                week += BuildRow(ArticleLanguage.older, ar.category.GetLastCategories(-123, 99999, culture));
            }
            else if (dayOfWeek == "Saturday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.category.GetLastCategories(-2, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.category.GetLastCategories(-3, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.category.GetLastCategories(-4, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-5).ToString("dddd")), ar.category.GetLastCategories(-5, culture));
                week += BuildRow(ArticleLanguage.week_ago, ar.category.GetLastCategories(-6, 7, culture));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.category.GetLastCategories(-13, 7, culture));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.category.GetLastCategories(-20, 7, culture));
                week += BuildRow(ArticleLanguage.four_weeks_ago, ar.category.GetLastCategories(-27, 7, culture));
                week += BuildRow(ArticleLanguage.month_ago, ar.category.GetLastCategories(-34, 30, culture));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.category.GetLastCategories(-64, 30, culture));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.category.GetLastCategories(-94, 30, culture));
                week += BuildRow(ArticleLanguage.older, ar.category.GetLastCategories(-124, 99999, culture));
            }
            else if (dayOfWeek == "Sunday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.category.GetLastCategories(-2, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.category.GetLastCategories(-3, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.category.GetLastCategories(-4, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-5).ToString("dddd")), ar.category.GetLastCategories(-5, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-6).ToString("dddd")), ar.category.GetLastCategories(-6, culture));
                week += BuildRow(ArticleLanguage.week_ago, ar.category.GetLastCategories(-7, 7, culture));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.category.GetLastCategories(-14, 7, culture));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.category.GetLastCategories(-21, 7, culture));
                week += BuildRow(ArticleLanguage.month_ago, ar.category.GetLastCategories(-28, 30, culture));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.category.GetLastCategories(-58, 30, culture));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.category.GetLastCategories(-88, 30, culture));
                week += BuildRow(ArticleLanguage.older, ar.category.GetLastCategories(-118, 99999, culture));
            }
            #endregion
            table.InnerHtml = BuildRow(ArticleLanguage.today, ar.category.GetLastCategories(0, culture))
                + BuildRow(ArticleLanguage.yesterday, ar.category.GetLastCategories(-1, culture)) + week;
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
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.category.GetLastBlogs(-2, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.category.GetLastBlogs(-3, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.category.GetLastBlogs(-4, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-5).ToString("dddd")), ar.category.GetLastBlogs(-5, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-6).ToString("dddd")), ar.category.GetLastBlogs(-6, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-7).ToString("dddd")), ar.category.GetLastBlogs(-7, culture));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.category.GetLastBlogs(-8, 7, culture));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.category.GetLastBlogs(-15, 7, culture));
                week += BuildRow(ArticleLanguage.four_weeks_ago, ar.category.GetLastBlogs(-22, 7, culture));
                week += BuildRow(ArticleLanguage.month_ago, ar.category.GetLastBlogs(-29, 30, culture));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.category.GetLastBlogs(-59, 30, culture));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.category.GetLastBlogs(-89, 30, culture));
                week += BuildRow(ArticleLanguage.older, ar.category.GetLastBlogs(-119, 99999, culture));
            }
            else if (dayOfWeek == "Tuesday")
            {
                week = BuildRow(ArticleLanguage.week_ago, ar.category.GetLastBlogs(-2, 7, culture));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.category.GetLastBlogs(-9, 7, culture));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.category.GetLastBlogs(-16, 7, culture));
                week += BuildRow(ArticleLanguage.four_weeks_ago, ar.category.GetLastBlogs(-23, 7, culture));
                week += BuildRow(ArticleLanguage.month_ago, ar.category.GetLastBlogs(-30, 30, culture));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.category.GetLastBlogs(-60, 30, culture));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.category.GetLastBlogs(-90, 30, culture));
                week += BuildRow(ArticleLanguage.older, ar.category.GetLastBlogs(-120, 99999, culture));
            }
            else if (dayOfWeek == "Wednesday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.category.GetLastBlogs(-2, culture));
                week += BuildRow(ArticleLanguage.week_ago, ar.category.GetLastBlogs(-3, 7, culture));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.category.GetLastBlogs(-10, 7, culture));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.category.GetLastBlogs(-17, 7, culture));
                week += BuildRow(ArticleLanguage.four_weeks_ago, ar.category.GetLastBlogs(-24, 7, culture));
                week += BuildRow(ArticleLanguage.month_ago, ar.category.GetLastBlogs(-31, 30, culture));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.category.GetLastBlogs(-61, 30, culture));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.category.GetLastBlogs(-91, 30, culture));
            }
            else if (dayOfWeek == "Thursday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.category.GetLastBlogs(-2, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.category.GetLastBlogs(-3, culture));
                week += BuildRow(ArticleLanguage.week_ago, ar.category.GetLastBlogs(-4, 7, culture));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.category.GetLastBlogs(-11, 7, culture));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.category.GetLastBlogs(-18, 7, culture));
                week += BuildRow(ArticleLanguage.four_weeks_ago, ar.category.GetLastBlogs(-25, 7, culture));
                week += BuildRow(ArticleLanguage.month_ago, ar.category.GetLastBlogs(-32, 30, culture));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.category.GetLastBlogs(-62, 30, culture));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.category.GetLastBlogs(-92, 30, culture));
                week += BuildRow(ArticleLanguage.older, ar.category.GetLastBlogs(-122, 99999, culture));
            }
            else if (dayOfWeek == "Friday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.category.GetLastBlogs(-2, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.category.GetLastBlogs(-3, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.category.GetLastBlogs(-4, culture));
                week += BuildRow(ArticleLanguage.week_ago, ar.category.GetLastBlogs(-5, 7, culture));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.category.GetLastBlogs(-12, 7, culture));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.category.GetLastBlogs(-19, 7, culture));
                week += BuildRow(ArticleLanguage.four_weeks_ago, ar.category.GetLastBlogs(-26, 7, culture));
                week += BuildRow(ArticleLanguage.month_ago, ar.category.GetLastBlogs(-33, 30, culture));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.category.GetLastBlogs(-63, 30, culture));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.category.GetLastBlogs(-93, 30, culture));
                week += BuildRow(ArticleLanguage.older, ar.category.GetLastBlogs(-123, 99999, culture));
            }
            else if (dayOfWeek == "Saturday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.category.GetLastBlogs(-2, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.category.GetLastBlogs(-3, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.category.GetLastBlogs(-4, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-5).ToString("dddd")), ar.category.GetLastBlogs(-5, culture));
                week += BuildRow(ArticleLanguage.week_ago, ar.category.GetLastBlogs(-6, 7, culture));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.category.GetLastBlogs(-13, 7, culture));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.category.GetLastBlogs(-20, 7, culture));
                week += BuildRow(ArticleLanguage.four_weeks_ago, ar.category.GetLastBlogs(-27, 7, culture));
                week += BuildRow(ArticleLanguage.month_ago, ar.category.GetLastBlogs(-34, 30, culture));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.category.GetLastBlogs(-64, 30, culture));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.category.GetLastBlogs(-94, 30, culture));
                week += BuildRow(ArticleLanguage.older, ar.category.GetLastBlogs(-124, 99999, culture));
            }
            else if (dayOfWeek == "Sunday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.category.GetLastBlogs(-2, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.category.GetLastBlogs(-3, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.category.GetLastBlogs(-4, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-5).ToString("dddd")), ar.category.GetLastBlogs(-5, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-6).ToString("dddd")), ar.category.GetLastBlogs(-6, culture));
                week += BuildRow(ArticleLanguage.week_ago, ar.category.GetLastBlogs(-7, 7, culture));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.category.GetLastBlogs(-14, 7, culture));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.category.GetLastBlogs(-21, 7, culture));
                week += BuildRow(ArticleLanguage.month_ago, ar.category.GetLastBlogs(-28, 30, culture));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.category.GetLastBlogs(-58, 30, culture));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.category.GetLastBlogs(-88, 30, culture));
                week += BuildRow(ArticleLanguage.older, ar.category.GetLastBlogs(-118, 99999, culture));
            }
            #endregion
            table.InnerHtml = BuildRow(ArticleLanguage.today, ar.category.GetLastBlogs(0, culture))
                + BuildRow(ArticleLanguage.yesterday, ar.category.GetLastBlogs(-1, culture)) + week;
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
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.category.GetLastTopics(-2, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.category.GetLastTopics(-3, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.category.GetLastTopics(-4, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-5).ToString("dddd")), ar.category.GetLastTopics(-5, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-6).ToString("dddd")), ar.category.GetLastTopics(-6, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-7).ToString("dddd")), ar.category.GetLastTopics(-7, culture));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.category.GetLastTopics(-8, 7, culture));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.category.GetLastTopics(-15, 7, culture));
                week += BuildRow(ArticleLanguage.four_weeks_ago, ar.category.GetLastTopics(-22, 7, culture));
                week += BuildRow(ArticleLanguage.month_ago, ar.category.GetLastTopics(-29, 30, culture));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.category.GetLastTopics(-59, 30, culture));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.category.GetLastTopics(-89, 30, culture));
                week += BuildRow(ArticleLanguage.older, ar.category.GetLastTopics(-119, 99999, culture));
            }
            else if (dayOfWeek == "Tuesday")
            {
                week = BuildRow(ArticleLanguage.week_ago, ar.category.GetLastTopics(-2, 7, culture));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.category.GetLastTopics(-9, 7, culture));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.category.GetLastTopics(-16, 7, culture));
                week += BuildRow(ArticleLanguage.four_weeks_ago, ar.category.GetLastTopics(-23, 7, culture));
                week += BuildRow(ArticleLanguage.month_ago, ar.category.GetLastTopics(-30, 30, culture));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.category.GetLastTopics(-60, 30, culture));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.category.GetLastTopics(-90, 30, culture));
                week += BuildRow(ArticleLanguage.older, ar.category.GetLastTopics(-120, 99999, culture));
            }
            else if (dayOfWeek == "Wednesday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.category.GetLastTopics(-2, culture));
                week += BuildRow(ArticleLanguage.week_ago, ar.category.GetLastTopics(-3, 7, culture));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.category.GetLastTopics(-10, 7, culture));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.category.GetLastTopics(-17, 7, culture));
                week += BuildRow(ArticleLanguage.four_weeks_ago, ar.category.GetLastTopics(-24, 7, culture));
                week += BuildRow(ArticleLanguage.month_ago, ar.category.GetLastTopics(-31, 30, culture));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.category.GetLastTopics(-61, 30, culture));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.category.GetLastTopics(-91, 30, culture));
            }
            else if (dayOfWeek == "Thursday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.category.GetLastTopics(-2, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.category.GetLastTopics(-3, culture));
                week += BuildRow(ArticleLanguage.week_ago, ar.category.GetLastTopics(-4, 7, culture));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.category.GetLastTopics(-11, 7, culture));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.category.GetLastTopics(-18, 7, culture));
                week += BuildRow(ArticleLanguage.four_weeks_ago, ar.category.GetLastTopics(-25, 7, culture));
                week += BuildRow(ArticleLanguage.month_ago, ar.category.GetLastTopics(-32, 30, culture));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.category.GetLastTopics(-62, 30, culture));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.category.GetLastTopics(-92, 30, culture));
                week += BuildRow(ArticleLanguage.older, ar.category.GetLastTopics(-122, 99999, culture));
            }
            else if (dayOfWeek == "Friday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.category.GetLastTopics(-2, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.category.GetLastTopics(-3, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.category.GetLastTopics(-4, culture));
                week += BuildRow(ArticleLanguage.week_ago, ar.category.GetLastTopics(-5, 7, culture));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.category.GetLastTopics(-12, 7, culture));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.category.GetLastTopics(-19, 7, culture));
                week += BuildRow(ArticleLanguage.four_weeks_ago, ar.category.GetLastTopics(-26, 7, culture));
                week += BuildRow(ArticleLanguage.month_ago, ar.category.GetLastTopics(-33, 30, culture));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.category.GetLastTopics(-63, 30, culture));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.category.GetLastTopics(-93, 30, culture));
                week += BuildRow(ArticleLanguage.older, ar.category.GetLastTopics(-123, 99999, culture));
            }
            else if (dayOfWeek == "Saturday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.category.GetLastTopics(-2, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.category.GetLastTopics(-3, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.category.GetLastTopics(-4, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-5).ToString("dddd")), ar.category.GetLastTopics(-5, culture));
                week += BuildRow(ArticleLanguage.week_ago, ar.category.GetLastTopics(-6, 7, culture));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.category.GetLastTopics(-13, 7, culture));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.category.GetLastTopics(-20, 7, culture));
                week += BuildRow(ArticleLanguage.four_weeks_ago, ar.category.GetLastTopics(-27, 7, culture));
                week += BuildRow(ArticleLanguage.month_ago, ar.category.GetLastTopics(-34, 30, culture));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.category.GetLastTopics(-64, 30, culture));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.category.GetLastTopics(-94, 30, culture));
                week += BuildRow(ArticleLanguage.older, ar.category.GetLastTopics(-124, 99999, culture));
            }
            else if (dayOfWeek == "Sunday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.category.GetLastTopics(-2, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.category.GetLastTopics(-3, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.category.GetLastTopics(-4, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-5).ToString("dddd")), ar.category.GetLastTopics(-5, culture));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-6).ToString("dddd")), ar.category.GetLastTopics(-6, culture));
                week += BuildRow(ArticleLanguage.week_ago, ar.category.GetLastTopics(-7, 7, culture));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.category.GetLastTopics(-14, 7, culture));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.category.GetLastTopics(-21, 7, culture));
                week += BuildRow(ArticleLanguage.month_ago, ar.category.GetLastTopics(-28, 30, culture));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.category.GetLastTopics(-58, 30, culture));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.category.GetLastTopics(-88, 30, culture));
                week += BuildRow(ArticleLanguage.older, ar.category.GetLastTopics(-118, 99999, culture));
            }
            #endregion
            table.InnerHtml = BuildRow(ArticleLanguage.today, ar.category.GetLastTopics(0, culture))
                + BuildRow(ArticleLanguage.yesterday, ar.category.GetLastTopics(-1, culture)) + week;
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
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.comment.GetComments(-2, culture, CommentType.Articles));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.comment.GetComments(-3, culture, CommentType.Articles));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.comment.GetComments(-4, culture, CommentType.Articles));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-5).ToString("dddd")), ar.comment.GetComments(-5, culture, CommentType.Articles));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-6).ToString("dddd")), ar.comment.GetComments(-6, culture, CommentType.Articles));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-7).ToString("dddd")), ar.comment.GetComments(-7, culture, CommentType.Articles));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.comment.GetComments(-8, 7, culture, CommentType.Articles));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.comment.GetComments(-15, 7, culture, CommentType.Articles));
                week += BuildRow(ArticleLanguage.four_weeks_ago, ar.comment.GetComments(-22, 7, culture, CommentType.Articles));
                week += BuildRow(ArticleLanguage.month_ago, ar.comment.GetComments(-29, 30, culture, CommentType.Articles));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.comment.GetComments(-59, 30, culture, CommentType.Articles));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.comment.GetComments(-89, 30, culture, CommentType.Articles));
                week += BuildRow(ArticleLanguage.older, ar.comment.GetComments(-119, 99999, culture, CommentType.Articles));
            }
            else if (dayOfWeek == "Tuesday")
            {
                week = BuildRow(ArticleLanguage.week_ago, ar.comment.GetComments(-2, 7, culture, CommentType.Articles));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.comment.GetComments(-9, 7, culture, CommentType.Articles));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.comment.GetComments(-16, 7, culture, CommentType.Articles));
                week += BuildRow(ArticleLanguage.four_weeks_ago, ar.comment.GetComments(-23, 7, culture, CommentType.Articles));
                week += BuildRow(ArticleLanguage.month_ago, ar.comment.GetComments(-30, 30, culture, CommentType.Articles));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.comment.GetComments(-60, 30, culture, CommentType.Articles));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.comment.GetComments(-90, 30, culture, CommentType.Articles));
                week += BuildRow(ArticleLanguage.older, ar.comment.GetComments(-120, 99999, culture, CommentType.Articles));
            }
            else if (dayOfWeek == "Wednesday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.comment.GetComments(-2, culture, CommentType.Articles));
                week += BuildRow(ArticleLanguage.week_ago, ar.comment.GetComments(-3, 7, culture, CommentType.Articles));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.comment.GetComments(-10, 7, culture, CommentType.Articles));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.comment.GetComments(-17, 7, culture, CommentType.Articles));
                week += BuildRow(ArticleLanguage.four_weeks_ago, ar.comment.GetComments(-24, 7, culture, CommentType.Articles));
                week += BuildRow(ArticleLanguage.month_ago, ar.comment.GetComments(-31, 30, culture, CommentType.Articles));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.comment.GetComments(-61, 30, culture, CommentType.Articles));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.comment.GetComments(-91, 30, culture, CommentType.Articles));
            }
            else if (dayOfWeek == "Thursday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.comment.GetComments(-2, culture, CommentType.Articles));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.comment.GetComments(-3, culture, CommentType.Articles));
                week += BuildRow(ArticleLanguage.week_ago, ar.comment.GetComments(-4, 7, culture, CommentType.Articles));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.comment.GetComments(-11, 7, culture, CommentType.Articles));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.comment.GetComments(-18, 7, culture, CommentType.Articles));
                week += BuildRow(ArticleLanguage.four_weeks_ago, ar.comment.GetComments(-25, 7, culture, CommentType.Articles));
                week += BuildRow(ArticleLanguage.month_ago, ar.comment.GetComments(-32, 30, culture, CommentType.Articles));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.comment.GetComments(-62, 30, culture, CommentType.Articles));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.comment.GetComments(-92, 30, culture, CommentType.Articles));
                week += BuildRow(ArticleLanguage.older, ar.comment.GetComments(-122, 99999, culture, CommentType.Articles));
            }
            else if (dayOfWeek == "Friday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.comment.GetComments(-2, culture, CommentType.Articles));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.comment.GetComments(-3, culture, CommentType.Articles));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.comment.GetComments(-4, culture, CommentType.Articles));
                week += BuildRow(ArticleLanguage.week_ago, ar.comment.GetComments(-5, 7, culture, CommentType.Articles));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.comment.GetComments(-12, 7, culture, CommentType.Articles));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.comment.GetComments(-19, 7, culture, CommentType.Articles));
                week += BuildRow(ArticleLanguage.four_weeks_ago, ar.comment.GetComments(-26, 7, culture, CommentType.Articles));
                week += BuildRow(ArticleLanguage.month_ago, ar.comment.GetComments(-33, 30, culture, CommentType.Articles));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.comment.GetComments(-63, 30, culture, CommentType.Articles));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.comment.GetComments(-93, 30, culture, CommentType.Articles));
                week += BuildRow(ArticleLanguage.older, ar.comment.GetComments(-123, 99999, culture, CommentType.Articles));
            }
            else if (dayOfWeek == "Saturday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.comment.GetComments(-2, culture, CommentType.Articles));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.comment.GetComments(-3, culture, CommentType.Articles));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.comment.GetComments(-4, culture, CommentType.Articles));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-5).ToString("dddd")), ar.comment.GetComments(-5, culture, CommentType.Articles));
                week += BuildRow(ArticleLanguage.week_ago, ar.comment.GetComments(-6, 7, culture, CommentType.Articles));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.comment.GetComments(-13, 7, culture, CommentType.Articles));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.comment.GetComments(-20, 7, culture, CommentType.Articles));
                week += BuildRow(ArticleLanguage.four_weeks_ago, ar.comment.GetComments(-27, 7, culture, CommentType.Articles));
                week += BuildRow(ArticleLanguage.month_ago, ar.comment.GetComments(-34, 30, culture, CommentType.Articles));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.comment.GetComments(-64, 30, culture, CommentType.Articles));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.comment.GetComments(-94, 30, culture, CommentType.Articles));
                week += BuildRow(ArticleLanguage.older, ar.comment.GetComments(-124, 99999, culture, CommentType.Articles));
            }
            else if (dayOfWeek == "Sunday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.comment.GetComments(-2, culture, CommentType.Articles));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.comment.GetComments(-3, culture, CommentType.Articles));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.comment.GetComments(-4, culture, CommentType.Articles));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-5).ToString("dddd")), ar.comment.GetComments(-5, culture, CommentType.Articles));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-6).ToString("dddd")), ar.comment.GetComments(-6, culture, CommentType.Articles));
                week += BuildRow(ArticleLanguage.week_ago, ar.comment.GetComments(-7, 7, culture, CommentType.Articles));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.comment.GetComments(-14, 7, culture, CommentType.Articles));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.comment.GetComments(-21, 7, culture, CommentType.Articles));
                week += BuildRow(ArticleLanguage.month_ago, ar.comment.GetComments(-28, 30, culture, CommentType.Articles));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.comment.GetComments(-58, 30, culture, CommentType.Articles));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.comment.GetComments(-88, 30, culture, CommentType.Articles));
                week += BuildRow(ArticleLanguage.older, ar.comment.GetComments(-118, 99999, culture, CommentType.Articles));
            }
            #endregion
            table.InnerHtml = BuildRow(ArticleLanguage.today, ar.comment.GetComments(0, culture, CommentType.Articles))
                + BuildRow(ArticleLanguage.yesterday, ar.comment.GetComments(-1, culture, CommentType.Articles)) + week;
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
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.comment.GetComments(-2, culture, CommentType.Blogs));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.comment.GetComments(-3, culture, CommentType.Blogs));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.comment.GetComments(-4, culture, CommentType.Blogs));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-5).ToString("dddd")), ar.comment.GetComments(-5, culture, CommentType.Blogs));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-6).ToString("dddd")), ar.comment.GetComments(-6, culture, CommentType.Blogs));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-7).ToString("dddd")), ar.comment.GetComments(-7, culture, CommentType.Blogs));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.comment.GetComments(-8, 7, culture, CommentType.Blogs));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.comment.GetComments(-15, 7, culture, CommentType.Blogs));
                week += BuildRow(ArticleLanguage.four_weeks_ago, ar.comment.GetComments(-22, 7, culture, CommentType.Blogs));
                week += BuildRow(ArticleLanguage.month_ago, ar.comment.GetComments(-29, 30, culture, CommentType.Blogs));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.comment.GetComments(-59, 30, culture, CommentType.Blogs));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.comment.GetComments(-89, 30, culture, CommentType.Blogs));
                week += BuildRow(ArticleLanguage.older, ar.comment.GetComments(-119, 99999, culture, CommentType.Blogs));
            }
            else if (dayOfWeek == "Tuesday")
            {
                week = BuildRow(ArticleLanguage.week_ago, ar.comment.GetComments(-2, 7, culture, CommentType.Blogs));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.comment.GetComments(-9, 7, culture, CommentType.Blogs));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.comment.GetComments(-16, 7, culture, CommentType.Blogs));
                week += BuildRow(ArticleLanguage.four_weeks_ago, ar.comment.GetComments(-23, 7, culture, CommentType.Blogs));
                week += BuildRow(ArticleLanguage.month_ago, ar.comment.GetComments(-30, 30, culture, CommentType.Blogs));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.comment.GetComments(-60, 30, culture, CommentType.Blogs));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.comment.GetComments(-90, 30, culture, CommentType.Blogs));
                week += BuildRow(ArticleLanguage.older, ar.comment.GetComments(-120, 99999, culture, CommentType.Blogs));
            }
            else if (dayOfWeek == "Wednesday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.comment.GetComments(-2, culture, CommentType.Blogs));
                week += BuildRow(ArticleLanguage.week_ago, ar.comment.GetComments(-3, 7, culture, CommentType.Blogs));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.comment.GetComments(-10, 7, culture, CommentType.Blogs));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.comment.GetComments(-17, 7, culture, CommentType.Blogs));
                week += BuildRow(ArticleLanguage.four_weeks_ago, ar.comment.GetComments(-24, 7, culture, CommentType.Blogs));
                week += BuildRow(ArticleLanguage.month_ago, ar.comment.GetComments(-31, 30, culture, CommentType.Blogs));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.comment.GetComments(-61, 30, culture, CommentType.Blogs));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.comment.GetComments(-91, 30, culture, CommentType.Blogs));
            }
            else if (dayOfWeek == "Thursday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.comment.GetComments(-2, culture, CommentType.Blogs));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.comment.GetComments(-3, culture, CommentType.Blogs));
                week += BuildRow(ArticleLanguage.week_ago, ar.comment.GetComments(-4, 7, culture, CommentType.Blogs));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.comment.GetComments(-11, 7, culture, CommentType.Blogs));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.comment.GetComments(-18, 7, culture, CommentType.Blogs));
                week += BuildRow(ArticleLanguage.four_weeks_ago, ar.comment.GetComments(-25, 7, culture, CommentType.Blogs));
                week += BuildRow(ArticleLanguage.month_ago, ar.comment.GetComments(-32, 30, culture, CommentType.Blogs));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.comment.GetComments(-62, 30, culture, CommentType.Blogs));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.comment.GetComments(-92, 30, culture, CommentType.Blogs));
                week += BuildRow(ArticleLanguage.older, ar.comment.GetComments(-122, 99999, culture, CommentType.Blogs));
            }
            else if (dayOfWeek == "Friday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.comment.GetComments(-2, culture, CommentType.Blogs));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.comment.GetComments(-3, culture, CommentType.Blogs));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.comment.GetComments(-4, culture, CommentType.Blogs));
                week += BuildRow(ArticleLanguage.week_ago, ar.comment.GetComments(-5, 7, culture, CommentType.Blogs));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.comment.GetComments(-12, 7, culture, CommentType.Blogs));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.comment.GetComments(-19, 7, culture, CommentType.Blogs));
                week += BuildRow(ArticleLanguage.four_weeks_ago, ar.comment.GetComments(-26, 7, culture, CommentType.Blogs));
                week += BuildRow(ArticleLanguage.month_ago, ar.comment.GetComments(-33, 30, culture, CommentType.Blogs));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.comment.GetComments(-63, 30, culture, CommentType.Blogs));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.comment.GetComments(-93, 30, culture, CommentType.Blogs));
                week += BuildRow(ArticleLanguage.older, ar.comment.GetComments(-123, 99999, culture, CommentType.Blogs));
            }
            else if (dayOfWeek == "Saturday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.comment.GetComments(-2, culture, CommentType.Blogs));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.comment.GetComments(-3, culture, CommentType.Blogs));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.comment.GetComments(-4, culture, CommentType.Blogs));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-5).ToString("dddd")), ar.comment.GetComments(-5, culture, CommentType.Blogs));
                week += BuildRow(ArticleLanguage.week_ago, ar.comment.GetComments(-6, 7, culture, CommentType.Blogs));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.comment.GetComments(-13, 7, culture, CommentType.Blogs));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.comment.GetComments(-20, 7, culture, CommentType.Blogs));
                week += BuildRow(ArticleLanguage.four_weeks_ago, ar.comment.GetComments(-27, 7, culture, CommentType.Blogs));
                week += BuildRow(ArticleLanguage.month_ago, ar.comment.GetComments(-34, 30, culture, CommentType.Blogs));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.comment.GetComments(-64, 30, culture, CommentType.Blogs));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.comment.GetComments(-94, 30, culture, CommentType.Blogs));
                week += BuildRow(ArticleLanguage.older, ar.comment.GetComments(-124, 99999, culture, CommentType.Blogs));
            }
            else if (dayOfWeek == "Sunday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.comment.GetComments(-2, culture, CommentType.Blogs));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.comment.GetComments(-3, culture, CommentType.Blogs));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.comment.GetComments(-4, culture, CommentType.Blogs));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-5).ToString("dddd")), ar.comment.GetComments(-5, culture, CommentType.Blogs));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-6).ToString("dddd")), ar.comment.GetComments(-6, culture, CommentType.Blogs));
                week += BuildRow(ArticleLanguage.week_ago, ar.comment.GetComments(-7, 7, culture, CommentType.Blogs));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.comment.GetComments(-14, 7, culture, CommentType.Blogs));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.comment.GetComments(-21, 7, culture, CommentType.Blogs));
                week += BuildRow(ArticleLanguage.month_ago, ar.comment.GetComments(-28, 30, culture, CommentType.Blogs));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.comment.GetComments(-58, 30, culture, CommentType.Blogs));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.comment.GetComments(-88, 30, culture, CommentType.Blogs));
                week += BuildRow(ArticleLanguage.older, ar.comment.GetComments(-118, 99999, culture, CommentType.Blogs));
            }
            #endregion
            table.InnerHtml = BuildRow(ArticleLanguage.today, ar.comment.GetComments(0, culture, CommentType.Blogs))
                + BuildRow(ArticleLanguage.yesterday, ar.comment.GetComments(-1, culture, CommentType.Blogs)) + week;
            #endregion
            result.AppendLine(table.ToString());
            return result.ToString();
        }
        public static string UnapprovedCommentsDetails(string culture)
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
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.comment.GetComments(-2, culture, CommentType.Unapproved));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.comment.GetComments(-3, culture, CommentType.Unapproved));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.comment.GetComments(-4, culture, CommentType.Unapproved));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-5).ToString("dddd")), ar.comment.GetComments(-5, culture, CommentType.Unapproved));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-6).ToString("dddd")), ar.comment.GetComments(-6, culture, CommentType.Unapproved));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-7).ToString("dddd")), ar.comment.GetComments(-7, culture, CommentType.Unapproved));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.comment.GetComments(-8, 7, culture, CommentType.Unapproved));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.comment.GetComments(-15, 7, culture, CommentType.Unapproved));
                week += BuildRow(ArticleLanguage.four_weeks_ago, ar.comment.GetComments(-22, 7, culture, CommentType.Unapproved));
                week += BuildRow(ArticleLanguage.month_ago, ar.comment.GetComments(-29, 30, culture, CommentType.Unapproved));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.comment.GetComments(-59, 30, culture, CommentType.Unapproved));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.comment.GetComments(-89, 30, culture, CommentType.Unapproved));
                week += BuildRow(ArticleLanguage.older, ar.comment.GetComments(-119, 99999, culture, CommentType.Unapproved));
            }
            else if (dayOfWeek == "Tuesday")
            {
                week = BuildRow(ArticleLanguage.week_ago, ar.comment.GetComments(-2, 7, culture, CommentType.Unapproved));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.comment.GetComments(-9, 7, culture, CommentType.Unapproved));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.comment.GetComments(-16, 7, culture, CommentType.Unapproved));
                week += BuildRow(ArticleLanguage.four_weeks_ago, ar.comment.GetComments(-23, 7, culture, CommentType.Unapproved));
                week += BuildRow(ArticleLanguage.month_ago, ar.comment.GetComments(-30, 30, culture, CommentType.Unapproved));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.comment.GetComments(-60, 30, culture, CommentType.Unapproved));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.comment.GetComments(-90, 30, culture, CommentType.Unapproved));
                week += BuildRow(ArticleLanguage.older, ar.comment.GetComments(-120, 99999, culture, CommentType.Unapproved));
            }
            else if (dayOfWeek == "Wednesday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.comment.GetComments(-2, culture, CommentType.Unapproved));
                week += BuildRow(ArticleLanguage.week_ago, ar.comment.GetComments(-3, 7, culture, CommentType.Unapproved));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.comment.GetComments(-10, 7, culture, CommentType.Unapproved));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.comment.GetComments(-17, 7, culture, CommentType.Unapproved));
                week += BuildRow(ArticleLanguage.four_weeks_ago, ar.comment.GetComments(-24, 7, culture, CommentType.Unapproved));
                week += BuildRow(ArticleLanguage.month_ago, ar.comment.GetComments(-31, 30, culture, CommentType.Unapproved));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.comment.GetComments(-61, 30, culture, CommentType.Unapproved));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.comment.GetComments(-91, 30, culture, CommentType.Unapproved));
            }
            else if (dayOfWeek == "Thursday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.comment.GetComments(-2, culture, CommentType.Unapproved));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.comment.GetComments(-3, culture, CommentType.Unapproved));
                week += BuildRow(ArticleLanguage.week_ago, ar.comment.GetComments(-4, 7, culture, CommentType.Unapproved));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.comment.GetComments(-11, 7, culture, CommentType.Unapproved));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.comment.GetComments(-18, 7, culture, CommentType.Unapproved));
                week += BuildRow(ArticleLanguage.four_weeks_ago, ar.comment.GetComments(-25, 7, culture, CommentType.Unapproved));
                week += BuildRow(ArticleLanguage.month_ago, ar.comment.GetComments(-32, 30, culture, CommentType.Unapproved));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.comment.GetComments(-62, 30, culture, CommentType.Unapproved));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.comment.GetComments(-92, 30, culture, CommentType.Unapproved));
                week += BuildRow(ArticleLanguage.older, ar.comment.GetComments(-122, 99999, culture, CommentType.Unapproved));
            }
            else if (dayOfWeek == "Friday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.comment.GetComments(-2, culture, CommentType.Unapproved));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.comment.GetComments(-3, culture, CommentType.Unapproved));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.comment.GetComments(-4, culture, CommentType.Unapproved));
                week += BuildRow(ArticleLanguage.week_ago, ar.comment.GetComments(-5, 7, culture, CommentType.Unapproved));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.comment.GetComments(-12, 7, culture, CommentType.Unapproved));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.comment.GetComments(-19, 7, culture, CommentType.Unapproved));
                week += BuildRow(ArticleLanguage.four_weeks_ago, ar.comment.GetComments(-26, 7, culture, CommentType.Unapproved));
                week += BuildRow(ArticleLanguage.month_ago, ar.comment.GetComments(-33, 30, culture, CommentType.Unapproved));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.comment.GetComments(-63, 30, culture, CommentType.Unapproved));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.comment.GetComments(-93, 30, culture, CommentType.Unapproved));
                week += BuildRow(ArticleLanguage.older, ar.comment.GetComments(-123, 99999, culture, CommentType.Unapproved));
            }
            else if (dayOfWeek == "Saturday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.comment.GetComments(-2, culture, CommentType.Unapproved));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.comment.GetComments(-3, culture, CommentType.Unapproved));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.comment.GetComments(-4, culture, CommentType.Unapproved));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-5).ToString("dddd")), ar.comment.GetComments(-5, culture, CommentType.Unapproved));
                week += BuildRow(ArticleLanguage.week_ago, ar.comment.GetComments(-6, 7, culture, CommentType.Unapproved));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.comment.GetComments(-13, 7, culture, CommentType.Unapproved));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.comment.GetComments(-20, 7, culture, CommentType.Unapproved));
                week += BuildRow(ArticleLanguage.four_weeks_ago, ar.comment.GetComments(-27, 7, culture, CommentType.Unapproved));
                week += BuildRow(ArticleLanguage.month_ago, ar.comment.GetComments(-34, 30, culture, CommentType.Unapproved));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.comment.GetComments(-64, 30, culture, CommentType.Unapproved));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.comment.GetComments(-94, 30, culture, CommentType.Unapproved));
                week += BuildRow(ArticleLanguage.older, ar.comment.GetComments(-124, 99999, culture, CommentType.Unapproved));
            }
            else if (dayOfWeek == "Sunday")
            {
                week = BuildRow(UppercaseFirst(DateTime.Now.AddDays(-2).ToString("dddd")), ar.comment.GetComments(-2, culture, CommentType.Unapproved));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-3).ToString("dddd")), ar.comment.GetComments(-3, culture, CommentType.Unapproved));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-4).ToString("dddd")), ar.comment.GetComments(-4, culture, CommentType.Unapproved));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-5).ToString("dddd")), ar.comment.GetComments(-5, culture, CommentType.Unapproved));
                week += BuildRow(UppercaseFirst(DateTime.Now.AddDays(-6).ToString("dddd")), ar.comment.GetComments(-6, culture, CommentType.Unapproved));
                week += BuildRow(ArticleLanguage.week_ago, ar.comment.GetComments(-7, 7, culture, CommentType.Unapproved));
                week += BuildRow(ArticleLanguage.two_weeks_ago, ar.comment.GetComments(-14, 7, culture, CommentType.Unapproved));
                week += BuildRow(ArticleLanguage.three_weeks_ago, ar.comment.GetComments(-21, 7, culture, CommentType.Unapproved));
                week += BuildRow(ArticleLanguage.month_ago, ar.comment.GetComments(-28, 30, culture, CommentType.Unapproved));
                week += BuildRow(ArticleLanguage.two_months_ago, ar.comment.GetComments(-58, 30, culture, CommentType.Unapproved));
                week += BuildRow(ArticleLanguage.three_months_ago, ar.comment.GetComments(-88, 30, culture, CommentType.Unapproved));
                week += BuildRow(ArticleLanguage.older, ar.comment.GetComments(-118, 99999, culture, CommentType.Unapproved));
            }
            #endregion
            table.InnerHtml = BuildRow(ArticleLanguage.today, ar.comment.GetComments(0, culture, CommentType.Unapproved))
                + BuildRow(ArticleLanguage.yesterday, ar.comment.GetComments(-1, culture, CommentType.Unapproved)) + week;
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
        #endregion

        #region BildRow helpers
        static string BuildRow(string title, IQueryable<mytrip_articles> articles)
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
                    div.AddCssClass("profile1");
                else
                    div.AddCssClass("profile2");
                TagBuilder alink = new TagBuilder("a");
                alink.MergeAttribute("href", LangLink(article.Culture, "/Article/View/" + article.ArticleId + "/" + article.Path));
                // tooltip
                alink.MergeAttribute("rel", "Article," + article.ArticleId);
                alink.AddCssClass("mtPopupTrigger");
                //
                alink.InnerHtml = article.Title;
                TagBuilder clink = new TagBuilder("a");
                clink.MergeAttribute("href", LangLink(article.Culture, "/Article/Index/1/10/" + article.mytrip_articlescategory.CategoryId 
                    + "/" + article.mytrip_articlescategory.Path));
                // tooltip
                clink.MergeAttribute("rel", "Category," + article.mytrip_articlescategory.CategoryId);
                clink.AddCssClass("mtPopupTrigger");
                //
                clink.InnerHtml = article.mytrip_articlescategory.Title;
                div.InnerHtml = EditDelete(article) + " <b>" + alink + " " + Keys(article.OnlyForRegisterUser, 14) + " " + Globe(article.AllCulture, 14)
                    + " " + Flag(article.Culture, 14) + "</b><br/>" + article.CreateDate.ToString("dd MMMM yyyy HH:mm") + " " + ArticleLanguage.in_the 
                    + " " + clink + " " + ArticleLanguage.by + " " + ProfileLink(article.UserName);
                accordionContent2.InnerHtml += div;
                ctr++;
            }
            accordion2.InnerHtml = accordionTitle2.ToString() + accordionContent2.ToString();
            td.InnerHtml = accordion2.ToString();
            tr.InnerHtml += td.ToString();
            return tr.ToString();
        }
        static string BuildRow(string title, IQueryable<mytrip_articlescategory> categories)
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
                    div.AddCssClass("profile1");
                else
                    div.AddCssClass("profile2");
                TagBuilder link = new TagBuilder("a");
                link.MergeAttribute("href", LangLink(category.Culture, "/Article/Index/1/10/" + category.CategoryId + "/" + category.Path));
                // tooltip
                link.MergeAttribute("rel", "Category," + category.CategoryId);
                link.AddCssClass("mtPopupTrigger");
                //
                link.InnerHtml = category.Title;
                div.InnerHtml += EditDelete(category) + " <b>" + link + " " + Flag(category.Culture, 14) + "</b><br/>" + category.CreateDate.ToString("dd MMMM yyyy HH:mm")
                    + SubCatLink(category) + " " + ArticleLanguage.by + " " + ProfileLink(category.UserName);
                accordionContent2.InnerHtml += div;
                ctr++;
            }
            accordion2.InnerHtml = accordionTitle2.ToString() + accordionContent2.ToString();
            td.InnerHtml = accordion2.ToString();
            tr.InnerHtml += td.ToString();
            return tr.ToString();
        }
        static string BuildRow(string title, IQueryable<mytrip_articlescomments> comments)
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
                link.MergeAttribute("href", LangLink(comment.mytrip_articles.Culture, "/Article/View/" + comment.mytrip_articles.ArticleId + "/" + comment.mytrip_articles.Path + "#" + comment.CommentId));
                // tooltip
                link.MergeAttribute("rel", "Comment," + comment.CommentId);
                link.AddCssClass("mtPopupTrigger");
                //
                link.InnerHtml = comment.mytrip_articles.Title;
                div.InnerHtml += ApproveComment(comment) + EditDelete(comment) + " " + comment.CreateDate.ToString("dd MMMM yyyy HH:mm:ss") + " " + ArticleLanguage.added_by + " " + ProfileLink(comment.UserName, comment.IsAnonym) + " in <b>" + link + " "
                    + Globe(comment.mytrip_articles.AllCulture, 14) + " " + Flag(comment.mytrip_articles.Culture, 14) + "</b>";
                accordionContent2.InnerHtml += div;
                ctr++;
            }
            accordion2.InnerHtml = accordionTitle2.ToString() + accordionContent2.ToString();
            td.InnerHtml = accordion2.ToString();
            tr.InnerHtml += td.ToString();
            return tr.ToString();
        }
        static string BuildRow(string title, IQueryable<mytrip_articlestag> tags)
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
                link.MergeAttribute("href", LangLink(title, "/Article/Index/1/10/" + tag.TagId + "/" + tag.Path));
                link.InnerHtml = tag.TagName;
                div.InnerHtml += EditDelete(tag) + "  <b>" + link + "</b>(" + ar.article.GetInTagCount(title.ToLower(), tag.TagId) + ")   ";
            }
            accordionContent2.InnerHtml += div;
            accordion2.InnerHtml = accordionTitle2.ToString() + accordionContent2.ToString();
            td.InnerHtml = accordion2.ToString();
            tr.InnerHtml += td.ToString();
            return tr.ToString();
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
        static string SubCatLink(mytrip_articlescategory category)
        {
            if (category.SubCategoryId == 0)
                return string.Empty;
            TagBuilder link = new TagBuilder("a");
            link.MergeAttribute("href", LangLink(category.Culture, "/Article/Index/1/10/" + category.SubCategoryId + "/" + category.mytrip_articlescategory2.Path));
            // tooltip
            link.MergeAttribute("rel", "Category," + category.SubCategoryId);
            link.AddCssClass("mtPopupTrigger");
            //
            link.InnerHtml = category.mytrip_articlescategory2.Title;
            return " " + ArticleLanguage.in_the + " " + link.ToString() + " ";
        }
        static string ProfileLink(string username)
        {
            TagBuilder link = new TagBuilder("a");
            link.MergeAttribute("href", "/Home/Profile/" + username);
            link.InnerHtml = username;
            return link.ToString();
        }
        static string ProfileLink(string username, bool isAnonym)
        {
            if (isAnonym)
                return username + "(" + ArticleLanguage.guest + ")";
            else
                return ProfileLink(username);
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
        #endregion

        #region Picture helpers
        static string Flag(string culture, int width)
        {
            return GeneralMethods.Flag(culture,width);
        }
        static string Globe(bool show, int width)
        {
            return GeneralMethods.Globe(show,ArticleLanguage.all_languages,width);
        }
        static string Keys(bool show, int width)
        {
            return GeneralMethods.Keys(show,ArticleLanguage.only_for_register,width);
        }
        static string EditDelete(object obj)
        {
            if (obj is mytrip_articlescategory)
            {
                mytrip_articlescategory category = obj as mytrip_articlescategory;
                string rel = category.CategoryId + "_";
                if (category.Blog || category.SubCategoryId != 0)
                    rel += "false_";
                else
                    rel += "true_";
                if (!LocalisationSetting.unlockAllCulture() || category.Blog || (category.SubCategoryId != 0 && !category.mytrip_articlescategory2.AllCulture))
                    rel += "false_";
                else
                    rel += "true_";
                rel += category.SeparateBlock.ToString().ToLower() + "_" + category.AllCulture.ToString().ToLower();
                return GeneralMethods.ImageLink("editCat_" + category.CategoryId, "#"
                    + category.Path, ArticleLanguage.edit, category.Title, rel, "/images/edite.png", "Edit", 14) + " " + GeneralMethods.ImageLink("deleteCat_"
                    + category.CategoryId, "/Article/DeleteCategory/" + category.CategoryId + "/Archive/" + HttpContext.Current.Request.Path.Replace("/", "(x)")
                    , ArticleLanguage.delete, "", "/images/delete.png", "Delete", 14) + " ";

            }
            else if (obj is mytrip_articles)
            {
                mytrip_articles article = obj as mytrip_articles;
                return GeneralMethods.ImageLink("editArticle_" + article.ArticleId, "/Article/Edit/" + article.ArticleId +"/"+ article.ArticleId + "/Archive/" 
                    + HttpContext.Current.Request.Path.Replace("/", "(x)"), ArticleLanguage.edit, "", "/images/edite.png", "Edit", 14)+" "
                    + GeneralMethods.ImageLink("deleteArticle_" + article.CategoryId, "/Article/Delete/" + article.ArticleId + "/Archive/" 
                    + HttpContext.Current.Request.Path.Replace("/", "(x)"), ArticleLanguage.delete, "", "/images/delete.png", "Delete", 14);
            }
            else if (obj is mytrip_articlescomments)
            {
                mytrip_articlescomments comment = obj as mytrip_articlescomments;
                return GeneralMethods.ImageLink("editComment_" + comment.CommentId, "/Article/EditComment/" + comment.CommentId + "/" + comment.ArticleId + "/Archive/"
                    + HttpContext.Current.Request.Path.Replace("/", "(x)"), ArticleLanguage.edit, "", "/images/edite.png", "Edit", 14) + " "
                    + GeneralMethods.ImageLink("deleteComment_" + comment.CommentId, "/Article/DeleteComment/" + comment.CommentId + "/" + comment.ArticleId + "/Archive/"
                    + HttpContext.Current.Request.Path.Replace("/", "(x)"), ArticleLanguage.delete, "", "/images/delete.png", "Delete", 14);

            }
            else if (obj is mytrip_articlestag)
            {
                mytrip_articlestag tag = obj as mytrip_articlestag;
                string rel = tag.TagId + "_false_false_false_false";
                return GeneralMethods.ImageLink("editCat_" + tag.TagId, ""+ tag.Path, ArticleLanguage.edit, tag.TagName,
                    rel, "/images/edite.png", "Edit", 14) + " " + GeneralMethods.ImageLink("deleteCat_" + tag.TagId,
                    "/Article/DeleteCategory/" + tag.TagId + "/(Tag)Archive/" + HttpContext.Current.Request.Path.Replace("/", "(x)")
                    , ArticleLanguage.delete, "", "/images/delete.png", "Delete", 14);
            }
            return "";
        }
        static string ApproveComment(mytrip_articlescomments comment)
        {
            if (!comment.IsApproved)
            {
                return GeneralMethods.ImageLink("approveComment_" + comment.CommentId,"/Article/ApproveComment/" + comment.CommentId +"/"+comment.ArticleId+ "/Archive/"
                    + HttpContext.Current.Request.Path.Replace("/", "(x)"), ArticleLanguage.approve_comment, "", "/images/approved.png", "Delete", 14);
            }
            else
                return "";
        }
        #endregion
    }
}
