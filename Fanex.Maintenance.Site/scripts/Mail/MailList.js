var AddMailConfigPopUp = function ($) {

    return {
        page: 1
    };

} (jQuery);

$(document).ready(function () {

    $('.resendmail').live('click', function () {
        var mailId = $(this).attr("mailid");
        $.ajax({
            url: NAP.resolveClientUrl("Mail/ResendMail"),
            data: { mailId: mailId },
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
                if (result) {
                    NAP.headlineMsgBox.showInfo("Resend Success !", { closeBtn: false });
                }
                else {
                    NAP.headlineMsgBox.showInfo("Resend Faild !", { closeBtn: false });
                }
            },
            error: function (xhr, status, err) {
                NAP.handleAjaxError(xhr, status, err);
            }
        });
    });

    $('.emailMessage').live('click', function () {
        var mailId = $(this).attr("mailid");
        $.ajax({
            url: NAP.resolveClientUrl("Mail/GetMessage"),
            data: { mailId: mailId },
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

    $('.pagiPre').click(function () {
        var appId = $('#ApplicationID').val();
        var from = $('#From').val();
        var to = $('#To').val();
        var size = $('#size').val();

        if (AddMailConfigPopUp.page > 0) {
            AddMailConfigPopUp.page--;
        }
        $.ajax({
            url: NAP.resolveClientUrl("Mail/MailListPagi"),
            data: { applicationID: appId, from: from, to: to, page: AddMailConfigPopUp.page, size: size },
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
                $("#contentMailList").html("");
                result = eval(result);
                if (result.length > 0) {
                    $("#contentMailList").append($("#templateContentEmail").render(result));
                }
                else {
                    var temp = "<tr><td colspan='10'> Not found </td></tr>";
                    $("#contentMailList").html(temp);
                }
            },
            error: function (xhr, status, err) {
                NAP.handleAjaxError(xhr, status, err);
            }
        });
    });

    $('.pagiNext').click(function () {
        var appId = $('#ApplicationID').val();
        var from = $('#From').val();
        var to = $('#To').val();
        var size = $('#size').val();

        AddMailConfigPopUp.page++;

        $.ajax({
            url: NAP.resolveClientUrl("Mail/MailListPagi"),
            data: { applicationID: appId, from: from, to: to, page: AddMailConfigPopUp.page, size: size },
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
                $("#contentMailList").html("");
                result = eval(result);
                if (result.length > 0) {
                    $("#contentMailList").append($("#templateContentEmail").render(result));
                }
                else {
                    AddMailConfigPopUp.page = 0;

                    var temp = "<tr><td colspan='10'> Not found </td></tr>";
                    $("#contentMailList").html(temp);

                }
            },
            error: function (xhr, status, err) {
                NAP.handleAjaxError(xhr, status, err);
            }
        });
    });

    $('#size').change(function () {
        var appId = $('#ApplicationID').val();
        var from = $('#From').val();
        var to = $('#To').val();
        var size = $('#size').val();
                
        $.ajax({
            url: NAP.resolveClientUrl("Mail/MailListPagi"),
            data: { applicationID: appId, from: from, to: to, page: AddMailConfigPopUp.page, size: size },
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
                $("#contentMailList").html("");
                result = eval(result);
                if (result.length > 0) {
                    $("#contentMailList").append($("#templateContentEmail").render(result));
                }
                else {
                    var temp = "<tr><td colspan='10'> Not found </td></tr>";
                    $("#contentMailList").html(temp);
                }
            },
            error: function (xhr, status, err) {
                NAP.handleAjaxError(xhr, status, err);
            }
        });
    });
});