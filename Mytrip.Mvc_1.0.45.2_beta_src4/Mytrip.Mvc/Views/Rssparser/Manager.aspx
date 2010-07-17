<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Rssparser.Models.ManagerModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Html.PageTitle(RssparserLanguage.rssparser_manager, "/")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="title"><%=RssparserLanguage.rssparser_manager %></h2>
       <div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content">
    <%=Html.VotesList()%>
    </div><div ><div class="contentbottomright"></div><div class="contentbottomleft"></div><div class="contentbottomcon"></div></div>
    <div class="acfooter"></div>

     <h2 class="title"><%=RssparserLanguage.add_rss_feed %></h2>
    <div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content2">
    
                <% Html.EnableClientValidation(); %>
                <% using (Html.BeginForm())
                   { %>
                <div class="editor-label">
                    <%= Html.MytripLabelFor("Title", RssparserLanguage.rssfeed_title)%><br />
                    <%= Html.TextBoxFor(x=>x.Title,new { style = "width: 350px;"})%>
                    <%=Html.ValidationMessage("Title")%>
                </div>
                <div class="editor-label">
                    <%= Html.MytripLabelFor("RssUrl", RssparserLanguage.rssfeed_url)%><br />
                    <%= Html.TextBoxFor(x => x.RssUrl, new { style = "width: 350px;" })%>
                    <%=Html.ValidationMessage("RssUrl")%>
                </div>
                <div class="editor-label">
                    <%= Html.MytripLabelFor("ImgUrl", RssparserLanguage.rssfeed_pic)%><br />
                    <%= Html.TextAreaFor(x => x.ImgUrl, new { id = "fotoabstract", style = "height: 200px; width:200px;" })%>
                    <%=Html.ValidationMessage("ImgUrl")%>
                </div>
                <div class="editor-label">
                    <%= Html.CheckBoxFor(model => model.AllCulture) %>
                    <%=Html.MytripLabelFor("AllCulture", RssparserLanguage.display_in_all_languages)%>
                </div>
                <%=Html.MytripInput("submit", "Add")%>
                <% } %>
           
    </div><div><div class="contentbottomright"></div><div class="contentbottomleft"></div><div class="contentbottomcon"></div></div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
 <script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script>
    <%// Html Editor %>
    <script src="/Scripts/jHtmlArea-0.7.0.js" type="text/javascript"></script>
    <link href="/Content/jHtmlArea.css" rel="stylesheet" type="text/css" />
    <%=Html.MytripAddTextAreaScript() %>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContentRight" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="MainContentLeft" runat="server">
</asp:Content>
