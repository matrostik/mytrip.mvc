<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/1_column.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Mvc.Model.Linq2sql.mt_artycle_teg>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= ViewData["abstract_model_domain"]%>/<%=Mytrip_Mvc_Language.add_tegs%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright ©  2009 Стюхин Олег Анатольевич   */-->
    <h2><%=Mytrip_Mvc_Language.add_tegs%></h2>
<table><tr><td>
<%int abc = (int)ViewData["artycleid"];
  int bcd = (int)ViewData["categoryid"];
  bool cde=(bool)ViewData["blog_bool"];
  bool nw = (bool)ViewData["news_bool"];
  string path = "";
  foreach (mt_artycle x in (IEnumerable<mt_artycle>)ViewData["artycle"])
  {
     
%>

<%
  if (x.Id == abc)
  {
      path = x.Path; 
      if (x.UrlImageDescription.Length > 7)
      { %>
        <img src="<%=x.UrlImageDescription %>" alt="<%=x.Title %>" style="position: relative; width:120px; float: left; margin-right: 5px;" />
        <%} %>



<a href="<%=Url.Action("C", "C", new { a = x.Id, b=x.Path})%>">
    <%= x.Title%></a> <%if (x.RegistrUser == true)
                        { %>&nbsp;&nbsp;&nbsp;&nbsp;<small><%=Mytrip_Mvc_Language.only_for_registered%></small> <%} %>
<br /><br />
<%= x.Description%></td></tr>
<tr><td style="border-bottom-style: dotted; border-bottom-width: 1px; border-bottom-color: #555152">
<small><%= Html.Encode(String.Format("{0:d MMMM yyyy }", x.AddedDate))%></small>&nbsp;&nbsp;&nbsp;&nbsp;

<%if (x.mt_artycle_category.Blog == false)
  {
      if (x.mt_artycle_category.CategoryId == 0)
      {
      
      %><small><%=Mytrip_Mvc_Language.article_heading%></small>
        <a href="<%=Url.Action("B", "C", new { a = x.mt_artycle_category.Id, b = 1,c=10, d=x.mt_artycle_category.Path })%>">
    <small><%= x.mt_artycle_category.Title.ToLower()%></small></a>&nbsp;&nbsp;&nbsp;&nbsp;   
<%}
      else
      { %>
      <small><%=Mytrip_Mvc_Language.article_heading%></small>
        <a href="<%=Url.Action("B", "C", new { a = x.mt_artycle_category.mt_artycle_category1.Id, b = 1,c=10, d=x.mt_artycle_category.mt_artycle_category1.Path })%>">
    <small><%= x.mt_artycle_category.mt_artycle_category1.Title.ToLower()%></small></a>&nbsp;&nbsp;&nbsp;&nbsp;   
      
      
      <small><%=Mytrip_Mvc_Language.article_subheading%></small>
        <a href="<%=Url.Action("B", "C", new { a = x.mt_artycle_category.Id, b = 1,c=10, d=x.mt_artycle_category.Path })%>">
    <small><%= x.mt_artycle_category.Title.ToLower()%></small></a>&nbsp;&nbsp;&nbsp;&nbsp;   
<% }
  }
  else
  {
      if (x.mt_artycle_category.CategoryId == 0)
      {
      
      %><small><%=Mytrip_Mvc_Language.post_blog%></small>
        <a href="<%=Url.Action("B", "C", new { a = x.mt_artycle_category.Id, b = 1,c=10, d=x.mt_artycle_category.Path })%>">
    <small><%= x.mt_artycle_category.Title.ToLower()%></small></a>&nbsp;&nbsp;&nbsp;&nbsp;   
<% }
      else { %><small><%=Mytrip_Mvc_Language.post_blog%></small>
        <a href="<%=Url.Action("B", "C", new { a = x.mt_artycle_category.mt_artycle_category1.Id, b = 1,c=10, d=x.mt_artycle_category.mt_artycle_category1.Path })%>">
    <small><%= x.mt_artycle_category.mt_artycle_category1.Title.ToLower()%></small></a>&nbsp;&nbsp;&nbsp;&nbsp;
    <small><%=Mytrip_Mvc_Language.post_blog%></small>
        <a href="<%=Url.Action("B", "C", new { a = x.mt_artycle_category.Id, b = 1,c=10, d=x.mt_artycle_category.Path })%>">
    <small><%= x.mt_artycle_category.Title.ToLower()%></small></a>&nbsp;&nbsp;&nbsp;&nbsp;    
<% }
  } %><small><%=Html.Language(Mytrip_Mvc_Language.article_views, x.Views.ToString())%></small>
<%if (x.AddComment == true)
  { %>&nbsp;&nbsp;&nbsp;&nbsp;<small><%=Html.Language(Mytrip_Mvc_Language.article_comments, x.mt_artycle_comment.Count().ToString())%></small><%} %>
  <%if (x.ApprovedVotes == true)
    { %>&nbsp;&nbsp;&nbsp;&nbsp;<small><%=Html.Language(Mytrip_Mvc_Language.article_rating, x.Votes.ToString())%></small><%} %>
</td></tr>
<tr><td style="border-bottom-style: dotted; border-bottom-width: 1px; border-bottom-color: #555152">  
<%if (x.mt_artycle_category.Blog == false)
  {%><small><%=Mytrip_Mvc_Language.tegs_article%></small><%}
  else
  {%><small><%=Mytrip_Mvc_Language.tegs_post%></small>  <%}%><br /><br />
   <div class="right_content">
  <% foreach (mt_artycle_in_teg y in x.mt_artycle_in_tegs)
    { %>
  
 
<a href="<%=Url.Action("ZR", new { a = y.ArtycleId , b=y.TegId})%>">
    <%= y.mt_artycle_teg.Title%></a>
  <%}%></div><%
  } %>

<%
    } %><br /><br /></td></tr>
    <tr><td style="border-bottom-style: dotted; border-bottom-width: 1px; border-bottom-color: #555152">
   <%if (cde == false)
     { %>
   <small><%=Mytrip_Mvc_Language.all_tegs_article%></small> <%}
     else
     { %><small><%=Mytrip_Mvc_Language.all_tegs_post%></small>
   <%} %>
    <br /><br />
    <div class="right_content">
    <%
                foreach (mt_artycle_teg x in (IEnumerable<mt_artycle_teg>)ViewData["helper_teg"])
                {
     
%> 
<a href="<%=Url.Action("ZQ", new { a = abc, b=x.Id})%>">
    <%= x.Title%></a>
<%} %></div><br /><br />
</td></tr>
<tr><td style="border-bottom-style: dotted; border-bottom-width: 1px; border-bottom-color: #555152; text-align: center;">

    <% using (Html.BeginForm()) {%>

       
           
           
               <%= Html.ValidationMessageFor(model => model.Title) %><br />
                <%= Html.TextBox("Title")%>             
            
                <input type="submit" value="<%=Mytrip_Mvc_Language.create_teg%>" class="input_boottom" /><br /><br />
           
       

    <% } %>
</td></tr>
   </table><br />

<%string a_b;
  if (cde == false)
  {
      if (nw == false)
      {
          a_b = "/C/ZK/" + abc + "/Edit_article";
      }
      else { a_b = "/C/ZK/" + abc + "/Edit_news"; }
  }
  else {a_b = "/C/ZK/" + abc+"/Edit_post"; } %>
  <div class="button">
   <input type="button" onclick="location.href('<%= a_b %>')" value="<%=Mytrip_Mvc_Language.back%>" class="input_boottom" />    
   <input type="button" onclick="location.href('/C/C/<%=abc %>/<%=path %>')" value="<%=Mytrip_Mvc_Language.next%>" class="input_boottom" /> 
  </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

