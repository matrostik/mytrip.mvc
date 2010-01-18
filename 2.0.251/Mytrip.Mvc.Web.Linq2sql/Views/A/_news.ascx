<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright ©  2009 Ñòþõèí Îëåã Àíàòîëüåâè÷   */-->
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
 <%if ((bool)ViewData["abstract_model_news"] == true)
   {
       if ((int)ViewData["news_count"] > 0)
       {%> 
 <!-- ÍÎÂÎÑÒÈ ÍÀ ÃËÀÂÍÎÉ -->
<div >
<a href="<%=Url.Action("Rss_C", "C")%>">
    <img src="/content/images/feed.png" alt="rss" class="rss_home"  /></a>

<a href="<%=Url.Action("A", "C", new { a = 1, b = 10, c="News"})%>">
    <h2><%=Mytrip_Mvc_Language.menu_news%></h2></a>
    
<%int abcd = 0;
  foreach (mt_artycle x in (IEnumerable<mt_artycle>)ViewData["news"])
  {     
      
%>

  <%if (x.Warning == false)
    { %>
<div class="news">
<div class="edit_content">
   <%= Html.EditeAndDeliteArtycle((bool)x.mt_artycle_category.Blog, (bool)x.mt_artycle_category.News, x.Id, x.mt_artycle_category.Id, x.AddedBy, x.mt_artycle_category.AddedBy)%>      
 </div>
  <!-- ÍÎÂÎÑÒÜ -->

<a href="<%=Url.Action("C", "C", new { a = x.Id, b=x.Path})%>">
    <%= x.Title%></a><%if (x.RegistrUser == true)
                       { %><br /><small><%=Mytrip_Mvc_Language.only_for_registered%></small> <%} %>

  <br /><div class="news_data"><%= Html.Encode(String.Format("{0:d MMMM}", x.AddedDate))%></div>
</div>
<%}
    else
    { %><div class="newsA">
  <div class="edit_content">
   <%= Html.EditeAndDeliteArtycle((bool)x.mt_artycle_category.Blog, (bool)x.mt_artycle_category.News, x.Id, x.mt_artycle_category.Id, x.AddedBy, x.mt_artycle_category.AddedBy)%>      
 </div>
  <!-- ÍÎÂÎÑÒÜ -->

<a href="<%=Url.Action("C", "C", new { a = x.Id, b=x.Path})%>">
    <%= x.Title%></a><%if (x.RegistrUser == true)
                       { %><br /><small><%=Mytrip_Mvc_Language.only_for_registered%></small> <%} %>

  <br /><div class="news_data"><%= Html.Encode(String.Format("{0:d MMMM}", x.AddedDate))%></div>
</div>
<%} %>

<%abcd++;
  if (abcd == 5)
      break;
  }  %>
   <!--END ÍÎÂÎÑÒÜ -->

</div>
<!--END ÍÎÂÎÑÒÈ ÍÀ ÃËÀÂÍÎÉ -->
<%}
   } %>