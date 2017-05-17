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

var emailListHelper = {
    settings: {
        gridSelector: '',
        gridContainerSelector:'',
        gridUrl: '',
        permalinkEmalUrl: '',
        getEmailBodyUrl:'',
        gridAddCustomGetParams: function (d) { },
        showPublisherColumn: false,
        publisherInfoContainerId: '',
        publisherEmailsContainerSelector: '',
        emailMinDateId: '',
        emailMaxDateId: '',
        gridFilterSelector: '',
        emailPermalinkClass: 'emailPermalink',
        emailWasNotClicked:true
    },
    init: function () {
        var self = this;
        $(self.settings.gridSelector).dataTable({
            serverSide: true,
            ajax:{
                url:self.settings.gridUrl,
                data: function ( d ) {
                    self.settings.gridAddCustomGetParams(d);
                }
            },
            columns: [
                {
                    name: 'publisherName',
                    data: 'publisherName',
                    title: "From",
                    sortable: true,
                    searchable: true,
                    visible: self.settings.showPublisherColumn,
                },
                {
                    name: 'subjectLine',
                    data: 'subjectLine',
                    title: "Subject",
                    sortable: true,
                    searchable: true
                },
                {
                    name: 'id',
                    data: "id",
                    title: "ID",
                    sortable: true,
                    searchable: false,
                    visible: false,
                },
                {
                    name: 'senderAddress',
                    data: "senderAddress",
                    title: "Sender Address",
                    sortable: true,
                    searchable: false,
                    visible: false,
                },
                {
                    name: 'copy',
                    data: "copy",
                    title: "Copy",
                    sortable: true,
                    searchable: false,
                    visible: false,
                },
                {
                    name: 'receivedGMT',
                    data: "receivedGMT",
                    title: "Sent",
                    sortable: true,
                    searchable: false,
                    width:260
                },
                {
                    name: 'shortReceivedGMT',
                    data: "shortReceivedGMT",
                    title: "Sent",
                    sortable: true,
                    searchable: false,
                    width:220,
                    visible: false,
                },
                {
                    name: 'btns',
                    data: null,
                    title: '',
                    sortable: false,
                    searchable: false,
                    defaultContent: "<div class='btnTD'></div><a class='"+self.settings.emailPermalinkClass+"' href='#' title='open in new window'><i class='fa fa-link'></i> </a>",
                }
            ],
            "iDisplayLength": 100,
            "bLengthChange": false,
            "oLanguage": {
                "sSearch": "Search emails by subject line:&nbsp;&nbsp;"
            },
            "order": []
        });

        $(self.settings.gridSelector).on('draw.dt', function (e, settings) {
            self.initButtons();
            self.changeGridStyle(settings);
            if (self.settings.emailWasNotClicked && $(self.settings.gridSelector + " tr").length > 1) {
                $($(self.settings.gridSelector + " tr")[1]).find("td")[0].click();
                self.settings.emailWasNotClicked = false;
            };
        });
    },
    changeGridStyle: function (settings) {
        var self = this;
        gridHelper.updateContainerHeight(self.settings.gridContainerSelector, self.settings.gridSelector, 200);
        gridHelper.changeTdCursorToPointer(self.settings.gridSelector, 'noClass');
        gridHelper.hidePginationIfOnePage(self.settings.gridSelector, settings);
    },
    initButtons: function () {
        var self = this;
        var table = $(self.settings.gridSelector).DataTable();
        $("." + self.settings.emailPermalinkClass).click(function () {
            var table = $(self.settings.gridSelector).DataTable();
            window.open(self.settings.permalinkEmalUrl + '?id=' + table.row($(this).parents('tr')).data().id, '_blank')
        });

        $(self.settings.gridSelector + " td").click(function () {
            if (!$(this).find("div.btnTD").length) {
                //INIT
                var rowData = table.row(this).data();
                singleEmailHelper.init(rowData, self.settings.getEmailBodyUrl, self.settings.publisherInfoContainerId, 'topNavMenu');

                //STYLE
                $(".selectedTdItem").removeClass("selectedTdItem");
                $(self.settings.publisherEmailsContainerSelector).removeClass("col-md-12").addClass("col-md-5");
                $(self.settings.gridContainerSelector).height($(self.settings.gridSelector).height() + 200);
                var parentTr = $(this).parent("tr");
                parentTr.addClass('selectedTdItem');
                $(".dataTables_filter .form-control").addClass('full-width-important');
                table.column("btns:name").visible(false);
                table.column("receivedGMT:name").visible(false);
                table.column("shortReceivedGMT:name").visible(true);

                //FOCUS
                if (!Utils.isElementInView(parentTr)) {
                    var rowpos = $(parentTr).offset();
                    $(window).scrollTop(rowpos.top - 200);
                }
            }
        });
    },
    addCustomFilters: function () {
        var self = this;
        if (!!$("#"+self.settings.emailMinDateId).length) {
            return;
        };
        var emailRange = '<label > From: <input type="text" id="'+self.settings.emailMaxDateId +'" class="form-control input-sm"/></label> &nbsp;';
        emailRange += '<label > To: <input type="text" id="'+self.settings.emailMaxDateId+'" class="form-control input-sm"/></label> &nbsp;';
        $(self.settings.gridFilterSelector).prepend(emailRange);
        $("#" + self.settings.emailMinDateId).datepicker();
        $("#" + self.settings.emailMaxDateId).datepicker();
        $("#" + self.settings.emailMinDateId).change(function () {
            $(self.settings.gridSelector).DataTable().draw();
        });
        $("#" + self.settings.emailMaxDateId).change(function () {
            $(self.settings.gridSelector).DataTable().draw();
        });
    }
}