$.getScript('/Scripts/mtm.jHtmlArea.js');
$.getScript('/Scripts/mtm.jHtmlArea.Smiles.js');
var jHtmlArea_API = new Object();
$(document).ready(function () {
    $.ajax({ type: "POST",
        url: "/mtm/Theme",
        success: function (data) {            
            $('#abstract').htmlarea({
                css: '/Theme/' + data + '/TextAreaContainer.css',
                toolbar: [
            ["html"], ["|"], ["bold", "italic", "underline", "strikethrough"], ["|"], ["subscript", "superscript"], ["|"],
            ["smile"], ["|"],
            ["cut", "copy", "paste"] ]
     });   
  }
});
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

    $('#myForm').ajaxForm(function (data) {
        $('#_myForm').html(data);
    });

var openid2 = new Object();
$('input.deleteImg').live('click',
        function () {
            openid2 = this.value;
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
$("#ok").live("click", function () {
    $.ajax({ type: "POST", url: openid2,
        success: function (data) {
            $('#_myForm').html(data);
        }
    });
    $('div.mask, div.window').hide();
});
$("#cancel").live("click", function () {
    $('div.mask,div.window').hide();
});
$('div#seo').live("click",
        function () {
            openid = $(this).attr("value");
            var id = 'div.window#seoModal';
            $(id).css({ width: (426 + 'px') });
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