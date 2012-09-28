var EditMenuItem = function ($) {

	return {
		form: null,
		parentId: null,
		parentName: null
	};

} (jQuery);

$(document).ready(function () {
	EditMenuItem.form = $('form:first');
	EditMenuItem.parentId = $('#ParentId');
	EditMenuItem.parentName = $('#ParentName');

	EditMenuItem.form.submit(function () {
		var $this = $(this),
			parentId = EditMenuItem.parentId.val();

		$this.find('input[type=submit]').focus();

		if (parentId.length > 0) {
			EditMenuItem.parentName.val(EditMenuItem.parentId.find("option[value=" + parentId + "]").text());
		}
	});
});