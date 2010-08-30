$(document).ready(function () {
    var openid = new Object();
    //$('.dropdown dt a').attr('style', 'width: 30px;margin: 0 0 0 60px;height: 22px;padding:4px 0 0 10px;');
     //$('.dropdown dd ul').attr('style', 'min-width: 35px;margin: 0 0 0 60px;text-align:center;');
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