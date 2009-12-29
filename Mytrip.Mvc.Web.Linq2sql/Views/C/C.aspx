<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/2_column.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Mvc.Model.Linq2sql.mt_artycle>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">

	<%= ViewData["model_domain"]%>/<%= Html.Encode(Model.Title) %>
	
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright ©  2009 Стюхин Олег Анатольевич   */-->
  
<!-- АДМИН ЧАСТЬ -->
<div class="edit_content">
   <%= Html.EditeAndDeliteArtycle((bool)Model.mt_artycle_category.Blog, (bool)Model.mt_artycle_category.News, Model.Id, Model.mt_artycle_category.Id, Model.AddedBy, Model.mt_artycle_category.AddedBy)%>      
 </div>
<!--END АДМИН ЧАСТЬ -->
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
  <small><%= Html.Encode(String.Format("{0:d MMMM yyyy г.}", Model.AddedDate))%></small><br /> 
<small>Добавил: <%=Model.AddedBy %></small><br />
<%if (Model.mt_artycle_category.Blog == false)
        {%><small>Рубрика: </small><%}else{%><small>Блог: </small><%} %>
        <a href="<%=Url.Action("B", "C", new { a = Model.mt_artycle_category.Id, b = 1,c=10, d=Model.mt_artycle_category.Path })%>">
    <small><%= Model.mt_artycle_category.Title.ToLower() %></small></a><br />   
        
<% if (Model.mt_artycle_in_teg.Count() > 0)
    { %>
  <div><small>Теги: </small>
  <%foreach (mt_artycle_in_teg y in Model.mt_artycle_in_teg)
    { %>&nbsp;<a href="<%=Url.Action("E", "C", new { a = y.mt_artycle_teg.Id, b = 1, c=10, d=y.mt_artycle_teg.Path})%>">
    <small><%= y.mt_artycle_teg.Title%></small></a>
  <%} %></div><br /><br /> <%}%>
  <%if (Model.AddComment == true)
    { %>
    <% Html.RenderPartial("_comment"); %>
    
    <%if (HttpContext.Current.User.Identity.IsAuthenticated)
      {
          using (Html.BeginForm())
          {%><br /><br /><div>Добавить комментарий<br /><br />
              <textarea name="comment" cols="20" style="width: 100%; height: 200px" rows="5"></textarea>
       <br />
       <%= Html.ValidationMessage("comment", "*")%> 
      </div>
                <input type="submit" value="добавить" class="input_boottom" />
      
       
    <%}
      }
      else
      { %><br /><br /><a href="<%=Url.Action("A", "B", new { returnUrl = (string)ViewData["logon_url"]})%>">
    добавить комментарий</a> 
    <%}} %>     
        
  

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="RightContent" runat="server">
  <% Html.RenderPartial("_S_admin"); %>
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
        mode: "textareas"
        
        
        });
</script>
</asp:Content>

