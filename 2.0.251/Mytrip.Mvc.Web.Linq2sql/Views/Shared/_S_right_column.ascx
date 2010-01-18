<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div class="accordion">
<% Html.RenderPartial("_S_admin"); %>
    <% Html.RenderPartial("_S_search"); %>
    <% Html.RenderPartial("_S_menu_user"); %>
    <% Html.RenderPartial("_S_category_artycle_right"); %>
    <% Html.RenderPartial("_S_news_right"); %>
    <% Html.RenderPartial("_S_artycle_right"); %>
    <% Html.RenderPartial("_S_blog_right"); %>
    <% Html.RenderPartial("_S_teg_right"); %>
</div>
<%// Html.RenderPartial("_S_move_content"); %> 