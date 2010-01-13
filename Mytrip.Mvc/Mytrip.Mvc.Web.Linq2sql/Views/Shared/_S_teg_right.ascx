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

 <div class="right_title"> 
    &nbsp;&nbsp;<%=Mytrip_Mvc_Language_1.menu_tegs%></div><div class="teg">
 <%string b = "";
     foreach (mt_artycle_in_teg x in (IEnumerable<mt_artycle_in_teg>)ViewData["teg"])
     {
         if (x.mt_artycle_teg.Title != b) {
          %>
          <%= Html.Tegs(x.mt_artycle_teg.Id,x.mt_artycle_teg.Title,x.mt_artycle_teg.Path) %> 
         
         <%b = x.mt_artycle_teg.Title;
     }
     }
%></div>