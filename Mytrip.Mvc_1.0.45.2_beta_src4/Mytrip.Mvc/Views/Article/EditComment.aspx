<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Articles.Models.CommentModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=Html.PageTitle(Model.PageTitle, "/")%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="title">
        <%=Model.PageTitle %></h2>
    <% Html.EnableClientValidation(); %>
    <% using (Html.BeginForm())
       {%>
    <div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content2">
        
                <div class="editor-label">
                    <%= Html.HiddenFor(model => model.Path) %>
                    <%= Html.HiddenFor(model => model.Url) %>
                    <%= Html.HiddenFor(model => model.CommentId) %>
                    <%= Html.MytripLabelFor("Comment", ArticleLanguage.content)%>
                </div>
                <div class="editor-field">
                    <%= Html.TextAreaFor(model => model.Comment, new { id = "abstract", style = "height: 250px; width:500px;" })%>
                    <%= Html.ValidationMessageFor(model => model.Comment) %>
                </div>
                <%=Html.MytripInput("submit", ArticleLanguage.edit)%>
          </div><div ><div class="contentbottomright"></div><div class="contentbottomleft"></div><div class="contentbottomcon"></div></div>

    <%
        } %>
        <div class="acfooter"></div>
    <div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content">
         
      
        <%=Html.MytripActionLink(Model.Url,ArticleLanguage.back) %>
    </div><div ><div class="contentbottomright"></div><div class="contentbottomleft"></div><div class="contentbottomcon"></div></div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script>
    <%// Html Editor %>
    <script src="/Scripts/jHtmlArea-0.7.0.js" type="text/javascript"></script>
    <link href="/Content/jHtmlArea.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">        var jHtmlArea_API = new Object();
        $(document).ready(function () {

            $('#abstract').htmlarea({
                toolbar: [['html'], ['bold', 'italic', 'underline', 'strikethrough'], ['increasefontsize', 'decreasefontsize'],
 ['link', 'unlink', {
     css: 'smile', text: 'Smiles', action: function (btn) {
         jHtmlArea_API['#abstract'] = $(this); var url = '/TextAreaFile/Smile/abstract';
         var gallery = window.open(url, 'gallery', 'width=300,height=300,menubar=0,location=0,resizable=0,scrollbars=1,status=0');
         gallery.focus();
     }
 }], ['p', 'h1', 'h2', 'h3', 'h4', 'h5', 'h6']]
            });
        });
</script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContentLeft" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContentRight" runat="server">
</asp:Content>
