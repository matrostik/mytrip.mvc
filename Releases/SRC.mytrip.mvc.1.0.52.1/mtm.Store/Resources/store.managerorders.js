$(document).ready(function () {
    $("input#orders0").live('click', function () {
        $('div.tabI').each(function () {
            $(this).find("div.tabR, div.tabL, div.tabC").removeClass("ac");
        });
        var a = $(this).parent("div.tabC");
        var b = $(a).parent("div.tabI");
        $(b).find("div.tabR, div.tabL, div.tabC").addClass("ac");
        $("div#_orders1").hide();
        $("div#_orders2").hide();
        $("div#_orders3").hide();
        $("div#_orders0").show();

    });
    $("input#orders1").live('click', function () {
        $('div.tabI').each(function () {
            $(this).find("div.tabR, div.tabL, div.tabC").removeClass("ac");
        });
        var a = $(this).parent("div.tabC");
        var b = $(a).parent("div.tabI");
        $(b).find("div.tabR, div.tabL, div.tabC").addClass("ac");
        $("div#_orders0").hide();
        $("div#_orders2").hide();
        $("div#_orders3").hide();
        $("div#_orders1").show();

    });
    $("input#orders2").live('click', function () {
        $('div.tabI').each(function () {
            $(this).find("div.tabR, div.tabL, div.tabC").removeClass("ac");
        });
        var a = $(this).parent("div.tabC");
        var b = $(a).parent("div.tabI");
        $(b).find("div.tabR, div.tabL, div.tabC").addClass("ac");
        $("div#_orders0").hide();
        $("div#_orders1").hide();
        $("div#_orders3").hide();
        $("div#_orders2").show();

    });
    $("input#orders3").live('click', function () {
        $('div.tabI').each(function () {
            $(this).find("div.tabR, div.tabL, div.tabC").removeClass("ac");
        });
        var a = $(this).parent("div.tabC");
        var b = $(a).parent("div.tabI");
        $(b).find("div.tabR, div.tabL, div.tabC").addClass("ac");
        $("div#_orders0").hide();
        $("div#_orders1").hide();
        $("div#_orders2").hide();
        $("div#_orders3").show();

    });


});