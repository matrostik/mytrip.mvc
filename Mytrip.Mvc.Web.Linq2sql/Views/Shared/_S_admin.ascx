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
    <div class="right_title">  <div class="right_title_a1"></div>
<div class="right_title_a2"></div>
    &nbsp;&nbsp;������ ����������</div><div class="right_content">
    <a href="<%=Url.Action("C", "A")%>">
    ��������� �����</a><br /><br />
   <a href="<%=Url.Action("D", "A", new{a=0, b=1,c=25,d="Users"})%>">
    ���������� ��������������</a><br /><br />
     <a href="<%=Url.Action("F", "F")%>">
    �������� ��������</a><br /><br /></div>  
    <%}
         %>