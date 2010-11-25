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
        $(location).attr('href', "/Store/CountProduct" + g);
    });
    if ($('div.yes').length != 0) {
        var id = 'div.window#_order';
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
    }
    if ($('div.onlinebuy').length != 0) {
        var id = 'div.window#_onlinebuy';
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
    }
    $('input#order').live('click',
        function () {
            var id = 'div.window#_order';
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
    $('input#variant').live('click',
        function () {
            var id = 'div.window#_variant';
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
    $('input#organisation').live('click',
        function () {
            $('input#orgOrPriv').val('true');
            $('div#vorg').show();
            $('div.mask, div.window').hide();
            var id = 'div.window#_order';
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
    $('input#people').live('click',
        function () {
            $('input#orgOrPriv').val('false');
            $('div#vorg').hide();
            $('div.mask, div.window').hide();
            var id = 'div.window#_order';
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
    /*TAB*/
    $("input#orders0").live('click', function () {
        $('div.tabI').each(function () {
            $(this).find("div.tabR, div.tabL, div.tabC").removeClass("ac");
        });
        var a = $(this).parent("div.tabC");
        var b = $(a).parent("div.tabI");
        $(b).find("div.tabR, div.tabL, div.tabC").addClass("ac");
        $("div#_orders1").hide();
        $("div#_orders2").hide();
        $("div#_orders3").hide();
        $("div#_orders4").hide();
        $("div#_orders0").show();

    });
    $("input#orders1").live('click', function () {
        $('div.tabI').each(function () {
            $(this).find("div.tabR, div.tabL, div.tabC").removeClass("ac");
        });
        var a = $(this).parent("div.tabC");
        var b = $(a).parent("div.tabI");
        $(b).find("div.tabR, div.tabL, div.tabC").addClass("ac");
        $("div#_orders0").hide();
        $("div#_orders2").hide();
        $("div#_orders3").hide();
        $("div#_orders4").hide();
        $("div#_orders1").show();

    });
    $("input#orders2").live('click', function () {
        $('div.tabI').each(function () {
            $(this).find("div.tabR, div.tabL, div.tabC").removeClass("ac");
        });
        var a = $(this).parent("div.tabC");
        var b = $(a).parent("div.tabI");
        $(b).find("div.tabR, div.tabL, div.tabC").addClass("ac");
        $("div#_orders0").hide();
        $("div#_orders1").hide();
        $("div#_orders3").hide();
        $("div#_orders4").hide();
        $("div#_orders2").show();

    });
    $("input#orders3").live('click', function () {
        $('div.tabI').each(function () {
            $(this).find("div.tabR, div.tabL, div.tabC").removeClass("ac");
        });
        var a = $(this).parent("div.tabC");
        var b = $(a).parent("div.tabI");
        $(b).find("div.tabR, div.tabL, div.tabC").addClass("ac");
        $("div#_orders0").hide();
        $("div#_orders1").hide();
        $("div#_orders2").hide();
        $("div#_orders4").hide();
        $("div#_orders3").show();

    });
    $("input#orders4").live('click', function () {
        $('div.tabI').each(function () {
            $(this).find("div.tabR, div.tabL, div.tabC").removeClass("ac");
        });
        var a = $(this).parent("div.tabC");
        var b = $(a).parent("div.tabI");
        $(b).find("div.tabR, div.tabL, div.tabC").addClass("ac");
        $("div#_orders0").hide();
        $("div#_orders1").hide();
        $("div#_orders2").hide();
        $("div#_orders3").hide();
        $("div#_orders4").show();

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