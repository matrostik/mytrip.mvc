<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Rssparser.Models.RssparserModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Html.PageTitle(Model.Title, "/")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentLeft" runat="server">

    <h2 class="title"><%=Model.Title %></h2>

    <div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content">
       <%=Html.MytripPager(10,Model.total,"...")%>
     </div><div><div class="contentbottomright"></div><div class="contentbottomleft"></div><div class="contentbottomcon"></div></div>
     <div class="acfooter"></div>
<%if (Model.category) {
      foreach (var item in Model.RssparserCategory)
      { %>
       <div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content">
     
    <table class="homepage">
        <tr>
            <td style="border:0px;">
                      <%=Html.MytripImageForAbstract(item.ImageUrl,60) %>
                      
                      
<h3 class="title"><%=Html.MytripActionLink(Url.Action("Index", new {id=1,id2=10, id3 = item.RssparserId, id4 = item.Path }), item.Title)%></h3>
       </td>
        </tr>
     </table>
     </div><div><div class="contentbottomright"></div><div class="contentbottomleft"></div><div class="contentbottomcon"></div></div>
     <div class="acfooter"></div>
       <%}
  
  }
  else
  {  foreach (var item in Model.RssparserContent)
       { %>
     <div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content">
     
    <table class="homepage">
        <tr>
            <td style="border:0px;">
                <%string desc = item.Element("description").Value;
                  string url = item.Element("link").Value;
                  desc = desc.Replace("href=\"/", ("href=\"" + Model.link + "/"));
                  if (Model.img)
                  {
                      string urlimg = item.Element("enclosure").Attribute("url").Value;
                      int imglength = urlimg.Length;
                      if (urlimg.IndexOf(".jpg") != -1 && urlimg.IndexOf(".jpg") > imglength - 4)
                      {
                          int _imglength = urlimg.IndexOf(".jpg");
                          urlimg = urlimg.Remove(_imglength + 4);
                      }
                      else if (urlimg.IndexOf(".png") != -1 && urlimg.IndexOf(".png") > imglength - 4)
                      {
                          int _imglength = urlimg.IndexOf(".png");
                          urlimg = urlimg.Remove(_imglength + 4);
                      }
                      else if (urlimg.IndexOf(".gif") != -1 && urlimg.IndexOf(".gif") > imglength - 4)
                      {
                          int _imglength = urlimg.IndexOf(".gif");
                          urlimg = urlimg.Remove(_imglength + 4);
                      }%>
                      <img src="<%=urlimg %>" alt="" style="position: relative; float: right" />
                      
                      <%
    }
            %>
            
           <h3 class="title"> <a href="<%=url %>"><%= item.Element("title").Value%></a></h3>
                <%= desc%>
            </td>
        </tr>
     </table>
     </div><div><div class="contentbottomright"></div><div class="contentbottomleft"></div><div class="contentbottomcon"></div></div>
     <div class="acfooter"></div>

    <% }
  } %>
<div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content">
       <%=Html.MytripPager(10,Model.total,"...")%>
     </div><div><div class="contentbottomright"></div><div class="contentbottomleft"></div><div class="contentbottomcon"></div></div>
    

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="MainContentRight" runat="server">
<%=Html.Partial("SideBar")%>
</asp:Content>

