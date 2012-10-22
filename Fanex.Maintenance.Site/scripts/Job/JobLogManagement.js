
var JobLogManagement = function ($) {

    return {
        grid: null,
        buttonImage: null,

        InitData: function () {
            JobLogManagement.buttonImage = $('#imagePath').val();
            JobLogManagement.grid = $('.grid:first');
        },

        InitDatetimePicker: function () {

            $( "#From" ).datepicker({
			        defaultDate: "+1w",
			        changeMonth: true,
			        numberOfMonths: 1,
                    dateFormat: "M dd,yy",
                    showOn: "button",
                    buttonImage: JobLogManagement.buttonImage,
                    buttonImageOnly: true,
			        onSelect: function( selectedDate ) {
				        $( "#To" ).datepicker( "option", "minDate", selectedDate );
                        $( "#To" ).val($(this).val());
			        }
		        });
		    $( "#To" ).datepicker({
			    defaultDate: "+1w",
			    changeMonth: true,
			    numberOfMonths: 1,
                dateFormat: "M dd,yy",
                showOn: "button",
                buttonImage: JobLogManagement.buttonImage,
                buttonImageOnly: true,
			    onSelect: function( selectedDate ) {
				    $( "#From" ).datepicker( "option", "maxDate", selectedDate );
                    $( "#From" ).val($(this).val());
			    }
		    });
        },
         StartJob: function (server, jobName, stepId, stepName) {
            var stepMessage = "";
            if(stepId != '0')
            {
                stepMessage = " at Step " + stepName;
            }
            $.ajax({
                url: NAP.resolveClientUrl("JobManagement/StartJob"),
                data: { 
                        server: server, 
                        jobName: jobName, 
                        stepName: stepId != 0 ? stepName : null 
                        },
                type: 'POST',
                dataType: 'JSON',
                cache: false,
                beforeSend: function () {
                    NAP.loadingBox.show();
                },
                complete: function () {
                    NAP.loadingBox.close();
                },
                success: function (result) {
                    if (result.Status.IsSuccessful) {
                        NAP.headlineMsgBox.showInfo("Start Job " + jobName + stepMessage + " Successfully !", { closeBtn: false });
                    }
                    else {
                        NAP.headlineMsgBox.showInfo("Start Job " + jobName + stepMessage + " Fail !<br />Message: " + result.Status.Message, { closeBtn: false });
                    }
                },
                error: function (xhr, status, err) {
                    NAP.handleAjaxError(xhr, status, err);
                }
            });
        },
    };

} (jQuery);

$(document).ready(function () {
    JobLogManagement.InitData();
    JobLogManagement.InitDatetimePicker();

    $('#Job_Name').chosen();

    // #region actions dropdownlist
    var event = NAP.agentIsOldIe || NAP.agentIsApple ? 'click' : 'hover';

    $('.action-wrp').bind(event, function () {
        var $this = $(this),
			dropdown = $this.find('.actions');

        if (dropdown.css('visibility') == 'hidden') {
            $('.actions').css({ visibility: 'hidden', opacity: 0 });
            $this.find('.actions').css({ visibility: 'visible', opacity: 1 });
        } else {
            $this.find('.actions').css({ visibility: 'hidden', opacity: 0 });
        }
    });

    $('.stepOutcome').live('click', function () {
        var $this = $(this);
        var instanceId = $($this).attr('instanceid');
        var $trThis = $this.parents('tr');

        if ($this.hasClass("expand")) {
            $this.removeClass("expand");
            $this.addClass("collapse");
        }
        else {
            $this.addClass("expand");
            $this.removeClass("collapse");
        }

        var trNext = $trThis.next();
        var isLoop = true;
        while (isLoop) {
            trNext.toggle();
            trNext = trNext.next();            
            if (trNext.length>0) {
                if (trNext.attr('id') != undefined) {
                    isLoop = false;
                }
            }
            else {
                isLoop = false;
            }
            
        }

    });
    // #endregion
});