<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/1_column.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= ViewData["model_domain"]%>/Результаты поиска
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div style="position: relative; float: right;">
<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright ©  2009 Стюхин Олег Анатольевич   */-->
    <% using (Html.BeginForm())
   {%><label >Поиск по Логину или Email:</label>
   <div style=" height: 20px">
    <input type="text" name="search" class="search_text" /><input type="submit" value="" class="search_bottom"  /></div>
    <% } %>   </div>
     
     
    <h2>Результаты поиска</h2>
    <p>Сейчас на сайте: <%=ViewData["UsersOnlineNow"]%></p>
<!-- КОНТЕНТ -->
    <% Html.RenderPartial("_S_pager"); %>
    <% Html.RenderPartial("_users"); %>
    <% Html.RenderPartial("_S_pager"); %>
    <!--END КОНТЕНТ -->
    <p>
        <a href="<%=Url.Action("D", "A", new{a=0, b=1,c=25,d="Users"})%>">
    управление пользователями</a>
    </p>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
