
var JobManagementPage = function ($) {

    return {

        GetSchedule: function (server) {
            $.ajax({
                url: NAP.resolveClientUrl("SqlJobManagement/GetSchedule"),
                data: { server: server },
                type: 'POST',
                dataType: 'JSON',
                cache: false,
                beforeSend: function () {
                    //                    NAP.loadingBox.show();
                    $('#Schedule').attr('disabled', 'disabled');
                },
                complete: function () {
                    //                    NAP.loadingBox.close();
                    $('#Schedule').removeAttr('disabled', 'disabled');
                },
                success: function (result) {
                    $("#Schedule").html("");
                    $("#Schedule").append("<option value=''>All</option>");
                    $("#Schedule").append($("#templateSchedule").render(result));
                },
                error: function (xhr, status, err) {
                    NAP.handleAjaxError(xhr, status, err);
                }
            });
        }
    };

} (jQuery);

$(document).ready(function () {

    //disabled when server is all
    if ($('#Server').val() == '') {
        $('#Schedule').attr('disabled', 'disabled');
    }
    else {
        $('#Schedule').removeAttr('disabled', 'disabled');
    }

    $('#Server').bind('change', function () {
        var server = $(this).val();
        JobManagementPage.GetSchedule(server);

        //disabled when server is all
        if (server == '') {
            $('#Schedule').attr('disabled', 'disabled');
        }
        else {
            $('#Schedule').removeAttr('disabled', 'disabled');
        }
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