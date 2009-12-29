<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title><%=ViewData["model_domain"] %>/<%= Html.Encode(Page.User.Identity.Name) %></title>
    <link rel="icon" href="/mt.ico" type="image/x-icon" />
<link rel="shortcut icon" href="/mt.ico" type="image/x-icon" />
    <link href="../../Content/Main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <div>
     <!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright ©  2009 Стюхин Олег Анатольевич   */-->
    
   
        <%= Html.ValidationSummary("Ошибка!") %>
       
    <img src="/content/files/foldergreen.png" style="height: 20px; position: relative;
        float: left; margin-right: 5px;" alt="" />
    
    <%string a1 = (string)ViewData["dir"];
      string a2 = a1.Replace("()", "/");
      string a7 = a1.Replace("()", "/");
      int g1 = 0;
      int gh1 = 0;
      for (; ; )
      {
          int a5 = a2.IndexOf("/", 2);
          if (a5 != -1)
          {
              gh1++;
              g1 = g1 + a5;
              string a6 = a2.Remove(a5);
              a2 = a2.Remove(0, a5);
              string g2 = a7.Remove(g1);
              g2 = g2.Replace("/", "()");
              if (gh1 > 1)
              {%>
    <a href="<%=Url.Action("B", "G", new{a=g2})%>">
        <%= a6%></a>
    <%}
          }
          else { break; }
      } %>
    <%string d1 = (string)ViewData["dir"];
      string d4 = d1.Replace("()", "/");
      int d2 = d4.LastIndexOf("/");
      string d3 = d4.Remove(0, d2);
    %>
    <a href="<%=Url.Action("B", "G", new{a=d1})%>">
        <%= d3 %></a>
         <table>
 <tr class="file_file">


   <% using (Html.BeginForm("H", "G", new {a=a1,b="folder" }))
      {%>
    <td>Создать папку:
            <%= Html.TextBox("b")%>
       
            <input type="submit" value="Создать"  />
             </td>
    <% } %>
  <%  using (Html.BeginForm("B", "G", FormMethod.Post,
new { enctype = "multipart/form-data" }))
   {%>
   
        <td>    
              Загрузить файл:  
                <input type="file" name="b"  style="width: 300px; background-color: #FFFFFF;" />
       
              <input type="submit" value="Загрузить"  /></td>
    <% } %>

 
 
 </tr>
 </table>
    <table >
        <tr class="file_file">
            <td>
            </td>
            <td>
            </td>
            <td>
                дата изменения
            </td>
            <td>
                размер
            </td>
        </tr>
        <% string fold_dir = (string)ViewData["dir"];
           int id_hidden = 0;
           foreach (DirectoryInfo x in (IEnumerable)ViewData["folder"])
           {
               string fd = fold_dir + "()" + x.Name;
        %>
        <tr>
            <td>
                <img src="/content/files/Stuffed_Folder.png" style="height: 32px;" alt="<%=x.Name %>" />
            </td>
            <td>  <div class="edit_content">            

                    <a href="<%=Url.Action("J", "G", new{a=fd})%>"
                        onclick="return confirm ('Вы уверены что хотите удалить папку?');">D</a>
                </div>
                <a href="<%=Url.Action("B", "G", new{a=fd})%>">
                    <%=x.Name %></a>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <%}%>
        <%string dir = "";
          foreach (FileInfo x in (IEnumerable)ViewData["file"])
          {
              id_hidden++;
              string rtyu = id_hidden + " id_hidden";
              string abc = fold_dir + "/" + x.Name;
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
        <tr>
            <td>
                <img src="<%= dir %>" style="height: 32px;" alt="<%=x.Name %>" />
            </td>
            <td>
                <%string ef4 = abc.Replace("/", "()"); %>
                <div class="edit_content">            

                    <a href="<%=Url.Action("C", "G", new{a=ef4})%>"
                        onclick="return confirm ('Вы уверены что хотите удалить файл?');">D</a>
                </div>
              

                <a href="<%= abc %>">
                    <%= abc %></a>
            </td>
            <td class="file_file">
                <%=x.LastWriteTime %>
            </td>
            <td class="file_file">
                <%=x.Length %>
                байт
            </td>
        </tr>
        <%}%>
    </table>
    </div>
</body>
</html>
