<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Core.Models.ChangePasswordModel>" %>
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

    <h2><%=CoreLanguage.change_password%></h2>
    <p>
        <%=CoreLanguage.change_password_text_first%>
    </p>
    <p>
        <%=String.Format(CoreLanguage.change_password_text_last, UsersSetting.minRequiredPasswordLength)%>
    </p>
    <% Html.EnableClientValidation(); %>
    <% using (Html.BeginForm()) { %>
        <div>
            <fieldset>
                <legend><%=CoreLanguage.account_information%></legend>
                
                <div class="editor-label">
                    <%= Html.Label(CoreLanguage.OldPassword)%>
                </div>
                <div class="editor-field">
                    <%= Html.PasswordFor(m => m.OldPassword) %>
                    <%= Html.ValidationMessageFor(m => m.OldPassword) %>
                </div>
                
                <div class="editor-label">
                    <%= Html.Label(CoreLanguage.NewPassword)%>
                </div>
                <div class="editor-field">
                    <%= Html.PasswordFor(m => m.NewPassword) %>
                    <%= Html.ValidationMessageFor(m => m.NewPassword) %>
                </div>
                
                <div class="editor-label">
                    <%= Html.Label(CoreLanguage.ConfirmPassword)%>
                </div>
                <div class="editor-field">
                    <%= Html.PasswordFor(m => m.ConfirmPassword) %>
                    <%= Html.ValidationMessageFor(m => m.ConfirmPassword) %>
                </div>
                
                <p>
                    <%=Html.MytripInput("submit", CoreLanguage.change_password)%>
                </p>
            </fieldset>
        </div>
    <% } %>
</asp:Content>
