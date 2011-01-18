$(document).ready(function () {
    $('img.captcha').wrap('<div class="captcha"/>');
    $('ul.logon li').hover(function () {
        $(this).find('ul').stop(true, true);
        $(this).find('ul').slideDown();
    },
function () { $(this).find('ul').delay(80).slideUp(); });
    $('ul.menu li').hover(function () {
        $(this).find('ul').stop(true, true);
        $(this).find('ul').slideDown();
    },
function () { $(this).find('ul').delay(80).slideUp(); });
    if ($('div.acc').length != 0) {
        $.ajax({ type: "POST", url: "/mtm/AccardionCookies",
            success: function (data) {
                var d = []; d = data.split('|');
                for (i = 0; i < d.length; i++) {
                    $('div.accT').each(function () {
                        var a = $(this).attr("value");
                        if (a == d[i]) {
                            $(this).addClass('ac');
                            $(this).next('div.accVC').hide();
                        }
                    });
                }
            }
        });
    }
    $('div.acc').each(function () {
        $(this).css('z-index', 1000);
        $(this).after('<div class="accFR"/>');
    });
    $('div.acc').hover(function () {
        $(this).find('div.accT').filter('.ac').addClass("ho");
        $(this).find('div.accT:not(.ac)').addClass("hoo");
    }, function () {
        $(this).find('div.accT').removeClass("ho");
        $(this).find('div.accT:not(.ac)').removeClass("hoo");
    });
    $("div.acc div.accT").live("click", function () {
        var a = "id=_" + $(this).attr("value") + "_";
        $.ajax({ type: "POST", url: "/mtm/Accardion", data: a });
        $(this).removeClass("ho").removeClass("hoo").toggleClass("ac");
        $(this).next('div.accVC').eq(0).slideToggle(300);
    });
    if ($('input[type=password]').length == 0 && !$('input[type=password]:first').is('.input-validation-error')) {
        $('input[type=password]:first').focus();
    }
    if ($('input[type=text]').length != 0 && !$('input[type=text]:first').is('.input-validation-error')) {
        $('input[type=text]:first').focus();
    }
    createDropDown();
    $("dl.dropdown dt a").live("click", function () {
        var a = $(this).closest("dl"); a.find("dd ul").toggle(); return false;
    });
    $(document).live("click", function (e) {
        var $clicked = $(e.target);
        if (!$clicked.parents().hasClass("dropdown"))
            $("dl.dropdown dd ul").hide();
    });
    $("dl.dropdown dd ul li a").live("click", function (e) {
        var a = $(this).closest("dl");
        var id = a.attr('id').replace("mtmddl", "");
        var b = $(this).html();
        a.find("dt a").html(b);
        a.find("dd ul").hide();
        var c = $("select[id=" + id + "]");
        c.val($(this).find("span.value").html());
        return false
    });
    $("dl.dropdown dd ul li a").live("click", function () {
        var a = $(this).closest("dl");
        var id = a.attr('id').replace("mtmddl", "");
        var b = $(this).html();
        a.find("dt a").html(b);
        a.find("dd ul").hide();
        var c = $("select[id=" + id + "]");
        c.val($(this).find("span.value").html());
        return false;
    });
    $('div.tabI:first').addClass("ac");
    $("input:checkbox").each(function () {
        var id = $(this).attr('id');
        var cbx = $(this).css({ "z-index": "-9999", "position": "relative" });

        if (cbx.is(':checked'))
            $(cbx).wrap('<span id="mtmcbx' + id + '" class="checkboxcheck" />');
        else
            $(cbx).wrap('<span id="mtmcbx' + id + '" class="checkbox" />');
    });
    $("span.checkbox").live("click", function () {
        //alert('span.checkbox');
        if ($(this).find('input:checkbox').is(':disabled'))
            return false;
        var id = $(this).find('input:checkbox').attr('id');
        $("input[id=" + id + "]").attr('checked', true);
        $("input[id=" + id + "]").triggerHandler('click');
        $(this).removeClass('checkbox').addClass('checkboxcheck');
        //fixCheckbox();
    });
    $("span.checkboxcheck").live("click", function () {
        //alert('span.checkboxcheck');
        if ($(this).find('input:checkbox').is(':disabled'))
            return false;
        var id = $(this).find('input:checkbox').attr('id');
        $("input[id=" + id + "]").attr('checked', false);
        $("input[id=" + id + "]").triggerHandler('click');
        $(this).removeClass('checkboxcheck').addClass('checkbox');
        //fixCheckbox();
    });
    $('input:checkbox').click(function (e) {
        //alert('input:checkbox click');
        e.stopPropagation();
        fixCheckbox();
    });
    $("div.window").prepend("<div class='modalTR'/>");
    $('div.window').after('<div class="mask"/>');
    //    $('div.window').hide();
    $('div.mask,div.modalTR').click(function () {
        $('div.mask, div.window').hide();
        $('input[type=text]:first').focus();
        var a = $('input[type=text]:first').parent('div.textC');
        var b = $(a).parent('div.textbox');
        $(b).find('div.textC, div.textL, div.textR').addClass("ac");
    });

});


function createDropDown() {
    var a = $("select");
    $("select").each(function () {
        var a = $(this);
        var b = a.attr('id');
        var c = a.find("option:selected");
        var d = $("option", a); a.before('<div class="dropdown"><dl id="mtmddl' + b + '" class="dropdown"></dl></div>');
        $("#mtmddl" + b).append('<dt><a href="#">' + c.text() + '<span class="value">' + c.attr("value") + '</span></a></dt>');
        $("#mtmddl" + b).append('<dd><ul></ul></dd>');
        d.each(function () {
            $("#mtmddl" + b + " dd ul").append('<li><a href="#">' + $(this).text() + '<span class="value">' + $(this).attr("value") + '</span></a></li>');
        });
    });
}
function fixCheckbox() {
    $("span.checkboxcheck,span.checkbox").each(function () {
        var checked = $(this).find('input:checkbox').is(':checked');
        if (checked == true && $(this).hasClass('checkboxcheck') == false) {
            $(this).removeClass('checkbox').addClass('checkboxcheck');
        }
        else if (checked == false && $(this).hasClass('checkbox') == false) {
            $(this).removeClass('checkboxcheck').addClass('checkbox');
        }
    });
}
$(function () {
    var hideDelay = 500;
    var id;
    var hideTimer = null;
    var container = $('<div id="mtPopupContainer"><div id="mtPopupContent"></div></div>');
    $('body').append(container);
    $('.mtPopupArticles').live('mouseover', function () {
        var settings = $(this).attr('rel').split(',');
        var type = settings[0];
        id = settings[1];
        if (id == '')
            return;
        if (hideTimer)
            clearTimeout(hideTimer);

        var pos = $(this).offset();
        var width = $(this).width();
        container.css({
            left: (pos.left + width) + 'px',
            top: pos.top - 5 + 'px'
        });
        $('#mtPopupContent').html('&nbsp;');
        $.ajax({
            type: 'Get',
            url: '/Article/Tooltip',
            cache: false,
            data: 'id=' + type + '&id1=' + id,
            success: function (data) {
                $('#mtPopupContent').html(data);
            }
        });
        container.css('display', 'block');
    });

    $('.mtPopupArticles').live('mouseout', function () {
        if (hideTimer)
            clearTimeout(hideTimer);
        hideTimer = setTimeout(function () {
            container.css('display', 'none');
        }, hideDelay);
    });

    // Allow mouse over of details without hiding details  
    $('#mtPopupContainer').mouseover(function () {
        if (hideTimer)
            clearTimeout(hideTimer);
    });

    // Hide after mouseout  
    $('#mtPopupContainer').mouseout(function () {
        if (hideTimer)
            clearTimeout(hideTimer);
        hideTimer = setTimeout(function () {
            container.css('display', 'none');
        }, hideDelay);
    });
});  