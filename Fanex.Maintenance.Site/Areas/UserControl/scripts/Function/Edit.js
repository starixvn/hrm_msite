var EditFunction = function ($) {

	return {
		form: null,
		parentId: null,
		parentName: null
	};

} (jQuery);

$(document).ready(function () {
	EditFunction.form = $('form:first');
	EditFunction.parentId = $('#ParentId');
	EditFunction.parentName = $('#ParentName');

	EditFunction.form.submit(function () {
		var $this = $(this),
			parentId = EditFunction.parentId.val();

		$this.find('input[type=submit]').focus();

		if (parentId.length > 0) {
			EditFunction.parentName.val(EditFunction.parentId.find("option[value=" + parentId + "]").text());
		}
	});
});