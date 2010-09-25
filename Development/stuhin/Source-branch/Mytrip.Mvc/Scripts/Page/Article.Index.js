﻿$(document).ready(function () {
    $("#close").live("click", function () {
        $('div.mask,div.window').hide();
    });
    $("#enter").live("click", function () {
        $(location).attr('href', link);
        $('div.mask, div.window').hide();
    });
    $("a[id^='editCat']").click(function () {
        $("input#catTitle").val($(this).attr('name'));
        $("input#catTitle").attr('name', $("#Path").val());
        $("div#editModal div.modalTC").text($(this).attr('title'));
        $("input#catTitle").removeClass('input-validation-error');
        $('span#Title_Error').removeClass('field-validation-error').addClass('field-validation-valid');
        var opts = $(this).attr('rel').split('_');
        //rel="catId_showMenu_showLang_checkMenu_checkLang"
        var catId = opts[0];
        var showMenu = opts[1];
        var showLang = opts[2];
        var checkMenu = opts[3];
        var checkLang = opts[4];
        var path = opts[5];
        $("input#editId").val(catId);
        if (showMenu == 'false')
            $("div#showMenu").hide();
        else {
            $("div#showMenu").show();
            if (checkMenu == 'true')
                $("#SeparateBlock").attr('checked', true);
            else
                $("#SeparateBlock").attr('checked', false);
        }
        if (showLang == 'false')
            $("div#showLang").hide();
        else {
            $("div#showLang").show();
            if (checkLang == 'true')
                $("#AllCulture").attr('checked', true);
            else
                $("#AllCulture").attr('checked', false);
        }

        var id = 'div.window#editModal';
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
    $("a[id^='CreateCat']").click(function () {
        $("input#catTitle").val($(this).attr('name'));
        $("div#editModal div.modalTC").text($(this).text());
        $("input#catTitle").attr('name', 'CreateCategory');
        $("input#catTitle").removeClass('input-validation-error');
        $('span#Title_Error').removeClass('field-validation-error').addClass('field-validation-valid');
        var opts = $(this).attr('rel').split('_');
        //rel="catId_showMenu_showLang_checkMenu_checkLang"
        var catId = opts[0];
        var showMenu = opts[1];
        var showLang = opts[2];
        var checkMenu = opts[3];
        var checkLang = opts[4];
        var path = opts[5];
        $("input#editId").val(catId);
        if (showMenu == 'false')
            $("div#showMenu").hide();
        else {
            $("div#showMenu").show();
            if (checkMenu == 'true')
                $("#SeparateBlock").attr('checked', true);
            else
                $("#SeparateBlock").attr('checked', false);
        }
        if (showLang == 'false')
            $("div#showLang").hide();
        else {
            $("div#showLang").show();
            if (checkLang == 'true')
                $("#AllCulture").attr('checked', true);
            else
                $("#AllCulture").attr('checked', false);
        }

        var id = 'div.window#editModal';
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
    $("input#edit").live("click", function () {
        $.ajax({
            type: 'POST',
            url: '/Article/Category',
            data: 'id=' + $("input#editId").val() + '&path=' + $("input#catTitle").attr('name') + '&title=' + $("input#catTitle").val() + '&menu=' + $("#SeparateBlock").attr('checked')
            + '&allculture=' + $("#AllCulture").attr('checked'),
            success: function (data) {
                if (data) {
                    var err = $('span#Title_Error');
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