var emailBodyContainerId = "emailBody";
function initSingleEmail(email, emailBodyURL) {
    $("#emailFrom").html(email.senderAddress);
    $("#emailReceivedGMT").html(email.receivedGMT);
    $("#emailSubject").html(email.subjectLine);
    $.get(emailBodyURL, { emailId: email.id }).done(function (data) {
        setIframeHeight(emailBodyContainerId, 200);
        $("#" + emailBodyContainerId).contents().find("html").html(data);
        updateIframeHeight(emailBodyContainerId);
        setTimeout(function () { updateIframeHeight(emailBodyContainerId); }, 1000);
    });
    $("#emailData").show();
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