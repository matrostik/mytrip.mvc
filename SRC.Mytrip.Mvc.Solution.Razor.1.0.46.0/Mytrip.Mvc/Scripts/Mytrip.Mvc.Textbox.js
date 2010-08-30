$(document).ready(function () {
    $('input[type=text],input[type=password]').wrap('<div class="textbox"/>');
    $('textarea.message').wrap('<div class="textarea"/>');
            $('div.textbox').each(function () {
                var a = $(this).html();
                    var b = $(this).children('input').width();
                $(this).html('<div class="textR"/><div class="textL"/><div class="textC">' + a + '</div>');
                    var c = $('div.textR').width();
                    var d = $('div.textL').width();
                    if (b != 0) {
                        $(this).css({ width: ((b + c + d) + 'px') });
                }
            });


            $('div.textarea').each(function () {
                var a = $(this).html();
                    var b = $(this).children('textarea').width();
                $(this).html('<div class="textareaTR"/><div class="textareaTL"/><div class="textareaTC"/><div class="textareaC">' + a + '</div><div class="textareaBR"/><div class="textareaBL"/><div class="textareaBC"/>');
              
                    var c = $('div.textareaBR').width();
                    var d = $('div.textareaBL').width();
                    if (b != 0) {
                        $(this).css({ width: ((b + c + d) + 'px') }); 
                 
                }
            });


    if ($('input[type=text]').length == 0 && !$('input[type=password]:first').is('.input-validation-error')) {
        $('input[type=password]:first').focus();
        var a = $('input[type=password]:first').parent('div.textC');
        var b = $(a).parent('div.textbox');
        $(b).find('div.textC,div.textL,div.textR').addClass("ac");
    }
    if ($('input[type=text]').length != 0 && !$('input[type=text]:first').is('.input-validation-error')) {
        $('input[type=text]:first').focus();
        var a = $('input[type=text]:first').parent('div.textC');
        var b = $(a).parent('div.textbox');
        $(b).find('div.textC,div.textL,div.textR').addClass("ac");
    }
    $('.input-validation-error').each(function () {
        var a = $(this).parent('div');
        var b = $(a).parent('div');
        $(b).find('div.textC,div.textL,div.textR,div.textareaTR,div.textareaTL,div.textareaTC,div.textareaBR,div.textareaBL,div.textareaBC,div.textareaC,textarea.message').removeClass("ac"); $(b).find('div.textC,div.textL,div.textR,div.textareaTR,div.textareaTL,div.textareaTC,div.textareaBR,div.textareaBL,div.textareaBC,div.textareaC,textarea.message').addClass("er");
    });
    $("input[type=text],input[type=password],textarea").bind('keypress focus click', function () {
        $('div.textC,div.textL,div.textR,div.textareaTR,div.textareaTL,div.textareaTC,div.textareaBR,div.textareaBL,div.textareaBC,div.textareaC,textarea.message').removeClass("ac"); if (!$(this).is('.input-validation-error')) {
            var a = $(this).parent('div');
            var b = $(a).parent('div');
            $(b).find('div.textC,div.textL,div.textR,div.textareaTR,div.textareaTL,div.textareaTC,div.textareaBR,div.textareaBL,div.textareaBC,div.textareaC,textarea.message').addClass("ac");
        }
        $("input[type=text],input[type=password],textarea").each(function () {
            if (!$(this).is('.input-validation-error')) {
                var a = $(this).parent('div');
                var b = $(a).parent('div');
                $(b).find('div.textC,div.textL,div.textR,div.textareaTR,div.textareaTL,div.textareaTC,div.textareaBR,div.textareaBL,div.textareaBC,div.textareaC,textarea.message').removeClass("er");
            }
        });
        setTimeout(function () {
            $('.input-validation-error').each(function () {
                var a = $(this).parent('div');
                var b = $(a).parent('div');
                $(this).prev('div.dropdown').addClass("er");
                $(b).find('div.textC,div.textL,div.textR,div.textareaTR,div.textareaTL,div.textareaTC,div.textareaBR,div.textareaBL,div.textareaBC,div.textareaC,textarea.message').removeClass("ac");
                $(b).find('div.textC,div.textL,div.textR,div.textareaTR,div.textareaTL,div.textareaTC,div.textareaBR,div.textareaBL,div.textareaBC,div.textareaC,textarea.message').addClass("er");
            });
        }, 10);
    });
    $('input[type=submit]').click(function () { setTimeout(function () { $('.input-validation-error').each(function () { var a = $(this).parent('div'); var b = $(a).parent('div'); $(this).prev('div.dropdown').addClass("er"); $(b).find('div.textC,div.textL,div.textR,div.textareaTR,div.textareaTL,div.textareaTC,div.textareaBR,div.textareaBL,div.textareaBC,div.textareaC,textarea.message').removeClass("ac"); $(b).find('div.textC,div.textL,div.textR,div.textareaTR,div.textareaTL,div.textareaTC,div.textareaBR,div.textareaBL,div.textareaBC,div.textareaC,textarea.message').addClass("er"); }); }, 10); });
});
/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich To learn more about Mytrip.Mvc visit http://mytripmvc.net  http://mytripmvc.codeplex.com mytripmvc@gmail.com license: Microsoft Public License (Ms-PL)*/