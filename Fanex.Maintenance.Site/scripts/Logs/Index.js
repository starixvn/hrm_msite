var Logs = function ($) {

    return {
        ddlCategory: null,
        ddlMachine: null,
        ddlSeverity: null,
        ddlStatus: null,
        dpkFrom: null,
        btnRemoveLog: null,
        btnSubmit: null,

        init: function () {
            Logs.ddlCategory = $('#CategoryId');
            Logs.ddlMachine = $('#MachineId');
            Logs.ddlSeverity = $('#Severity');
            Logs.ddlStatus = $('#IsMonitored');
            Logs.dpkFrom = $('#From');
            Logs.btnRemoveLog = $("#btnRemoveLog");
            Logs.btnSubmit = $('#submit');
        },
        generateData: function (pageSize, index) {
            var categoryId = Logs.ddlCategory.val();
            var machineId = Logs.ddlMachine.val();
            var severity = Logs.ddlSeverity.val();
            var from = Logs.dpkFrom.datepicker('option', 'dateFrom');
            var to = Logs.dpkFrom.datepicker('option', 'dateTo');
            var isProduction = null;
            var isMonitored = Logs.ddlStatus.val();

            machineId = machineId == null ? 0 : machineId;
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
                    var categoryName = "";

                    $.each(data, function (key, value) {

                        if (value.Category != null) {
                            categoryName = value.Category[0].CategoryName;
                        } else {
                            categoryName = ""
                        }

                        bodyData.push({
                            Columns:
                        [
                            { Name: "No", Value: pageSize * (index - 1) + count++ },
                            { Name: "Message", Value: "<div class='hiden selected-message' id='" + value.LogId + "'>" + value.FullMessage + "</div><div data-logid='" + value.LogId + "' class='message'>" + value.Message + "</div>" },
                            { Name: "Category", Value: categoryName },
                            { Name: "Severity", Value: value.Severity },
                            { Name: "MachineName", Value: value.MachineName },
                            { Name: "MonitoringUser", Value: "<span id='monitoringUser" + value.LogId + "'>" + (value.IsMonitored ? value.MonitoringUser : "<input type='checkbox' data-logid='" + value.LogId + "' class='monitored' />") + "</span>" },
                            { Name: "Timestamp", Value: value.LogDate }
                        ]
                        });
                    });

                    var gridOption = {
                        columns: [
                            { HeaderText: "#", Width: 25, Name: "No", HeaderAlign: "center", CellAlign: "center" },
                            { HeaderText: "Message", Width: 652, Name: "Message", HeaderAlign: "center", CellAlign: "left" },
                            { HeaderText: "Category", Width: 120, Name: "Category", HeaderAlign: "center", CellAlign: "center" },
                            { HeaderText: "Severity", Width: 80, Name: "Severity", HeaderAlign: "center", CellAlign: "center" },
                            { HeaderText: "Server Name", Width: 100, Name: "MachineName", HeaderAlign: "center", CellAlign: "center" },
                            { HeaderText: "Handled By", Width: 100, Name: "MonitoringUser", HeaderAlign: "center", CellAlign: "center" },
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
                    $('.message').on('click', Logs.clickMessage);
                    $('.monitored').on('click', Logs.monitorLog);
                },
                error: function (xhr, status, err) {
                    NAP.handleAjaxError(xhr, status, err);
                }
            });
        },

        reRenderAfterPaged: function (page_index, itemsPerPage, jq) {
            Logs.generateData(itemsPerPage, page_index);
        },
        removeLog: function () {
            var answer = confirm("Are you sure delete logs !");
            if (answer) {
                $.ajax({
                    type: "POST",
                    url: "Log/RemoveLog",
                    beforeSend: function () {
                        NAP.loadingBox.show();
                    },
                    complete: function () {
                        NAP.loadingBox.close();
                    },
                    success: function (data) {
                        if (data == 0)
                            NAP.headlineMsgBox.showInfo("No log removed !", { closeBtn: false });
                        else {
                            NAP.headlineMsgBox.showInfo("Removed " + data + " logs !", { closeBtn: false });
                        }
                        Logs.btnRemoveLog.attr("disabled", "true");
                    },
                    error: function (xhr, status, err) {
                        NAP.handleAjaxError(xhr, status, err);
                    }
                });
            }
        },
        clickMessage: function () {
            var logId = $(this).attr('data-logId');
            $('.hiden').hide();
            $('.message').show();
            $(this).hide();
            $('#' + logId).show();
        },
        monitorLog: function () {
            var $this = $(this);
            var isMonitored = $this.is(':checked');

            $this.prop('checked', !isMonitored).change();

            NAP.headlineMsgBox.confirm('Are you sure that this log is being handled', function () {
                var logId = $this.attr('data-logId');

                $.ajax({
                    type: "POST",
                    url: "Log/MonitorLog",
                    data: { logId: logId, isMonitored: isMonitored },
                    beforeSend: function () {
                        NAP.loadingBox.show();
                    },
                    complete: function () {
                        NAP.loadingBox.close();
                    },
                    success: function (data) {
                        var monitoringUser = $('#monitoringUser' + logId);
                        $this.hide();
                        if (monitoringUser)
                            monitoringUser.text(data);
                    },
                    error: function (xhr, status, err) {
                        NAP.handleAjaxError(xhr, status, err);
                    }
                });
            }, { model: true, closeBtn: false, position: ['top:420', 'center'] });
        },
        getMachinesByType: function (machineType) {
            $.ajax({
                type: "GET",
                url: "Machine/GetMachinesByType",
                data: { machineType: machineType },
                beforeSend: function () {
                    NAP.loadingBox.show();
                },
                complete: function () {
                    NAP.loadingBox.close();
                },
                success: function (data) {
                    if (data.length > 0) {
                        //var ddlMachines = $('#MachineId');
                        var uatGroup = $('<optgroup>').attr('label', 'UAT');
                        var productionGroup = $('<optgroup>').attr('label', 'Production');
                        var optionItem;

                        Logs.ddlMachine.empty();
                        Logs.ddlMachine.append($('<option>', { value: '0', text: 'All' }));

                        $.each(data, function (key, value) {
                            optionItem = $('<option>', { value: value.MachineId, text: value.MachineName });
                            if (value.IsProduction) {
                                productionGroup.append(optionItem);
                            } else {
                                uatGroup.append(optionItem);
                            }
                        });

                        if (productionGroup[0].childElementCount > 0)
                            Logs.ddlMachine.append(productionGroup);
                        if (uatGroup[0].childElementCount > 0)
                            Logs.ddlMachine.append(uatGroup);

                        Logs.ddlMachine.select2();
                    }
                },
                error: function (xhr, status, err) {
                    NAP.handleAjaxError(xhr, status, err);
                }
            });
        },

        getMachines: function () {
            var machineType = $('#IsProduction').val();
            Logs.getMachinesByType(machineType);
        },

        bindEventHandlers: function () {
            Logs.getMachines(0);
            Logs.ddlCategory.select2();
            Logs.btnRemoveLog.bind("click", Logs.removeLog);
            Logs.btnSubmit.live('click', function () {
                Logs.generateData(50, 0);
            });
            Logs.dpkFrom.datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                numberOfMonths: 2,
                dateFormat: "M dd,yy",
                showOn: "button",
                buttonImage: $('#imagePath').val(),
                buttonText: "From",
                buttonImageOnly: true,
                isDateRange: true,
                dateFrom: new Date(),
                dateTo: new Date()
            }).datepicker('init', '#From');
        }
    };

} (jQuery);

$(document).ready(function () {
    Logs.init();
    Logs.bindEventHandlers();

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

    Logs.generateData(50, 0);
});