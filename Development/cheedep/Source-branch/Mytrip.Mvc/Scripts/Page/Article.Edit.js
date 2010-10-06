var jHtmlArea_API = new Object();
var identity = '';
$(document).ready(function () {
    CheckOptions();
    $("div[id='editors'] textarea[id^='editor_']").each(function () {
        var name = $(this).attr('id');
        BuildjHtml(name);
    });
    AddEditor();
    CheckPages();
    $.ajax({ type: "POST",
        url: "/MytripMvc/Theme",
        success: function (data) {
            $('#fotoabstract').htmlarea({
                css: '/Theme/' + data + '/TextAreaContainer.css',
                toolbar: [
            [{
                css: 'image', text: 'Image Gallery',
                action: function (btn) {
                    jHtmlArea_API['#fotoabstract'] = $(this);
                    var url = '/TextAreaFile/Index/()Content()Articles/fotoabstract';
                    var gallery = window.open(url, 'gallery', 'width=800,height=600,menubar=0,location=0,resizable=0,scrollbars=1,status=0');
                    gallery.focus();
                }
            }]]
            });
            $('#abstract').htmlarea({
                css: '/Theme/' + data + '/TextAreaContainer.css',
                toolbar: [
            ["html"], ["|"], ["bold", "italic", "underline", "strikethrough"], ["|"], ["subscript", "superscript"], ["|"],
         ["smile"], ["|"], ["cut", "copy", "paste"]
        ]
            });
            $('#article').htmlarea({
                css: '/Theme/' + data + '/TextAreaContainer.css',
                toolbar: [
            ["html"], ["|"], ["bold", "italic", "underline", "strikethrough"], ["|"], ["subscript", "superscript"], ["|"],
        ["increasefontsize", "decreasefontsize"], ["|"],
        ["orderedlist", "unorderedlist"], ["|"],
        ["indent", "outdent"], ["|"], ["horizontalrule"], ["|"],
        ["justifyleft", "justifycenter", "justifyright"], ["|"],
        ["link", "unlink"], ["|"],
        [{
            css: 'image', text: 'Image Gallery',
            action: function (btn) {
                jHtmlArea_API['#abstract'] = $(this);
                var url = '/TextAreaFile/Index/()Content()Articles/article';
                var gallery = window.open(url, 'gallery', 'width=800,height=600,menubar=0,location=0,resizable=0,scrollbars=1,status=0');
                gallery.focus();
            }
        }],

        ["|"], ["p", "h1", "h2", "h3", "h4", "h5", "h6"], ["|"],
        ["smile"], ["|"],["cut", "copy", "paste"]
        ]
            });
            $('input.smile').click(function () {
                var htmlText = $(this).val();
                jHtmlArea_API['#' + identity][0].pasteHTML(htmlText);
                $('div.mask, div.window').hide();
            });
        }
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
function CheckOptions() {
    var st = $('.dropdown dt a').find('.value').html();
    $('.dropdown dd ul li a').click(function () {
        var selected = $(this).find('.value').html();
        if (st != selected) {
            $.ajax({ type: "POST",
                url: "/Article/CheckAllCulture",
                data: 'id=' + selected,
                success: function (data) {
                    if (data == "True") {
                        $("#ShowAllCulture").show();
                    }
                    else {
                        $("#ShowAllCulture").hide();
                        $("#AllCulture").attr('checked', false);
                    }
                }
            });
        }
    });
    if (!$("#ApprovedComment").is(':checked')) {
        $("#moderateComments").hide();
    }
    $("#ApprovedComment").click(function () {
        var comchecked = $(this).is(':checked');
        if (comchecked) {
            if (!$("#OnlyForRegisterUser").is(':checked')) {
                $("#showAnonymComment").show();
            }
            $("#moderateComments").show();
        }
        else {
            $("#showAnonymComment").hide();
            $("#moderateComments").hide();
            $("#IncludeAnonymComment").attr('checked', false);
            $("#ModerateComments").attr('checked', false);
        }
    });
    $("#OnlyForRegisterUser").click(function () {
        var regchecked = $(this).is(':checked');
        if (regchecked) {
            $("#IncludeAnonymComment").attr('checked', false);
            $("#showAnonymComment").hide();
        }
        else {
            if ($("#ApprovedComment").is(':checked')) {
                $("#showAnonymComment").show();
            }
        }
    });
}
function CheckPages() {
    $('#createarticle').click(function () {
        var isOk = true;
        $("div[id='editors'] textarea[id^='editor_']").each(function () {
            var id = $(this).attr('id');
            var value = $(this).val();
            var sp = $("div[id='editors']").find("#" + id + "_validationMessage");
            if (value == "") {
                isOk = false;
                sp.removeClass('field-validation-valid');
                sp.addClass('field-validation-error');
            }
            else {
                isOk = true;
                sp.addClass('field-validation-valid');
                sp.removeClass('field-validation-error');
            }
        });
        return isOk;
    })
}
function AddEditor() {
    var ctr = 2;
    $('#addneweditor').click(function () {
        var s = "editor_" + ctr;
        var ta = $("<div id='page_" + ctr + "'>" + ctr + "<div class='dark'><div class='darkTR'/><div class='darkTL'/><div class='darkTC'/><div class='darkC'><textarea cols='20' id='editor_" + ctr +
        "' name='pages' rows='2' style = 'height: 400px; width:100%;'></textarea>"
                + "<span class='field-validation-valid' id='editor_"
                + ctr +
                 "_validationMessage'><%=ArticleLanguage.content_empty %></span></div><div class='darkBR'/><div class='darkBL'/><div class='darkBC'/></div></div>");
        if (ctr == 2) {
            $("#deleteeditor").show();
        };
        ta.appendTo("#editors");
        BuildjHtml(s);
        ctr++;
        return false;
    })
    $('#deleteeditor').click(function () {
        //alert(ctr);
        $("div[id='editors'] div[id^='page_']:last").remove();
        ctr--;
        if (ctr == 2) {
            $("#deleteeditor").hide();
        };
        return false;
    })
}
function BuildjHtml(name) {
    $.ajax({ type: "POST",
        url: "/MytripMvc/Theme",
        success: function (data) {
            var ed = $('#editors').find("#" + name);
            ed.htmlarea({
                css: '/Theme/' + data + '/TextAreaContainer.css',
                toolbar: [
            ["html"], ["|"], ["bold", "italic", "underline", "strikethrough"], ["|"], ["subscript", "superscript"], ["|"],
        ["increasefontsize", "decreasefontsize"], ["|"],
        ["orderedlist", "unorderedlist"], ["|"],
        ["indent", "outdent"], ["|"], ["horizontalrule"], ["|"],
        ["justifyleft", "justifycenter", "justifyright"], ["|"],
        ["link", "unlink"], ["|"],
        [{
            css: 'image', text: 'Image Gallery',
            action: function (btn) {
                jHtmlArea_API['#' + name] = $(this);
                var url = '/TextAreaFile/Index/()Content()Articles/' + name;
                var gallery = window.open(url, 'gallery', 'width=800,height=600,menubar=0,location=0,resizable=0,scrollbars=1,status=0');
                gallery.focus();
            }
        }],
        ["|"], ["p", "h1", "h2", "h3", "h4", "h5", "h6"], ["|"],
        ["smile"], ["|"], ["cut", "copy", "paste"]
        ]
            });

        }
    });
}
$(function () {
    $('#CloseDate').datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'yy-mm-dd'
    });
});