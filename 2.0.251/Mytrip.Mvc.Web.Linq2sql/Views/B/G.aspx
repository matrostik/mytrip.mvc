<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/1_column.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	 <%= ViewData["abstract_model_domain"]%>/<%=Mytrip_Mvc_Language.email_approved %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright ©  2009 Стюхин Олег Анатольевич   */-->
    <h2><%=Mytrip_Mvc_Language.email_approved %></h2>
<p><%=Mytrip_Mvc_Language.email_approved_text2 %>  <b><%= ViewData["abstract_model_domain"]%>.</b></p>
<p><%=Mytrip_Mvc_Language.email_approved_text1 %></p>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
