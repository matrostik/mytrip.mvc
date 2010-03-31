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
        <% foreach (var item in Model.Users){ %>
        <tr>
            <td>
                <%=Html.MytripImageLink(Url.Action("Details", new { userName = item.UserName }),
                                                                 "/Content/images/Users.png", item.UserName, 20, 0, 0)%>
                <%=Html.MytripImageLink(Url.Action("Delete", new { userName = item.UserName }),
                                                                     "/Content/images/delete.png", "delete", 20, 0, 0, CoreLanguage.are_you_sure)%>
            </td>
            <td>
                <b>
                    <%= Html.Encode(item.UserName) %></b>
                <%int rolecount = item.mytrip_Roles.Count();
                  if (rolecount > 0)
                  { %><br />
                role:
                <% int _rolecount = 0;
                   foreach (mytrip_Roles _item in item.mytrip_Roles.ToList())
                   {  %><%=_item.RoleName%><%_rolecount++;
                                            if (_rolecount == rolecount)
                                            {%>.<% }
                    else
                    {%>,
                <% } %>
                <%}
                  } %>
            </td>
            <td>
                <%= Html.Encode(item.mytrip_Membership.Email) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:d}", item.LastActivityDate)) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:d}", item.mytrip_Membership.CreationDate)) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:d}", item.mytrip_Membership.LastLoginDate)) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:d}", item.mytrip_Membership.LastPasswordChangedDate)) %>
            </td>
            <td>
                <%= Html.Encode(item.mytrip_Membership.UserIP) %>
            </td>
            <td>
                <%= Html.CheckBox("IsApproved", item.mytrip_Membership.IsApproved, new { disabled="true"})%>
            </td>
        </tr>
        <% } %>
    </table>
    <%=Html.MytripPager(Model.DefaultCount, Model.Total, "...")%>
</asp:Content>
