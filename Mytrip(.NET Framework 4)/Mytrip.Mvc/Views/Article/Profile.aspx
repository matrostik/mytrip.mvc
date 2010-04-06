<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Articles.Models.ProfileModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Html.PageTitle(Model.Username, "/")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=ArticleLanguage.profile %> <%=Model.Username %></h2>
    <div style="float:left; width:180px; margin-right:10px;"><%--border:1px solid black;--%>
    <div style="text-align:center;"><%=Html.Avatar(Model.Email, new { title=Model.Username})%></div>
    <%=Html.UserData(Model.Username) %>
    </div>
    <div style="overflow:hidden;">
    <div style="font-size:large; font-weight:bold; vertical-align:text-top; border-bottom:4px solid black; overflow:hidden;margin-bottom:20px;">Recent Activity</div>
    <%=Html.LastActivity(Model.Username) %>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContentLeft" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="MainContentRight" runat="server">
</asp:Content>
