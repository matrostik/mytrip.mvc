<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/1_column.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Mvc.Model.Linq2sql.mt_artycle_category>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= ViewData["model_domain"]%>/������� ����������
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright �  2009 ������ ���� �����������   */-->
    <h2>������� ����������</h2>


    <% using (Html.BeginForm()) {%>

        <fieldset>           
              <p>
                 <label for="Title">�������� ����������:</label>
                <%= Html.TextBox("Title")%><br />
                <%= Html.ValidationMessageFor(model => model.Title) %>
            </p>            
                       
            <div class="button">
   <input type="button" onclick="location.href('<%=ViewData["url"] %>')" value="�����" class="input_boottom"  />    
   <input type="submit" value="�������" class="input_boottom"  />
  </div>
        </fieldset>

    <% } %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

