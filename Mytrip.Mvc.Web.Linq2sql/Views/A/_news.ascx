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
<div class="edit_content">
   <%= Html.EditeAndDeliteArtycle((bool)x.mt_artycle_category.Blog, (bool)x.mt_artycle_category.News, x.Id,x.mt_artycle_category.Id, x.AddedBy, x.mt_artycle_category.AddedBy)%>      
 </div>
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