<%@ Page Language="C#" MasterPageFile="~/Views/Shared/2_column.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
    <%= ViewData["model_domain"]%>/Главная
</asp:Content>
<asp:Content ContentPlaceHolderID="RightContent" runat="server">
<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright ©  2009 Стюхин Олег Анатольевич   */-->
    <% Html.RenderPartial("_S_admin"); %>
    <% Html.RenderPartial("_S_search"); %>
    <% Html.RenderPartial("_S_menu_user"); %>
    <% Html.RenderPartial("_S_news_right"); %>
    <% Html.RenderPartial("_S_artycle_right"); %>
    <% Html.RenderPartial("_S_blog_right"); %>
    <% Html.RenderPartial("_S_teg_right"); %>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <% Html.RenderPartial("_news"); %>
    <% Html.RenderPartial("_artycle"); %>
    <% Html.RenderPartial("_post"); %>
</asp:Content>
