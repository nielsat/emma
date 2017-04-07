var emailBodyContainerId = "emailBody";
function initSingleEmail(email, emailBodyURL, staticHeight) {
    $("#emailFrom").html(email.senderAddress);
    $("#emailReceivedGMT").html(email.receivedGMT);
    $("#emailSubject").html(email.subjectLine);
    $("#emailData").show();
    $.get(emailBodyURL, { emailId: email.id }).done(function (data) {
        $("#" + emailBodyContainerId).contents().find("html").html(data);
        if (!staticHeight) {
            setIframeHeight(emailBodyContainerId, 200);
            updateIframeHeight(emailBodyContainerId);
            setTimeout(function () { updateIframeHeight(emailBodyContainerId); }, 1000);
        } else {
            var newHeight = staticHeight - $("#emailDataTitle").height() - $("#emailDataSubject").height() - 120;
            if ($("#" + emailBodyContainerId).height() != newHeight) {
                setIframeHeight(emailBodyContainerId, newHeight);
                var offset = $("#emailData").offset();
                var offsetTop = offset.top > $(".nav.navbar-nav").height()+12 ? offset.top : $(".nav.navbar-nav").height()+12;
                var width = $("#emailData").width();
                $("#emailData").css({ 'position': 'fixed', 'top': offsetTop, 'left': offset.left, 'width': width });
            }
        }
    });
}

function updateIframeHeight(id)
{
    setIframeHeight(id, $("#"+id).contents().find("html").height())
}

function setIframeHeight(id, value)
{
    $("#" + id).height(value);
}

function hideSingleEmail() {
    $("#emailData").hide();
}

function hideSingleEmailButtons() {
    $("#emailDataButtons").hide();
}