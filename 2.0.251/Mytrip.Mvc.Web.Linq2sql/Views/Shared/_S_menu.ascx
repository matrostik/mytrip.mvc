<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright ©  2009 Стюхин Олег Анатольевич oleg@stuh.in   */-->
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%string urla = (string)ViewData["helper_menu_url"]; 
  bool news = (bool)ViewData["abstract_model_news"];
  bool artycles = (bool)ViewData["abstract_model_artycle"];
  bool blogs = (bool)ViewData["abstract_model_blog"];
  bool captcha = (bool)ViewData["abstract_captcha"]; %>


<!--<%//=Html.Menu(urla,news,artycles,blogs,captcha) %>-->
        
 <div class="topmenu">
    <ul><li><% if (urla != "home")
           { %>
        <a href="<%=Url.Action("A", "A")%>"><%=Mytrip_Mvc_Language.menu_home%></a>
        <%}
           else {%>
        <a href="<%=Url.Action("A", "A")%>" style="color: #2E2633;
    background-image: url(/content/images/4.png);    
    border: 1px solid #E5C365;"><%=Mytrip_Mvc_Language.menu_home%></a>
        <% } %></li>
         <%foreach (mt_artycle_category x in (IEnumerable<mt_artycle_category>)ViewData["helper_artycle_category_menu"])
            {
%><li><%if (urla != x.Path)
        { %><a href="<%=Url.Action("B", "C", new { a = x.Id, b = 1,c=10,d=x.Path })%>">
    <%= x.Title%></a><%}
        else {%><a href="<%=Url.Action("B", "C", new { a = x.Id, b = 1,c=10,d=x.Path })%>"style="color: #2E2633;
    background-image: url(/content/images/4.png);    
    border: 1px solid #E5C365;" >
    <%= x.Title%></a><% } %>
<ul><li>
<%foreach (mt_artycle_category y in x.mt_artycle_category2)
  { %>
<a href="<%=Url.Action("B", "C", new { a = y.Id, b = 1,c=10,d=y.Path })%>">
    <%= y.Title%></a>
<%} %></li>
</ul>

</li>



<%} %>
     <% if (news == true)
        {
            if ((int)ViewData["helper_news_category_count"] > 0)
            {%><li>

                <%if (urla != "news")
                  { %>
        <a href="<%=Url.Action("A", "C", new { a = 1, b = 10, c="News"})%>"><%=Mytrip_Mvc_Language.menu_news%></a>
        <%}
                  else
                  {%>
        <a href="<%=Url.Action("A", "C", new { a = 1, b = 10, c="News"})%>" style="color: #2E2633;
    background-image: url(/content/images/4.png);    
    border: 1px solid #E5C365;"><%=Mytrip_Mvc_Language.menu_news%></a>
        <% } %>
            <ul><li><%foreach (mt_artycle_category x in (IEnumerable<mt_artycle_category>)ViewData["helper_news_category"])
                      {%>
        	   <a href="<%=Url.Action("B", "C", new { a = x.Id, b = 1,c=10,d=x.Path })%>">
    <%= x.Title%></a>
        	   <%} %></li>
            </ul>
        </li><%}
            else { if (Page.User.IsInRole("artycle_editor")) { %><li>

                <%if (urla != "news")
                  { %>
        <a href="<%=Url.Action("A", "C", new { a = 1, b = 10, c="News"})%>"><%=Mytrip_Mvc_Language.menu_news%></a>
        <%}
                  else
                  {%>
        <a href="<%=Url.Action("A", "C", new { a = 1, b = 10, c="News"})%>" style="color: #2E2633;
    background-image: url(/content/images/4.png);    
    border: 1px solid #E5C365;"><%=Mytrip_Mvc_Language.menu_news%></a>
        <% } %>
            <ul><li><%foreach (mt_artycle_category x in (IEnumerable<mt_artycle_category>)ViewData["helper_news_category"])
                      {%>
        	   <a href="<%=Url.Action("B", "C", new { a = x.Id, b = 1,c=10,d=x.Path })%>">
    <%= x.Title%></a>
        	   <%} %></li>
            </ul>
        </li><%} if (Page.User.IsInRole("chief_editor")) {%><li>

                <%if (urla != "news")
                  { %>
        <a href="<%=Url.Action("A", "C", new { a = 1, b = 10, c="News"})%>"><%=Mytrip_Mvc_Language.menu_news%></a>
        <%}
                  else
                  {%>
        <a href="<%=Url.Action("A", "C", new { a = 1, b = 10, c="News"})%>" style="color: #2E2633;
    background-image: url(/content/images/4.png);    
    border: 1px solid #E5C365;"><%=Mytrip_Mvc_Language.menu_news%></a>
        <% } %>
            <ul><li><%foreach (mt_artycle_category x in (IEnumerable<mt_artycle_category>)ViewData["helper_news_category"])
                      {%>
        	   <a href="<%=Url.Action("B", "C", new { a = x.Id, b = 1,c=10,d=x.Path })%>">
    <%= x.Title%></a>
        	   <%} %></li>
            </ul>
        </li><% } }
        } if (artycles == true)
        {
            if ((int)ViewData["helper_artycle_category_no_menu_count"] > 0)
            {
%><li><%
                 if (urla != "artycles")
                 {%>
        <a href="<%=Url.Action("A", "C", new { a = 1, b = 10, c="Articles"})%>">
    <%=Mytrip_Mvc_Language.menu_artycles%></a>
        <%}
                 else
                 {%>
        <a href="<%=Url.Action("A", "C", new { a = 1, b = 10, c="Articles"})%>"style="color: #2E2633;
    background-image: url(/content/images/4.png);    
    border: 1px solid #E5C365;">
    <%=Mytrip_Mvc_Language.menu_artycles%></a>
        <% } %>
            <ul><li>
        	 <%foreach (mt_artycle_category x in (IEnumerable<mt_artycle_category>)ViewData["helper_artycle_category_no_menu"])
            {
%><a href="<%=Url.Action("B", "C", new { a = x.Id, b = 1,c=10,d=x.Path })%>">
    <%= x.Title%></a><%} %></li> 
            </ul>
        </li> <%}
            else { if (Page.User.IsInRole("artycle_editor")) { %><li><%
                 if (urla != "artycles")
                 {%>
        <a href="<%=Url.Action("A", "C", new { a = 1, b = 10, c="Articles"})%>">
    <%=Mytrip_Mvc_Language.menu_artycles%></a>
        <%}
                 else
                 {%>
        <a href="<%=Url.Action("A", "C", new { a = 1, b = 10, c="Articles"})%>"style="color: #2E2633;
    background-image: url(/content/images/4.png);    
    border: 1px solid #E5C365;">
    <%=Mytrip_Mvc_Language.menu_artycles%></a>
        <% } %>
            <ul><li>
        	 <%foreach (mt_artycle_category x in (IEnumerable<mt_artycle_category>)ViewData["helper_artycle_category_no_menu"])
            {
%><a href="<%=Url.Action("B", "C", new { a = x.Id, b = 1,c=10,d=x.Path })%>">
    <%= x.Title%></a><%} %></li> 
            </ul>
        </li> <%}if (Page.User.IsInRole("chief_editor")) { %><li><%
                 if (urla != "artycles")
                 {%>
        <a href="<%=Url.Action("A", "C", new { a = 1, b = 10, c="Articles"})%>">
    <%=Mytrip_Mvc_Language.menu_artycles%></a>
        <%}
                 else
                 {%>
        <a href="<%=Url.Action("A", "C", new { a = 1, b = 10, c="Articles"})%>"style="color: #2E2633;
    background-image: url(/content/images/4.png);    
    border: 1px solid #E5C365;">
    <%=Mytrip_Mvc_Language.menu_artycles%></a>
        <% } %>
            <ul><li>
        	 <%foreach (mt_artycle_category x in (IEnumerable<mt_artycle_category>)ViewData["helper_artycle_category_no_menu"])
            {
%><a href="<%=Url.Action("B", "C", new { a = x.Id, b = 1,c=10,d=x.Path })%>">
    <%= x.Title%></a><%} %></li> 
            </ul>
        </li> <%} }
        } if (blogs == true)
            {if ((int)ViewData["helper_blog_count"] > 0)
            {
%><li><%
                 if (urla != "blogs")
                 {%>
        <a href="<%=Url.Action("A", "C", new { a = 1, b = 10, c="Blogs"})%>">
    <%=Mytrip_Mvc_Language.menu_blogs%></a>
        <%}
                 else
                 {%>
        <a href="<%=Url.Action("A", "C", new { a = 1, b = 10, c="Blogs"})%>"style="color: #2E2633;
    background-image: url(/content/images/4.png);    
    border: 1px solid #E5C365;">
    <%=Mytrip_Mvc_Language.menu_blogs%></a>
        <% } %>
            <ul><li>
        	 <%int a = 0;
            foreach (mt_artycle_category x in (IEnumerable<mt_artycle_category>)ViewData["helper_blog"])
            {
%><a href="<%=Url.Action("B", "C", new { a = x.Id, b = 1,c=10,d=x.Path })%>">
    <%= x.Title%></a><%
                         a++; if (a == 20) break;
            } %></li> 
            </ul>
        </li> <%}}%> 
        <%=Html.Menu(urla,captcha) %>
    </ul>
</div>     
