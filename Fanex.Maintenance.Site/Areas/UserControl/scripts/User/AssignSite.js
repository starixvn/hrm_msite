var AssignSite = function ($) {

	return {
		GetUserSite: function (userId) {
			var url = NAP.resolveClientUrl('User/GetUserSite');
			var data = {
				userId: userId
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

					for (var i = 0; i < content.length; i++) {

						var siteId = content[i].Site.Id;

						var isActive = content[i].IsActive;

						var level = content[i].Level;

						if (isActive) {
							$('#chbSite_' + siteId).attr("checked", "checked");
						}
						else {
						}

						var exist = $('#ddlSiteLevel_' + siteId + ' option[value=' + level + ']').length !== 0;

						if (!exist && isActive) {
							var textLevel = "";
							if (level == 0) {
								textLevel = "User";
							}
							if (level == 50) {
								textLevel = "Site Admin";
							}
							if (level == 100) {
								textLevel = "Portal Admin";
							}
							var template = "<option value=" + level + ">" + textLevel + "</option>";
							//console.log(template);
							$('#ddlSiteLevel_' + siteId).append(template);
							$('#ddlSiteLevel_' + siteId).attr("disabled", "disabled");
							$('#chbSite_' + siteId).attr("disabled", "disabled");
						}

						$('#ddlSiteLevel_' + siteId).val(content[i].Level);
					}
				},
				error: function (xhr, status, err) {

				}
			});
		},

		AssignSiteToUser: function (userId) {
			var value = "";
			$('.chbSite').each(function () {
				var check = $(this).attr('checked');
				if (check == 'checked') {
					var siteId = $(this).attr('id').substring(8, $(this).attr('id').length);
					var level = $('#ddlSiteLevel_' + siteId).val();
					value += siteId + "," + level + ";";
				}
			});
			console.log(value);

			var url = NAP.resolveClientUrl('User/AssignSiteToUser');
			var data = {
				userId: userId,
				value: value
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
								}
							},
							width: 'auto',
							modal: true,
							resizable: false,
							closeOnEscape: false,
							open: function (event, ui) { $(".ui-dialog-titlebar-close").hide(); }
						});
					}

				},
				error: function (xhr, status, err) {

				}
			});
		},

		userSiteContainer: null,

		availableSiteContainer: null,

		dragOption: {
			stack: ".site-container .site",
			revert: "invalid",
			cursor: "move",
			containment: ".site-container"
		}
	};

} (jQuery);

$(document).ready(function () {
	Layout.body.children('.content').addClass('no-scale');

	AssignSite.userSiteContainer = $('.site-container td.user-site');
	AssignSite.availableSiteContainer = $('.site-container td.available-site');

	$('.site-container .site').draggable(AssignSite.dragOption);

	$('.site-container .site a').tipsy({ gravity: 'sw' }).click(function () {
		$('.tipsy').remove(); // work around for hiding tipsy
		var parent = $(this).parent();

		parent.fadeOut(function () {
			parent.appendTo(AssignSite.availableSiteContainer)
				.removeClass('active').fadeIn().draggable(AssignSite.dragOption);
		});
	});

	AssignSite.userSiteContainer.droppable({
		over: function () {
			$(this).addClass('over');
		},
		out: function () {
			$(this).removeClass('over');
		},
		drop: function (event, ui) {
			$(this).removeClass('over');

			ui.draggable.fadeOut(function () {
				ui.draggable.appendTo(AssignSite.userSiteContainer)
					.css({ left: 0, top: 0, cursor: 'default' })
					.addClass('active')
					.fadeIn().draggable("destroy");
			});
		}
	});

	/*var userId = $('#UserId').val();
	AssignSite.GetUserSite(userId);

	$('#btnAssignSite').bind('click', function () {
	AssignSite.AssignSiteToUser(userId);
	});

	var hasAssignSite = $('#HasAssignSite').val().toLowerCase();
	if (hasAssignSite == "false") {
	$('input[type=checkbox]').attr('disabled', 'disabled');
	$('select').attr('disabled', 'disabled');
	$('#btnAssignSite').hide();
	}*/
});