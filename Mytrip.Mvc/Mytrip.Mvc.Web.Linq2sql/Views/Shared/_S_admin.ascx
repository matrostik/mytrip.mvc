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

<%if (HttpContext.Current.User.IsInRole("admin"))
        {%>
    <div class="right_title"> 
    &nbsp;&nbsp;<%=Mytrip_Mvc_Language_1.right_control_panel%></div><div class="right_content">
    <a href="<%=Url.Action("C", "A", new{a=2})%>">    
    <%=Mytrip_Mvc_Language_1.menu_site_adjustment%></a><br /><br />
    <%bool a = (bool)ViewData["email_approved"];
      if (a == true)
      { %>
     <a href="<%=Url.Action("L", "A")%>">
    <%=Mytrip_Mvc_Language_1.email_setting%></a><br /><br /><%} %>
   <a href="<%=Url.Action("D", "A", new{a=0, b=1,c=25,d="Users"})%>">
    <%=Mytrip_Mvc_Language_1.menu_management_users%></a><br /><br />
     <a href="<%=Url.Action("F", "F")%>">
    <%=Mytrip_Mvc_Language_1.menu_file_manager%></a><br /><br /></div>  
    <%}
         %>