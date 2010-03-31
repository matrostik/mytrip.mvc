<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Articles.Models.ArchiveIndexModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Html.PageTitle(Model.PageTitle , "/")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=Model.PageTitle %></h2><%=Html.CulturePager(Model.Path)%>
    <%=Html.ShowDetails(Model.Path,Model.Culture)%><br />
    <%=Html.ActionLink("Back to Archive","Index") %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContentLeft" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="MainContentRight" runat="server">
</asp:Content>
