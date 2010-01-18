<%@ Page Language="C#" MasterPageFile="~/Views/Shared/1_column.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Mvc.Web.Linq2sql.Models.ChangePasswordModel>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
    <%= ViewData["abstract_model_domain"]%>/<%=Mytrip_Mvc_Language.menu_change_password%>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright ©  2009 Стюхин Олег Анатольевич   */-->
    <h2><%=Mytrip_Mvc_Language.menu_change_password%> </h2>
    
    <p>
       <%=Html.Language(Mytrip_Mvc_Language.register_text, ViewData["PasswordLength"].ToString())%> 
    </p>
     <%= Html.ValidationSummary(Mytrip_Mvc_Language.error)%>

    <% using (Html.BeginForm()) { %>
        <div>
            <fieldset>
                
                <p>
                   <label for="OldPassword"><%=Mytrip_Mvc_Language.old_password%></label>
                    <%= Html.Password("OldPassword") %>
                    <%= Html.ValidationMessageFor(m => m.OldPassword) %>
                </p>
                <p>
                    <label for="NewPassword"><%=Mytrip_Mvc_Language.new_password%></label>
                    <%= Html.Password("NewPassword") %>
                    <%= Html.ValidationMessageFor(m => m.NewPassword) %>
                </p>
                <p>
                   <label for="ConfirmPassword"><%=Mytrip_Mvc_Language.confirm_password%></label>
                    <%= Html.Password("ConfirmPassword") %>
                    <%= Html.ValidationMessageFor(m => m.ConfirmPassword) %>
                </p>
                <p>
                    <input type="submit" value="<%=Mytrip_Mvc_Language.menu_change_password%>" class="input_boottom" />
                </p>
            </fieldset>
        </div>
    <% } %>
</asp:Content>
