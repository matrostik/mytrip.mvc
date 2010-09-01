$(document).ready(function () {
    $('input[type=file]').change(function () {
        $('#filetext').attr('value', $('input[type=file]').val());
        $('.textcontent').removeClass("ac");
        $('.textleft').removeClass("ac");
        $('.textright').removeClass("ac");
        $('#divfiletext .textbox').toggleClass("ac");
        $('#divfiletext .textbox').find('.textcontent').addClass("ac");
        $('#divfiletext .textbox').find('.textleft').addClass("ac");
        $('#divfiletext .textbox').find('.textright').addClass("ac");
    });
    $('input[type=file]').hover(
        function () {
            $('div.file').addClass("ac");

        },
        function () {
            $('div.file').removeClass("ac");
        }
    );
        var openid = new Object();
        $('input.delete').click(
        function () {
            openid = this.value;
            var id = 'div.window';
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
        $('input.rename').click(
        function () {
            openid = this.value;
            $(location).attr('href', openid);
        });
        $('div.buttonIWR').click(
        function () {
            $(location).attr('href', openid);
            $('div.mask, div.window').hide();
        });
        $('div.buttonIR').click(function () {
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