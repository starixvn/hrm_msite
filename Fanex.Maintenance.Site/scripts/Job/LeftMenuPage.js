
var LeftMenuPage = function ($) {

    return {
    };

} (jQuery);


// Proxy created on the fly
var notitficationHub = $.connection.notificationHub;

// Declare a function on the chat hub so the server can invoke it
notitficationHub.addMessage = function (message) {
    alert(message);
};

notitficationHub.gotMessage = function (notifyInfoes) {
    for (var i = 0; i < notifyInfoes.length; i++) {
        var notifyInfo = notifyInfoes[i];
        var linkErrorPage = "";
        if (notifyInfo.Type == 2) {
            linkErrorPage = NAP.resolveClientUrl("JobManagement/JobLogManagement?server=" + notifyInfo.ServerIp.replaceAll('_', '.') + "&job=" + notifyInfo.JobName + "&top=1");
        }
        else {
            linkErrorPage = NAP.resolveClientUrl("JobManagement/AgentLogManagement?server=" + server.replaceAll('_', '.') + "&top=1");
        }

        var message = notifyInfo.Type == 1 ? "Sql Agent Error" : "Job Error";
        message += "<br/>" + notifyInfo.Message;
        //                $('span.' + notifyInfo.ServerIp).addClass("notifyErrorServer");
        $('span.' + notifyInfo.ServerIp).attr("style", "color:red;background-position:0px 0px; ");

        $('span.' + notifyInfo.ServerIp + notifyInfo.Type).addClass("notifyError").attr("linkerrorpage", linkErrorPage);
        //$('span.' + notifyInfo.ServerIp + notifyInfo.Type).remove();
        $('span.' + notifyInfo.ServerIp + notifyInfo.Type).qtip({
            content: message,
            position:
                {
                    corner: {
                        tooltip: "topRight", // Use the corner...
                        target: "bottomRight" // ...and opposite corner
                    }
                },
            hide: {
                fixed: true // Make it fixed so it can be hovered over
            },
            style: {
                border: {
                    width: 2,
                    radius: 4
                },
                color:'#000',
                background: '#fff',
                width: 150,
                padding: 5,
                textAlign: 'left',
                tip: true, // Give it a speech bubble tip with automatic corner detection
                name: 'red' // Style it according to the preset 'cream' style
            }
        })
    }
}

$(document).ready(function () {
    $.connection.hub.start(function () {
    });

    $('.removedErrorIcon').click(function () {
        $(this).removeClass("notifyError");
        $(this).removeAttr("style");
        $(this).parent().parent().prev().removeClass("notifyError");
        parent.$("#centerFr").attr("src", $(this).attr("linkerrorpage"));
    });

    $('.agentlog').click(function () {
        $(this).next().removeClass("notifyError");
        $(this).parent().parent().prev().removeClass("notifyError");

    });

    $('.job').click(function () {
        $(this).next().removeClass("notifyError");
        $(this).parent().parent().prev().removeClass("notifyError");
    });
    $("#navigation").treeview({
        animated: "fast"
    });

    $('.rootServer').live('click', function () {
        var liChildren = $(this).parent().find("ul");
        liChildren.toggle("fast");
    });

    $('.menu').live('click', function () {
        $('.menu').removeClass("selected");
        $('.navChild').removeClass("selected");
        $(this).addClass("selected");
        parent.NAP.loadingBox.show();
    });

    $('.navChild').live('click', function () {
        $('.navChild').removeClass("selected");
        $('.menu').removeClass("selected");
        $(this).addClass("selected");
        parent.NAP.loadingBox.show();
    });

    $('a.imgcollapse').click(function () {
        parent.$('#centerFr').contents().find('a.navigater').show();
        parent.$('div.leftcontent').css("width", "0%");
        parent.$('div.rightcontent').css("width", "100%");
        parent.$("#collapsepanel").val("1");
    });

    var go = parent.$("#leftPanel").attr("go");
    if (go != '') {
        $('.menu').removeClass("selected");
        if (go == '0') {
            $('.databaseNavi').addClass("selected");
        }
        if (go == '1') {
            $('.scheduleNavi').addClass("selected");
        }
        if (go == '2') {
            $('.jobNavi').addClass("selected");
        }
        if (go == '3') {
            $('.JobLogNavi').addClass("selected");
        }
    }
});