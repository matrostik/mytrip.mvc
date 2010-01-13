<%@ Page Language="C#" MasterPageFile="~/Views/Shared/1_column.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Mvc.Web.Linq2sql.Models.RegisterModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%= ViewData["model_domain"]%>/<%=Mytrip_Mvc_Language_1.menu_register %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright ©  2009 Стюхин Олег Анатольевич   */-->
    <h2>
        <%=Mytrip_Mvc_Language_1.menu_register %></h2>
    <p>
        <%=Html.Language(Mytrip_Mvc_Language_1.register_text, ViewData["PasswordLength"].ToString())%>       
    </p>
      <%= Html.ValidationSummary(Mytrip_Mvc_Language_1.error)%>
    <% using (Html.BeginForm())
       { %>
    <div>
        <fieldset>
            <p>
                <label for="UserName">
                    <%=Mytrip_Mvc_Language_1.username%></label>
                <%= Html.TextBoxFor(m => m.UserName) %> 
                <%= Html.ValidationMessageFor(m => m.UserName) %>
            </p>
            <p>
                <label for="Email">
                    <%=Mytrip_Mvc_Language_1.email%></label>
                <%= Html.TextBoxFor(m => m.Email) %> 
                <%= Html.ValidationMessageFor(m => m.Email) %>
            </p>
            <p>
                <label for="Password">
                    <%=Mytrip_Mvc_Language_1.password%></label>
                <%= Html.Password("Password") %>
                <%= Html.ValidationMessageFor(m => m.Password) %>
            </p>
            <p>
                <label for="ConfirmPassword">
                    <%=Mytrip_Mvc_Language_1.confirm_password%></label>
                <%= Html.Password("ConfirmPassword") %>
                <%= Html.ValidationMessageFor(m => m.ConfirmPassword) %>
            </p>           
                       <div style="width: 200px"><label><%=Mytrip_Mvc_Language_1.verifying_phrase%></label>
                 
               <%= Html.GenerateCaptcha() %>
</div><br />
               <p>
                <input type="submit" value="<%=Mytrip_Mvc_Language_1.menu_register %>" class="input_boottom" />
            </p>
        </fieldset>
    </div>
    <% } %>
</asp:Content>
