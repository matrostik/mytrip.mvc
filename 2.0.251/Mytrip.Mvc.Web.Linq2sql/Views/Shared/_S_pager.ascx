<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright ©  2009 Стюхин Олег Анатольевич oleg@stuh.in 
Эта программа является свободным программным продуктом; вы вправе распространять и/или изменять 
ее в соответствии с условиями Генеральной Общественной Лицензии GNU в том виде, как она была опубликована 
Фондом Свободного ПО; либо версии 2 Лицензии либо (по вашему желанию) любой более поздней версии.
Программа распространяется в надежде, что она будет полезной, но БЕЗ КАКИХ БЫ ТО НИ БЫЛО ГАРАНТИЙНЫХ ОБЯЗАТЕЛЬСТВ;
даже без косвенных гарантийных обязательств, связанных с ПОТРЕБИТЕЛЬСКИМИ СВОЙСТВАМИ и ПРИГОДНОСТЬЮ ДЛЯ ОПРЕДЕЛЕННЫХ ЦЕЛЕЙ. 
Для подробностей смотрите Генеральную Общественную Лицензию GNU.
Вы должны были получить копию Генеральной Общественной Лицензии GNU вместе с этой программой; 
если это не так, напишите в Фонд Свободного ПО 
(Free Software Foundation, Inc., 675 Mass Ave, Cambridge, MA 02139, USA).  */-->
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%int abc = (int)ViewData["content_count"];//общее количество статей
  string url = ViewData["helper_pager_url"].ToString();//обратный url
  int bcd = (int)ViewData["helper_pager_int"];//количество на странице
  int cda = (int)ViewData["helper_pager_cat"];//номер рубрики
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
            <%= Html.Pager(url, cda, das, bcd, path, abc) %>  
        </td>
        <td>
            <div class="pagercount">
              <%= Html.PagerCount(url, cda, bcd, path, abc) %> 
            </div>
        </td>
    </tr>
</table>
