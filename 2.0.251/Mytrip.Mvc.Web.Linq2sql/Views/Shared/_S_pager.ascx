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
<%int abc = (int)ViewData["content_count"];//����� ���������� ������
  string url = ViewData["helper_pager_url"].ToString();//�������� url
  int bcd = (int)ViewData["helper_pager_int"];//���������� �� ��������
  int cda = (int)ViewData["helper_pager_cat"];//����� �������
  int das = (int)ViewData["helper_pager_page"];//������� ��������
  string path = ViewData["helper_pager_path"].ToString();//�������� path
%>
<table>
    <tr>
        <td>
            <div class="pager">
                <small><%=Mytrip_Mvc_Language.pager_pages%></small></div>
        </td>
        <td>
            <div class="pagercount">
                <small><%=Mytrip_Mvc_Language.pager_count%></small></div>
        </td>
    </tr>
    <tr>
        <td>
            <div class="pager">
              </div>
            <%= Html.Pager(url, cda, das, bcd, path, abc) %>  
        </td>
        <td>
            <div class="pagercount">
              <%= Html.PagerCount(url, cda, bcd, path, abc) %> 
            </div>
        </td>
    </tr>
</table>
