<%@ Page Language="C#" MasterPageFile="~/Views/Shared/1.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Mvc.Web.Linq2sql.Models.RegisterModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
   <%=Mytrip_Mvc_Language.reg_admin%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright �  2009 ������ ���� ����������� oleg@stuh.in 
��� ��������� �������� ��������� ����������� ���������; �� ������ �������������� �/��� �������� 
�� � ������������ � ��������� ����������� ������������ �������� GNU � ��� ����, ��� ��� ���� ������������ 
������ ���������� ��; ���� ������ 2 �������� ���� (�� ������ �������) ����� ����� ������� ������.
��������� ���������������� � �������, ��� ��� ����� ��������, �� ��� ����� �� �� �� ���� ����������� ������������;
���� ��� ��������� ����������� ������������, ��������� � ���������������� ���������� � ������������ ��� ������������ �����. 
��� ������������ �������� ����������� ������������ �������� GNU.
�� ������ ���� �������� ����� ����������� ������������ �������� GNU ������ � ���� ����������; 
���� ��� �� ���, �������� � ���� ���������� �� 
(Free Software Foundation, Inc., 675 Mass Ave, Cambridge, MA 02139, USA).  */-->
    <h2><%=Mytrip_Mvc_Language.reg_admin%></h2>
    
    <p>
        <%=Html.Language(Mytrip_Mvc_Language.register_text, ViewData["PasswordLength"].ToString())%> 
    </p>
   <%= Html.ValidationSummary(Mytrip_Mvc_Language.error)%>
    <% using (Html.BeginForm()) { %>
        <div>
            <fieldset>
              
                <p>
                    <label for="UserName"><%=Mytrip_Mvc_Language.username%></label>
                    <%= Html.TextBoxFor(m => m.UserName) %>
                    <%= Html.ValidationMessageFor(m => m.UserName) %>
                </p>
                <p>
                    <label for="Email"><%=Mytrip_Mvc_Language.email%></label>
                    <%= Html.TextBoxFor(m => m.Email) %>
                    <%= Html.ValidationMessageFor(m => m.Email) %>
                </p>
                <p>
                    <label for="Password"><%=Mytrip_Mvc_Language.password%></label>
                    <%= Html.Password("Password") %>
                    <%= Html.ValidationMessageFor(m => m.Password) %>
                </p>
                <p>
                    <label for="ConfirmPassword"><%=Mytrip_Mvc_Language.confirm_password%></label>
                    <%= Html.Password("ConfirmPassword") %>
                    <%= Html.ValidationMessageFor(m => m.ConfirmPassword) %>
                </p><br />
              
                <p>
                    <input type="submit" value="<%=Mytrip_Mvc_Language.menu_register %>" class="input_boottom" />
                </p>
            </fieldset>
        </div>
    <% } %>
</asp:Content>

