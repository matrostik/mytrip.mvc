<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<% if ((bool)ViewData["abstract_model_artycle"] == true)
   {
       foreach (mt_artycle_category x in (IEnumerable<mt_artycle_category>)ViewData["helper_artycle_category_menu"])
       {%>
<div class="right_title">
    &nbsp; <a href="<%=Url.Action("B", "C", new { a = x.Id, b = 1,c=10,d=x.Path })%>">
    <%= x.Title%></a></div><div class="right_content">
<%foreach (mt_artycle_category y in x.mt_artycle_category2)
  {
%><a href="<%=Url.Action("Rss_A", "C", new { a = y.Id})%>">
    <img src="/content/images/feed.png" alt="rss" style="border-width: 0px; width: 14px; float: right;" /></a>
<a href="<%=Url.Action("B", "C", new { a = y.Id, b = 1,c=10,d=y.Path })%>">
    <%= y.Title%></a>
    
<br />
<br />
<%
    }%></div><%
    }
   }
   %>
