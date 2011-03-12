$(document).ready(function () {
    $('input[type=file]').change(function () {
        $('#filetext').attr('value', $('input[type=file]').val());
        $('.textcontent').removeClass("ac");
        $('.textleft').removeClass("ac");
        $('.textright').removeClass("ac");
        $('#divfiletext .textbox').toggleClass("ac");
        $('#divfiletext .textbox').find('.textcontent').addClass("ac");
        $('#divfiletext .textbox').find('.textleft').addClass("ac");
        $('#divfiletext .textbox').find('.textright').addClass("ac");
    });
    $('input[type=file]').hover(
        function () {
            $('div.file').addClass("ac");
        },
        function () {
            $('div.file').removeClass("ac");
        }
    );

});