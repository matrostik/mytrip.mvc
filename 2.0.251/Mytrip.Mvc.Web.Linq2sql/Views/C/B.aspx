<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/2_column.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Mvc.Model.Linq2sql.mt_artycle_category>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
    <%= ViewData["abstract_model_domain"]%>/<%= Html.Encode(Model.Title) %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright �  2009 ������ ���� �����������   */-->
    <!-- ������ ����� -->
    <%if (Model.Blog == true)
      { %>
    <div style="position: relative; width: 100px; float: right">
        <%= Html.Gravatar(Model.Email, new { s = "100", r = "g" })%>
    </div><%} %>
    <!--END ������ ����� -->  
 <!-- ����� ����� -->
    <div class="edit_content">
    <%= Html.CreateCategory(Model.News,Model.Blog, Model.CategoryId) %>
    <%if (Model.CategoryId == 0) {%>
    <%= Html.EditeAndDeliteCategory(Model.Blog, Model.News, Model.Id, Model.CategoryId, Model.AddedBy)%>
    <% }
      else
      { %>
    <%= Html.EditeAndDeliteCategory(Model.Blog, Model.News, Model.Id, Model.CategoryId, Model.AddedBy, Model.mt_artycle_category1.AddedBy)%>
    <%} %>    
    <%= Html.CreateReCategory(Model.News,Model.Blog,Model.Id,Model.CategoryId,Model.AddedBy) %>  
    <%= Html.CreateArtycle(Model.Id,Model.News, Model.Blog, Model.AddedBy) %>
    </div>
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
   <% Html.RenderPartial("_S_right_column"); %> 
    <!--END ������ ������� -->
</asp:Content>
<asp:Content ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
