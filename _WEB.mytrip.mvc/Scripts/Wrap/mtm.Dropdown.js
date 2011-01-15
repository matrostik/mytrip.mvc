$(document).ready(function () {
    createDropDown();
    $('div.dropdown').each(function () {
        var a = $(this).html();
        $(this).html('<div class="dropdownR"/><div class="dropdownL"/><div class="dropdownC">' + a + '</div>');
    });
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
});
function createDropDown() { 
var a = $("select");
$("select").each(function () {
    var a = $(this);
    var b = a.attr('id');
    var c = a.find("option[selected]");
    var d = $("option", a); a.before('<div class="dropdown"><dl id="mtmddl' + b + '" class="dropdown"></dl></div>');
    $("#mtmddl" + b).append('<dt><a href="#">' + c.text() + '<span class="value">' + c.attr("value") + '</span></a></dt>');
    $("#mtmddl" + b).append('<dd><ul></ul></dd>');
    d.each(function () {
        $("#mtmddl" + b + " dd ul").append('<li><a href="#">' + $(this).text() + '<span class="value">' + $(this).attr("value") + '</span></a></li>');
    });
}); 
}