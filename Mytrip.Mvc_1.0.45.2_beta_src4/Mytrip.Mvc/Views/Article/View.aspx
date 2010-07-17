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
    <script src="/Scripts/SLPlaerChrome.js" type="text/javascript"></script>
    <link href="/Content/jHtmlArea.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        var jHtmlArea_API = new Object();
        $(document).ready(function () {
            Rating();
        var anc='<%=Model.Anchor %>';
        if(anc!=null)
        {
        location.href = '#<%= Model.Anchor %>';
        };
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
function Rating() {
    var sts = new Array;
    $("#votes input[name='vote']").each(function () {
        var style = $(this).attr("style");
        sts.push(style);
    });
    $("#votes input[name='vote']").click(function () {
        if ($("#votes input[name='vote']").attr('onclick') == null) {
            var vote = $(this).val();
            var count = $("#votes input[id='VotesCount']").val();
            var id = $("#Article_ArticleId").val();
            $("#votes").load("/Article/Rate", { id: id, vote: vote, count: count });
        }
    });
    $("#votes input[name='vote']").mouseover(function () {
        var rate = $(this).val();
        $("#votes input[name='vote']").each(function () {
            var star = $(this).val();
            if (rate >= star) {
                $(this).removeAttr("style").removeClass("ratingempty").addClass("ratingfull");
            }
            else {
                $(this).removeAttr("style").removeClass("ratingfull").addClass("ratingempty");
            }
        });
    });
    $("#votes").mouseleave(function () {
        var div = $("#votes");
        $(this).removeClass("ratingempty");
        $(this).removeClass("ratingfull")
        $.each(sts,
         function (ctr, val) {
             var id = ctr + 1;
             div.find("#vote" + id).removeClass("ratingempty").removeClass("ratingfull").attr("style", val);
         });
     });
};
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContentLeft" runat="server">
    <%--<%using (Ajax.BeginForm(new AjaxOptions { UpdateTargetId = "votes"  }))
      {%>--%>
      <div id="votes" class="votes" style="position:relative; float: right; height:25px">
        <%=Html.ArticleRating(Model.Article.ApprovedVotes, true, Model.Article.TotalVotes, Model.VotesCount)%>
        <%=Html.HiddenFor(x=>x.VotesCount) %>
        <%=Html.HiddenFor(x=>x.Article.ArticleId) %>
    </div>
   <%-- <%} %>--%>
    <%=Html.ShowArticle(Model.Article) %>
   <div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content">
   <%=Html.ShowArticleTags(Model.Article) %>
    <%=Html.ArticleSpecification(Model.Article)%>
    </div><div><div class="contentbottomright"></div><div class="contentbottomleft"></div><div class="contentbottomcon"></div></div>
    <div class="acfooter"></div>
    <%if (Model.showRelatedLinks)
      { %>
     <div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content2">
     <%=Html.ShowRelated(Model.Article)%>
     </div><div><div class="contentbottomright"></div><div class="contentbottomleft"></div><div class="contentbottomcon"></div></div>
     <%} %>
    <%=Html.ShowComments(Model.Article) %>
    <% if (Model.Article.ApprovedComment && (User.Identity.IsAuthenticated || Model.Article.IncludeAnonymComment))
       {
           if (!Model.replaceCommentsEmail || Model.Blog)
           {%>
    <h3 class="title"><%=ArticleLanguage.add_comment%></h3>
      <%} %>  
            <% Html.EnableClientValidation(); %>
            <% using (Html.BeginForm())
               {%>
                <div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content2">
        <%=Html.HiddenFor(model=>model.Blog) %>
        <%=Html.HiddenFor(model=>model.Path) %>
        <%=Html.HiddenFor(model=>model.Title) %>
               <%
                   if (!User.Identity.IsAuthenticated && Model.Article.IncludeAnonymComment)
                   {%>
           <table class="input">
            <tr>
                <td>
                        <%=Html.MytripLabelFor("AnonymName", ArticleLanguage.name) %>
                    </td>
            </tr>
            <tr>
                <td>
                        <%= Html.MytripTextBoxFor(model => model.AnonymName)%>
                        </td>
                <td>
                        <%= Html.ValidationMessageFor(model => model.AnonymName)%>
                   </td>
            </tr>
            <tr>
                <td>
                        <%=Html.MytripLabelFor("AnonymEmail", ArticleLanguage.email)%>
                   </td>
            </tr>
            <tr>
                <td>
                        <%= Html.MytripTextBoxFor(model => model.AnonymEmail)%> </td>
                <td>
                        <%= Html.ValidationMessageFor(model => model.AnonymEmail)%>
                 </td>
            </tr> </table>
            <%} %>
            <div class="editor-field">
                <%= Html.TextAreaFor(model => model.Comment, new { id = "abstract", style = "height: 150px; width:99%;" })%>
                <%= Html.ValidationMessageFor(model => model.Comment)%>
            </div>
            <%  if (!User.Identity.IsAuthenticated && Model.Article.IncludeAnonymComment)
                {%>
            <table class="input">
            <tr>
                <td><%= Html.CaptchaImage(202, 60, "Times New Roman")%>
                 </td>
            </tr>
            <tr>
                <td>
                        <%= Html.MytripTextBoxFor(model => model.Captcha)%> </td>
                <td>
                        <%= Html.ValidationMessageFor(model => model.Captcha)%></td>
            </tr> </table>
<%} %><br/>
        <div class="inputbutton">
            <%=Html.MytripInput(ArticleLanguage.create, true)%>
        </div>
            <%} %>
         </div><div><div class="contentbottomright"></div><div class="contentbottomleft"></div><div class="contentbottomcon"></div></div>
     
    <%}%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContentRight" runat="server">
    <%=Html.Partial("SideBar")%>
</asp:Content>
