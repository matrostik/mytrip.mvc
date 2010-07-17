<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Mvc.Models.ProfileUsersModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
<%=Html.PageTitle(Model.UserName, "/")%>	
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2 class="title">
        <%=string.Format(CoreLanguage.profile, Model.UserName) %></h2>
       <div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content">
         <table style="border: 0;">
         <tr>
         <td style="width: 180px;border: 0; vertical-align: top;"><div style="text-align: center;">
         <%=Html.Avatar(Model.Email, new { title=Model.UserName})%></div>
         <%=Html.UserData(Model.UserName) %>
         <%=Html.Partial("ProfileSmall")%>
         </td>
         <td style="border: 0;">         
         <%=Html.Partial("ProfileBig")%>         
         </td>       
         
         </tr>
         
         
         
         </table>




          </div><div ><div class="contentbottomright"></div><div class="contentbottomleft"></div><div class="contentbottomcon"></div></div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContentRight" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="MainContentLeft" runat="server">
</asp:Content>

