/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich To learn more about Mytrip.Mvc visit http://mytripmvc.net  http://mytripmvc.codeplex.com mytripmvc@gmail.com license: Microsoft Public License (Ms-PL)*/
$(document).ready(function () {
    /*$.ajax({ type: "POST", url: "/AjaxPartial/PartialMenu",
    success: function (data) {
    $('#menucontainer').html(data);           
    }
    });*/
    /*Layout Content*/
    var a = $('#header').html();
    var b = $('#main').html();
    var c = $('#footer').html();
    $('#header').html('<div class="headerTR"/><div class="headerTL"/><div class="headerTC"/><div class="headerC">' + a + '</div><div class="headerBR"/><div class="headerBL"/><div class="headerBC"/>');
    $('#main').html('<div class="mainTR"/><div class="mainTL"/><div class="mainTC"/><div class="mainC">' + b + '</div><div class="mainBR"/><div class="mainBL"/><div class="mainBC"/>');
    $('#footer').html('<div class="footerTR"/><div class="footerTL"/><div class="footerTC"/><div class="footerC">' + c + '</div><div class="footerBR"/><div class="footerBL"/><div class="footerBC"/>');

    $.ajax({ type: "POST", url: "/MytripMvc/Browser",
        success: function (data) {
            /*Logon*/
            $('div.logonI').each(function () {
                var d = $(this).children('div');
                if (data == 'IE_7.0')
                    var e = $(d).children('a').width();
                var f = $(this).html();
                if ($(this).children('div.logonC').length != 0) {
                    $(this).html('<div class="logonR"/><div class="logonL"/>' + f);
                    if (data == 'IE_7.0') {
                        var g = $('div.logonR').width();
                        var h = $('div.logonL').width();
                        $(this).css({ width: ((e + g + h) + 'px') });
                    }
                }
                else if ($(this).children('div.logonCD').length != 0) {
                    $(this).html('<div class="logonRD"/><div class="logonL"/>' + f);
                    if (data == 'IE_7.0') {
                        var g = $('div.logonRD').width();
                        var h = $('div.logonL').width();
                        $(this).css({ width: ((e + g + h) + 'px') });
                    }
                }
                else if (
$(this).children('div.logonCV').length != 0) {
                    $(this).html('<div class="logonRV"/><div class="logonLV"/>' + f);
                    if (data == 'IE_7.0') {
                        var g = $('div.logonRV').width();
                        var h = $('div.logonLV').width();
                        $(this).css({ width: ((e + g + h) + 'px') });
                    }
                }
                else if ($(this).children('div.logonCVD').length != 0) {
                    $(this).html('<div class="logonRVD"/><div class="logonLV"/>' + f);
                    if (data == 'IE_7.0') {
                        var g = $('div.logonRVD').width();
                        var h = $('div.logonLV').width();
                        $(this).css({ width: ((e + g + h) + 'px') });
                    }
                } else if ($(this).children('div.logonCW').length != 0) {
                    $(this).html('<div class="logonRW"/><div class="logonLW"/>' + f);
                    if (data == 'IE_7.0') {
                        var g = $('div.logonRW').width();
                        var h = $('div.logonLW').width();
                        $(this).css({ width: ((e + g + h) + 'px') });
                    }
                } else if ($(this).children('div.logonCWD').length != 0) {
                    $(this).html('<div class="logonRWD"/><div class="logonLW"/>' + f);
                    if (data == 'IE_7.0') {
                        var g = $('div.logonRWD').width();
                        var h = $('div.logonLW').width();
                        $(this).css({ width: ((e + g + h) + 'px') });
                    }
                }
            });
            $('div.logonUL').each(function () {

                var d = $(this).html();
                if (data == 'IE_7.0') {
                    var e = $(this).parent('div.logonI').width();
                    var f = 0;
                    $(this).find('a').each(function () {
                        var g = $(this).width();
                        if (e < g)
                            f = g;
                    });
                    if (e <= f)
                        e = f + 12;
                    $(this).css({ width: (e + 'px') });
                }
                $(this).hide();
                $(this).css('top', 24);
                $(this).html('<div class="logonSTL"/><div class="logonSTR"/><div class="logonSTC"/><div class="logonSC">' + d + '</div><div class="logonSBL"/><div class="logonSBR"/><div class="logonSBC"/>');
            });

            /*Menu*/
            $('div.menuI').each(function () {
                if (data == 'IE_7.0') {
                    var d = $(this).children('div');
                    var f = $(d).children('a').html();
                    var g = f.replace(' ', '_');
                    $(d).children('a').html(g);
                    var e = $(d).children('a').width();
                    $(d).children('a').html(f);
                }
                var h = $(this).html();
                if ($(this).children('div.menuC').length != 0) {
                    $(this).html('<div class="menuR"/><div class="menuL"/>' + h);
                    if (data == 'IE_7.0') {
                        var j = $('div.menuR').width();
                        var r = $('div.menuL').width();
                        $(this).css({ width: ((e + j + r) + 'px') });
                    }
                }
                else if ($(this).children('div.menuCD').length != 0) {
                    $(this).html('<div class="menuRD"/><div class="menuL"/>' + h);
                    if (data == 'IE_7.0') {
                        var j = $('div.menuRD').width();
                        var r = $('div.menuL').width();
                        $(this).css({ width: ((e + j + r) + 'px') });
                    }
                }
                else if ($(this).children('div.menuCV').length != 0) {
                    $(this).html('<div class="menuRV"/><div class="menuLV"/>' + h);
                    if (data == 'IE_7.0') {
                        var j = $('div.menuRV').width();
                        var r = $('div.menuLV').width();
                        $(this).css({ width: ((e + j + r) + 'px') });
                    }
                }
                else if ($(this).children('div.menuCVD').length != 0) {
                    $(this).html('<div class="menuRVD"/><div class="menuLV"/>' + h);
                    if (data == 'IE_7.0') {
                        var j = $('div.menuRVD').width();
                        var r = $('div.menuLV').width();
                        $(this).css({ width: ((e + j + r) + 'px') });
                    }
                }
                else if ($(this).children('div.menuCW').length != 0) {
                    $(this).html('<div class="menuRW"/><div class="menuLW"/>' + h);
                    if (data == 'IE_7.0') {
                        var j = $('div.menuRW').width();
                        var r = $('div.menuLW').width();
                        $(this).css({ width: ((e + j + r) + 'px') });
                    }
                }
                else if ($(this).children('div.menuCWD').length != 0) {
                    $(this).html('<div class="menuRWD"/><div class="menuLW"/>' + h);
                    if (data == 'IE_7.0') {
                        var j = $('div.menuRWD').width();
                        var r = $('div.menuLW').width();
                        $(this).css({ width: ((e + j + r) + 'px') });
                    }
                }
            });
            $('div.menuUL').each(function () {
                var ulmenu = $(this).html();
                if (data == 'IE_7.0') 
                {
                    var e = $(this).parent('div.menuI').width();
                    var h = 0;
                    $(this).find('a').each(function () {
                        var f = $(this).html();
                        var g = f.replace(' ', '_');
                        $(this).html(g);
                        var g = $(this).width();
                        $(this).html(f);
                        if (e < g)
                            h = g;
                    });
                    if (e <= h)
                        e = h + 12;
                    $(this).css({ width: (e + 'px') });
                }
                $(this).hide();
                $(this).css('top', 25);
                $(this).html('<div class="menuSTL"/><div class="menuSTR"/><div class="menuSTC"/><div class="menuSC">' + ulmenu + '</div><div class="menuSBL"/><div class="menuSBR"/><div class="menuSBC"/>');
            });
        }
    });
    /////////////////////////////////////////////
    $('div.logonI').hover(function () {
        $(this).find('div.logonC,div.logonCD,div.logonL,div.logonR,div.logonRD,div.logonCW,div.logonCWD,div.logonLW,div.logonRW,div.logonRWD').addClass("ac");
        $(this).find('div.logonUL').stop(true, true);
        $(this).find('div.logonUL').slideDown();
    },
function () {
    $(this).find('div.logonC,div.logonCD,div.logonL,div.logonR,div.logonRD,div.logonCW,div.logonCWD,div.logonLW,div.logonRW,div.logonRWD').removeClass("ac");
    $(this).find('div.logonUL').delay(80).slideUp();
});

    $('div.menuI').hover(function () {
        $(this).find('div.menuC,div.menuCD,div.menuL,div.menuR,div.menuRD,div.menuCV,div.menuCVD,div.menuLV,div.menuRV,div.menuRVD').addClass("ac");
        $(this).find('div.menuUL').stop(true, true);
        $(this).find('div.menuUL').slideDown();
    },
function () {
    $(this).find('div.menuC,div.menuCD,div.menuL,div.menuR,div.menuRD,div.menuCV,div.menuCVD,div.menuLV,div.menuRV,div.menuRVD').removeClass("ac");
    $(this).find('div.menuUL').delay(80).slideUp();
});
    /*Content*/
    $('div.content').each(function () {
        var d = $(this).html();
        $(this).html('<div class="contentTR"/><div class="contentTL"/><div class="contentTC"/><div class="contentC">' + d + '</div><div class="contentBR"/><div class="contentBL"/><div class="contentBC"/>');
    });
    $('fieldset').each(function () {
        var d = $(this).html();
        $(this).html('<div class="contentTR"/><div class="contentTL"/><div class="contentTC"/><div class="contentC">' + d + '</div><div class="contentBR"/><div class="contentBL"/><div class="contentBC"/>');
    });
    $('td.first1,td.last1').each(function () {
        $(this).html('<div class="contentTR"/><div class="contentTL"/><div class="contentTC"/>');
    });
    $('td.first3,td.last3').each(function () {
        $(this).html('<div class="contentBR"/><div class="contentBL"/><div class="contentBC"/>');
    });
    $('div.withe').each(function () {
        var d = $(this).html();
        $(this).html('<div class="witheTR"/><div class="witheTL"/><div class="witheTC"/><div class="witheC">' + d + '</div><div class="witheBR"/><div class="witheBL"/><div class="witheBC"/>');
    });
    $('div.dark').each(function () {
        var d = $(this).html();
        $(this).html('<div class="darkTR"/><div class="darkTL"/><div class="darkTC"/><div class="darkC">' + d + '</div><div class="darkBR"/><div class="darkBL"/><div class="darkBC"/>');
    });
    $('div.comment').each(function () {
        var d = $(this).html();
        $(this).html('<div class="commentTR"/><div class="commentTL"/><div class="commentTC"/><div class="commentC">' + d + '</div><div class="commentBR"/><div class="commentBL"/><div class="commentBC"/>');
    });
    $('div.noappr').each(function () {
        var d = $(this).html();
        $(this).html('<div class="noapprTR"/><div class="noapprTL"/><div class="noapprTC"/><div class="noapprC">' + d + '</div><div class="noapprBR"/><div class="noapprBL"/><div class="noapprBC"/>');
    });
});