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

 <% if (HttpContext.Current.User.Identity.IsAuthenticated)
    { %>     
  
  <%if ((bool)ViewData["abstract_model_blog"] == true)
    {%> <div class="right_title"> 
    &nbsp;&nbsp;<%=Mytrip_Mvc_Language.right_menu_user%>&nbsp;<%= Html.Encode(Page.User.Identity.Name)%></div><div class="right_content"><%
string date = "date";
int abcd = 0;
foreach (mt_artycle_comment x in (IEnumerable<mt_artycle_comment>)ViewData["helper_comment_user"])
{
    if (date != x.AddedDate.DayOfYear.ToString())
    {
        abcd++;
        date = x.AddedDate.DayOfYear.ToString();
    }

}

if (abcd >= (int)ViewData["abstract_model_comment"])
{
    if ((int)ViewData["helper_blog_user_count"] == 0)
    {
        %><a href="<%=Url.Action("ZF", "C")%>">
    <%=Mytrip_Mvc_Language.right_create_blog%></a><%
}
    else
    {
        foreach (mt_artycle_category x in (IEnumerable<mt_artycle_category>)ViewData["helper_blog_user"])
        {
        %><a href="<%=Url.Action("B", "C", new { a = x.Id, b = 1,c=10,d=x.Path })%>">
    <%= x.Title%></a><% break;
        }
    }
      %><br /><br /><% }
else
{
    abcd = (int)ViewData["abstract_model_comment"] - abcd;
        %><%=Html.Language(Mytrip_Mvc_Language.right_activation_blog, abcd.ToString())%>
        
        <br /><br /><% }
    } 
    %></div><%} %>