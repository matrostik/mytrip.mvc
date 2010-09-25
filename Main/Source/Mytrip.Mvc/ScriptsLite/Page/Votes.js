$(document).ready(function () {
    var openid = new Object();
    var st = $('.dropdown dt a').find('.value').html();
    $('.dropdown dd ul li a').click(function () {
        var selected = $(this).find('.value').html();
        if (st != selected) {
            $("form").submit();
        }
    });
});
$(function () {
    $('#CloseDate').datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'yy-mm-dd'
    });
});