<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Mvc.Models.ModulesModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
<%//************************************************************ 
  // Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich
  // To learn more about Mytrip.Mvc.Entyty visit 
  // http://starterkitmytripmvc.codeplex.com/
  // mytripmvc@gmail.com
  // license: Microsoft Public License (Ms-PL) 
  // *********************************************************** %>
    <%=Html.PageTitle(CoreLanguage.InstallModules, "/")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="title"><%=CoreLanguage.InstallModules %></h2>
    <div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content2">
   <table>
   <tr>
   <th style="width: 50%">
   <%=CoreLanguage.InstallModules %>
   </th>
   <th>
   <%=CoreLanguage.uninstallmodules%>
   
   </th></tr>
   <tr>
   <td>   
    <%if (Model.modules.Count() != 0)
      { %>
    <% using (Html.BeginForm())
       {%>
        
        
            <%foreach (var x in Model.modules)
              { %><div class="editor-field">
              <%=Html.CheckBox(x.Key)%>
              <%=x.Key%></div>
            <%} %>
            <p>
                <%=Html.MytripInput("submit", CoreLanguage.save)%>
            </p>

    <% }
      } %> 
      </td><td>
      <%foreach (var x in Model.uninstall)
              { %>
              <p>
              <%=Html.MytripImageLink(Url.Action("UninstallModule",new{id=x.Value}), 
                      "/Content/images/delete.png", "delete", 20, 0, 0, CoreLanguage.are_you_sure)%>
              <%=x.Value%>
              </p>
      <%} %>
      </td>
   </tr>
   </table>
    </div><div><div class="contentbottomright"></div><div class="contentbottomleft"></div><div class="contentbottomcon"></div></div>

    <div class="acfooter"></div>
    <div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content">

        <%=Html.ActionLink(CoreLanguage.cansel, "Index","Home") %>
    </div><div><div class="contentbottomright"></div><div class="contentbottomleft"></div><div class="contentbottomcon"></div></div>



</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContentRight" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="MainContentLeft" runat="server">
</asp:Content>

