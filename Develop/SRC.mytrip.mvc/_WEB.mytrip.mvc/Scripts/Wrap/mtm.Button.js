﻿$(document).ready(function () {
    $.ajax({ type: "POST", url: "/mtm/Browser",
        success: function (data) {
            $('div.buttonIWL,div.buttonIWR').each(function () {
                var a = $(this).html();
                if (data == 'IE_7.0'||data == 'IE_6.0')
                    var b = $(this).children('input').width();
                $(this).html('<div class="buttonRW"/><div class="buttonLW"/><div class="buttonCW">' + a + '</div>');
                if (data == 'IE_7.0'||data == 'IE_6.0') {
                    var c = $('div.buttonLW').width();
                    var d = $('div.buttonRW').width();
                    if (b > 0)
                        $(this).css({ width: ((b + c + d) + 'px') });
                }
            });
            $('div.buttonIL,div.buttonIR').each(function () {
                var a = $(this).html();
                if (data == 'IE_7.0'||data == 'IE_6.0')
                    var b = $(this).children('input').width();
                $(this).html('<div class="buttonR"/><div class="buttonL"/><div class="buttonC">' + a + '</div>');
                if (data == 'IE_7.0'||data == 'IE_6.0') {
                    var c = $('div.buttonL').width();
                    var d = $('div.buttonR').width();
                    if (b > 0)
                        $(this).css({ width: ((b + c + d) + 'px') });
                }
            });
            $('div.buttonAWL,div.buttonAWR').each(function () {
                var a = $(this).html();
                if (data == 'IE_7.0'||data == 'IE_6.0')
                    var b = $(this).children('a').width();
                $(this).html('<div class="buttonRW"/><div class="buttonLW"/><div class="buttonCW">' + a + '</div>');
                if (data == 'IE_7.0'||data == 'IE_6.0') {
                    var c = $('div.buttonLW').width();
                    var d = $('div.buttonRW').width();
                    if (b > 0)
                        $(this).css({ width: ((b + c + d) + 'px') });
                }
            });
            $('div.buttonAL,div.buttonAR').each(function () {
                var a = $(this).html();
                if (data == 'IE_7.0'||data == 'IE_6.0')
                    var b = $(this).children('a').width();
                $(this).html('<div class="buttonR"/><div class="buttonL"/><div class="buttonC">' + a + '</div>');
                if (data == 'IE_7.0'||data == 'IE_6.0') {
                    var c = $('div.buttonL').width();
                    var d = $('div.buttonR').width();
                    if (b > 0)
                        $(this).css({ width: ((b + c + d) + 'px') });
                }
            });
            $('div.tabI').each(function () {
                var a = $(this).html();
                if (data == 'IE_7.0'||data == 'IE_6.0')
                    var b = $(this).children('input').width();
                    $(this).html('<div class="tabR"/><div class="tabL"/><div class="tabC">' + a + '</div>');                
                if (data == 'IE_7.0'||data == 'IE_6.0') {
                    var c = $('div.tabL').width();
                    var d = $('div.tabR').width();
                    if (b > 0)
                        $(this).css({ width: ((b + c + d) + 'px') });
                }
            });
            $('div.pagerAWL,div.pagerAWR').each(function () {
                var a = $(this).html();
                if (data == 'IE_7.0' || data == 'IE_6.0')
                    var b = $(this).children('a').width();
                $(this).html('<div class="pagerRW"/><div class="pagerLW"/><div class="pagerCW">' + a + '</div>');
                if (data == 'IE_7.0' || data == 'IE_6.0') {
                    var c = $('div.pagerLW').width();
                    var d = $('div.pagerRW').width();
                    if (b > 0)
                        $(this).css({ width: ((b + c + d) + 'px') });
                }
            });
            $('div.pagerAL,div.pagerAR').each(function () {
                var a = $(this).html();
                if (data == 'IE_7.0' || data == 'IE_6.0')
                    var b = $(this).children('a').width();
                $(this).html('<div class="pagerR"/><div class="pagerL"/><div class="pagerC">' + a + '</div>');
                if (data == 'IE_7.0' || data == 'IE_6.0') {
                    var c = $('div.pagerL').width();
                    var d = $('div.pagerR').width();
                    if (b > 0)
                        $(this).css({ width: ((b + c + d) + 'px') });
                }
            });
            $('div.tabI:first').find("div.tabR, div.tabL, div.tabC").addClass("ac");

        }
    });
    $('div.buttonIL,div.buttonIR,div.buttonAL,div.buttonAR').hover(function () {
        $(this).find('div.buttonC,div.buttonL,div.buttonR').addClass("ac");
    }, function ()
    { $(this).find('div.buttonC,div.buttonL,div.buttonR').removeClass("ac"); });
    $('div.buttonIWL,div.buttonIWR,div.buttonAWL,div.buttonAWR').hover(function () {
        $(this).find('div.buttonCW,div.buttonLW,div.buttonRW').addClass("ac");
    }, function () {
        $(this).find('div.buttonCW,div.buttonLW,div.buttonRW').removeClass("ac");
    });
    $('div.pagerAL,div.pagerAR').hover(function () {
        $(this).find('div.pagerC,div.pagerL,div.pagerR').addClass("ac");
    }, function ()
    { $(this).find('div.pagerC,div.pagerL,div.pagerR').removeClass("ac"); });
    $('div.pagerAWL,div.pagerAWR').hover(function () {
        $(this).find('div.pagerCW,div.pagerLW,div.pagerRW').addClass("ac");
    }, function () {
        $(this).find('div.pagerCW,div.pagerLW,div.pagerRW').removeClass("ac");
    });
});
/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich To learn more about mytrip.mvc visit http://mytripmvc.net  http://mytripmvc.codeplex.com mytripmvc@gmail.com license: Microsoft Public License (Ms-PL)*/