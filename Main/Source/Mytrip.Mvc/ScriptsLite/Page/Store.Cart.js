$(document).ready(function () {
    $("input[type=text]").bind('keypress focus click', function () {
        var b = $(this).parent("div.textC");
        var c = $(b).parent("div.textbox");
        var a = $(c).parent("td");
        $(a).find("div.right").show();
    });

});