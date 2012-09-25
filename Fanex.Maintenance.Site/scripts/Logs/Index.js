var Index = function ($) {

    return {
        page: 1
    };

} (jQuery);

$(document).ready(function () {

    $('.logMessage').live('click', function () {
        var logId = $(this).attr("logid");
        $.ajax({
            url: NAP.resolveClientUrl("Log/GetLogMessage"),
            data: { id: logId },
            type: 'POST',
            dataType: 'html',
            cache: false,
            beforeSend: function () {
                NAP.loadingBox.show();
            },
            complete: function () {
                NAP.loadingBox.close();
            },
            success: function (result) {
                if (result.length > 0) {
                    NAP.popup("Details", result);
                }
            },
            error: function (xhr, status, err) {
                NAP.handleAjaxError(xhr, status, err);
            }
        });
    });

    $("#btnRemoveLog").bind("click", function () {
        var answer = confirm("Are you sure delete logs !");
        if (answer) {
            $.ajax({
                type: "POST",
                url: "Log/RemoveLog",
                success: function (data) {
                    if (data == 0)
                        NAP.headlineMsgBox.showInfo("No log removed !", { closeBtn: false });
                    else {
                        NAP.headlineMsgBox.showInfo("Removed " + data + " logs !", { closeBtn: false });
                    }
                    $("#btnRemoveLog").attr("disabled", "true");
                }
            });
        }
        else {

        }
    });
});