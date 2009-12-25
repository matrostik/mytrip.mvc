<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/2_column.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  
    <%= ViewData["model_domain"]%>/�������
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright �  2009 ������ ���� �����������   */-->
    <!-- ����� ����� -->
    <%int cat = (int)ViewData["count_canegory"];
        if (HttpContext.Current.User.IsInRole("artycle_editor"))
      {%><div class="edit_content">
          ������� ��������: <a href="<%=Url.Action("ZE", "Artycle")%>"><img src="/content/images/create.png" alt="�������" style="border-width: 0px;" /></a>
      </div>
    <br />
    <br />
    <%if (cat > 0)
      { %>
    <div class="edit_content">
        �������: <a href="<%=Url.Action("ZW", "C", new { a = 0 })%>"><img src="/content/images/create.png" alt="�������" style="border-width: 0px;" /></a>
    </div>
    <%}
      } if (HttpContext.Current.User.IsInRole("chief_editor"))
        {%><div class="edit_content">
              ������� ��������: <a href="<%=Url.Action("ZE", "C")%>"><img src="/content/images/create.png" alt="�������" style="border-width: 0px;" /></a>
          </div>
    <br />
    <br /> <%if (cat > 0)
             { %>
    <div class="edit_content">
        �������: <a href="<%=Url.Action("ZW", "C", new { a = 0 })%>"><img src="/content/images/create.png" alt="�������" style="border-width: 0px;" /></a>
    </div>
    <%}
        }      
    %>
    <!--END ����� ����� -->
    <!-- ��������� -->
    <b style="font-size: 18px">�������</b>
    <!--END ��������� -->
    <br />
    <br />
    <!-- ������� -->
    <% Html.RenderPartial("_S_pager"); %>
    <% Html.RenderPartial("_artycle"); %>
    <% Html.RenderPartial("_S_pager"); %>
    <!--END ������� -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="RightContent" runat="server">
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
