<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Articles.Models.CategoryModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=Html.PageTitle(Model.PageTitle, "/")%>
</asp:Content>
<asp:Content ContentPlaceHolderID="ScriptContent" runat="server">
    <script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="title">
        <%=Model.PageTitle%></h2>
    <% Html.EnableClientValidation(); %>
    <% using (Html.BeginForm())
       {%>
   <div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content2">
         
         
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
    </div><div ><div class="contentbottomright"></div><div class="contentbottomleft"></div><div class="contentbottomcon"></div></div>

    <% } %>
    <div class="acfooter"></div>
     <div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content">
         
         
        <%=Html.ActionLink(ArticleLanguage.back_to_list, "Index", new { id = 1, id2 = 10, id3 = Model.CategoryId, id4 = Model.Path })%>
   </div><div ><div class="contentbottomright"></div><div class="contentbottomleft"></div><div class="contentbottomcon"></div></div>

</asp:Content>
