<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Mvc.Models.CreateBaseModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%--************************************************************
Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich
To learn more about Mytrip.Mvc.Entity visit
http://mytripmvc.codeplex.com
http://starterkitmytripmvc.codeplex.com
mytripmvc@gmail.com
license: Microsoft Public License (Ms-PL)
***********************************************************--%>
<%=Html.PageTitle(CoreLanguage.CreateBase, "/")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="title"><%=CoreLanguage.CreateBase%></h2>
    <% Html.EnableClientValidation(); %>
 <% using (Html.BeginForm()) {%>
    
     <div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content2">
          
            <div class="editor-label">
                <%= CoreLanguage.Provider %>
            </div>
            <div class="editor-field">
                <%= Html.DropDownListFor(model => model.Provider, Model.AllProvider) %>
                <%= Html.ValidationMessageFor(model => model.Provider)%>
            </div>
            <div class="editor-field">
                <%= Html.CheckBoxFor(model => model.IntegratedSecurity, new { id = "integratedS" })%>
                <%= CoreLanguage.IntegratedSecurity %>
            </div>
            
            <div class="editor-label">
                <%= CoreLanguage.Server %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Server) %>
                <%= Html.ValidationMessageFor(model => model.Server) %>
            </div>
            
            <div class="editor-label">
                <%= CoreLanguage.DataBase %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.DataBase) %>
                <%= Html.ValidationMessageFor(model => model.DataBase) %>
            </div>
            <div id="_integratedS" style="display: none">
            <div class="editor-label">
                <%= CoreLanguage.UserName %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.User) %>
                <%= Html.ValidationMessageFor(model => model.User) %>
            </div>
            
            <div class="editor-label">
                <%= CoreLanguage.Password %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Password)%>
                <%= Html.ValidationMessageFor(model => model.Password) %>
            </div>
       </div>
            
            <p>
                <%=Html.MytripInput("submit", CoreLanguage.next)%>
            </p>
 </div><div ><div class="contentbottomright"></div><div class="contentbottomleft"></div><div class="contentbottomcon"></div></div>

    <% } %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
<script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script>
    <%if(!Model.IntegratedSecurity){ %>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#_integratedS").show();
        });
    </script>
    <%} %>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#integratedS").click(function () {
                $("#_integratedS").slideToggle(300);
            });
        });
    </script>
</asp:Content>

