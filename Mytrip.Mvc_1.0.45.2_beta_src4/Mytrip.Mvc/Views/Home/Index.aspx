<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Mvc.Models.HomeModel>" %>
<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
<%--************************************************************
Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich
To learn more about Mytrip.Mvc.Entity visit
http://mytripmvc.codeplex.com
http://starterkitmytripmvc.codeplex.com
mytripmvc@gmail.com
license: Microsoft Public License (Ms-PL)
***********************************************************--%>
<%=Html.PageTitle(Model.title, "/")%>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContentLeft" runat="server">
<%--<div class="itemlogon"><div>
<div class="logontopright"></div>
<div class="logontopleft"></div>
<div class="logontopcon"></div></div>
<div class="logoncontent">
<input id="save" type="submit" name="option" value="ArticleLanguage" 
class="button" />
</div><div><div class="logonbottomright"></div>
<div class="logonbottomleft"></div>
<div class="logonbottomcon"></div></div></div>--%>





<%if (!Model.developer)
  { %>
<%=Html.Partial("HomePage")%>
<%} %>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContentRight" runat="server">
<%if (!Model.developer)
  { %>
<%=Html.Partial("SideBar")%>
<%} %>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<%if (Model.developer)
  { %>
<%=Html.Partial("License")%>
<%} %>
</asp:Content>