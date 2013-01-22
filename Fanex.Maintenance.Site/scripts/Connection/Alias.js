var Alias = function ($) {

    return {
        ShowNumOfSite: function (alaisId, aliasName) {
            $.ajax({
                url: NAP.resolveClientUrl("ConnectionString/GetSiteByAlias/" + alaisId),
                type: 'GET',
                cache: false,
                beforeSend: function () {
                    NAP.loadingBox.show();
                },
                complete: function () {
                    NAP.loadingBox.close();
                },
                success: function (result) {
                    NAP.popup("Site of alias '" + aliasName + "'", result);
                },
                error: function (xhr, status, err) {
                    NAP.handleAjaxError(xhr, status, err);
                }
            });
        }
    };

} (jQuery);

$(document).ready(function () {
    $('.numofsite').click(function () {
        var $this = $(this);
        var aliasId = $this.attr('aid');
        var aliasName = $this.attr('aname');

        Alias.ShowNumOfSite(aliasId, aliasName);
    });
});
