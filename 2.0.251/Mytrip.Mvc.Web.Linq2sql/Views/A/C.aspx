<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/1_column.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Mvc.Model.Linq2sql.mt_model>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Mytrip_Mvc_Language.menu_site_adjustment%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright �  2009 ������ ���� �����������   */-->
    <h2><%=Mytrip_Mvc_Language.menu_site_adjustment%></h2>

    <% using (Html.BeginForm()) {%>

        <fieldset><legend><%=Mytrip_Mvc_Language.Domain_name%></legend>
             <p>
                 <label for="DomainName"><%=Mytrip_Mvc_Language.domain_name_text%></label>
                <%= Html.TextBox("DomainName")%><br />
                <%= Html.ValidationMessageFor(model => model.DomainName)%>
            </p>
             </fieldset>
              <fieldset><legend><%=Mytrip_Mvc_Language.Content%></legend>
              <p>
                <label for="LanguageId"><%=Mytrip_Mvc_Language.lang_setting%></label>
                <%= Html.DropDownList("LanguageId", (IEnumerable<SelectListItem>)ViewData["language1"])%>
                <%= Html.ValidationMessageFor(model => model.LanguageId)%>
            </p>
            <p>
                <label for="Language_approved"><%=Mytrip_Mvc_Language.display_lang%></label>
                <%= Html.CheckBox("Language_approved")%>               
            </p>
              <p>
                <label for="Artycle"><%=Mytrip_Mvc_Language.include_articles%></label>
                <%= Html.CheckBox("Artycle")%>               
            </p>
               <p>
                <label for="News"><%=Mytrip_Mvc_Language.include_news%></label>
                <%= Html.CheckBox("News")%>
               
            </p>           
            <p>
                <label for="Blog"><%=Mytrip_Mvc_Language.include_blogs%></label>
                <%= Html.CheckBox("Blog")%>
               
            </p>
              <p>
                 <label for="CountComment"><%=Mytrip_Mvc_Language.count_comment%></label>
                <%= Html.TextBox("CountComment")%><br />
                <%= Html.ValidationMessageFor(model => model.CountComment)%>
            </p>          
        </fieldset>
              <fieldset><legend><%=Mytrip_Mvc_Language.reg_users%></legend>
          <p>
                <label for="Blog"><%=Mytrip_Mvc_Language.include_captcha%></label>
                <%= Html.CheckBox("Captcha_approved")%>
               
            </p>
            <p><%int emailset = (int)ViewData["emailset"]; if (emailset == 1) {%>
                <label for="Blog"><%=Mytrip_Mvc_Language.include_email%></label><% }
           if (emailset == 2) {%>
                <label for="Blog"><%=Mytrip_Mvc_Language.include_email1%></label><% }%>
                <%= Html.CheckBox("Email_approved")%>
               
            </p>
            </fieldset>
            
            <p>
                <input type="submit" value="<%=Mytrip_Mvc_Language.save%>" class="input_boottom" />
            </p>
       

    <% } %>
   
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

