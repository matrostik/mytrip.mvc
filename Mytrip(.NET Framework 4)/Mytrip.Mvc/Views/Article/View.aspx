<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Articles.Models.ArticleViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=Html.PageTitle(Model.Article.Title, "/")%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script>
    <script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script>
    <%// Html Editor %>
    <script src="/Scripts/jHtmlArea-0.7.0.js" type="text/javascript"></script>
    <link href="/Content/jHtmlArea.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        var jHtmlArea_API = new Object();
        $(document).ready(function () {
            $('#abstract').htmlarea({
                toolbar: [['html'], ['bold', 'italic', 'underline', 'strikethrough'], ['increasefontsize', 'decreasefontsize'],
 ['link', 'unlink', {
     css: 'smile', text: 'Smiles', action: function (btn) {
         jHtmlArea_API['#abstract'] = $(this); var url = '/ArticleFile/Smile/abstract';
         var gallery = window.open(url, 'gallery', 'width=300,height=300,menubar=0,location=0,resizable=0,scrollbars=1,status=0');
         gallery.focus();
     }
 }], ['p', 'h1', 'h2', 'h3', 'h4', 'h5', 'h6']]
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContentLeft" runat="server">
    <%using (Ajax.BeginForm(new AjaxOptions { UpdateTargetId = "votes" }))
      {
          string input0 = "1"; string input1 = "2"; string input2 = "3"; string input3 = "4"; string input4 = "5";
    %><div id="votes" class="votes" style="position: relative; float: right">
        <%=Html.ArticleRating(Model.Article, true)%>
    </div>
    <%} %>
    <%=Html.ShowArticle(Model.Article) %>
    <br />
    <%=Html.ShowArticleTags(Model.Article) %>
    <br />
    <%=Html.ArticleSpecification(Model.Article)%>
    <br />
    <br />
    <%=Html.ShowComments(Model.Article,Model.Comments) %>
    <% if (Model.Article.ApprovedComment && (User.Identity.IsAuthenticated || Model.Article.IncludeAnonymComment))
       {%>
    <div class="accordion2">
        <div class="accordiontitle2">
            <b>
                <%=ArticleLanguage.add_comment %></b>
        </div>
        <div class="accordioncontent2" style="border: 1px solid #ccc">
            <% Html.EnableClientValidation(); %>
            <% using (Html.BeginForm())
               {
                   if (!User.Identity.IsAuthenticated && Model.Article.IncludeAnonymComment)
                   {%>
            <table>
                <tr>
                    <td style="width: 100px">
                        <%=Html.MytripLabelFor("AnonymName", ArticleLanguage.name) %>
                    </td>
                    <td>
                        <%= Html.TextBoxFor(model => model.AnonymName)%>
                        <%= Html.ValidationMessageFor(model => model.AnonymName)%>
                    </td>
                </tr>
                <tr>
                    <td>
                        <%=Html.MytripLabelFor("AnonymEmail", ArticleLanguage.email)%>
                    </td>
                    <td>
                        <%= Html.TextBoxFor(model => model.AnonymEmail)%>
                        <%= Html.ValidationMessageFor(model => model.AnonymEmail)%>
                    </td>
                </tr>
            </table>
            <%} %>
            <div class="editor-field">
                <%= Html.TextAreaFor(model => model.Comment, new { id = "abstract", style = "height: 150px; width:90%;" })%>
                <%= Html.ValidationMessageFor(model => model.Comment)%>
            </div>
            <%=Html.MytripInput("submit", ArticleLanguage.create)%>
            <%} %>
        </div>
    </div>
    <%}%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContentRight" runat="server">
    <%=Html.Partial("SmallColumn")%>
</asp:Content>
