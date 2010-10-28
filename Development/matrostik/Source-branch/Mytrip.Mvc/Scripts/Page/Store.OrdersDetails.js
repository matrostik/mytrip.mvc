$(document).ready(function () {
    $("input[type=text]").live('keypress focus click', function () {
        var b = $(this).parent("div.textC");
        var c = $(b).parent("div.textbox");
        var a = $(c).parent("td");
        $(a).find("div.right").show();
    });
    $("div.right").live('click', function () {
        var a = $(this).parent("td");
        var d = $(a).parent("tr");
        var f = $(d).find("h3").attr("id");
        var c = $(a).find("input[type=text]").val();
        var g = "/" + c + "/" + f;
        $(location).attr('href', "/Store/CountProductManager" + g);
    });
});