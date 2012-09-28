var AddNewMenuItem = function ($) {

	return {
		form: null,
		siteId: null,
		parentId: null,
		parentName: null,

		getFunctionBySite: function (siteId) {
			var url = 'ListFunctionBySite?siteId=' + siteId;

			$.ajax({
				url: url,
				type: 'POST',
				dataType: 'html',
				cache: false,
				async: false,
				success: function (result) {
					var content = eval(result);

					AddNewMenuItem.functionId.html('');

					if (content.length > 0) {
						AddNewMenuItem.functionId.attr('disabled', false);
						AddNewMenuItem.functionId.append($('#DropdownListTpl').render(content));
					}
					else {
						AddNewMenuItem.functionId.attr('disabled', true);
					}
				},
				error: function (xhr, status, err) {
					NAP.handleAjaxError(xhr, status, err);
				}
			});
		},

		getMenuBySite: function (siteId) {
			var url = 'ListMenuItemBySite?siteId=' + siteId;

			$.ajax({
				url: url,
				type: 'POST',
				dataType: 'html',
				cache: false,
				async: false,
				success: function (result) {
					var content = eval(result);

					AddNewMenuItem.parentId.html('');

					if (content.length > 0) {
						AddNewMenuItem.parentId.attr('disabled', false);
						AddNewMenuItem.parentId.append($('#DropdownListTpl').render(content));
					}
					else {
						AddNewMenuItem.parentId.attr('disabled', true);
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
	AddNewMenuItem.form = $('form:first');
	AddNewMenuItem.siteId = $('#SiteId');
	AddNewMenuItem.parentId = $('#ParentId');
	AddNewMenuItem.parentName = $('#ParentName');
	AddNewMenuItem.functionId = $('#FunctionId');

	AddNewMenuItem.siteId.change(function () {
		AddNewMenuItem.getFunctionBySite($(this).val());
		AddNewMenuItem.getMenuBySite($(this).val());
	});

	AddNewMenuItem.form.submit(function () {
		var $this = $(this),
		parentId = AddNewMenuItem.parentId.val();

		$this.find('input[type=submit]').focus();

		if (parentId.length > 0) {
			AddNewMenuItem.parentName.val(AddNewMenuItem.parentId.find("option[value=" + parentId + "]").text());
		}
	});
});