<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Store.Models.DepartmentModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
<%--************************************************************
Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich
To learn more about Mytrip.Mvc.Entity visit
http://mytripmvc.codeplex.com
http://starterkitmytripmvc.codeplex.com
mytripmvc@gmail.com
license: Microsoft Public License (Ms-PL)
***********************************************************--%>
<%=Html.PageTitle(Model.titleDepartmentModel.title, "/")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentLeft" runat="server">
  
    <%=Html.TitleDepartment(Model.titleDepartmentModel)%>
     <div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content">
      
 <% using (Html.BeginForm())
             {%>
             <table style="border-width: 0px; padding: 0px"><tr><td style="border-width: 0px; padding: 0px">
             <%=Html.TextBoxFor(m=>m.Search, new { style = "width:95%" })%>
             </td><td style="border-width: 0px; padding: 0px">
             <%= Html.DropDownListFor(m=>m.DepartmentId, Model.SelectDepartment)%>
             </td><td style="border-width: 0px; padding: 0px">
             <%= Html.DropDownListFor(m=>m.ProducerId, Model.SelectProducer)%>
             </td><td style="border-width: 0px; padding: 0px">
             <%=StoreLanguage.Price_from %>
             </td><td style="border-width: 0px; padding: 0px">
              <%=Html.TextBoxFor(m=>m.smallprice, new { style="width:50px"})%>
              </td><td style="border-width: 0px; padding: 0px">
             <%=StoreLanguage.Price_to %>
             </td><td style="border-width: 0px; padding: 0px">
              <%=Html.TextBoxFor(m=>m.bigprice, new { style = "width:50px" })%>
              </td><td style="border-width: 0px; padding: 0px">
             <input id="_search" type="submit" value=""/></td></tr></table>
             <%} if (Model.paging2){ %>
             <%=Html.Sorting() %><%} %>
             </div><div><div class="contentbottomright"></div><div class="contentbottomleft"></div><div class="contentbottomcon"></div></div>
     <div class="acfooter"></div>
        <%if (Model.paging)
          { %>
        <div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content">
       <%=Html.MytripPager(10, Model.takepaging, "...")%>
     </div><div><div class="contentbottomright"></div><div class="contentbottomleft"></div><div class="contentbottomcon"></div></div>
     <div class="acfooter"></div>
     <% }%> 
                  
         <%if (!Model.titleDepartmentModel._search&&Model.titleDepartmentModel.producer&&Model.titleDepartmentModel.count<0)
          {%>
    <%=Html.Producer(Model.Producer, Model.take)%>
    <% }
           else if (!Model.titleDepartmentModel._search && !Model.titleDepartmentModel.producer)
          { %>
    <%=Html.Department(Model.Department, Model.take)%>
    <%} if (Model.paging2)
      { %>
      <div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content">
       <%=Html.MytripPager(10, Model.takepaging, "...")%>
     </div><div><div class="contentbottomright"></div><div class="contentbottomleft"></div><div class="contentbottomcon"></div></div>
     <div class="acfooter"></div>
    <%=Html.Product(Model.Product, Model.take, Model.titleDepartmentModel.subDepartmentId, 
        Model.titleDepartmentModel.producer,Model.DepartmentAndProducer,Model.DepartmentAndProducer2)%>

    <%} if (Model.paging || Model.paging2)
          { %>
        <div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content">
       <%=Html.MytripPager(10, Model.takepaging, "...")%>
     </div><div><div class="contentbottomright"></div><div class="contentbottomleft"></div><div class="contentbottomcon"></div></div>
     <%} %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContentRight" runat="server">
<%=Html.Partial("SideBar")%>
</asp:Content>
