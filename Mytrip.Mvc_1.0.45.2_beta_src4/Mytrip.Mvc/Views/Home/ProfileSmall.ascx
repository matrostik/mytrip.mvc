<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Mytrip.Mvc.Models.ProfileUsersModel>" %>
<%=Html.PageUserProfile(Model.UserName,new{
//StartProfile
i1=Mytrip.Articles.Export.ProfileArticles()
//EndProfile
})%>

































