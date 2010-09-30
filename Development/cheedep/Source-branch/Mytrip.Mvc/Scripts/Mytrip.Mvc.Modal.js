$(document).ready(function () {
        $("div.window").append("<div class='modalBL'/><div class='modalBR'/><div class='modalBC'/>");
        $("div.window").prepend("<div class='modalTL'/><div class='modalTR'/>");
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