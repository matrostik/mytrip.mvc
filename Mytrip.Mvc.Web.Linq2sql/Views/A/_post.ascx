<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright ©  2009 Стюхин Олег Анатольевич   */-->
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
 <%if ((bool)ViewData["model_blog"] == true)
    {%>
<!-- ПОСТЫ НА ГЛАВНОЙ -->
<div>
<a href="<%=Url.Action("Rss_D", "C")%>">
    <img src="/content/images/feed.png" alt="rss" style="border-width: 0px; width: 14px; float: right;" /></a>

<a href="<%=Url.Action("D", "C", new { a = 0, b = 1, c=10, d="Blogs"})%>">
    <h2>Блоги</h2></a>
<table >
<%int abc = 0;
  foreach (mt_artycle x in (IEnumerable<mt_artycle>)ViewData["post"])
  {
      
%><tr><td style="border-bottom-style: dotted; border-bottom-width: 1px; border-bottom-color: #555152;">
<div style="position: relative; width: 40px; float: right">
<%= Html.Gravatar(x.Email, new  { s = "40", r = "g" })%>

</div>
<!-- АДМИН ЧАСТЬ -->
<div class="edit_content">
   <%= Html.EditeAndDeliteArtycle((bool)x.mt_artycle_category.Blog, (bool)x.mt_artycle_category.News, x.Id,x.mt_artycle_category.Id, x.AddedBy, x.mt_artycle_category.AddedBy)%>      
 </div>
  <!--END АДМИН ЧАСТЬ -->
  <!--ПОСТ -->
<%if (x.UrlImageDescription.Length > 7)
                { %>
        <img src="<%=x.UrlImageDescription %>" alt="<%=x.Title %>" style="position: relative; width:100px; float: left; margin-right: 5px;" />
        <%} %>
<a href="<%=Url.Action("C", "C", new { a = x.Id, b=x.Path})%>">
    <%= x.Title%></a><%if (x.RegistrUser == true)
                        { %>&nbsp;&nbsp;&nbsp;&nbsp;<small>[только для зарегестрированных]</small> <%} %>
<br /><br />
<%= x.Description%>
<br />
<small>

<%= Html.Encode(String.Format("{0:d MMMM yyyy г.}", x.AddedDate))%></small>&nbsp;&nbsp;&nbsp;&nbsp;
<small>Блог: </small>
        <a href="<%=Url.Action("B", "C", new { a = x.mt_artycle_category.Id, b = 1,c=10, d=x.mt_artycle_category.Path })%>">
    <small><%= x.mt_artycle_category.Title.ToLower() %></small></a>&nbsp;&nbsp;&nbsp;&nbsp;   
<small>Просмотров: <%= x.Views %></small>
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
  <%} %></div><%} %></td></tr>
<%abc++;
  if (abc == 5)
      break;
    } %>
<!--END ПОСТ -->
</table>
</div>
<!--END ПОСТЫ НА ГЛАВНОЙ -->
<%} %>