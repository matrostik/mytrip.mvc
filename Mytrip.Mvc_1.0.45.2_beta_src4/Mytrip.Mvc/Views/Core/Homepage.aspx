<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Mytrip.Mvc.Models.HomePageModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=Html.PageTitle(CoreLanguage.homepage_setting, "/")%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2 class="title">
<%=CoreLanguage.homepage_setting %></h2>
<div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div><div class="content2">              
     <%=CoreLanguage.homepage_desc %>
<table id="homepage" class="tablenoborders">
    <tr>
         <th>
            <%=CoreLanguage.display_on_main %>
         </th>
         <th>
            <%=CoreLanguage.available_moduls %>
         </th>
     </tr>
     <tr>
         <td id="left" style="width: 50%;">
             <% foreach (var item in Model.HomeItems)
                { %>
           <div id="<%=item.Id %>" style="border: 0 none;vertical-align:middle">
           <div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div>
           <div class="content2">
           <table class="tablenoborders">
             <tr>
                <td> 
                    <span><a href="#" id="addremove<%=item.Id %>">
                            <img src="/Theme/<%=Model.Theme %>/images/rightarrow_blue.png" alt="<%=CoreLanguage.remove %>" 
                             title="<%=CoreLanguage.remove %>" height="16px" /></a></span>
                            <img id="flag<%=item.Id %>" src="/Theme/<%=Model.Theme %>/images/<%=item.Culture %>.png"
                             alt="<%=item.Culture %>" title="<%=item.Culture %>" height="16px"/>
                             <span id="index<%=item.Id %>"><%=item.Index %></span>
                             <b>(<label id="assembly<%=item.Id %>"><%=item.Assembly%></label>)</b>
                    <label id="title<%=item.Id %>"><%=item.Name%></label>
                     <span id="sort<%=item.Id %>" style="float:right;">
                    <a href="#" id="up<%=item.Id %>">
                    <img src="/Theme/<%=Model.Theme %>/images/uparrow_blue.png" alt="<%=CoreLanguage.up %>" 
                    title="<%=CoreLanguage.up %>" height="16px" /></a> <a href="#" id="down<%=item.Id %>">
                        <img src="/Theme/<%=Model.Theme %>/images/downarrow_blue.png" alt="<%=CoreLanguage.down %>"
                         title="<%=CoreLanguage.down %>" height="16px" /></a>
                    </span>
              </td>
             </tr>
             <tr id="options<%=item.Id %>" style="border: 0;">
              <td style="border: 0; ">
              <table style="border: 0;text-align:center;">
                    <tr>
                    <td style="border: 0; "><%=CoreLanguage.rows %>
                    </td>
                     <td style="border: 0; "><%=CoreLanguage.columns %>
                    </td>
                    <td style="border: 0; "><%=CoreLanguage.content_lenght %>
                   </td>
                   <td style="border: 0; "><%=CoreLanguage.image_width %>
                   </td>
                    <td style="border: 0; "><%=CoreLanguage.style %>
                   </td>
                   <td style="border: 0; "><label for="showtitle<%=item.Id %>" ><%=CoreLanguage.show_title %></label>
                   </td>
                    </tr>
                   <tr style="border: 0;">
                   <td style="border: 0;">
                     <a href="#" id="addrow<%=item.Id %>">
                     <img src="/Theme/<%=Model.Theme %>/images/plus_blue.png" alt="+" title="+" height="12px" /></a>
                     <span id="ctrrows<%=item.Id %>" style="padding: 0 8px 0 8px; border: 1px solid #000000;background-color:#ccc"><%=item.Rows%></span>  
                     <a href="#" id="removerow<%=item.Id %>">
                     <img src="/Theme/<%=Model.Theme %>/images/minus_blue.png" alt="-" title="-" height="12px"/></a>
                    </td>
                   <td style="border: 0;">
                    <a href="#" id="addcolumn<%=item.Id %>">
                    <img src="/Theme/<%=Model.Theme %>/images/plus_blue.png" alt="+" title="+" height="12px" /></a>    
                    <span id="ctrcolumns<%=item.Id %>" style="padding: 0 8px 0 8px; border: 1px solid #000000;background-color:#ccc"><%=item.Columns %></span>
                    <a href="#" id="removecolumn<%=item.Id %>">
                    <img src="/Theme/<%=Model.Theme %>/images/minus_blue.png" alt="-" title="-" height="12px" /></a> 
                    </td>
                    <td style="border: 0;">
                            <input id="content<%=item.Id %>" type="text" value="<%=item.Content %>" style="text-align:center;padding: 0 2px 0 2px; border: 1px solid #000000;background-color:#ccc;width:30px;" /> chars.
                     </td>
                    <td style="border: 0;">
                            <input id="image<%=item.Id %>" type="text" value="<%=item.Image %>" style="text-align:center;padding: 0 2px 0 2px; border: 1px solid #000000;background-color:#ccc;width:30px;" /> px.
                     </td>
                     <td style="border: 0;">
                           v.<input id="style<%=item.Id %>" type="text" value="<%=item.Style %>" style="text-align:center;padding: 0 2px 0 2px; border: 1px solid #000000;background-color:#ccc;width:15px;" /> 
                     </td>
                    <td style="border: 0;">
                            <%=Html.CheckBox("showtitle" + item.Id, item.Showtitle, new { style = "padding: 2px; border: 1px solid #000000;background-color:#ccc;" })%>
                     </td>
                   </tr>
                   </table>
                    </td>
                   </tr>
                   </table>
                     </div><div ><div class="contentbottomright"></div><div class="contentbottomleft"></div><div class="contentbottomcon"></div></div>
                     <div class="acfooter"></div>
                </div>
                <% } %>
          </td>
         <td id="right">
             <%foreach (var newitem in Model.NewHomeItems)
               {%>
            <div id="<%=newitem.Id %>">
                 <div><div class="contenttopright"></div><div class="contenttopleft"></div><div class="contenttopcon"></div></div>
                 <div class="content2">
                   <table class="tablenoborders">
                   <tr>
                   <td> 
                    <span><a href="#" id="addremove<%=newitem.Id %>">
                        <img src="/Theme/<%=Model.Theme %>/images/leftarrow_blue.png" alt="<%=CoreLanguage.add %>" 
                        title="<%=CoreLanguage.add %>" height="16px" /></a></span>
                        <img id="flag<%=newitem.Id %>" src="/Theme/<%=Model.Theme %>/images/<%=newitem.Culture %>.png" 
                        alt="<%=newitem.Culture %>" title="<%=newitem.Culture %>" height="16px"/>
                        <span id="index<%=newitem.Id %>"></span>
                        <b>(<label id="assembly<%=newitem.Id %>"><%=newitem.Assembly %></label>)</b>
                    <label id="title<%=newitem.Id %>"><%=newitem.Name%></label>
                    <span id="sort<%=newitem.Id %>" style="float:right; display: none;">
                    <a href="#" id="up<%=newitem.Id %>">
                    <img src="/Theme/<%=Model.Theme %>/images/uparrow_blue.png" alt="<%=CoreLanguage.up %>" 
                    title="<%=CoreLanguage.up %>" height="16px"/></a> <a href="#" id="down<%=newitem.Id %>">
                        <img src="/Theme/<%=Model.Theme %>/images/downarrow_blue.png" alt="<%=CoreLanguage.down %>" 
                        title="<%=CoreLanguage.down %>" height="16px" /></a>
                    </span>
                     </td>
                   </tr>
                   <tr id="options<%=newitem.Id %>" style="display: none;">
                   <td>
                   <table class="tablenoborders" style="text-align:center;">
                   <tr>
                    <td><%=CoreLanguage.rows %>
                    </td>
                     <td><%=CoreLanguage.columns %>
                    </td>
                    <td><%=CoreLanguage.content_lenght %>
                   </td>
                   <td><%=CoreLanguage.image_width %>
                   </td>
                    <td><%=CoreLanguage.style %>
                   </td>
                   <td><label for="showtitle<%=newitem.Id %>" ><%=CoreLanguage.show_title %></label>
                   </td>
                   </tr>
                   <tr>
                   <td>
                    <a href="#" id="addrow<%=newitem.Id %>">
                    <img src="/Theme/<%=Model.Theme %>/images/plus_blue.png" alt="+" title="+" height="12px"/></a>       
                    <span id="ctrrows<%=newitem.Id %>" style="padding: 0 8px 0 8px; border: 1px solid #000000; background-color:#ccc">1</span>
                    <a href="#" id="removerow<%=newitem.Id %>">
                    <img src="/Theme/<%=Model.Theme %>/images/minus_blue.png" alt="-" title="-" height="12px" /></a>  
                   </td>
                   <td>
                    <a href="#" id="addcolumn<%=newitem.Id %>">
                    <img src="/Theme/<%=Model.Theme %>/images/plus_blue.png" alt="+" title="+" height="12px"/></a> 
                    <span id="ctrcolumns<%=newitem.Id %>" style="padding: 0 8px 0 8px; border: 1px solid #000000;background-color:#ccc">1</span>
                    <a href="#" id="removecolumn<%=newitem.Id %>">
                    <img src="/Theme/<%=Model.Theme %>/images/minus_blue.png" alt="-" title="-" height="12px" /></a> 
                   </td>
                   <td>
                            <input id="content<%=newitem.Id %>" type="text" value="100" style="text-align:center; padding: 0 2px 0 2px; border: 1px solid #000000;background-color:#ccc;width:30px;" /> chars.
                    </td>
                    <td>
                            <input id="image<%=newitem.Id %>" type="text" value="50" style="text-align:center;padding: 0 2px 0 2px; border: 1px solid #000000;background-color:#ccc;width:30px;" /> px.
                     </td>
                     <td>
                            v.<input id="style<%=newitem.Id %>" type="text" value="2" style="text-align:center;padding: 0 2px 0 2px; border: 1px solid #000000;background-color:#ccc;width:15px;" /> 
                     </td>
                    <td>
                            <%=Html.CheckBox("showtitle" + newitem.Id, false, new { style = "padding: 2px; border: 1px solid #000000;background-color:#ccc;" })%>
                     </td>
                   </tr>
                   </table>
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
            Amount();
            Order();
            CheckInputs();
            Save();
        });
        function Save() {
            $("#save").click(function () {
                $.ajax({ type: "POST",
                    url: "/Core/HomePage",
                    data:  'ids=' +IdsArr() ,
                    success: function (data) {
                        $("#result").html(data);
                    }
                });
            });
        };
        function CheckInputs() {
            $("#homepage input[id]").keyup(function () {
                var len = this.value.length;
                var id = $(this).attr('id')
                if (id.indexOf("style") != -1 && len >= 2) {
                    this.value = this.value.substring(0, 1);
                }
                if ((id.indexOf("content") != -1 || id.indexOf("image") != -1) && len >= 4) {
                    this.value = this.value.substring(0, 4);
                }
            });
            $("#homepage input[id]").blur(function () {
                var id = $(this).attr('id');
                var len = this.value.length;
                if (len == 0) {
                    if (id.indexOf("content") != -1) {
                        this.value = 100;
                    }
                    if (id.indexOf("image") != -1) {
                        this.value = 0;
                    }
                    if (id.indexOf("style") != -1) {
                        this.value = 2; ;
                    }
                }
            });
            $("#homepage input[id]").keydown(function (event) {
                if (event.keyCode == 46 || event.keyCode == 8) {
                } else {
                    if (event.keyCode < 95) {
                        if (event.keyCode < 48 || event.keyCode > 57) {
                            event.preventDefault();
                        }
                    } else {
                        if (event.keyCode < 96 || event.keyCode > 105) {
                            event.preventDefault();
                        }
                    }
                }
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
        function Amount() {
            $("a[id*='row'],a[id*='column']").click(function () {
                var div = $(this).closest("div[id]");
                var a_id = $(this).attr('id');
                var id = div.attr('id');
                if (a_id.indexOf("row") != -1) {
                    var ctr = div.find("#ctrrows" + id).html();
                    if (a_id.indexOf("add") != -1 && ctr != 10) {
                        ctr++;
                        div.find("#ctrrows" + id).html(ctr);
                    }
                    if (a_id.indexOf("remove") != -1 && ctr != 1) {
                        ctr--;
                        div.find("#ctrrows" + id).html(ctr);
                    }
                }
                else {
                    ctr = div.find("#ctrcolumns" + id).html();
                    if (a_id.indexOf("add") != -1 && ctr != 10) {
                        ctr++;
                        div.find("#ctrcolumns" + id).html(ctr);
                    }
                    if (a_id.indexOf("remove") != -1 && ctr != 1) {
                        ctr--;
                        div.find("#ctrcolumns" + id).html(ctr);
                    }
                }
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
          div.find("#options" + id).hide();
          div.find("#sort" + id).hide();
          div.find("#ctrrows" + id).html(1);
          div.find("#ctrcolumns" + id).html(1);
          FixIndex();
          FixUpDown();
      },
      function () {
          var div = $(this).closest("div[id]");
          div.appendTo("#left");
          $(this).children("img").attr("src", "/Theme/<%=Model.Theme %>/images/rightarrow_blue.png").attr('title', '<%=CoreLanguage.remove %>');
          var id = div.attr('id');
          div.find("#options" + id).show();
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
          div.find("#options" + id).show();
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
          div.find("#options" + id).hide();
          div.find("#sort" + id).hide();
          div.find("#ctrrows" + id).html(1);
          div.find("#ctrcolumns" + id).html(1);
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
                 var data = $(this).find("#assembly" + id).html() + "_" + id + "_" + $(this).find("#ctrrows" + id).html() + "_"
                  + $(this).find("#ctrcolumns" + id).html() + "_" + $(this).find("#content" + id).val() + "_"
                  + $(this).find("#image" + id).val() + "_" + $(this).find("#style" + id).val() + "_"
                  + $(this).find("#showtitle" + id).attr('checked');
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
