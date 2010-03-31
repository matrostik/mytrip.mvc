<%@Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
<%  //************************************************************ 
    // Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich
    // To learn more about Mytrip.Mvc.Entyty visit 
    // http://starterkitmytripmvc.codeplex.com/
    // mytripmvc@gmail.com
    // license: Microsoft Public License (Ms-PL) 
    // *********************************************************** %>
    <%=Html.PageTitle(CoreLanguage.change_password, "/")%>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2><%=CoreLanguage.change_password%></h2>
    <p>
        <%=CoreLanguage.change_password_success%>
    </p>
</asp:Content>
