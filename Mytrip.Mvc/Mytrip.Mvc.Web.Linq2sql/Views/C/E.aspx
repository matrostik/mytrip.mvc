<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/2_column.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Mvc.Model.Linq2sql.mt_artycle_teg>" %>

<asp:Content  ContentPlaceHolderID="TitleContent" runat="server">
    <%= ViewData["model_domain"]%>/<%= Html.Encode(Model.Title) %>
    
</asp:Content>
<asp:Content  ContentPlaceHolderID="MainContent" runat="server">
<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright �  2009 ������ ���� �����������   */-->
    <!-- ����� ����� -->
     <%
         if (HttpContext.Current.User.IsInRole("blogger"))
         {
             if (Model.AddedBy == Page.User.Identity.Name)
             {%><div class="edit_content">                  
                  <a href="<%=Url.Action("ZU", "C", new { a = Model.Id })%>">
                      <img src="/content/images/edit.png" alt="<%=Mytrip_Mvc_Language_1.edit%>" style="border-width: 0px;" /></a> <a href="<%=Url.Action("ZV", "C", new { a = Model.Id })%>"
                          onclick="return confirm ('<%=Mytrip_Mvc_Language_2.delete_teg%>');"><img src="/content/images/delete.png" alt="�������" style="border-width: 0px;" /></a>
              </div>   
    <% } if (HttpContext.Current.User.IsInRole("chief_editor"))
             {
                 if (Model.AddedBy != Page.User.Identity.Name)
                 {%><div class="edit_content">                  
                  <a href="<%=Url.Action("ZU", "C", new { a = Model.Id })%>">
                     <img src="/content/images/edit.png" alt="<%=Mytrip_Mvc_Language_1.edit%>" style="border-width: 0px;" /></a> <a href="<%=Url.Action("ZV", "C", new { a = Model.Id })%>"
                          onclick="return confirm ('<%=Mytrip_Mvc_Language_2.delete_teg%>');"><img src="/content/images/delete.png" alt="�������" style="border-width: 0px;" /></a>
              </div>  
    <%}
             }
    }else{ if (HttpContext.Current.User.IsInRole("artycle_editor"))
          {
              if (Model.AddedBy == Page.User.Identity.Name)
              {%><div class="edit_content">                  
                  <a href="<%=Url.Action("ZU", "C", new { a = Model.Id })%>">
                      <img src="/content/images/edit.png" alt="<%=Mytrip_Mvc_Language_1.edit%>" style="border-width: 0px;" /></a> <a href="<%=Url.Action("ZV", "C", new { a = Model.Id })%>"
                          onclick="return confirm ('<%=Mytrip_Mvc_Language_2.delete_teg%>');"><img src="/content/images/delete.png" alt="�������" style="border-width: 0px;" /></a>
              </div>   
    <%}%>
    <%} if (HttpContext.Current.User.IsInRole("chief_editor"))
          {
             %><div class="edit_content">                  
                  <a href="<%=Url.Action("ZU", "C", new { a = Model.Id })%>">
                      <img src="/content/images/edit.png" alt="<%=Mytrip_Mvc_Language_1.edit%>" style="border-width: 0px;" /></a> <a href="<%=Url.Action("ZV", "C", new { a = Model.Id })%>"
                          onclick="return confirm ('<%=Mytrip_Mvc_Language_2.delete_teg%>');"><img src="/content/images/delete.png" alt="�������" style="border-width: 0px;" /></a>
              </div>
    <%
  }}%>
    <!--END ����� ����� -->
    <!-- ��������� -->
    <b style="font-size: 18px">
        <%= Html.Encode(Model.Title) %></b>
         <!--END ��������� -->
        <br />
    <br /> 
   
    <!-- ������� -->
    <% Html.RenderPartial("_S_pager"); %>
    <% Html.RenderPartial("_teg"); %>
    <% Html.RenderPartial("_S_pager"); %>
    <!--END ������� -->
</asp:Content>
<asp:Content  ContentPlaceHolderID="RightContent" runat="server">
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
<asp:Content  ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>