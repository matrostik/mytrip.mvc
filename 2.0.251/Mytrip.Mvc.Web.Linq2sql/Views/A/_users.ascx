<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright ©  2009 Стюхин Олег Анатольевич   */-->
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div class="artycle_summtop"></div>

<%
    foreach (aspnet_Users x in (IEnumerable<aspnet_Users>)ViewData["users"])
  {
     
%><table><tr><td style="width: 25%">
<%=x.UserName %><br /><small>


<%string admin = "f";
        foreach (aspnet_UsersInRoles y in x.aspnet_UsersInRoles)
        { if (y.aspnet_Roles.RoleName == "admin") { admin = "admin"; } } 
  foreach (aspnet_UsersInRoles y in x.aspnet_UsersInRoles)
  {if(y.aspnet_Roles.RoleName=="blogger"){%>
  <%=Mytrip_Mvc_Language.blogger%>&nbsp;&nbsp;
  <%}if(y.aspnet_Roles.RoleName=="admin"){%>
  <%=Mytrip_Mvc_Language.admin%>&nbsp;&nbsp;
  <%} if (admin == "f")
  {
      if (y.aspnet_Roles.RoleName == "chief_editor")
      {%>
 <%=Mytrip_Mvc_Language.chief_editor%>&nbsp;&nbsp;
  <%}
  } if (y.aspnet_Roles.RoleName == "artycle_editor")
      {%>
  <%=Mytrip_Mvc_Language.artycle_editor%>&nbsp;&nbsp;
  <%}} %></small>
</td>
<td style="width: 25%">
<%=x.aspnet_Membership.Email %>
</td>
<td style="width: 25%">
<%=x.aspnet_Membership.CreateDate %>
</td>
<td style="width: 25%"><div class="edit_content">
<a href="<%=Url.Action("F", "A", new { a = x.UserName })%>"><img src="/content/images/edit.png" alt="<%=Mytrip_Mvc_Language.edit%>" style="border-width: 0px;" /></a>
<a href="<%=Url.Action("H", "A", new { a = x.UserName })%>" onclick = "return confirm ('<%=Mytrip_Mvc_Language.delete_user%>');"><img src="/content/images/delete.png" alt="<%=Mytrip_Mvc_Language.delete%>" style="border-width: 0px;" /></a>     
</div>
<%=x.LastActivityDate%>
</td></tr>
</table><div class="artycle_summtop"></div>
<%
    } %>

