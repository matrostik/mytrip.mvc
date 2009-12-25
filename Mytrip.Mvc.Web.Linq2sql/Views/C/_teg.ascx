<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright ©  2009 Стюхин Олег Анатольевич   */-->
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div class="artycle_summtop"></div>
<table>
<%
  foreach (mt_artycle_in_teg x in (IEnumerable<mt_artycle_in_teg>)ViewData["artycle"])
  {
     
%><tr><td>
<%if (x.mt_artycle.mt_artycle_category.Blog == true)
  {
      if (HttpContext.Current.User.IsInRole("blogger"))
      {
          if (x.mt_artycle.AddedBy == Page.User.Identity.Name)
          {%>
<div class="edit_content"> 
<a href="<%=Url.Action("ZT", "C", new { a = x.mt_artycle.Id })%>"><img src="/content/images/edit.png" alt="правка" style="border-width: 0px;" /></a>
<a href="<%=Url.Action("ZM", "C", new { a = x.mt_artycle.Id })%>" onclick = "return confirm ('Вы уверены что хотите удалить пост?');"><img src="/content/images/delete.png" alt="удалить" style="border-width: 0px;" /></a>     
</div>
<%} if (HttpContext.Current.User.IsInRole("chief_editor"))
          {
              if (x.mt_artycle.AddedBy != Page.User.Identity.Name)
              {%><div class="edit_content">
<a href="<%=Url.Action("ZT", "C", new { a = x.mt_artycle.Id })%>"><img src="/content/images/edit.png" alt="правка" style="border-width: 0px;" /></a>
<a href="<%=Url.Action("ZM", "C", new { a = x.mt_artycle.Id })%>" onclick = "return confirm ('Вы уверены что хотите удалить пост?');"><img src="/content/images/delete.png" alt="удалить" style="border-width: 0px;" /></a>     
</div>
<%}
          }
      }
  }
  else
  {
      if (x.mt_artycle.mt_artycle_category.News == false)
      {
          if (HttpContext.Current.User.IsInRole("artycle_editor"))
          {
              if (x.mt_artycle.mt_artycle_category.AddedBy == Page.User.Identity.Name)
              {%><div class="edit_content">
<a href="<%=Url.Action("ZK", "C", new { a = x.mt_artycle.Id, b=x.mt_artycle.CategoryId })%>"><img src="/content/images/edit.png" alt="правка" style="border-width: 0px;" /></a>
<a href="<%=Url.Action("ZL", "C", new { a = x.mt_artycle.Id })%>" onclick = "return confirm ('Вы уверены что хотите удалить статью?');"><img src="/content/images/delete.png" alt="удалить" style="border-width: 0px;" /></a>     
</div>
<%}
          } if (HttpContext.Current.User.IsInRole("chief_editor"))
          {%><div class="edit_content">
<a href="<%=Url.Action("ZK", "C", new { a = x.mt_artycle.Id, b=x.mt_artycle.CategoryId })%>"><img src="/content/images/edit.png" alt="правка" style="border-width: 0px;" /></a>
<a href="<%=Url.Action("ZL", "C", new { a = x.mt_artycle.Id })%>" onclick = "return confirm ('Вы уверены что хотите удалить статью?');"><img src="/content/images/delete.png" alt="удалить" style="border-width: 0px;" /></a>     
</div>
<%}
      }
      else { 
      if (HttpContext.Current.User.IsInRole("artycle_editor"))
          {
              if (x.mt_artycle.mt_artycle_category.AddedBy == Page.User.Identity.Name)
              {%><div class="edit_content">
<a href="<%=Url.Action("ZX", "C", new { a = x.mt_artycle.Id, b=x.mt_artycle.CategoryId })%>"><img src="/content/images/edit.png" alt="правка" style="border-width: 0px;" /></a>
<a href="<%=Url.Action("ZL", "C", new { a = x.mt_artycle.Id })%>" onclick = "return confirm ('Вы уверены что хотите удалить новость?');"><img src="/content/images/delete.png" alt="удалить" style="border-width: 0px;" /></a>     
</div>
<%}
          } if (HttpContext.Current.User.IsInRole("chief_editor"))
          {%><div class="edit_content">
<a href="<%=Url.Action("ZX", "C", new { a = x.mt_artycle.Id, b=x.mt_artycle.CategoryId })%>"><img src="/content/images/edit.png" alt="правка" style="border-width: 0px;" /></a>
<a href="<%=Url.Action("ZL", "C", new { a = x.mt_artycle.Id })%>" onclick = "return confirm ('Вы уверены что хотите удалить новость?');"><img src="/content/images/delete.png" alt="удалить" style="border-width: 0px;" /></a>     
</div>
<%}
      }
         
  }%>
<%if (x.mt_artycle.UrlImageDescription.Length > 7)
                { %>
        <img src="<%=x.mt_artycle.UrlImageDescription %>" alt="<%=x.mt_artycle.Title %>" style="position: relative; width:120px; float: left; margin-right: 5px;" />
        <%} %>



<a href="<%=Url.Action("C", "C", new { a = x.mt_artycle.Id, b=x.mt_artycle.Path})%>">
    <%= x.mt_artycle.Title%></a> <%if (x.mt_artycle.RegistrUser == true)
                        { %>&nbsp;&nbsp;&nbsp;&nbsp;<small>[только для зарегестрированных]</small> <%} %>
<br /><br />
<%= x.mt_artycle.Description%></td></tr>
<tr><td style="border-bottom-style: dotted; border-bottom-width: 1px; border-bottom-color: #555152">
<small><%= Html.Encode(String.Format("{0:d MMMM yyyy г.}", x.mt_artycle.AddedDate))%></small>&nbsp;&nbsp;&nbsp;&nbsp;

<%if (x.mt_artycle.mt_artycle_category.CategoryId == 0)
  {
      if (x.mt_artycle.mt_artycle_category.Blog == false)
      {
        
      %><small>Рубрика: </small><%}
      else
      { %><small>Блог: </small><%} %>
        <a href="<%=Url.Action("B", "C", new { a = x.mt_artycle.mt_artycle_category.Id, b = 1,c=10, d=x.mt_artycle.mt_artycle_category.Path })%>">
    <small><%= x.mt_artycle.mt_artycle_category.Title.ToLower()%></small></a>&nbsp;&nbsp;&nbsp;&nbsp;
    <%}
  else {if (x.mt_artycle.mt_artycle_category.Blog == false)
      {
        
      %><small>Рубрика: </small><%}
      else
      { %><small>Блог: </small><%} %>
        <a href="<%=Url.Action("B", "C", new { a = x.mt_artycle.mt_artycle_category.CategoryId, b = 1,c=10, d=x.mt_artycle.mt_artycle_category.mt_artycle_category1.Path })%>">
    <small><%= x.mt_artycle.mt_artycle_category.mt_artycle_category1.Title.ToLower()%></small></a>&nbsp;&nbsp;&nbsp;&nbsp;
    <% } if (x.mt_artycle.mt_artycle_category.CategoryId != 0)
      {
          if (x.mt_artycle.mt_artycle_category.mt_artycle_category1.Blog == false)
          {
        
      %><small>Под рубрика: </small><%}
          else
          { %><small>Тема блога: </small><%} %>
        <a href="<%=Url.Action("B", "C", new { a = x.mt_artycle.mt_artycle_category.Id, b = 1,c=10, d=x.mt_artycle.mt_artycle_category.Path })%>">
    <small><%= x.mt_artycle.mt_artycle_category.Title.ToLower()%></small></a>&nbsp;&nbsp;&nbsp;&nbsp;   
<%

  
    } %>   
<small>Просмотров: <%= x.mt_artycle.Views%></small>
<%if (x.mt_artycle.AddComment == true)
  { %>&nbsp;&nbsp;&nbsp;&nbsp;<small>Комментариев: <%= x.mt_artycle.mt_artycle_comment.Count() %></small><%} %>
  <%if (x.mt_artycle.ApprovedVotes == true)
  { %>&nbsp;&nbsp;&nbsp;&nbsp;<small>Рейтинг: <%= x.mt_artycle.Votes%></small><%}%>
    
  <div><small>Теги: </small>
  <%foreach (mt_artycle_in_teg y in x.mt_artycle.mt_artycle_in_teg)
    { %>&nbsp;<a href="<%=Url.Action("E", "C", new { a = y.mt_artycle_teg.Id, b = 1, c=10, d=y.mt_artycle_teg.Path})%>">
    <small><%= y.mt_artycle_teg.Title%></small></a>
  <%} %></div>
</td></tr>
<%
    } %>
</table>

