﻿$(document).ready(function () {
    var openid = new Object();
    $('input.delete').live("click",
        function () {
            openid = $(this).attr("value");
            var id = 'div.window#deleteModal';
            $(id).css({ width: (326 + 'px') });
            var maskHeight = $(document).height();
            var maskWidth = $(window).width();
            $('div.mask').css({ 'width': maskWidth, 'height': maskHeight });
            $('div.mask').show();
            $('div.mask').fadeTo("fast", 0.1);
            var winH = $(window).height();
            var winW = $(window).width();
            $(id).css('top', (winH / 2 - $(id).height() / 2) + getScrollY());
            $(id).css('left', winW / 2 - $(id).width() / 2);
            $(id).slideDown('slow');
        });
        $('input.rename').live("click",
        function () {
            openid = $(this).attr("value");
            window.location.href = openid;
        });
        $("#ok").live("click", function () {
            window.location.href = openid;
        $('div.mask, div.window').hide();
    });
    $("#cancel").live("click", function () {
        $('div.mask,div.window').hide();
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