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
            $('#fotoabstract').htmlarea({
                css: '/Theme/' + data + '/TextAreaContainer.css',
                toolbar: [
            [{
                css: 'image', text: 'Image Gallery',
                action: function (btn) {
                    jHtmlArea_API['#fotoabstract'] = $(this);
                    var url = '/TextAreaFile/Index/()Content()UserFiles/fotoabstract';
                    var gallery = window.open(url, 'gallery', 'width=800,height=600,menubar=0,location=0,resizable=0,scrollbars=1,status=0');
                    gallery.focus();
                }
            }]]
        });
        $('#hotel').htmlarea({
            css: '/Theme/' + data + '/TextAreaContainer.css',
            toolbar: [
           ["html"]]
        });
        $('#services').htmlarea({
            css: '/Theme/' + data + '/TextAreaContainer.css',
            toolbar: [
           ["html"]]
        });
            $('input.smile').click(function () {
                var htmlText = $(this).val();
                jHtmlArea_API['#' + identity][0].pasteHTML(htmlText);
                $('div.mask, div.window').hide();
            });
            $('div#varr').click(
        function () {
            var id = '#variantForm';
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

        }


    });
    $('#myForm').ajaxForm(function (data) {
        $('#_myForm').html(data);
        $('div.mask,div.window').hide();
    });
    $("#close").live("click", function () {
        $('div.mask,div.window').hide();
    });
    $('div.deletevariant').live("click", function () {
        var id = "id=" + $(this).attr("id");
        $.ajax({ type: "POST",
            url: "/Tours/DeleteVariant",
            data: id,
            success: function (data) {
                $("#_myForm").html(data);
            }
        });
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
$(function () {
    $('#startdate').datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'yy-mm-dd'
    });
    $('#stopdate').datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'yy-mm-dd'
    });
});