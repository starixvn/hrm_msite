var ManageFunction = function ($) {

	return {
		grid: null,
	form: null
	};

} (jQuery);

$(document).ready(function () {
	ManageFunction.grid = $('.grid');
	ManageFunction.form = $('form:first');

	// #region tooltips
	$('.tip').tipsy({ gravity: 'se' });
	$('#headline h1 .lnk').tipsy({ gravity: 'w' });
	$('.grid input[type=checkbox][fid]').tipsy({ gravity: 's' });
	// #endregion

	// #region tree view/ grid view
	$('#btnGridView, #btnTreeView').bind('click', function () {
		var $this = $(this);

		if (!$this.hasClass('active')) {
			if (NAP.agentIsOldIe) {
				$('.grid-mode-only').toggle();
				$('.tree-mode-only').toggle();
			} else {
				$('.grid-mode-only').toggle('slow');
				$('.tree-mode-only').toggle('slow');
			}
		}

		$this.addClass('active').siblings().removeClass('active');
	});
	// #endregion

	// #region activate/deactivate functions
	ManageFunction.grid.on('mousedown', 'input[type=checkbox][fid]', function (e) {
		var $this = $(this),
			functionName = $this.attr('fname'),
			active = !$this.is(':checked'),
			action = active ? 'activate' : 'deactivate',
			msg = "Are you sure you want to " + action + " '" + functionName + "' ?";

		NAP.headlineMsgBox.confirm(msg, function () {
			var functionId = $this.attr('fid');

			$this.attr('checked', active).change();

			$.ajax({
				url: NAP.resolveClientUrl("Function/Activate" + "?id=" + functionId + "&active=" + active),
				type: 'POST',
				contentType: 'application/json',
				dataType: 'json',
				cache: false,
				beforeSend: function () {
					NAP.loadingBox.show();
				},
				complete: function () {
					NAP.loadingBox.close();
				},
				success: function (result) {
					if (result == 'OK') {
						var msg = active ? "Activation succeeded!" : "Deactivation succeeded!";

						NAP.headlineMsgBox.showInfo(msg, { closeBtn: false });
						if (active) {
							$this.attr('original-title', $this.attr('original-title').replace('Activate', 'Deactivate'));
						} else {
							$this.attr('original-title', $this.attr('original-title').replace('Deactivate', 'Activate'));
						}
					}
				},
				error: function (xhr, status, err) {
					NAP.handleAjaxError(xhr, status, err);
				}
			});
		}, { modal: true, closeBtn: false });
	});
	// #endregion

	// #region show users & groups popup
	$('.btn-user-info, .dd-user-info').click(function () {
		var $this = $(this),
			functionid = $this.attr('fid'),
			functionName = $this.attr('fname'),
			siteId = $('#SiteId').val();

		$.ajax({
			url: NAP.resolveClientUrl("Function/GetAssigneeList?siteid=" + siteId + "&functionId=" + functionid),
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
					NAP.popup("Privileged groups and users of '" + functionName + "'", result);
				}
			},
			error: function (xhr, status, err) {
				NAP.handleAjaxError(xhr, status, err);
			}
		});

		return false;
	});
	// #endregion

	ManageFunction.form.submit(function () {
		NAP.loadingBox.show();
	});
});