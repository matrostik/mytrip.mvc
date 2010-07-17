<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Mvc.Models.EmailModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%//************************************************************ 
  // Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich
  // To learn more about Mytrip.Mvc.Entyty visit 
  // http://starterkitmytripmvc.codeplex.com/
  // mytripmvc@gmail.com
  // license: Microsoft Public License (Ms-PL) 
  // *********************************************************** %>
    <%=Html.PageTitle(CoreLanguage.Email_setting, "/")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="title"><%=CoreLanguage.Email_setting%></h2>
    <% Html.EnableClientValidation(); %>
    <% using (Html.BeginForm()) {%>        
        <div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content2">
            
            <div class="editor-label">
                <%=CoreLanguage.Email%>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.FromEmail) %>
                <%=Html.ValidationMessageFor(model => model.FromEmail) %>
            </div>
            
            <div class="editor-label">
                Smtp
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Smtp) %>
                <%= Html.ValidationMessageFor(model => model.Smtp) %>
            </div>
            
            <div class="editor-label">
                Port
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Port) %>
                <%=Html.ValidationMessageFor(model => model.Port) %>
            </div>
            
            <div class="editor-label">
                Ssl
            </div>
            <div class="editor-field">
                <%= Html.CheckBoxFor(model => model.Ssl) %>
                <%=Html.ValidationMessageFor(model => model.Ssl) %>
            </div>
            
            <div class="editor-label">
                 <%=CoreLanguage.UserName%>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.LoginEmail) %>
                <%= Html.ValidationMessageFor(model => model.LoginEmail) %>
            </div>
            
            <div class="editor-label">
               <%=CoreLanguage.Password%>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.PasswordEmail) %>
                <%= Html.ValidationMessageFor(model => model.PasswordEmail) %>
            </div>
            
            <p>
                <%=Html.MytripInput("submit", CoreLanguage.save)%>
            </p>
 </div><div ><div class="contentbottomright"></div><div class="contentbottomleft"></div><div class="contentbottomcon"></div></div>

    <% } %>
    <div class="acfooter"></div>
   <div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content">

        <%=Html.ActionLink(CoreLanguage.cansel, "Index","Home") %>
     </div><div ><div class="contentbottomright"></div><div class="contentbottomleft"></div><div class="contentbottomcon"></div></div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
<script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContentRight" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="MainContentLeft" runat="server">
</asp:Content>

