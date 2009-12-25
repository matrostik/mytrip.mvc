<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright ©  2009 Стюхин Олег Анатольевич   */-->
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
 <%if ((bool)ViewData["model_news"] == true)
    {%> 
 <!-- НОВОСТИ НА ГЛАВНОЙ -->
<div >
<a href="<%=Url.Action("Rss_C", "C")%>">
    <img src="/content/images/feed.png" alt="rss" style="border-width: 0px; width: 14px; float: right;" /></a>

<a href="<%=Url.Action("F", "C", new { a = 0, b = 1, c=10, d="News"})%>">
    <h2>Новости</h2></a>
    
<%int abcd = 0;
  foreach (mt_artycle x in (IEnumerable<mt_artycle>)ViewData["news"])
  {     
      
%>
 <!-- АДМИН ЧАСТЬ -->
<% if (HttpContext.Current.User.IsInRole("artycle_editor"))
   {
       if (x.mt_artycle_category.AddedBy == Page.User.Identity.Name)
       {%><div class="edit_content">
<a href="<%=Url.Action("ZX", "C", new { a = x.Id, b=x.CategoryId })%>"><img src="/content/images/edit.png" alt="правка" style="border-width: 0px;" /></a>
<a href="<%=Url.Action("ZL", "C", new { a = x.Id })%>" onclick = "return confirm ('Вы уверены что хотите удалить новость?');"><img src="/content/images/delete.png" alt="удалить" style="border-width: 0px;" /></a>     
</div>
<%}
   } if (HttpContext.Current.User.IsInRole("chief_editor"))
   {
              %><div class="edit_content">
<a href="<%=Url.Action("ZX", "C", new { a = x.Id, b=x.CategoryId })%>"><img src="/content/images/edit.png" alt="правка" style="border-width: 0px;" /></a>
<a href="<%=Url.Action("ZL", "C", new { a = x.Id })%>" onclick = "return confirm ('Вы уверены что хотите удалить новость?');"><img src="/content/images/delete.png" alt="удалить" style="border-width: 0px;" /></a>     
</div>
<%}%>
  <!--END АДМИН ЧАСТЬ -->
  <%if (x.Warning == false)
  { %>
<div class="news">
  <!-- НОВОСТЬ -->
<%= Html.Encode(String.Format("{0:d MMMM}", x.AddedDate))%>&nbsp;&nbsp;&nbsp;&nbsp;

<a href="<%=Url.Action("C", "C", new { a = x.Id, b=x.Path})%>">
    <%= x.Title%></a><%if (x.RegistrUser == true)
                       { %>&nbsp;&nbsp;&nbsp;&nbsp;<small>[только для зарегестрированных]</small> <%} %>

  <br /><br /></div>
<%}
  else
  { %><div class="newsA">
  <!-- НОВОСТЬ -->
<%= Html.Encode(String.Format("{0:d MMMM}", x.AddedDate))%>&nbsp;&nbsp;&nbsp;&nbsp;

<a href="<%=Url.Action("C", "C", new { a = x.Id, b=x.Path})%>">
    <%= x.Title%></a><%if (x.RegistrUser == true)
                       { %>&nbsp;&nbsp;&nbsp;&nbsp;<small>[только для зарегестрированных]</small> <%} %>

  <br /><br /></div>
<%} %>

<%abcd++;
  if (abcd == 10)
      break;
   }  %>
   <!--END НОВОСТЬ -->
</table>
</div>
<!--END НОВОСТИ НА ГЛАВНОЙ -->
<%} %>