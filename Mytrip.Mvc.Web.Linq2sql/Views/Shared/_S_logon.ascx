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
<%bool captcha = (bool)ViewData["captcha"];
    if (Request.IsAuthenticated) {
%>
        Здравствуй <b><%= Html.Encode(Page.User.Identity.Name) %></b>!
       <div class="log_a">
<div class="log_gr"><%= Html.ActionLink("выход", "F", "B") %></div></div>
        <div class="log_a">
<div class="log_gr"><%= Html.ActionLink("сменить пароль", "C", "B")%></div></div>
<%
    }
    else
    {
        string urla = (string)ViewData["logon_url"];
%> 
        
        <div class="log_a">
<div class="log_gr"><%if(captcha==false){ %> <%= Html.ActionLink("регистрация", "B", "B")%><%}else{%> <%= Html.ActionLink("регистрация", "H", "B")%><%} %></div></div>
<div class="log_a">
<div class="log_gr"><a href="<%=Url.Action("A", "B", new { returnUrl = urla})%>">вход</a></div></div>

<%
    }
%>
