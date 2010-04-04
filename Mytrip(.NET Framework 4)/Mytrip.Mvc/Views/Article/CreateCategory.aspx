<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Articles.Models.CategoryModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=Html.PageTitle(Model.PageTitle, "/")%>
</asp:Content>
<asp:Content ContentPlaceHolderID="ScriptContent" runat="server">
    <script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%=Model.PageTitle%></h2>
    <% Html.EnableClientValidation(); %>
    <% using (Html.BeginForm())
       {%>
    <fieldset>
        <legend>Enter information</legend>
        <%=Html.HiddenFor(model=>model.CategoryId) %>
        <%=Html.HiddenFor(model => model.Path)%>
        <div class="editor-label">
            <%= Html.MytripLabelFor("Title", ArticleLanguage.name)%>
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
            <%= Html.CheckBoxFor(model => model.AllCulture)%>
            <%=Html.MytripLabelFor("AllCulture", ArticleLanguage.display_all_lang)%>
        </div>
        <p>
            <%=Html.MytripInput("submit", ArticleLanguage.create)%>
        </p>
    </fieldset>
    <% } %>
    <div>
        <%=Html.ActionLink(ArticleLanguage.back_to_list, "Index", new { pageIndex = 1, pageSize = 10, id = Model.CategoryId, Path = Model.Path })%>
    </div>
</asp:Content>
