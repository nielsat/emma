var gridHelper = {
    updateContainerHeight: function (containerSelector, gridSelector, minHeight) {
        var newHeight = minHeight + $(gridSelector).height();
        $(containerSelector).height(newHeight);
    },
    changeTdCursorToPointer: function (tableSelector, exceptionSelector) {
        $(tableSelector).find('td').css("cursor", "pointer");
        $(exceptionSelector).parents('td').css("cursor", "default");
    },
    hidePginationIfOnePage: function (tableSelector, oSettings) {
        if (oSettings._iDisplayLength > oSettings.fnRecordsDisplay()) {
            $(oSettings.nTableWrapper).find('.dataTables_paginate').hide();
        }
    }
}