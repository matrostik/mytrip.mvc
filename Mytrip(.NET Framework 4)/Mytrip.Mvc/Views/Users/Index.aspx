<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Core.Models.IndexUsersModel>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
<%  //************************************************************ 
    // Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich
    // To learn more about Mytrip.Mvc.Entyty visit 
    // http://starterkitmytripmvc.codeplex.com/
    // mytripmvc@gmail.com
    // license: Microsoft Public License (Ms-PL) 
    // *********************************************************** %>
    <%=Html.PageTitle(CoreLanguage.all_users, "/")%>
</asp:Content>
<asp:Content ContentPlaceHolderID="ScriptContent" runat="server">
<script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="adminlink">
        <%= Html.ActionLink(CoreLanguage.all_roles, "IndexRole", new { pageIndex = 1, pageSize = 10, sorting = "RoleName" })%></h2>
    <h2>
        <%=CoreLanguage.all_users%></h2>
    <%=Html.MytripPager(Model.DefaultCount,Model.Total,"...")%>
    <table>
        <tr>
            <th class="oneinput">
            <% Html.EnableClientValidation(); %>
                <% using (Html.BeginForm())
                   { %><%= Html.ValidationMessageFor(m => m.Search) %><br />
                <%= Html.TextBoxFor(m=>m.Search)%>
                <%=Html.MytripInput("submit", CoreLanguage.search_user)%>
                
                <%} %>
            </th>
        </tr>
    </table>
    <table>
        <tr>
            <th class="editdelite">
            </th>
            <th>
                <%= Html.ActionLink(CoreLanguage.UserName, "Index", new { pageIndex = 1, pageSize = Model.DefaultCount, sorting = Html.MytripSort("UserName") })%>
            </th>
            <th>
                <%= Html.ActionLink(CoreLanguage.Email, "Index", new { pageIndex = 1, pageSize = Model.DefaultCount, sorting = Html.MytripSort("Email") })%>
            </th>
            <th>
                <%= Html.ActionLink(CoreLanguage.LastActivityDate, "Index", new { pageIndex = 1, pageSize = Model.DefaultCount, sorting = Html.MytripSort("LastActivityDate") })%>
            </th>
            <th>
                <%= Html.ActionLink(CoreLanguage.CreationDate, "Index", new { pageIndex = 1, pageSize = Model.DefaultCount, sorting = Html.MytripSort("CreationDate") })%>
            </th>
            <th>
                <%= Html.ActionLink(CoreLanguage.LastLoginDate, "Index", new { pageIndex = 1, pageSize = Model.DefaultCount, sorting = Html.MytripSort("LastLoginDate") })%>
            </th>
            <th>
                <%= Html.ActionLink(CoreLanguage.LastPasswordChangedDate, "Index", new { pageIndex = 1, pageSize = Model.DefaultCount, sorting = Html.MytripSort("LastPasswordChangedDate") })%>
            </th>
            <th>
                <%= Html.ActionLink(CoreLanguage.UserIP, "Index", new { pageIndex = 1, pageSize = Model.DefaultCount, sorting = Html.MytripSort("UserIP") })%>
            </th>
            <th>
                <%=CoreLanguage.IsApproved%>
            </th>
        </tr>
        <%=Html.UserIndex(Model.Users,Model.UsersXml,CoreLanguage.are_you_sure) %>
    </table>
    <%=Html.MytripPager(Model.DefaultCount, Model.Total, "...")%>
</asp:Content>
