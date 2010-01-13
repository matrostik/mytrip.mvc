<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%bool a = (bool)ViewData["lang_panel"];
    if(a==true){ %>
<div class="log_a">
<div class="log_gr">
<%string url = ViewData["language_url"].ToString(); %>
<form action="<%=Url.Action("AL", "A", new{url}) %>" method="post">
    <%= Html.DropDownList("language", (IEnumerable<SelectListItem>)ViewData["language"], "Language...", new { style = "border:0; color: #fff; background-color: #495D7F; height: 19px;" })%><input type="submit" value="go" class="input_lang"/>
</form></div></div>
<%} %>
