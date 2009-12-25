<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/1_column.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=ViewData["model_domain"]%>/Папки
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright ©  2009 Стюхин Олег Анатольевич   */-->

  <%= Html.ValidationSummary("Ошибка!") %>
  
   
 
   
   
    <img src="/content/files/foldergreen.png" 
    style="height: 20px; position: relative; float: left; margin-right: 5px;" alt="<%=ViewData["model_domain"] %>" />

<a href="<%=Url.Action("F", "F")%>"> <%=ViewData["model_domain"]%></a>
 <table>
 <tr class="file_file">


   <% using (Html.BeginForm("E", "F"))
   {%>
    <td>Создать папку:
            <%= Html.TextBox("a")%>
       
            <input type="submit" value="Создать" class="input_boottom" />
             </td>
    <% } %>
  <% using (Html.BeginForm("F", "F", FormMethod.Post,
new { enctype = "multipart/form-data" }))
   {%>
   
        <td>    
              Загрузить файл:  
                <input type="file" name="a" class="input_boottom" style="width: 300px" />
       
              <input type="submit" value="Загрузить" class="input_boottom" /></td>
    <% } %>

 
 
 </tr>
 </table>
  
     <table>
     <tr class="file_file"><td>
     
     </td>
     <td></td>
     <td>дата изменения</td>
     <td>размер</td>
     
     </tr>
<% foreach (DirectoryInfo x in (IEnumerable)ViewData["folder"])
   {
       string abf = "/" + x.Name;
       abf = abf.Replace("/", "()");%>
   <tr><td>
   <img src="/content/files/Stuffed_Folder.png" 
    style=" height: 32px;" alt="<%=x.Name %>" />
</td><td>
 <div class="edit_content">

<a href="<%=Url.Action("G", "F", new{a=abf})%>" onclick = "return confirm ('Вы уверены что хотите удалить папку?');"><img src="/content/images/delete.png" alt="удалить" style="border-width: 0px;" /></a>     
</div>
<a href="<%=Url.Action("B", "F", new{a=abf})%>"> <%=x.Name %></a>
</td>
<td></td>
<td></td>
</tr>
<%}%>
<%string dir = "";
    foreach (FileInfo x in (IEnumerable)ViewData["file"])
    {
        string abc = "/" + x.Name;
        abc = abc.Replace("()", "/");
        dir = "/content/files/" + x.Extension + ".png";
        if (x.Extension == ".ico")
        { dir = abc; }
        if (x.Extension == ".png")
        { dir = abc; }
        if (x.Extension == ".jpg")
        { dir = abc; }
        if (x.Extension == ".gif")
        { dir = abc; }%>
  <tr> <td >   
    <img src="<%= dir %>" 
    style=" height: 32px; " alt="<%=x.Name %>" /></td>
      <td >
       <%string ef4 = abc.Replace("/", "()"); %>
            <div class="edit_content">

<a href="<%=Url.Action("D", "F", new{a=ef4})%>" onclick = "return confirm ('Вы уверены что хотите удалить файл?');"><img src="/content/images/delete.png" alt="удалить" style="border-width: 0px;" /></a>     
</div> 
<a  href="<%= abc %>" > <%= abc %></a> 
</td>
    
   <td class="file_file"> 
  <%=x.LastWriteTime %> 

</td>
<td class="file_file">     
  <%=x.Length %> байт
  </td>
</tr>
 
  
<%}%>

</table>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
