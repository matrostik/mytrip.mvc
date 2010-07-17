<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Mvc.Models.AboutModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
<%  //************************************************************ 
    // Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich
    // To learn more about Mytrip.Mvc.Entyty visit 
    // http://starterkitmytripmvc.codeplex.com/
    // mytripmvc@gmail.com
    // license: Microsoft Public License (Ms-PL) 
    // *********************************************************** %>
   <%=Html.PageTitle(Model.title, "/")%>
</asp:Content>
 
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentLeft" runat="server">

    <h2 class="title"><%=Model.title%></h2>
    <div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content2">

    
       <%= Model.body %>
        
          </div><div><div class="contentbottomright"></div><div class="contentbottomleft"></div><div class="contentbottomcon"></div></div>

<%if (Model.approvedemail){%>
<%Html.EnableClientValidation();%>
   <% using (Html.BeginForm()) {%>
   <div class="acfooter"></div> 
         <div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content2">

         <table class="input">
            <tr>
                <td>
                <%= CoreLanguage.UserName %>
            </td>
            </tr>
            <tr>
                <td>
                <%= Html.MytripTextBoxFor(model => model.name) %></td>
                <td>
                <%= Html.ValidationMessageFor(model => model.name) %>
           </td>
            </tr>
            <tr>
                <td>
                <%= CoreLanguage.Email%>
            </td>
            </tr>
            <tr>
                <td>
                <%= Html.MytripTextBoxFor(model => model.email) %></td>
                <td>
                <%= Html.ValidationMessageFor(model => model.email) %>
            </td>
            </tr>
            <tr>
                <td>
                <%= CoreLanguage.message %>
            </td>
            </tr></table><table class="input" style="width: 60%">
            <tr>
                <td>
                <%= Html.TextAreaFor(model => model.messege, new { style = "height: 150px; width:100%;border:0;" })%>
                </td>
            </tr>
            <tr>
                <td>
                <%= Html.ValidationMessageFor(model => model.messege) %>
          </td>
            </tr>
        </table><br />
        <div class="inputbutton">
            <%=Html.MytripInput(CoreLanguage.send, true)%>
            </div> 
        </div><div><div class="contentbottomright"></div><div class="contentbottomleft"></div><div class="contentbottomcon"></div></div>


    <% }} %>
  
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentRight" runat="server">
<%=Html.Partial("SideBar")%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ScriptContent" runat="server">
<script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script>
</asp:Content>
