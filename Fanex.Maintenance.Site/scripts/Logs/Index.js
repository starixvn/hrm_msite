var Logs = function ($) {

    return {
        generateData: function (pageSize, index) {
            var categoryId = $('#CategoryId').val();
            var machineId = $('#MachineId').val();
            var severity = $('#Severity').val();
            var from = $('#From').val();
            var to = $('#To').val();
            var isProduction = $('#IsProduction').val();
            var isMonitored = $('#IsMonitored').val();

            index++;

            $.ajax({
                url: NAP.resolveClientUrl("Log/Index"),
                data: {
                    CategoryId: categoryId,
                    MachineId: machineId,
                    Severity: severity,
                    IsMonitored: isMonitored,
                    From: from,
                    To: to,
                    IsProduction: isProduction,
                    Page: index,
                    Size: pageSize 
                },
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
                            { Name: "IsMonitored", Value: "<input type='checkbox' data-logid='" + value.LogId + "' class='monitored'" + (value.IsMonitored ? " checked='true'" : "") + " />" },
                            { Name: "Message", Value: "<div class='hiden selected-message' id='" + value.LogId + "'>" + value.FullMessage + "</div><div data-logid='" + value.LogId + "' class='message'>" + value.Message + "</div>" },
                            { Name: "Category", Value: value.Category[0].CategoryName },
                            { Name: "Severity", Value: value.Severity },
                            { Name: "MachineName", Value: value.MachineName },
                            { Name: "MonitoringUser", Value: "<span id='monitoringUser" + value.LogId + "'>" + (value.MonitoringUser == null ? "" : value.MonitoringUser) + "</span>" },
                            { Name: "Timestamp", Value: value.LogDate }
                        ]
                        });
                    });

                    var gridOption = {
                        columns: [
                            { HeaderText: "#", Width: 35, Name: "No", HeaderAlign: "center", CellAlign: "center" },
                            { HeaderText: "Monitored", Width: 50, Name: "IsMonitored", HeaderAlign: "center", CellAlign: "center" },
                            { HeaderText: "Message", Width: 583, Name: "Message", HeaderAlign: "center", CellAlign: "left" },
                            { HeaderText: "Category", Width: 100, Name: "Category", HeaderAlign: "center", CellAlign: "left" },
                            { HeaderText: "Severity", Width: 100, Name: "Severity", HeaderAlign: "center", CellAlign: "left" },
                            { HeaderText: "Machine Name", Width: 100, Name: "MachineName", HeaderAlign: "center", CellAlign: "left" },
                            { HeaderText: "Monitoring User", Width: 95, Name: "MonitoringUser", HeaderAlign: "center", CellAlign: "left" },
                            { HeaderText: "Time (GMT+7)", Width: 120, Name: "Timestamp", HeaderAlign: "center", CellAlign: "center" }
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
                        gridExpandHeight: 320
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

    $('.monitored').live('click', function () {
        var logId = $(this).attr('data-logId');
        var isMonitored = $(this).prop('checked');

        $.ajax({
            type: "POST",
            url: "Log/MonitorLog",
            data: { logId: logId, isMonitored: isMonitored },
            success: function (data) {
                var monitoringUser = $('#monitoringUser' + logId);
                if (monitoringUser)
                    monitoringUser.text(data);
            }
        });
    });

    Logs.generateData(50, 0);

    $('#submit').live('click', function () {
        Logs.generateData(50, 0);
    });
});