var jHtmlArea_API = new Object();
$(document).ready(function () {
    $.ajax({ type: "POST",
        url: "/mtm/Theme",
        success: function (data) {
            $('#article').htmlarea({
                css: '/Theme/' + data + '/TextAreaContainer.css',
                toolbar: [
            ["html"], ["|"], ["bold", "italic", "underline", "strikethrough"], ["|"], ["subscript", "superscript"], ["|"],
            ["increasefontsize", "decreasefontsize"], ["|"], ["orderedlist", "unorderedlist"], ["|"],
            ["indent", "outdent"], ["|"], ["horizontalrule"], ["|"], ["justifyleft", "justifycenter", "justifyright"],
            ["|"], ["link", "unlink"], ["|"],
            [{
            css: 'image', text: 'Image Gallery',
            action: function (btn) {
                jHtmlArea_API['#article'] = $(this);
                var url = '/TextAreaFile/Index/()Content()UserFiles/article';
                var gallery = window.open(url, 'gallery', 'width=800,height=600,menubar=0,location=0,resizable=0,scrollbars=1,status=0');
                gallery.focus();
            }
        }],
            ["|"], ["p", "h1", "h2", "h3", "h4", "h5", "h6"], ["|"],
            ["smile"], ["|"],["cut", "copy", "paste"]
            ]
            });
        }
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