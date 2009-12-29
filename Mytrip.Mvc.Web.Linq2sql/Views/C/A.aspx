<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/2_column.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
  
    <%= ViewData["model_domain"]%>/—Ú‡Ú¸Ë
    
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright ©  2009 —Ú˛ıËÌ ŒÎÂ„ ¿Ì‡ÚÓÎ¸Â‚Ë˜   */-->
    <!-- ¿ƒÃ»Õ ◊¿—“‹ -->
    <%int cat = (int)ViewData["count_canegory"];%>
    <div class="edit_content">
    <%= Html.CreateCategory(false) %>
    <%= Html.CreateArtycle(cat, false) %>
      </div>   
    <!--END ¿ƒÃ»Õ ◊¿—“‹ -->
    <!-- «¿√ŒÀŒ¬Œ  -->
    <b style="font-size: 18px">—Ú‡Ú¸Ë</b>
    <!--END «¿√ŒÀŒ¬Œ  -->
    <br />
    <br />
    <!--  ŒÕ“≈Õ“ -->
    <% Html.RenderPartial("_S_pager"); %>
    <% Html.RenderPartial("_artycle"); %>
    <% Html.RenderPartial("_S_pager"); %>
    <!--END  ŒÕ“≈Õ“ -->
</asp:Content>
<asp:Content ContentPlaceHolderID="RightContent" runat="server">
    <!-- œ–¿¬¿ﬂ  ŒÀŒÕ ¿ -->
     <% Html.RenderPartial("_S_admin"); %>
    <% Html.RenderPartial("_S_search"); %>
    <% Html.RenderPartial("_S_menu_user"); %>
    <% Html.RenderPartial("_S_news_right"); %>
    <% Html.RenderPartial("_S_artycle_right"); %>
    <% Html.RenderPartial("_S_blog_right"); %>
    <% Html.RenderPartial("_S_teg_right"); %>
    <!--END œ–¿¬¿ﬂ  ŒÀŒÕ ¿ -->
</asp:Content>
