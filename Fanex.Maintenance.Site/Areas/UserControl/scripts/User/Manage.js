var ManageUser = function ($) {

	return {
		grid: null,
		siteId: null,
		GetUsers: function (username, groupId, siteId) {
			var url = 'Index';
			var data = {
				username: username,
				groupId: groupId,
				siteId: siteId
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
				},
				error: function (xhr, status, err) {
				}
			});
		}
	};

} (jQuery);

$(document).ready(function () {
	ManageUser.grid = $('.grid');
	ManageUser.siteId = $('#SiteId');

	ManageUser.grid.find('.lnk-user-info').tipsy({ gravity: 'w' });

	ManageUser.grid.on('click', '.lnk-user-info', function () {
		var $this = $(this),
			userId = $this.attr('uid'),
			username = $this.text();

		$.ajax({
			url: NAP.resolveClientUrl("User/UserInfo/" + userId),
			type: 'POST',
			dataType: 'html',
			cache: false,
			success: function (result) {
				if (result.length > 0) {
					NAP.popup("Details of '" + username + "'", result);
				}
			},
			error: function (xhr, status, err) {
				NAP.handleAjaxError(xhr, status, err);
			}
		});
	});

	// #region actions dropdownlist
	var event = NAP.agentIsOldIe || NAP.agentIsApple ? 'click' : 'hover';

	$('.action-wrp').bind(event, function () {
		var $this = $(this),
			dropdown = $this.find('.actions');

		if (dropdown.css('visibility') == 'hidden') {
			$('.actions').css({ visibility: 'hidden', opacity: 0 });
			$this.find('.actions').css({ visibility: 'visible', opacity: 1 });
		} else {
			$this.find('.actions').css({ visibility: 'hidden', opacity: 0 });
		}
	});
	// #endregion
});