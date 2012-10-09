
var JobLogManagement = function ($) {

    return {
        buttonImage: null,

        InitData: function () {
            JobLogManagement.buttonImage = $('#imagePath').val();

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

            var dates = $("#From, #To").datepicker({
                changeMonth: true,
                numberOfMonths: 1,
                dateFormat: "MM dd,yy",
                showOn: "button",
                buttonImage: JobLogManagement.buttonImage,
                buttonImageOnly: true,
                onSelect: function (selectedDate) {
                    var option = this.id == "From" ? "minDate" : "maxDate",
					instance = $(this).data("datepicker"),
					date = $.datepicker.parseDate(
						instance.settings.dateFormat ||
						$.datepicker._defaults.dateFormat,
						selectedDate, instance.settings);
                    dates.not(this).datepicker("option", option, date);
                }
            });
        }
    };

} (jQuery);

$(document).ready(function () {
    JobLogManagement.InitData();
    JobLogManagement.InitDatetimePicker();

    $('.chosen').chosen();

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