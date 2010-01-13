<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/1_column.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Mvc.Model.Linq2sql.aspnet_Users>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= ViewData["model_domain"]%>/<%= Html.Encode(Model.UserName) %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright ©  2009 Стюхин Олег Анатольевич   */-->
    <h2><%= Html.Encode(Model.UserName) %></h2>

   
        <p>
            Email:
            <%= Html.Encode(Model.aspnet_Membership.Email) %>
        </p>
         <p>
            <%=Html.Language(Mytrip_Mvc_Language_1.last_visit, Model.LastActivityDate.ToString()) %>
        </p> <%if ((bool)ViewData["model_blog"] == true)
               {%>
        <p>
        
            <%foreach (aspnet_UsersInRoles y in Model.aspnet_UsersInRoles)
              {
                  if (y.aspnet_Roles.RoleName == "blogger")
                  { %>
    <%=Mytrip_Mvc_Language_1.blogger%><%}
              } %>
        </p><%} %>
    
    
<table>
<tr><td style="border-bottom-style: dotted; border-bottom-width: 1px; border-bottom-color: #555152">
<small><%=Mytrip_Mvc_Language_1.roles_user%></small>
 
    <br /><br />
    <div class="teg">
    <%string admin = "f";
      string artycle_editor = "f";
      string chief_editor = "f";
        foreach (aspnet_UsersInRoles x in (IEnumerable<aspnet_UsersInRoles>)ViewData["roles_user"])
        {
            if (x.aspnet_Roles.RoleName == "admin")
            {
                admin = "admin";
     
%> 
&nbsp;&nbsp;<a href="<%=Url.Action("I", new { a = Model.UserName, b="admin"})%>">
    <%=Mytrip_Mvc_Language_1.admin%></a>&nbsp;&nbsp;
<%} if (x.aspnet_Roles.RoleName == "artycle_editor")
            {
                artycle_editor = "artycle_editor";
     
%> 
&nbsp;&nbsp;<a href="<%=Url.Action("I", new { a = Model.UserName, b="artycle_editor"})%>">
    <%=Mytrip_Mvc_Language_1.artycle_editor%></a>&nbsp;&nbsp;
<%} if (admin == "f")
            {
                if (x.aspnet_Roles.RoleName == "chief_editor")
                {
                    chief_editor = "chief_editor";
%> 
&nbsp;&nbsp;<a href="<%=Url.Action("I", new { a = Model.UserName, b="chief_editor"})%>">
    <%=Mytrip_Mvc_Language_1.chief_editor%></a>&nbsp;&nbsp;
<%}
            }
        } %></div><br /><br /> 
</td>
</tr>
<tr><td style="border-bottom-style: dotted; border-bottom-width: 1px; border-bottom-color: #555152">
<small><%=Mytrip_Mvc_Language_1.roles_site%></small>
 
    <br /><br />
    <div class="teg">
    <% foreach (aspnet_Roles x in (IEnumerable<aspnet_Roles>)ViewData["roles"])
       {
           if (admin == "f")
           {
               if (x.RoleName == "admin")
               {                
     
%> 
&nbsp;&nbsp;<a href="<%=Url.Action("J", new { a = Model.UserName, b="admin"})%>">
    <%=Mytrip_Mvc_Language_1.admin%></a>&nbsp;&nbsp;
<%}
           }
           if (artycle_editor == "f")
           {
               if (x.RoleName == "artycle_editor")
               {
     
%> 
&nbsp;&nbsp;<a href="<%=Url.Action("J", new { a = Model.UserName, b="artycle_editor"})%>">
    <%=Mytrip_Mvc_Language_1.artycle_editor%></a>&nbsp;&nbsp;
<%}
           } 
           
               if (chief_editor == "f")
           {
               if (x.RoleName == "chief_editor")
               {
     
%> 
&nbsp;&nbsp;<a href="<%=Url.Action("J", new { a = Model.UserName, b="chief_editor"})%>">
    <%=Mytrip_Mvc_Language_1.chief_editor%></a>&nbsp;&nbsp;
<%}
           }
        } %></div><br /><br />  
</td>
</tr>
</table>
<p>
        <a href="<%=Url.Action("D", "A", new{a=0, b=1,c=25,d="Users"})%>">
    <%=Mytrip_Mvc_Language_1.menu_management_users%></a>
    </p>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

