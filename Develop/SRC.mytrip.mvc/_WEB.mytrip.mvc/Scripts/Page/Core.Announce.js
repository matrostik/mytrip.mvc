$(document).ready(function () {
    FixUpDown();
    Move();
    Order();
    Save();
});
function Save() {
    $("#save").live("click", function () {
        $.ajax({ type: "POST",
            url: "/Core/Announce",
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
    var theme = 'z';
    $.ajax({ type: "POST",
        url: "/mtm/Theme",
        success: function (data) {
            theme = data;
        }
    });
    $("td[id=left] a[id^='addremove']").toggle(
      function () {
          var div = $(this).closest("div[id]");
          div.insertBefore($("td[id=right] div:last"));
          $(this).children("img").attr("src", "/Theme/" + theme + "/images/leftarrow_blue.png");
          var id = div.attr('id');
          div.find("#index" + id).hide();
          div.find("#sort" + id).hide();
          FixIndex();
          FixUpDown();
      },
      function () {
          var div = $(this).closest("div[id]");
          div.insertBefore($("td[id=left] div:last"));
          $(this).children("img").attr("src", "/Theme/" + theme + "/images/rightarrow_blue.png");
          var id = div.attr('id');
          div.find("#sort" + id).show();
          div.find("#index" + id).show();
          FixIndex();
          FixUpDown();
      });
    $("td[id=right] a[id^='addremove']").toggle(
      function () {
          var div = $(this).closest("div[id]");
          div.insertBefore($("td[id=left] div:last"));
          $(this).children("img").attr("src", "/Theme/" + theme + "/images/rightarrow_blue.png");
          var id = div.attr('id');
          div.find("#sort" + id).show();
          div.find("#index" + id).show();
          FixIndex();
          FixUpDown();
      },
      function () {
          var div = $(this).closest("div[id]");
          div.insertBefore($("td[id=right] div:last"));
          $(this).children("img").attr("src", "/Theme/" + theme + "/images/leftarrow_blue.png");
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