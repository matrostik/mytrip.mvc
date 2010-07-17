<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Rssparser.Models.RssparserSettingModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
		<%--************************************************************
Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich
To learn more about Mytrip.Mvc.Entity visit
http://mytripmvc.codeplex.com
http://starterkitmytripmvc.codeplex.com
mytripmvc@gmail.com
license: Microsoft Public License (Ms-PL)
***********************************************************--%>
<%=Html.PageTitle(RssparserLanguage.Rssparsersetting, "/")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="title"><%=RssparserLanguage.Rssparsersetting%></h2>
    <% Html.EnableClientValidation(); %>

    <% using (Html.BeginForm()) {%>
        <%= Html.ValidationSummary(true) %>
        
        <div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content2">
         
        
            <div class="editor-field">
                <%= Html.CheckBoxFor(model => model.unlockRssparser) %>
                <%= RssparserLanguage.unlockRssparser%>
            </div>
            
            <div class="editor-label">
                <%= RssparserLanguage.nameRssparser%>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.nameRssparser) %>
                <%= Html.ValidationMessageFor(model => model.nameRssparser) %>
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

