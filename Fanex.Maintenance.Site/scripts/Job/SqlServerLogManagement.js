/* File Created: October 10, 2012 */

var SqlServerLogManagement = function ($) {

    return {
        dateFrom: null,
        dateTo: null,
        buttonImage: null,
        serverIpOld: null,

        InitData: function () {
            SqlServerLogManagement.dateFrom = $("#dateFrom").val();
            SqlServerLogManagement.dateTo = $("#dateTo").val();
            SqlServerLogManagement.buttonImage = $('#imagePath').val();
            SqlServerLogManagement.serverIp = $('#Server').val();
            $('#PageSizeSelect').val($('#PageSize').val());
        },

        InitDatetimePicker: function () {
            $("#From").val(SqlServerLogManagement.dateFrom);
            $("#To").val(SqlServerLogManagement.dateTo);

            var dates = $("#From, #To").datepicker({
                changeMonth: true,
                numberOfMonths: 1,
                dateFormat: "M dd,yy",
                showOn: "button",
                buttonImage: SqlServerLogManagement.buttonImage,
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
        },
        OnNextPage: function (moveIndex) {
            var currentIndex = parseInt($('#PageIndex').val());
            if (currentIndex + moveIndex >= 0) {
                $('#PageIndex').val(currentIndex + moveIndex);
                $('.content form').submit();
            }
        },
        OnChangePageSize: function () {
            $('#PageSize').val($('#PageSizeSelect').val());
            if (SqlServerLogManagement.serverIp != $('#Server').val()) {
                SqlServerLogManagement.UpdatePaging();
            }
            $('.content form').submit();
        },
        UpdatePaging: function () {
            $('#PageIndex').val(0);
            $('#PageSize').val($('#PageSizeSelect').val());
        }
    };

} (jQuery);

$(document).ready(function () {
    SqlServerLogManagement.InitData();
    SqlServerLogManagement.InitDatetimePicker();
});     