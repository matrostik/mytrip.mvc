//************************************************************ 
// Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich
// To learn more about Mytrip.Mvc.Entyty visit 
// http://starterkitmytripmvc.codeplex.com/
// mytripmvc@gmail.com
// license: Microsoft Public License (Ms-PL) 
// ***********************************************************
$(document).ready(function () {
    $('.topmenudisplay ul li').hover(
        function () {
            $(this).addClass("active");
            $(this).find('ul').stop(true, true);
            $(this).find('ul').slideDown();
        },
        function () {
            $(this).removeClass("active");
            $(this).find('ul').slideUp('fast');
        }
    );

    $('.menucontainer ul li').hover(
        function () {
            $(this).addClass("active");
            $(this).find('ul').stop(true, true);
            $(this).find('ul').slideDown();
        },
        function () {
            $(this).removeClass("active");
            $(this).find('ul').slideUp('fast');
        }
    );


    $(".accordion div.accordiontitle").eq(0).addClass("active");
    $(".accordion div.accordioncontent").eq(0).show();

    $(".accordion div.accordiontitle").click(function () {
        $(this).toggleClass("active");
        $(this).next('div.accordioncontent').eq(0).slideToggle(300);
    });
    $(".accordion2 div.accordiontitle2").eq(0).addClass("active");
    $(".accordion2 div.accordioncontent2").eq(0).show();

    $(".accordion2 div.accordiontitle2").click(function () {
        $(this).toggleClass("active");
        $(this).next('div.accordioncontent2').eq(0).slideToggle(300);
    });
});