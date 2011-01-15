$(document).ready(function () {
    $.ajax({ type: "POST", url: "/Store/ProductCart",
        success: function (data) {
            $("#appr").html(data);
        }
    });
    $("input.comparision").live('click', function () {

        var a = "id=" + $(this).attr('value');
        $.ajax({ type: "POST", url: "/Store/ProductForComparison", data: a });

    });
    //$("input.cart").bind('click', function () {
    $("input.cart").click(function (e) {
        //alert('input.cart');
        var a = "id=" + $(this).attr('value');
        $.ajax({ type: "POST", url: "/Store/ProductForCart", data: a,
            success: function (data) {
                $("#appr").html(data);
            }
        });
    });
});
  