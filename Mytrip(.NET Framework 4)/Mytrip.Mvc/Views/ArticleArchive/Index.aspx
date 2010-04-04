<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Articles.Models.ArchiveIndexModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=Html.PageTitle(ArticleLanguage.articles_manager, "/")%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%=ArticleLanguage.articles_manager%></h2>
    <%=Html.ArchiveStatistic()%>
    <table>
        <tr>
            <td style="border:0px;">
                <h2>
                    <%=ArticleLanguage.latest_updates%></h2>
            </td>
            <td  style="border:0px;">
                <%=Html.CountPager(Model.Count) %>
            </td>
        </tr>
    </table>
    <%=Html.LatestUpdates(Model.Count) %>
    <h2>
       <%=Html.ActionLink(ArticleLanguage.closed_articles,"Details",new {path="ClosedArticles"}) %> </h2>
    <%=Html.ClosedArticles(Model.Count)%>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContentLeft" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContentRight" runat="server">
</asp:Content>
