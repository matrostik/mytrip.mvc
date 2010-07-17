<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Articles.Models.ArticleIndexModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=Html.PageTitle(Model.PageTitle, "/")%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentLeft" runat="server">


    
    <table style="width: 100%; padding: 0px; margin: 0px; border: 0px;">
    <tr>
    <td style="padding: 0px; margin: 0px;border: 0px;">
    <%=Html.ParrentCategory(Model.ParentCategory,Model.Path)%>
    <h3 class="title">
        <%=Html.EditDeleteCategory(Model.ShowEditDelete,Model.ShowEditDeleteBlog,Model.CategoryId,
                        Model.Path)%>
        <%=Model.PageTitle%>
        <%=Html.ArticleRssLink(Model.PageTitle,Model.Path, Model.CategoryId, 14)%>
    </h3>
    <%=Html.ShowDetailsBlog(Model.ShowDetailsBlog,Model.ParentCategory)%>
    
    </td>
    <td style="padding: 0px; margin: 0px;border: 0px;">
    <div style="position: relative; float: right">
        <%=Html.EditorCategory(Model.ShowAddCategory,Model.ShowAddSubCategory,Model.ShowAddArticle,Model.ShowAddBlog,
            Model.ShowAddPost, Model.CategoryId)%>
    </div>
    
    </td>
    </tr>
    
    </table>
    



    <%if (Model.Articles == null)
      {%>
       <div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content">
       <%=Html.MytripPager(Model.DefaultCount,Model.Total,"...")%>
     </div><div><div class="contentbottomright"></div><div class="contentbottomleft"></div><div class="contentbottomcon"></div></div>
     <div class="acfooter"></div>
    <%}%>
    <%=Html.ListCategories(Model.Categories,Model.Path)%>
    <%if (Model.Articles == null)
      {%>
        <div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content">
       
       <%=Html.MytripPager(Model.DefaultCount,Model.Total,"...")%>
       </div><div><div class="contentbottomright"></div><div class="contentbottomleft"></div><div class="contentbottomcon"></div></div>
       <div class="acfooter"></div>
    <%}else{%>
    <div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content">
       <%=Html.MytripPager(Model.DefaultCount,Model.Total,"...")%>
      </div><div><div class="contentbottomright"></div><div class="contentbottomleft"></div><div class="contentbottomcon"></div></div>
       <div class="acfooter"></div>
          <%
              foreach (mytrip_articles article in Model.Articles)
              { %><div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content">
      
         <table class="homepage"> <tr>
              <td style="width: 170px;border:0;">
                  <%=Html.ArticleSpecification(article, Model.Path)%>
              </td>
              <td style="border-top:0px;border-bottom:0px;border-right:0px;">
                  <div style="position: relative; float: right">
                      <%=Html.ArticleRating(article.ApprovedVotes,false,article.TotalVotes)%>
                  </div>
                  <b>
                      <%=Html.MytripActionLink(Url.Action("View", new { id = article.ArticleId, id2 = article.Path }), article.Title)%>
                      <%if (article.OnlyForRegisterUser)
                        { %>
                      <%=Html.MytripImage("/Content/images/Keys.png", "", 20, 0, 0)%><%}%>
                  </b>
                  <p>
                  </p>
                  <%=Html.ImageForAbstract(article.ImageForAbstract,150)%>
                  <%=article.Abstract%>
                  <br />
                  <%=Html.ShowArticleTags(article)%>
              </td>
          </tr></table>  </div><div ><div class="contentbottomright"></div><div class="contentbottomleft"></div><div class="contentbottomcon"></div></div>
          <div class="acfooter"></div>
          <%}%>
     <div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content">
        
   <%=Html.MytripPager(Model.DefaultCount,Model.Total,"...")%>
       </div><div><div class="contentbottomright"></div><div class="contentbottomleft"></div><div class="contentbottomcon"></div></div>

    <% } %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentRight" runat="server">
    <%=Html.Partial("SideBar")%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
