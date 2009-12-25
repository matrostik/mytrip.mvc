<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/1_column.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Mvc.Model.Linq2sql.mt_artycle>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= ViewData["model_domain"]%>/Написать статью
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright ©  2009 Стюхин Олег Анатольевич   */-->
    <h2>Написать статью</h2>
<br />

    <% using (Html.BeginForm()) {%>

       
        <div style="position: relative; float: right; width: 300px; padding-top: 100px;">
        
	    
	<p>
        
<a href="#" onclick= "window.open('/G/B/()UserFile()<%= Html.Encode(Page.User.Identity.Name) %>', 'myWindow', 'tooldar=0, menubar=0, height=600, width=1000, scrollbars=1');">
Менеджер файлов</a>
       </p>
        
       
            <p>
                <label for="UrlImageDescription">Url фото к краткому:</label>
                <%= Html.TextBox("UrlImageDescription", "null")%>
                <%= Html.ValidationMessageFor(model => model.UrlImageDescription) %>
            </p>
            <p>
                <label for="UrlImageBody">Url фото к статье:</label>                
                <%= Html.TextBox("UrlImageBody", "null")%>
                <%= Html.ValidationMessageFor(model => model.UrlImageBody) %>
            </p>
            <p>
            
                
                <%= Html.CheckBox("DescriptionBody")%>
                <label for="DescriptionBody">Фото к краткому как фото к статье?</label>
                
            </p>
          
            <p>
                
                <%= Html.CheckBox("RegistrUser")%><label for="RegistrUser">Только для зарегестрированных?</label>
              
            </p>
             <p>
                
                <%= Html.CheckBox("AddComment")%><label for="AddComment">Разрешить комментарии?</label>
                
            </p>
            <p>
                
                <%= Html.CheckBox("ApprovedVotes")%><label for="ApprovedVotes">Разрешить голосование?</label>
                
            </p>
          
	<p>
                <label for="CloseDate">Дата закрытия:</label>
                <%= Html.TextBox("CloseDate", DateTime.MaxValue)%>
                <%= Html.ValidationMessageFor(model => model.CloseDate) %>
            </p></div>
         <p>
                <label for="CategoryId">Выберите рубрику:</label>
                <%= Html.DropDownList("a", (IEnumerable<SelectListItem>)ViewData["Category"])%>
                <%= Html.ValidationMessageFor(model => model.CategoryId) %>
            </p>
	
            
            <p>
               <label for="Title">Заголовок:</label>
                <%= Html.TextBox("Title")%>
                <%= Html.ValidationMessageFor(model => model.Title) %>
            </p>
            <p>
                <label for="Description">Кратко:</label>
                <%= Html.TextArea("Description", new { style = "height: 250px" })%>
                <%= Html.ValidationMessageFor(model => model.Description) %>
            </p>
           
            <p>
           
                <label for="Body">Статья:</label>
                <%= Html.TextArea("Body", new { style = "height: 600px" })%>
                <%= Html.ValidationMessageFor(model => model.Body) %>
            </p>            
	
	<div class="button">
   <input type="button" onclick="location.href('<%= ViewData["url"] %>')" value="назад" class="input_boottom" />    
   <input type="submit" value="далее" class="input_boottom" />
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

