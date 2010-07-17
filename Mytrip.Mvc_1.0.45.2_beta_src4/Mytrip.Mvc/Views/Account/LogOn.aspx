<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Mvc.Models.LogOnModel>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
    <%//************************************************************ 
        // Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich
        // To learn more about Mytrip.Mvc.Entyty visit 
        // http://starterkitmytripmvc.codeplex.com/
        // mytripmvc@gmail.com
        // license: Microsoft Public License (Ms-PL) 
        // *********************************************************** %>
    <%=Html.PageTitle(CoreLanguage.logon, "/")%>
</asp:Content>
<asp:Content ContentPlaceHolderID="ScriptContent" runat="server">
    <script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="title">
        <%=CoreLanguage.logon%></h2>
    <p>
        <%=CoreLanguage.logon_text %>
        <%if (Model.unlockRegistration)
          {%><%=String.Format(CoreLanguage.logon_text1, Html.ActionLink(CoreLanguage.register, "Register", new { returnUrl=Request.QueryString["returnUrl"] }))%><%} %>
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
                    <%= Html.MytripLabelFor("UserName",CoreLanguage.UserName)%>
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
                    <%= Html.MytripLabelFor("Password", CoreLanguage.Password)%>
                </td>
            </tr>
            <tr>
                <td>
                    <%= Html.MytripPasswordFor(m => m.Password) %>
                </td>
                <td>
                    <%= Html.ValidationMessageFor(m => m.Password) %>
                </td>
            </tr>
            <tr>
                <td>
                    <%= Html.CheckBoxFor(m => m.RememberMe) %>
                    <%= Html.MytripLabelFor("RememberMe", CoreLanguage.RememberMe)%>
                </td>
            </tr>
        </table><br />
        <div class="inputbutton">
            <%=Html.MytripInput(CoreLanguage.logon,true)%>
            <%if (Model.unlockRegistration)
              {%><%=Html.MytripInput("/Account/Register?ReturnUrl=" + Request.QueryString["returnUrl"], CoreLanguage.register, false)%><%} %>
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
