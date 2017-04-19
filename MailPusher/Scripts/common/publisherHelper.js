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

var injectedCountryFilter = {
    settings: {
        countryFilterContainerID: '',
        selectedCountryCode:'',
        onCountryChanged: function (countryCode) { }
    },
    innerSettings: {
        countryFilterId:'countryListFilter',
    },
    init: function (inSettings) {
        var self = this;
        self.settings = inSettings;
        var initCountryFilter = function (settings) {
            $("#" + settings.countryFilterContainerID).removeClass("hidden");
            $("#" + settings.countryFilterContainerID).append('<div class="row"><div class="col-md-8"><h2> Show only publishers from the following country:</h2></div><div class="col-md-4"><select id="' + self.innerSettings.countryFilterId + '" class="form-control"/></div></div>');
            initShortCountrySelect(self.innerSettings.countryFilterId, self.settings.selectedCountryCode);
            $("#" + self.innerSettings.countryFilterId).change(function () {
                self.countryChanged();
            });
        };
        initCountryFilter(self.settings);
    },
    countryChanged: function () {
        var self = this;
        var countryCode = $("#" + self.innerSettings.countryFilterId).val();
        self.settings.onCountryChanged(countryCode);
    }
}