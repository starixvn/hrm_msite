/* File Created: October 17, 2012 */
// Proxy created on the fly
var notitficationHub = $.connection.notificationHub;

// Declare a function on the chat hub so the server can invoke it
notitficationHub.addMessage = function (message) {
    alert(message);
};

notitficationHub.gotMessage = function (notifyInfoes) {
    for (var i = 0; i < notifyInfoes.length; i++) {
        var notifyInfo = notifyInfoes[i];
        var message = "Server: " + notifyInfo.ServerIp + "<br/>"
                + (notifyInfo.Type == 1 ? "Sql Server Agent Error" : "Job Error") + "<br />"
                + "Occur at " + notifyInfo.OccurTime + "<br />"
                + "Message: " + notifyInfo.Message;

        $.notify({
            inline: true,
            html: '<p>' + message + '</p>'
        }, 20000);
    }
}


$(function () {
    $.connection.hub.start(function () {
    });
});