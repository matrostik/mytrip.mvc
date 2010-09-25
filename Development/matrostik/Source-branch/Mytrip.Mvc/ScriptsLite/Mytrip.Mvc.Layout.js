/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich To learn more about Mytrip.Mvc visit http://mytripmvc.net  http://mytripmvc.codeplex.com mytripmvc@gmail.com license: Microsoft Public License (Ms-PL)*/
$(document).ready(function () {
    $('div.logonUL').each(function () {
        $(this).hide();
        $(this).css('top', 24);
    });
    $('div.logonI').hover(function () {
        $(this).find('div.logonC,div.logonCD').addClass("ac");
        $(this).find('div.logonUL').stop(true, true);
        $(this).find('div.logonUL').slideDown();
    },
function () {
    $(this).find('div.logonC,div.logonCD').removeClass("ac");
    $(this).find('div.logonUL').delay(80).slideUp();
});
    $('div.menuI').hover(function () {
        $(this).find('div.menuC,div.menuCD,div.menuCV,div.menuCVD').addClass("ac");
        $(this).find('div.menuUL').stop(true, true);
        $(this).find('div.menuUL').slideDown();
    },
function () {
    $(this).find('div.menuC,div.menuCD,div.menuCV,div.menuCVD').removeClass("ac");
    $(this).find('div.menuUL').delay(80).slideUp();
});
});