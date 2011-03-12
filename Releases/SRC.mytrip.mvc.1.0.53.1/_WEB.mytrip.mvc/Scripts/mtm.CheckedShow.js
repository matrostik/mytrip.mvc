/*   
Функция по показу/скрытыю div-ов при нажатии на чекбокс.
Id чекбокса - "имя дива_опция" (через "_")
имя дива - значение аттрибута 'name'  дива для скрытия/показа
опция - hideOffDiv или hideOnDiv (прятать если выключено или включено)
*/
$(document).ready(function () {
    $("input:checkbox[id$='_hideOnDiv'],input:checkbox[id$='_hideOffDiv']").each(function () {
        var opts = $(this).attr('id').split('_');
        if (opts[1] == 'hideOnDiv') {
            if (!$(this).is(':checked'))
                $("div[name=" + opts[0] + "]").show();
            $(this).bind('click', function () {
                if (!$(this).is(':checked'))
                    $("div[name=" + opts[0] + "]").show('slow');
                else
                    $("div[name=" + opts[0] + "]").hide('slow');
            });
        }
        else {
            if ($(this).is(':checked'))
                $("div[name=" + opts[0] + "]").show();
            $(this).bind('click', function () {
                if ($(this).is(':checked'))
                    $("div[name=" + opts[0] + "]").show('slow');
                else
                    $("div[name=" + opts[0] + "]").hide('slow');
            });
        }

    });
});