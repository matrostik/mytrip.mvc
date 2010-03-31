<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Articles.Models.CategoryModels>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=Html.PageTitle(Model.PageTitle, "/")%>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ScriptContent" runat="server">
    <script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%=Model.PageTitle%></h2>
    <% using (Html.BeginForm())
       {%>
    <fieldset>
        <legend>Enter information</legend>
        <%=Html.HiddenFor(model=>model.CategoryId) %>
        <%=Html.HiddenFor(model => model.Path)%>
        <div class="editor-label">
            <%= Html.MytripLabelFor("Title",ArticleLanguage.name) %>
        </div>
        <div class="editor-field">
            <%= Html.TextBoxFor(model => model.Title) %>
            <%= Html.ValidationMessageFor(model => model.Title) %>
        </div>
        <div class="editor-label" style="display: <%=Model.ShowSeparateBlock %>">
            <%= Html.CheckBoxFor(model => model.SeparateBlock)%>
            <%=Html.MytripLabelFor("SeparateBlock", ArticleLanguage.add_to_menu)%>
        </div>
        <div class="editor-label" style="display: <%=Model.ShowAllCulture %>">
            <%= Html.CheckBoxFor(model => model.AllCulture) %>
            <%=Html.MytripLabelFor("AllCulture", ArticleLanguage.display_all_lang)%>
        </div>
        <p>
            <%=Html.MytripInput("submit", ArticleLanguage.edit)%>
        </p>
    </fieldset>
    <% } %>
    <div>
        <%=Html.MytripActionLink(Model.Url,ArticleLanguage.back) %>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContentRight" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContentLeft" runat="server">
</asp:Content>
