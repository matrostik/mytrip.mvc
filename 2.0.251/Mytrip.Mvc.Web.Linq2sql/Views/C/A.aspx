<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/2_column.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
  
    <%= ViewData["abstract_model_domain"]%>/
    <%string a = ViewData["helper_menu_url"].ToString();
      if (a == "artycles")
      { %>
    <%=Mytrip_Mvc_Language.menu_artycles%>
    <%} if (a == "news")
      { %>
    <%=Mytrip_Mvc_Language.menu_news%>
    <%} if (a == "blogs")
      { %>
    <%=Mytrip_Mvc_Language.menu_blogs%>
    <%} %>
    
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright ©  2009 Ñòþõèí Îëåã Àíàòîëüåâè÷   */-->
    <%string a = ViewData["helper_menu_url"].ToString();
      if (a == "artycles")
      { %>
    <!-- ÀÄÌÈÍ ×ÀÑÒÜ -->
    <%int cat = (int)ViewData["count_canegory"];%>
    <div class="edit_content">
    <%= Html.CreateCategory(false) %>
    <%= Html.CreateArtycle(cat, false) %>
      </div>   
    <!--END ÀÄÌÈÍ ×ÀÑÒÜ -->
    <!-- ÇÀÃÎËÎÂÎÊ -->
<a href="<%=Url.Action("Rss_B", "C")%>">
    <img src="/content/images/feed.png" alt="rss" class="rss_category" /></a>
    <h2><%=Mytrip_Mvc_Language.menu_artycles%></h2>
    <!--END ÇÀÃÎËÎÂÎÊ -->
    <%} if (a == "news")
      { %>
    <!-- ÀÄÌÈÍ ×ÀÑÒÜ -->
    <%int cat = (int)ViewData["count_canegory"];%>
    <div class="edit_content">
    <%= Html.CreateCategory(true) %>
    <%= Html.CreateArtycle(cat, true) %>
      </div>  
    <!--END ÀÄÌÈÍ ×ÀÑÒÜ -->
       <!-- ÇÀÃÎËÎÂÎÊ -->
<a href="<%=Url.Action("Rss_B", "C")%>">
    <img src="/content/images/feed.png" alt="rss" class="rss_category" /></a>
    <h2><%=Mytrip_Mvc_Language.menu_news%></h2>
    <!--END ÇÀÃÎËÎÂÎÊ -->
    <%} if (a == "blogs")
      { %>
    <!-- ÇÀÃÎËÎÂÎÊ -->
<a href="<%=Url.Action("Rss_B", "C")%>">
    <img src="/content/images/feed.png" alt="rss" class="rss_category" /></a>
    <h2><%=Mytrip_Mvc_Language.menu_blogs%></h2>
    <!--END ÇÀÃÎËÎÂÎÊ -->
    <%} %> 
    <!-- ÊÎÍÒÅÍÒ -->
    <% Html.RenderPartial("_S_pager_all"); %>
    <%if (a == "blogs")
      { %>
    <% Html.RenderPartial("_blog"); %>
    <%}
      else {%><% Html.RenderPartial("_artycle"); %><% } %>    
    <% Html.RenderPartial("_S_pager_all"); %>
    <!--END ÊÎÍÒÅÍÒ -->
</asp:Content>
<asp:Content ContentPlaceHolderID="RightContent" runat="server">
    <!-- ÏÐÀÂÀß ÊÎËÎÍÊÀ -->
<% Html.RenderPartial("_S_right_column"); %> 
    <!--END ÏÐÀÂÀß ÊÎËÎÍÊÀ -->
</asp:Content>
