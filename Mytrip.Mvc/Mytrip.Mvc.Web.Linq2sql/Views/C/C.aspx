<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/2_column.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Mvc.Model.Linq2sql.mt_artycle>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">

	<%= ViewData["model_domain"]%>/<%= Html.Encode(Model.Title) %>
	
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright �  2009 ������ ���� �����������   */-->
  
<!-- ����� ����� -->
<div class="edit_content">
   <%= Html.EditeAndDeliteArtycle(Model.mt_artycle_category.Blog, Model.mt_artycle_category.News, Model.Id, Model.mt_artycle_category.Id, Model.AddedBy, Model.mt_artycle_category.AddedBy)%>      
 </div>
<!--END ����� ����� -->
 <%if (Model.ApprovedVotes == true)
  { using (Ajax.BeginForm(new AjaxOptions{UpdateTargetId="votes"})){
      string comment = "votes";%>
  <div class="votes_gr"><input type="submit" value="" name="b" class="input_up" />
  <div id="votes" class="votes">  
<%=Model.Votes %></div> 
<input type="submit" value="" name="comment" class="input_down" /> 
</div> 
  
  <%}%> 

<%}%>
  
  
    <h2><%= Html.Encode(Model.Title) %></h2>
 <%if (Model.DescriptionBody == true)
   {
       if (Model.UrlImageDescription.Length > 7)
       { %>
        <img src="<%=Model.UrlImageDescription %>" alt="<%=Model.Title %>"  style="position: relative;  float: right; margin-left: 5px; margin-top: 5px;" />
        <%}
   }
   else
   {
       if (Model.UrlImageBody.Length > 7)
       { %>
        <img src="<%=Model.UrlImageBody %>" alt="<%=Model.Title %>" style="position: relative;  float: right; margin-left: 5px; margin-top: 5px;" />
        <%}
   }%>
<%= Model.Body %>


 <br /><br />
  <small><%= Html.Encode(String.Format("{0:d MMMM yyyy �.}", Model.AddedDate))%></small><br /> 
<small><%=Html.Language(Mytrip_Mvc_Language_1.addedby,Model.AddedBy) %></small><br />
<%if (Model.mt_artycle_category.CategoryId == 0)
  {
      if (Model.mt_artycle_category.Blog == false)
      {%><small><%=Mytrip_Mvc_Language_1.article_heading%></small><%}
      else
      {%><small><%=Mytrip_Mvc_Language_1.post_blog%></small><%} %>
        <a href="<%=Url.Action("B", "C", new { a = Model.mt_artycle_category.Id, b = 1,c=10, d=Model.mt_artycle_category.Path })%>">
    <small><%= Model.mt_artycle_category.Title.ToLower()%></small></a><br />   
 <%}
  else {if (Model.mt_artycle_category.mt_artycle_category1.Blog == false)
      {%><small><%=Mytrip_Mvc_Language_1.article_heading%></small><%}
      else
      {%><small><%=Mytrip_Mvc_Language_1.post_blog%></small><%} %>
        <a href="<%=Url.Action("B", "C", new { a = Model.mt_artycle_category.mt_artycle_category1.Id, b = 1,c=10, d=Model.mt_artycle_category.mt_artycle_category1.Path })%>">
    <small><%= Model.mt_artycle_category.mt_artycle_category1.Title.ToLower()%></small></a><br />
    <% if (Model.mt_artycle_category.Blog == false)
      {%><small><%=Mytrip_Mvc_Language_1.article_subheading%></small><%}
      else
      {%><small><%=Mytrip_Mvc_Language_1.blog_heading%></small><%} %>
        <a href="<%=Url.Action("B", "C", new { a = Model.mt_artycle_category.Id, b = 1,c=10, d=Model.mt_artycle_category.Path })%>">
    <small><%= Model.mt_artycle_category.Title.ToLower()%></small></a><br />      
 <% } %>       
<% if (Model.mt_artycle_in_tegs.Count() > 0)
    { %>
  <div><small><%=Mytrip_Mvc_Language_1.article_tegs%></small>
  <%foreach (mt_artycle_in_teg y in Model.mt_artycle_in_tegs)
    { %>&nbsp;<a href="<%=Url.Action("E", "C", new { a = y.mt_artycle_teg.Id, b = 1, c=10, d=y.mt_artycle_teg.Path})%>">
    <small><%= y.mt_artycle_teg.Title%></small></a>
  <%} %></div><br /><br /> <%}%>
  <%if (Model.AddComment == true)
    {
        if (Model.mt_artycle_comment.Count != 0) {%><a href="<%=Url.Action("Rss_E", "C", new{a=Model.Id})%>">
    <img src="/content/images/feed.png" alt="rss" class="rss_home" /></a><% }%>
    <% Html.RenderPartial("_comment"); %>
     <%= Html.ValidationSummary(Mytrip_Mvc_Language_1.error)%>
    <%if (HttpContext.Current.User.Identity.IsAuthenticated)
      {
          using (Html.BeginForm())
          {%><br /><br /><div><%=Mytrip_Mvc_Language_1.add_comment%><br /><br />
              <%= Html.TextArea("comment", new { style = "height: 200px; width: 400px" })%>
       <br />
       <%= Html.ValidationMessage("comment", "*")%> 
      </div>
                <input type="submit" value="<%=Mytrip_Mvc_Language_1.add%>" class="input_boottom" />
      
       
    <%}
      }
      else
      { %><br /><br /><a href="<%=Url.Action("A", "B", new { returnUrl = (string)ViewData["logon_url"]})%>">
    <%=Mytrip_Mvc_Language_1.add_comment%></a> 
    <%}} %>     
        
  

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="RightContent" runat="server">
  <% Html.RenderPartial("_S_admin"); %>
    <% Html.RenderPartial("_S_search"); %>
    <% Html.RenderPartial("_S_menu_user"); %>
    <% Html.RenderPartial("_S_news_right"); %>
    <% Html.RenderPartial("_S_artycle_right"); %>
    <% Html.RenderPartial("_S_blog_right"); %>
    <% Html.RenderPartial("_S_teg_right"); %>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptContent" runat="server">
<script src="../../../Scripts/MicrosoftAjax.js" type="text/javascript"></script>
<script src="../../../Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script>
  <script src="../../../Scripts/tiny_mce/tiny_mce_src.js" type="text/javascript"></script>
    <script type="text/javascript">
        tinyMCE.init({
        mode: "textareas",
        
        });
</script>
</asp:Content>

