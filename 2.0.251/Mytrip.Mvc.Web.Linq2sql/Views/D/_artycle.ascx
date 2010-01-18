<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright ©  2009 Стюхин Олег Анатольевич   */-->
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<table>
<%string search = (string)ViewData["search"];
  
  string search2 = "<b style = 'background-color: #FFFF00;'>" + search + "</b>";
  string search3 = (search2.Remove(42).ToUpper()) + (search2.Substring(42));
  string search4 = search2.ToUpper();
  string search5 = (search.Remove(2).ToUpper()) + (search.Substring(2));
  string search6 = search.ToUpper();
  string search7 = search.ToLower();
  string search8 = search2.ToLower();  
  foreach (mt_artycle x in (IEnumerable<mt_artycle>)ViewData["artycle"])
  {
     
%><tr><td style="background-color: #BCC7D8">
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
      else {if (x.mt_artycle_category.mt_artycle_category1.Blog == false)
          {
        
      %><small><%=Mytrip_Mvc_Language.article_heading%></small><%}
          else
          { %><small><%=Mytrip_Mvc_Language.post_blog%></small><%} %>
        <a href="<%=Url.Action("B", "C", new { a = x.mt_artycle_category.CategoryId, b = 1,c=10, d=x.mt_artycle_category.mt_artycle_category1.Path })%>">
    <small><%= x.mt_artycle_category.mt_artycle_category1.Title.ToLower()%></small></a><br />   
<% }
  }if (x.mt_artycle_category.CategoryId != 0)
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
</td><td style="border: 1px solid #BCC7D8">
<div class="edit_content">
   <%= Html.EditeAndDeliteArtycle((bool)x.mt_artycle_category.Blog, (bool)x.mt_artycle_category.News, x.Id,x.mt_artycle_category.Id, x.AddedBy, x.mt_artycle_category.AddedBy)%>      
 </div>
<%if (x.UrlImageDescription.Length > 7)
                { %>
        <img src="<%=x.UrlImageDescription %>" alt="<%=x.Title %>" style="position: relative; width:120px; float: left; margin-right: 5px;" />
        <%} 
      
      string title = x.Title.Replace(search, search2);
      title=title.Replace(search5, search3);
      title = title.Replace(search6, search4);
      title = title.Replace(search7, search8);
      %>



<a href="<%=Url.Action("C", "C", new { a = x.Id, b=x.Path})%>">
    <%= title%></a> <%if (x.RegistrUser == true)
                        { %>&nbsp;&nbsp;&nbsp;&nbsp;<small><%=Mytrip_Mvc_Language.only_for_registered%></small> <%}
                      string description = x.Description.Replace(search, search2);
                      description = description.Replace(search5, search3);
                      description = description.Replace(search6, search4);
                      description = description.Replace(search7, search8);
      
      %>  
       
<br /><br />
<%= description%></td></tr>
<%
    } %>
</table>