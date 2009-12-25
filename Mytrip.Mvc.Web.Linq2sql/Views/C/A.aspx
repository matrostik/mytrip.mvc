<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/2_column.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
  
    <%= ViewData["model_domain"]%>/Статьи
    
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright ©  2009 Стюхин Олег Анатольевич   */-->
    <!-- АДМИН ЧАСТЬ -->
    <%int cat = (int)ViewData["count_canegory"];
    if (HttpContext.Current.User.IsInRole("artycle_editor"))
      {%><div class="edit_content">
          рубрика: <a href="<%=Url.Action("ZA", "C")%>"><img src="/content/images/create.png" alt="создать" style="border-width: 0px;" /></a>
      </div>
    <br />
    <br /><%if (cat > 0)
            { %>
    <div class="edit_content">
        статья: <a href="<%=Url.Action("ZJ", "C", new { a = 0 })%>"><img src="/content/images/create.png" alt="создать" style="border-width: 0px;" /></a>
    </div>
    <%}
      } if (HttpContext.Current.User.IsInRole("chief_editor"))
    {%><div class="edit_content">
              рубрика: <a href="<%=Url.Action("ZA", "C")%>"><img src="/content/images/create.png" alt="создать" style="border-width: 0px;" /></a>
          </div>
    <br />
    <br /><%if (cat > 0)
            { %>
    <div class="edit_content">
        статья: <a href="<%=Url.Action("ZJ", "C", new { a = 0 })%>"><img src="/content/images/create.png" alt="создать" style="border-width: 0px;" /></a>
    </div>
    <%}
    }       
    %>
    <!--END АДМИН ЧАСТЬ -->
    <!-- ЗАГОЛОВОК -->
    <b style="font-size: 18px">Статьи</b>
    <!--END ЗАГОЛОВОК -->
    <br />
    <br />
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
