var jHtmlArea_API = new Object();
$(document).ready(function () {
    var openid = new Object();
    $.ajax({ type: "POST",
        url: "/mtm/Theme",
        success: function (data) {
            $('#fotoabstract').htmlarea({
                css: '/Theme/' + data + '/TextAreaContainer.css',
                toolbar: [
            [{
                css: 'image', text: 'Image Gallery',
                action: function (btn) {
                    jHtmlArea_API['#fotoabstract'] = $(this);
                    var url = '/TextAreaFile/Index/()Content()UserFiles/fotoabstract';
                    var gallery = window.open(url, 'gallery', 'width=800,height=600,menubar=0,location=0,resizable=0,scrollbars=1,status=0');
                    gallery.focus();
                }
            }]]
            });
        }
    });
   
});
