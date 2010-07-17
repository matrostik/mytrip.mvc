<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%=Html.AccordionUsersAndFiles(new {
//StartManagerControlPanel
i1=Mytrip.Articles.Export.ManagerArticle(),
i2=Mytrip.Votes.Export.ManagerVotes()
//EndManagerControlPanel
},new {
//StartSettingControlPanel
i1=Mytrip.Articles.Export.SettingArticle(),
i2=Mytrip.Votes.Export.SettingVotes()
//EndSettingControlPanel
}, true)%>
<%=Html.AccordionUserProfile(new {
//StartProfile
i1=Mytrip.Articles.Export.ProfileArticles()
//EndProfile
})%>
<%=Html.AccordionDonateProject() %>
<%=Html.Partial("SideBarExport")%>































