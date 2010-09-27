$(document).ready(function () {
    $('img.captcha').wrap('<div class="captcha"/>');
    var dw = $('div.captcha').html();
    $('div.captcha').html('<div class="captchaTR"/><div class="captchaTL"/><div class="captchaTC"/><div class="captchaC">' + dw + '</div><div class="captchaBR"/><div class="captchaBL"/><div class="captchaBC"/>');
});