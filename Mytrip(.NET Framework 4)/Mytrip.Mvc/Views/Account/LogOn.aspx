<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Core.Models.LogOnModel>" %>

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
    <h2><%=CoreLanguage.logon%></h2>
    <p>
        <%=CoreLanguage.logon_text %> <%if (UsersSetting.unlockRegistration)
                                        {%><%=String.Format(CoreLanguage.logon_text1, Html.ActionLink(CoreLanguage.register, "Register"))%><%} %> 
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
                    <%= Html.Label(CoreLanguage.Password)%>
                </div>
                <div class="editor-field">
                    <%= Html.PasswordFor(m => m.Password) %>
                    <%= Html.ValidationMessageFor(m => m.Password) %>
                </div>
                
                <div class="editor-label">
                    <%= Html.CheckBoxFor(m => m.RememberMe) %>
                    <%= Html.Label(CoreLanguage.RememberMe)%>
                </div>
                
                <p>
                   <%=Html.MytripInput("submit", CoreLanguage.logon)%>
                </p>
            </fieldset>
        </div>
    <% } %>
</asp:Content>
