<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/1_column.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Mvc.Model.Linq2sql.mt_model>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Настройка сайта
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright ©  2009 Стюхин Олег Анатольевич   */-->
    <h2>Настройка сайта</h2>

    <% using (Html.BeginForm()) {%>

        <fieldset><legend>Домен</legend>
             <p>
                 <label for="DomainName">Доменное имя отоброжается в титле страницы и прописывается в обратный адрес RSS, если доменное имя введено неправильно обратные ссылки RSS будут работать неправильно (http:// не вводить):</label>
                <%= Html.TextBox("DomainName")%><br />
                <%= Html.ValidationMessageFor(model => model.DomainName)%>
            </p>
             </fieldset>
              <fieldset><legend>Контент</legend>
              <p>
                <label for="Blog">Включить статьи? (по умолчанию включены)</label>
                <%= Html.CheckBox("Artycle")%>
               
            </p>
               <p>
                <label for="News">Включить новости? (по умолчанию включены)</label>
                <%= Html.CheckBox("News")%>
               
            </p>           
            <p>
                <label for="Blog">Включить блоги? (по умолчанию включены)</label>
                <%= Html.CheckBox("Blog")%>
               
            </p>
              <p>
                 <label for="CountComment">Количество комментариев которые необходимо написать пользователю для активации его блога (комментарии учитываются по одному за каждый день):</label>
                <%= Html.TextBox("CountComment")%><br />
                <%= Html.ValidationMessageFor(model => model.CountComment)%>
            </p>          
        </fieldset>
              <fieldset><legend>Регистрация пользователей</legend>
          <p>
                <label for="Blog">Включить CAPTCHA? (по умолчанию включена)</label>
                <%= Html.CheckBox("Captcha_approved")%>
               
            </p>
            <p>
                <label for="Blog">Включить подтверждение пользователя по Email? (по умолчанию выключена)</label>
                <%= Html.CheckBox("Email_approved")%>
               
            </p>
            </fieldset>
              <fieldset><legend>Настройка Email для отправки писем с сайта</legend>
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
                 <label for="Port">Порт:</label>
                <%= Html.TextBox("Port")%><br />
                <%= Html.ValidationMessageFor(model => model.Port)%>
            </p>
            <p>
                 <label for="Login_email">Логин для входа на почтовый ящик:</label>
                <%= Html.TextBox("Login_email")%><br />
                <%= Html.ValidationMessageFor(model => model.Login_email)%>
            </p>
             <p>
                 <label for="Password_email">Пароль для входа на почтовый ящик:</label>
                <%= Html.Password("Password_email")%><br />
                <%= Html.ValidationMessageFor(model => model.Password_email)%>
            </p>
            <p>
                <label for="EnableSsl">Включить Ssl? (по умолчанию выключен)</label>
                <%= Html.CheckBox("EnableSsl")%>
               
            </p>
            <p>
                <input type="submit" value="сохранить" class="input_boottom" />
            </p>
        </fieldset>

    <% } %>
   
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

