<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
<%  //************************************************************ 
    // Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich
    // To learn more about Mytrip.Mvc.Entyty visit 
    // http://starterkitmytripmvc.codeplex.com/
    // mytripmvc@gmail.com
    // license: Microsoft Public License (Ms-PL) 
    // *********************************************************** %>
   <%=Html.PageTitle(CoreLanguage.about, "/")%>
</asp:Content>
 
<asp:Content ContentPlaceHolderID="MainContentLeft" runat="server">

    <h2><%=CoreLanguage.about%></h2>
    <p>
        Put content here.
        </p>

  
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContentRight" runat="server">
<%=Html.Partial("SmallColumn")%>
<%--<%=Html.AccordionSearch(Html.TextBox("search", "", new { style = "width:170px" }).ToString(), Html.MytripInput("submit", ArticleLanguage.search).ToString(), Url.Action("Index", "Article", new { url = Request.Path }))%>
<%=Html.AccordionArticle() %>
<%=Html.AccordionCategory() %>
<%=Html.AccordionBlogs() %>
<%=Html.AccordionTag()%>
<%=Html.AccordionUsersAndFiles(true) %>
<%=Html.AccordionDonateProject() %>     --%>
</asp:Content>
