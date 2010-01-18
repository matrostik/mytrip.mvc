<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright ©  2009 Ñòþõèí Îëåã Àíàòîëüåâè÷   */-->
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
 <%if ((bool)ViewData["abstract_model_blog"] == true)
   {
       if ((int)ViewData["post_count"] > 0)
       {%>
<!-- ÏÎÑÒÛ ÍÀ ÃËÀÂÍÎÉ -->
<div>
<a href="<%=Url.Action("Rss_D", "C")%>">
    <img src="/content/images/feed.png" alt="rss" class="rss_home"  /></a>

<a href="<%=Url.Action("A", "C", new { a = 1, b = 10, c="Blogs"})%>">
    <h2><%=Mytrip_Mvc_Language.menu_blogs%></h2></a>
<table >
<%int abc = 0;
  foreach (mt_artycle x in (IEnumerable<mt_artycle>)ViewData["post"])
  {
      
%><tr>
<td style="background-color: #BCC7D8">
<small><%= Html.Encode(String.Format("{0:d MMMM yyyy }", x.AddedDate))%></small><br />

<%if ((int)ViewData["helper_category_status"] == 0)
  {
      if (x.mt_artycle_category.CategoryId == 0)
      {
          if (x.mt_artycle_category.Blog == false)
          {
        
      %><small><%=Mytrip_Mvc_Language.article_heading%></small><%}
          else
          { %><small><%=Mytrip_Mvc_Language.post_blog%></small><%} %>
        <a href="<%=Url.Action("B", "C", new { a = x.mt_artycle_category.Id, b = 1,c=10, d=x.mt_artycle_category.Path })%>">
    <small><%= x.mt_artycle_category.Title.ToLower()%></small></a><br />   
<%

  
     }
      else
      {
          if (x.mt_artycle_category.mt_artycle_category1.Blog == false)
          {
        
      %><small><%=Mytrip_Mvc_Language.article_heading%></small><%}
          else
          { %><small><%=Mytrip_Mvc_Language.post_blog%></small><%} %>
        <a href="<%=Url.Action("B", "C", new { a = x.mt_artycle_category.CategoryId, b = 1,c=10, d=x.mt_artycle_category.mt_artycle_category1.Path })%>">
    <small><%= x.mt_artycle_category.mt_artycle_category1.Title.ToLower()%></small></a><br />   
<% }
  } if (x.mt_artycle_category.CategoryId != 0)
  {
      if (x.mt_artycle_category.mt_artycle_category1.Blog == false)
      {
        
      %><small><%=Mytrip_Mvc_Language.article_subheading%></small><%}
      else
      { %><small><%=Mytrip_Mvc_Language.blog_heading%></small><%} %>
        <a href="<%=Url.Action("B", "C", new { a = x.mt_artycle_category.Id, b = 1,c=10, d=x.mt_artycle_category.Path })%>">
    <small><%= x.mt_artycle_category.Title.ToLower()%></small></a><br />   
<%

  
     } %><small><%=Html.Language(Mytrip_Mvc_Language.article_views, x.Views.ToString())%></small>
<%if (x.AddComment == true)
  { %><br /><small><%=Html.Language(Mytrip_Mvc_Language.article_comments, x.mt_artycle_comment.Count().ToString())%></small><%} %>
  <%if (x.ApprovedVotes == true)
    { %><br /><small><%=Html.Language(Mytrip_Mvc_Language.article_rating, x.Votes.ToString())%></small><%}
    if (x.mt_artycle_in_tegs.Count() > 0)
    { %>
  <div><small><%=Mytrip_Mvc_Language.article_tegs%></small>
  <%foreach (mt_artycle_in_teg y in x.mt_artycle_in_tegs)
    { %>&nbsp;<a href="<%=Url.Action("E", "C", new { a = y.mt_artycle_teg.Id, b = 1, c=10, d=y.mt_artycle_teg.Path})%>">
    <small><%= y.mt_artycle_teg.Title%></small></a>
  <%} %></div><%} %>
</td>
<td style="border: 1px solid #BCC7D8">
<div style="position: relative; width: 70px; float: right">
<%= Html.Gravatar(x.Email, new { s = "70", r = "g" })%>

</div>
<!-- ÀÄÌÈÍ ×ÀÑÒÜ -->
<div class="edit_content">
   <%= Html.EditeAndDeliteArtycle((bool)x.mt_artycle_category.Blog, (bool)x.mt_artycle_category.News, x.Id, x.mt_artycle_category.Id, x.AddedBy, x.mt_artycle_category.AddedBy)%>      
 </div>
  <!--END ÀÄÌÈÍ ×ÀÑÒÜ -->
  <!--ÏÎÑÒ -->
<%if (x.UrlImageDescription.Length > 7)
  { %>
        <img src="<%=x.UrlImageDescription %>" alt="<%=x.Title %>" style="position: relative; width:100px; float: left; margin-right: 5px;" />
        <%} %>
<a href="<%=Url.Action("C", "C", new { a = x.Id, b=x.Path})%>" style="font-size: 14px; font-weight: bold">
    <%= x.Title%></a><%if (x.RegistrUser == true)
                       { %><br /><small><%=Mytrip_Mvc_Language.only_for_registered%></small> <%} %>
<br />
<%= x.Description%>
</td></tr>
<%abc++;
  if (abc == 5)
      break;
  } %>
<!--END ÏÎÑÒ -->
</table>
</div>
<!--END ÏÎÑÒÛ ÍÀ ÃËÀÂÍÎÉ -->
<%}
   } %>