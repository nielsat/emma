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
    if (countryCode == '') {
        alert('Please select country!');
        return null;
    }
    var requestParams = { 'countryCode': countryCode};
    return filterSettings.extendRequestParams(requestParams);
    //{ 'countryCode': countryCode, 'publisherId': publisherId, 'status': filterSettings.defaultFilterStatus };
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

function HidePublisherFilter() {
    $("#publisherEmailFilterPanel").hide();
}