<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
<%  //************************************************************ 
    // Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich
    // To learn more about Mytrip.Mvc.Entyty visit 
    // http://starterkitmytripmvc.codeplex.com/
    // mytripmvc@gmail.com
    // license: Microsoft Public License (Ms-PL) 
    // *********************************************************** %>
    <%=Html.PageTitle(CoreLanguage.home,"/") %>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContentLeft" runat="server">
    <h2><%= Html.Encode(ViewData["Message"]) %></h2>
    <p>
        To learn more about Mytrip.Mvc.Entity visit <a href="http://starterkitmytripmvc.codeplex.com/" title="Mytrip.Mvc.Entyty">http://starterkitmytripmvc.codeplex.com/</a>.
    </p>
    <fieldset>
    <legend><%=CoreLanguage.account_information %></legend>
    <p>username: admin</p>
    <p>password: 123456</p>
    </fieldset>
    <fieldset><legend>Preview Latest Articles From Category For Home Page</legend>
    <div class="accordion2">
    <div class="accordiontitle2">
    <b>Specification</b>
    </div>
    <div class="accordioncontent2">
    <h3>Html.ArticleFromCategoryHomeContent("NAME PREVIEW","COUNT STRING","LENGTH ABSTRACT","CATEGORY ID","UNLOCK CULTURE")</h3> 
    </div>
    </div>
    <div class="accordion2">
    <div class="accordiontitle2">
    <b>Html.ArticleFromCategoryHomeContent("small1column",4,300,3,true)</b>
    </div>
    <div class="accordioncontent2">
    <%=Html.ArticleFromCategoryHomeContent("small1column", 4, 300,3,true)%> 
    </div>
    </div>
    <div class="accordion2">
    <div class="accordiontitle2">
    <b>Html.ArticleFromCategoryHomeContent("1column",4,300,3,false)</b>
    </div>
    <div class="accordioncontent2">
    <%=Html.ArticleFromCategoryHomeContent("1column", 4, 300,3,false)%> 
    </div>
    </div>
    <div class="accordion2">
    <div class="accordiontitle2">
    <b>Html.ArticleFromCategoryHomeContent("small2column",2,100,3,false)</b>
    </div>
    <div class="accordioncontent2">
    <%=Html.ArticleFromCategoryHomeContent("small2column", 2, 100,3,false)%>
    </div>
    </div>
    <div class="accordion2">
    <div class="accordiontitle2">
    <b>Html.ArticleFromCategoryHomeContent("2column",2,100,3,false)</b>
    </div>
    <div class="accordioncontent2">
    <%=Html.ArticleFromCategoryHomeContent("2column", 2, 100,3,false)%>
    </div>
    </div>
    <div class="accordion2">
    <div class="accordiontitle2">
    <b>Html.ArticleFromCategoryHomeContent("small3column",2,100,3,false)</b>
    </div>
    <div class="accordioncontent2">
    <%=Html.ArticleFromCategoryHomeContent("small3column", 2, 100,3,false)%>
    </div>
    </div>
    <div class="accordion2">
    <div class="accordiontitle2">
    <b>Html.ArticleFromCategoryHomeContent("3column",2,100,3,false)</b>
    </div>
    <div class="accordioncontent2">
    <%=Html.ArticleFromCategoryHomeContent("3column", 2, 100,3,false)%>
    </div>
    </div> 
    <div class="accordion2">
    <div class="accordiontitle2">
    <b>Html.ArticleFromCategoryHomeContent("AsArticles",4,300,3,false)</b>
    </div>
    <div class="accordioncontent2">
    <%=Html.ArticleFromCategoryHomeContent("AsArticles", 4, 300,3,false)%>
    </div>
    </div></fieldset>
    <fieldset><legend>Preview Latest Articles For Home Page (category no add menu)</legend>
    <div class="accordion2">
    <div class="accordiontitle2">
    <b>Specification</b>
    </div>
    <div class="accordioncontent2">
    <h3>Html.ArticleHomeContent("NAME PREVIEW","COUNT STRING","LENGTH ABSTRACT")</h3> 
    </div>
    </div>
    <div class="accordion2">
    <div class="accordiontitle2">
    <b>Html.ArticleHomeContent("small1column",4,300)</b>
    </div>
    <div class="accordioncontent2">
    <%=Html.ArticleHomeContent("small1column",4,300)%> 
    </div>
    </div>
    <div class="accordion2">
    <div class="accordiontitle2">
    <b>Html.ArticleHomeContent("1column",4,300)</b>
    </div>
    <div class="accordioncontent2">
    <%=Html.ArticleHomeContent("1column",4,300)%> 
    </div>
    </div>
    <div class="accordion2">
    <div class="accordiontitle2">
    <b>Html.ArticleHomeContent("small2column",2,100)</b>
    </div>
    <div class="accordioncontent2">
    <%=Html.ArticleHomeContent("small2column",2,100)%>
    </div>
    </div>
    <div class="accordion2">
    <div class="accordiontitle2">
    <b>Html.ArticleHomeContent("2column",2,100)</b>
    </div>
    <div class="accordioncontent2">
    <%=Html.ArticleHomeContent("2column",2,100)%>
    </div>
    </div>
    <div class="accordion2">
    <div class="accordiontitle2">
    <b>Html.ArticleHomeContent("small3column",2,100)</b>
    </div>
    <div class="accordioncontent2">
    <%=Html.ArticleHomeContent("small3column",2,100)%>
    </div>
    </div>
    <div class="accordion2">
    <div class="accordiontitle2">
    <b>Html.ArticleHomeContent("3column",2,100)</b>
    </div>
    <div class="accordioncontent2">
    <%=Html.ArticleHomeContent("3column",2,100)%>
    </div>
    </div> 
    <div class="accordion2">
    <div class="accordiontitle2">
    <b>Html.ArticleHomeContent("AsArticles",4,300)</b>
    </div>
    <div class="accordioncontent2">
    <%=Html.ArticleHomeContent("AsArticles",4,300)%>
    </div>
    </div></fieldset> 
    <fieldset><legend>Preview Latest Posts For Home Page</legend>
    <div class="accordion2">
    <div class="accordiontitle2">
    <b>Specification</b>
    </div>
    <div class="accordioncontent2">
    <h3>Html.PostsHomeContent("NAME PREVIEW","COUNT STRING","LENGTH ABSTRACT")</h3> 
    </div>
    </div>
    <div class="accordion2">
    <div class="accordiontitle2">
    <b>Html.PostsHomeContent("small1column",4,300)</b>
    </div>
    <div class="accordioncontent2">
    <%=Html.PostsHomeContent("small1column", 4, 300)%> 
    </div>
    </div>
    <div class="accordion2">
    <div class="accordiontitle2">
    <b>Html.PostsHomeContent("1column",4,300)</b>
    </div>
    <div class="accordioncontent2">
    <%=Html.PostsHomeContent("1column", 4, 300)%> 
    </div>
    </div>
    <div class="accordion2">
    <div class="accordiontitle2">
    <b>Html.PostsHomeContent("small2column",2,100)</b>
    </div>
    <div class="accordioncontent2">
    <%=Html.PostsHomeContent("small2column", 2, 100)%>
    </div>
    </div>
    <div class="accordion2">
    <div class="accordiontitle2">
    <b>Html.PostsHomeContent("2column",2,100)</b>
    </div>
    <div class="accordioncontent2">
    <%=Html.PostsHomeContent("2column", 2, 100)%>
    </div>
    </div>
    <div class="accordion2">
    <div class="accordiontitle2">
    <b>Html.PostsHomeContent("small3column",2,100)</b>
    </div>
    <div class="accordioncontent2">
    <%=Html.PostsHomeContent("small3column", 2, 100)%>
    </div>
    </div>
    <div class="accordion2">
    <div class="accordiontitle2">
    <b>Html.PostsHomeContent("3column",2,100)</b>
    </div>
    <div class="accordioncontent2">
    <%=Html.PostsHomeContent("3column", 2, 100)%>
    </div>
    </div> 
    <div class="accordion2">
    <div class="accordiontitle2">
    <b>Html.PostsHomeContent("AsArticles",4,300)</b>
    </div>
    <div class="accordioncontent2">
    <%=Html.PostsHomeContent("AsArticles", 4, 300)%>
    </div>
    </div></fieldset>    
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContentRight" runat="server">
<%=Html.Partial("SmallColumn")%>
</asp:Content>
