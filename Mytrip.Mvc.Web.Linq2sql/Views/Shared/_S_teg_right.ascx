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

 <div class="right_title">  <div class="right_title_a1"></div>
<div class="right_title_a2"></div>
    &nbsp;&nbsp;����</div><div class="teg">
 <%
     foreach (mt_artycle_teg x in (IEnumerable<mt_artycle_teg>)ViewData["teg"])
     { if (x.mt_artycle_in_teg.Count() != 0) {
         Random ran = new Random();
         int abc = ran.Next(10, 18);
         if (abc == 10)
         {%>
  <a href="<%=Url.Action("E", "C", new { a = x.Id, b = 1, c=10, d=x.Path})%>" style="font-size: 10px">
    [<%=x.Title%>]</a>
 <%}if (abc == 11)
         {%>
  <a href="<%=Url.Action("E", "C", new { a = x.Id, b = 1, c=10, d=x.Path})%>" style="font-size: 10px; font-weight: bold;">
    [<%=x.Title%>]</a>
 <%}if (abc == 12)
         {%>
  <a href="<%=Url.Action("E", "C", new { a = x.Id, b = 1, c=10, d=x.Path})%>" style="font-size: 12px">
    [<%=x.Title%>]</a>
 <%}if (abc == 13)
         {%>
  <a href="<%=Url.Action("E", "C", new { a = x.Id, b = 1, c=10, d=x.Path})%>" style="font-size: 12px; font-weight: bold;">
   [<%=x.Title%>]</a>
 <%}if (abc == 14)
         {%>
  <a href="<%=Url.Action("E", "C", new { a = x.Id, b = 1, c=10, d=x.Path})%>" style="font-size: 14px">
    [<%=x.Title%>]</a>
 <%}if (abc == 15)
         {%>
  <a href="<%=Url.Action("E", "C", new { a = x.Id, b = 1, c=10, d=x.Path})%>" style="font-size: 14px; font-weight: bold;">
    [<%=x.Title%>]</a>
 <%}if (abc == 16)
         {%>
  <a href="<%=Url.Action("E", "C", new { a = x.Id, b = 1, c=10, d=x.Path})%>" style="font-size: 16px">
    [<%=x.Title%>]</a>
 <%}if (abc == 17)
         {%>
  <a href="<%=Url.Action("E", "C", new { a = x.Id, b = 1, c=10, d=x.Path})%>" style="font-size: 16px; font-weight: bold;">
    [<%=x.Title%>]</a>
 <%}if (abc == 18)
         {%>
  <a href="<%=Url.Action("E", "C", new { a = x.Id, b = 1, c=10, d=x.Path})%>" style="font-size: 18px; font-weight: bold;">
    [<%=x.Title%>]</a>
 <%}
     }
     }
%></div>