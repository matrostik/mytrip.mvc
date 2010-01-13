<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
 <table>
    <%
      foreach (mt_artycle_category x in (IEnumerable<mt_artycle_category>)ViewData["blog"])
      {
%><tr><td style="border: 1px solid #BCC7D8">
<div style="position: relative; width: 70px; float: right; margin-left: 5px;">
<%= Html.Gravatar(x.Email, new  { s = "70", r = "g" })%>

</div>
<a href="<%=Url.Action("Rss_A", "C", new { a = x.Id})%>">
    <img src="/content/images/feed.png" alt="rss" class="rss_home"  /></a>
<a href="<%=Url.Action("B", "C", new { a = x.Id, b = 1,c=10,d=x.Path })%>" style="font-size: 14px; font-weight: bold">
   <%= x.Title%></a>
    
<br /><br />
<small style="font-style: italic"><%=x.AddedBy%></small>
<br />
<small><%=Html.Language(Mytrip_Mvc_Language_1.article_views, x.Views.ToString())%></small>
<br />
<small><%=Html.Language(Mytrip_Mvc_Language_1.blog_posts, x.mt_artycle.Count.ToString())%></small>
<br /></td></tr>
<%    }
   %></table>
