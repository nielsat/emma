var getUrlParameter = function getUrlParameter(sParam) {
    var sPageURL = decodeURIComponent(window.location.search.substring(1)),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : sParameterName[1];
        }
    }
};

function FixNavigation() {
    var currentPathName = location.pathname+location.search;
    $("a[href='" + currentPathName + "']").addClass("current-page");
}

function GetURLParamPublisherStatuses() {
    return getUrlParameter('publisherStatuses');
}

function GetURLParamIsPotentiallyCancelled() {
    return getUrlParameter('isPotentiallyCancelled')
}

function IsPotentiallyCancelled() {
    var urlParameter = GetURLParamIsPotentiallyCancelled();
    return !!urlParameter;
}

Utils.prototype = {
    constructor: Utils,
    isElementInView: function (element, fullyInView) {
        var pageTop = $(window).scrollTop();
        var pageBottom = pageTop + $(window).height();
        var elementTop = $(element).offset().top;
        var elementBottom = elementTop + $(element).height();

        if (fullyInView === true) {
            return ((pageTop < elementTop) && (pageBottom > elementBottom));
        } else {
            return ((elementTop <= pageBottom) && (elementBottom >= pageTop));
        }
    }
};

var Utils = new Utils();