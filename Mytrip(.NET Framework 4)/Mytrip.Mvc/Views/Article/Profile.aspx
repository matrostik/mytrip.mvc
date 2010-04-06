<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Articles.Models.ProfileModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=Html.PageTitle(Model.Username, "/")%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%=ArticleLanguage.profile %>
        <%=Model.Username %></h2>
    <div style="float: left; width: 180px; margin-right: 10px;">
        <%--border:1px solid black;--%>
        <div style="text-align: center;">
            <%=Html.Avatar(Model.Email, new { title=Model.Username})%></div>
        <%=Html.UserData(Model.Username) %>
    </div>
    <div style="overflow: hidden; padding: 0px 5px 5px 5px;">
        <div style="font-size: large; font-weight: bold; vertical-align: text-top; border-bottom: 4px solid black;
            overflow: hidden; margin-bottom: 20px;">
            <%=ArticleLanguage.recent_activity %></div>
        <table style="border: 0px;">
            <tr>
                <th style="border: 0px; width: 100px">
                    <% using (Html.BeginForm("Profile","Article", FormMethod.Post, new { id = "TheForm" }))
                       {%>
                    <%=Html.DropDownListFor(model => model.Path, Model.Places, new { style = "height: 20px; width:100%;", onchange="this.form.submit();" })%>
                    <%} %>
                </th>
                <th style="border: 0px;">
                    <%=ArticleLanguage.activity %>
                </th>
                <th style="border: 0px; width: 125px">
                    <%=ArticleLanguage.date %>
                </th>
            </tr>
        </table>
        <%=Html.LastActivity(Model.Username,Model.Path) %>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContentLeft" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContentRight" runat="server">
</asp:Content>
