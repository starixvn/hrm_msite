
var JobManagementPage = function ($) {

    return {
        dateFrom: null,
        dateTo: null,
        buttonImage: null,

        InitData: function () {
            JobManagementPage.dateFrom = $("#dateFrom").val();
            JobManagementPage.dateTo = $("#dateTo").val();
            JobManagementPage.buttonImage = $('#imagePath').val() ;
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
            $("#From").val(JobManagementPage.dateFrom);
            $("#To").val(JobManagementPage.dateTo);

            $("#From").datepicker({
                showOn: "button",
                buttonImage: JobManagementPage.buttonImage,
                buttonImageOnly: true,
                dateFormat: "dd/mm/yy"
            });

            $("#To").datepicker({
                showOn: "button",
                buttonImage: JobManagementPage.buttonImage,
                buttonImageOnly: true,
                dateFormat: "dd/mm/yy"
            });
        }
    };

} (jQuery);

$(document).ready(function () {
    JobManagementPage.InitData();
    JobManagementPage.InitDatetimePicker();
    JobManagementPage.SelectDateType();

    $('#DateType').bind('change', function () {
        JobManagementPage.SelectDateType();
    });
});