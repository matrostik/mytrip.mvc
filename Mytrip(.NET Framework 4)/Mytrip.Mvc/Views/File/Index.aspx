<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Core.Models.IndexFilesModels>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
<%//************************************************************ 
  // Copyright � 2009 - 2010 Oleg Stuhin & Rostislav Haitovich
  // To learn more about Mytrip.Mvc.Entyty visit 
  // http://starterkitmytripmvc.codeplex.com/
  // mytripmvc@gmail.com
  // license: Microsoft Public License (Ms-PL) 
  // *********************************************************** %>
    <%=Html.PageTitle(CoreLanguage.file_manager, "/")%>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContent" runat="server">
    <script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%=CoreLanguage.file_manager%></h2>
    <table>
        <tr>
            <th class="rootfolder">
                <%=Html.MytripImageLink(Url.Action("Index",new { directory= string.Empty }),
                "/Content/files/Stuffed_Folder.png", Membership.ApplicationName,0,32,0) %>
            </th>
            <th>
                <%= Html.ActionLink(Membership.ApplicationName, "Index", new { directory = string.Empty })%>
                <%=Html.MytripDirectory(Model.Directory)%>
            </th>
        </tr>
    </table>
    <table>
        <tr><% Html.EnableClientValidation(); %>
            <% using (Html.BeginForm())
               {%>
            <th class="dableinput">
            <%= Html.ValidationMessageFor(m => m.Name) %><br />
                <%=CoreLanguage.create_new_folder%>
                <%= Html.TextBoxFor(m=>m.Name)%>
                <%=Html.MytripInput("submit", CoreLanguage.create)%>
                
            </th>
            <% } %>
            <% using (Html.BeginForm("AddFile", "File", new { directory = Model.Directory}, FormMethod.Post,
                                new {enctype = "multipart/form-data" }))
               {%>
            <th class="oneinput">
                <%=Html.ValidationMessageFor(m => m.Error)%><br />
                <%=CoreLanguage.upload_file%>
                <%=Html.MytripInput("file",string.Empty,"file",string.Empty) %>
                <%=Html.MytripInput("submit", CoreLanguage.upload)%>
                
            </th>
            <% } %>
        </tr>
    </table>
    <table>
        <tr>
            <th class="editdelite">
            </th>
            <th class="folderfile">
            </th>
            <th>
            </th>
            <th class="filechangedate">
                <%=CoreLanguage.change_date%>
            </th>
            <th class="filesize">
                <%=CoreLanguage.size%>
            </th>
        </tr>
        <% foreach (System.IO.DirectoryInfo x in Model.Folders)
           {
               string directory = Model.Directory + "()" + x.Name;
        %>
        <tr>
            <td>
                <%=Html.MytripImageLink(Url.Action("Rename", "File", new { directory, param = "folder" }),
                                                                     "/Content/images/rename.png", CoreLanguage.rename, 20, 0, 0)%>
                <%=Html.MytripImageLink(Url.Action("Delete", "File", new { directory, param = "folder" }),
                                                                     "/Content/images/delete.png", "delete", 20, 0, 0, CoreLanguage.are_you_sure)%>
            </td>
            <td>
                <%=Html.MytripImageLink(Url.Action("Index", "File", new { directory }),
                                            "/Content/files/Stuffed_Folder.png", x.Name, 0, 32, 0)%>
            </td>
            <td>
                <%=Html.MytripActionLink(Url.Action("Index", "File", new { directory}), x.Name)%>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <%}
           foreach (System.IO.FileInfo x in Model.Files)
           {
               string directory = Model.Directory + "()" + x.Name;%>
        <tr>
            <td>
                <%=Html.MytripImageLink(Url.Action("Rename", "File", new { directory, param = "file" }),
                                                                     "/Content/images/rename.png", CoreLanguage.rename, 20, 0, 0)%>
                <%=Html.MytripImageLink(Url.Action("Delete", "File", new { directory, param = "file" }),
                                                                     "/Content/images/delete.png", "delete", 20, 0, 0, CoreLanguage.are_you_sure)%>
            </td>
            <td>
                <%=Html.FileImageLink(x.Name, 0, 32, 0,directory,x.Name,x.Extension)%>
            </td>
            <td>
                <%=Html.FileActionLink(directory, x.Name, x.Extension)%>
            </td>
            <td>
                <%=x.LastWriteTime %>
            </td>
            <td>
                <%=x.Length %>
                <%=CoreLanguage.bytes%>
            </td>
        </tr>
        <%}%>
    </table>
</asp:Content>
