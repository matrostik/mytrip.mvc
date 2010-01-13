<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/2_column.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
  
    <%= ViewData["model_domain"]%>/
    <%string a = ViewData["menu_url"].ToString();
      if (a == "artycles")
      { %>
    <%=Mytrip_Mvc_Language_1.menu_artycles%>
    <%} if (a == "news")
      { %>
    <%=Mytrip_Mvc_Language_1.menu_news%>
    <%} if (a == "blogs")
      { %>
    <%=Mytrip_Mvc_Language_1.menu_blogs%>
    <%} %>
    
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright �  2009 ������ ���� �����������   */-->
    <%string a = ViewData["menu_url"].ToString();
      if (a == "artycles")
      { %>
    <!-- ����� ����� -->
    <%int cat = (int)ViewData["count_canegory"];%>
    <div class="edit_content">
    <%= Html.CreateCategory(false) %>
    <%= Html.CreateArtycle(cat, false) %>
      </div>   
    <!--END ����� ����� -->
    <!-- ��������� -->
<a href="<%=Url.Action("Rss_B", "C")%>">
    <img src="/content/images/feed.png" alt="rss" class="rss_category" /></a>
    <h2><%=Mytrip_Mvc_Language_1.menu_artycles%></h2>
    <!--END ��������� -->
    <%} if (a == "news")
      { %>
    <!-- ����� ����� -->
    <%int cat = (int)ViewData["count_canegory"];%>
    <div class="edit_content">
    <%= Html.CreateCategory(true) %>
    <%= Html.CreateArtycle(cat, true) %>
      </div>  
    <!--END ����� ����� -->
       <!-- ��������� -->
<a href="<%=Url.Action("Rss_B", "C")%>">
    <img src="/content/images/feed.png" alt="rss" class="rss_category" /></a>
    <h2><%=Mytrip_Mvc_Language_1.menu_news%></h2>
    <!--END ��������� -->
    <%} if (a == "blogs")
      { %>
    <!-- ��������� -->
<a href="<%=Url.Action("Rss_B", "C")%>">
    <img src="/content/images/feed.png" alt="rss" class="rss_category" /></a>
    <h2><%=Mytrip_Mvc_Language_1.menu_blogs%></h2>
    <!--END ��������� -->
    <%} %> 
    <!-- ������� -->
    <% Html.RenderPartial("_S_pager_all"); %>
    <%if (a == "blogs")
      { %>
    <% Html.RenderPartial("_blog"); %>
    <%}
      else {%><% Html.RenderPartial("_artycle"); %><% } %>    
    <% Html.RenderPartial("_S_pager_all"); %>
    <!--END ������� -->
</asp:Content>
<asp:Content ContentPlaceHolderID="RightContent" runat="server">
    <!-- ������ ������� -->
     <% Html.RenderPartial("_S_admin"); %>
    <% Html.RenderPartial("_S_search"); %>
    <% Html.RenderPartial("_S_menu_user"); %>
    <% Html.RenderPartial("_S_news_right"); %>
    <% Html.RenderPartial("_S_artycle_right"); %>
    <% Html.RenderPartial("_S_blog_right"); %>
    <% Html.RenderPartial("_S_teg_right"); %>
    <!--END ������ ������� -->
</asp:Content>
