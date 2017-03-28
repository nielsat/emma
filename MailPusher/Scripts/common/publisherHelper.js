var filterSettings = {
    onPublisherChanged: function () { },
    getNextUrl: '',
    getPrevUrl: '',
    getFirstUrl: '',
    extendRequestParams: function () { }
};

function InitPublisherFilter(settings) {
    filterSettings = settings;
    InitFilterButtons();
}

function InitFilterButtons() {
    $(".getNext").click(function () {
        GetFilteredData(filterSettings.getNextUrl);
    });
    $(".getPrev").click(function () {
        GetFilteredData(filterSettings.getPrevUrl);
    });
    $(".getFirst").click(function () {
        GetFilteredData(filterSettings.getFirstUrl);
    });
    initCountryTypeahead("countryList");
}

function GetFilteredData(url) {
    var requestData = GetFliterRequestData();
    if (requestData) {
        $.get(url, requestData).done(function (data) {
            filterSettings.onPublisherChanged(data);
        });
    }
}

function GetFliterRequestData() {
    var countryCode = GetCountryCode($("#countryList").val());
    var requestParams = { 'countryCode': countryCode };
    return filterSettings.extendRequestParams(requestParams);
}

function GetFirstByFilter() {
    GetFilteredData(filterSettings.getFirstUrl);
}

function HidePublisherFilter() {
    $("#publisherEmailFilterPanel").hide();
}