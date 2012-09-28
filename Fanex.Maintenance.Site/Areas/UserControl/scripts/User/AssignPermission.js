var AssignUserPermission = function ($) {

    return {
        updateForm: null,
        grid: null,
        updateParent: function (type) {
            var $this = $(this),
                pid = $this.attr('pid');

            if (pid && pid.length > 0) {
                var thisIsChecked = $this.is(':checked'),
                    parent = $('#func' + type + pid);

                if (thisIsChecked && !parent.is(':checked')) {
                    parent.attr('checked', true);
                }

                if (!thisIsChecked) {
                    var siblings = $('[name=func' + type + '][pid=' + parent.val() + ']:checked');

                    if (siblings.length == 0) {
                        parent.attr('checked', false);

                        if (type == 'Read') {
                            parent.parents('tr').find('[name=funcHost]').attr('checked', false);
                        }
                    }
                }

                AssignUserPermission.updateParent.call(parent.get(0), type);
            }
        }
    };

} (jQuery);

$(document).ready(function () {
    Layout.body.children('.content').addClass('no-scale');

    AssignUserPermission.grid = $('.grid:first');
    AssignUserPermission.updateForm = $('form:last');

    AssignUserPermission.updateForm.submit(function () {
        AssignUserPermission.grid.find('tr').each(function () {
            var $this = $(this),
                chkHost = $this.find('[name=funcHost]'),
                chkRead = $this.find('[name=funcRead]');

            if (chkHost.is(':checked')) {
                chkRead.removeAttr('name');
            } else {
                chkHost.removeAttr('name');
            }
        });
    });

    AssignUserPermission.grid.find('[type=checkbox]').change(function () {
        var $this = $(this),
            thisIsChecked = $this.is(':checked'),
            row = $this.parents('tr'),
            table = row.parents('table');

        if ($this.attr('name') == 'funcHost') {
            // host checked -> read checked
            if (thisIsChecked) {
                AssignUserPermission.updateParent.call(row.find('[name=funcRead]').attr('checked', true).get(0), 'Read');
                table.find('[name=funcRead][pid=' + $this.val() + ']').attr('checked', true).change();
            }

            // parent changed -> children changed
            table.find('[name=funcHost][pid=' + $this.val() + ']').attr('checked', thisIsChecked).change();

            // child checked -> parent checked
            // last child unchecked -> parent unchecked
            AssignUserPermission.updateParent.call($this.get(0), 'Host');
        } else { // funcRead
            // read unchecked -> host unchecked
            if (!thisIsChecked) {
                AssignUserPermission.updateParent.call(row.find('[name=funcHost]').attr('checked', false).get(0), 'Host');
            }

            // parent changed -> children changed
            table.find('[name=funcRead][pid=' + $this.val() + ']').attr('checked', thisIsChecked).change();

            // child checked -> parent checked
            // last child unchecked -> parent unchecked
            AssignUserPermission.updateParent.call($this.get(0), 'Read');
        }
    });

    var hasAssignPermission = $('#HasAssignPermission').val().toLowerCase();
    if (hasAssignPermission == "false") {
        $('input[type=checkbox]').attr('disabled', 'disabled');
        
        $('#btnAssignPermission').hide();
    }
});