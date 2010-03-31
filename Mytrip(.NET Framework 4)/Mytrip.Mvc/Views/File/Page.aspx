<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Core.Models.PageModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div>
      <%=Html.ActionLink("Back", "Index", new { directory = Model.directory.Remove(Model.directory.LastIndexOf("()")) })%> | <%=Html.ActionLink("Edite Page", "EditePage", new { directory = Model.directory })%>
    </div>
<%= Model.page%>
<div>
        <%=Html.ActionLink("Back", "Index", new { directory = Model.directory.Remove(Model.directory.LastIndexOf("()")) })%> | <%=Html.ActionLink("Edite Page", "EditePage", new { directory = Model.directory })%>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
