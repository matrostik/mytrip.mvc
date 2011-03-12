$(document).ready(function () {
    $('fieldset div.ansv').each(function () {
        if ($(this).find('input.an').attr('value') == 'null') {
            $(this).hide();
        }
    });
    var openid = new Object();
    var st = $('.dropdown dt a').find('.value').html();
    $('.dropdown dd ul li a').click(function () {
        var selected = $(this).find('.value').html();
        //if (st != selected) {            
            $('fieldset div.ansv').each(function () {
                $(this).hide();
            });
            if (selected == '2') {
                if ($('#Answers1').attr('value') == 'null') {
                    $('#Answers1').val('');
                }
                if ($('#Answers2').attr('value') == 'null') {
                    $('#Answers2').val('');
                }
                $('#Answers3').val('null');
                $('#Answers4').val('null');
                $('#Answers5').val('null');
                $('#Answers6').val('null');
                $('#Answers7').val('null');
                $('#Answers8').val('null');
                $('#Answers9').val('null');
                $('#Answers10').val('null');                
                $('fieldset div.ansv').each(function () {
                    if ($(this).find('#Answers1').length != 0 || $(this).find('#Answers2').length != 0) {
                      $(this).show();
                    }
                });
            }
          if (selected == '3') {
              if ($('#Answers1').attr('value') == 'null') {
                  $('#Answers1').val('');
              }
              if ($('#Answers2').attr('value') == 'null') {
                  $('#Answers2').val('');
              }
              if ($('#Answers3').attr('value') == 'null') {
                  $('#Answers3').val('');
              }
              $('#Answers4').val('null');
              $('#Answers5').val('null');
              $('#Answers6').val('null');
              $('#Answers7').val('null');
              $('#Answers8').val('null');
              $('#Answers9').val('null');
              $('#Answers10').val('null');
              $('fieldset div.ansv').each(function () {
                  if ($(this).find('#Answers1').length != 0 || $(this).find('#Answers2').length != 0
                  || $(this).find('#Answers3').length != 0) {
                      $(this).show();
                  }
              });
          }
          if (selected == '4') {
              if ($('#Answers1').attr('value') == 'null') {
                  $('#Answers1').val('');
              }
              if ($('#Answers2').attr('value') == 'null') {
                  $('#Answers2').val('');
              }
              if ($('#Answers3').attr('value') == 'null') {
                  $('#Answers3').val('');
              }
              if ($('#Answers4').attr('value') == 'null') {
                  $('#Answers4').val('');
              }
              $('#Answers5').val('null');
              $('#Answers6').val('null');
              $('#Answers7').val('null');
              $('#Answers8').val('null');
              $('#Answers9').val('null');
              $('#Answers10').val('null');
              $('fieldset div.ansv').each(function () {
                  if ($(this).find('#Answers1').length != 0 || $(this).find('#Answers2').length != 0
                  || $(this).find('#Answers3').length != 0 || $(this).find('#Answers4').length != 0) {
                      $(this).show();
                  }
              });
          }
          if (selected == '5') {
              if ($('#Answers1').attr('value') == 'null') {
                  $('#Answers1').val('');
              }
              if ($('#Answers2').attr('value') == 'null') {
                  $('#Answers2').val('');
              }
              if ($('#Answers3').attr('value') == 'null') {
                  $('#Answers3').val('');
              }
              if ($('#Answers4').attr('value') == 'null') {
                  $('#Answers4').val('');
              }
              if ($('#Answers5').attr('value') == 'null') {
                  $('#Answers5').val('');
              }
              $('#Answers6').val('null');
              $('#Answers7').val('null');
              $('#Answers8').val('null');
              $('#Answers9').val('null');
              $('#Answers10').val('null');
              $('fieldset div.ansv').each(function () {
                  if ($(this).find('#Answers1').length != 0 || $(this).find('#Answers2').length != 0
                  || $(this).find('#Answers3').length != 0 || $(this).find('#Answers4').length != 0
                  || $(this).find('#Answers5').length != 0) {
                      $(this).show();
                  }
              });
          }
          if (selected == '6') {
              if ($('#Answers1').attr('value') == 'null') {
                  $('#Answers1').val('');
              }
              if ($('#Answers2').attr('value') == 'null') {
                  $('#Answers2').val('');
              }
              if ($('#Answers3').attr('value') == 'null') {
                  $('#Answers3').val('');
              }
              if ($('#Answers4').attr('value') == 'null') {
                  $('#Answers4').val('');
              }
              if ($('#Answers5').attr('value') == 'null') {
                  $('#Answers5').val('');
              }
              if ($('#Answers6').attr('value') == 'null') {
                  $('#Answers6').val('');
              }
              $('#Answers7').val('null');
              $('#Answers8').val('null');
              $('#Answers9').val('null');
              $('#Answers10').val('null');
              $('fieldset div.ansv').each(function () {
                  if ($(this).find('#Answers1').length != 0 || $(this).find('#Answers2').length != 0
                  || $(this).find('#Answers3').length != 0 || $(this).find('#Answers4').length != 0
                  || $(this).find('#Answers5').length != 0 || $(this).find('#Answers6').length != 0) {
                      $(this).show();
                  }
              });
          }
          if (selected == '7') {
              if ($('#Answers1').attr('value') == 'null') {
                  $('#Answers1').val('');
              }
              if ($('#Answers2').attr('value') == 'null') {
                  $('#Answers2').val('');
              }
              if ($('#Answers3').attr('value') == 'null') {
                  $('#Answers3').val('');
              }
              if ($('#Answers4').attr('value') == 'null') {
                  $('#Answers4').val('');
              }
              if ($('#Answers5').attr('value') == 'null') {
                  $('#Answers5').val('');
              }
              if ($('#Answers6').attr('value') == 'null') {
                  $('#Answers6').val('');
              }
              if ($('#Answers7').attr('value') == 'null') {
                  $('#Answers7').val('');
              }
              $('#Answers8').val('null');
              $('#Answers9').val('null');
              $('#Answers10').val('null');
              $('fieldset div.ansv').each(function () {
                  if ($(this).find('#Answers1').length != 0 || $(this).find('#Answers2').length != 0
                  || $(this).find('#Answers3').length != 0 || $(this).find('#Answers4').length != 0
                  || $(this).find('#Answers5').length != 0 || $(this).find('#Answers6').length != 0
                  || $(this).find('#Answers7').length != 0) {
                      $(this).show();
                  }
              });
          }
          if (selected == '8') {
              if ($('#Answers1').attr('value') == 'null') {
                  $('#Answers1').val('');
              }
              if ($('#Answers2').attr('value') == 'null') {
                  $('#Answers2').val('');
              }
              if ($('#Answers3').attr('value') == 'null') {
                  $('#Answers3').val('');
              }
              if ($('#Answers4').attr('value') == 'null') {
                  $('#Answers4').val('');
              }
              if ($('#Answers5').attr('value') == 'null') {
                  $('#Answers5').val('');
              }
              if ($('#Answers6').attr('value') == 'null') {
                  $('#Answers6').val('');
              }
              if ($('#Answers7').attr('value') == 'null') {
                  $('#Answers7').val('');
              }
              if ($('#Answers8').attr('value') == 'null') {
                  $('#Answers8').val('');
              }
              $('#Answers9').val('null');
              $('#Answers10').val('null');
              $('fieldset div.ansv').each(function () {
                  if ($(this).find('#Answers1').length != 0 || $(this).find('#Answers2').length != 0
                  || $(this).find('#Answers3').length != 0 || $(this).find('#Answers4').length != 0
                  || $(this).find('#Answers5').length != 0 || $(this).find('#Answers6').length != 0
                  || $(this).find('#Answers7').length != 0 || $(this).find('#Answers8').length != 0) {
                      $(this).show();
                  }
              });
          }
          if (selected == '9') {
              if ($('#Answers1').attr('value') == 'null') {
                  $('#Answers1').val('');
              }
              if ($('#Answers2').attr('value') == 'null') {
                  $('#Answers2').val('');
              }
              if ($('#Answers3').attr('value') == 'null') {
                  $('#Answers3').val('');
              }
              if ($('#Answers4').attr('value') == 'null') {
                  $('#Answers4').val('');
              }
              if ($('#Answers5').attr('value') == 'null') {
                  $('#Answers5').val('');
              }
              if ($('#Answers6').attr('value') == 'null') {
                  $('#Answers6').val('');
              }
              if ($('#Answers7').attr('value') == 'null') {
                  $('#Answers7').val('');
              }
              if ($('#Answers8').attr('value') == 'null') {
                  $('#Answers8').val('');
              }
              if ($('#Answers9').attr('value') == 'null') {
                  $('#Answers9').val('');
              }
              $('#Answers10').val('null');
              $('fieldset div.ansv').each(function () {
                  if ($(this).find('#Answers1').length != 0 || $(this).find('#Answers2').length != 0
                  || $(this).find('#Answers3').length != 0 || $(this).find('#Answers4').length != 0
                  || $(this).find('#Answers5').length != 0 || $(this).find('#Answers6').length != 0
                  || $(this).find('#Answers7').length != 0 || $(this).find('#Answers8').length != 0
                  || $(this).find('#Answers9').length != 0) {
                      $(this).show();
                  }
              });
          }
          if (selected == '10') {
              if ($('#Answers1').attr('value') == 'null') {
                  $('#Answers1').val('');
              }
              if ($('#Answers2').attr('value') == 'null') {
                  $('#Answers2').val('');
              }
              if ($('#Answers3').attr('value') == 'null') {
                  $('#Answers3').val('');
              }
              if ($('#Answers4').attr('value') == 'null') {
                  $('#Answers4').val('');
              }
              if ($('#Answers5').attr('value') == 'null') {
                  $('#Answers5').val('');
              }
              if ($('#Answers6').attr('value') == 'null') {
                  $('#Answers6').val('');
              }
              if ($('#Answers7').attr('value') == 'null') {
                  $('#Answers7').val('');
              }
              if ($('#Answers8').attr('value') == 'null') {
                  $('#Answers8').val('');
              }
              if ($('#Answers9').attr('value') == 'null') {
                  $('#Answers9').val('');
              }
              if ($('#Answers10').attr('value') == 'null') {
                  $('#Answers10').val('');
              }
              $('fieldset div.ansv').each(function () {
                      $(this).show();                  
              });
          }
       // }
    });
});
$(function () {
    $('#CloseDate').datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'yy-mm-dd'
    });
});