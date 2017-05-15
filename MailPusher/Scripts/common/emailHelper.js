var singleEmailHelper = {
    emailBodyContainerId: "emailBody",
    singleEmailFilterContainerId: 'singleEmailFilterContainer',
    emailDataTitleId: 'emailDataTitle',
    emailDataSubjectId:'emailDataSubject',
    resizeIsListening :false,
    init: function (email, emailBodyURL, leftContainerId, headerId) {
        var self = this;
        self.updateControls(email);
        $("#emailData").show();
        $.get(emailBodyURL, { emailId: email.id }).done(function (data) {
            var iframeParent = $("#" + self.emailBodyContainerId).parent();
            $("#" + self.emailBodyContainerId).remove();
            iframeParent.append("<iframe class='col-md-12' id='" + self.emailBodyContainerId + "' frameborder='0'></iframe>");
            $("#" + self.emailBodyContainerId).contents().find("html").html(data);
            $("#" + self.emailBodyContainerId)[0].contentWindow.scrollTo(0, 0);
            if (!leftContainerId || !headerId) {
                self.setBodyHeight(self.emailBodyContainerId, 200);
                self.updateBodyHeight(self.emailBodyContainerId);
                setTimeout(function () { self.updateBodyHeight(self.emailBodyContainerId); }, 1000);
            } else {
                self.changeStaticPosition(leftContainerId, headerId);
                if (!self.resizeIsListening) {
                    self.resizeIsListening = true;
                    $(window).resize(function () {
                        self.changeStaticPosition(leftContainerId, headerId);
                    });
                }
            }
        });
    },
    updateBodyHeight: function (id) {
        this.setBodyHeight(id, $("#" + id).contents().find("html").height())
    },
    setBodyHeight: function (id, value) {
        $("#" + id).height(value);
    },
    hide: function () {
        $("#emailData").hide();
    },
    hideButtons: function () {
        $("#emailDataButtons").hide();
    },
    map: function (data) {
        return {
            "senderAddress": data.SenderAddress,
            "receivedGMT": data.ReceivedGMT,
            "subjectLine": data.SubjectLine,
            "id": data.ID
        };
    },
    updateControls: function (email) {
        $("#emailFrom").html(!email.senderAddress ? '' : email.senderAddress.replace(/"/g, ''));
        $("#emailReceivedGMT").html(email.receivedGMT);
        $("#emailSubject").html(email.subjectLine);
        $("#singleEmailPermalink").attr("href", $("#singleEmailPermalink").attr("data-baseaddress") + "?id=" + email.id);
    },
    hideData: function () {
        $(".singleEmailDataContainer").hide();
    },
    showData: function () {
        $(".singleEmailDataContainer").show();
    },
    changeStaticPosition: function (leftContainerId, headerId) {
        var self = this;
        var headerHeight = $("#" + headerId).height() + 12;
        var leftContainerWidth = $("#" + leftContainerId).width() + 20;

        var newHeight = $(window).height() - headerHeight - $("#" + self.emailDataTitleId).height() - $("#" + self.emailDataSubjectId).height() - 65;
        var newWidth = $(window).width() - leftContainerWidth - 10;

        self.setBodyHeight(self.emailBodyContainerId, newHeight);
        $("#emailData").css({ 'position': 'fixed', 'top': headerHeight, 'left': leftContainerWidth, 'width': newWidth });
    }
}