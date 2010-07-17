<%@ Page Title="" Language="C#" MasterPageFile="~/Views/TextAreaFile/PopUp.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Mvc.Models.IndexFilesModels>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Smile
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

   <% int id = 1;
           foreach (System.IO.FileInfo x in Model.Files)
           {
               string directory = "()Content()smiles()" + x.Name;%>
        
             <%=Html.MytripAddTextArea(id,directory.Replace("()", "/"),x.Name)%>
                <%id++; %>
                
                <%} %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
<script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-1.3.2.js" type="text/javascript"></script>
    <% int id = 1;
       foreach (System.IO.FileInfo x in Model.Files)
       { id++; } %>
       <%=Html.MytripAddTextAreaScript(id, Model.Directory)%>
</asp:Content>
