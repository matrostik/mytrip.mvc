<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/1_column.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Mvc.Model.Linq2sql.mt_artycle_teg>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= ViewData["model_domain"]%>/<%=Mytrip_Mvc_Language_1.edit_teg%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright ©  2009 Стюхин Олег Анатольевич   */-->
    <h2><%=Mytrip_Mvc_Language_1.edit_teg%></h2>


    <% using (Html.BeginForm()) {%>

        <fieldset>
          
              <p>
                 <label for="Title"><%=Mytrip_Mvc_Language_1.title%></label><br />
                <%= Html.TextBox("Title")%><br />
                <%= Html.ValidationMessageFor(model => model.Title) %>
            </p>                    
                    
             <div class="button">
   <input type="button" onclick="location.href('<%= ViewData["url"] %>')" value="<%=Mytrip_Mvc_Language_1.back%>" class="input_boottom" />    
   <input type="submit" value="<%=Mytrip_Mvc_Language_1.save%>" class="input_boottom" />
  </div> </fieldset>
    <% } %>  

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

