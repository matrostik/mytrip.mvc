<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%if ((bool)ViewData["abstract_model_artycle"] == true)
  {
      foreach (mt_artycle_category z in (IEnumerable<mt_artycle_category>)ViewData["helper_artycle_category_menu"])
      {
          if (z.mt_artycle.Count > 0)
          {%> 
 <!-- СТАТЬИ НА ГЛАВНОЙ -->
<div >
<a href="<%=Url.Action("Rss_A", "C", new { a = z.Id})%>">
    <img src="/content/images/feed.png" alt="rss" class="rss_home" /></a>

<a href="<%=Url.Action("B", "C", new { a = z.Id, b = 1,c=10,d=z.Path })%>">
    <h2><%=z.Title%></h2></a>
<table>
    
<%int abcd = 0;
  foreach (mt_artycle x in (IEnumerable<mt_artycle>)ViewData["artycle_for_category"])
  {
      if(x.CategoryId==z.Id)
  {
%><tr ><td style="background-color: #BCC7D8">
<small><%= Html.Encode(String.Format("{0:d MMMM yyyy }", x.AddedDate))%></small><br />
<small><%=Mytrip_Mvc_Language.article_heading%></small>
        <a href="<%=Url.Action("B", "C", new { a = x.mt_artycle_category.Id, b = 1,c=10, d=x.mt_artycle_category.Path })%>">
    <small><%= x.mt_artycle_category.Title.ToLower()%></small></a><br />   
<small><%=Html.Language(Mytrip_Mvc_Language.article_views, x.Views.ToString())%></small>
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
 <!-- АДМИН ЧАСТЬ -->
<div class="edit_content">
   <%= Html.EditeAndDeliteArtycle((bool)x.mt_artycle_category.Blog, (bool)x.mt_artycle_category.News, x.Id, x.mt_artycle_category.Id, x.AddedBy, x.mt_artycle_category.AddedBy)%>      
 </div>
  <!--END АДМИН ЧАСТЬ -->
  <!-- СТАТЬЯ -->
  <%if (x.UrlImageDescription.Length > 7)
    { %>
        <img src="<%=x.UrlImageDescription %>" alt="<%=x.Title %>" style="position: relative; width:100px; float: right; margin-left: 5px;" />
        <%} %>
<a href="<%=Url.Action("C", "C", new { a = x.Id, b=x.Path})%>" style="font-size: 14px; font-weight: bold">
    <%= x.Title%></a><%if (x.RegistrUser == true)
                       { %><br /><small><%=Mytrip_Mvc_Language.only_for_registered%></small> <%} %>
<br />
<%= x.Description%>
</td>
</tr>
<%abcd++;
  if (abcd == 5)
      break;
  }
  else if(x.mt_artycle_category.CategoryId ==z.Id)
  {
%><tr ><td style="background-color: #BCC7D8">
<small><%= Html.Encode(String.Format("{0:d MMMM yyyy }", x.AddedDate))%></small><br />
<small><%=Mytrip_Mvc_Language.article_heading%></small>
        <a href="<%=Url.Action("B", "C", new { a = x.mt_artycle_category.CategoryId, b = 1,c=10, d=x.mt_artycle_category.mt_artycle_category1.Path })%>">
    <small><%= x.mt_artycle_category.mt_artycle_category1.Title.ToLower()%></small></a><br />
    <small><%=Mytrip_Mvc_Language.article_subheading%></small>
    <a href="<%=Url.Action("B", "C", new { a = x.mt_artycle_category.Id, b = 1,c=10, d=x.mt_artycle_category.Path })%>">
    <small><%= x.mt_artycle_category.Title.ToLower()%></small></a><br />   
<small><%=Html.Language(Mytrip_Mvc_Language.article_views, x.Views.ToString())%></small>
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
 <!-- АДМИН ЧАСТЬ -->
<div class="edit_content">
   <%= Html.EditeAndDeliteArtycle((bool)x.mt_artycle_category.Blog, (bool)x.mt_artycle_category.News, x.Id, x.mt_artycle_category.Id, x.AddedBy, x.mt_artycle_category.AddedBy)%>      
 </div>
  <!--END АДМИН ЧАСТЬ -->
  <!-- СТАТЬЯ -->
  <%if (x.UrlImageDescription.Length > 7)
    { %>
        <img src="<%=x.UrlImageDescription %>" alt="<%=x.Title %>" style="position: relative; width:100px; float: right; margin-left: 5px;" />
        <%} %>
<a href="<%=Url.Action("C", "C", new { a = x.Id, b=x.Path})%>" style="font-size: 14px; font-weight: bold">
    <%= x.Title%></a><%if (x.RegistrUser == true)
                       { %><br /><small><%=Mytrip_Mvc_Language.only_for_registered%></small> <%} %>
<br />
<%= x.Description%>
</td>
</tr>
<%abcd++;
  if (abcd == 5)
      break;
  }
  }
      %>
   <!--END СТАТЬЯ -->
</table>
</div>
<!--END СТАТЬИ НА ГЛАВНОЙ -->
<%}
      }
  }
  %>
