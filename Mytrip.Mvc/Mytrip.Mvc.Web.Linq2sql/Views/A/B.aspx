<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/1.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= Mytrip_Mvc_Language_1.licence%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
<div style="text-align: center">
    <h2><%= Mytrip_Mvc_Language_1.licence%></h2>
    <p>
<textarea  cols="20" rows="20" style="width: 600px; height: 400px">
<%= Mytrip_Mvc_Language_1.GPLv2%>
 </textarea></p></div>
<% using (Html.BeginForm())
   {%>
  
                  <div class="button1">   
    <input type="submit" value="<%= Mytrip_Mvc_Language_1.agree%>"  class="input_boottom" />
  </div>
    <% } %>
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
