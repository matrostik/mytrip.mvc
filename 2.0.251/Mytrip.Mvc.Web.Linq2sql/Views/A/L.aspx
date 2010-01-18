<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/1_column.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Mvc.Model.Linq2sql.mt_model>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Mytrip_Mvc_Language.email_setting1%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=Mytrip_Mvc_Language.email_setting1%></h2>

    <% using (Html.BeginForm()) {%>

        <fieldset>
              <p>
                 <label for="Email"><%=Mytrip_Mvc_Language.email%></label>
                <%= Html.TextBox("Email")%><br />
                <%= Html.ValidationMessageFor(model => model.Email)%>
            </p>
            <p>
                 <label for="Smtp"><%=Mytrip_Mvc_Language.smtp%></label>
                <%= Html.TextBox("Smtp")%><br />
                <%= Html.ValidationMessageFor(model => model.Smtp)%>
            </p>
            <p>
                 <label for="Port"><%=Mytrip_Mvc_Language.port%></label>
                <%= Html.TextBox("Port")%><br />
                <%= Html.ValidationMessageFor(model => model.Port)%>
            </p>
            <p>
                 <label for="Login_email"><%=Mytrip_Mvc_Language.username%></label>
                <%= Html.TextBox("Login_email")%><br />
                <%= Html.ValidationMessageFor(model => model.Login_email)%>
            </p>
             <p>
                 <label for="Password_email"><%=Mytrip_Mvc_Language.password%></label>
                <%= Html.Password("Password_email", Model.Password_email)%><br />
                <%= Html.ValidationMessageFor(model => model.Password_email)%>
            </p>
            <p>
                <label for="EnableSsl"><%=Mytrip_Mvc_Language.Ssl%></label>
                <%= Html.CheckBox("EnableSsl")%>
               
            </p>
            
            <p>
               <input type="submit" value="<%=Mytrip_Mvc_Language.save%>" class="input_boottom" />
            </p>
        </fieldset>

    <% } %>



</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

