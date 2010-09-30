$(function () {
    var hideDelay = 500;
    var id;
    var hideTimer = null;
    var container = $('<div id="mtPopupContainer">'
      + '<table width="" border="0" cellspacing="0" cellpadding="0" align="center" class="mtPopupPopup">'
      + '<tr>'
      + '   <td class="corner topLeft"></td>'
      + '   <td class="top"></td>'
      + '   <td class="corner topRight"></td>'
      + '</tr>'
      + '<tr>'
      + '   <td class="left">&nbsp;</td>'
      + '   <td><div id="mtPopupContent"></div></td>'
      + '   <td class="right">&nbsp;</td>'
      + '</tr>'
      + '<tr>'
      + '   <td class="corner bottomLeft">&nbsp;</td>'
      + '   <td class="bottom">&nbsp;</td>'
      + '   <td class="corner bottomRight"></td>'
      + '</tr>'
      + '</table>'
      + '</div>');
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
            type: 'GET',
            url: '/Article/Tooltip',
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