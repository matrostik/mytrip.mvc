<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Mvc.Models.DetailsUserModel>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
<%  //************************************************************ 
    // Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich
    // To learn more about Mytrip.Mvc.Entyty visit 
    // http://starterkitmytripmvc.codeplex.com/
    // mytripmvc@gmail.com
    // license: Microsoft Public License (Ms-PL) 
    // *********************************************************** %>
    <%= Html.PageTitle(Model.UserName,"/") %>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContent" runat="server">
<script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="title">
        <%= Html.Encode(Model.UserName) %></h2>
        <div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content">

   
    <table>
        <tr>
            <th class="oneinput">
                <h2 class="white">
                    <%=CoreLanguage.account_information%></h2>
            </th>
            <th>
            </th>
        </tr>
        <tr>
            <th class="oneinput">
                <%=CoreLanguage.Email%>
            </th>
            <td>
                <%= Html.Encode(Model.Email) %>
            </td>
        </tr>
        <tr>
            <th class="oneinput">
                <%=CoreLanguage.LastActivityDate%>
            </th>
            <td>
                <%= Html.Encode(String.Format("{0:g}", Model.LastActivityDate)) %>
            </td>
        </tr>
        <tr>
            <th class="oneinput">
                <%=CoreLanguage.CreationDate%>
            </th>
            <td>
                <%= Html.Encode(String.Format("{0:g}", Model.CreationDate)) %>
            </td>
        </tr>
        <tr>
            <th class="oneinput">
                <%=CoreLanguage.LastLoginDate%>
            </th>
            <td>
                <%= Html.Encode(String.Format("{0:g}", Model.LastLoginDate)) %>
            </td>
        </tr>
        <tr>
            <th class="oneinput">
                <%=CoreLanguage.LastPasswordChangedDate%>
            </th>
            <td>
                <%= Html.Encode(String.Format("{0:g}", Model.LastPasswordChangedDate)) %>
            </td>
        </tr>
        <tr>
            <th class="oneinput">
                <%=CoreLanguage.UserIP%>
            </th>
            <td>
                <%= Html.Encode(Model.UserIP) %>
            </td>
        </tr>
        <tr>
            <th class="oneinput">
                <%=CoreLanguage.IsApproved%>
            </th>
            <td>
                <%if (Model.IsApproved)
                  { %>
                <%=CoreLanguage.user_is_approved%>
                <%=Html.MytripImage("/Content/images/approved.png", CoreLanguage.user_is_approved, 15, 0, 0)%>
                [
                <%=Html.MytripImageLink(Url.Action("IsApproved", new { id = Model.UserName }),
                                                          "/Content/images/noapproved.png", CoreLanguage.user_is_not_approved, 15, 0, 0, CoreLanguage.are_you_sure)%>
                ]
                <%}
                  else
                  {%>
                <%=CoreLanguage.user_is_not_approved%>
                <%=Html.MytripImage("/Content/images/noapproved.png", CoreLanguage.user_is_not_approved, 15, 0, 0)%>
                [
                <%=Html.MytripImageLink(Url.Action("IsApproved", new { id = Model.UserName }),
                                                          "/Content/images/approved.png", CoreLanguage.user_is_approved, 15, 0, 0, CoreLanguage.are_you_sure)%>
                ]
                <% } %>
            </td>
        </tr>
        <tr>
            <th class="oneinput">
                <h2 class="white">
                    <%=CoreLanguage.role_information%></h2>
            </th><% Html.EnableClientValidation(); %>
            <% using (Html.BeginForm())
               {%>
            <th class="addrole"><%= Html.ValidationMessageFor(m => m.RoleName) %><br />
                <%= Html.DropDownListFor(m => m.RoleName, Model.AllRoles, CoreLanguage.choose_role)%>
                <%=Html.MytripInput("submit", CoreLanguage.add_user_to_role)%>
            </th>
            <%} %>
        </tr>
        <%foreach (string item in Model.RolesInUser)
          {  %><tr>
              <th class="oneinput">
                  <%=item %>
              </th>
              <td>
                  <%=CoreLanguage.user_in_role%>
                  <%=Html.MytripImage("/Content/images/approved.png", CoreLanguage.user_in_role, 15, 0, 0)%>
                  [
                  <%=Html.MytripImageLink(Url.Action("IsUserInRole", new { id = Model.UserName, id2 = item }),
                                        "/Content/images/noapproved.png", "delete", 15, 0, 0, CoreLanguage.are_you_sure)%>
                  ]
              </td>
          </tr>
        <%} %>
    </table>
    </div><div ><div class="contentbottomright"></div><div class="contentbottomleft"></div><div class="contentbottomcon"></div></div>
      <div class="acfooter"></div> 
       <div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content">

   

        <%=Html.ActionLink(CoreLanguage.back_to_list, "Index")%>
    
      </div><div><div class="contentbottomright"></div><div class="contentbottomleft"></div><div class="contentbottomcon"></div></div>

</asp:Content>
