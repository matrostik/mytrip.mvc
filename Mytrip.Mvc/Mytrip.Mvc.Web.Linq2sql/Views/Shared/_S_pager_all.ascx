<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%int abc = (int)ViewData["content_count"];//общее количество статей
  string url = ViewData["content_url"].ToString();//обратный url
  int bcd = (int)ViewData["content_int"];//количество на странице
  int das = (int)ViewData["content_page"];//текущая страница
  string path = ViewData["content_path"].ToString();//обратный path
%>
<table>
    <tr>
        <td>
            <div class="pager">
                <small><%=Mytrip_Mvc_Language_1.pager_pages%></small></div>
        </td>
        <td>
            <div class="pagercount">
                <small><%=Mytrip_Mvc_Language_1.pager_count%></small></div>
        </td>
    </tr>
    <tr>
        <td>
            <div class="pager">
              </div>
            <%= Html.PagerAll(url, das, bcd, path, abc) %>  
        </td>
        <td>
            <div class="pagercount">
              <%= Html.PagerCountAll(url, bcd, path, abc) %> 
            </div>
        </td>
    </tr>
</table>
