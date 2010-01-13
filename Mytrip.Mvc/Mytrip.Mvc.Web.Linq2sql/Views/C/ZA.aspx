<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/1_column.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Mvc.Model.Linq2sql.mt_artycle_category>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= ViewData["model_domain"]%>/<%string c = ViewData["create"].ToString();
  if (c == "Create_article_heding") 
  {%><%=Mytrip_Mvc_Language_2.menu_create_heading_article%><% }
  if (c == "Create_news_heding")
  {%><%=Mytrip_Mvc_Language_2.menu_create_heading_news%><%}
  if (c == "Create_article_subheding")
  {%><%=Mytrip_Mvc_Language_2.menu_create_subheading_article%><%}
  if (c == "Create_news_subheding")
  {%><%=Mytrip_Mvc_Language_2.menu_create_subheading_news%><%}
  if (c == "Create_blog_heading")
  {%><%=Mytrip_Mvc_Language_1.menu_create_blog_theme%><%}%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright ©  2009 Стюхин Олег Анатольевич   */-->
<%string c = ViewData["create"].ToString();
  if (c == "Create_article_heding") 
  {%><h2><%=Mytrip_Mvc_Language_2.menu_create_heading_article%></h2><% }
  if (c == "Create_news_heding")
  {%><h2><%=Mytrip_Mvc_Language_2.menu_create_heading_news%></h2><%}
  if (c == "Create_article_subheding")
  {%><h2><%=Mytrip_Mvc_Language_2.menu_create_subheading_article%></h2><%}
  if (c == "Create_news_subheding")
  {%><h2><%=Mytrip_Mvc_Language_2.menu_create_subheading_news%></h2><%}
  if (c == "Create_blog_heading")
  {%><h2><%=Mytrip_Mvc_Language_1.menu_create_blog_theme%></h2><%}%>


    <% using (Html.BeginForm()) {%>

        <fieldset>           
              <p>
                 <label for="Title"><%=Mytrip_Mvc_Language_1.title%></label>
                <%= Html.TextBox("Title")%><br />
                <%= Html.ValidationMessageFor(model => model.Title) %>
            </p>
             <%bool d = (bool)ViewData["create_heding"];
               if (d == true)
               { %>           
            <p> <label for="AddMenu"><%=Mytrip_Mvc_Language_1.add_home_page%></label>              
                <%= Html.CheckBox("AddMenu")%>              
            </p>            
            <%bool a = (bool)ViewData["lang_panel"];
              if (a == true)
              { %>
              <p> <label for="AllCulture"><%=Mytrip_Mvc_Language_2.view_all_culture%></label>              
                <%= Html.CheckBox("AllCulture")%>               
            </p>
              <%}
               } %>
                         
            <div class="button">
   <input type="button" onclick="location.href('<%=ViewData["url"] %>')" value="<%=Mytrip_Mvc_Language_1.back%>" class="input_boottom" />    
   <input type="submit" value="<%=Mytrip_Mvc_Language_1.create%>" class="input_boottom" />
  </div>
        </fieldset>

    <% } %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

