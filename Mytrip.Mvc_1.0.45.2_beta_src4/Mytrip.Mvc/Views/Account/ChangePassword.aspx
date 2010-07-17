<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Mvc.Models.ChangePasswordModel>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
    <%  //************************************************************ 
        // Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich
        // To learn more about Mytrip.Mvc.Entyty visit 
        // http://starterkitmytripmvc.codeplex.com/
        // mytripmvc@gmail.com
        // license: Microsoft Public License (Ms-PL) 
        // *********************************************************** %>
    <%=Html.PageTitle(CoreLanguage.change_password, "/")%>
</asp:Content>
<asp:Content ContentPlaceHolderID="ScriptContent" runat="server">
    <script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="title">
        <%=CoreLanguage.change_password%></h2>
    <p>
        <%=CoreLanguage.change_password_text_first%>
    </p>
    <p>
        <%=String.Format(CoreLanguage.change_password_text_last, Model.minRequiredPasswordLength)%>
    </p>
    <% Html.EnableClientValidation(); %>
    <% using (Html.BeginForm())
       { %>
    <div>
        <div class="contenttopright">
        </div>
        <div class="contenttopleft">
        </div>
        <div class="contenttopcon">
        </div>
    </div>
    <div class="content">
       <table class="input">
            <tr>
                <td>
            <%= Html.Label(CoreLanguage.OldPassword)%>
        </td>
            </tr>
            <tr>
                <td>
            <%= Html.MytripPasswordFor(m => m.OldPassword) %>
            </td>
                <td>
            <%= Html.ValidationMessageFor(m => m.OldPassword) %>
        </td>
            </tr>
            <tr>
                <td>
            <%= Html.Label(CoreLanguage.NewPassword)%>
        </td>
            </tr>
            <tr>
                <td>
            <%= Html.MytripPasswordFor(m => m.NewPassword) %>
            </td>
                <td>
            <%= Html.ValidationMessageFor(m => m.NewPassword) %>
       </td>
            </tr>
            <tr>
                <td>
            <%= Html.Label(CoreLanguage.ConfirmPassword)%>
       </td>
            </tr>
            <tr>
                <td>
            <%= Html.MytripPasswordFor(m => m.ConfirmPassword) %>
             </td>
                <td>
            <%= Html.ValidationMessageFor(m => m.ConfirmPassword) %>
          </td>
            </tr> </table><br/>
        <div class="inputbutton">
            <%=Html.MytripInput(CoreLanguage.change_password, true)%>
        </div>
    </div>
    <div>
        <div class="contentbottomright">
        </div>
        <div class="contentbottomleft">
        </div>
        <div class="contentbottomcon">
        </div>
    </div>
    <% } %>
</asp:Content>
