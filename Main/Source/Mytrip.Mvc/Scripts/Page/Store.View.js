$(document).ready(function () {
    $.ajax({ type: "POST", url: "/Store/ProductCart",
        success: function (data) {
            $("#appr").html(data);
            if ($("div#review2").length != 0)
            { $("input#review").click(); }
            if ($("div#errorReview").length != 0) {
                $("input#review").click();
                var id = 'div.window#review';
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
        }
    });
    
    Rating();
    $("input.comparision").live('click', function () {

        var a = "id=" + $(this).attr('value');
        $.ajax({ type: "POST", url: "/Store/ProductForComparison", data: a });

    });
    $("input.cart").bind('click', function () {

        var a = "id=" + $(this).attr('value');
        $.ajax({ type: "POST", url: "/Store/ProductForCart", data: a,
            success: function (data) {
                $("#appr").html(data);
            }
        });
    });
    $("input#options").live('click', function () {
        $('div.tabI').each(function () {
            $(this).find("div.tabR, div.tabL, div.tabC").removeClass("ac");
        });
        var a = $(this).parent("div.tabC");
        var b = $(a).parent("div.tabI");
        $(b).find("div.tabR, div.tabL, div.tabC").addClass("ac");
        $("div#_foto").hide();
        $("div#_review").hide();
        $("div#_options").show();

    });
    $("input#foto").live('click', function () {
        $('div.tabI').each(function () {
            $(this).find("div.tabR, div.tabL, div.tabC").removeClass("ac");
        });
        var a = $(this).parent("div.tabC");
        var b = $(a).parent("div.tabI");
        $(b).find("div.tabR, div.tabL, div.tabC").addClass("ac");
        $("div#_options").hide();
        $("div#_review").hide();
        $("div#_foto").show();

    });
    $("input#review").live('click', function () {
        $('div.tabI').each(function () {
            $(this).find("div.tabR, div.tabL, div.tabC").removeClass("ac");
        });
        var a = $(this).parent("div.tabC");
        var b = $(a).parent("div.tabI");
        $(b).find("div.tabR, div.tabL, div.tabC").addClass("ac");
        $("div#_options").hide();
        $("div#_foto").hide();
        $("div#_review").show();

    });
    var openid = new Object();
    $('input.delete').click(
        function () {
            openid = this.value;
            var id = 'div.window#delete';
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
    $("#enter").live("click", function () {
        $(location).attr('href', openid);
        $('div.mask, div.window').hide();
    });
    $("#close").live("click", function () {
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
function Rating() {
    var sts = new Array;
    $("#votes input[name='vote']").each(function () {
        var style = $(this).attr("style");
        sts.push(style);
    });
    $("#votes input[name='vote']").click(function () {
        if ($("#votes input[name='vote']").attr('onclick') == null) {
            /*start review*/
            var id = 'div.window#review';
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
            /*end review*/
            var vote = $(this).val();
            var count = $("#votes input[id='VotesCount']").val();
            var id = $("#Store_StoreId").val();
            $("#votes").load("/Store/Rate", { id: id, vote: vote, count: count });
            $("input#review").click();
        }
    });
    $("#votes input[name='vote']").mouseover(function () {
        var rate = $(this).val();
        $("#votes input[name='vote']").each(function () {
            var star = $(this).val();
            if (rate >= star) {
                $(this).removeAttr("style").removeClass("ratingempty").addClass("ratingfull");
            }
            else {
                $(this).removeAttr("style").removeClass("ratingfull").addClass("ratingempty");
            }
        });
    });
    $("#votes").mouseleave(function () {
        var div = $("#votes");
        $(this).removeClass("ratingempty");
        $(this).removeClass("ratingfull")
        $.each(sts,
         function (ctr, val) {
             var id = ctr + 1;
             div.find("#vote" + id).removeClass("ratingempty").removeClass("ratingfull").attr("style", val);
         });
    });
}