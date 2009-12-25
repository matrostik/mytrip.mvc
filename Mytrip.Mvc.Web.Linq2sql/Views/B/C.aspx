<%@ Page Language="C#" MasterPageFile="~/Views/Shared/1_column.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Mvc.Model.Linq2sql.Models.ChangePasswordModel>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
    <%= ViewData["model_domain"]%>/Изменение пароля
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright ©  2009 Стюхин Олег Анатольевич   */-->
    <h2>Сменить пароль</h2>
    
    <p>
       Длина пароля должна иметь не менее <%= Html.Encode(ViewData["PasswordLength"]) %> знаков.
    </p>
    <%= Html.ValidationSummary("Ошибка!") %>

    <% using (Html.BeginForm()) { %>
        <div>
            <fieldset>
                
                <p>
                   <label for="OldPassword">Старый пароль:</label>
                    <%= Html.Password("OldPassword") %>
                    <%= Html.ValidationMessageFor(m => m.OldPassword) %>
                </p>
                <p>
                    <label for="NewPassword">Новый пароль:</label>
                    <%= Html.Password("NewPassword") %>
                    <%= Html.ValidationMessageFor(m => m.NewPassword) %>
                </p>
                <p>
                   <label for="ConfirmPassword">Повторите новый пароль:</label>
                    <%= Html.Password("ConfirmPassword") %>
                    <%= Html.ValidationMessageFor(m => m.ConfirmPassword) %>
                </p>
                <p>
                    <input type="submit" value="Сменить пароль" class="input_boottom" />
                </p>
            </fieldset>
        </div>
    <% } %>
</asp:Content>
