<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/2_column.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Mvc.Model.Linq2sql.mt_artycle_category>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
    <%= ViewData["model_domain"]%>/<%= Html.Encode(Model.Title) %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright ©  2009 Стюхин Олег Анатольевич   */-->
    <!-- АДМИН ЧАСТЬ -->
    <%if (Model.Blog == false)
      {
          if (Model.News == false)
          {
              if (HttpContext.Current.User.IsInRole("artycle_editor"))
              {
                  if (Model.AddedBy == Page.User.Identity.Name)
                  {%><div class="edit_content">
                  <% if (Model.CategoryId == 0)
                     {%>
                      <!-- создать рубрику -->
                      рубрика: <a href="<%=Url.Action("ZA", "C")%>"><img src="/content/images/create.png" alt="создать" style="border-width: 0px;" /></a>
                      <!-- редактировать рубрику -->
                      <a href="<%=Url.Action("ZB", "C", new { a = Model.Id })%>"><img src="/content/images/edit.png" alt="правка" style="border-width: 0px;" /></a>
                      <%}
                     else {%> <!-- редактировать рубрику -->
                     подрубрика: <a href="<%=Url.Action("ZD", "C", new { a = Model.Id })%>"><img src="/content/images/edit.png" alt="правка" style="border-width: 0px;" /></a><% } %>
                      <!-- удалить рубрику -->
                      <a href="<%=Url.Action("ZH", "C", new { a = Model.Id })%>" onclick="return confirm ('Вы уверены что хотите удалить рубрику?');">
                          <img src="/content/images/delete.png" alt="удалить" style="border-width: 0px;" /></a>
                  </div>
    <br />
    <br />
    <%} if (Model.CategoryId == 0)
                  {%>
    <div class="edit_content">
        подрубрика: <a href="<%=Url.Action("ZC", "C", new { a = Model.Id })%>"><img src="/content/images/create.png" alt="создать" style="border-width: 0px;" /></a>
    </div>
    <br />
    <br />
    <%} %>
    <div class="edit_content">
        статья: <a href="<%=Url.Action("ZJ", "C", new { a = Model.Id })%>"><img src="/content/images/create.png" alt="создать" style="border-width: 0px;" /></a>
    </div>
    <%
        } if (HttpContext.Current.User.IsInRole("chief_editor"))
              {
    %><div class="edit_content">
       <% if (Model.CategoryId == 0)
                     {%>
                      <!-- создать рубрику -->
                      рубрика: <a href="<%=Url.Action("ZA", "C")%>"><img src="/content/images/create.png" alt="создать" style="border-width: 0px;" /></a>
                      <!-- редактировать рубрику -->
                      <a href="<%=Url.Action("ZB", "C", new { a = Model.Id })%>"><img src="/content/images/edit.png" alt="правка" style="border-width: 0px;" /></a>
                      <%}
                     else {%> <!-- редактировать рубрику -->
                     подрубрика: <a href="<%=Url.Action("ZD", "C", new { a = Model.Id })%>"><img src="/content/images/edit.png" alt="правка" style="border-width: 0px;" /></a><% } %>
                      <!-- удалить рубрику -->
                      <a href="<%=Url.Action("ZH", "C", new { a = Model.Id })%>" onclick="return confirm ('Вы уверены что хотите удалить рубрику?');">
                          <img src="/content/images/delete.png" alt="удалить" style="border-width: 0px;" /></a>
    </div>
    <br />
    <br />
    <%if (Model.CategoryId == 0)
      {%>
    <div class="edit_content">
        подрубрика: <a href="<%=Url.Action("ZC", "C", new { a = Model.Id })%>"><img src="/content/images/create.png" alt="создать" style="border-width: 0px;" /></a>
    </div>
    <br />
    <br />
    <%} %>
    <div class="edit_content">
        статья: <a href="<%=Url.Action("ZJ", "C", new { a = Model.Id })%>"><img src="/content/images/create.png" alt="создать" style="border-width: 0px;" /></a>
    </div>
    <%
        }
          }
          else
          {
              if (HttpContext.Current.User.IsInRole("artycle_editor"))
              {
                  if (Model.AddedBy == Page.User.Identity.Name)
                  {%><div class="edit_content">
                  <% if (Model.CategoryId == 0)
                     {%>
                   <!-- создать рубрику -->
                      рубрика новостей: <a href="<%=Url.Action("ZE", "C")%>"><img src="/content/images/create.png" alt="создать" style="border-width: 0px;" /></a>
                       <!-- редактировать рубрику -->
                       <a href="<%=Url.Action("ZB", "C", new { a = Model.Id })%>">
                          <img src="/content/images/edit.png" alt="правка" style="border-width: 0px;" /></a>
                          <%} else {%> <!-- редактировать рубрику -->
                     подрубрика: <a href="<%=Url.Action("ZD", "C", new { a = Model.Id })%>"><img src="/content/images/edit.png" alt="правка" style="border-width: 0px;" /></a><% } %>
                           <!-- удалить рубрику -->
                          <a href="<%=Url.Action("ZH", "C", new { a = Model.Id })%>" onclick="return confirm ('Вы уверены что хотите удалить рубрику?');">
                              <img src="/content/images/delete.png" alt="удалить" style="border-width: 0px;" /></a>
                  </div>
    <br />
    <br />
    <%} if (Model.CategoryId == 0)
                  {%>
    <div class="edit_content">
        подрубрика: <a href="<%=Url.Action("YZ", "C", new { a = Model.Id })%>"><img src="/content/images/create.png" alt="создать" style="border-width: 0px;" /></a>
    </div>
    <br />
    <br />
    <%} %><div class="edit_content">
        новость: <a href="<%=Url.Action("ZW", "C", new { a = Model.Id })%>"><img src="/content/images/create.png" alt="создать" style="border-width: 0px;" /></a>
    </div>
    <%
        } if (HttpContext.Current.User.IsInRole("chief_editor"))
              {
    %><div class="edit_content">
         <% if (Model.CategoryId == 0)
                     {%>
                   <!-- создать рубрику -->
                      рубрика новостей: <a href="<%=Url.Action("ZE", "C")%>"><img src="/content/images/create.png" alt="создать" style="border-width: 0px;" /></a>
                       <!-- редактировать рубрику -->
                       <a href="<%=Url.Action("ZB", "C", new { a = Model.Id })%>">
                          <img src="/content/images/edit.png" alt="правка" style="border-width: 0px;" /></a>
                          <%} else {%> <!-- редактировать рубрику -->
                     подрубрика: <a href="<%=Url.Action("ZD", "C", new { a = Model.Id })%>"><img src="/content/images/edit.png" alt="правка" style="border-width: 0px;" /></a><% } %>
                           <!-- удалить рубрику -->
                          <a href="<%=Url.Action("ZH", "C", new { a = Model.Id })%>" onclick="return confirm ('Вы уверены что хотите удалить рубрику?');">
                              <img src="/content/images/delete.png" alt="удалить" style="border-width: 0px;" /></a>
    </div>
    <br />
    <br />
    <%if (Model.CategoryId == 0)
      {%>
    <div class="edit_content">
        подрубрика: <a href="<%=Url.Action("YZ", "C", new { a = Model.Id })%>"><img src="/content/images/create.png" alt="создать" style="border-width: 0px;" /></a>
    </div>
    <br />
    <br />
    <%} %>
    <div class="edit_content">
        новость: <a href="<%=Url.Action("ZW", "C", new { a = Model.Id })%>"><img src="/content/images/create.png" alt="создать" style="border-width: 0px;" /></a>
    </div>
    <%}
          }

      }
      else
      {%>
    <!-- АВАТАР БЛОГА -->
    <div style="position: relative; width: 100px; float: right">
        <%= Html.Gravatar(Model.Email, new  { s = "100", r = "g" })%>
    </div>
    <!--END АВАТАР БЛОГА -->
    <%
        if (HttpContext.Current.User.IsInRole("blogger"))
        {
            if (Model.AddedBy == Page.User.Identity.Name)
            {%><div class="edit_content">
                блог: <a href="<%=Url.Action("ZG", "C", new { a = Model.Id })%>"><img src="/content/images/edit.png" alt="правка" style="border-width: 0px;" /></a>
                 <a href="<%=Url.Action("ZI", "C", new { a = Model.Id })%>"
                    onclick="return confirm ('Вы уверены что хотите удалить блог?');"><img src="/content/images/delete.png" alt="удалить" style="border-width: 0px;" /></a>
            </div>
    <br />
    <br />
    <%if (Model.CategoryId == 0)
      {%>
    <div class="edit_content">
        тема блога: <a href="<%=Url.Action("YY", "C", new { a = Model.Id })%>"><img src="/content/images/create.png" alt="создать" style="border-width: 0px;" /></a>
    </div>
    <br />
    <br />
    <%} %>
    <div class="edit_content">
        пост: <a href="<%=Url.Action("ZS", "C", new { a = Model.Id })%>"><img src="/content/images/create.png" alt="создать" style="border-width: 0px;" /></a>
    </div>
    <%
        }
        } if (HttpContext.Current.User.IsInRole("chief_editor"))
        {
            if (Model.AddedBy != Page.User.Identity.Name)
            {%><div class="edit_content">
                блог: <a href="<%=Url.Action("ZG", "C", new { a = Model.Id })%>"><img src="/content/images/edit.png" alt="правка" style="border-width: 0px;" /></a> 
                <a href="<%=Url.Action("ZI", "C", new { a = Model.Id })%>"
                    onclick="return confirm ('Вы уверены что хотите удалить блог?');"><img src="/content/images/delete.png" alt="удалить" style="border-width: 0px;" /></a>
            </div>
    <%}
        }
      }
    
    %>
    <!--END АДМИН ЧАСТЬ -->
    <!-- ЗАГОЛОВОК -->
    <%if (Model.CategoryId != 0)
      {%><a href="<%=Url.Action("B", "C", new { a = Model.CategoryId, b = 1, c=10, d=Model.mt_artycle_category1.Path})%>">
          <h2>
              <%= Html.Encode(Model.mt_artycle_category1.Title) %></h2>
      </a>
    <%} %>
    <h2>
        <%= Html.Encode(Model.Title) %></h2>
    <%if (Model.Blog == true)
      {%>
    <b style="font-style: italic">Автор блога:
        <%= Model.AddedBy %></b>
    <br />
    <b style="font-style: italic">просмотров:
        <%=Model.Views%></b>
    <br />
    <b style="font-style: italic">постов:
        <%=Model.mt_artycle.Count%></b>
    <%} %>
    <!--END ЗАГОЛОВОК -->
    <%if (Model.mt_artycle_category2.Count() != 0)
      { %>
    <div>
        <%foreach (mt_artycle_category y in Model.mt_artycle_category2)
          { %><a href="<%=Url.Action("B", "C", new { a = y.Id, b = 1, c=10, d=y.Path})%>">
        <%= y.Title%></a><br />
        <br />
        <%} %></div>
    <%} %>
    <!-- КОНТЕНТ -->
    <% Html.RenderPartial("_S_pager"); %>
    <% Html.RenderPartial("_artycle"); %>
    <% Html.RenderPartial("_S_pager"); %>
    <!--END КОНТЕНТ -->
</asp:Content>
<asp:Content ContentPlaceHolderID="RightContent" runat="server">
    <!-- ПРАВАЯ КОЛОНКА -->
    <% Html.RenderPartial("_S_admin"); %>
    <% Html.RenderPartial("_S_search"); %>
    <% Html.RenderPartial("_S_menu_user"); %>
    <% Html.RenderPartial("_S_news_right"); %>
    <% Html.RenderPartial("_S_artycle_right"); %>
    <% Html.RenderPartial("_S_blog_right"); %>
    <% Html.RenderPartial("_S_teg_right"); %>
    <!--END ПРАВАЯ КОЛОНКА -->
</asp:Content>
<asp:Content ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
