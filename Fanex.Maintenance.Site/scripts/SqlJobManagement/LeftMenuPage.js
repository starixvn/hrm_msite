/* File Created: October 8, 2012 */
$(function () {
    $("#browser").treeview({
        animated: "medium"
    });
    $("#browser li span").hover(function () {
        $(this).addClass("hover");
    }, function () {
        $(this).removeClass("hover");
    })


});
var LeftMenuPage = function ($) {
    return {};
} (jQuery);

LeftMenuPage.onRedirect = function (urlRedirect) {
    parent.$("#centerFr").attr("src", urlRedirect);
};