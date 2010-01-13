<%@ Page Language="C#" MasterPageFile="~/Views/Shared/1_column.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Mvc.Web.Linq2sql.Models.LogOnModel>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
    <%= ViewData["model_domain"]%>/<%=Mytrip_Mvc_Language_1.menu_logon %>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright ©  2009 Стюхин Олег Анатольевич   */-->
   <%bool captcha = (bool)ViewData["captcha"]; %> 
    <h2><%=Mytrip_Mvc_Language_1.menu_logon %></h2>
    <p><%=Html.Language(Mytrip_Mvc_Language_1.logon_text, ViewData["model_domain"].ToString())%>
          <%if (captcha == false)
            {%> <%= Html.ActionLink(Mytrip_Mvc_Language_1.logon_register, "B")%><%} else {%> <%= Html.ActionLink(Mytrip_Mvc_Language_1.logon_register, "H")%><% } %>.
    </p>
       <%= Html.ValidationSummary(Mytrip_Mvc_Language_1.error)%>

    <% using (Html.BeginForm()) { %>
        <div>
            <fieldset>
               
                <p>
                    <label for="UserName"><%=Mytrip_Mvc_Language_1.username%></label>
                    <%= Html.TextBoxFor(m => m.UserName) %>
                    <%= Html.ValidationMessageFor(m => m.UserName) %>
                </p>
                <p>
                    <label for="Password"><%=Mytrip_Mvc_Language_1.password%></label>
                    <%= Html.Password("Password") %>
                    <%= Html.ValidationMessageFor(m => m.Password) %>
                </p>
                <p>
                    <%= Html.CheckBox("rememberMe") %> <label class="inline" for="rememberMe"><%=Mytrip_Mvc_Language_1.rememberme%></label>
                </p>
                <p>
                    <input type="submit" value="<%=Mytrip_Mvc_Language_1.menu_logon %>" class="input_boottom" />
                </p>
            </fieldset>
        </div>
    <% } %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">


</asp:Content>