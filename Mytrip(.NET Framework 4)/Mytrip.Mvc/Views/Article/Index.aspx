<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Articles.Models.ArticleIndexModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=Html.PageTitle(Model.PageTitle, "/")%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentLeft" runat="server">
    <div style="position: relative; float: right">
        <%=Html.EditorCategory(Model.ShowAddCategory,Model.ShowAddSubCategory,Model.ShowAddArticle,Model.ShowAddBlog,
            Model.ShowAddPost, Model.CategoryId)%>
    </div>
    <%=Html.ParrentCategory(Model.ParentCategory,Model.Path)%>
    <h3>
        <%=Html.EditDeleteCategory(Model.ShowEditDelete,Model.ShowEditDeleteBlog,Model.CategoryId,
                        Model.Path)%>
        <%=Model.PageTitle%>
        <%=Html.ArticleRssLink(Model.PageTitle,Model.Path, Model.CategoryId, 14)%>
    </h3>
    <%=Html.ShowDetailsBlog(Model.ShowDetailsBlog,Model.ParentCategory)%>
    <%if (Model.Articles == null)
      {%><%=Html.MytripPager(Model.DefaultCount,Model.Total,"...")%>
    <%}%>
    <%=Html.ListCategories(Model.Categories,Model.Path)%>
    <%=Html.MytripPager(Model.DefaultCount,Model.Total,"...")%>
    <%if (Model.Articles != null)
      {%><table>
          <%
              foreach (mytrip_Articles article in Model.Articles)
              { %>
          <tr>
              <td style="width: 170px">
                  <%=Html.ArticleSpecification(article)%>
              </td>
              <td>
                  <div style="position: relative; float: right">
                      <%=Html.ArticleRating(article,false)%>
                  </div>
                  <b>
                      <%=Html.MytripActionLink(Url.Action("View", new { id = article.ArticleId, Path = article.Path }), article.Title)%>
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
          </tr>
          <%}%>
      </table>
    <%=Html.MytripPager(Model.DefaultCount, Model.Total, "...")%>
    <% } %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentRight" runat="server">
    <%=Html.Partial("SmallColumn")%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
