
var Index = function ($) {

    return {

    };

} (jQuery);

$(document).ready(function () {

    var type = $('#type').val();
    var server = $('#server').val().replaceAll('_', '.');
    var jobName = $('#jobname').val();

    if (type != '' && server != '') {
        if (type == '2') {
            document.getElementById('centerFr').src = NAP.resolveClientUrl("JobManagement/JobLogManagement?server=" + server + "&job=" + jobName + "&top=1");
        }
        else {
            document.getElementById('centerFr').src = NAP.resolveClientUrl("JobManagement/AgentLogManagement?server=" + server + "&top=1");
        }
    }
    else {
        document.getElementById('centerFr').src = NAP.resolveClientUrl("JobManagement/ServerManagement");
    }
});