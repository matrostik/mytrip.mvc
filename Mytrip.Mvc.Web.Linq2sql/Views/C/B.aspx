<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/2_column.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Mvc.Model.Linq2sql.mt_artycle_category>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
    <%= ViewData["model_domain"]%>/<%= Html.Encode(Model.Title) %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright �  2009 ������ ���� �����������   */-->
    <!-- ����� ����� -->
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
                      <!-- ������� ������� -->
                      �������: <a href="<%=Url.Action("ZA", "C")%>"><img src="/content/images/create.png" alt="�������" style="border-width: 0px;" /></a>
                      <!-- ������������� ������� -->
                      <a href="<%=Url.Action("ZB", "C", new { a = Model.Id })%>"><img src="/content/images/edit.png" alt="������" style="border-width: 0px;" /></a>
                      <%}
                     else {%> <!-- ������������� ������� -->
                     ����������: <a href="<%=Url.Action("ZD", "C", new { a = Model.Id })%>"><img src="/content/images/edit.png" alt="������" style="border-width: 0px;" /></a><% } %>
                      <!-- ������� ������� -->
                      <a href="<%=Url.Action("ZH", "C", new { a = Model.Id })%>" onclick="return confirm ('�� ������� ��� ������ ������� �������?');">
                          <img src="/content/images/delete.png" alt="�������" style="border-width: 0px;" /></a>
                  </div>
    <br />
    <br />
    <%} if (Model.CategoryId == 0)
                  {%>
    <div class="edit_content">
        ����������: <a href="<%=Url.Action("ZC", "C", new { a = Model.Id })%>"><img src="/content/images/create.png" alt="�������" style="border-width: 0px;" /></a>
    </div>
    <br />
    <br />
    <%} %>
    <div class="edit_content">
        ������: <a href="<%=Url.Action("ZJ", "C", new { a = Model.Id })%>"><img src="/content/images/create.png" alt="�������" style="border-width: 0px;" /></a>
    </div>
    <%
        } if (HttpContext.Current.User.IsInRole("chief_editor"))
              {
    %><div class="edit_content">
       <% if (Model.CategoryId == 0)
                     {%>
                      <!-- ������� ������� -->
                      �������: <a href="<%=Url.Action("ZA", "C")%>"><img src="/content/images/create.png" alt="�������" style="border-width: 0px;" /></a>
                      <!-- ������������� ������� -->
                      <a href="<%=Url.Action("ZB", "C", new { a = Model.Id })%>"><img src="/content/images/edit.png" alt="������" style="border-width: 0px;" /></a>
                      <%}
                     else {%> <!-- ������������� ������� -->
                     ����������: <a href="<%=Url.Action("ZD", "C", new { a = Model.Id })%>"><img src="/content/images/edit.png" alt="������" style="border-width: 0px;" /></a><% } %>
                      <!-- ������� ������� -->
                      <a href="<%=Url.Action("ZH", "C", new { a = Model.Id })%>" onclick="return confirm ('�� ������� ��� ������ ������� �������?');">
                          <img src="/content/images/delete.png" alt="�������" style="border-width: 0px;" /></a>
    </div>
    <br />
    <br />
    <%if (Model.CategoryId == 0)
      {%>
    <div class="edit_content">
        ����������: <a href="<%=Url.Action("ZC", "C", new { a = Model.Id })%>"><img src="/content/images/create.png" alt="�������" style="border-width: 0px;" /></a>
    </div>
    <br />
    <br />
    <%} %>
    <div class="edit_content">
        ������: <a href="<%=Url.Action("ZJ", "C", new { a = Model.Id })%>"><img src="/content/images/create.png" alt="�������" style="border-width: 0px;" /></a>
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
                   <!-- ������� ������� -->
                      ������� ��������: <a href="<%=Url.Action("ZE", "C")%>"><img src="/content/images/create.png" alt="�������" style="border-width: 0px;" /></a>
                       <!-- ������������� ������� -->
                       <a href="<%=Url.Action("ZB", "C", new { a = Model.Id })%>">
                          <img src="/content/images/edit.png" alt="������" style="border-width: 0px;" /></a>
                          <%} else {%> <!-- ������������� ������� -->
                     ����������: <a href="<%=Url.Action("ZD", "C", new { a = Model.Id })%>"><img src="/content/images/edit.png" alt="������" style="border-width: 0px;" /></a><% } %>
                           <!-- ������� ������� -->
                          <a href="<%=Url.Action("ZH", "C", new { a = Model.Id })%>" onclick="return confirm ('�� ������� ��� ������ ������� �������?');">
                              <img src="/content/images/delete.png" alt="�������" style="border-width: 0px;" /></a>
                  </div>
    <br />
    <br />
    <%} if (Model.CategoryId == 0)
                  {%>
    <div class="edit_content">
        ����������: <a href="<%=Url.Action("YZ", "C", new { a = Model.Id })%>"><img src="/content/images/create.png" alt="�������" style="border-width: 0px;" /></a>
    </div>
    <br />
    <br />
    <%} %><div class="edit_content">
        �������: <a href="<%=Url.Action("ZW", "C", new { a = Model.Id })%>"><img src="/content/images/create.png" alt="�������" style="border-width: 0px;" /></a>
    </div>
    <%
        } if (HttpContext.Current.User.IsInRole("chief_editor"))
              {
    %><div class="edit_content">
         <% if (Model.CategoryId == 0)
                     {%>
                   <!-- ������� ������� -->
                      ������� ��������: <a href="<%=Url.Action("ZE", "C")%>"><img src="/content/images/create.png" alt="�������" style="border-width: 0px;" /></a>
                       <!-- ������������� ������� -->
                       <a href="<%=Url.Action("ZB", "C", new { a = Model.Id })%>">
                          <img src="/content/images/edit.png" alt="������" style="border-width: 0px;" /></a>
                          <%} else {%> <!-- ������������� ������� -->
                     ����������: <a href="<%=Url.Action("ZD", "C", new { a = Model.Id })%>"><img src="/content/images/edit.png" alt="������" style="border-width: 0px;" /></a><% } %>
                           <!-- ������� ������� -->
                          <a href="<%=Url.Action("ZH", "C", new { a = Model.Id })%>" onclick="return confirm ('�� ������� ��� ������ ������� �������?');">
                              <img src="/content/images/delete.png" alt="�������" style="border-width: 0px;" /></a>
    </div>
    <br />
    <br />
    <%if (Model.CategoryId == 0)
      {%>
    <div class="edit_content">
        ����������: <a href="<%=Url.Action("YZ", "C", new { a = Model.Id })%>"><img src="/content/images/create.png" alt="�������" style="border-width: 0px;" /></a>
    </div>
    <br />
    <br />
    <%} %>
    <div class="edit_content">
        �������: <a href="<%=Url.Action("ZW", "C", new { a = Model.Id })%>"><img src="/content/images/create.png" alt="�������" style="border-width: 0px;" /></a>
    </div>
    <%}
          }

      }
      else
      {%>
    <!-- ������ ����� -->
    <div style="position: relative; width: 100px; float: right">
        <%= Html.Gravatar(Model.Email, new  { s = "100", r = "g" })%>
    </div>
    <!--END ������ ����� -->
    <%
        if (HttpContext.Current.User.IsInRole("blogger"))
        {
            if (Model.AddedBy == Page.User.Identity.Name)
            {%><div class="edit_content">
                ����: <a href="<%=Url.Action("ZG", "C", new { a = Model.Id })%>"><img src="/content/images/edit.png" alt="������" style="border-width: 0px;" /></a>
                 <a href="<%=Url.Action("ZI", "C", new { a = Model.Id })%>"
                    onclick="return confirm ('�� ������� ��� ������ ������� ����?');"><img src="/content/images/delete.png" alt="�������" style="border-width: 0px;" /></a>
            </div>
    <br />
    <br />
    <%if (Model.CategoryId == 0)
      {%>
    <div class="edit_content">
        ���� �����: <a href="<%=Url.Action("YY", "C", new { a = Model.Id })%>"><img src="/content/images/create.png" alt="�������" style="border-width: 0px;" /></a>
    </div>
    <br />
    <br />
    <%} %>
    <div class="edit_content">
        ����: <a href="<%=Url.Action("ZS", "C", new { a = Model.Id })%>"><img src="/content/images/create.png" alt="�������" style="border-width: 0px;" /></a>
    </div>
    <%
        }
        } if (HttpContext.Current.User.IsInRole("chief_editor"))
        {
            if (Model.AddedBy != Page.User.Identity.Name)
            {%><div class="edit_content">
                ����: <a href="<%=Url.Action("ZG", "C", new { a = Model.Id })%>"><img src="/content/images/edit.png" alt="������" style="border-width: 0px;" /></a> 
                <a href="<%=Url.Action("ZI", "C", new { a = Model.Id })%>"
                    onclick="return confirm ('�� ������� ��� ������ ������� ����?');"><img src="/content/images/delete.png" alt="�������" style="border-width: 0px;" /></a>
            </div>
    <%}
        }
      }
    
    %>
    <!--END ����� ����� -->
    <!-- ��������� -->
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
    <b style="font-style: italic">����� �����:
        <%= Model.AddedBy %></b>
    <br />
    <b style="font-style: italic">����������:
        <%=Model.Views%></b>
    <br />
    <b style="font-style: italic">������:
        <%=Model.mt_artycle.Count%></b>
    <%} %>
    <!--END ��������� -->
    <%if (Model.mt_artycle_category2.Count() != 0)
      { %>
    <div>
        <%foreach (mt_artycle_category y in Model.mt_artycle_category2)
          { %><a href="<%=Url.Action("B", "C", new { a = y.Id, b = 1, c=10, d=y.Path})%>">
        <%= y.Title%></a><br />
        <br />
        <%} %></div>
    <%} %>
    <!-- ������� -->
    <% Html.RenderPartial("_S_pager"); %>
    <% Html.RenderPartial("_artycle"); %>
    <% Html.RenderPartial("_S_pager"); %>
    <!--END ������� -->
</asp:Content>
<asp:Content ContentPlaceHolderID="RightContent" runat="server">
    <!-- ������ ������� -->
    <% Html.RenderPartial("_S_admin"); %>
    <% Html.RenderPartial("_S_search"); %>
    <% Html.RenderPartial("_S_menu_user"); %>
    <% Html.RenderPartial("_S_news_right"); %>
    <% Html.RenderPartial("_S_artycle_right"); %>
    <% Html.RenderPartial("_S_blog_right"); %>
    <% Html.RenderPartial("_S_teg_right"); %>
    <!--END ������ ������� -->
</asp:Content>
<asp:Content ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
