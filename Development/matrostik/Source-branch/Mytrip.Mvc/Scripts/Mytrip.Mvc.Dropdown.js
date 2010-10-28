$(document).ready(function () {
    createDropDown();
    $('div.dropdown').each(function () {
        var a = $(this).html();
        $(this).html('<div class="dropdownR"/><div class="dropdownL"/><div class="dropdownC">' + a + '</div>');
    });
    $("dl.dropdown dt a").live("click", function () {
        var a = $(this).closest("dl"); a.find("dd ul").toggle(); return false;
    });
    $(document).bind('click', function (e) {
        var $clicked = $(e.target);
        if (!$clicked.parents().hasClass("dropdown"))
            $("dl.dropdown dd ul").hide();
    });
    $("dl.dropdown dd ul li a").bind('click', function (e) {
        var a = $(this).closest("dl");
        var id = a.attr('id').replace("mtmddl", "");
        var b = $(this).html();
        a.find("dt a").html(b);
        a.find("dd ul").hide();
        var c = $("select[id=" + id + "]");
        c.val($(this).find("span.value").html());
        return false
    });
    $("dl.dropdown dd ul li a").click(function () {
        var a = $(this).closest("dl");
        var id = a.attr('id').replace("mtmddl", "");
        var b = $(this).html();
        a.find("dt a").html(b);
        a.find("dd ul").hide();
        var c = $("select[id=" + id + "]");
        c.val($(this).find("span.value").html());
        return false;
    });
    //checkboxes   .css({"z-index":"-9999","position":"relative"})
    $("input:checkbox").each(function () {
        var id = $(this).attr('id'); //alert(id);
        var cbx = $(this).css({ "z-index": "-9999", "position": "relative" });
        //cbx.hide();
        if (cbx.is(':checked')) {
            $(cbx).wrap('<span id="mtmcbx' + id + '" class="checkboxcheck" />');
        }
        else {
            $(cbx).wrap('<span id="mtmcbx' + id + '" class="checkbox" />');
        }
    });
    $("span.checkbox").live("click", function () {
        //var id = $(this).attr('id').replace("mtmcbx", "");
        var ck = $(this).find('"input:checkbox"');
        //alert(ck.attr('id') + "  " + ck.attr('checked'));
        var id = ck.attr('id');
        //alert('span');
        $("input[id=" + id + "]").attr('checked', true);
        $("input[id=" + id + "]").triggerHandler('click');

        $(this).removeClass('checkbox').addClass('checkboxcheck');
        createChecbox();
    });
    $("span.checkboxcheck").live("click", function () {
        //var id = $(this).attr('id').replace("mtmcbx", "");
        var ck = $(this).find('"input:checkbox"');
        //alert(ck.attr('id') + "  " + ck.attr('checked'));
        var id = ck.attr('id');
        //alert('span');
        $("input[id=" + id + "]").attr('checked', false);
        $("input[id=" + id + "]").triggerHandler('click');



        $(this).removeClass('checkboxcheck').addClass('checkbox');
        createChecbox();
    });
//    $("input#IncludeAnonymComment").bind('change', function () {
//        var id = "mtmcbx" + $(this).attr('id');
//        alert('change id=' + id);
//        //$(this).triggerHandler('click');
//    });
    $("input#IncludeAnonymComment").load(function () {
        alert('loaded');
        //$(this).triggerHandler('click');
    });
    //
});
function createChecbox() {
    $("span.checkboxcheck,span.checkbox").each(function () {
        //alert($(this).attr('id'));
        var cbx = $(this).find('input:checkbox').is(':checked'); //alert($(this).attr('id')+'='+cbx);
        if (cbx != $(this).hasClass('checkboxcheck')) {
            //alert($(this).attr('id') + ' - no');
            $(this).removeClass('checkboxcheck').addClass('checkbox');
        }
    });
}
function createDropDown() { 
var a = $("select");
$("select").each(function () {
    var a = $(this);
    var b = a.attr('id');
    var c = a.find("option[selected]");
    var d = $("option", a); a.before('<div class="dropdown"><dl id="mtmddl' + b + '" class="dropdown"></dl></div>');
    $("#mtmddl" + b).append('<dt><a href="#">' + c.text() + '<span class="value">' + c.val() + '</span></a></dt>');
    $("#mtmddl" + b).append('<dd><ul></ul></dd>');
    d.each(function () {
        $("#mtmddl" + b + " dd ul").append('<li><a href="#">' + $(this).text() + '<span class="value">' + $(this).val() + '</span></a></li>');
    });
}); 
}