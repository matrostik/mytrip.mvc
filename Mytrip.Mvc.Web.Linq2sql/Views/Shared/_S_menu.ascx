<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright �  2009 ������ ���� ����������� oleg@stuh.in 
��� ��������� �������� ��������� ����������� ���������; �� ������ �������������� �/��� �������� 
�� � ������������ � ��������� ����������� ������������ �������� GNU � ��� ����, ��� ��� ���� ������������ 
������ ���������� ��; ���� ������ 2 �������� ���� (�� ������ �������) ����� ����� ������� ������.
��������� ���������������� � �������, ��� ��� ����� ��������, �� ��� ����� �� �� �� ���� ����������� ������������;
���� ��� ��������� ����������� ������������, ��������� � ���������������� ���������� � ������������ ��� ������������ �����. 
��� ������������ �������� ����������� ������������ �������� GNU.
�� ������ ���� �������� ����� ����������� ������������ �������� GNU ������ � ���� ����������; 
���� ��� �� ���, �������� � ���� ���������� �� 
(Free Software Foundation, Inc., 675 Mass Ave, Cambridge, MA 02139, USA).  */-->
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%string urla = (string)ViewData["menu_url"]; 
  bool news = (bool)ViewData["model_news"];
  bool artycles = (bool)ViewData["model_artycle"];
  bool blogs = (bool)ViewData["model_blog"];%>
<div class="menu_a">
<div class="menu_a1"></div>
<div class="menu_a2"></div>
<%if (urla != "home")
  { %>
<div class="menu_gr">
    
        <%= Html.ActionLink("�������", "A", "A")%></div>
        
        <%}
  else { %>
<div class="menu_gr1">
    
        <%= Html.ActionLink("�������", "A", "A")%></div>
        
        <% } %></div>
         <%if(news==true)
      {
    %><div class="menu_a">
    <div class="menu_a1"></div>
<div class="menu_a2"></div>
<%if (urla != "news")
  { %>
<div class="menu_gr">
    
        <a href="<%=Url.Action("F", "C", new { a = 0, b = 1,c=10, d="News" })%>">
        �������</a></div>
        
        <%}
  else { %>
<div class="menu_gr1">
    
        <a href="<%=Url.Action("F", "C", new { a = 0, b = 1,c=10, d="News" })%>">
        �������</a></div>
        
        <% } %></div>
    <%
        } %>
    <%if(artycles==true)
      {
    %><div class="menu_a">
    <div class="menu_a1"></div>
<div class="menu_a2"></div>
<%if (urla != "artycles")
  { %>
<div class="menu_gr">
    
        <a href="<%=Url.Action("A", "C", new { a = 0, b = 1,c=10, d="Artycles" })%>">
        ������</a></div>
        
        <%}
  else { %>
<div class="menu_gr1">
    
        <a href="<%=Url.Action("A", "C", new { a = 0, b = 1,c=10, d="Artycles" })%>">
        ������</a></div>
        
        <% } %></div>
    <%
        } %>  
   <%if(artycles==true)
      {
    %><div class="menu_a">
    <div class="menu_a1"></div>
<div class="menu_a2"></div>
<%if (urla != "blogs")
  { %>
<div class="menu_gr">
    
        <a class="tt" href="<%=Url.Action("D", "C", new { a = 0, b = 1,c=10, d="Blogs" })%>">
        �����</a></div>
        
        <%}
  else { %>
<div class="menu_gr1">
    
        <a class="tt" href="<%=Url.Action("D", "C", new { a = 0, b = 1,c=10, d="Blogs" })%>">
        �����</a></div>
        
        <% } %></div>
    <%
        }if(urla=="logon"){ %> 

<div class="menu_a">
    <div class="menu_a1"></div>
<div class="menu_a2"></div>

<div class="menu_gr1">
    
        <a class="tt" href="<%=Url.Action("A", "B")%>">
        ����</a></div>
        
        </div><%} if (urla == "registr")
     {
         bool captcha = (bool)ViewData["captcha"]; 
                    if (captcha == false){ %>
                     <div class="menu_a">
    <div class="menu_a1"></div>
<div class="menu_a2"></div>

<div class="menu_gr1">
    
        <a class="tt" href="<%=Url.Action("B", "B")%>">
        �����������</a></div>
        
        </div>
                     <%} else {%> 
                     <div class="menu_a">
    <div class="menu_a1"></div>
<div class="menu_a2"></div>

<div class="menu_gr1">
    
        <a class="tt" href="<%=Url.Action("H", "B")%>">
        �����������</a></div>
        
        </div><% }
                }if(urla=="tegs"){ %> 

<div class="menu_a">
    <div class="menu_a1"></div>
<div class="menu_a2"></div>

<div class="menu_gr1">
    
        <a class="tt">
        ����</a></div>
        
        </div><%}if(urla=="search"){ %> 

<div class="menu_a">
    <div class="menu_a1"></div>
<div class="menu_a2"></div>

<div class="menu_gr1">
    
        <a class="tt">
        �����</a></div>
        
        </div><%}if(urla=="thema_blog"){ %> 

<div class="menu_a">
    <div class="menu_a1"></div>
<div class="menu_a2"></div>

<div class="menu_gr1">
    
        <a class="tt">
        ������� ���� �����</a></div>
        
        </div><%}if(urla=="thema_news"){ %> 

<div class="menu_a">
    <div class="menu_a1"></div>
<div class="menu_a2"></div>

<div class="menu_gr1">
    
        <a class="tt">
        ������� ����������</a></div>
        
        </div><%}if(urla=="rub_artycle"){ %> 

<div class="menu_a">
    <div class="menu_a1"></div>
<div class="menu_a2"></div>

<div class="menu_gr1">
    
        <a class="tt">
        ������� �������</a></div>
        
        </div><%}if(urla=="edit_rub_artycle"){ %> 

<div class="menu_a">
    <div class="menu_a1"></div>
<div class="menu_a2"></div>

<div class="menu_gr1">
    
        <a class="tt">
        ������������� �������</a></div>
        
        </div><%}if(urla=="edit_thema_news"){ %> 

<div class="menu_a">
    <div class="menu_a1"></div>
<div class="menu_a2"></div>

<div class="menu_gr1">
    
        <a class="tt">
        ������������� ����������</a></div>
        
        </div><%}if(urla=="edit_blog"){ %> 

<div class="menu_a">
    <div class="menu_a1"></div>
<div class="menu_a2"></div>

<div class="menu_gr1">
    
        <a class="tt">
        ������������� ����</a></div>
        
        </div><%}if(urla=="create_artycle"){ %> 

<div class="menu_a">
    <div class="menu_a1"></div>
<div class="menu_a2"></div>

<div class="menu_gr1">
    
        <a class="tt">
        �������� ������</a></div>
        
        </div><%}if(urla=="edit_artycle"){ %> 

<div class="menu_a">
    <div class="menu_a1"></div>
<div class="menu_a2"></div>

<div class="menu_gr1">
    
        <a class="tt">
        ������������� ������</a></div>
        
        </div><%}if(urla=="create_post"){ %> 

<div class="menu_a">
    <div class="menu_a1"></div>
<div class="menu_a2"></div>

<div class="menu_gr1">
    
        <a class="tt">
        �������� ����</a></div>
        
        </div><%}if(urla=="edit_post"){ %> 

<div class="menu_a">
    <div class="menu_a1"></div>
<div class="menu_a2"></div>

<div class="menu_gr1">
    
        <a class="tt">
        ������������� ����</a></div>
        
        </div><%}if(urla=="create_news"){ %> 

<div class="menu_a">
    <div class="menu_a1"></div>
<div class="menu_a2"></div>

<div class="menu_gr1">
    
        <a class="tt">
        �������� �������</a></div>
        
        </div><%}if(urla=="edit_news"){ %> 

<div class="menu_a">
    <div class="menu_a1"></div>
<div class="menu_a2"></div>

<div class="menu_gr1">
    
        <a class="tt">
        ������������� �������</a></div>
        
        </div><%}if(urla=="edit_comm"){ %> 

<div class="menu_a">
    <div class="menu_a1"></div>
<div class="menu_a2"></div>

<div class="menu_gr1">
    
        <a class="tt">
        ������������� �����������</a></div>
        
        </div><%}if(urla=="file"){ %> 

<div class="menu_a">
    <div class="menu_a1"></div>
<div class="menu_a2"></div>

<div class="menu_gr1">
    
        <a class="tt">
        �������� ��������</a></div>
        
        </div><%}%>
