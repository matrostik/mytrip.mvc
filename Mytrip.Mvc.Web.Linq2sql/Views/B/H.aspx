<%@ Page Language="C#" MasterPageFile="~/Views/Shared/1_column.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Mvc.Model.Linq2sql.Models.RegisterModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%= ViewData["model_domain"]%>/�����������
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright �  2009 ������ ���� �����������   */-->
    <h2>
        �����������</h2>
    <p>
        ����� ������ ������ ����� �� �����
        <%= Html.Encode(ViewData["PasswordLength"]) %>
        ������.
    </p>
      <%= Html.ValidationSummary("������!") %>
    <% using (Html.BeginForm())
       { %>
    <div>
        <fieldset>
            <p>
                <label for="UserName">
                    �����:</label>
                <%= Html.TextBoxFor(m => m.UserName) %> 
                <%= Html.ValidationMessageFor(m => m.UserName) %>
            </p>
            <p>
                <label for="Email">
                    Email:</label>
                <%= Html.TextBoxFor(m => m.Email) %> 
                <%= Html.ValidationMessageFor(m => m.Email) %>
            </p>
            <p>
                <label for="Password">
                    ������:</label>
                <%= Html.Password("Password") %>
                <%= Html.ValidationMessageFor(m => m.Password) %>
            </p>
            <p>
                <label for="ConfirmPassword">
                    ��������� ������:</label>
                <%= Html.Password("ConfirmPassword") %>
                <%= Html.ValidationMessageFor(m => m.ConfirmPassword) %>
            </p>           
                       <div style="width: 200px"><label>����������� �����:</label>
                 
               <%= Html.GenerateCaptcha() %>
</div><br />
               <p>
                <input type="submit" value="�����������" class="input_boottom" />
            </p>
        </fieldset>
    </div>
    <% } %>
</asp:Content>
