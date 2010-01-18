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
<div class="login">
<%bool captcha = (bool)ViewData["abstract_captcha"];
  if (Request.IsAuthenticated)
  {
%><b ><%=Mytrip_Mvc_Language.hi_user%> <%= Html.Encode(Page.User.Identity.Name)%>!</b><%} %>
<ul><%bool a = (bool)ViewData["abstract_lang_panel"];
      if (a == true)
      {string url = ViewData["helper_language_url"].ToString();
     %>
 <li><a >Language</a>
            <ul><li><%foreach (mt_language x in (IEnumerable<mt_language>)ViewData["abstract_language"])
                  {                        
%>
        	   <a href="<%=Url.Action("AL", "A", new{url, language=x.param})%>"><%= x.name %></a>
        	 <%} %></li>
            </ul>
        </li> 
<%} if (Request.IsAuthenticated)
  { %>
 <li><%= Html.ActionLink(Mytrip_Mvc_Language.menu_logoff, "F", "B")%></li>
       <li><%= Html.ActionLink(Mytrip_Mvc_Language.menu_change_password, "C", "B")%></li>
       <%}
  else {  string urla = (string)ViewData["helper_logon_url"];
if(captcha==false){ %> 
<li><%= Html.ActionLink(Mytrip_Mvc_Language.menu_register, "B", "B")%></li>
<%}else{%> 
<li><%= Html.ActionLink(Mytrip_Mvc_Language.menu_register, "H", "B")%></li><%} %>
<li><a href="<%=Url.Action("A", "B", new { returnUrl = urla})%>"><%=Mytrip_Mvc_Language.menu_logon %></a></li>

<%} %>
      
    </ul>    
</div>
