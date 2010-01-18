<%@ Page Language="C#" MasterPageFile="~/Views/Shared/2_column.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
    <%= ViewData["abstract_model_domain"]%>/<%= Mytrip_Mvc_Language.menu_home %>
</asp:Content>
<asp:Content ContentPlaceHolderID="RightContent" runat="server">
<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright ©  2009 Стюхин Олег Анатольевич   */-->
    <% Html.RenderPartial("_S_right_column"); %>    
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
 <% Html.RenderPartial("_news"); %>
 <% Html.RenderPartial("_artycle_category"); %>  
   <% Html.RenderPartial("_artycle"); %>    
    <% Html.RenderPartial("_post"); %>
</asp:Content>
