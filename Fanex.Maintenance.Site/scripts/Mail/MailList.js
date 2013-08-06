var MailList = function ($) {

    return {
        generateData: function (pageSize, index) {
            var appId = $('#ApplicationID').val();
            var from = $('#From').val();
            var to = $('#To').val();
            var status = $('#MailStatus').val();
            index++;

            $.ajax({
                url: NAP.resolveClientUrl("Mail/MailListPagi"),
                data: { status: status, applicationID: appId, from: from, to: to, page: index, size: pageSize },
                type: 'POST',
                dataType: 'JSON',
                cache: false,
                beforeSend: function () {
                    NAP.loadingBox.show();
                },
                complete: function () {
                    NAP.loadingBox.close();
                },
                success: function (result) {
                    var data = eval(result.ListMail);
                    var bodyData = [];
                    var count = 1;
                    $.each(data, function (key, value) {

                        var classFail = "";
                        var linkSendFail = "0";
                        if (value.SentFail > 0) {
                            classFail = "mailfail";
                            linkSendFail = "<a href='#' class='errormail' data-mailid='" + value.MailID + "'>" + value.SentFail + "</a>";
                        }


                        bodyData.push({
                            Columns:
                        [
                            { Name: "No", Value: count++ },
                            { Name: "Subject", Value: "<a href='#' class='emailMessage' data-mailid='" + value.MailID + "'>" + value.Subject + "</a>" },
                            { Name: "ApplicationName", Value: value.Config.AppName },
                            { Name: "MailTo", Value: value.MailTo },
                            //                            { Name: "MailCc", Value: "" },
                            {Name: "CreatedDate", Value: value.MailDate },
                            { Name: "LatestSendDate", Value: value.MailUpdatedDate },
                            { Name: "SendSuccess", Value: value.SentSuccess },
                            { Name: "SendFail", Value: linkSendFail },
                            { Name: "Resend", Value: "<a href='#' class='resendmail " + classFail + "' data-mailid='" + value.MailID + "'>Resend</a>" }
                        ]
                        });
                    });

                    var gridOption = {
                        columns: [
                            { HeaderText: "#", Width: 35, Name: "No", HeaderAlign: "center", CellAlign: "center" },
                            { HeaderText: "Subject", Width: 320, Name: "Subject", HeaderAlign: "center", CellAlign: "left" },
                            { HeaderText: "Application Name", Width: 100, Name: "ApplicationName", HeaderAlign: "center", CellAlign: "left" },
                            { HeaderText: "Mail To", Width: 200, Name: "MailTo", HeaderAlign: "center", CellAlign: "left" },
                        //                            { HeaderText: "Mail Cc", Width: 100, Name: "MailCc", HeaderAlign: "center", CellAlign: "left" },
                            {HeaderText: "Created Date", Width: 130, Name: "CreatedDate", HeaderAlign: "center", CellAlign: "center" },
                            { HeaderText: "Latest Send Date", Width: 130, Name: "LatestSendDate", HeaderAlign: "center", CellAlign: "center" },
                            { HeaderText: "Success", Width: 80, Name: "SendSuccess", HeaderAlign: "center", CellAlign: "center" },
                            { HeaderText: "Fail", Width: 80, Name: "SendFail", HeaderAlign: "center", CellAlign: "center" },
                            { HeaderText: "Resend", Width: 70, Name: "Resend", HeaderAlign: "center", CellAlign: "center" }
                        ],
                        bodyRows: bodyData,
                        showPager: true,
                        customPager: true,
                        pagerOption: {
                            itemsPerPage: pageSize,
                            maxItemsPerPage: 100,
                            currentPage: index - 1,
                            totalItem: result.TotalRows,
                            startPerPage: 50,
                            pageStep: 10,
                            pagerSelectCallBack: MailList.reRenderAfterPaged
                        },
                        gridExpandHeight: 270
                    };

                    $("#gridId").fanexGrid(gridOption);
                },
                error: function (xhr, status, err) {
                    NAP.handleAjaxError(xhr, status, err);
                }
            });
        },

        reRenderAfterPaged: function (page_index, itemsPerPage, jq) {
            MailList.generateData(itemsPerPage, page_index);
        },

        resend: function (mailId) {
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

                    window.location.href = window.location.href;
                },
                error: function (xhr, status, err) {
                    NAP.handleAjaxError(xhr, status, err);
                }
            });
        },

        getMessage: function (mailId) {
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
        },

        getError: function (mailId) {
            $.ajax({
                url: NAP.resolveClientUrl("Mail/GetError"),
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
        }
    };

} (jQuery);

$(document).ready(function () {

    $('.resendmail').live('click', function () {
        var mailId = $(this).attr("data-mailid");
        MailList.resend(mailId);
    });

    $('.emailMessage').live('click', function () {
        var mailId = $(this).attr("data-mailid");
        MailList.getMessage(mailId);
    });

    $('.errormail').live('click', function () {
        var mailId = $(this).attr("data-mailid");
        MailList.getError(mailId);
    });

    MailList.generateData(50, 0);

    $('#submit').live('click', function () {
        MailList.generateData(50, 0);
    });
});