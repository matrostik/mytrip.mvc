<%@ Page Title="" Language="C#" MasterPageFile="~/Views/TextAreaFile/PopUp.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Mvc.Models.IndexFilesModels>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
<%//************************************************************ 
  // Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich
  // To learn more about Mytrip.Mvc.Entyty visit 
  // http://starterkitmytripmvc.codeplex.com/
  // mytripmvc@gmail.com
  // license: Microsoft Public License (Ms-PL) 
  // *********************************************************** %>
    <%=Html.PageTitle(CoreLanguage.file_manager,"/") %>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContent" runat="server">
    <script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-1.3.2.js" type="text/javascript"></script>
    <% int id = 1;
       foreach (System.IO.FileInfo x in Model.Files)
       { id++; } %>
       <%=Html.MytripAddTextAreaScript(id,Model.Param)%>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
 <div style="position: relative; float: right; width: 450px; text-align: right;">
        <% Html.EnableClientValidation(); %>
            <% using (Html.BeginForm())
               {%>
            <div>
            <%= Html.ValidationMessageFor(m => m.Name) %><br />
                <%=CoreLanguage.create_new_folder%>
                <%= Html.TextBoxFor(m=>m.Name)%>
                <%=Html.MytripInput("submit", CoreLanguage.create)%>
                
            </div>
            <% } %>
            <% using (Html.BeginForm("AddFile", "TextAreaFile", new { id = Model.Directory,id2=Model.Param}, FormMethod.Post,
                                new {enctype = "multipart/form-data" }))
               {%>
            <div >
                <%=Html.ValidationMessageFor(m => m.Error)%><br />
                <%=CoreLanguage.upload_file%>
                <%=Html.MytripInput("file",string.Empty,"id3",string.Empty) %>
                <%=Html.MytripInput("submit", CoreLanguage.upload)%>
                
            </div>
            <% } %>
    </div>
    <h2>
        <%=CoreLanguage.file_manager%></h2>
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
            <th>
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
                <%=Html.MytripImageLink(Url.Action("Rename", "TextAreaFile", new { id=directory,id2=Model.Param, id3 = "folder" }),
                                                                     "/Content/images/rename.png", CoreLanguage.rename, 20, 0, 0)%>
                <%=Html.MytripImageLink(Url.Action("Delete", "TextAreaFile", new { id=directory, id2 = Model.Param, id3 = "folder" }),
                                                                     "/Content/images/delete.png", "delete", 20, 0, 0, CoreLanguage.are_you_sure)%>
            </td>
            <td>
                <%=Html.MytripImageLink(Url.Action("Index", "TextAreaFile", new { id=directory, id2 = Model.Param }),
                                            "/Content/files/Stuffed_Folder.png", x.Name, 0, 32, 0)%>
            </td>
            <td>
                <%=Html.MytripActionLink(Url.Action("Index", "TextAreaFile", new {id= directory, id2 = Model.Param }), x.Name)%>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <%} int id = 1;
           foreach (System.IO.FileInfo x in Model.Files)
           {
               string directory = Model.Directory + "()" + x.Name;%>
        <tr>
            <td>
             <%=Html.MytripAddTextArea(id,directory.Replace("()", "/"),x.Name,x.Extension)%>
                <%id++; %>
                <%=Html.MytripImageLink(Url.Action("Rename", "TextAreaFile", new {id= directory, id2 = Model.Param, id3 = "file" }),
                                                                     "/Content/images/rename.png", CoreLanguage.rename, 20, 0, 0)%>
                <%=Html.MytripImageLink(Url.Action("Delete", "TextAreaFile", new {id= directory, id2 = Model.Param, id3 = "file" }),
                                                                     "/Content/images/delete.png", "delete", 20, 0, 0, CoreLanguage.are_you_sure)%>
            </td>
            <td>
                <%=Html.MytripImage(Html.MytripMim(directory,x.Name,x.Extension), x.Name, 0, 32, 0)%>
            </td>
            <td>
               
                <%=x.Name%>
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
