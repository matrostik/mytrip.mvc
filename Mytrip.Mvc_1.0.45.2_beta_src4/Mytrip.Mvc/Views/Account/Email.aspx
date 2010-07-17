<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%//************************************************************ 
  // Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich
  // To learn more about Mytrip.Mvc.Entyty visit 
  // http://starterkitmytripmvc.codeplex.com/
  // mytripmvc@gmail.com
  // license: Microsoft Public License (Ms-PL) 
  // *********************************************************** %>
    <%=Html.PageTitle(CoreLanguage.Email, "/")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="title"><%=CoreLanguage.Email%></h2>
    <div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content">

    <p><%=CoreLanguage.Email_account_body%></p>
    </div><div><div class="contentbottomright"></div><div class="contentbottomleft"></div><div class="contentbottomcon"></div></div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContentRight" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="MainContentLeft" runat="server">
</asp:Content>
