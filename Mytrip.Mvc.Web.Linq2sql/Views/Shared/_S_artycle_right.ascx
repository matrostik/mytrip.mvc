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
<%if ((bool)ViewData["model_artycle"] == true)
  {%>
<div class="right_title">
    &nbsp; <a href="<%=Url.Action("A", "C", new { a = 0, b = 1, c=10, d="Artycles"})%>">
    ������</a></div><div class="right_content">
<%foreach (mt_artycle_category x in (IEnumerable<mt_artycle_category>)ViewData["artycle_category"])
  {
%><a href="<%=Url.Action("Rss_A", "C", new { a = x.Id})%>">
    <img src="/content/images/feed.png" alt="rss" style="border-width: 0px; width: 14px; float: right;" /></a>
<a href="<%=Url.Action("B", "C", new { a = x.Id, b = 1,c=10,d=x.Path })%>">
    <%= x.Title%></a>
    
<br />
<br />
<%
    }%></div><%
  } %>