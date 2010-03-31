<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Start.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Core.Models.CoreModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Core Settings
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Core Settings</h2>

    <% using (Html.BeginForm()) {%>
    <table>
    <tr>
    <td style="width:50%">
     <fieldset>
            <legend>SQL Settings</legend>
            <div class="editor-field">
                <%= Html.CheckBoxFor(model => model.Development) %>
                <%= Html.LabelFor(model => model.Development) %>
            </div>
            <div class="editor-field">
                <%= Html.CheckBoxFor(model => model.MSSQLIntegratedSecurity) %>
                <%= Html.LabelFor(model => model.MSSQLIntegratedSecurity) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.MSSQLServer) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.MSSQLServer) %>
                <%= Html.ValidationMessageFor(model => model.MSSQLServer) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.MSSQLDataBase) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.MSSQLDataBase) %>
                <%= Html.ValidationMessageFor(model => model.MSSQLDataBase) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.MSSQLUser) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.MSSQLUser) %>
                <%= Html.ValidationMessageFor(model => model.MSSQLUser) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.MSSQLPassword) %>
            </div>
            <div class="editor-field">
                <%= Html.PasswordFor(model => model.MSSQLPassword) %>
                <%= Html.ValidationMessageFor(model => model.MSSQLPassword) %>
            </div>
     </fieldset>
    </td>
    <td><fieldset><legend>Membership Settings</legend>
    <div class="editor-label">
                <%= Html.LabelFor(model => model.minRequiredPasswordLength) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.minRequiredPasswordLength) %>
                <%= Html.ValidationMessageFor(model => model.minRequiredPasswordLength) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.maxInvalidPasswordAttempts) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.maxInvalidPasswordAttempts) %>
                <%= Html.ValidationMessageFor(model => model.maxInvalidPasswordAttempts) %>
            </div>
            <div class="editor-field">
                <%= Html.CheckBoxFor(model => model.requiresUniqueEmail) %>
                <%= Html.LabelFor(model => model.requiresUniqueEmail) %>
            </div>
            <div class="editor-field">
                <%= Html.CheckBoxFor(model => model.unlockCaptcha) %>
                <%= Html.LabelFor(model => model.unlockCaptcha) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.roleAdmin) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.roleAdmin) %>
                <%= Html.ValidationMessageFor(model => model.roleAdmin) %>
            </div>
            <div class="editor-field">
                <%= Html.CheckBoxFor(model => model.unlockRegistration) %>
                <%= Html.LabelFor(model => model.unlockRegistration) %>
            </div>
            <div class="editor-field">
                <%= Html.CheckBoxFor(model => model.unlockVisibleLogon) %>
                <%= Html.LabelFor(model => model.unlockVisibleLogon) %>
            </div>
    
    
    </fieldset></td>
    </tr>
    <tr><td><fieldset><legend>Profile Settings</legend>
    
            <div class="editor-field">
                <%= Html.CheckBoxFor(model => model.unlockGravatar) %>
                <%= Html.LabelFor(model => model.unlockGravatar) %>
            </div>
    
    </fieldset></td>
    <td><fieldset><legend>Localization Settings</legend>
    <div class="editor-label">
                <%= Html.LabelFor(model => model.defaultCulture) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.defaultCulture) %>
                <%= Html.ValidationMessageFor(model => model.defaultCulture) %>
            </div>
            <div class="editor-field">
                <%= Html.CheckBoxFor(model => model.unlockAllCulture) %>
                <%= Html.LabelFor(model => model.unlockAllCulture) %>
            </div>
    </fieldset></td>
    </tr>
    <tr><td><fieldset><legend>Theme Settings</legend>
    <div class="editor-label">
                <%= Html.LabelFor(model => model.defaultTheme) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.defaultTheme) %>
                <%= Html.ValidationMessageFor(model => model.defaultTheme) %>
            </div>
            <div class="editor-field">
                <%= Html.CheckBoxFor(model => model.unlockTheme) %>
                <%= Html.LabelFor(model => model.unlockTheme) %>
            </div>
    
    </fieldset></td></tr>
    </table>
            
           
            
            
            
            
            
            
            
            
            <p>
                <input type="submit" value="Save" />
            </p>

    <% } %>

    <div>
        <%=Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

