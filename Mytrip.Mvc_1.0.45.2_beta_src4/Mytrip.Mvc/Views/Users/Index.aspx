<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Mvc.Models.IndexUsersModel>" %>

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
        <%= Html.ActionLink(CoreLanguage.all_roles, "IndexRole", new { id = 1, id2 = 10, id3 = "RoleName" })%></h2>
    <h2 class="title">
        <%=CoreLanguage.all_users%></h2>
<div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content">

    <%=Html.MytripPager(Model.DefaultCount,Model.Total,"...")%>
    </div><div><div class="contentbottomright"></div><div class="contentbottomleft"></div><div class="contentbottomcon"></div></div>
     <div class="acfooter"></div>
     <div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content">
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
                <%= Html.ActionLink(CoreLanguage.UserName, "Index", new { id = 1, id2 = Model.DefaultCount, id3 = Html.MytripSort("UserName") })%>
            </th>
            <th>
                <%= Html.ActionLink(CoreLanguage.Email, "Index", new { id = 1, id2 = Model.DefaultCount, id3 = Html.MytripSort("Email") })%>
            </th>
            <th>
                <%= Html.ActionLink(CoreLanguage.LastActivityDate, "Index", new { id = 1, id2 = Model.DefaultCount, id3 = Html.MytripSort("LastActivityDate") })%>
            </th>
            <th>
                <%= Html.ActionLink(CoreLanguage.CreationDate, "Index", new { id = 1, id2 = Model.DefaultCount, id3 = Html.MytripSort("CreationDate") })%>
            </th>
            <th>
                <%= Html.ActionLink(CoreLanguage.LastLoginDate, "Index", new { id = 1, id2 = Model.DefaultCount, id3 = Html.MytripSort("LastLoginDate") })%>
            </th>
            <th>
                <%= Html.ActionLink(CoreLanguage.LastPasswordChangedDate, "Index", new { id = 1, id2 = Model.DefaultCount, id3 = Html.MytripSort("LastPasswordChangedDate") })%>
            </th>
            <th>
                <%= Html.ActionLink(CoreLanguage.UserIP, "Index", new { id = 1, id2 = Model.DefaultCount, id3 = Html.MytripSort("UserIP") })%>
            </th>
            <th>
                <%=CoreLanguage.IsApproved%>
            </th>
        </tr>
        <%=Html.UserIndex(Model.Users,CoreLanguage.are_you_sure) %>
    </table>
      </div><div><div class="contentbottomright"></div><div class="contentbottomleft"></div><div class="contentbottomcon"></div></div>
     <div class="acfooter"></div>
     <div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content">
   
    <%=Html.MytripPager(Model.DefaultCount, Model.Total, "...")%>
      </div><div><div class="contentbottomright"></div><div class="contentbottomleft"></div><div class="contentbottomcon"></div></div>

</asp:Content>
