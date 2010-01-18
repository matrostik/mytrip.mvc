<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div id="newsticker-demo">
    <div class="title">
        Latest News</div>
    <div class="newsticker-jcarousellite">
     <div class="newsticker-jcarousellite">
                        <ul>  <%foreach (mt_artycle x in (IEnumerable<mt_artycle>)ViewData["helper_move_content"])
          {
        %>
                            <li>
                                <div class="thumbnail">
                                   <%if (x.UrlImageDescription.Length > 7)
    { %>
        <img src="<%=x.UrlImageDescription %>" alt="<%=x.Title %>" style="width:100px;" />
        <%} %>

                                </div>
                                <div class="info">
                                <a href="<%=Url.Action("C", "C", new { a = x.Id, b=x.Path})%>" style="font-size: 14px; font-weight: bold">
    <%= x.Title%></a><%if (x.RegistrUser == true)
                       { %><br /><small><%=Mytrip_Mvc_Language.only_for_registered%></small> <%} %>
                                   <br /><%= x.Description%>
                                </div>
                                <div class="clear">
                                </div>
                            </li>
                           <%} %>
                        </ul>
                    </div>
    </div>
</div>
