<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/1_column.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Mvc.Model.Linq2sql.mt_model>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	��������� �����
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright �  2009 ������ ���� �����������   */-->
    <h2>��������� �����</h2>

    <% using (Html.BeginForm()) {%>

        <fieldset><legend>�����</legend>
             <p>
                 <label for="DomainName">�������� ��� ������������ � ����� �������� � ������������� � �������� ����� RSS, ���� �������� ��� ������� ����������� �������� ������ RSS ����� �������� ����������� (http:// �� �������):</label>
                <%= Html.TextBox("DomainName")%><br />
                <%= Html.ValidationMessageFor(model => model.DomainName)%>
            </p>
             </fieldset>
              <fieldset><legend>�������</legend>
              <p>
                <label for="Blog">�������� ������? (�� ��������� ��������)</label>
                <%= Html.CheckBox("Artycle")%>
               
            </p>
               <p>
                <label for="News">�������� �������? (�� ��������� ��������)</label>
                <%= Html.CheckBox("News")%>
               
            </p>           
            <p>
                <label for="Blog">�������� �����? (�� ��������� ��������)</label>
                <%= Html.CheckBox("Blog")%>
               
            </p>
              <p>
                 <label for="CountComment">���������� ������������ ������� ���������� �������� ������������ ��� ��������� ��� ����� (����������� ����������� �� ������ �� ������ ����):</label>
                <%= Html.TextBox("CountComment")%><br />
                <%= Html.ValidationMessageFor(model => model.CountComment)%>
            </p>          
        </fieldset>
              <fieldset><legend>����������� �������������</legend>
          <p>
                <label for="Blog">�������� CAPTCHA? (�� ��������� ��������)</label>
                <%= Html.CheckBox("Captcha_approved")%>
               
            </p>
            <p>
                <label for="Blog">�������� ������������� ������������ �� Email? (�� ��������� ���������)</label>
                <%= Html.CheckBox("Email_approved")%>
               
            </p>
            </fieldset>
              <fieldset><legend>��������� Email ��� �������� ����� � �����</legend>
              <p>
                 <label for="Email">Email:</label>
                <%= Html.TextBox("Email")%><br />
                <%= Html.ValidationMessageFor(model => model.Email)%>
            </p>
            <p>
                 <label for="Smtp">smtp:</label>
                <%= Html.TextBox("Smtp")%><br />
                <%= Html.ValidationMessageFor(model => model.Smtp)%>
            </p>
            <p>
                 <label for="Port">����:</label>
                <%= Html.TextBox("Port")%><br />
                <%= Html.ValidationMessageFor(model => model.Port)%>
            </p>
            <p>
                 <label for="Login_email">����� ��� ����� �� �������� ����:</label>
                <%= Html.TextBox("Login_email")%><br />
                <%= Html.ValidationMessageFor(model => model.Login_email)%>
            </p>
             <p>
                 <label for="Password_email">������ ��� ����� �� �������� ����:</label>
                <%= Html.Password("Password_email")%><br />
                <%= Html.ValidationMessageFor(model => model.Password_email)%>
            </p>
            <p>
                <label for="EnableSsl">�������� Ssl? (�� ��������� ��������)</label>
                <%= Html.CheckBox("EnableSsl")%>
               
            </p>
            <p>
                <input type="submit" value="���������" class="input_boottom" />
            </p>
        </fieldset>

    <% } %>
   
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

