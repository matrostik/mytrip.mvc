var jHtmlArea_API = new Object();
$(document).ready(function () {
    var dw = $('div.divsmile').html();
    $('div.divsmile').html('<div class="modalTL"/><div class="modalTR"/>' + dw + '<div class="modalBL"/><div class="modalBR"/><div class="modalBC"/>');

    $('div.divsmile').hide();
    $('div.mask,div.modalTR').click(function () {
        $('div.mask, div.divsmile').hide();
    });


    
    Rating();
    $.ajax({ type: "POST",
        url: "/MytripMvc/Theme",
        success: function (data) {
            jHtmlArea_API['#article'] = $(this);
            $('#article').htmlarea({
                css: '/Theme/' + data + '/TextAreaContainer.css',
                toolbar: [
            ["html"], ["|"], ["bold", "italic", "underline", "strikethrough"], ["|"], ["subscript", "superscript"], ["|"],

        ["link", "unlink"], ["|"],
         [{
             css: 'smile', text: 'Smiles', action: function (btn) {
                 jHtmlArea_API['#article'] = $(this);
                 openid = this.value;
                 var id = 'div.divsmile';
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
         }]
        ]
            });
            $('input.smile').click(function () {
                var htmlText = $(this).val();
                jHtmlArea_API['#article'][0].pasteHTML(htmlText);
                $('div.mask, div.divsmile').hide();
            });

        }
    });
    Quote();
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
    $("#enter").live("click", function () {
        $(location).attr('href', openid);
        $('div.mask, div.window').hide();
    });
    $("#close").live("click", function () {
        $('div.mask, div.window').hide();
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
function Quote() {
    $("a[id^='quote']").click(function () {
        var c = $(this).closest("div[id]");
        var a = $(this).prev($("a"));
        //alert(c.attr('id') + '  ' + a.text());
        alert($('.htmlarea').html());
        alert($('.diviframe').html());
        //$('textarea#article').val($('textarea#article').val() + a.html());
        jHtmlArea_API['#article'][0].pasteHTML(a.html());
        return false;
    }); 
}
function Rating() {
    var sts = new Array;
    $("#votes input[name='vote']").each(function () {
        var style = $(this).attr("style");
        sts.push(style);
    });
    $("#votes input[name='vote']").click(function () {
        if ($("#votes input[name='vote']").attr('onclick') == null) {
            var vote = $(this).val();
            var count = $("#votes input[id='VotesCount']").val();
            var id = $("#Article_ArticleId").val();
            $("#votes").load("/Article/Rate", { id: id, vote: vote, count: count });
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