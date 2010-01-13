<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/1_column.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Mvc.Model.Linq2sql.mt_artycle>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= ViewData["model_domain"]%>/<%string c = ViewData["edit"].ToString();
  if (c == "Edit_news") {%><%=Mytrip_Mvc_Language_1.menu_edit_news%><% }
  if (c == "Edit_article")
  {%><%=Mytrip_Mvc_Language_1.menu_edit_article%><%}
  if (c == "Edit_post")
  {%><%=Mytrip_Mvc_Language_1.menu_edit_post%><%}%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright ©  2009 Стюхин Олег Анатольевич   */-->
    <%string c = ViewData["edit"].ToString();
  if (c == "Edit_news") {%><h2><%=Mytrip_Mvc_Language_1.menu_edit_news%></h2><% }
  if (c == "Edit_article")
  {%><h2><%=Mytrip_Mvc_Language_1.menu_edit_article%></h2><%}
  if (c == "Edit_post")
  {%><h2><%=Mytrip_Mvc_Language_1.menu_edit_post%></h2><%}%>
<br />

    <% using (Html.BeginForm()) {%>

       
         <div style="position: relative; float: right; width: 300px; padding-top: 100px;">
        
	<p>
        
<a href="#" onclick= "window.open('/G/B/()UserFile()<%= Html.Encode(Page.User.Identity.Name) %>', 'myWindow', 'tooldar=0, menubar=0, height=600, width=1000, scrollbars=1');">
<%=Mytrip_Mvc_Language_1.menu_file_manager%></a>
       </p>
            <p>
                 <label for="UrlImageDescription"><%=Mytrip_Mvc_Language_1.photo_summary%></label><br />
                <%= Html.TextBox("UrlImageDescription")%>
                <%= Html.ValidationMessageFor(model => model.UrlImageDescription) %>
            </p>
            <p>
                <label for="UrlImageBody"><%=Mytrip_Mvc_Language_1.photo_article%></label><br />               
                <%= Html.TextBox("UrlImageBody")%>
                <%= Html.ValidationMessageFor(model => model.UrlImageBody) %>
            </p>
            <p>
               <label for="DescriptionBody"><%=Mytrip_Mvc_Language_1.photo_article_summary%></label> 
                <%= Html.CheckBox("DescriptionBody")%>
            </p>
          
            <p>
               <label for="RegistrUser"><%=Mytrip_Mvc_Language_1.article_only_registr%></label> 
                <%= Html.CheckBox("RegistrUser")%>              
            </p>
            <%if (c != "Edit_post")
              { %>
             <p>
                <label for="AddComment"><%=Mytrip_Mvc_Language_1.article_comment%></label>
                <%= Html.CheckBox("AddComment")%>                
            </p>
            <p>
               <label for="ApprovedVotes"><%=Mytrip_Mvc_Language_1.artycle_votes%></label> 
                <%= Html.CheckBox("ApprovedVotes")%>                
            </p>
            <% if (c == "Edit_news")
              { %><p>
               <label for="Warning"><%=Mytrip_Mvc_Language_1.allocate_color%></label> 
                <%= Html.CheckBox("Warning")%>                
            </p>
          <%} %>
          
	<p>
                <label for="CloseDate"><%=Mytrip_Mvc_Language_1.date_close%></label><br />
                <%= Html.TextBox("CloseDate",Html.Encode(String.Format("{0:yyyy-MM-dd}", Model.CloseDate)))%>
                <%= Html.ValidationMessageFor(model => model.CloseDate)%>
            </p>
             <% bool a = (bool)ViewData["lang_panel"];
                if (a == true)
                {
                    if (Model.mt_artycle_category.AllCulture == true)
                    {%>
              <p> <label for="AllCulture"><%=Mytrip_Mvc_Language_2.view_all_culture%></label>              
                <%= Html.CheckBox("AllCulture")%>               
            </p>
              <%}
                }
            } %>
            </div>
         <%if (c != "Edit_post")
              { %>
         <p>
                <label for="CategoryId"><%=Mytrip_Mvc_Language_1.chose_heding%></label><br />
                <%= Html.DropDownList("c", (IEnumerable<SelectListItem>)ViewData["Category"])%>
                <%= Html.ValidationMessageFor(model => model.CategoryId)%>
            </p>
	<%} %>
	
            
            <p>
               <label for="Title"><%=Mytrip_Mvc_Language_1.title%></label><br />
                <%= Html.TextBox("Title")%>
                <%= Html.ValidationMessageFor(model => model.Title) %>
            </p>
            <p>
                <label for="Description"><%=Mytrip_Mvc_Language_1.summary%></label><br />
                <%= Html.TextArea("Description", new { style = "height: 250px" })%>
                <%= Html.ValidationMessageFor(model => model.Description) %>
            </p>
           
            <p>
           
                <label for="Body"><%=Mytrip_Mvc_Language_1.article1%></label><br />
                <%= Html.TextArea("Body", new { style = "height: 600px" })%>
                <%= Html.ValidationMessageFor(model => model.Body) %>
            </p>         
	
	
              
            

    <div class="button">
   <input type="button" onclick="location.href('<%=ViewData["url"] %>')" value="<%=Mytrip_Mvc_Language_1.back%>" class="input_boottom" />    
   <input type="submit" value="<%=Mytrip_Mvc_Language_1.next%>" class="input_boottom" />
  </div>                    
           
        

    <% } %>

   

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
 <script src="../../../Scripts/tiny_mce/tiny_mce_src.js" type="text/javascript"></script>
    <script type="text/javascript">
        tinyMCE.init({
        mode: "textareas",
        theme: "advanced",        
        plugins: "table,save,advhr,advimage,advlink,insertdatetime, preview,zoom, searchreplace,print,contextmenu,paste,directionality ",
        theme_advanced_buttons1_add_before: "save,newdocument,separator",
        theme_advanced_buttons1_add: "fontselect,fontsizeselect",
        theme_advanced_buttons2_add: "separator,insertdate,inserttime,preview,zoom,separator, forecolor,backcolor",
        theme_advanced_buttons2_add_before: "cut,copy,paste,pastetext,pasteword,separator",
        theme_advanced_buttons3_add: "advhr,separator,print,separator,ltr,rtl,separator ",
        theme_advanced_toolbar_location: "top",
        theme_advanced_toolbar_align: "left",
        theme_advanced_statusbar_location: "bottom",
        plugi2n_insertdate_dateFormat: "%Y-%m-%d",
        plugi2n_insertdate_timeFormat: "%H:%M:%S",
        theme_advanced_resizing: true,
        theme_advanced_resize_horizontal: false,
        paste_auto_cleanup_on_paste: true,
        paste_convert_headers_to_strong: false,
        paste_strip_class_attributes: "all",
        paste_remove_spans: false,
        paste_remove_styles: false
        });
</script>
</asp:Content>