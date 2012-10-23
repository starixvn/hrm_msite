var Index = function ($) {

    return {
        type: null,
        server: null,
        jobName: null,

        InitData: function () {
            Index.type = $('#type').val();
            Index.server = $('#server').val().replaceAll('_', '.');
            Index.jobName = $('#jobname').val();
        },

        GotoLogDetail: function (type, server, jobName) {
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
        }
    };

} (jQuery);

$(document).ready(function () {
    Index.InitData();
    Index.GotoLogDetail(Index.type, Index.server, Index.jobName);
});
