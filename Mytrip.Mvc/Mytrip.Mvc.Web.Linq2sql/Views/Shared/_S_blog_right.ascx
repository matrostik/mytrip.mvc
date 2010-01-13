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

 <%if ((bool)ViewData["model_blog"] == true)
    {%>
  <div class="right_title">
    &nbsp; <a href="<%=Url.Action("A", "C", new { a = 1, b = 10, c="Blogs"})%>">
    <%=Mytrip_Mvc_Language_1.menu_blogs%></a></div><div class="right_content">
  <%int a=0;
      foreach (mt_artycle_category x in (IEnumerable<mt_artycle_category>)ViewData["blog"])
      {
%><a href="<%=Url.Action("Rss_A", "C", new { a = x.Id})%>">
    <img src="/content/images/feed.png" alt="rss" style="border-width: 0px; width: 14px; float: right;" /></a>
<a href="<%=Url.Action("B", "C", new { a = x.Id, b = 1,c=10,d=x.Path })%>">
    <%= x.Title%></a>
    
<br />
<small><%=x.AddedBy%></small>
<br />
<small><%=Html.Language(Mytrip_Mvc_Language_1.article_views, x.Views.ToString())%></small>
<br />
<small><%=Html.Language(Mytrip_Mvc_Language_1.blog_posts, x.mt_artycle.Count.ToString())%></small>
<br />
<%a++;if(a==5)break;
    }%></div><%
   } %>