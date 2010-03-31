<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%ArticleCache article = new ArticleCache(); %>
<%=Html.AccordionSearch(Html.TextBox("search", "", new { style = "width:170px" }).ToString(), Html.MytripInput("submit", ArticleLanguage.search).ToString(), Url.Action("Index", "Article", new { url = Request.Path }))%>
<%=Html.AccordionUserProfile(Html.ArticlesUserProfile())%>
<%=article.AccordionArticle()%>
<%=article.AccordionCategory()%>
<%=article.AccordionBlogs()%>
<%=article.AccordionTag()%>
<%=Html.AccordionUsersAndFiles(true) %>
<%=Html.AccordionDonateProject() %>
