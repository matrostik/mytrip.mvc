$.getScript('/Scripts/mtm.jHtmlArea.js');
$.getScript('/Scripts/mtm.jHtmlArea.Smiles.js');
var jHtmlArea_API = new Object();
var theme = '';
var identity = '';
var link = '';
$(document).ready(function () {
    modalSetup();
    editCategory();
    EditComment();
    RatingArticle();
    QuoteComment();
    AddRemoveEditors();
    CreateEditArticleOptions();
    editJournalists();
    CommentVote();
});
//Manage editors
function editJournalists() {
    var st = $('dl#mtmddlUser dt a').find('span.value').html();
    $('dl#mtmddlUser dd ul li a').click(function () {
        var selected = $(this).find('span.value').html();
        if (st != selected)
            $("#journalistForm").submit();
    });
    $("a[id^='modalJournalist_']").click(
        function () {
            var place = $(this).attr('rel');
            $("#Data").val(place);
            if (!place.match(/_0$/) || place.indexOf("Article") != -1)
                $("#subcats").hide();
            else
                $("#subcats").show();
            setModalMask('modalJournalist');
            return false;
        });
    $("#okJournalistEdit").live("click", function () {
        $("#modalJournalistForm").submit();
        $('div.mask, div.window').hide();
    });
}
function modalSetup() {
    $("a[id^='delete']").click(function () {
        link = $(this).attr('href');
        setModalMask('deleteModal');
        return false;
    });
    $("input#ok").live("click", function () {
        window.location.href = link;
        $('div.mask, div.window').hide();
    });
    $("input#cancel").live("click", function () {
        $('div.mask, div.window').hide();
        return false;
    });
}
function editCategory() {
    ///add new category including subcategory
    $("a#addnewcategory").click(function () {
        $("input#catTitle").removeClass('input-validation-error');
        $('span#titleError').removeClass('field-validation-error').addClass('field-validation-valid');
        $("input#modalEditId").val(0);
        $("input#catTitle").attr('name', $(this).find('img').attr('alt'));
        $("div#modalddlcategories").hide();
        $("#modalShowMenu,#modalShowLang").show();
        $("#modalCategory input:checkbox").attr('checked', false);
        fixCheckbox();
        setModalMask('modalCategory');
        return false;
    });
    $("#modalSubCategory").click(function () {
        if ($(this).is(':checked')) {
            $("div#modalddlcategories").show();
            $("#modalShowMenu,#modalShowLang").hide();
            $("input#modalEditId").val($('dl#mtmddlmodalCategoryId dt a').find('.value').html());
        }
        else {
            $("div#modalddlcategories").hide();
            $("#modalShowMenu,#modalShowLang").show();
            $("#modalSeparateBlock,#modalAllCulture").attr('checked', false);
            $("input#modalEditId").val(0);
        }
        $("#modalddlcategories dl.dropdown dd ul li").remove(":contains('--')");
        if (CheckCategoryAllCulture($('dl#mtmddlmodalCategoryId dt a').find('.value').html()) == 'true')
            $("#modalShowLang").show();
    });
    $('dl#mtmddlmodalCategoryId dd ul li a').click(function () {
        var selectedid = $(this).find('.value').html();
        if (CheckCategoryAllCulture(selectedid) == 'true') {
            $("#modalShowLang").show();
        }
        else {
            $("#modalShowLang").hide();
            $("#modalAllCulture").attr('checked', false);
            fixCheckbox();
        }
        $("input#modalEditId").val(selectedid);
    });
    /// add or edit category or subcategory
    $("a[id^='modalCategory_']").click(function () {
        $("input#catTitle").val($(this).attr('name'));
        $("input#catTitle").attr('name', $(this).find('img').attr('alt'));
        if ($(this).find('img').attr('alt').indexOf("Edit") != -1) {
            $("div#modalCategory div.modalTC").text($(this).attr('title')).prepend("<img alt='edit' class='img14' src='/Theme/" + theme + "/images/edite.png'></img> ");
            //$("input#catTitle").attr('name', $("#Path").val());
        }
        else {
            $("div#modalCategory div.modalTC").text($(this).text()).prepend("<img alt='edit' class='img14' src='/Theme/" + theme + "/images/add.png'></img> ");
            //$("input#catTitle").attr('name', 'CreateCategory');
        }
        $("input#catTitle").removeClass('input-validation-error');
        $('span#titleError').removeClass('field-validation-error').addClass('field-validation-valid');
        var opts = $(this).attr('rel').split('_');
        //rel="catId_showMenu_showLang_checkMenu_checkLang"
        var catId = opts[0]; var showMenu = opts[1]; var showLang = opts[2]; var checkMenu = opts[3]; var checkLang = opts[4];
        $("input#modalEditId").val(catId);
        if (showMenu == 'false')
            $("div#modalShowMenu").hide();
        else {
            $("div#modalShowMenu").show();
            if (checkMenu == 'true')
                $("#modalSeparateBlock").attr('checked', true);
            else
                $("#modalSeparateBlock").attr('checked', false);
        }
        if (showLang == 'false')
            $("div#modalShowLang").hide();
        else {
            $("div#modalShowLang").show();
            if (checkLang == 'true')
                $("#modalAllCulture").attr('checked', true);
            else
                $("#modalAllCulture").attr('checked', false);
        }
        fixCheckbox();
        setModalMask('modalCategory');
        return false;
    });
    $("input#okEditCategory").live("click", function () {
        $.ajax({
            type: 'POST',
            url: '/Article/Category',
            data: 'id=' + $("input#modalEditId").val() + '&path=' + $("input#catTitle").attr('name') + '&title=' + $("input#catTitle").val() + '&menu=' + $("#modalSeparateBlock").attr('checked')
            + '&allculture=' + $("#modalAllCulture").attr('checked'),
            success: function (data) {
                if (data) {
                    var err = $('span#titleError');
                    err.text(data);
                    err.removeClass('field-validation-valid').addClass('field-validation-error');
                    $("input#catTitle").addClass('input-validation-error');
                }
                else {
                    location.reload();
                }
            }
        });
    });
}
function EditComment() {
    BuildjHtml('addCommentEditor');
    $("a[id^='inlineEditComment']").click(function () {
        $("table.comment").show()
        $("div#inlineEditComment").hide().fadeIn('fast');
        $('span#Comment_Error').removeClass('field-validation-error').addClass('field-validation-valid');
        var comment = $(this).closest("div").next("div").find(".commentC");
        var table = $(this).closest("table.comment")
        $("input#editId").val($(this).attr('rel'));
        $("textarea#inlineCommentEditor").val(comment.html());
        var edit = $("div#inlineEditComment");
        $(table).after(edit);
        edit.show();
        table.hide();
        BuildjHtml('inlineCommentEditor');
        var obj = window.jHtmlArea($('textarea#inlineCommentEditor'));
        obj.updateHtmlArea();
        return false;
    });
    $("input#okInlineEditComment").live("click", function () {
        var comId = $("input#editId").val();
        var text = $('textarea#inlineCommentEditor').val();
        $.ajax({ type: "POST",
            url: "/Article/Comment",
            data: 'id=' + comId + '&comment=' + text + '&approved=' + $("#CommentApproved").val(),
            success: function (data) {
                if (data) {
                    var err = $('span#Comment_Error');
                    err.text(data);
                    err.removeClass('field-validation-valid').addClass('field-validation-error');
                }
                else {
                    $("#" + comId).find(".commentC").html(text);
                    $("div#inlineEditComment").hide();
                    $("table.comment:hidden").show();
                }
            }
        });
    });
    BuildjHtml('modalCommentEditor');
    $("a[id^='editComment']").click(function () {
        $('span#Comment_Error').removeClass('field-validation-error').addClass('field-validation-valid');
        var comId = $(this).attr('rel');
        $("input#editId").val(comId);
        $.ajax({ type: "Get",
            url: "/Article/Comment",
            cache: false,
            data: 'id=' + comId,
            success: function (data) {
                $("textarea#modalCommentEditor").val(data);
                var obj = window.jHtmlArea($('textarea#modalCommentEditor'));
                obj.updateHtmlArea();
            }
        });
        setModalMask('modalComment');
        return false;
    });
    $("input#okEditComment").live("click", function () {
        var comId = $("input#editId").val();
        var text = $('textarea#modalCommentEditor').val();
        $.ajax({ type: "POST",
            url: "/Article/Comment",
            data: 'id=' + comId + '&comment=' + text + '&approved=true',
            success: function (data) {
                if (data) {
                    var err = $('span#Comment_Error');
                    err.text(data);
                    err.removeClass('field-validation-valid').addClass('field-validation-error');
                }
                else {
                    $('div.mask, div.window').hide();
                }
            }
        });
    });
    $("#cancelInlineEdit").live("click", function () {
        $("div#inlineEditComment").hide();
        $("table.comment").show();
    });
}
function CreateEditArticleOptions() {
    if ($("#ApprovedComment").is(':checked')) {
        $("#moderateComments,#commentVotes").show();
    }
    else {
        $("#moderateComments").hide();
    }
    $('dl#mtmddlCategoryId dd ul li a').click(function () {
        if (CheckCategoryAllCulture($(this).find('.value').html()) == "true") {
            $("#ShowAllCulture").show();
        }
        else {
            $("#ShowAllCulture").hide();
            $("#AllCulture").attr('checked', false);
        }
    });
    $("#ApprovedComment").click(function () {
        var comchecked = $(this).is(':checked');
        if (comchecked) {
            if (!$("#OnlyForRegisterUser").is(':checked'))
                $("#showAnonymComment").show();
            $("#moderateComments,#commentVotes").show();
        }
        else {
            $("#moderateComments,#commentVotes,#showAnonymComment").hide();
            $("#ModerateComments,#CommentVotes,#IncludeAnonymComment").attr('checked', false);
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
    if($('#CloseDate').length != 0) {
        $('#CloseDate').datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'yy-mm-dd'
    });
    }
}
function AddRemoveEditors() {
    //
    BuildjHtml('fotoabstract');
    BuildjHtml('abstract');
    BuildjHtml('article');
    //
    var editors = $("div[id='editors'] textarea[id^='editor_']");
    editors.each(function () {
        BuildjHtml($(this).attr('id'));
    });
    var ctr = editors.length + 2;
    if (ctr > 2)
        $("#deleteeditor").show();

    $('#addneweditor').click(function () {
        var newpage = $("div#page_").clone();
        newpage.find('textarea').attr('id', 'editor_' + ctr);
        newpage.find('label').html(ctr);
        newpage.find('span').attr('id', 'editor_' + ctr + '_validationMessage');
        newpage.attr('id', 'page_' + ctr);
        newpage.appendTo("#editors");
        newpage.show();
        BuildjHtml("editor_" + ctr);
        ctr++;
        $("#deleteeditor").show();
        return false;
    })
    $('#deleteeditor').click(function () {
        $("div[id='editors'] div[id^='page_']:last").remove();
        ctr--;
        if (ctr <= 2)
            $("#deleteeditor").hide();
        return false;
    })
    $("input#createEditArticle").live("click", function () {
        var isOk = true;
        $("div[id='editors'] textarea[id^='editor_']").each(function () {
            var id = $(this).attr('id');
            var value = $(this).val();
            var sp = $("div[id='editors']").find("#" + id + "_validationMessage");
            if (value == "") {
                isOk = false;
                sp.removeClass('field-validation-valid').addClass('field-validation-error');
                sp.show();
            }
            else
                sp.addClass('field-validation-valid').removeClass('field-validation-error');
        });
        return isOk;
    })
}
function BuildjHtml(name) {
    if (!theme) {
        $.ajax({ type: "POST",
            url: "/mtm/Theme",
            success: function (data) {
                theme = data;
            }
        });
    }
    if ($('textarea#' + name).length == 0)
        return false;
    if (name == "fotoabstract") {
        $('#fotoabstract').htmlarea({
            css: '/Theme/' + theme + '/TextAreaContainer.css',
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
    }
    else if (name == "abstract") {
        $('#abstract').htmlarea({
            css: '/Theme/' + theme + '/TextAreaContainer.css',
            toolbar: [
            ["html"], ["|"], ["bold", "italic", "underline", "strikethrough"], ["|"],
            ["subscript", "superscript"], ["|"], ["smile"], ["|"], ["cut", "copy", "paste"]
            ]
        });
    }
    else if (name.indexOf("Comment") != -1) {
        $('textarea#' + name).htmlarea({
            css: '/Theme/' + theme + '/TextAreaContainer.css',
            toolbar: [
           ["html"], ["|"], ["bold", "italic", "underline", "strikethrough"], ["|"], ["subscript", "superscript"]
            , ["|"], ["link", "unlink"], ["|"], ["smile"]
        ]
        });
    }
    else {
        var ed = $('#editors').find("#" + name);
        ed.htmlarea({
            css: '/Theme/' + theme + '/TextAreaContainer.css',
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
                var url = '/TextAreaFile/Index/()Content()UserFiles/' + name;
                var gallery = window.open(url, 'gallery', 'width=800,height=600,menubar=0,location=0,resizable=0,scrollbars=1,status=0');
                gallery.focus();
            }
        }],
        ["|"], ["p", "h1", "h2", "h3", "h4", "h5", "h6"], ["|"],
        ["smile"], ["|"], ["cut", "copy", "paste"]
        ]
        });
    }
}
function CheckCategoryAllCulture(selectedid) {
    return $.ajax({
        url: "/Article/CheckAllCulture",
        data: 'id=' + selectedid,
        async: false
    }).responseText;
}
function CommentVote() {
    $("a[id^='voteComment_']").click(function () {
        var a = $(this).attr('name');
        var id = $(this).attr('rel');
        //alert(a + id);
//        var t= $.ajax({
//            url: "/Article/VoteComment",
//            data: 'id=' + id + '&id2=' + a,
//            async: false
//            }).responseText;
//        $('div#voteCommentDiv' + id).html(t);
        $.ajax({
            url: "/Article/VoteComment",
            data: 'id=' + id + '&id2=' + a,
            cache: false,
            success: function (html) {
                $('div#voteCommentDiv' + id).html(html);
            }
        });
        return false;
    });
    
    var selectedid = 5;
//    return $.ajax({
//        url: "/Article/CheckAllCulture",
//        data: 'id=' + selectedid,
//        async: false
//    }).responseText;
}
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
function setModalMask(modalId) {
    var id = "div.window#" + modalId;
    if (modalId == 'modalComment')
        $(id).css({ width: (650 + 'px') });
    else
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
function QuoteComment() {
    $("a[id^='quote']").click(function () {
        var c = $(this).closest("div[id]");
        var a = c.find("a#user" + c.attr('id'));
        var selected = getSelected();
        if (!selected)
            selected = c.find("div.comment").html();
        var txt = "<br/><div style='border: 1px dotted #000000;'><STRONG>" + a.html() + "</STRONG>:<br/>" + selected + "</div><br/>";
        var obj = window.jHtmlArea($('textarea#addCommentEditor'));
        obj.pasteHTML(txt);
        return false;
    });
}
function RatingArticle() {
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
            if (rate >= star)
                $(this).removeAttr("style").removeClass("ratingempty").addClass("ratingfull");
            else
                $(this).removeAttr("style").removeClass("ratingfull").addClass("ratingempty");
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