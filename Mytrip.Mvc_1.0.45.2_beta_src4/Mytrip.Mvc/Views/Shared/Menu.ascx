<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%= Html.MytripMenu(new {
//StartMenu
i1=Mytrip.Articles.Export.MenuArticle(),
i2=Mytrip.Articles.Export.MenuCategory(),
//EndMenu (Don't remove this line)
})%>
