<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright ©  2009 Стюхин Олег Анатольевич   */-->
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<table>
<%
  foreach (mt_artycle x in (IEnumerable<mt_artycle>)ViewData["artycle"])
  {
     
%><tr><td style="background-color: #BCC7D8">
<small><%= Html.Encode(String.Format("{0:d MMMM yyyy г.}", x.AddedDate))%></small><br />

<%if ((int)ViewData["category_status"] == 0)
  {
      if (x.mt_artycle_category.CategoryId == 0)
      {
          if (x.mt_artycle_category.Blog == false)
          {
        
      %><small><%=Mytrip_Mvc_Language_1.article_heading%></small><%}
          else
          { %><small><%=Mytrip_Mvc_Language_1.post_blog%></small><%} %>
        <a href="<%=Url.Action("B", "C", new { a = x.mt_artycle_category.Id, b = 1,c=10, d=x.mt_artycle_category.Path })%>">
    <small><%= x.mt_artycle_category.Title.ToLower()%></small></a><br />   
<%

  
    }
      else {if (x.mt_artycle_category.mt_artycle_category1.Blog == false)
          {
        
      %><small><%=Mytrip_Mvc_Language_1.article_heading%></small><%}
          else
          { %><small><%=Mytrip_Mvc_Language_1.post_blog%></small><%} %>
        <a href="<%=Url.Action("B", "C", new { a = x.mt_artycle_category.CategoryId, b = 1,c=10, d=x.mt_artycle_category.mt_artycle_category1.Path })%>">
    <small><%= x.mt_artycle_category.mt_artycle_category1.Title.ToLower()%></small></a><br />   
<% }
  }if (x.mt_artycle_category.CategoryId != 0)
      {
        if (x.mt_artycle_category.mt_artycle_category1.Blog == false)
          {
        
      %><small><%=Mytrip_Mvc_Language_1.article_subheading%></small><%}
          else
          { %><small><%=Mytrip_Mvc_Language_1.blog_heading%></small><%} %>
        <a href="<%=Url.Action("B", "C", new { a = x.mt_artycle_category.Id, b = 1,c=10, d=x.mt_artycle_category.Path })%>">
    <small><%= x.mt_artycle_category.Title.ToLower()%></small></a><br />   
<%

  
    } %><small><%=Html.Language(Mytrip_Mvc_Language_1.article_views, x.Views.ToString())%></small>
<%if (x.AddComment == true)
  { %><br /><small><%=Html.Language(Mytrip_Mvc_Language_1.article_comments, x.mt_artycle_comment.Count().ToString())%></small><%} %>
  <%if (x.ApprovedVotes == true)
  { %><br /><small><%=Html.Language(Mytrip_Mvc_Language_1.article_rating, x.Votes.ToString())%></small><%}
    if (x.mt_artycle_in_tegs.Count() > 0)
    { %>
  <div><small><%=Mytrip_Mvc_Language_1.article_tegs%></small>
  <%foreach (mt_artycle_in_teg y in x.mt_artycle_in_tegs)
    { %>&nbsp;<a href="<%=Url.Action("E", "C", new { a = y.mt_artycle_teg.Id, b = 1, c=10, d=y.mt_artycle_teg.Path})%>">
    <small><%= y.mt_artycle_teg.Title%></small></a>
  <%} %></div><%} %>
</td><td style="border: 1px solid #BCC7D8">
<div class="edit_content">
   <%= Html.EditeAndDeliteArtycle(x.mt_artycle_category.Blog, x.mt_artycle_category.News, x.Id,x.mt_artycle_category.Id, x.AddedBy, x.mt_artycle_category.AddedBy)%>      
 </div>
<%if (x.UrlImageDescription.Length > 7)
                { %>
        <img src="<%=x.UrlImageDescription %>" alt="<%=x.Title %>" style="position: relative; width:120px; float: right; margin-left: 5px;" />
        <%} %>



<a href="<%=Url.Action("C", "C", new { a = x.Id, b=x.Path})%>" style="font-size: 14px; font-weight: bold">
    <%= x.Title%></a> <%if (x.RegistrUser == true)
                        { %><br /><small><%=Mytrip_Mvc_Language_1.only_for_registered%></small> <%} %>
<br />
<%= x.Description%></td></tr>
<%
    } %>
</table>