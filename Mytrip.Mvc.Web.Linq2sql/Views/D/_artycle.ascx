<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright ©  2009 Стюхин Олег Анатольевич   */-->
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div class="artycle_summtop"></div>
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
     
%><tr><td>
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
                        { %>&nbsp;&nbsp;&nbsp;&nbsp;<small>[только для зарегестрированных]</small> <%}
                      string description = x.Description.Replace(search, search2);
                      description = description.Replace(search5, search3);
                      description = description.Replace(search6, search4);
                      description = description.Replace(search7, search8);
      
      %>  
       
<br /><br />
<%= description%></td></tr>
<tr><td style="border-bottom-style: dotted; border-bottom-width: 1px; border-bottom-color: #555152">
<small><%= Html.Encode(String.Format("{0:d MMMM yyyy г.}", x.AddedDate))%></small>&nbsp;&nbsp;&nbsp;&nbsp;

<%if ((int)ViewData["category_status"] == 0)
  {
      if (x.mt_artycle_category.Blog == false)
      {
        
      %><small>Рубрика: </small><%}
      else
      { %><small>Блог: </small><%} %>
        <a href="<%=Url.Action("B", "C", new { a = x.mt_artycle_category.Id, b = 1,c=10, d=x.mt_artycle_category.Path })%>">
    <small><%= x.mt_artycle_category.Title.ToLower() %></small></a>&nbsp;&nbsp;&nbsp;&nbsp;   
<%} %><small>Просмотров: <%= x.Views %></small>
<%if (x.AddComment == true)
  { %>&nbsp;&nbsp;&nbsp;&nbsp;<small>Комментариев: <%= x.mt_artycle_comment.Count() %></small><%} %>
  <%if (x.ApprovedVotes == true)
  { %>&nbsp;&nbsp;&nbsp;&nbsp;<small>Рейтинг: <%= x.Votes %></small><%}
    if (x.mt_artycle_in_teg.Count() > 0)
    { %>
  <div><small>Теги: </small>
  <%foreach (mt_artycle_in_teg y in x.mt_artycle_in_teg)
    { %>&nbsp;<a href="<%=Url.Action("E", "C", new { a = y.mt_artycle_teg.Id, b = 1, c=10, d=y.mt_artycle_teg.Path})%>">
    <small><%= y.mt_artycle_teg.Title%></small></a>
  <%} %></div><%} %>
</td></tr>
<%
    } %>
</table>