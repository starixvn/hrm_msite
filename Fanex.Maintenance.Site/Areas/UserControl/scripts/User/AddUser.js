
var AddUser = function ($) {

    return {
        ShowGroup: function (btnEdit) {

            var userId = $(btnEdit).attr('id').substring(11, $(btnEdit).attr('id').length);
            $(btnEdit).hide();
            var btnAdd = "<input type='button' value='Add' id='btnAddUserId_" + userId + "'></input>";
            var btnCancel = "<input type='button' value='Cancel' id='btnCancelAddUserId_" + userId + "'></input>";
            $('#tdUserId_' + userId).append(btnAdd);
            $('#tdUserId_' + userId).append(btnCancel);

            $('#ddlGroupUserId_' + userId).show();

            $('#btnCancelAddUserId_' + userId).bind('click', function () {
                $('#btnAddUserId_' + userId).remove();
                $(this).remove();
                $(btnEdit).show();
                $('#ddlGroupUserId_' + userId).hide();
            });

            $('#btnAddUserId_' + userId).bind('click', function () {

                var url = 'AddUserToGroup';
                var data = {
                    userId: userId,
                    groupId: $('#ddlGroupUserId_' + userId).val()
                };

                $.ajax({
                    url: url,
                    type: 'POST',
                    data: data,
                    dataType: 'html',
                    cache: false,
                    async: false,
                    beforeSend: function (xhr) {
                        //msgBox.showLoading();
                    },
                    success: function (result) {
                        var content = eval(result);

                        if (content) {
                            $("#dialog-message").dialog({
                                buttons: {
                                    OK: function () {
                                        window.location.replace(NAP.resolveClientUrl('User/Manage'));
                                        //$('#dialog-message').hide();
                                    }
                                },
                                width: 'auto',
                                modal: true,
                                resizable: false,
                                closeOnEscape: false,
                                open: function (event, ui) { $(".ui-dialog-titlebar-close").hide(); }
                            });

                            $('#tdUserId_' + userId).remove();
                        }

                    },
                    error: function (xhr, status, err) {

                    }
                });
            });
        }
    };

} (jQuery);

$(document).ready(function () {
    $('.btn-edit').bind('click', function () {
        AddUser.ShowGroup($(this));
    });
});