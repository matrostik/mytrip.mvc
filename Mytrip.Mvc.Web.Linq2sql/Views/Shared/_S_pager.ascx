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
  string url = ViewData["content_url"].ToString();//�������� url
  int bcd = (int)ViewData["content_int"];//���������� �� ��������
  int cda = (int)ViewData["content_cat"];//����� �������
  int das = (int)ViewData["content_page"];//������� ��������
  string path = ViewData["content_path"].ToString();//�������� path
%>
<table>
    <tr>
        <td>
            <div class="pager">
                <small>��������</small></div>
        </td>
        <td>
            <div class="pagercount">
                <small>���������� �� ��������</small></div>
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
