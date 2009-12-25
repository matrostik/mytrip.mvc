<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright ©  2009 Стюхин Олег Анатольевич   */-->
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%if ((bool)ViewData["model_artycle"] == true)
  {%> 
 <!-- СТАТЬИ НА ГЛАВНОЙ -->
<div >
<a href="<%=Url.Action("Rss_B", "C")%>">
    <img src="/content/images/feed.png" alt="rss" style="border-width: 0px; width: 14px; float: right;" /></a>

<a href="<%=Url.Action("A", "C", new { a = 0, b = 1, c=10, d="Artycles"})%>">
    <h2>Статьи</h2></a>
<table>
    
<%int abcd = 0;
  foreach (mt_artycle x in (IEnumerable<mt_artycle>)ViewData["artycle"])
  {
    
%><tr>
<td>
 <!-- АДМИН ЧАСТЬ -->
<% if (HttpContext.Current.User.IsInRole("artycle_editor"))
      {
          if (x.mt_artycle_category.AddedBy == Page.User.Identity.Name)
          {%><div class="edit_content">
<a href="<%=Url.Action("ZK", "C", new { a = x.Id, b=x.CategoryId })%>"><img src="/content/images/edit.png" alt="правка" style="border-width: 0px;" /></a>
<a href="<%=Url.Action("ZL", "C", new { a = x.Id })%>" onclick = "return confirm ('Вы уверены что хотите удалить статью?');"><img src="/content/images/delete.png" alt="удалить" style="border-width: 0px;" /></a>     
</div>
<%}
      }if (HttpContext.Current.User.IsInRole("chief_editor"))
          {
              %><div class="edit_content">
<a href="<%=Url.Action("ZK", "C", new { a = x.Id, b=x.CategoryId })%>"><img src="/content/images/edit.png" alt="правка" style="border-width: 0px;" /></a>
<a href="<%=Url.Action("ZL", "C", new { a = x.Id })%>" onclick = "return confirm ('Вы уверены что хотите удалить статью?');"><img src="/content/images/delete.png" alt="удалить" style="border-width: 0px;" /></a>     
</div>
<%}%>
  <!--END АДМИН ЧАСТЬ -->
  <!-- СТАТЬЯ -->
  <%if (x.UrlImageDescription.Length > 7)
                { %>
        <img src="<%=x.UrlImageDescription %>" alt="<%=x.Title %>" style="position: relative; width:100px; float: left; margin-right: 5px;" />
        <%} %>
<a href="<%=Url.Action("C", "C", new { a = x.Id, b=x.Path})%>">
    <%= x.Title%></a><%if (x.RegistrUser == true)
                        { %>&nbsp;&nbsp;&nbsp;&nbsp;<small>[только для зарегестрированных]</small> <%} %>
<br /><br />
<%= x.Description%>
</td></tr>
<tr><td style="border-bottom-style: dotted; border-bottom-width: 1px; border-bottom-color: #555152; padding: 0px; margin: 0px">
<small>

<%= Html.Encode(String.Format("{0:d MMMM yyyy г.}", x.AddedDate))%></small>&nbsp;&nbsp;&nbsp;&nbsp;
<small>Рубрика: </small>
        <a href="<%=Url.Action("B", "C", new { a = x.mt_artycle_category.Id, b = 1,c=10, d=x.mt_artycle_category.Path })%>">
    <small><%= x.mt_artycle_category.Title.ToLower() %></small></a><%
                                                                       if (x.mt_artycle_in_teg.Count() > 0)
                                                                       { %>
  <div><small>Теги: </small>
  <%foreach (mt_artycle_in_teg y in x.mt_artycle_in_teg)
    { %>&nbsp;<a href="<%=Url.Action("E", "C", new { a = y.mt_artycle_teg.Id, b = 1, c=10, d=y.mt_artycle_teg.Path})%>">
    <small><%= y.mt_artycle_teg.Title%></small></a>
  <%} %></div><%}
                                                                       else { %><br /><%} %><br /></td></tr>
<%abcd++;
  if (abcd == 5)
      break;
   }  %>
   <!--END СТАТЬЯ -->
</table>
</div>
<!--END СТАТЬИ НА ГЛАВНОЙ -->
<%} %>