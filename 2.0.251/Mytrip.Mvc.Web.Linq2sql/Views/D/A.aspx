<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/2_column.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= ViewData["abstract_model_domain"]%>/<%=Mytrip_Mvc_Language.results_search%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright ©  2009 Ñòþõèí Îëåã Àíàòîëüåâè÷   */-->
    <h2><%=Mytrip_Mvc_Language.results_search%></h2>
<!--END ÇÀÃÎËÎÂÎÊ -->
    <!-- ÊÎÍÒÅÍÒ -->
    <% Html.RenderPartial("_S_pager"); %>
    <% Html.RenderPartial("_artycle"); %>
    <% Html.RenderPartial("_S_pager"); %>
    <!--END ÊÎÍÒÅÍÒ -->
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="RightContent" runat="server">
 <% Html.RenderPartial("_S_right_column"); %> 
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
