<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Gismeteo.Models.GismeteoSettingModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
		<%--************************************************************
Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich
To learn more about Mytrip.Mvc.Entity visit
http://mytripmvc.codeplex.com
http://starterkitmytripmvc.codeplex.com
mytripmvc@gmail.com
license: Microsoft Public License (Ms-PL)
***********************************************************--%>
<%=Html.PageTitle(GismeteoLanguage.Gismeteosetting, "/")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="title"><%=GismeteoLanguage.Gismeteosetting%></h2>
    <% Html.EnableClientValidation(); %>

    <% using (Html.BeginForm()) {%>
        <%= Html.ValidationSummary(true) %>
        
        <div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content2">
         
        
            <div class="editor-field">
                <%= Html.CheckBoxFor(model => model.unlockGismeteo)%>
                <%= GismeteoLanguage.unlockGismeteo%>
            </div>
            
            <div class="editor-label">
                <%= GismeteoLanguage.nameGismeteo%>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.nameGismeteo)%>
                <%= Html.ValidationMessageFor(model => model.nameGismeteo)%>
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
