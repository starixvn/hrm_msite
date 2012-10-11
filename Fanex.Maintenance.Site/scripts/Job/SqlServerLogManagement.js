/* File Created: October 10, 2012 */

var SqlServerLogManagement = function ($) {

    return {
        dateFrom: null,
        dateTo: null,
        buttonImage: null,

        InitData: function () {
            SqlServerLogManagement.dateFrom = $("#dateFrom").val();
            SqlServerLogManagement.dateTo = $("#dateTo").val();
            SqlServerLogManagement.buttonImage = $('#imagePath').val();
            $('#PageSizeSelect').val($('#PageSize').val());
        },

        InitDatetimePicker: function () {
            $("#From").val(SqlServerLogManagement.dateFrom);
            $("#To").val(SqlServerLogManagement.dateTo);

            $("#From").datepicker({
                changeMonth: true,
                numberOfMonths: 1,
                showOn: "button",
                buttonImage: SqlServerLogManagement.buttonImage,
                buttonImageOnly: true,
                dateFormat: "mm/dd/yy"
            });

            $("#To").datepicker({
                changeMonth: true,
                numberOfMonths: 1,
                showOn: "button",
                buttonImage: SqlServerLogManagement.buttonImage,
                buttonImageOnly: true,
                dateFormat: "mm/dd/yy"
            });
        },
        OnNextPage: function (moveIndex) {
            var currentIndex = parseInt($('#PageIndex').val());
            if (currentIndex + moveIndex >= 0) {
                $('#PageIndex').val(currentIndex + moveIndex);
                $('.content_wrap form').submit();
            }
        },
        OnChangePageSize: function () {
            $('#PageSize').val($('#PageSizeSelect').val());
            $('.content_wrap form').submit();
        }
    };

} (jQuery);

$(document).ready(function () {
    SqlServerLogManagement.InitData();
    SqlServerLogManagement.InitDatetimePicker();
});     