<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Mvc.Models.HomePageModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	 <%=Html.PageTitle(CoreLanguage.sidebar_setting, "/")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2 class="title">
<%=CoreLanguage.sidebar_setting%></h2>
<div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content2">              
* <%=CoreLanguage.sidebar_setting%>. <%=CoreLanguage.menu_setting_desc %>
<table id="homepage" class="tablenoborders">
    <tr>
            <th>
                 <%=CoreLanguage.display_in_menu %>
            </th>
            <th>
                <%=CoreLanguage.available_menuitems %>
            </th>
      </tr>
      <tr>
            <td id="left" style="width: 50%;">
                <% foreach (var item in Model.HomeItems)
                  {%>
                <div id="<%=item.Index %>" style="border: 0 none;vertical-align:middle">
                 <div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content2">
                   <table class="tablenoborders">
                   <tr>
                   <td> 
                    <span><a href="#" id="addremove<%=item.Index %>">
                        <img src="/Theme/<%=Model.Theme %>/images/rightarrow_blue.png" alt="<%=CoreLanguage.remove %>" 
                        title="<%=CoreLanguage.remove %>" height="16px"/></a></span>
                            
                             <span id="index<%=item.Index %>"><%=item.Index %></span>
                             <b>(<%=item.Culture%>)</b>
                             <input id="assembly<%=item.Index %>"  type="hidden" value="<%=item.Assembly %>" />
                    <label id="title<%=item.Index %>"><%=item.Name%></label>
                     <span id="sort<%=item.Index %>" style="float:right;">
                    <a href="#" id="up<%=item.Index %>">
                    <img src="/Theme/<%=Model.Theme %>/images/uparrow_blue.png" alt="<%=CoreLanguage.up %>" 
                    title="<%=CoreLanguage.up %>" height="16px"/></a> 
                    <a href="#" id="down<%=item.Index %>">
                        <img src="/Theme/<%=Model.Theme %>/images/downarrow_blue.png" alt="<%=CoreLanguage.down %>" 
                        title="<%=CoreLanguage.down %>" height="16px"/></a>
                    </span>
              </td>
             </tr>
                   </table>
                     </div><div ><div class="contentbottomright"></div><div class="contentbottomleft"></div><div class="contentbottomcon"></div></div>
                     <div class="acfooter"></div>
                </div>
                <% } %>
            </td>
            <td id="right">
                <%
                    foreach (var newitem in Model.NewHomeItems)
                    { %>
                <div id="<%=newitem.Index %>">
                 <div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content2">
                   <table class="tablenoborders">
                   <tr>
                   <td> 
                    <span><a href="#" id="addremove<%=newitem.Index %>">
                        <img src="/Theme/<%=Model.Theme %>/images/leftarrow_blue.png" alt="<%=CoreLanguage.add %>" 
                        title="<%=CoreLanguage.add %>" height="16px" /></a></span>
                            <span id="index<%=newitem.Index %>"></span>
                            <b>(<%=newitem.Culture %>)</b>
                             <input id="assembly<%=newitem.Index %>"  type="hidden" value="<%=newitem.Assembly %>" />
                    <label id="title<%=newitem.Index %>"><%=newitem.Name%></label>
                    <span id="sort<%=newitem.Index %>" style="float:right; display: none;">
                    <a href="#" id="up<%=newitem.Index %>">
                    <img src="/Theme/<%=Model.Theme %>/images/uparrow_blue.png" alt="<%=CoreLanguage.up %>" 
                    title="<%=CoreLanguage.up %>" height="16px" /></a> 
                    <a href="#" id="down<%=newitem.Index %>">
                        <img src="/Theme/<%=Model.Theme %>/images/downarrow_blue.png" alt="<%=CoreLanguage.down %>" 
                        title="<%=CoreLanguage.down %>" height="16px" /></a>
                    </span>
                     </td>
                     </tr>
                    
                   </table>
                    </div><div ><div class="contentbottomright"></div><div class="contentbottomleft"></div><div class="contentbottomcon"></div></div>
                     <div class="acfooter"></div>
                </div>
                <% } %>
            </td>
        </tr>
    </table>
                         <input id="save" type="submit" value="<%=CoreLanguage.save_changes %>" /><span id="result" style="padding-left:10px;"></span>
    </div><div ><div class="contentbottomright"></div><div class="contentbottomleft"></div><div class="contentbottomcon"></div></div>
                     <div class="acfooter"></div>
    <div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content">
        <%=Html.ActionLink(CoreLanguage.back_to_main,"Index","Home")%>
   </div><div ><div class="contentbottomright"></div><div class="contentbottomleft"></div><div class="contentbottomcon"></div></div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            FixUpDown();
            Move();
            Order();
            Save();
        });
        function Save() {
            $("#save").click(function () {
                $.ajax({ type: "POST",
                    url: "/Core/SideBar",
                    data: 'ids=' + IdsArr(),
                    success: function (data) {
                        $("#result").html(data);
                    }
                });
            });
        };
       
        function Order() {
            $("a[id^='up'],a[id^='down']").click(function () {
                var current = $(this).closest("div[id]");
                var id = $(this).attr('id');
                if (id.indexOf("up") != -1) {
                    var prev = current.prev($("div"));
                    current.slideUp();
                    current.slideDown(800);
                    current.insertBefore(prev);
                }
                else {
                    var next = current.next($("div"));
                    current.slideUp();
                    current.slideDown(800);
                    current.insertAfter(next);
                }
                FixUpDown();
                FixIndex();
                return false;
            });
        }
          function Move() {
            $("td[id=left] a[id^='addremove']").toggle(
      function () {
          //confirm("Are you sure?");
          var div = $(this).closest("div[id]");
          div.appendTo("#right");
          $(this).children("img").attr("src", "/Theme/<%=Model.Theme %>/images/leftarrow_blue.png").attr('title', '<%=CoreLanguage.add %>');
          var id = div.attr('id');
          div.find("#index" + id).hide();
          div.find("#sort" + id).hide();
          FixIndex();
          FixUpDown();
      },
      function () {
          var div = $(this).closest("div[id]");
          div.appendTo("#left");
          $(this).children("img").attr("src", "/Theme/<%=Model.Theme %>/images/rightarrow_blue.png").attr('title', '<%=CoreLanguage.remove %>');
          var id = div.attr('id');
          div.find("#sort" + id).show();
          div.find("#index" + id).show();
          FixIndex();
          FixUpDown();
      });
      $("td[id=right] a[id^='addremove']").toggle(
      function () {
          var div = $(this).closest("div[id]");
          div.appendTo("#left");
          $(this).children("img").attr("src", "/Theme/<%=Model.Theme %>/images/rightarrow_blue.png").attr('title', '<%=CoreLanguage.remove %>');
          var id = div.attr('id');
          div.find("#sort" + id).show();
          div.find("#index" + id).show();
          FixIndex();
          FixUpDown();
      },
      function () {
          //alert("start remove");
          var div = $(this).closest("div[id]");
          div.appendTo("#right");
          $(this).children("img").attr("src", "/Theme/<%=Model.Theme %>/images/leftarrow_blue.png").attr('title', '<%=CoreLanguage.add %>');
          var id = div.attr('id');
          div.find("#index" + id).hide();
          div.find("#sort" + id).hide();
          FixIndex();
          FixUpDown();
      });
        }
        function FixIndex() {
            var ctr = 0;
            $("td[id=left] div[id]").each(function () {
                var id = $(this).attr('id');
                ctr++;
                $(this).find("#index" + id).html(ctr);
            });
            return ctr;
        }
        function IdsArr() {
            var ids = new Array;
            $("td[id=left] div[id]").each(
             function () {
                 var id = $(this).attr('id');
                 var data = $(this).find("#assembly" + id).val();
                 ids.push(data);
             });
            if (ids.length == 0) {
                ids.push("");
            }
            return ids.join("|");
        }
        function FixUpDown() {
            $("a[id^='up']").show();
            $("a[id^='down']").show();
            $("td[id=left] a[id^='up']:first").hide();
            $("td[id=left] a[id^='down']:last").hide();
        }
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContentRight" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContentLeft" runat="server">
</asp:Content>