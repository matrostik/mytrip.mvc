var jHtmlArea_API = new Object();
var identity = '';
$(document).ready(function () {
    $.ajax({ type: "POST",
        url: "/MytripMvc/Theme",
        success: function (data) {           
            $('#abstract').htmlarea({
                css: '/Theme/' + data + '/TextAreaContainer.css',
                toolbar: [
            ["html"], ["|"], ["bold", "italic", "underline", "strikethrough"], ["|"], ["subscript", "superscript"], ["|"],

         [{
             css: 'smile', text: 'Smiles', action: function (btn) {
                 jHtmlArea_API['#abstract'] = $(this);
                 identity = 'abstract';
                 openid = this.value;
                 var id = 'div.window#smile';
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
         }], ["|"],
        ["cut", "copy", "paste"]
        ]
            });
     $('#article').htmlarea({
         css: '/Theme/' + data + '/TextAreaContainer.css',
         toolbar: [
            ["html"], ["|"], ["bold", "italic", "underline", "strikethrough"], ["|"], ["subscript", "superscript"], ["|"],

         [{
             css: 'smile', text: 'Smiles', action: function (btn) {
                 jHtmlArea_API['#article'] = $(this);
                 identity = 'article';
                 openid = this.value;
                 var id = 'div.window#smile';
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
         }], ["|"],
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
                $('#_myForm').html(data);
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