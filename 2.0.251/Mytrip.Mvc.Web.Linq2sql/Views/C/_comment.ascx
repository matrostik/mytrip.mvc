<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright �  2009 ������ ���� �����������   */-->
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>


<%
    foreach (mt_artycle_comment x in (IEnumerable<mt_artycle_comment>)ViewData["artycle_comment"])
    {
     
%><table>
<tr>
<td style="border: 1px solid #BCC7D8; width: 74px; text-align: center">
<%= Html.Gravatar(x.Email, new  { s = "70", r = "g" })%>
<br /><%=x.AddedBy %><br /><%= Html.Encode(x.AddedDate)%>
</td>
<td style="border: 1px solid #BCC7D8">
<% if (HttpContext.Current.User.Identity.IsAuthenticated)
       {
           if (x.AddedBy == Page.User.Identity.Name)
           {%>
<div class="edit_com"> 
<a href="<%=Url.Action("ZZ", "C", new { a = x.Id })%>"><img src="/content/images/edit.png" alt="<%=Mytrip_Mvc_Language.edit%>" style="border-width: 0px;" /></a><br />
<a href="<%=Url.Action("ZY", "C", new { a = x.Id })%>" onclick = "return confirm ('<%=Mytrip_Mvc_Language.delete_comment%>');"><img src="/content/images/delete.png" alt="<%=Mytrip_Mvc_Language.delete%>" style="border-width: 0px;" /></a>     
</div>
<%}
           else {
               if (x.mt_artycle.mt_artycle_category.Blog == true)
  {
      if (HttpContext.Current.User.IsInRole("blogger"))
      {
          if (x.mt_artycle.mt_artycle_category.AddedBy == Page.User.Identity.Name)
          {%>
<div class="edit_com"> 

<a href="<%=Url.Action("ZY", "C", new { a = x.Id })%>" onclick = "return confirm ('<%=Mytrip_Mvc_Language.delete_comment%>');"><img src="/content/images/delete.png" alt="<%=Mytrip_Mvc_Language.delete%>" style="border-width: 0px;" /></a>     
</div>
<%} if (HttpContext.Current.User.IsInRole("chief_editor"))
          {
              if (x.mt_artycle.mt_artycle_category.AddedBy != Page.User.Identity.Name)
              {%><div class="edit_com"> 

<a href="<%=Url.Action("ZY", "C", new { a = x.Id })%>" onclick = "return confirm ('<%=Mytrip_Mvc_Language.delete_comment%>');"><img src="/content/images/delete.png" alt="<%=Mytrip_Mvc_Language.delete%>" style="border-width: 0px;" /></a>     
</div>
<%}
          }
      }
  }
  else
  {
     
          if (HttpContext.Current.User.IsInRole("artycle_editor"))
          {
              if (x.mt_artycle.AddedBy == Page.User.Identity.Name)
              {%><div class="edit_com"> 

<a href="<%=Url.Action("ZY", "C", new { a = x.Id })%>" onclick = "return confirm ('<%=Mytrip_Mvc_Language.delete_comment%>');"><img src="/content/images/delete.png" alt="<%=Mytrip_Mvc_Language.delete%>" style="border-width: 0px;" /></a>     
</div>
<%}
          } if (HttpContext.Current.User.IsInRole("chief_editor"))
          {%><div class="edit_com"> 

<a href="<%=Url.Action("ZY", "C", new { a = x.Id })%>" onclick = "return confirm ('<%=Mytrip_Mvc_Language.delete_comment%>');"><img src="/content/images/delete.png" alt="<%=Mytrip_Mvc_Language.delete%>" style="border-width: 0px;" /></a>     
</div>
<%}
      } }} %>
<%=x.Body %>
</td>

</tr>
</table>
<%} %>
