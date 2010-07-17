<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Gismeteo.Models.ManagerModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	 <%=Html.PageTitle(GismeteoLanguage.edit_Gismeteo, "/")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="title"><%=GismeteoLanguage.edit_Gismeteo%></h2>
    <div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content2">
    
                <% Html.EnableClientValidation(); %>
                <% using (Html.BeginForm())
                   { %>
                <div class="editor-label">
                    <%= GismeteoLanguage.Title%><br />
                    <%= Html.TextBoxFor(model => model.Title, new { style = "width: 350px;" })%>
                    <%=Html.ValidationMessageFor(model => model.Title)%>
                </div>
                <div class="editor-label">
                    <%= GismeteoLanguage.UrlXml%>   <a href="http://informer.gismeteo.ru/getcode/xml.php" target="_blank">informer.gismeteo.ru</a><br />
                    <%= Html.TextBoxFor(model => model.UrlXml, new { style = "width: 350px;" })%>
                    <%=Html.ValidationMessageFor(model => model.UrlXml)%>
                </div>
                <div class="editor-label">
                    <%= Html.CheckBoxFor(model => model.VisibleInformer) %>
                    <%= GismeteoLanguage.VisibleInformer%>
                </div>
                <div class="editor-label">
                    <%= Html.CheckBoxFor(model => model.AllCulture) %>
                    <%= GismeteoLanguage.AllCulture %>
                </div>
                <%=Html.MytripInput("submit", GismeteoLanguage.edit_Gismeteo)%>
                <% } %>
           
    </div><div><div class="contentbottomright"></div><div class="contentbottomleft"></div><div class="contentbottomcon"></div></div><div class="acfooter"></div>
    <div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content">
        <%=Html.ActionLink(CoreLanguage.back_to_list, "Manager")%>
    </div><div><div class="contentbottomright"></div><div class="contentbottomleft"></div><div class="contentbottomcon"></div></div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
 <script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContentRight" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="MainContentLeft" runat="server">
</asp:Content>
