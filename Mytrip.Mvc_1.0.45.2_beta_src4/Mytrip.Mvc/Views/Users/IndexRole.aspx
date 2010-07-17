<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Mvc.Models.IndexRolesModel>" %>

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
    <h2 class="title">
        <%=CoreLanguage.all_roles%></h2>
        <div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content">

   
    <%=Html.MytripPager(Model.DefaultCount, Model.Total,"...")%>
      </div><div><div class="contentbottomright"></div><div class="contentbottomleft"></div><div class="contentbottomcon"></div></div>
     <div class="acfooter"></div>
     <div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content">
   
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
                <%= Html.ActionLink(CoreLanguage.RoleName, "IndexRole", new { id = 1, id2 = Model.DefaultCount, id3 = Html.MytripSort("RoleName") })%>
            </th>
            <th>
                <%= Html.ActionLink(CoreLanguage.UserCount, "IndexRole", new { id = 1, id2 = Model.DefaultCount, id3 = Html.MytripSort("UserCount") })%>
            </th>
        </tr>
        <% =Html.RoleIndex(Model.Roles,CoreLanguage.are_you_sure) %>
    </table>
      </div><div><div class="contentbottomright"></div><div class="contentbottomleft"></div><div class="contentbottomcon"></div></div>
     <div class="acfooter"></div>
     <div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content">
   
    <%=Html.MytripPager(Model.DefaultCount, Model.Total,"...")%>
       </div><div ><div class="contentbottomright"></div><div class="contentbottomleft"></div><div class="contentbottomcon"></div></div>

</asp:Content>
