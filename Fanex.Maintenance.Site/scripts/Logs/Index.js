var Logs = function ($) {

    return {
        ddlCategory: null,
        ddlMachine: null,
        ddlSeverity: null,
        ddlStatus: null,
        dpkFrom: null,
        btnRemoveLog: null,
        btnSubmit: null,
        chbGroupMessage: null,

        init: function () {
            Logs.ddlCategory = $('#CategoryId');
            Logs.ddlMachine = $('#MachineId');
            Logs.ddlSeverity = $('#Severity');
            Logs.ddlStatus = $('#IsMonitored');
            Logs.dpkFrom = $('#From');
            Logs.btnRemoveLog = $("#btnRemoveLog");
            Logs.btnSubmit = $('#submit');
            Logs.chbGroupMessage = $('#GroupMessage');
        },
        generateData: function (pageSize, index) {
            var categoryId = Logs.ddlCategory.val();
            var machineId = Logs.ddlMachine.val();
            var severity = Logs.ddlSeverity.val();
            var from = Logs.dpkFrom.datepicker('option', 'dateFrom');
            var to = Logs.dpkFrom.datepicker('option', 'dateTo');
            var isProduction = null;
            var isMonitored = Logs.ddlStatus.val();

            if (machineId == 'UAT') {
                isProduction = false;
                machineId = 0;
            } else if (machineId == 'Production') {
                isProduction = true;
                machineId = 0;
            }

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
                    Size: pageSize,
                    GroupMessage: Logs.chbGroupMessage.is(":checked")
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
                    var count = 1;
                    $('#content').html('');
                    $.each(data, function (key, value) {

                        var countMessage = "";
                        if (value.CountGroupMessage > 0) {
                            countMessage = "<div>(" + value.CountGroupMessage + " Message)</div>";
                        }

                        var content = "<tr><td>"
                        + (pageSize * (index - 1) + count++) + "</td><td>"
                        + "<div class='hiden selected-message' id='" + value.LogId + "'>" + value.FullMessage + "</div><div data-logid='" + value.LogId + "' class='message'>" + value.Message + "</div>" + "</td><td class='align-center'>"
                        + value.Category[0].CategoryName + "</td><td class='align-center'>"
                        + value.Severity + "</td><td class='align-center'>"
                        + value.MachineName + "</td><td class='align-center'>"
                        + "<span id='monitoringUser" + value.LogId + "'>" + (value.IsMonitored ? value.MonitoringUser : "<input type='checkbox' data-logids='" + value.GroupMessageIds + "' data-logid='" + value.LogId + "' class='monitored' />") + "</span>" + countMessage + "</td><td class='align-center'>"
                        + value.LogDate + "</td></tr>";

                        $('#content').append(content);
                    });

                    $('.message').on('click', Logs.clickMessage);
                    $('.monitored').on('click', Logs.monitorLog);

                    $('#pagination').fanexPagination(result.TotalRows, {
                        itemsPerPage: pageSize,
                        startPerPage: 50,
                        itemsPerPageArray: [50, 100, 500],
                        numDisplayEntries: 5,
                        currentPage: index - 1,
                        numEdgeEntries: 3,
                        linkTo: "javascript:void(0)",
                        prevText: "&lt;",
                        nextText: "&gt;",
                        ellipseText: "...",
                        prevShowAlways: true,
                        nextShowAlways: true,
                        maxTtemsPerPage: 100,
                        showDetail: true,
                        callback: Logs.reRenderAfterPaged
                    });
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
            var message = $('#' + logId).text();
            NAP.popup('Detail', message);
        },
        monitorLog: function () {
            var $this = $(this);
            var isMonitored = $this.is(':checked');

            $this.prop('checked', !isMonitored).change();

            NAP.headlineMsgBox.confirm('Are you sure that this log is being handled', function () {
                var logId = $this.attr('data-logIds');

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
                        $('#submit').click();
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
                        var uatGroup = $('<optgroup>').attr('label', 'UAT').append($('<option>', { value: 'UAT', text: 'All UAT' }));
                        var productionGroup = $('<optgroup>').attr('label', 'Production').append($('<option>', { value: 'Production', text: 'All Production' }));
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

                        if (productionGroup[0].childElementCount > 1)
                            Logs.ddlMachine.append(productionGroup);
                        if (uatGroup[0].childElementCount > 1)
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
        },

        initFreeHeader: function () {

            var clonedHeaderRow;
            $(".persist-area").each(function () {
                clonedHeaderRow = $(".persist-header", this);
                clonedHeaderRow
					 .before(clonedHeaderRow.clone())
					 .css({ "width": clonedHeaderRow.width() })
					 .addClass("floatingHeader")
					 .removeClass("persist-header");
            });

            $(window)
				.scroll(Logs.updateTableHeaders)
				.trigger("scroll");
            $(window).resize(function () {
                Logs.updateTableHeaders();
            });
        },

        updateTableHeaders: function () {
            if ($(".persist-area").val() != undefined) {
                var widthspace = $(".persist-area").width();

                $('.floatingHeader').width(widthspace);
                var left = $('table thead tr:first').position()["left"];
                $(".persist-area").each(function () {
                    var el = $(this),
						offset = el.offset(),
						scrollTop = $(window).scrollTop(),
						floatingHeader = $(".floatingHeader", this);
                    if ((scrollTop > offset.top) && (scrollTop < offset.top + el.height())) {
                        floatingHeader.css({
                            "visibility": "visible",
                            "left": (left - $(window).scrollLeft()),
                            "z-index": "34",
                            "margin-left": "10px"
                        });
                    } else {
                        floatingHeader.css({
                            "visibility": "hidden"
                        });
                    };
                });

                var i = 0;

                var persistheader = $("table.persist-area thead.persist-header tr td");
                var floatingheader = $("table.persist-area thead.floatingHeader tr td");

                if (persistheader.length <= 0) {
                    persistheader = $("table.persist-area thead.persist-header tr th");
                }

                if (floatingheader.length <= 0) {
                    floatingheader = $("table.persist-area thead.floatingHeader tr th");
                }

                persistheader.each(function () {
                    var x = $(this).width();
                    $(floatingheader[i]).width(x);
                    i++;
                });
            }
        }
    };

} (jQuery);

$(document).ready(function () {
    Logs.init();
    Logs.bindEventHandlers();
    Logs.initFreeHeader();

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