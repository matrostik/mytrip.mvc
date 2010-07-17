<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Mvc.Models.CoreModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%//************************************************************ 
  // Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich
  // To learn more about Mytrip.Mvc.Entyty visit 
  // http://starterkitmytripmvc.codeplex.com/
  // mytripmvc@gmail.com
  // license: Microsoft Public License (Ms-PL) 
  // *********************************************************** %>
    <%=Html.PageTitle(CoreLanguage.Core_Settings, "/")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="title"><%=CoreLanguage.Core_Settings%></h2>
    <% Html.EnableClientValidation(); %>
    <% using (Html.BeginForm()) {%>
    <div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content2">

    <table style="border:0;padding:0;">
    <tr>
    <td style="width:50%;border:0">
     <fieldset>
            <legend><%=CoreLanguage.Connecting_String_Setting%></legend>
            <div class="editor-label">
                <%= CoreLanguage.Provider%>
            </div>
            <div class="editor-field">
                <%= Html.DropDownListFor(model => model.Provider, Model.AllProvider) %>
                <%= Html.ValidationMessageFor(model => model.Provider)%>
            </div>
            <div class="editor-field">
                <%= Html.CheckBoxFor(model => model.IntegratedSecurity, new { id = "integratedS" })%>
                <%= CoreLanguage.IntegratedSecurity %>
            </div>
            
            <div class="editor-label">
                <%= CoreLanguage.Server %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Server) %>
                <%= Html.ValidationMessageFor(model => model.Server) %>
            </div>
            
            <div class="editor-label">
                <%= CoreLanguage.DataBase %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.DataBase) %>
                <%= Html.ValidationMessageFor(model => model.DataBase) %>
            </div>
           <div id="_integratedS" style="display: none">
            <div class="editor-label">
                <%= CoreLanguage.UserName %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.User) %>
                <%= Html.ValidationMessageFor(model => model.User) %>
            </div>
            
            <div class="editor-label">
                <%= CoreLanguage.Password %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Password)%>
                <%= Html.ValidationMessageFor(model => model.Password) %>
            </div></div>
     </fieldset>
    </td>
    <td style="width:50%;border:0"><fieldset><legend><%=CoreLanguage.Membership_Settings %></legend>
    <div class="editor-label">
                <%= CoreLanguage.minRequiredPasswordLength %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.minRequiredPasswordLength) %>
                <%= Html.ValidationMessageFor(model => model.minRequiredPasswordLength) %>
            </div>
            
            <div class="editor-label">
                <%= CoreLanguage.maxInvalidPasswordAttempts %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.maxInvalidPasswordAttempts) %>
                <%= Html.ValidationMessageFor(model => model.maxInvalidPasswordAttempts) %>
            </div>
            <div class="editor-field">
                <%= Html.CheckBoxFor(model => model.requiresUniqueEmail) %>
                <%= CoreLanguage.requiresUniqueEmail %>
            </div>
            <div class="editor-field">
                <%= Html.CheckBoxFor(model => model.unlockCaptcha) %>
                <%= CoreLanguage.unlockCaptcha %>
            </div>
            <div class="editor-field">
                <%= Html.CheckBoxFor(model => model.unlockApprovedEmail)%>
                <%= CoreLanguage.unlockApprovedEmail %>
            </div>
            
            <div class="editor-label">
                <%= CoreLanguage.roleAdmin %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.roleAdmin) %>
                <%= Html.ValidationMessageFor(model => model.roleAdmin) %>
            </div>
            <div class="editor-label">
                <%= CoreLanguage.roleChiefEditor%>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.roleChiefEditor) %>
                <%= Html.ValidationMessageFor(model => model.roleChiefEditor) %>
            </div>
            <div class="editor-field">
                <%= Html.CheckBoxFor(model => model.unlockRegistration) %>
                <%= CoreLanguage.unlockRegistration %>
            </div>
            <div class="editor-field">
                <%= Html.CheckBoxFor(model => model.unlockVisibleLogon) %>
                <%= CoreLanguage.unlockVisibleLogon %>
            </div>
    
    
    </fieldset></td>
    </tr>
    <tr><td style="width:50%;border:0"><fieldset><legend><%=CoreLanguage.Profile_Settings %></legend>
    <div class="editor-label">
                <%= CoreLanguage.nameTitle%>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.nameTitle) %>
                <%= Html.ValidationMessageFor(model => model.nameTitle) %>
            </div>
    <div class="editor-label">
                <%= CoreLanguage.Homepage %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.nameHome) %>
                <%= Html.ValidationMessageFor(model => model.nameHome) %>
            </div>
            <div class="editor-label">
                <%= CoreLanguage.Aboutpage %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.nameAbout) %>
                <%= Html.ValidationMessageFor(model => model.nameAbout) %>
            </div>
            <div class="editor-field">
                <%= Html.CheckBoxFor(model => model.unlockGravatar) %>
                <%= CoreLanguage.unlockGravatar %>
            </div>
    
    </fieldset></td>
    <td style="width:50%;border:0"><fieldset><legend><%=CoreLanguage.Localization_Settings %></legend>
    <div class="editor-label">
                <%= CoreLanguage.defaultCulture %>
            </div>
            <div class="editor-field">
                <%= Html.DropDownListFor(model => model.defaultCulture, Model.AllCulture)%>
            </div>
            <div class="editor-field">
                <%= Html.CheckBoxFor(model => model.unlockAllCulture) %>
                <%= CoreLanguage.unlockAllCulture %>
            </div>
    </fieldset></td>
    </tr>
    <tr><td style="width:50%;border:0"><fieldset><legend><%=CoreLanguage.Theme_Settings%></legend>
    <div class="editor-label">
                <%= CoreLanguage.defaultTheme %>
            </div>
            <div class="editor-field">
                <%= Html.DropDownListFor(model => model.defaultTheme,Model.AllTheme) %>
            </div>
            <div class="editor-field">
                <%= Html.CheckBoxFor(model => model.unlockTheme) %>
                <%= CoreLanguage.unlockTheme %>
            </div>
    
    </fieldset></td></tr>
    </table>
     <p>
                <%=Html.MytripInput("submit", CoreLanguage.save)%>
            </p>
 </div><div><div class="contentbottomright"></div><div class="contentbottomleft"></div><div class="contentbottomcon"></div></div>

    <% } %>
    <div class="acfooter"></div>
    <div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content">

        <%=Html.ActionLink(CoreLanguage.cansel, "Index","Home") %>
    </div><div><div class="contentbottomright"></div><div class="contentbottomleft"></div><div class="contentbottomcon"></div></div>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
<script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script>
    <%if(!Model.IntegratedSecurity){ %>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#_integratedS").show();
        });
    </script>
    <%} %>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#integratedS").click(function () {
                $("#_integratedS").slideToggle(300);
            });
        });
    </script>
</asp:Content>

