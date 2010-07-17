<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Articles.Models.ArticleSettingModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%--************************************************************
Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich
To learn more about Mytrip.Mvc.Entity visit
http://mytripmvc.codeplex.com
http://starterkitmytripmvc.codeplex.com
mytripmvc@gmail.com
license: Microsoft Public License (Ms-PL)
***********************************************************--%>
<%=Html.PageTitle(ArticleLanguage.article_setting, "/")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="title"><%=ArticleLanguage.article_setting %></h2>
    <% Html.EnableClientValidation(); %>
    <% using (Html.BeginForm()) {%>
        
        <div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content2">
         
         
            <div class="editor-field">
                <%= Html.CheckBoxFor(model => model.articles)%>
                <%=Html.MytripLabelFor("articles",ArticleLanguage.unlock_articles) %>
            </div>
            <div class="editor-field">
                <%= Html.CheckBoxFor(model => model.blogs)%>
                <%=Html.MytripLabelFor("blogs", ArticleLanguage.unlock_blogs)%>
            </div>
            <div id="_closecount" style="display: none">
            <div class="editor-label">
                <%=Html.MytripLabelFor("countCommentForBlogs", ArticleLanguage.countCommentForBlogs)%>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.countCommentForBlogs)%>
                <%= Html.ValidationMessageFor(model => model.countCommentForBlogs) %>
            </div></div>
            <div class="editor-field">
                <%= Html.CheckBoxFor(model => model.closecountCommentForBlogs, new { id = "closecount" })%>
                <%=Html.MytripLabelFor("closecount", ArticleLanguage.closecountCommentForBlogs)%>
            </div>
            <div class="editor-field">
                <%= Html.CheckBoxFor(model => model.viewInfoAuthorArticle)%>
                <%=Html.MytripLabelFor("viewInfoAuthorArticle", ArticleLanguage.viewInfoAuthorArticle)%>
            </div>
            <div class="editor-field">
                <%= Html.CheckBoxFor(model => model.viewInfoViewsArticle)%>
                <%=Html.MytripLabelFor("viewInfoViewsArticle", ArticleLanguage.viewInfoViewsArticle)%>
            </div>
             <div class="editor-field">
                <%= Html.CheckBoxFor(model => model.showRelatedLinks)%>
                <%=Html.MytripLabelFor("showRelatedLinks", ArticleLanguage.showRelatedLinks)%>
            </div>
            <div class="editor-field">
                <%= Html.CheckBoxFor(model => model.viewInfoClosedComments)%>
                <%=Html.MytripLabelFor("viewInfoClosedComments", ArticleLanguage.viewInfoClosedComments)%>
            </div>
            <div class="editor-field">
                <%= Html.CheckBoxFor(model => model.replaceСommentsEmail)%>
                <%=Html.MytripLabelFor("replaceCommentsEmail", ArticleLanguage.replace_comments_email2)%>
            </div>
            <div class="editor-label">
                <%=Html.MytripLabelFor("nameArticles", ArticleLanguage.nameArticles)%>
            </div>
            <div class="editor-field">
                <%=Html.TextBoxFor(model => model.nameArticles) %>
                <%= Html.ValidationMessageFor(model => model.nameArticles)%>
            </div>
            <div class="editor-label">
                <%=Html.MytripLabelFor("nameBlogs", ArticleLanguage.nameBlogs)%>
            </div>
            <div class="editor-field">
                <%=Html.TextBoxFor(model => model.nameBlogs) %>
                <%= Html.ValidationMessageFor(model => model.nameBlogs)%>
            </div>
            <div class="editor-label">
                <%=Html.MytripLabelFor("nameSearch", ArticleLanguage.nameSearch)%>
            </div>
            <div class="editor-field">
                <%=Html.TextBoxFor(model => model.nameSearch) %>
                <%= Html.ValidationMessageFor(model => model.nameSearch)%>
            </div>
            <div class="editor-label">
                <%=Html.MytripLabelFor("nameTags", ArticleLanguage.nameTags)%>
            </div>
            <div class="editor-field">
                <%=Html.TextBoxFor(model => model.nameTags) %>
                <%= Html.ValidationMessageFor(model => model.nameTags)%>
            </div>            
            <div class="editor-label">
                <%=Html.MytripLabelFor("roleArticleEditor", ArticleLanguage.roleArticleEditor)%>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.roleArticleEditor) %>
                <%= Html.ValidationMessageFor(model => model.roleArticleEditor) %>
            </div>
            
            <div class="editor-label">
                <%=Html.MytripLabelFor("roleBlogger", ArticleLanguage.roleBlogger)%>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.roleBlogger) %>
                <%= Html.ValidationMessageFor(model => model.roleBlogger) %>
            </div>
            
            <div class="editor-label">
                <%=Html.MytripLabelFor("cacheSeconds", ArticleLanguage.cacheSeconds)%>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.cacheSeconds) %>
                <%= Html.ValidationMessageFor(model => model.cacheSeconds) %>
            </div>
            
            <p>
                <%=Html.MytripInput("submit", CoreLanguage.save)%>
            </p>
</div><div ><div class="contentbottomright"></div><div class="contentbottomleft"></div><div class="contentbottomcon"></div></div>

    <% } %>
    <div class="acfooter"></div>
    <div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content">
         
         
        <%=Html.ActionLink(CoreLanguage.cansel, "Index","Home") %>
  </div><div ><div class="contentbottomright"></div><div class="contentbottomleft"></div><div class="contentbottomcon"></div></div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
<script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script>
    <%if(!Model.closecountCommentForBlogs){ %>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#_closecount").show();
        });
    </script>
    <%} %>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#closecount").click(function () {              
            $("#_closecount").slideToggle(300);
            });
        });
    </script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContentRight" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="MainContentLeft" runat="server">
</asp:Content>

