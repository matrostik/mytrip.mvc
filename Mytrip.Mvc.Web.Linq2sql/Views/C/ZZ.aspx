<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/1_column.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Mvc.Model.Linq2sql.mt_artycle_comment>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= ViewData["model_domain"]%>/������������� �����������
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright �  2009 ������ ���� �����������   */-->
    <h2>������������� �����������</h2>


    <% using (Html.BeginForm()) {%>

        <fieldset>
          
            <p>
       <%= Html.TextArea("Body", new { style = "height: 200px" })%><br />
       <%= Html.ValidationMessage("Body", "*")%> 
            </p>
          
              <div class="button">
   <input type="button" onclick="location.href('<%= ViewData["url"] %>')" value="�����" class="input_boottom" />    
   <input type="submit" value="���������" class="input_boottom" />
  </div>
        </fieldset>

    <% } %>

  

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
  <script src="../../../Scripts/tiny_mce/tiny_mce_src.js" type="text/javascript"></script>
    <script type="text/javascript">
        tinyMCE.init({
        mode: "textareas"
        
        
        });
</script>
</asp:Content>

