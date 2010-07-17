<%@ Page Title="" Language="C#" MasterPageFile="~/Views/TextAreaFile/PopUp.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Mvc.Models.RenameModels>" %>

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
    <h2>
        <%=CoreLanguage.rename_file_or_folder%></h2>
    <table>
        <tr>
            <th class="rootfolder">
                <%=Html.MytripImage("/Content/files/Stuffed_Folder.png", Membership.ApplicationName,0,32,0) %>
            </th>
            <th>
                <%= Html.MytripDirectory(Model.Directory, "TextAreaFile",Model.Param)%>
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
    <%=Html.MytripActionLink(Url.Action("Index", "TextAreaFile", new { id = Model.Back, id2 = Model.Param }), CoreLanguage.back_to_list)%>
</asp:Content>
