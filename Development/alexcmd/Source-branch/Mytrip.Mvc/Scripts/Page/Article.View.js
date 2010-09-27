var jHtmlArea_API = new Object();
var identity = '';
$(document).ready(function () {
    var dw = $('div.divsmile').html();
    $('div.divsmile').html('<div class="modalTL"/><div class="modalTR"/>' + dw + '<div class="modalBL"/><div class="modalBR"/><div class="modalBC"/>');

    $('div.divsmile').hide();
    $('div.mask,div.modalTR').click(function () {
        $('div.mask, div.divsmile').hide();
    });
    var theme;
    Rating();
    $.ajax({ type: "POST",
        url: "/MytripMvc/Theme",
        success: function (data) {
            theme = data;
            $('textarea#article').htmlarea({
                css: '/Theme/' + data + '/TextAreaContainer.css',
                toolbar: [
            ["html"], ["|"], ["bold", "italic", "underline", "strikethrough"], ["|"], ["subscript", "superscript"]
            , ["|"], ["link", "unlink"], ["|"],
         [{
             css: 'smile', text: 'Smiles', action: function (btn) {
                 jHtmlArea_API['#article'] = $(this);
                 identity = 'article';
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
                jHtmlArea_API['#' + identity][0].pasteHTML(htmlText);
                $('div.mask, div.divsmile').hide();
            });
        }
    });
    Quote();
    var openid = new Object();
    $("a[id^='delete']").click(function () {
        link = $(this).attr('href');
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
        return false;
    });
    $("a[id^='editComment']").click(function () {
        $("table.comment").show()
        $("div#editComment").hide().fadeIn('fast');
        $('span#Comment_Error').removeClass('field-validation-error').addClass('field-validation-valid');

        var comment = $(this).closest("div").next("div").find(".commentC");
        var table = $(this).closest("table.comment")

        $("input#editId").val($(this).attr('rel'));

        $("textarea#edit").val(comment.html());

        var edit = $("div#editComment");
        $(table).after(edit);
        edit.show();
        table.hide();

        $('textarea#edit').htmlarea({
            css: '/Theme/' + theme + '/TextAreaContainer.css',
            toolbar: [
           ["html"], ["|"], ["bold", "italic", "underline", "strikethrough"], ["|"], ["subscript", "superscript"]
            , ["|"], ["link", "unlink"], ["|"],
         [{
             css: 'smile', text: 'Smiles', action: function (btn) {
                 jHtmlArea_API['#edit'] = $(this);
                 openid = this.value;
                 identity = 'edit';
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
        var obj = window.jHtmlArea($('textarea#edit'));
        obj.updateHtmlArea();
        return false;
    });
    $('input.rename').click(
        function () {
            openid = this.value;
            $(location).attr('href', openid);
        });
    $("#enter").live("click", function () {
        $(location).attr('href', link);
        $('div.mask, div.window').hide();
    });
    $("input#edit").live("click", function () {
        //alert($('input#editId').val() + " " + $('textarea#edit').html() + " " + $("#CommentApproved").val());
        var com = "simple test<br/>";
        $.ajax({ type: "POST",
            url: "/Article/Comment",
            data: 'id=' + $("input#editId").val() + '&comment=' + $('textarea#edit').val() + '&approved=' + $("#CommentApproved").val(),
            success: function (data) {
                if (data) {
                    var err = $('span#Comment_Error');
                    err.text(data);
                    err.removeClass('field-validation-valid').addClass('field-validation-error');
                }
                else {
                    location.reload();
                }
            }
        });

    });
    $("#cancel").live("click", function () {
        $("div#editComment").hide();
        $("table.comment").show();
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
        var a = c.find("a#user" + c.attr('id'));
        var selected = getSelected();
        if (!selected) {
            selected = c.find("div.comment").html();
        }
        var txt = "<br/><div style='border: 1px dotted #000000;'><STRONG>" + a.html() + "</STRONG> wrote:<br/>" + selected + "</div><br/>";
        var obj = window.jHtmlArea($('textarea#article'));
        obj.pasteHTML(txt);
        return false;
    });
}
function getSelected() {
    if (window.getSelection) { return window.getSelection(); }
    else if (document.getSelection) { return document.getSelection(); }
    else {
        var selection = document.selection && document.selection.createRange();
        if (selection.text) { return selection.text; }
        return false;
    }
    return false;
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