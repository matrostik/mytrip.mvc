<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Mvc.Models.RenameModels>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
<%  //************************************************************ 
    // Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich
    // To learn more about Mytrip.Mvc.Entyty visit 
    // http://starterkitmytripmvc.codeplex.com/
    // mytripmvc@gmail.com
    // license: Microsoft Public License (Ms-PL) 
    // *********************************************************** %>
    <%=Html.PageTitle(CoreLanguage.rename_file_or_folder, "/")%>
</asp:Content>
<asp:Content ContentPlaceHolderID="ScriptContent" runat="server">
    <script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="title">
        <%=CoreLanguage.rename_file_or_folder%></h2>
        <div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content">

    <table>
        <tr>
            <th class="rootfolder">
                <%=Html.MytripImageLink(Url.Action("Index",new{id=string.Empty}),
                "/Content/files/Stuffed_Folder.png", Membership.ApplicationName,0,32,0) %>
            </th>
            <th>
                <%= Html.ActionLink(Membership.ApplicationName, "Index", new { id = string.Empty })%>
                <%=Html.MytripDirectory(Model.Directory)%>
            </th>
        </tr>
    </table>
    <table>
        <tr>
            <% Html.EnableClientValidation(); %>
            <% using (Html.BeginForm())
               {%>
            <td>
                <p>
                    <%=CoreLanguage.choose_new_name%></p>
                <p>
                    <%= Html.TextBoxFor(m=>m.Name)%>
                    <%= Html.ValidationMessageFor(m => m.Name) %>
                </p>
                <p>
                    <%=Html.MytripInput("submit", CoreLanguage.rename)%></p>
            </td>
            <% } %>
        </tr>
    </table>
     </div><div ><div class="contentbottomright"></div><div class="contentbottomleft"></div><div class="contentbottomcon"></div></div>
       <div class="acfooter"></div> 
       <div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content">

   
    <%=Html.MytripActionLink(Url.Action("Index", "File", new { directory = Model.Back }), CoreLanguage.back_to_list)%>
       </div><div><div class="contentbottomright"></div><div class="contentbottomleft"></div><div class="contentbottomcon"></div></div>

</asp:Content>
