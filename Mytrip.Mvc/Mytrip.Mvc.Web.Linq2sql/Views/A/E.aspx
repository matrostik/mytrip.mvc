<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/1_column.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= ViewData["model_domain"]%>/<%=Mytrip_Mvc_Language_1.results_search%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div style="position: relative; float: right;">
<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright ©  2009 Ñòþõèí Îëåã Àíàòîëüåâè÷   */-->
    <% using (Html.BeginForm())
   {%><label ><%=Mytrip_Mvc_Language_1.search_users%></label>
   <div style=" height: 20px">
    <input type="text" name="search" class="search_text" /><input type="submit" value="" class="search_bottom"  /></div>
    <% } %>   </div>
     
     
    <h2><%=Mytrip_Mvc_Language_1.results_search%></h2>
<!-- ÊÎÍÒÅÍÒ -->
    <% Html.RenderPartial("_S_pager"); %>
    <% Html.RenderPartial("_users"); %>
    <% Html.RenderPartial("_S_pager"); %>
    <!--END ÊÎÍÒÅÍÒ -->
    <p>
        <a href="<%=Url.Action("D", "A", new{a=0, b=1,c=25,d="Users"})%>">
    <%=Mytrip_Mvc_Language_1.menu_management_users%></a>
    </p>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
