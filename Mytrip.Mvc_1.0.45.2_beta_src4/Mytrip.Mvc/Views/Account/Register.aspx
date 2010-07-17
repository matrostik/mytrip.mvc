<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Mvc.Models.RegisterModel>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
    <%--************************************************************
Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich
To learn more about Mytrip.Mvc.Entity visit
http://mytripmvc.codeplex.com
http://starterkitmytripmvc.codeplex.com
mytripmvc@gmail.com
license: Microsoft Public License (Ms-PL)
***********************************************************--%>
    <%=Html.PageTitle(CoreLanguage.register, "/")%>
</asp:Content>
<asp:Content ContentPlaceHolderID="ScriptContent" runat="server">
    <script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="title">
        <%=CoreLanguage.create_account%></h2>
    <p>
        <%=CoreLanguage.register_text_first%>
    </p>
    <p>
        <%=String.Format(CoreLanguage.register_text_last, Model.minRequiredPasswordLength)%>
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
    <div class="content2">
        <table class="input">
            <tr>
                <td>
                    <%= Html.MytripLabelFor("UserName", CoreLanguage.UserName)%>
                </td>
            </tr>
            <tr>
                <td>
                    <%= Html.MytripTextBoxFor(m => m.UserName)%>
                </td>
                <td>
                    <%= Html.ValidationMessageFor(m => m.UserName) %>
                </td>
            </tr>
            <tr>
                <td>
                    <%= Html.MytripLabelFor("Email", CoreLanguage.Email)%>
                </td>
            </tr>
            <tr>
                <td>
                    <%= Html.MytripTextBoxFor(m => m.Email)%>
                </td>
                <td>
                    <%= Html.ValidationMessageFor(m => m.Email) %>
                </td>
            </tr>
            <tr>
                <td>
                    <%= Html.MytripLabelFor("Password", CoreLanguage.Password)%>
                </td>
            </tr>
            <tr>
                <td>
                    <%= Html.MytripPasswordFor(m => m.Password)%>
                </td>
                <td>
                    <%= Html.ValidationMessageFor(m => m.Password) %>
                </td>
            </tr>
            <tr>
                <td>
                    <%= Html.MytripLabelFor("ConfirmPassword", CoreLanguage.ConfirmPassword)%>
                </td>
            </tr>
            <tr>
                <td>
                    <%= Html.MytripPasswordFor(m => m.ConfirmPassword)%>
                </td>
                <td>
                    <%= Html.ValidationMessageFor(m => m.ConfirmPassword) %>
                </td>
            </tr>
            <%if (Model.unlockCaptcha)
              { %>
            <tr>
                <td>
                    <%= Html.Label(CoreLanguage.Captcha)%>
                </td>
            </tr>
            <tr>
                <td>
                    <%= Html.CaptchaImage(202, 60, "Times New Roman")%>
                </td>
            </tr>
           <%-- <tr>
                <td>
                    <%= Html.MytripLabelFor("Captcha", CoreLanguage.Captcha)%>
                </td>
            </tr>--%>
            <tr>
                <td>
                    <%= Html.MytripTextBoxFor(m => m.Captcha)%>
                </td>
                <td>
                    <%= Html.ValidationMessageFor(m => m.Captcha)%>
                </td>
            </tr>
       
        <%} %> </table><br/>
        <div class="inputbutton">
            <%=Html.MytripInput(CoreLanguage.register, true)%>
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
