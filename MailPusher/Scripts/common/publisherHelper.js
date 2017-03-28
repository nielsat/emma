var publisherFilter = {
    settings : {
        onPublisherChanged: function () { },
        getNextUrl: '',
        getPrevUrl: '',
        getFirstUrl: '',
        extendRequestParams: function () { }
    },
    GetFilteredData: function (url) {
        var self = this;
        var requestData = GetFliterRequestData();
        if (requestData) {
            $.get(url, requestData).done(function (data) {
                self.settings.onPublisherChanged(data);
            });
        }
        function GetFliterRequestData() {
            var countryCode = GetCountryCode($("#countryList").val());
            var requestParams = { 'countryCode': countryCode };
            return self.settings.extendRequestParams(requestParams);
        }
    },
    initButtons: function () {
        var self = this;
        $(".getNext").click(function () {
            self.GetFilteredData(self.settings.getNextUrl);
        });
        $(".getPrev").click(function () {
            self.GetFilteredData(self.settings.getPrevUrl);
        });
        $(".getFirst").click(function () {
            self.GetFilteredData(self.settings.getFirstUrl);
        });
        initCountryTypeahead("countryList");
    },
    init: function (inSettings) {
        var self = this;
        self.settings = inSettings;
        self.initButtons();
    },
    getFirst: function () {
        var self = this;
        self.GetFilteredData(this.settings.getFirstUrl);
    },
    hide: function () {
        $("#publisherEmailFilterPanel").hide();
    }
};