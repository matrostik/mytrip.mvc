<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Core.Models.IndexRolesModel>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
<%  //************************************************************ 
    // Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich
    // To learn more about Mytrip.Mvc.Entyty visit 
    // http://starterkitmytripmvc.codeplex.com/
    // mytripmvc@gmail.com
    // license: Microsoft Public License (Ms-PL) 
    // *********************************************************** %>
    <%=Html.PageTitle(CoreLanguage.all_roles, "/")%>
</asp:Content>
<asp:Content ContentPlaceHolderID="ScriptContent" runat="server">
<script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="adminlink">
        <%= Html.ActionLink(CoreLanguage.all_users, "Index")%></h2>
    <h2>
        <%=CoreLanguage.all_roles%></h2>
    <%=Html.MytripPager(Model.DefaultCount, Model.Total,"...")%>
    <table>
        <tr>
            <th class="oneinput"><% Html.EnableClientValidation(); %>
                <% using (Html.BeginForm())
                   { %><%= Html.ValidationMessageFor(m=>m.RoleName)%><br />
                <%= Html.TextBoxFor(m=>m.RoleName) %>                
                <%=Html.MytripInput("submit", CoreLanguage.add_role)%>
                <% } %>
            </th>
        </tr>
    </table>
    <table>
        <tr>
            <th class="editdelite">
            </th>
            <th>
                <%= Html.ActionLink(CoreLanguage.RoleName, "IndexRole", new { pageIndex = 1, pageSize = Model.DefaultCount, sorting = Html.MytripSort("RoleName") })%>
            </th>
            <th>
                <%= Html.ActionLink(CoreLanguage.UserCount, "IndexRole", new { pageIndex = 1, pageSize = Model.DefaultCount, sorting = Html.MytripSort("UserCount") })%>
            </th>
        </tr>
        <% =Html.RoleIndex(Model.Roles,Model.RolesXml,CoreLanguage.are_you_sure) %>
    </table>
    <%=Html.MytripPager(Model.DefaultCount, Model.Total,"...")%>
</asp:Content>
