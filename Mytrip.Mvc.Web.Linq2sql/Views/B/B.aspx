<%@ Page Language="C#" MasterPageFile="~/Views/Shared/1_column.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Mvc.Model.Linq2sql.Models.RegisterModel>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
    <%= ViewData["model_domain"]%>/Регистрация
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright ©  2009 Стюхин Олег Анатольевич   */-->
    <h2>Регистрация</h2>
    
    <p>
        Длина пароля должна иметь не менее <%= Html.Encode(ViewData["PasswordLength"]) %> знаков.
    </p>
   
    <% using (Html.BeginForm()) { %>
        <div>
            <fieldset>
              
                <p>
                    <label for="UserName">Логин:</label>
                    <%= Html.TextBoxFor(m => m.UserName) %>
                    <%= Html.ValidationMessageFor(m => m.UserName) %>
                </p>
                <p>
                    <label for="Email">Email:</label>
                    <%= Html.TextBoxFor(m => m.Email) %>
                    <%= Html.ValidationMessageFor(m => m.Email) %>
                </p>
                <p>
                    <label for="Password">Пароль:</label>
                    <%= Html.Password("Password") %>
                    <%= Html.ValidationMessageFor(m => m.Password) %>
                </p>
                <p>
                    <label for="ConfirmPassword">Повторите пароль:</label>
                    <%= Html.Password("ConfirmPassword") %>
                    <%= Html.ValidationMessageFor(m => m.ConfirmPassword) %>
                </p><br />

                <p>
                    <input type="submit" value="Регистрация" class="input_boottom" />
                </p>
            </fieldset>
        </div>
    <% } %>
</asp:Content>
