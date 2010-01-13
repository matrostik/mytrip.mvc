<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/1_column.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	 <%= ViewData["model_domain"]%>/Подтверждение регистрации по email
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright ©  2009 Стюхин Олег Анатольевич   */-->
    <h2>Подтверждение регистрации по email</h2>
<p>Спасибо за регистрацию на сайте <b><%= ViewData["model_domain"]%>.</b></p>
<p>На email указанный Вами при регистрации выслано письмо. Вам необходимо подтвердить регистрацию перейдя по ссылке в письме.</p>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
