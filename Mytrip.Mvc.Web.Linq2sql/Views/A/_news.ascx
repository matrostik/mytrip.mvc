<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright �  2009 ������ ���� �����������   */-->
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
 <%if ((bool)ViewData["model_news"] == true)
    {%> 
 <!-- ������� �� ������� -->
<div >
<a href="<%=Url.Action("Rss_C", "C")%>">
    <img src="/content/images/feed.png" alt="rss" style="border-width: 0px; width: 14px; float: right;" /></a>

<a href="<%=Url.Action("F", "C", new { a = 0, b = 1, c=10, d="News"})%>">
    <h2>�������</h2></a>
    
<%int abcd = 0;
  foreach (mt_artycle x in (IEnumerable<mt_artycle>)ViewData["news"])
  {     
      
%>
 <!-- ����� ����� -->
<div class="edit_content">
   <%= Html.EditeAndDeliteArtycle((bool)x.mt_artycle_category.Blog, (bool)x.mt_artycle_category.News, x.Id,x.mt_artycle_category.Id, x.AddedBy, x.mt_artycle_category.AddedBy)%>      
 </div>
  <!--END ����� ����� -->
  <%if (x.Warning == false)
  { %>
<div class="news">
  <!-- ������� -->
<%= Html.Encode(String.Format("{0:d MMMM}", x.AddedDate))%>&nbsp;&nbsp;&nbsp;&nbsp;

<a href="<%=Url.Action("C", "C", new { a = x.Id, b=x.Path})%>">
    <%= x.Title%></a><%if (x.RegistrUser == true)
                       { %>&nbsp;&nbsp;&nbsp;&nbsp;<small>[������ ��� ������������������]</small> <%} %>

  <br /><br /></div>
<%}
  else
  { %><div class="newsA">
  <!-- ������� -->
<%= Html.Encode(String.Format("{0:d MMMM}", x.AddedDate))%>&nbsp;&nbsp;&nbsp;&nbsp;

<a href="<%=Url.Action("C", "C", new { a = x.Id, b=x.Path})%>">
    <%= x.Title%></a><%if (x.RegistrUser == true)
                       { %>&nbsp;&nbsp;&nbsp;&nbsp;<small>[������ ��� ������������������]</small> <%} %>

  <br /><br /></div>
<%} %>

<%abcd++;
  if (abcd == 10)
      break;
   }  %>
   <!--END ������� -->
</table>
</div>
<!--END ������� �� ������� -->
<%} %>