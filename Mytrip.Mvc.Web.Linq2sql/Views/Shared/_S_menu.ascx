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
<%string urla = (string)ViewData["menu_url"]; 
  bool news = (bool)ViewData["model_news"];
  bool artycles = (bool)ViewData["model_artycle"];
  bool blogs = (bool)ViewData["model_blog"];%>
<div class="menu_a">

<%if (urla != "home")
  { %>
<div class="menu_gr">
    
        <%= Html.ActionLink("Главная", "A", "A")%></div>
        
        <%}
  else { %>
<div class="menu_gr1">
    
        <%= Html.ActionLink("Главная", "A", "A")%></div>
        
        <% } %></div>
         <%if(news==true)
      {
    %><div class="menu_a">

<%if (urla != "news")
  { %>
<div class="menu_gr">
    
        <a href="<%=Url.Action("F", "C", new { a = 0, b = 1,c=10, d="News" })%>">
        Новости</a></div>
        
        <%}
  else { %>
<div class="menu_gr1">
    
        <a href="<%=Url.Action("F", "C", new { a = 0, b = 1,c=10, d="News" })%>">
        Новости</a></div>
        
        <% } %></div>
    <%
        } %>
    <%if(artycles==true)
      {
    %><div class="menu_a">
   
<%if (urla != "artycles")
  { %>
<div class="menu_gr">
    
        <a href="<%=Url.Action("A", "C", new { a = 0, b = 1,c=10, d="Artycles" })%>">
        Статьи</a></div>
        
        <%}
  else { %>
<div class="menu_gr1">
    
        <a href="<%=Url.Action("A", "C", new { a = 0, b = 1,c=10, d="Artycles" })%>">
        Статьи</a></div>
        
        <% } %></div>
    <%
        } %>  
   <%if(artycles==true)
      {
    %><div class="menu_a">
   
<%if (urla != "blogs")
  { %>
<div class="menu_gr">
    
        <a class="tt" href="<%=Url.Action("D", "C", new { a = 0, b = 1,c=10, d="Blogs" })%>">
        Блоги</a></div>
        
        <%}
  else { %>
<div class="menu_gr1">
    
        <a class="tt" href="<%=Url.Action("D", "C", new { a = 0, b = 1,c=10, d="Blogs" })%>">
        Блоги</a></div>
        
        <% } %></div>
    <%
        }if(urla=="logon"){ %> 

<div class="menu_a">
 

<div class="menu_gr1">
    
        <a class="tt" href="<%=Url.Action("A", "B")%>">
        Вход</a></div>
        
        </div><%} if (urla == "registr")
     {
         bool captcha = (bool)ViewData["captcha"]; 
                    if (captcha == false){ %>
                     <div class="menu_a">
 

<div class="menu_gr1">
    
        <a class="tt" href="<%=Url.Action("B", "B")%>">
        Регистрация</a></div>
        
        </div>
                     <%} else {%> 
                     <div class="menu_a">
    

<div class="menu_gr1">
    
        <a class="tt" href="<%=Url.Action("H", "B")%>">
        Регистрация</a></div>
        
        </div><% }
                }if(urla=="tegs"){ %> 

<div class="menu_a">
 

<div class="menu_gr1">
    
        <a class="tt">
        Теги</a></div>
        
        </div><%}if(urla=="search"){ %> 

<div class="menu_a">
  

<div class="menu_gr1">
    
        <a class="tt">
        Поиск</a></div>
        
        </div><%}if(urla=="thema_blog"){ %> 

<div class="menu_a">
 

<div class="menu_gr1">
    
        <a class="tt">
        Создать тему блога</a></div>
        
        </div><%}if(urla=="thema_news"){ %> 

<div class="menu_a">


<div class="menu_gr1">
    
        <a class="tt">
        Создать подрубрику</a></div>
        
        </div><%}if(urla=="rub_artycle"){ %> 

<div class="menu_a">
   

<div class="menu_gr1">
    
        <a class="tt">
        Создать рубрику</a></div>
        
        </div><%}if(urla=="edit_rub_artycle"){ %> 

<div class="menu_a">
  

<div class="menu_gr1">
    
        <a class="tt">
        Редактировать рубрику</a></div>
        
        </div><%}if(urla=="edit_thema_news"){ %> 

<div class="menu_a">
   

<div class="menu_gr1">
    
        <a class="tt">
        Редактировать подрубрику</a></div>
        
        </div><%}if(urla=="edit_blog"){ %> 

<div class="menu_a">
  

<div class="menu_gr1">
    
        <a class="tt">
        Редактировать блог</a></div>
        
        </div><%}if(urla=="create_artycle"){ %> 

<div class="menu_a">


<div class="menu_gr1">
    
        <a class="tt">
        Написать статью</a></div>
        
        </div><%}if(urla=="edit_artycle"){ %> 

<div class="menu_a">


<div class="menu_gr1">
    
        <a class="tt">
        Редактировать статью</a></div>
        
        </div><%}if(urla=="create_post"){ %> 

<div class="menu_a">
  

<div class="menu_gr1">
    
        <a class="tt">
        Написать пост</a></div>
        
        </div><%}if(urla=="edit_post"){ %> 

<div class="menu_a">
  

<div class="menu_gr1">
    
        <a class="tt">
        Редактировать пост</a></div>
        
        </div><%}if(urla=="create_news"){ %> 

<div class="menu_a">
 

<div class="menu_gr1">
    
        <a class="tt">
        Написать новость</a></div>
        
        </div><%}if(urla=="edit_news"){ %> 

<div class="menu_a">
 

<div class="menu_gr1">
    
        <a class="tt">
        Редактировать новость</a></div>
        
        </div><%}if(urla=="edit_comm"){ %> 

<div class="menu_a">


<div class="menu_gr1">
    
        <a class="tt">
        Редактировать комментарий</a></div>
        
        </div><%}if(urla=="file"){ %> 

<div class="menu_a">
 

<div class="menu_gr1">
    
        <a class="tt">
        Файловый менеджер</a></div>
        
        </div><%}if(urla=="user"){ %> 

<div class="menu_a">


<div class="menu_gr1">
    
        <a href="<%=Url.Action("D", "A", new{a=0, b=1,c=25,d="Users"})%>">
    Управление пользователями</a></div>
        
        </div><%} if (urla == "site_setting")
     { %> 

<div class="menu_a">
   

<div class="menu_gr1">
    
        <a href="<%=Url.Action("C", "A")%>">
    Настройка сайта</a></div>
        
        </div><%}%>
