$(document).ready(function () {
    $('div.textbox').each(function () {
        if ($(this).find('input').is('.w40px')) {
            $(this).css({ width: (46 + 'px') });
            $(this).find('input').css({ width: (40 + 'px') });
        }
        else if ($(this).find('input').is('.w100px')) {
            $(this).css({ width: (106 + 'px') });
            $(this).find('input').css({ width: (100 + 'px') });
        }
        else if ($(this).find('input').is('.w80px')) {
            $(this).css({ width: (86 + 'px') });
            $(this).find('input').css({ width: (80 + 'px') });
        }
        else if ($(this).find('input').is('.w20px')) {
            $(this).css({ width: (26 + 'px') });
            $(this).find('input').css({ width: (20 + 'px') });
        }
    });
    FixUpDown();
    Move();
    Order();
    CheckInputs();
    Save();
});
function Save() {
    $("#save").live("click",function () {
        $("input[type=text]").each(function () {
            var len = this.value.length;
            if (!$(this).is('.input-validation-error')) {
                if (len == 0) {
                    $(this).addClass('.input-validation-error');
                    var a = $(this).parent('div');
                    var b = $(a).parent('div');
                    $(b).find('div.textC,div.textL,div.textR').removeClass("ac");
                    $(b).find('div.textC,div.textL,div.textR').addClass("er");
                }
            } else {
                if (len > 0) {
                    $(this).removeClass('.input-validation-error');
                    var a = $(this).parent('div');
                    var b = $(a).parent('div');
                    $(b).find('div.textC,div.textL,div.textR').removeClass("er");
                }
            }
        });
        var a = 1;
        $("div.textC").each(function () {
            if ($(this).is('.er')) { a = 0 }
        });
        if (a = 1) {
            $.ajax({ type: "POST",
                url: "/Core/HomePage",
                data: 'ids=' + IdsArr(),
                success: function (data) {
                    $("#result").html(data);
                }
            });
        } else
            $("#result").html("error");
    });
}
function CheckInputs() {
    $("input[type=text]").bind('keypress focus click', function () {
        if ($(this).is('.input-validation-error'))
            $(this).removeClass('.input-validation-error');
        $("input[type=text]").each(function () {
            var len = this.value.length;
            if (!$(this).is('.input-validation-error')) {
                if (len == 0) {
                    $(this).addClass('.input-validation-error');
                    var a = $(this).parent('div');
                    var b = $(a).parent('div');
                    $(b).find('div.textC,div.textL,div.textR').removeClass("ac");
                    $(b).find('div.textC,div.textL,div.textR').addClass("er");
                }

            } else {
                if (len > 0) {
                    $(this).removeClass('.input-validation-error');
                    var a = $(this).parent('div');
                    var b = $(a).parent('div');
                    $(b).find('div.textC,div.textL,div.textR').removeClass("er");
                }
            }
        });
    });
    $("#content input[id]").keyup(function () {
        var len = this.value.length;
        var id = $(this).attr('id')
        if (id.indexOf("style") != -1 && len >= 2) {
            this.value = this.value.substring(0, 1);
        }
        if ((id.indexOf("content") != -1 || id.indexOf("image") != -1) && len >= 4) {
            this.value = this.value.substring(0, 4);
        }
    });
    $("#content input[id]").keydown(function (event) {
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
}
function Order() {
    $("a[id^='up'],a[id^='down']").click(function () {
        var current = $(this).closest("div[id]");
        var id = $(this).attr('id');
        $()
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
          div.find("#options" + id).hide();
          div.find("#sort" + id).hide();
          div.find("#ctrrows" + id).val(1);
          div.find("#ctrcolumns" + id).val(1);
          FixIndex();
          FixUpDown();
      },
      function () {
          var div = $(this).closest("div[id]");
          div.insertBefore($("td[id=left] div:last"));
          $(this).children("img").attr("src", "/Theme/" + theme + "/images/rightarrow_blue.png");
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
          div.insertBefore($("td[id=left] div:last"));
          $(this).children("img").attr("src", "/Theme/" + theme + "/images/rightarrow_blue.png");
          var id = div.attr('id');
          div.find("#options" + id).show();
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
          div.find("#options" + id).hide();
          div.find("#sort" + id).hide();
          div.find("#ctrrows" + id).val(1);
          div.find("#ctrcolumns" + id).val(1);
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
                 var data = $(this).find("#assembly" + id).html() + "_" + id + "_" + $(this).find("#ctrrows" + id).val() + "_"
                  + $(this).find("#ctrcolumns" + id).val() + "_" + $(this).find("#content" + id).val() + "_"
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