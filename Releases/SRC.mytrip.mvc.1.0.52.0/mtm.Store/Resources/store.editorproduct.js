$.getScript('/Scripts/mtm.jHtmlArea.js');
$.getScript('/Scripts/mtm.jHtmlArea.Smiles.js');
var jHtmlArea_API = new Object();
var identity = '';
$(document).ready(function () {
    $.ajax({ type: "POST",
        url: "/mtm/Theme",
        success: function (data) {
            $('#abstract').htmlarea({
                css: '/Theme/' + data + '/TextAreaContainer.css',
                toolbar: [
           ["html"], ["|"], ["bold", "italic", "underline", "strikethrough"], ["|"], ["subscript", "superscript"], ["|"],
           ["smile"], ["|"], ["cut", "copy", "paste"]]
            });
            $('#article').htmlarea({
                css: '/Theme/' + data + '/TextAreaContainer.css',
                toolbar: [
            ["html"], ["|"], ["bold", "italic", "underline", "strikethrough"], ["|"], ["subscript", "superscript"], ["|"],
            ["smile"], ["|"], ["cut", "copy", "paste"]]
            });
        }

    });
    if ($.browser.safari) {
        $('input[type=file]').live("change", function () {
            $('input#' + $(this).attr('name')).attr('value', $(this).attr('value'));
        });
    }
    else {
        $('input[type=file]').change(function () {
            $('input#' + $(this).attr('name')).attr('value', $(this).attr('value'));
        });
    }
    $('input[type=file]').hover(
        function () {
            $('div.file').addClass("ac");
        },
        function () {
            $('div.file').removeClass("ac");
        }
    );

    $('#myForm').ajaxForm(function (data) {
        $('#_myForm').html(data);
    });
    $('#myForm1').ajaxForm(function (data) {
        $('#_myForm1').html(data);
    });
    var openid2 = new Object();
    var openid3 = new Object();
    $('input.deleteImg').live('click',
        function () {
            openid2 = this.value;
            openid3 = "yes";
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
    $('input.deleteImg2').live('click',
        function () {
            openid2 = this.value;
            openid3 = "no";
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
    $("#enter").live("click", function () {

        $.ajax({ type: "POST", url: openid2,
            success: function (data) {
                if (openid3 == "yes")
                    $('#_myForm').html(data);
                else
                    $('#_myForm1').html(data);
            }
        });

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