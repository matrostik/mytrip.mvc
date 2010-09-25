$(document).ready(function () {
    if ($("#integ").attr('checked') == 0) {
        $("#_integ").show();
    }
    if ($("#integ2").attr('checked') != 0) {
        $("#_integ2").show();
    }
    $("#integ").bind('click', function () {
        if ($("#integ").attr('checked') == 0) {
            $("#_integ").show('slow');
        } else {
            $("#_integ").hide('slow');
        }
    });
    $("#integ2").bind('click', function () {
        if ($("#integ2").attr('checked') != 0) {
            $("#_integ2").show('slow');
        } else {
            $("#_integ2").hide('slow');
        }
    });
});