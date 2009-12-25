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
  string url = ViewData["content_url"].ToString();//обратный url
  int bcd = (int)ViewData["content_int"];//количество на странице
  int cda = (int)ViewData["content_cat"];//номер рубрики
  int das = (int)ViewData["content_page"];//текущая страница
  string path = ViewData["content_path"].ToString();//обратный path
  int page_count = (int)Math.Ceiling((double)abc / bcd);//количество страниц
%>
<table>
    <tr>
        <td>
            <div class="pager">
                <small>страницы</small></div>
        </td>
        <td>
            <div class="pagercount">
                <small>количество на странице</small></div>
        </td>
    </tr>
    <tr>
        <td>
            <div class="pager">
                <%
if (das == 1)
{%>
                <a style=" border: 1px solid #E5C365;text-decoration: none;
    color: #2E2633;
    background-image: url(/content/images/4.png);">1</a>
                <%}
else
{ %>
                <a href="<%= Url.Action(url, new { a = cda,b=1,c=bcd,d=path })%>">1</a>
                <%}
int page;
int q = das;
if (das == 1)
    q = 2;
if (das == 3)
    q = das - 1;
if (das > 3)
    q = das - 2;
if (das <= 4)
{
    //начало со второй                                                
    for (page = q; (int)Math.Ceiling((double)abc / bcd) >= page; page = page + 1)
    {
        if (page == das)
        {%>
                <a style=" border: 1px solid #E5C365;text-decoration: none;
    color: #2E2633;
    background-image: url(/content/images/4.png);">
                    <%= page%></a>
                <%}
        else
        {%>
                <a href="<%= Url.Action(url, new { a = cda,b=page,c=bcd,d=path })%>">
                    <%= page%></a>
                <%} if (page == page_count - 1)
            break;
        if (page == das + 3)
            break;
    } if (page <= das + 3)
    {
        if (page <= page_count - 3)
        { %>...<%}
    }
}
else
{
    if (das > 5)
    {%>...<%}
    //начало с пятой                                                
    for (page = q - 1; (int)Math.Ceiling((double)abc / bcd) >= page; page = page + 1)
    {
        if (page == das)
        {%>
                <a style=" border: 1px solid #E5C365;text-decoration: none;
    color: #2E2633;
    background-image: url(/content/images/4.png);">
                    <%= page%></a>
                <%}
        else
        {%>
                <a href="<%= Url.Action(url, new { a = cda,b=page,c=bcd,d=path })%>">
                    <%= page%></a>
                <%} if (page == page_count - 1)
            break;
        if (page == das + 3)
            break;
    } if (page <= das + 3)
    {
        if (page <= page_count - 3)
        { %>...<%}
    }
}
if (page_count > 3)
{
    if (das == page_count)
    {%>
                <a style=" border: 1px solid #E5C365;text-decoration: none;
    color: #2E2633;
    background-image: url(/content/images/4.png);"><%=page_count%></a>
                <%}
    else
    { %>
                <a href="<%= Url.Action(url, new { a = cda,b=page_count,c=bcd,d=path })%>"><%=page_count%></a>
                <%}
}%></div>
        </td>
        <td>
            <div class="pagercount">
                <% if (bcd == 5)
                   {%>
                <a style=" border: 1px solid #E5C365;text-decoration: none;
    color: #2E2633;
    background-image: url(/content/images/4.png);">5</a><% }
                   else
                   { %>
                <a href="<%= Url.Action(url, new { a = cda,b=1,c=5,d=path })%>">5</a><%}

                   if (bcd == 10)
                   {%>
                <a style=" border: 1px solid #E5C365;text-decoration: none;
    color: #2E2633;
    background-image: url(/content/images/4.png);">10</a><% }
                   else
                   { %>
                <a href="<%= Url.Action(url, new { a = cda,b=1,c=10,d=path })%>">10</a><%}
                   if (abc > 10)
                   {
                       if (bcd == 25)
                       {%>
                <a style=" border: 1px solid #E5C365;text-decoration: none;
    color: #2E2633;
    background-image: url(/content/images/4.png);">25</a><% }
                       else
                       { %>
                <a href="<%= Url.Action(url, new { a = cda,b=1,c=25,d=path })%>">25</a><%}
                   } if (abc > 25)
                   {
                       if (bcd == 50)
                       {%>
                <a style=" border: 1px solid #E5C365;text-decoration: none;
    color: #2E2633;
    background-image: url(/content/images/4.png);">50</a><% }
                       else
                       { %>
                <a href="<%= Url.Action(url, new { a = cda,b=1,c=50,d=path })%>">50</a><%}
                   } %>
            </div>
        </td>
    </tr>
</table>
