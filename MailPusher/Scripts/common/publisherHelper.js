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
        countryFilterContainerID:'',
        onCountryChanged: function (countryCode) { }
    },
    innerSettings: {
        countryFilterId:'countryListFilter',
    },
    init: function (inSettings) {
        var self = this;
        self.settings = inSettings;
        var initCountryFilter = function (settings) {
            $("#" + settings.countryFilterContainerID).append('<div class="row"><div class="col-md-3"><h2> Country:</h2></div><div class="col-md-9"><input type="text" data-provide="typeahead" id="' + self.innerSettings.countryFilterId + '" class="form-control col-md-7 col-xs-12"/></div></div>');
            initCountryTypeahead(self.innerSettings.countryFilterId);
            $("#" + self.innerSettings.countryFilterId).change(function () {
                self.countryChanged();
            });
        };
        initCountryFilter(self.settings);
    },
    countryChanged: function () {
        var self = this;
        var countryCode = GetCountryCode($("#" + self.innerSettings.countryFilterId).val());
        self.settings.onCountryChanged(countryCode);
    }
}