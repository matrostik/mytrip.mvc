<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<h2 class="title"><%=CoreLanguage.license_agreement %></h2>
<div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content2">
<textarea id="TextArea1" cols="80" rows="20" style="width: 99%">
<%foreach (string x in Mytrip.Mvc.StartUpSettings.License.ViewLicense())
{ %>
<%=x %>
<%} %></textarea><p>
<%=Html.ActionLink(CoreLanguage.i_agree, "CreateBase", "Core")%></p>
</div><div ><div class="contentbottomright"></div><div class="contentbottomleft"></div><div class="contentbottomcon"></div></div>
        
