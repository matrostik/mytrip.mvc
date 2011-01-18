$(document).ready(function () {
    //checkboxes   .css({"z-index":"-9999","position":"relative"})
    $("input:checkbox").each(function () {
        var id = $(this).attr('id');
        var cbx = $(this).css({ "z-index": "-9999", "position": "relative" });

        if (cbx.is(':checked'))
            $(cbx).wrap('<span id="mtmcbx' + id + '" class="checkboxcheck" />');
        else
            $(cbx).wrap('<span id="mtmcbx' + id + '" class="checkbox" />');
    });
    $("span.checkbox").live("click", function () {
        //alert('span.checkbox');
        if ($(this).find('input:checkbox').is(':disabled'))
            return false;
        var id = $(this).find('input:checkbox').attr('id');
        $("input[id=" + id + "]").attr('checked', true);
        $("input[id=" + id + "]").triggerHandler('click');
        $(this).removeClass('checkbox').addClass('checkboxcheck');
        //fixCheckbox();
    });
    $("span.checkboxcheck").live("click", function () {
        //alert('span.checkboxcheck');
        if ($(this).find('input:checkbox').is(':disabled'))
            return false;
        var id = $(this).find('input:checkbox').attr('id');
        $("input[id=" + id + "]").attr('checked', false);
        $("input[id=" + id + "]").triggerHandler('click');
        $(this).removeClass('checkboxcheck').addClass('checkbox');
        //fixCheckbox();
    });
    $('input:checkbox').click(function (e) {
        //alert('input:checkbox click');
        e.stopPropagation();
        fixCheckbox();
    });
    //
});
function fixCheckbox() {
    $("span.checkboxcheck,span.checkbox").each(function () {
        var checked = $(this).find('input:checkbox').is(':checked');
        if (checked == true && $(this).hasClass('checkboxcheck') == false) {
            $(this).removeClass('checkbox').addClass('checkboxcheck');
        }
        else if (checked == false && $(this).hasClass('checkbox') == false) {
            $(this).removeClass('checkboxcheck').addClass('checkbox');
        }
    });
}