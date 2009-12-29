<%@ Page Language="C#" MasterPageFile="~/Views/Shared/1_column.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Mvc.Model.Linq2sql.Models.LogOnModel>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
    <%= ViewData["model_domain"]%>/Вход
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright ©  2009 Стюхин Олег Анатольевич   */-->
   <%bool captcha = (bool)ViewData["captcha"]; %> 
    <h2>Вход</h2>
    <p>
        Если у Вас нет аккаунта на сайте <b><%= ViewData["model_domain"]%></b> воспользуйтесь  <%if (captcha == false)
                                                                                               { %> <%= Html.ActionLink("Регистрацией", "B")%><%}
                                                                                               else {%> <%= Html.ActionLink("Регистрацией", "H")%><% } %>.
    </p>
       <%= Html.ValidationSummary("Ошибка!") %>

    <% using (Html.BeginForm()) { %>
        <div>
            <fieldset>
               
                <p>
                    <label for="UserName">Логин:</label>
                    <%= Html.TextBoxFor(m => m.UserName) %>
                    <%= Html.ValidationMessageFor(m => m.UserName) %>
                </p>
                <p>
                    <label for="Password">Пароль:</label>
                    <%= Html.Password("Password") %>
                    <%= Html.ValidationMessageFor(m => m.Password) %>
                </p>
                <p>
                    <%= Html.CheckBox("rememberMe") %> <label class="inline" for="rememberMe">Запомнить?</label>
                </p>
                <p>
                    <input type="submit" value="Вход" class="input_boottom" />
                </p>
            </fieldset>
        </div>
    <% } %>
</asp:Content>
