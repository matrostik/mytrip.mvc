<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Core.Models.PageModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	EditPage
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>EditPage</h2>
<div>
        <%=Html.ActionLink("Back to Page", "Page", new { directory = Model.directory })%>
    </div>
    <% using (Html.BeginForm()) {%>

        <fieldset>
            <legend>Fields</legend>
            <div class="editor-field">
                <%= Html.TextAreaFor(model => model.page, new { style="width:100%;height:600px;"})%>
                <%= Html.ValidationMessageFor(model => model.page) %>
            </div>
            
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%=Html.ActionLink("Back to Page", "Page", new { directory = Model.directory })%>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContentLeft" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="MainContentRight" runat="server">
</asp:Content>

