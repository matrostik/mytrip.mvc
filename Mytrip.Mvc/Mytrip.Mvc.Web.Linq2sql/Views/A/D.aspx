<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/1_column.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= ViewData["model_domain"]%>/<%=Mytrip_Mvc_Language_1.users%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div style="position: relative; float: right;">
<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright �  2009 ������ ���� �����������   */-->
    <% using (Html.BeginForm())
   {%><label ><%=Mytrip_Mvc_Language_1.search_users%></label>
   <div style=" height: 20px">
    <input type="text" name="search" class="search_text" /><input type="submit" value="" class="search_bottom"  /></div>
    <% } %>   </div>
     
     
    <h2><%=Mytrip_Mvc_Language_1.users%></h2>
    <p><%=Html.Language(Mytrip_Mvc_Language_1.user_online,ViewData["UsersOnlineNow"].ToString())%></p>
    <p><%=Html.Language(Mytrip_Mvc_Language_1.total_registered, ViewData["content_count"].ToString())%></p>
<!-- ������� -->
    <% Html.RenderPartial("_S_pager"); %>
    <% Html.RenderPartial("_users"); %>
    <% Html.RenderPartial("_S_pager"); %>
    <!--END ������� -->
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
