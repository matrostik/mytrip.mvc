<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/1_column.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Mvc.Model.Linq2sql.mt_artycle>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= ViewData["abstract_model_domain"]%>/<%string c = ViewData["create"].ToString();
  if (c == "Create_news") {%><%=Mytrip_Mvc_Language.menu_write_news%><% }
  if (c == "Create_article")
  {%><%=Mytrip_Mvc_Language.menu_write_article%><%}
  if (c == "Create_post")
  {%><%=Mytrip_Mvc_Language.menu_write_post%><%}%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright ©  2009 Стюхин Олег Анатольевич   */-->
<%string c = ViewData["create"].ToString();
  if (c == "Create_news") {%><h2><%=Mytrip_Mvc_Language.menu_write_news%></h2><% }
  if (c == "Create_article")
  {%>
<h2><%=Mytrip_Mvc_Language.menu_write_article%></h2><%}
  if (c == "Create_post")
  {%>
<h2><%=Mytrip_Mvc_Language.menu_write_post%></h2><%}%>
<br />

    <% using (Html.BeginForm()) {%>

       
        <div style="position: relative; float: right; width: 300px; padding-top: 100px;">
        
	    
	<p>
        
<a href="#" onclick= "window.open('/G/B/()UserFile()<%= Html.Encode(Page.User.Identity.Name) %>', 'myWindow', 'tooldar=0, menubar=0, height=600, width=1000, scrollbars=1');">
<%=Mytrip_Mvc_Language.menu_file_manager%></a>
       </p>
        
       
            <p>
                <label for="UrlImageDescription"><%=Mytrip_Mvc_Language.photo_summary%></label><br />
                <%= Html.TextBox("UrlImageDescription", "null")%>
                <%= Html.ValidationMessageFor(model => model.UrlImageDescription) %>
            </p>
            <p>
                <label for="UrlImageBody"><%=Mytrip_Mvc_Language.photo_article%></label><br />                
                <%= Html.TextBox("UrlImageBody", "null")%>
                <%= Html.ValidationMessageFor(model => model.UrlImageBody) %>
            </p>
            <p>
               <label for="DescriptionBody"><%=Mytrip_Mvc_Language.photo_article_summary%></label> 
                <%= Html.CheckBox("DescriptionBody")%>
            </p>
          
            <p>
               <label for="RegistrUser"><%=Mytrip_Mvc_Language.article_only_registr%></label> 
                <%= Html.CheckBox("RegistrUser")%>              
            </p>
            <%if (c != "Create_post")
              { %>
             <p>
                <label for="AddComment"><%=Mytrip_Mvc_Language.article_comment%></label>
                <%= Html.CheckBox("AddComment")%>                
            </p>
            <p>
               <label for="ApprovedVotes"><%=Mytrip_Mvc_Language.artycle_votes%></label> 
                <%= Html.CheckBox("ApprovedVotes")%>                
            </p>
            <% if (c == "Create_news")
              { %><p>
               <label for="Warning"><%=Mytrip_Mvc_Language.allocate_color%></label> 
                <%= Html.CheckBox("Warning")%>                
            </p>
          <%} %>
          
	<p>
                <label for="CloseDate"><%=Mytrip_Mvc_Language.date_close%></label><br />
                <%= Html.TextBox("CloseDate",Html.Encode(String.Format("{0:yyyy-MM-dd}", DateTime.MaxValue)))%>
                <%= Html.ValidationMessageFor(model => model.CloseDate)%>
            </p>
             <% bool a = (bool)ViewData["abstract_lang_panel"];
                bool b = (bool)ViewData["category_allculture"];
                if (a == true)
                {
                    if (b == true)
                    {%>
              <p> <label for="AllCulture"><%=Mytrip_Mvc_Language.view_all_culture%></label>              
                <%= Html.CheckBox("AllCulture")%>               
            </p>
              <%}
                }
            } %>
            </div>
            <%if (c != "Create_post")
              { %>
         <p>
                <label for="CategoryId"><%=Mytrip_Mvc_Language.chose_heding%></label><br />
                <%= Html.DropDownList("a", (IEnumerable<SelectListItem>)ViewData["Category"])%>
                <%= Html.ValidationMessageFor(model => model.CategoryId)%>
            </p>
	<%} %>
            
            <p>
               <label for="Title"><%=Mytrip_Mvc_Language.title%></label><br />
                <%= Html.TextBox("Title")%>
                <%= Html.ValidationMessageFor(model => model.Title) %>
            </p>
            <p>
                <label for="Description"><%=Mytrip_Mvc_Language.summary%></label><br />
                <%= Html.TextArea("Description", new { style = "height: 250px" })%>
                <%= Html.ValidationMessageFor(model => model.Description) %>
            </p>
           
            <p>
           
                <label for="Body"><%=Mytrip_Mvc_Language.article1%></label><br />
                <%= Html.TextArea("Body", new { style = "height: 600px" })%>
                <%= Html.ValidationMessageFor(model => model.Body) %>
            </p>            
	
	<div class="button">
   <input type="button" onclick="location.href('<%= ViewData["helper_back_url"] %>')" value="<%=Mytrip_Mvc_Language.back%>" class="input_boottom" />    
   <input type="submit" value="<%=Mytrip_Mvc_Language.next%>" class="input_boottom" />
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

