using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;
using mtm.Core.Models;
using System.Collections;
using mtm.Core.Helpers;
using mtm.Articles.Repository;
using mtm.Core.Repository;
using mtm.Articles.Repository.DataEntities;
using mtm.Core;
using mtm.Core.Settings;

namespace mtm.Articles.Helpers
{
    public static class ArticlesHelper
    {
        #region Show Edit Delete Rating Rss .......
        public static HtmlString ArticleRssLink(this HtmlHelper html, string title, string path, int id, int width)
        {
            if (path.StartsWith("(Search)"))
            { return null; }
            else
            {
                StringBuilder result = new StringBuilder();
                TagBuilder img = new TagBuilder("img");
                img.MergeAttribute("src", "/Theme/" + ThemeSetting.theme() + "/images/rss.png");
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
                return new HtmlString(result.ToString());
            }
        }

        public static string _ArticleRssLink(string title, string path, int id, int width)
        {
            if (path.StartsWith("(Search)"))
            { return string.Empty; }
            else
            {
                StringBuilder result = new StringBuilder();
                TagBuilder img = new TagBuilder("img");
                img.MergeAttribute("src", "/Theme/" + ThemeSetting.theme() + "/images/rss.png");
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

        public static HtmlString EditorCategory(this HtmlHelper html, bool ShowAddCategory, bool ShowAddSubCategory, bool ShowAddArticle, bool ShowAddBlog, bool ShowAddPost, int id)
        {
            CategoryRepository cr = new CategoryRepository();
            mytrip_articlescategory category = cr.GetCategory(id);
            StringBuilder result = new StringBuilder();
            if (isUserHasRights(category, true))
            {
                TagBuilder img = new TagBuilder("img");
                img.MergeAttribute("src", "/Theme/" + ThemeSetting.theme() + "/images/add.png");
                img.MergeAttribute("alt", "Add");
                img.MergeAttribute("style", "height:14px;");
                if (ShowAddCategory)
                {
                    TagBuilder CreateCategoryP = new TagBuilder("div");
                    TagBuilder CreateCategory = new TagBuilder("a");
                    CreateCategory.MergeAttribute("href", "#");
                    CreateCategory.MergeAttribute("id", "modalCategory_" + category.CategoryId);
                    CreateCategory.MergeAttribute("rel", "0_true_true_false_false");
                    CreateCategory.InnerHtml = ArticleLanguage.add_category + " " + img.ToString();
                    CreateCategoryP.InnerHtml = CreateCategory.ToString();
                    result.AppendLine(CreateCategoryP.ToString());
                }
                if (ShowAddSubCategory)
                {
                    TagBuilder CreateSubCategoryP = new TagBuilder("div");
                    TagBuilder CreateSubCategory = new TagBuilder("a");
                    CreateSubCategory.MergeAttribute("href", "#");
                    CreateSubCategory.MergeAttribute("id", "modalCategory_" + category.CategoryId);
                    if (category.AllCulture)
                        CreateSubCategory.MergeAttribute("rel", category.CategoryId + "_false_true_false_false");
                    else
                        CreateSubCategory.MergeAttribute("rel", category.CategoryId + "_false_false_false_false");
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
                    CreateSubCategory.MergeAttribute("id", "modalCategory_" + category.CategoryId);
                    CreateSubCategory.MergeAttribute("rel", category.CategoryId + "_false_false_false_false");
                    CreateSubCategory.MergeAttribute("href", "#");
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
            return new HtmlString(result.ToString());
        }

        public static HtmlString EditDeleteCategory(this HtmlHelper html, bool ShowEditDelete, int id, string path)
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
            if (ShowEditDelete)
            {
                if (isUserHasRights(obj, false))
                {
                    string a = "";
                    if (obj is mytrip_articlescategory)
                    {
                        var cat = obj as mytrip_articlescategory;
                        string rel = id + "_";
                        if (cat.Blog || cat.SubCategoryId != 0)
                            rel += "false_";
                        else
                            rel += "true_";
                        if (!LocalisationSetting.unlockAllCulture() || cat.Blog || (cat.SubCategoryId != 0 && !cat.mytrip_articlescategory2.AllCulture))
                            rel += "false_";
                        else
                            rel += "true_";
                        rel += cat.SeparateBlock.ToString().ToLower() + "_" + cat.AllCulture.ToString().ToLower();
                        a += GeneralMethods.ImageLink("modalCategory_" + cat.CategoryId, "", ArticleLanguage.edit, cat.Title, rel, "/images/edite.png", "Edit", 14) 
                            + " " + GeneralMethods.ImageLink("deleteCat_" + cat.CategoryId, "/Article/DeleteCategory/" + cat.CategoryId + "/" + cat.Path,
                            ArticleLanguage.delete, "", "/images/delete.png", "Delete", 14);
                    }
                    else
                    {
                        var tag = obj as mytrip_articlestag;
                        string rel = id + "_false_false_false_false";
                        a += GeneralMethods.ImageLink("modalCategory_" + tag.TagId, "", ArticleLanguage.edit, tag.TagName, rel, "/images/edite.png", "EditTag", 14)
                            + " " + GeneralMethods.ImageLink("deleteCat_" + tag.TagId, "/Article/DeleteCategory/" + tag.TagId + "/" + tag.Path,
                            ArticleLanguage.delete, "", "/images/delete.png", "Delete", 14);
                    }
                    result.AppendLine(a);
                }
            }
            return new HtmlString(result.ToString());
        }

        public static HtmlString ParrentCategory(this HtmlHelper html, mytrip_articlescategory parentCat, string path)
        {
            StringBuilder result = new StringBuilder();
            if (parentCat.CategoryId != -1 && parentCat.SubCategoryId == 0 && parentCat.Path != path)
            {
                TagBuilder h2 = new TagBuilder("h2");
                h2.AddCssClass("title");
                TagBuilder a = new TagBuilder("a");
                a.MergeAttribute("href", "/Article/Index/1/10/" + parentCat.CategoryId + "/" + parentCat.Path);
                a.InnerHtml = parentCat.Title;
                h2.InnerHtml = a.ToString();
                result.AppendLine(h2.ToString());
            }
            return new HtmlString(result.ToString());
        }

        public static HtmlString ShowDetailsBlog(this HtmlHelper html, bool showDetailsBlog, mytrip_articlescategory parentCat)
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
                TagBuilder divGravatar = new TagBuilder("a");
                divGravatar.MergeAttribute("href", "/Home/Profile/" + parentCat.UserName);
                divGravatar.MergeAttribute("title", ArticleLanguage.view_user_profile);
                divGravatar.InnerHtml = AvatarHelper.Avatar(html, parentCat.UserEmail).ToString();
                result.AppendLine("<div style='position: relative; float: right'>" + divGravatar + "</div>");
                TagBuilder profile = new TagBuilder("a");
                profile.MergeAttribute("href", "/Home/Profile/" + parentCat.UserName);
                profile.MergeAttribute("title", ArticleLanguage.view_user_profile);
                profile.InnerHtml = parentCat.UserName;
                result.AppendLine(ArticleLanguage.author + ": " + profile + "<br/>");
                result.AppendLine(ArticleLanguage.views + ": " + parentCat.Views + "<br/>");
                result.AppendLine(ArticleLanguage.posts + ": " + CountPosts(parentCat.CategoryId) + "<br/>");
                result.AppendLine(ArticleLanguage.create_date + ": " + string.Format("{0:dd MMMM yy}", parentCat.CreateDate));
                td.InnerHtml = result.ToString();
                tr.InnerHtml = td.ToString();
                table.InnerHtml = tr.ToString();
                return new HtmlString(table.ToString());
            }
            else
            {
                return null;
            }
        }

        public static HtmlString ShowTitleArticle(this HtmlHelper html, mytrip_articles article, int returnId)
        {
            TagBuilder h2 = new TagBuilder("h1");
            h2.AddCssClass("title");
            if (isUserHasRights(article, false))
            {
                string a = GeneralMethods.ImageLink("editArticle_" + article.ArticleId, "/Article/Edit/" + article.ArticleId + "/" + article.ArticleId
                    , ArticleLanguage.edit, "", "/images/edite.png", "Edit", 14) + " " + GeneralMethods.ImageLink("deleteArticle_" + article.CategoryId,
                    "/Article/Delete/" + article.ArticleId , ArticleLanguage.delete, "", "/images/delete.png", "Delete", 14);
                h2.InnerHtml += a + " ";
            }
            h2.InnerHtml += article.Title;
            return new HtmlString(h2.ToString());
        }
        public static HtmlString ShowBodyArticle(this HtmlHelper html, mytrip_articles article)
        {
            string body = article.Body.Replace("[SilverWmvStart]", StartWmvPlaer()).Replace("[SilverWmvEnd]", EndWmvPlaer());

            return new HtmlString(body);
        }
        public static HtmlString ArticleOptions(this HtmlHelper html, mytrip_articles article, bool isSubscribed)
        {
            if (!article.ApprovedComment || !HttpContext.Current.User.Identity.IsAuthenticated || !EmailSetting.unlockSendEmail())
                return null;
            TagBuilder input = new TagBuilder("input");
            input.MergeAttribute("type", "submit");
            input.MergeAttribute("name", "id");
            input.MergeAttribute("id", "subscribe");
            input.MergeAttribute("value", article.ArticleId.ToString());
            if (!isSubscribed)
            {
                input.MergeAttribute("style", "background:url('/Theme/" + ThemeSetting.theme() + "/images/alert.png')");
                input.MergeAttribute("title", ArticleLanguage.subscribe_comments);
            }
            else
            {
                input.MergeAttribute("style", "background:url('/Theme/" + ThemeSetting.theme() + "/images/noalert.png')");
                input.MergeAttribute("title", ArticleLanguage.unsubscribe_comments);
            }
            input.AddCssClass("otheroptions");
            TagBuilder otheroptions = new TagBuilder("div");
            otheroptions.MergeAttribute("id", "otheroptions");
            otheroptions.MergeAttribute("style", "text-align:right");
            otheroptions.InnerHtml = input.ToString();
            TagBuilder form = new TagBuilder("form");
            form.MergeAttribute("action", "/Article/SubscribeComments");
            form.MergeAttribute("method", "post");
            form.MergeAttribute("onclick", "Sys.Mvc.AsyncForm.handleClick(this, new Sys.UI.DomEvent(event));");
            form.MergeAttribute("onsubmit", "Sys.Mvc.AsyncForm.handleSubmit(this, new Sys.UI.DomEvent(event), { insertionMode: Sys.Mvc.InsertionMode.replace, updateTargetId:\"otheroptions\" });");
            form.InnerHtml = otheroptions.ToString();

            return new HtmlString(form.ToString());
        }

        public static HtmlString ShowArticleTags(this HtmlHelper html, mytrip_articles article)
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
            if (count > 0)
                return new HtmlString(ArticleLanguage.tags + " " + result.ToString() + "<br/>");
            else
                return null;
        }

        public static HtmlString ShowRelated(this HtmlHelper html, mytrip_articles article)
        {
            IArticleRepository ar = new IArticleRepository();
            TagBuilder list = new TagBuilder("ol");
            list.AddCssClass("related");
            foreach (var item in ar.article.GetRelated(LocalisationSetting.culture(), article.mytrip_articlescategory.Blog, article.Title, 5))
            {
                TagBuilder li = new TagBuilder("li");
                TagBuilder a = new TagBuilder("a");
                a.MergeAttribute("href", "/Article/View/" + item.ArticleId + "/" + item.Path);
                a.MergeAttribute("title", item.Title);
                a.InnerHtml = item.Title;
                li.InnerHtml = a.ToString();
                list.InnerHtml += li.ToString();
            }
            return new HtmlString(ArticleLanguage.related_links + list.ToString());
        }

        public static HtmlString ListCategories(this HtmlHelper html, IQueryable<mytrip_articlescategory> categories, string path)
        {
            StringBuilder result = new StringBuilder();
            bool _start = true;
            foreach (var category in categories)
            {
                int count = CountPosts(category.CategoryId);
                if (category.Blog && category.SubCategoryId == 0)
                {
                    #region представление для блогов
                    TagBuilder tr = new TagBuilder("tr");
                    TagBuilder td_first = new TagBuilder("td");
                    td_first.AddCssClass("artcontent");
                    if (isUserHasRights(category, false))
                    {
                        td_first.InnerHtml = GeneralMethods.ImageLink("modalCategory_" + category.CategoryId, "#", ArticleLanguage.edit,
                            category.Title, category.CategoryId + "_false_false_false_false", "/images/edite.png", "Edit", 14) + " "
                            + GeneralMethods.ImageLink("deleteCat_" + category.CategoryId, "/Article/DeleteCategory/" + category.CategoryId + "/"
                            + category.Path, ArticleLanguage.delete, "", "/images/delete.png", "Delete", 14) + " ";
                    }
                    TagBuilder a_category = new TagBuilder("a");
                    a_category.MergeAttribute("href", "/Article/Index/1/10/" + category.CategoryId + "/" + category.Path);
                    a_category.InnerHtml = category.Title;
                    td_first.InnerHtml += a_category.ToString();

                    TagBuilder divGravatar = new TagBuilder("a");
                    divGravatar.MergeAttribute("href", "/Home/Profile/" + category.UserName);
                    divGravatar.MergeAttribute("title", ArticleLanguage.view_user_profile);
                    divGravatar.InnerHtml = AvatarHelper.Avatar(html, category.UserEmail).ToString();
                    td_first.InnerHtml = "<div class=\"right\">" + divGravatar + "</div>" + "<h3 class=\"hometitle\">" + td_first.InnerHtml + "</h3>";
                    TagBuilder profile = new TagBuilder("a");
                    profile.MergeAttribute("href", "/Home/Profile/" + category.UserName);
                    profile.MergeAttribute("title", ArticleLanguage.view_user_profile);
                    profile.InnerHtml = category.UserName;
                    TagBuilder td_last = new TagBuilder("td");
                    td_last.AddCssClass("artspcific");
                    td_last.InnerHtml += "<div class=\"info\">" + ArticleLanguage.author + ": " + profile + "<br/>";
                    td_last.InnerHtml += ArticleLanguage.views + ": " + category.Views + "<br/>";
                    td_last.InnerHtml += ArticleLanguage.posts + ": " + count + "<br/>";
                    td_last.InnerHtml += string.Format("{0:dd MMMM yyyy}", category.CreateDate) + "</div>";
                    tr.InnerHtml = td_last.ToString() + td_first.ToString();
                    TagBuilder table = new TagBuilder("table");
                    table.AddCssClass("content");
                    table.InnerHtml = tr.ToString();
                    result.AppendLine("<div class=\"content\">" + table.ToString() + "</div><div class=\"last\"></div>");
                    #endregion
                }
                else
                {
                    #region представление для статей
                    TagBuilder h4 = new TagBuilder("b");
                    h4.AddCssClass("title");
                    if (isUserHasRights(category, false) || (category.mytrip_articlescategory2.Blog && isUserHasRights(category, false)))
                    {
                        //rel="catId_showMenu_showLang_checkMenu_checkLang"
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
                        string a = GeneralMethods.ImageLink("modalCategory_" + category.CategoryId, "#", ArticleLanguage.edit, category.Title, rel, "/images/edite.png", "Edit", 14)
                            + " " + GeneralMethods.ImageLink("deleteCat_" + category.CategoryId, "/Article/DeleteCategory/" + category.CategoryId + "/"+ category.Path, 
                            ArticleLanguage.delete, "", "/images/delete.png", "Delete", 14);
                        h4.InnerHtml = a + " ";
                    #endregion
                    }
                    TagBuilder a_category = new TagBuilder("a");
                    a_category.MergeAttribute("href", "/Article/Index/1/10/" + category.CategoryId + "/" + category.Path);
                    if (category.mytrip_articlescategory2!=null&&category.mytrip_articlescategory2.Blog)
                    {
                        a_category.InnerHtml = ArticleLanguage.topic+": " + category.Title;
                        h4.InnerHtml += a_category.ToString() + "<br/><span>" + ArticleLanguage.create_date + ": " + string.Format("{0:dd MMMM yyyy}", category.CreateDate) +
                            ", " + ArticleLanguage.views + ": " + category.Views + "," + ArticleLanguage.posts + ": " + count + "</span><br/>";
                        result.AppendLine(h4.ToString());
                    }
                    else
                    {
                        a_category.InnerHtml = category.Title;
                        h4.InnerHtml += a_category.ToString();
                        if (_start)
                        {
                            result.AppendLine(h4.ToString());
                            _start = false;
                        }
                        else
                            result.AppendLine(" | " + h4.ToString());
                    }

                }
            }
            return new HtmlString(result.ToString());
        }

        public static HtmlString ArticleRating(this HtmlHelper html, bool enable, bool active, decimal totalvotes, int votescount)
        {
            return new HtmlString(GeneralMethods.CoreRating(enable, active, (double)totalvotes, votescount));
        }

        public static HtmlString ArticleRating(this HtmlHelper html, bool enable, bool active, decimal totalvotes)
        {
            return new HtmlString(GeneralMethods.CoreRating(enable, active, (double)totalvotes, -1));
        }

        public static HtmlString ArticleSpecification(this HtmlHelper html, mytrip_articles article, string path)
        {
            return new HtmlString(ArticleInfo(path, article, true));
        }

        public static HtmlString ArticleSpecification(this HtmlHelper html, mytrip_articles article)
        {
            return new HtmlString(ArticleInfo("Articles", article, true));
        }

        public static HtmlString ShowComments(this HtmlHelper html, mytrip_articles article, int returnId)
        {
            StringBuilder result = new StringBuilder();
            #region Comments title
            bool blog = article.mytrip_articlescategory.Blog;
            TagBuilder tlegend = new TagBuilder("h3");
            tlegend.AddCssClass("title");
            if (!blog && isUserHasRights(article, false))
            {
                TagBuilder imgOnOff = new TagBuilder("img");
                if (!article.ApprovedComment)
                {
                    imgOnOff.MergeAttribute("src", "/Theme/" + ThemeSetting.theme() + "/images/approved.png");
                    imgOnOff.MergeAttribute("title", ArticleLanguage.open_comments);
                    imgOnOff.MergeAttribute("style", "width:14px;");
                }
                else
                {
                    TagBuilder imgClose = new TagBuilder("img");
                    imgOnOff.MergeAttribute("src", "/Theme/" + ThemeSetting.theme() + "/images/noapproved.png");
                    imgOnOff.MergeAttribute("title", ArticleLanguage.close_comments);
                    imgOnOff.MergeAttribute("style", "width:14px;");
                }
                TagBuilder OnOffComments = new TagBuilder("a");
                OnOffComments.MergeAttribute("href", "/Article/OnOffComments/" + article.ArticleId + "/" + returnId + "/" + article.Path);
                OnOffComments.InnerHtml = imgOnOff.ToString();
                tlegend.InnerHtml = OnOffComments.ToString();
            }
            if (!article.ApprovedComment && ModuleSetting.viewInfoClosedComments())
            {
                tlegend.InnerHtml += " " + ArticleLanguage.comments_closed;
            }
            else if (article.ApprovedComment)
            {
                tlegend.InnerHtml += " " + ArticleLanguage.comments;
                if (article.mytrip_articlescomments.Count == 0)
                    tlegend.InnerHtml += " ( " + ArticleLanguage.no_comments_yet + " ) ";
            }
            result.AppendLine(tlegend.ToString());
            #endregion
            #region Comments
            int count = 1;
            foreach (var comment in article.mytrip_articlescomments.OrderBy(x => x.CreateDate))
            {
                bool hasRights = isUserHasRights(comment, false);
                if ((!article.ModerateComments || comment.IsApproved) || (article.ModerateComments && hasRights))
                {
                    TagBuilder fieldset = new TagBuilder("div");
                    if (comment.IsApproved)
                        fieldset.AddCssClass("content");
                    else
                        fieldset.AddCssClass("noappr");
                    fieldset.MergeAttribute("id", comment.CommentId.ToString());
                    TagBuilder legend = new TagBuilder("div");
                    legend.AddCssClass("info");
                    if (hasRights)
                    {
                        if (article.ModerateComments && !comment.IsApproved && isUserHasRights(article, false))
                            legend.InnerHtml += GeneralMethods.ImgInput("/images/approved.png", "/Article/ApproveComment/" + comment.CommentId + "/" + returnId + "/" + HttpContext.Current.Request.Path.Replace("/", "(x)"), "rename", 14) + " ";
                        string a = GeneralMethods.ImageLink("inlineEditComment_" + comment.CommentId, "#", ArticleLanguage.edit, comment.CommentId.ToString(), "/images/edite.png", "Edit", 14) + " "
                    + GeneralMethods.ImageLink("deleteComment_" + comment.CommentId, "/Article/DeleteComment/" + comment.CommentId + "/" + comment.ArticleId + "/"
                    + HttpContext.Current.Request.Path.Replace("/", "(x)") , ArticleLanguage.delete, "", "/images/delete.png", "Delete", 14);
                        legend.InnerHtml += a + " ";
                    }
                    TagBuilder profile = new TagBuilder("a");
                    profile.MergeAttribute("id", "user" + comment.CommentId);
                    if (comment.IsAnonym)
                        profile.SetInnerText(comment.UserName + "(" + ArticleLanguage.guest + ")");
                    else
                    {
                        profile.MergeAttribute("href", "/Home/Profile/" + comment.UserName);
                        profile.MergeAttribute("title", ArticleLanguage.view_user_profile);
                        profile.InnerHtml = comment.UserName;
                    }
                    legend.InnerHtml += "  #" + count + " " + profile + " " + comment.CreateDate.ToString("dd MMMM yy HH:mm");
                    if (article.CommentVotes)
                        legend.InnerHtml += "<div  id='voteCommentDiv" + comment.CommentId + "' class='right'><div class='right' style='margin-right:10px;text-align:center;width:50px;height:11px'>" + CommentVotes(comment.CommentId, comment.Votes) + "</div></div>";
                    TagBuilder divGravatar = new TagBuilder("div");
                    TagBuilder quote = new TagBuilder("a");
                    if (!comment.IsApproved)
                        legend.InnerHtml += " (" + ArticleLanguage.waiting_for_moderation + ")";
                    else if (article.IncludeAnonymComment || HttpContext.Current.User.Identity.IsAuthenticated)
                    {
                        quote.MergeAttribute("href", "#");
                        quote.MergeAttribute("id", "quote" + comment.CommentId);
                        quote.InnerHtml = ArticleLanguage.quote;
                    }
                    if (ProfileSetting.unlockGravatar())
                    {
                        divGravatar.MergeAttribute("style", "position: relative; float: right");
                        TagBuilder gravatar = new TagBuilder("a");
                        gravatar.MergeAttribute("href", "/Home/Profile/" + HttpContext.Current.User.Identity.Name);
                        gravatar.MergeAttribute("title", ArticleLanguage.view_user_profile);
                        gravatar.InnerHtml = AvatarHelper.Avatar(html, comment.UserEmail, new { width = 50 }).ToString();
                        divGravatar.InnerHtml = gravatar.ToString();
                    }
                    fieldset.InnerHtml = "<table class='comment'><tr><td class='first'>" + legend.ToString() + "<div class='comment'>" + comment.Body + "</div>"
                           + "<div class='right'><div class='info'>" + quote + "</div></div>"
                           + "</td><td class='last'>" + divGravatar.ToString() + "</td></tr></table>";
                        result.AppendLine(fieldset.ToString() + "<div class='last'></div>");
                   
                    count++;
                }
            }
            #endregion
            return new HtmlString(result.ToString());
        }
        #endregion

        #region Article Pagers
        public static HtmlString ArticlePager(this HtmlHelper html, int[] pagesIds, string path)
        {
            if (pagesIds == null || pagesIds.Length == 1)
                return null;
            TagBuilder div = new TagBuilder("div");
            div.MergeAttribute("style", "float:left;");
            div.InnerHtml = Pager(html, pagesIds, "...", path);
            return new HtmlString(div.ToString());
        }
        private static string GetIdx(int[] pagesIds, int pageIndex)
        {
            for (int i = 0; i < pagesIds.Length; i++)
            {
                if (pageIndex == i + 1)
                    return pagesIds[i].ToString();
            }
            return "";
        }
        private static string Pager(this HtmlHelper html, int[] pagesIds, string staticText, string path)
        {
            int pageIndex = 1;
            int pageTotal = pagesIds.Length;
            string[] urlpath = HttpContext.Current.Request.Path.Split('/');
            string Controller = "/" + urlpath[1] + "/" + urlpath[2] + "/";
            int articleId = int.Parse(urlpath[3]);
            for (int i = 0; i < pagesIds.Length; i++)
            {
                if (pagesIds[i] == articleId)
                {
                    pageIndex = i + 1;
                    break;
                }
            }
            StringBuilder result = new StringBuilder();
            result.AppendLine("<div class='left'>" + CoreLanguage.pagerPage + "</div>");
            if (pageIndex == 1)
            {
                result.AppendLine(GeneralMethods.Pager(string.Empty, "1", true, "left"));
            }
            else
            {
                result.AppendLine(GeneralMethods.Pager(Controller + GetIdx(pagesIds, 1) + "/" + path, "1", false, "left"));
            }
            int _pageIndex = pageIndex;
            if (pageIndex == 1 || pageIndex == 3)
                _pageIndex = 2;
            if (pageIndex > 3)
                _pageIndex = pageIndex - 2;
            int _page_Index;
            if (pageIndex <= 4)
            {
                for (_page_Index = _pageIndex; pageTotal >= _page_Index; _page_Index++)
                {
                    if (_page_Index == pageIndex)
                    {
                        result.AppendLine(GeneralMethods.Pager(string.Empty, _page_Index.ToString(), true, "left"));
                    }
                    else
                    {
                        result.AppendLine(GeneralMethods.Pager(Controller + GetIdx(pagesIds, _page_Index) + "/" + path, _page_Index.ToString(), false, "left"));
                    }
                    if (_page_Index == pageTotal - 1)
                        break;
                    if (_page_Index == pageIndex + 3)
                        break;
                } if (_page_Index <= pageIndex + 3 && _page_Index <= pageTotal - 3)
                {
                    result.AppendLine(GeneralMethods.pagerStaticText(staticText));
                }
            }
            else
            {
                if (pageIndex > 5)
                {
                    result.AppendLine(GeneralMethods.pagerStaticText(staticText));
                }
                for (_page_Index = _pageIndex - 1; pageTotal >= _page_Index; _page_Index++)
                {
                    if (_page_Index == pageIndex)
                    {
                        result.AppendLine(GeneralMethods.Pager(string.Empty, _page_Index.ToString(), true, "left"));
                    }
                    else
                    {
                        result.AppendLine(GeneralMethods.Pager(Controller + GetIdx(pagesIds, _page_Index) + "/" + path, _page_Index.ToString(), false, "left"));
                    }
                    if (_page_Index == pageTotal - 1)
                        break;
                    if (_page_Index == pageIndex + 3)
                        break;
                } if (_page_Index <= pageIndex + 3 && _page_Index <= pageTotal - 3)
                {
                    result.AppendLine(GeneralMethods.pagerStaticText(staticText));
                }
            }
            if (pageTotal > 5 && pageIndex == pageTotal - 5)
            {
                result.AppendLine(GeneralMethods.pagerStaticText(staticText));
            }
            if (pageTotal >= 3)
            {
                if (pageTotal == pageIndex)
                {
                    result.AppendLine(GeneralMethods.Pager(string.Empty, pageTotal.ToString(), true, "left"));
                }
                else
                {
                    result.AppendLine(GeneralMethods.Pager(Controller + GetIdx(pagesIds, pageTotal) + "/" + path, pageTotal.ToString(), false, "left"));
                }
            }
            return result.ToString();
        }

        public static HtmlString SortPager(this HtmlHelper html, string path, int categoryId)
        {
            path = path.Replace("(Viewed)", "").Replace("(Rated)", "");
            TagBuilder div = new TagBuilder("div");
            div.AddCssClass("right");
            div.MergeAttribute("id", "populararticles");
            TagBuilder recentlink = new TagBuilder("a");
            recentlink.MergeAttribute("href", "/Article/Index/1/10/" + categoryId + "/" + path);
            recentlink.InnerHtml = ArticleLanguage.by_date;
            TagBuilder ratedlink = new TagBuilder("a");
            ratedlink.MergeAttribute("href", "/Article/Index/1/10/" + categoryId + "/(Rated)" + path);
            ratedlink.InnerHtml = ArticleLanguage.most_rated;
            TagBuilder viewedlink = new TagBuilder("a");
            viewedlink.MergeAttribute("href", "/Article/Index/1/10/" + categoryId + "/(Viewed)" + path);
            viewedlink.InnerHtml = ArticleLanguage.most_viewed;
            div.InnerHtml = recentlink.ToString() + " | " + ratedlink.ToString() + " | " + viewedlink.ToString();

            return new HtmlString(div.ToString());
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
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
                return false;
            if (MytripUser.UserInRole(ModuleSetting.roleChiefEditor()))
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
                        isBlog = category.mytrip_articlescategory2.Blog || category.Blog;
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
                bool articleEditor = MytripUser.UserInRole(ModuleSetting.roleArticleEditor());
                if (MytripUser.UserInRole(ModuleSetting.roleBlogger()) || articleEditor)
                {
                    if (HttpContext.Current.User.Identity.Name == articleUserName || HttpContext.Current.User.Identity.Name == categoryUserName || HttpContext.Current.User.Identity.Name == category2UserName)
                        return true;
                    if (forAdd && articleEditor && (!isBlog || obj == null))
                        return true;
                }
            }
            return false;
        }
        static int CountPosts(int categoryId)
        {
            IArticleRepository ar = new IArticleRepository();
            return ar.article.GetCount(categoryId);
        }
        #endregion

        /*PRIVATE*/
        #region Private Helpers
        private static string StartWmvPlaer()
        {
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
                                        "<MediaSource>http://" + CoreSetting.applicationName();
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
            bool blog = article.mytrip_articlescategory.Blog;
            path = path.Replace("(Viewed)", "").Replace("(Rated)", "").Replace("(Recent)", "");
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
                    if (article.mytrip_articlescategory.SubCategoryId == 0)
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
                if (!blog && ModuleSetting.viewInfoAuthorArticle())
                    result.AppendLine(ArticleLanguage.author + ": " + profile + "<br/>");
                else if (blog)
                    result.AppendLine(ArticleLanguage.author + ": " + profile + "<br/>");
                if (!blog && ModuleSetting.viewInfoViewsArticle())
                    result.AppendLine(ArticleLanguage.views + ": " + article.Views + "<br/>");
                else if (blog)
                    result.AppendLine(ArticleLanguage.views + ": " + article.Views + "<br/>");
                if (article.ApprovedComment)
                    result.AppendLine(ArticleLanguage.comments + ": " + article.mytrip_articlescomments.Where(x => x.IsApproved).Count() + "<br/>");
                result.AppendLine(string.Format("{0:dd MMMM yyyy}", article.CreateDate));
            }
            return result.ToString();
        }
        static string CommentVotes(int id,int votes)
        {
            string result = GeneralMethods.ImageLink("voteComment_" + id, "#", "-", "false", id.ToString(), "/images/minus.png", "Minus", 11)
                +" <b>"+votes+"</b> "+ GeneralMethods.ImageLink("voteComment_" + id, "#", "+","true", id.ToString(), "/images/plus.png", "Plus", 11);
            TagBuilder minus = new TagBuilder("a");
            return result;
        }
        #endregion
    }
}
