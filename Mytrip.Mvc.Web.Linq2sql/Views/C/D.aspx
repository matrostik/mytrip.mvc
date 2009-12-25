<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/2_column.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  
    <%= ViewData["model_domain"]%>/Блоги
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright ©  2009 Стюхин Олег Анатольевич   */-->
    <!-- ЗАГОЛОВОК -->
    <b style="font-size: 18px">Блоги</b>
    <!--END ЗАГОЛОВОК -->
    <br />
    <br />
    <!-- КОНТЕНТ -->
    <% Html.RenderPartial("_S_pager"); %>
    <div class="artycle_summtop"></div>
    <table>
    <%
      foreach (mt_artycle_category x in (IEnumerable<mt_artycle_category>)ViewData["blog"])
      {
%><tr><td style="border-bottom-style: dotted; border-bottom-width: 1px; border-bottom-color: #555152">
<div style="position: relative; width: 70px; float: right; margin-left: 5px;">
<%= Html.Gravatar(x.Email, new  { s = "70", r = "g" })%>

</div>
<a href="<%=Url.Action("Rss_A", "C", new { a = x.Id})%>">
    <img src="/content/images/feed.png" alt="rss" style="border-width: 0px; width: 14px; float: right;" /></a>
<a href="<%=Url.Action("B", "C", new { a = x.Id, b = 1,c=10,d=x.Path })%>">
    <%= x.Title%></a>
    
<br />
<small style="font-style: italic"><%=x.AddedBy%></small>
<br />
<small style="font-style: italic">просмотров: <%=x.Views%></small>
<br />
<small style="font-style: italic">постов: <%=x.mt_artycle.Count%></small>
<br /></td></tr>
<%    }
   %></table>
    
    <% Html.RenderPartial("_S_pager"); %>
    <!--END КОНТЕНТ -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="RightContent" runat="server">
    <!-- ПРАВАЯ КОЛОНКА -->
    <% Html.RenderPartial("_S_admin"); %>
    <% Html.RenderPartial("_S_search"); %>
    <% Html.RenderPartial("_S_menu_user"); %>
    <% Html.RenderPartial("_S_news_right"); %>
    <% Html.RenderPartial("_S_artycle_right"); %>
    <% Html.RenderPartial("_S_blog_right"); %>
    <% Html.RenderPartial("_S_teg_right"); %>
    <!--END ПРАВАЯ КОЛОНКА -->
</asp:Content>
