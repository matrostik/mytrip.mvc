//************************************************************ 
// Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich
// To learn more about Mytrip.Mvc.Entyty visit 
// http://starterkitmytripmvc.codeplex.com/
// mytripmvc@gmail.com
// license: Microsoft Public License (Ms-PL) 
// ***********************************************************
$(document).ready(function () {
    //LOGON MENU
    $('.itemlogon').hover(
        function () {
            $(this).addClass("active");
            $(this).find('.logoncontent').addClass("active");
            $(this).find('.logoncontentdrop').addClass("active");
            $(this).find('.logontopcon').addClass("active");
            $(this).find('.logonbottomcon').addClass("active");
            $(this).find('.logontopleft').addClass("active");
            $(this).find('.logonbottomleft').addClass("active");
            $(this).find('.logontopright').addClass("active");
            $(this).find('.logonbottomright').addClass("active");

            $(this).find('.logoncontentwarning').addClass("active");
            $(this).find('.logoncontentwarningdrop').addClass("active");
            $(this).find('.logontopconwarning').addClass("active");
            $(this).find('.logonbottomconwarning').addClass("active");
            $(this).find('.logontopleftwarning').addClass("active");
            $(this).find('.logonbottomleftwarning').addClass("active");
            $(this).find('.logontoprightwarning').addClass("active");
            $(this).find('.logonbottomrightwarning').addClass("active");

            $(this).find('.logonul').stop(true, true);
            $(this).find('.logonul').slideDown();
        },
        function () {
            $(this).removeClass("active");
            $(this).find('.logoncontent').removeClass("active");
            $(this).find('.logoncontentdrop').removeClass("active");
            $(this).find('.logontopcon').removeClass("active");
            $(this).find('.logonbottomcon').removeClass("active");
            $(this).find('.logontopleft').removeClass("active");
            $(this).find('.logonbottomleft').removeClass("active");
            $(this).find('.logontopright').removeClass("active");
            $(this).find('.logonbottomright').removeClass("active");

            $(this).find('.logoncontentwarning').removeClass("active");
            $(this).find('.logoncontentwarningdrop').removeClass("active");
            $(this).find('.logontopconwarning').removeClass("active");
            $(this).find('.logonbottomconwarning').removeClass("active");
            $(this).find('.logontopleftwarning').removeClass("active");
            $(this).find('.logonbottomleftwarning').removeClass("active");
            $(this).find('.logontoprightwarning').removeClass("active");
            $(this).find('.logonbottomrightwarning').removeClass("active");

            $(this).find('.logonul').slideUp('fast');
        }
    );
    //MENU
    $('.itemmenu').hover(
        function () {
            $(this).addClass("active");
            $(this).find('.menucontent').addClass("active");
            $(this).find('.menucontentdrop').addClass("active");
            $(this).find('.menutopcon').addClass("active");
            $(this).find('.menubottomcon').addClass("active");
            $(this).find('.menutopleft').addClass("active");
            $(this).find('.menubottomleft').addClass("active");
            $(this).find('.menutopright').addClass("active");
            $(this).find('.menubottomright').addClass("active");

            $(this).find('.menucontentvisible').addClass("active");
            $(this).find('.menucontentvisibledrop').addClass("active");
            $(this).find('.menutopconvisible').addClass("active");
            $(this).find('.menubottomconvisible').addClass("active");
            $(this).find('.menutopleftvisible').addClass("active");
            $(this).find('.menubottomleftvisible').addClass("active");
            $(this).find('.menutoprightvisible').addClass("active");
            $(this).find('.menubottomrightvisible').addClass("active");

            $(this).find('.menuul').stop(true, true);
            $(this).find('.menuul').slideDown();
        },
        function () {
            $(this).removeClass("active");
            $(this).find('.menucontent').removeClass("active");
            $(this).find('.menucontentdrop').removeClass("active");
            $(this).find('.menutopcon').removeClass("active");
            $(this).find('.menubottomcon').removeClass("active");
            $(this).find('.menutopleft').removeClass("active");
            $(this).find('.menubottomleft').removeClass("active");
            $(this).find('.menutopright').removeClass("active");
            $(this).find('.menubottomright').removeClass("active");

            $(this).find('.menucontentvisible').removeClass("active");
            $(this).find('.menucontentvisibledrop').removeClass("active");
            $(this).find('.menutopconvisible').removeClass("active");
            $(this).find('.menubottomconvisible').removeClass("active");
            $(this).find('.menutopleftvisible').removeClass("active");
            $(this).find('.menubottomleftvisible').removeClass("active");
            $(this).find('.menutoprightvisible').removeClass("active");
            $(this).find('.menubottomrightvisible').removeClass("active");

            $(this).find('.menuul').slideUp('fast');
        }
    );
    //BUTTON
    $('.itembutton').hover(
        function () {
            $(this).addClass("active");
            $(this).find('.buttoncontent').addClass("active");
            $(this).find('.buttoncontentdrop').addClass("active");
            $(this).find('.buttontopcon').addClass("active");
            $(this).find('.buttonbottomcon').addClass("active");
            $(this).find('.buttontopleft').addClass("active");
            $(this).find('.buttonbottomleft').addClass("active");
            $(this).find('.buttontopright').addClass("active");
            $(this).find('.buttonbottomright').addClass("active");

            $(this).find('.buttoncontentwarning').addClass("active");
            $(this).find('.buttoncontentwarningdrop').addClass("active");
            $(this).find('.buttontopconwarning').addClass("active");
            $(this).find('.buttonbottomconwarning').addClass("active");
            $(this).find('.buttontopleftwarning').addClass("active");
            $(this).find('.buttonbottomleftwarning').addClass("active");
            $(this).find('.buttontoprightwarning').addClass("active");
            $(this).find('.buttonbottomrightwarning').addClass("active");
        },
        function () {
            $(this).removeClass("active");
            $(this).find('.buttoncontent').removeClass("active");
            $(this).find('.buttoncontentdrop').removeClass("active");
            $(this).find('.buttontopcon').removeClass("active");
            $(this).find('.buttonbottomcon').removeClass("active");
            $(this).find('.buttontopleft').removeClass("active");
            $(this).find('.buttonbottomleft').removeClass("active");
            $(this).find('.buttontopright').removeClass("active");
            $(this).find('.buttonbottomright').removeClass("active");

            $(this).find('.buttoncontentwarning').removeClass("active");
            $(this).find('.buttoncontentwarningdrop').removeClass("active");
            $(this).find('.buttontopconwarning').removeClass("active");
            $(this).find('.buttonbottomconwarning').removeClass("active");
            $(this).find('.buttontopleftwarning').removeClass("active");
            $(this).find('.buttonbottomleftwarning').removeClass("active");
            $(this).find('.buttontoprightwarning').removeClass("active");
            $(this).find('.buttonbottomrightwarning').removeClass("active");
        }
    );
    //TEXTBOX
    $(".textbox").click(
        function () {
            $('.textcontent').removeClass("active");
            $('.texttopcon').removeClass("active");
            $('.textbottomcon').removeClass("active");
            $('.texttopleft').removeClass("active");
            $('.textbottomleft').removeClass("active");
            $('.texttopright').removeClass("active");
            $('.textbottomright').removeClass("active");
            $(this).toggleClass("active");
            $(this).find('.textcontent').addClass("active");
            $(this).find('.texttopcon').addClass("active");
            $(this).find('.textbottomcon').addClass("active");
            $(this).find('.texttopleft').addClass("active");
            $(this).find('.textbottomleft').addClass("active");
            $(this).find('.texttopright').addClass("active");
            $(this).find('.textbottomright').addClass("active");
        }
    );
    //ACCORDION
    $('.accordion div.accordiontitle').hover(
        function () {
            $(this).addClass("hover");
            $(this).find('.accordioncontent').filter('.active').addClass("hover");
            $(this).find('.accordiontopcon').filter('.active').addClass("hover");
            $(this).find('.accordionbottomcon').filter('.active').addClass("hover");
            $(this).find('.accordiontopleft').filter('.active').addClass("hover");
            $(this).find('.accordionbottomleft').filter('.active').addClass("hover");
            $(this).find('.accordiontopright').filter('.active').addClass("hover");
            $(this).find('.accordionbottomright').filter('.active').addClass("hover");

            $(this).find('.accordioncontent:not(.active)').addClass("hoveropen");
            $(this).find('.accordiontopcon:not(.active)').addClass("hoveropen");
            $(this).find('.accordionbottomcon:not(.active)').addClass("hoveropen");
            $(this).find('.accordiontopleft:not(.active)').addClass("hoveropen");
            $(this).find('.accordionbottomleft:not(.active)').addClass("hoveropen");
            $(this).find('.accordiontopright:not(.active)').addClass("hoveropen");
            $(this).find('.accordionbottomright:not(.active)').addClass("hoveropen");
        },
        function () {
            $(this).removeClass("hover");
            $(this).find('.accordioncontent').removeClass("hover");
            $(this).find('.accordiontopcon').removeClass("hover");
            $(this).find('.accordionbottomcon').removeClass("hover");
            $(this).find('.accordiontopleft').removeClass("hover");
            $(this).find('.accordionbottomleft').removeClass("hover");
            $(this).find('.accordiontopright').removeClass("hover");
            $(this).find('.accordionbottomright').removeClass("hover");

            $(this).find('.accordioncontent:not(.active)').removeClass("hoveropen");
            $(this).find('.accordiontopcon:not(.active)').removeClass("hoveropen");
            $(this).find('.accordionbottomcon:not(.active)').removeClass("hoveropen");
            $(this).find('.accordiontopleft:not(.active)').removeClass("hoveropen");
            $(this).find('.accordionbottomleft:not(.active)').removeClass("hoveropen");
            $(this).find('.accordiontopright:not(.active)').removeClass("hoveropen");
            $(this).find('.accordionbottomright:not(.active)').removeClass("hoveropen");
        }
    );
    $(".accordion div.accordiontitle").click(function () {
        var a = "id=" + this.id;
        $.ajax({ type: "POST",
            url: "/Accordion/Index",
            data: a
        });
        $(this).toggleClass("active");
        $(this).next('div.accordioncontent').eq(0).slideToggle(300);
    });
    $(".accordion div.accordiontitle").click(function () {
        $(this).toggleClass("active");
        $(this).find('.accordioncontent').removeClass("hover").removeClass("hoveropen").toggleClass("active");
        $(this).find('.accordiontopcon').removeClass("hover").removeClass("hoveropen").toggleClass("active");
        $(this).find('.accordionbottomcon').removeClass("hover").removeClass("hoveropen").toggleClass("active");
        $(this).find('.accordiontopleft').removeClass("hover").removeClass("hoveropen").toggleClass("active");
        $(this).find('.accordionbottomleft').removeClass("hover").removeClass("hoveropen").toggleClass("active");
        $(this).find('.accordiontopright').removeClass("hover").removeClass("hoveropen").toggleClass("active");
        $(this).find('.accordionbottomright').removeClass("hover").removeClass("hoveropen").toggleClass("active");
        $(this).next('div.accordioncontentground').eq(0).slideToggle(300);
    });
    //DROPDOWN
    createDropDown();
    $(".dropdown dt a").click(function () {
        var ddl = $(this).closest("dl");
        ddl.find("dd ul").toggle();
        return false;
    });
    $(document).bind('click', function (e) {
        var $clicked = $(e.target);
        if (!$clicked.parents().hasClass("dropdown"))
            $(".dropdown dd ul").hide();
    });

    $(".dropdown dd ul li a").click(function () {
        var ddl = $(this).closest("dl");
        var text = $(this).html();
        ddl.find("dt a").html(text);
        ddl.find("dd ul").hide();
        var source = $("select");
        source.val($(this).find("span.value").html())
        return false;
    });
});
function createDropDown() {
    var source = $("select");
    $("select").each(function () {
        var source = $(this);
        var id = source.attr('id');
        var selected = source.find("option[selected]");
        var options = $("option", source);

        source.before('<dl id="mtmddl' + id + '" class="dropdown"></dl>')

        $("#mtmddl" + id).append('<dt><a href="#">' + selected.text() +
                '<span class="value">' + selected.val() +
                '</span></a></dt>')
        $("#mtmddl" + id).append('<dd><ul></ul></dd>')

        options.each(function () {
            $("#mtmddl" + id + " dd ul").append('<li><a href="#">' +
                    $(this).text() + '<span class="value">' +
                    $(this).val() + '</span></a></li>');
        });
    });

}