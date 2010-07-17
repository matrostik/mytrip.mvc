<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Votes.Models.VotesModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=Html.PageTitle(VotesLanguage.votes_manager, "/")%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="title">
        <%=VotesLanguage.votes_manager %></h2>

        <div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content">

    <%=Html.VotesList() %>
    </div><div ><div class="contentbottomright"></div><div class="contentbottomleft"></div><div class="contentbottomcon"></div></div>

    <div class="acfooter"></div>
    <h2 class="title"><%=VotesLanguage.add_new_vote %></h2>
    <div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content2">

    
                <% Html.EnableClientValidation(); %>
                <% using (Html.BeginForm())
                   { %>
                <div class="editor-label">
                    <%= Html.MytripLabelFor("Question", VotesLanguage.question_max_lenght)%><br />
                    <%= Html.TextAreaFor(x=>x.Question,new { style = "width: 350px;height:50px"})%>
                    <%=Html.ValidationMessage("Question")%>
                </div>
                <div class="editor-label">
                    <%=VotesLanguage.choose_number_of_answers %>
                    <%=Html.DropDownListFor(model => model.Count, Model.QtAnswers)%>
                </div>
                <% int ctr = 0;
                       foreach (var answer in Model.Answers){%>
                <div class="editor-label">
                    <%= Html.MytripLabelFor("Answers_"+ctr+"_", VotesLanguage.answer+" "+ (ctr+1))%><br />
                    <%= Html.TextBoxFor(x=>x.Answers[ctr],new { style = "width: 350px;"})%>
                    <%=Html.ValidationMessage("Answers_"+ctr+"_") %>
                </div>
                <% ctr++;
                  } %>
                <div class="editor-label">
                    <%= Html.CheckBoxFor(model => model.AllCulture) %>
                    <%=Html.MytripLabelFor("AllCulture", VotesLanguage.display_in_all_languages)%>
                </div>
                <div class="editor-label">
                        <%= Html.CheckBoxFor(model => model.OnlyForRegisterUser) %>
                        <%=Html.MytripLabelFor("OnlyForRegisterUser", VotesLanguage.only_for_registered_users)%>
                    </div>
                    <div class="editor-label">
                        <%= Html.CheckBoxFor(model => model.Active) %>
                        <%=Html.MytripLabelFor("Active", VotesLanguage.make_active)%>
                    </div>
                <div class="editor-label">
                    <%= Html.MytripLabelFor("CloseDate",VotesLanguage.close_date) %>
                    <%= Html.ValidationMessageFor(model => model.CloseDate) %><br />
                    <%= Html.TextBoxFor(model => model.CloseDate) %>
                </div>
                <%=Html.MytripInput("submit",VotesLanguage.add_new_vote)%>
                <% } %>
           
    </div><div><div class="contentbottomright"></div><div class="contentbottomleft"></div><div class="contentbottomcon"></div></div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script>
    <link href="/Content/calendar/ui.datepicker.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/ui.datepicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.dropdown dt a').attr('style', 'width: 30px;margin: 0 0 0 60px;height: 22px;padding:4px 0 0 10px;');
            $('.dropdown dd ul').attr('style', 'min-width: 35px;margin: 0 0 0 60px;text-align:center;');
            var st = $('.dropdown dt a').find('.value').html();
            $('.dropdown dd ul li a').click(function () {
                var selected = $(this).find('.value').html();
                if (st != selected) {
                    $("form").submit();
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
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContentLeft" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContentRight" runat="server">
</asp:Content>
