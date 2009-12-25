<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/1_column.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Mvc.Model.Linq2sql.mt_artycle_category>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= ViewData["model_domain"]%>/Создать рубрику
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright ©  2009 Стюхин Олег Анатольевич   */-->
    <h2>Создать рубрику</h2>


    <% using (Html.BeginForm()) {%>

        <fieldset>           
              <p>
                 <label for="Title">Название рубрики:</label>
                <%= Html.TextBox("Title")%><br />
                <%= Html.ValidationMessageFor(model => model.Title) %>
            </p>            
            <p>
                
                <%= Html.CheckBox("AddMenu")%><label for="AddMenu">Добавить на главную?</label>
               
            </p>           
            <div class="button">
   <input type="button" onclick="location.href('<%=ViewData["url"] %>')" value="назад" class="input_boottom" />    
   <input type="submit" value="создать" class="input_boottom" />
  </div>
        </fieldset>

    <% } %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

