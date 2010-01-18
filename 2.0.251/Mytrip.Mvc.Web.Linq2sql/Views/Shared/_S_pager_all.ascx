<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%int abc = (int)ViewData["content_count"];//общее количество статей
  string url = ViewData["helper_pager_url"].ToString();//обратный url
  int bcd = (int)ViewData["helper_pager_int"];//количество на странице
  int das = (int)ViewData["helper_pager_page"];//текущая страница
  string path = ViewData["helper_pager_path"].ToString();//обратный path
%>
<table>
    <tr>
        <td>
            <div class="pager">
                <small><%=Mytrip_Mvc_Language.pager_pages%></small></div>
        </td>
        <td>
            <div class="pagercount">
                <small><%=Mytrip_Mvc_Language.pager_count%></small></div>
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
