function initSingleEmail(email, emailBodyURL) {
    $("#emailFrom").html(email.senderAddress);
    $("#emailReceivedGMT").html(email.receivedGMT);
    $("#emailSubject").html(email.subjectLine);
    $.get(emailBodyURL, { emailId: email.id }).done(function (data) {
        $("#emailBody").contents().find("html").html(data);
        updateIframeHeight();
        setTimeout( function () { updateIframeHeight(); }, 1000);
    });
    $("#emailData").show();
}

function updateIframeHeight()
{
    $("#emailBody").height($("#emailBody").contents().find("html").height());
}

function hideSingleEmail() {
    $("#emailData").hide();
}