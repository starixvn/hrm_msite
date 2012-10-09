
var JobLogManagement = function ($) {

    return {
        buttonImage: null,

        InitData: function () {
            JobLogManagement.buttonImage = $('#imagePath').val();

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