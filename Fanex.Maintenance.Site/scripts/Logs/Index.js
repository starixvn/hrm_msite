var Index = function ($) {

    return {
        page: 1,

        OnNextPage: function (moveIndex) {
            var currentIndex = parseInt($('#Page').val());
            if (currentIndex + moveIndex >= 0) {
                $('#Page').val(currentIndex + moveIndex);
                $('.content form').submit();
            }
        },

        OnChangePageSize: function () {
            $('#PageSize').val($('#Size').val());
            console.log($('#PageSize').val($('#Size').val()));
            Index.UpdatePaging();
            $('.content form').submit();
        },

        UpdatePaging: function () {
            $('#Page').val(0);
            $('#PageSize').val($('#Size').val());
        },

        UpdateNavigation: function () {
            var count = $('#countData').val();
            if (count == 0) {
                $('.pagiNext').hide();
            }
            if ($('#Page').val() == 0) {
                $('.pagiPre').hide();
            }
        }
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

    Index.UpdateNavigation();
});