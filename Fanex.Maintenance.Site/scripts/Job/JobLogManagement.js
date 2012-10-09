
var JobLogManagement = function ($) {

    return {
        date: null,
        buttonImage: null,

        InitData: function () {
            JobLogManagement.buttonImage = $('#imagePath').val();
            JobLogManagement.date = $('#dateTime').val();
        },

        //disbaled when datetype is all
        SelectDateType: function () {
            var type = $('#DateType').val();
            if (type == "") {
                $('#From').attr('disabled', 'disabled');
                $('#To').attr('disabled', 'disabled');
                $('.ui-datepicker-trigger').hide();
            }
            else {
                $('#From').removeAttr('disabled', 'disabled');
                $('#To').removeAttr('disabled', 'disabled');
                $('.ui-datepicker-trigger').show();
            }
        },

        InitDatetimePicker: function () {
            $("#From").val(JobLogManagement.date);
            $("#To").val(JobLogManagement.date);

            $("#From").datepicker({
                showOn: "button",
                buttonImage: JobLogManagement.buttonImage,
                buttonImageOnly: true,
                dateFormat: "dd/mm/yy"
            });

            $("#To").datepicker({
                showOn: "button",
                buttonImage: JobLogManagement.buttonImage,
                buttonImageOnly: true,
                dateFormat: "dd/mm/yy"
            });
        }
    };

} (jQuery);

$(document).ready(function () {
    JobLogManagement.InitData();
    JobLogManagement.InitDatetimePicker();
    JobLogManagement.SelectDateType();

    $('.chosen').chosen();

    $('#DateType').bind('change', function () {
        JobLogManagement.SelectDateType();
    });

    // #region actions dropdownlist
    var event = NAP.agentIsOldIe || NAP.agentIsApple ? 'click' : 'hover';

    $('.action-wrp').bind(event, function () {
        var $this = $(this),
			dropdown = $this.find('.actions');

        if (dropdown.css('visibility') == 'hidden') {
            $('.actions').css({ visibility: 'hidden', opacity: 0 });
            $this.find('.actions').css({ visibility: 'visible', opacity: 1 });
        } else {
            $this.find('.actions').css({ visibility: 'hidden', opacity: 0 });
        }
    });
    // #endregion
});