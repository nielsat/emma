var singleEmailHelper = {
    emailBodyContainerId: "emailBody",
    singleEmailFilterContainerId:'singleEmailFilterContainer',
    init: function (email, emailBodyURL, staticHeight) {
        var self = this;
        self.updateControls(email);
        $("#emailData").show();
        $.get(emailBodyURL, { emailId: email.id }).done(function (data) {
            $("#" + self.emailBodyContainerId).contents().find("html").html(data);
            $("#emailBody")[0].contentWindow.scrollTo(0, 0);
            if (!staticHeight) {
                self.setBodyHeight(self.emailBodyContainerId, 200);
                self.updateBodyHeight(self.emailBodyContainerId);
                setTimeout(function () { self.updateBodyHeight(self.emailBodyContainerId); }, 1000);
            } else {
                var newHeight = staticHeight - $("#emailDataTitle").height() - $("#emailDataSubject").height() - 120;
                if ($("#" + self.emailBodyContainerId).height() != newHeight) {
                    self.setBodyHeight(self.emailBodyContainerId, newHeight);
                    var offset = $("#emailData").offset();
                    var offsetTop = offset.top > $(".nav.navbar-nav").height() + 12 ? offset.top : $(".nav.navbar-nav").height() + 12;
                    var width = $("#emailData").width();
                    $("#emailData").css({ 'position': 'fixed', 'top': offsetTop, 'left': offset.left, 'width': width });
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
    }
}