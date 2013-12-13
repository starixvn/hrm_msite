var Connection = function ($) {

    return {
        grid: null,
        aliasName: null,
        aliasId: null,

        GetDatabaseByAliasName: function (id) {
            $.ajax({
                url: NAP.resolveClientUrl("ConnectionString/GetDatabaseByDbServerId"),
                data: { databaseServerId: id },
                type: 'GET',
                cache: false,
                success: function (result) {
                    $('#DatabaseId').html("");
                    $.each(result, function (i, database) {
                        $('#DatabaseId').append("<option value='" + database.Id + "' data-serveralias='" + database.ServerAlias + "'>" + database.Name + "</option>");

                        if (database.id == id) {
                            $('#ServerAlias').val(database.ServerAlias);
                        }
                    });

                    Connection.GenerateConnectionName();
                },
                error: function (xhr, status, err) {
                    NAP.handleAjaxError(xhr, status, err);
                }
            });
        },

        UpdateStatusConnection: function (connectionid, isActive) {
            $.ajax({
                url: NAP.resolveClientUrl("ConnectionString/UpdateStatusConnection"),
                data: { id: connectionid, isActive: isActive },
                type: 'POST',
                cache: false,
                beforeSend: function () {
                    NAP.loadingBox.show();
                },
                complete: function () {
                    NAP.loadingBox.close();
                },
                success: function (result) {
                    if (result == 'true') {
                        NAP.headlineMsgBox.showInfo('Your changes were saved!', { closeBtn: false });
                        NAP.redirect('ConnectionString/Connection');
                    }
                },
                error: function (xhr, status, err) {
                    NAP.handleAjaxError(xhr, status, err);
                }
            });
        },

        GenerateConnectionName: function () {
            var aliasName = $('#DatabaseId option:selected').attr('data-serveralias');
            var databaseName = $("#DatabaseId option:selected").text();
            var loginName = $('#LoginName').val();

            $('#Name').val("");
            $('#Name').val(aliasName + "." + databaseName + "." + loginName);
        },

        SwitchDbServer: function () {
            var connectionIds = "";
            var toDbServerId = $('#ToDbServerId').val();
            var dbServerName = $('#ToDbServerId option:selected').text();

            $('.update-server').each(function (i, item) {
                var $this = $(this);
                var isCheck = $this.is(':checked');
                var id = $this.attr('sid');

                if (isCheck) {
                    connectionIds += id + ",";
                }
            });

            if (connectionIds == "") {
                alert("Select connection");
                return;
            }

            $.ajax({
                url: NAP.resolveClientUrl("ConnectionString/SwitchConnectionDbServer"),
                data: { connectionIds: connectionIds, toDbServerId: toDbServerId },
                type: 'POST',
                cache: false,
                beforeSend: function () {
                    NAP.loadingBox.show();
                },
                complete: function () {
                    NAP.loadingBox.close();
                },
                success: function (result) {
                    if (result.IsSuccessful) {
                        NAP.headlineMsgBox.showInfo('Your changes were saved!', { closeBtn: false });
                        NAP.redirect('ConnectionString/Connection?DbServerId=' + toDbServerId);
                    }
                    else {
                        NAP.headlineMsgBox.showInfo("The database server " + dbServerName + " had no database : " + result.Message, { closeBtn: true });
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

    Connection.grid = $('.grid');

    Connection.grid.on('click', 'input[type=checkbox][did]', function (e) {
        var $this = $(this);
        var id = $this.attr('did');
        var name = $this.attr('dname');
        var active = $this.is(':checked'),
			action = active ? 'activate' : 'deactivate',
             msg = "Are you sure you want to " + action + " '" + name + "' ?";

        $this.attr('checked', !active).change();

        NAP.headlineMsgBox.confirm(msg, function () {

            $this.attr('checked', active).change();

            Connection.UpdateStatusConnection(id, active);
        });
    });

    $('#DbServerId').change(function () {
        var aliasName = $("#DbServerId").val();
        Connection.GetDatabaseByAliasName(aliasName);
    });

    $('#DatabaseId').change(function () {
        Connection.GenerateConnectionName();
    });

    $('#LoginName').keyup(function () {
        Connection.GenerateConnectionName();
    });

    Connection.grid.on('click', 'input[type=checkbox][name=checkall]', function (e) {
        var $this = $(this);
        var isCheck = $this.is(':checked');
        if (isCheck) {
            $('.update-server').attr('checked', 'checked');
        }
        else {
            $('.update-server').removeAttr('checked');
        }
    });

    $('#btnSwitchServer').click(function () {
        Connection.SwitchDbServer();
    });

});
