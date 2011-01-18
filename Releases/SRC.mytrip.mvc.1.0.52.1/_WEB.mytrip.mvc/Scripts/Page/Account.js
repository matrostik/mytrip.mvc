$(document).ready(function () {
    var openid = new Object();
    $('input.nameopenid').live("click",
        function () {
            openid = $(this).attr("value");
            var id = 'div.window';
            $(id).css({ width: (326 + 'px') });
            var maskHeight = $(window).height();
            var maskWidth = $(window).width();
            $('div.mask').css({ 'width': maskWidth, 'height': maskHeight });
            $('div.mask').show();
            $('div.mask').fadeTo("fast", 0.1);
            var winH = $(document).height();
            var winW = $(window).width();
            $(id).css('top', (winH / 2 - $(id).height() / 2) + getScrollY());
            $(id).css('left', winW / 2 - $(id).width() / 2);
            $(id).slideDown('slow');
            $("#TitleOpenId").html($(this).attr("name"));
            $('#_op').focus();
            var a = $('#_op').parent('div.textC');
            var b = $(a).parent('div.textbox');
            $(b).find('div.textC, div.textL, div.textR').addClass("ac");
        });
        $('input.nonameopenid').live("click",
        function () {
            openid = $(this).attr("value");
            window.location.href = '/Account/OpenIdLogin?openid=' + openid;
        });
        $("#enter").live("click", function () {
            window.location.href = '/Account/OpenIdLogin?openid='
            + String.format(openid, $('#_op').attr('value'));
            $('div.mask, div.window').hide();
        });
        $("#close").live("click", function () {
        $('div.mask,div.window').hide();
        $('input[type=text]:first').focus();
        var a = $('input[type=text]:first').parent('div.textC');
        var b = $(a).parent('div.textbox');
        $(b).find('div.textC, div.textL, div.textR').addClass("ac");
    });
});
function getScrollY() {
    scrollY = 0;
    if (typeof window.pageYOffset == "number") {
        scrollY = window.pageYOffset;
    } else if (document.documentElement && document.documentElement.scrollTop) {
        scrollY = document.documentElement.scrollTop;
    } else if (document.body && document.body.scrollTop) {
        scrollY = document.body.scrollTop;
    } else if (window.scrollY) {
        scrollY = window.scrollY;
    }
    return scrollY;
} 