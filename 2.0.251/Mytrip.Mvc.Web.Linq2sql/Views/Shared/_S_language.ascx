<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div class="login">

<ul><%bool a = (bool)ViewData["abstract_lang_panel"];
      if (a == true)
      {string url = ViewData["helper_language_url"].ToString();%>
 <li><a >Language</a>
            <ul><%foreach (mt_language x in (IEnumerable<mt_language>)ViewData["abstract_language"])
                  {
%>
        	   <a href="<%=Url.Action("AL", "A", new{url, language=x.param})%>"><%=x.name %></a>
        	 <%} %></li>
            </ul>
        </li><%} %>       
    </ul>    
</div>