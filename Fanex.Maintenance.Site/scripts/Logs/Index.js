var Logs = function ($) {

    return {
        generateData: function (pageSize, index) {
            var categoryId = $('#CategoryId').val();
            var machineId = $('#MachineId').val();
            var severity = $('#Severity').val();
            var from = $('#From').val();
            var to = $('#To').val();

            index++;

            $.ajax({
                url: NAP.resolveClientUrl("Log/Index"),
                data: { CategoryId: categoryId, MachineId: machineId, Severity: severity, From: from, To: to, Page: index, Size: pageSize },
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
                    var data = eval(result.ListLog);
                    var bodyData = [];
                    var count = 1;
                    $.each(data, function (key, value) {

                        bodyData.push({
                            Columns:
                        [
                            { Name: "No", Value: pageSize * (index - 1) + count++ },
                            { Name: "Message", Value: "<div class='hiden selected-message' id='" + value.LogId + "'>" + value.FullMessage + "</div><div data-logid='" + value.LogId + "' class='message'>" + value.Message + "</div>" },
                            { Name: "Category", Value: value.Category[0].CategoryName },
                            { Name: "Severity", Value: value.Severity },
                            { Name: "MachineName", Value: value.MachineName },
                            { Name: "Timestamp", Value: value.LogDate }
                        ]
                        });
                    });

                    var gridOption = {
                        columns: [
                            { HeaderText: "#", Width: 35, Name: "No", HeaderAlign: "center", CellAlign: "center" },
                            { HeaderText: "Message", Width: 700, Name: "Message", HeaderAlign: "center", CellAlign: "left" },
                            { HeaderText: "Category", Width: 100, Name: "Category", HeaderAlign: "center", CellAlign: "left" },
                            { HeaderText: "Severity", Width: 100, Name: "Severity", HeaderAlign: "center", CellAlign: "left" },
                            { HeaderText: "MachineName", Width: 100, Name: "MachineName", HeaderAlign: "center", CellAlign: "left" },
                            { HeaderText: "Timestamp", Width: 130, Name: "Timestamp", HeaderAlign: "center", CellAlign: "center" }
                        ],
                        bodyRows: bodyData,
                        showPager: true,
                        customPager: true,
                        pagerOption: {
                            itemsPerPage: pageSize,
                            itemsPerPageArray: [50, 100, 500],
                            currentPage: index - 1,
                            totalItem: result.TotalRows,
                            startPerPage: 50,
                            pagerSelectCallBack: Logs.reRenderAfterPaged
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
            Logs.generateData(itemsPerPage, page_index);
        }
    };

} (jQuery);

$(document).ready(function () {
    $('#CategoryId').select2();
    $('#MachineId').select2();
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

    $('.message').live('click', function () {
        var logId = $(this).attr('data-logId');
        $('.hiden').hide();
        $('.message').show();
        $(this).hide();
        $('#' + logId).show();
    });

    Logs.generateData(50, 0);

    $('#submit').live('click', function () {
        Logs.generateData(50, 0);
    });
});