var jHtmlArea_API = new Object();
var identity = '';
$(document).ready(function () {
    $.ajax({ type: "POST",
        url: "/mtm/Theme",
        success: function (data) {
            $('#article').htmlarea({
                css: '/Theme/' + data + '/TextAreaContainer.css',
                toolbar: [
            ["html"], ["|"], ["bold", "italic", "underline", "strikethrough"], ["|"], ["subscript", "superscript"], ["|"],

         ["smile"], ["|"],
        ["cut", "copy", "paste"]
        ]
            });
            $('input.smile').click(function () {
                var htmlText = $(this).val();
                jHtmlArea_API['#' + identity][0].pasteHTML(htmlText);
                $('div.mask, div.window').hide();
            });
        }


    });
   
    $("#close").live("click", function () {
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