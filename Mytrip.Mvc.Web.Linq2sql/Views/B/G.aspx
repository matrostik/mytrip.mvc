<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/1_column.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	 <%= ViewData["model_domain"]%>/������������� ����������� �� email
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright �  2009 ������ ���� �����������   */-->
    <h2>������������� ����������� �� email</h2>
<p>������� �� ����������� �� ����� <b><%= ViewData["model_domain"]%>.</b></p>
<p>�� email ��������� ���� ��� ����������� ������� ������. ��� ���������� ����������� ����������� ������� �� ������ � ������.</p>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
