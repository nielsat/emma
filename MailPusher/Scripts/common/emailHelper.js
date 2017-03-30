function initSingleEmail(email, emailBodyURL) {
    $("#emailFrom").html(email.senderAddress);
    $("#emailReceivedGMT").html(email.receivedGMT);
    $("#emailSubject").html(email.subjectLine);
    $.get(emailBodyURL, { emailId: email.id }).done(function (data) {
        $("#emailBody").contents().find("html").html(data);
        $("#emailBody").height($("#emailBody").contents().find("html").height());
    });
    $("#emailData").show();
}

function hideSingleEmail() {
    $("#emailData").hide();
}