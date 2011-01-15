$(function () {
    var hideDelay = 500;
    var id;
    var hideTimer = null;
    var container = $('<div id="mtPopupContainer"><div class="mtPopupTL"/><div class="mtPopupTR"/><div class="mtPopupTC"/><div id="mtPopupContent"></div><div class="mtPopupBL"/><div class="mtPopupBR"/><div class="mtPopupBC"/></div>');
    $('body').append(container);
    $('.mtPopupArticles').live('mouseover', function () {
        var settings = $(this).attr('rel').split(',');
        var type = settings[0];
        id = settings[1];        
        if (id == '')
            return;
        if (hideTimer)
            clearTimeout(hideTimer);

        var pos = $(this).offset();
        var width = $(this).width();
        container.css({
            left: (pos.left + width) + 'px',
            top: pos.top - 5 + 'px'
        });
        $('#mtPopupContent').html('&nbsp;');
        $.ajax({
            type: 'Get',
            url: '/Article/Tooltip',
            cache: false,
            data: 'id=' + type + '&id1=' + id,
            success: function (data) {
                $('#mtPopupContent').html(data);
            }
        });
        container.css('display', 'block');
    });

    $('.mtPopupArticles').live('mouseout', function () {
        if (hideTimer)
            clearTimeout(hideTimer);
        hideTimer = setTimeout(function () {
            container.css('display', 'none');
        }, hideDelay);
    });

    // Allow mouse over of details without hiding details  
    $('#mtPopupContainer').mouseover(function () {
        if (hideTimer)
            clearTimeout(hideTimer);
    });

    // Hide after mouseout  
    $('#mtPopupContainer').mouseout(function () {
        if (hideTimer)
            clearTimeout(hideTimer);
        hideTimer = setTimeout(function () {
            container.css('display', 'none');
        }, hideDelay);
    });
});  