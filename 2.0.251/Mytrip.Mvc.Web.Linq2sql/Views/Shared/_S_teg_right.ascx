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
<%if((int)ViewData["helper_teg_count"]>0){ %>
 <div class="right_title"> 
    &nbsp;&nbsp;<%=Mytrip_Mvc_Language.menu_tegs%></div><div class="right_content">
 <%string b = "";
     foreach (mt_artycle_in_teg x in (IEnumerable<mt_artycle_in_teg>)ViewData["helper_teg"])
     {
         if (x.mt_artycle_teg.Title != b) {
          %>
          <%= Html.Tegs(x.mt_artycle_teg.Id,x.mt_artycle_teg.Title,x.mt_artycle_teg.Path) %> 
         
         <%b = x.mt_artycle_teg.Title;
     }
     }
%></div><%} %>