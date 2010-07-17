<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Articles.Models.ArticleModel>" %>

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
            <%= Html.HiddenFor(model => model.ArticleId) %>
            <%= Html.HiddenFor(model => model.Path) %>
            <%= Html.HiddenFor(model => model.Url) %>
        </div>
        <div class="editor-label">
            <%= Html.MytripLabelFor("Title", ArticleLanguage.title)%>
        </div>
        <div class="editor-field">
            <%= Html.TextBoxFor(model => model.Title) %>
            <%= Html.ValidationMessageFor(model => model.Title) %>
        </div>
        <div class="editor-label">
            <%= Html.MytripLabelFor("CategoryId", ArticleLanguage.category)%>
        </div>
        <div class="editor-field">
            <%= Html.DropDownListFor(model => model.CategoryId, Model.Categories)%>
        </div>
        <table>
            <tr>
                <td style="width: 200px">
                    <div class="editor-label">
                        <%= Html.MytripLabelFor("ImageForAbstract", ArticleLanguage.image_for_abstract)%>
                    </div>
                    <div class="editor-field">
                        <%= Html.TextAreaFor(model => model.ImageForAbstract, new { id = "fotoabstract", style = "height: 200px; width:200px;" })%>
                        <%= Html.ValidationMessageFor(model => model.ImageForAbstract) %>
                    </div>
                </td>
                <td>
                    <div class="editor-label">
                        <%= Html.MytripLabelFor("Abstract", ArticleLanguage.short_description)%>
                    </div>
                    <div class="editor-field">
                        <%= Html.TextAreaFor(model => model.Abstract, new { id = "abstract", style = "height: 200px; width:100%;" })%>
                        <%= Html.ValidationMessageFor(model => model.Abstract) %>
                    </div>
                </td>
            </tr>
        </table>
        <br />
        <table>
            <tr>
                <td>
                    <div class="editor-label">
                        <%= Html.MytripLabelFor("Body", ArticleLanguage.content)%>
                    </div>
                    <div class="editor-field">
                        <%= Html.TextAreaFor(model => model.Body, new { id = "article", style = "height: 400px; width:100%;" })%>
                        <%= Html.ValidationMessageFor(model => model.Body) %>
                    </div>
                </td>
            </tr>
        </table>
        <br />
        <table>
            <tr>
                <td>
                    <div class="editor-label">
                        <%= Html.MytripLabelFor("Tags", ArticleLanguage.add_tags)%>
                    </div>
                    <div class="editor-field">
                        <% foreach (var tag in Model.Tags)
                           {%>
                        <%if (tag.mytrip_articles.FirstOrDefault(x => x.ArticleId == Model.ArticleId) != null)
                          {%>
                        <%=Html.CheckBox("tag"+tag.TagId,true)%>
                        <%}
                          else
                          {%>
                        <%=Html.CheckBox("tag"+tag.TagId)%>
                        <%}%>
                        <%=Html.MytripLabelFor("tag"+tag.TagId,tag.TagName) %>
                        <%}%><br />
                        <%= Html.MytripLabelFor("NewTags", ArticleLanguage.enter_new_tags)%><br />
                        <%=Html.TextBoxFor(model=>model.NewTags) %>
                    </div>
                </td>
            </tr>
        </table>
        <br />
         <table>
            <tr>
                <td>
                    <div class="editor-label" style="display: <%=Model.ShowArticleOptions %>">
                        <%= Html.MytripLabelFor("CloseDate",ArticleLanguage.close_date) %>
                        <%= Html.ValidationMessageFor(model => model.CloseDate) %><br />
                        <%= Html.TextBoxFor(model => model.CloseDate) %>
                    </div>
                    <div class="editor-label" style="display: <%=Model.ShowOnlyForRegisted %>">
                        <%= Html.CheckBoxFor(model => model.OnlyForRegisterUser, new { id = "OnlyForRegisterUser" })%>
                        <%=Html.MytripLabelFor("OnlyForRegisterUser", ArticleLanguage.only_for_register)%>
                    </div>
                    <div class="editor-label" style="display: <%=Model.ShowArticleOptions %>">
                        <%= Html.CheckBoxFor(model => model.ApprovedComment) %>
                        <%=Html.MytripLabelFor("ApprovedComment", ArticleLanguage.enable_comments)%>
                    </div>
                    
                    <div id="showAnonymComment" class="editor-label" style="display: <%=Model.ShowIncludeAnonymComment %>">
                        <%= Html.CheckBoxFor(model => model.IncludeAnonymComment) %>
                        <%=Html.MytripLabelFor("IncludeAnonymComment", ArticleLanguage.enable_comments_for_anonymous)%>
                        <%--<label for="IncludeAnonymComment" style="display: <%=Model.ShowArticleOptions %>"><%=ArticleLanguage.enable_comments_for_anonymous1 %></label>--%>
                    </div>
                    <div class="editor-label" style="display: <%=Model.ShowArticleOptions %>">
                        <%= Html.CheckBoxFor(model => model.ApprovedVotes) %>
                        <%=Html.MytripLabelFor("ApprovedVotes", ArticleLanguage.enable_voting)%>
                    </div>
                    <div class="editor-label" style="display: <%=Model.ShowArticleOptions %>">
                        <%= Html.CheckBoxFor(model => model.AllCulture) %>
                        <%=Html.MytripLabelFor("AllCulture", ArticleLanguage.display_all_lang_w_note)%>
                    </div>
                </td>
            </tr>
        </table>
        <br />
        <p>
            <%=Html.MytripInput("submit", ArticleLanguage.edit)%>
        </p>
    </div><div ><div class="contentbottomright"></div><div class="contentbottomleft"></div><div class="contentbottomcon"></div></div>

    <% } %>
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
    <%=Html.MytripAddTextAreaScript() %>
    <link href="/Content/calendar/ui.datepicker.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/ui.datepicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#ApprovedComment").click(function () {
                var comchecked = $(this).is(':checked');
                if (comchecked) {
                    if (!$("#OnlyForRegisterUser").is(':checked')) {
                        $("#showAnonymComment").show();
                    }
                }
                else {
                    $("#IncludeAnonymComment").attr('checked', false);
                    $("#showAnonymComment").hide();
                }
            });
            $("#OnlyForRegisterUser").click(function () {
                var regchecked = $(this).is(':checked');
                if (regchecked) {
                    $("#IncludeAnonymComment").attr('checked', false);
                    $("#showAnonymComment").hide();
                }
                else {
                    if ($("#ApprovedComment").is(':checked')) {
                        $("#showAnonymComment").show();
                    }
                }
            });
        });
        $(function () {
            $('#CloseDate').datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: 'yy-mm-dd'
            });
        });
    </script>
    <%if(!Model.OnlyForRegisterUser){ %>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#_OnlyForRegisterUser").show();
        });
    </script>
    <%} %>
     <script type="text/javascript">
         $(document).ready(function () {
             $("#OnlyForRegisterUser").click(function () {
                 $("#_OnlyForRegisterUser").slideToggle(300);
             });

         });
    </script>
</asp:Content>
