<%@Language="C#" MasterPageFile="~/Views/Shared/1_column.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
    <%= ViewData["abstract_model_domain"]%>/<%=Mytrip_Mvc_Language.menu_change_password%>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright ©  2009 Стюхин Олег Анатольевич   */-->
    <h2><%=Mytrip_Mvc_Language.menu_change_password%></h2>
    <p>
        <%=Mytrip_Mvc_Language.change_password_text%>
    </p>
</asp:Content>
