<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Mytrip.Mvc.Models.ProfileUsersModel>" %>
<%=Html.LastActivity(new {
//StartLastActivity
i1=Mytrip.Articles.Export.LastActivity(Model.UserName)
//EndLastActivity
})%>
