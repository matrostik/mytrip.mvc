<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Core.Models.RegisterModel>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
<%  //************************************************************ 
    // Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich
    // To learn more about Mytrip.Mvc.Entyty visit 
    // http://starterkitmytripmvc.codeplex.com/
    // mytripmvc@gmail.com
    // license: Microsoft Public License (Ms-PL) 
    // *********************************************************** %>
    <%=Html.PageTitle(CoreLanguage.register, "/")%>
</asp:Content>
<asp:Content  ContentPlaceHolderID="ScriptContent" runat="server">
<script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script>
<script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2><%=CoreLanguage.create_account%></h2>
    <p>
        <%=CoreLanguage.register_text_first%>
    </p>
    <p>
        <%=String.Format(CoreLanguage.register_text_last, UsersSetting.minRequiredPasswordLength)%>
    </p>
    <% Html.EnableClientValidation(); %>
    <% using (Html.BeginForm()) { %>
        <div>
            <fieldset>
                <legend><%=CoreLanguage.account_information%></legend>
                
                <div class="editor-label">
                    <%= Html.Label(CoreLanguage.UserName)%>
                </div>
                <div class="editor-field">
                    <%= Html.TextBoxFor(m => m.UserName) %>
                    <%= Html.ValidationMessageFor(m => m.UserName) %>
                </div>
                
                <div class="editor-label">
                    <%= Html.Label(CoreLanguage.Email)%>
                </div>
                <div class="editor-field">
                    <%= Html.TextBoxFor(m => m.Email) %>
                    <%= Html.ValidationMessageFor(m => m.Email) %>
                </div>
                
                <div class="editor-label">
                    <%= Html.Label(CoreLanguage.Password)%>
                </div>
                <div class="editor-field">
                    <%= Html.PasswordFor(m => m.Password) %>
                    <%= Html.ValidationMessageFor(m => m.Password) %>
                </div>
                
                <div class="editor-label">
                    <%= Html.Label(CoreLanguage.ConfirmPassword)%>
                </div>
                <div class="editor-field">
                    <%= Html.PasswordFor(m => m.ConfirmPassword) %>
                    <%= Html.ValidationMessageFor(m => m.ConfirmPassword) %>
                    </div>
                    <%if (UsersSetting.unlockCaptcha)
                      { %>
                      <div class="editor-label">
                 <%= Html.Label(CoreLanguage.Captcha)%>
                 </div>
                <div class="editor-label">
                  <%= Html.CaptchaImage(202, 60, "Times New Roman")%>
                </div>
                 <div class="editor-label">
                 <%= Html.Label(CoreLanguage.Captcha)%>
                 </div>
                <div class="editor-field">
                    <%= Html.TextBoxFor(m => m.Captcha)%>
                    <%= Html.ValidationMessageFor(m => m.Captcha)%>
                </div>
                <%} %>
                <p>
                    <%=Html.MytripInput("submit", CoreLanguage.register)%>
                </p>
            </fieldset>
        </div>
    <% } %>
</asp:Content>
