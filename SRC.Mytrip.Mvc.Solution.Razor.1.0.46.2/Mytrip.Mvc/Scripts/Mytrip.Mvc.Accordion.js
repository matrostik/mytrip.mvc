$(document).ready(function () {
    $('div.accT').each(function () {
        var a = $(this).html();
        $(this).html('<div class="accTR"/><div class="accTL"/><div class="accTC">' + a + '</div>');
    });
    $('div.noaccT').each(function () {
        var a = $(this).html();
        $(this).html('<div class="noaccTR"/><div class="noaccTL"/><div class="noaccTC">' + a + '</div>');
    });
    $('div.accVC').each(function () {
        var a = $(this).html();
        $(this).html('<div class="accC">' + a + '</div><div class="accBR"/><div class="accBL"/><div class="accBC"/>');
    });
    $('div.acc').each(function () {
        $(this).after('<div class="accFR"/>');
    });
    if ($('div.accT').length != 0) {
        $.ajax({ type: "POST", url: "/MytripMvc/AccardionCookies",
            success: function (data) {
                var d = []; d = data.split('|');
                for (i = 0; i < d.length; i++) {
                    $('div.acc div.accT').each(function () {
                        var a = $(this).children('div.accTC').html();
                        var b = $(this).children('div.accTC');
                        var c = $(b).children('a').html();
                        if (a == d[i] || c == d[i]) {
                            $(this).find('div.accTC,div.accTL,div.accTR').addClass('ac');
                            $(this).next('div.accVC').hide();
                        }
                    });
                }
            }
        });
    }
    $('div.acc div.accT').hover(function () {
        $(this).find('div.accTC,div.accTL,div.accTR').filter('.ac').addClass("ho");
        $(this).find('div.accTC:not(.ac),div.accTL:not(.ac),div.accTR:not(.ac)').addClass("hoo");
    }, function () {
        $(this).find('div.accTC,div.accTL,div.accTR').removeClass("ho");
        $(this).find('div.accTC:not(.ac),div.accTL:not(.ac),div.accTR:not(.ac)').removeClass("hoo");
    });
    $("div.acc div.accT").click(function () {
        var b = $(this).children('div.accTC'); var a = null;
        if ($(b).children('a').length == 0)
            a = "id=_" + $(this).children('div.accTC').html() + "_";
        else
            a = "id=_" + $(b).children('a').html() + "_";
        $.ajax({ type: "POST", url: "/MytripMvc/Accardion", data: a });
        $(this).find('div.accTC,div.accTL,div.accTR').removeClass("ho").removeClass("hoo").toggleClass("ac");
        $(this).next('div.accVC').eq(0).slideToggle(300);
    });
});