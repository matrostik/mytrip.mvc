<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/1_column.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Mvc.Model.Linq2sql.mt_model>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Mytrip_Mvc_Language_1.email_setting1%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=Mytrip_Mvc_Language_1.email_setting1%></h2>

    <% using (Html.BeginForm()) {%>

        <fieldset>
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
                 <label for="Port">Port:</label>
                <%= Html.TextBox("Port")%><br />
                <%= Html.ValidationMessageFor(model => model.Port)%>
            </p>
            <p>
                 <label for="Login_email">Login:</label>
                <%= Html.TextBox("Login_email")%><br />
                <%= Html.ValidationMessageFor(model => model.Login_email)%>
            </p>
             <p>
                 <label for="Password_email">Password:</label>
                <%= Html.Password("Password_email")%><br />
                <%= Html.ValidationMessageFor(model => model.Password_email)%>
            </p>
            <p>
                <label for="EnableSsl">Ssl</label>
                <%= Html.CheckBox("EnableSsl")%>
               
            </p>
            
            <p>
               <input type="submit" value="<%=Mytrip_Mvc_Language_1.save%>" class="input_boottom" />
            </p>
        </fieldset>

    <% } %>



</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

