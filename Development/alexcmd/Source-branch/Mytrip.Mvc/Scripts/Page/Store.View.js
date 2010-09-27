$(document).ready(function () {
    $.ajax({ type: "POST", url: "/Store/ProductCart",
        success: function (data) {
            $("#appr").html(data);
        }
    });
    Rating();
    $("input.comparision").bind('click', function () {

        var a = "id=" + $(this).attr('value');
        $.ajax({ type: "POST", url: "/Store/ProductForComparison", data: a });

    });
    $("input.cart").bind('click', function () {

        var a = "id=" + $(this).attr('value');
        $.ajax({ type: "POST", url: "/Store/ProductForCart", data: a,
            success: function (data) {
                $("#appr").html(data);
            }
        });
    });
    $("input#options").live('click', function () {
        $("div#_foto").hide();
        $("div#_review").hide();
        $("div#_options").show();

    });
    $("input#foto").live('click', function () {
        $("div#_options").hide();
        $("div#_review").hide();
        $("div#_foto").show();

    });
    $("input#review").live('click', function () {
        $("div#_options").hide();
        $("div#_foto").hide();
        $("div#_review").show();

    });
});
function Rating() {
    var sts = new Array;
    $("#votes input[name='vote']").each(function () {
        var style = $(this).attr("style");
        sts.push(style);
    });
    $("#votes input[name='vote']").click(function () {
        if ($("#votes input[name='vote']").attr('onclick') == null) {
            var vote = $(this).val();
            var count = $("#votes input[id='VotesCount']").val();
            var id = $("#Store_StoreId").val();
            $("#votes").load("/Store/Rate", { id: id, vote: vote, count: count });
        }
    });
    $("#votes input[name='vote']").mouseover(function () {
        var rate = $(this).val();
        $("#votes input[name='vote']").each(function () {
            var star = $(this).val();
            if (rate >= star) {
                $(this).removeAttr("style").removeClass("ratingempty").addClass("ratingfull");
            }
            else {
                $(this).removeAttr("style").removeClass("ratingfull").addClass("ratingempty");
            }
        });
    });
    $("#votes").mouseleave(function () {
        var div = $("#votes");
        $(this).removeClass("ratingempty");
        $(this).removeClass("ratingfull")
        $.each(sts,
         function (ctr, val) {
             var id = ctr + 1;
             div.find("#vote" + id).removeClass("ratingempty").removeClass("ratingfull").attr("style", val);
         });
    });
}