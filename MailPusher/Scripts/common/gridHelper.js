var gridHelper = {
    updateContainerHeight: function (containerId, gridId, minHeight) {
        var newHeight = minHeight + $("#" + gridId).height();
        $("#" + containerId).height(newHeight);
    },
    changeTdCursorToPointer: function (tableId, exceptionClass) {
        $("#" + tableId).find('td').css("cursor", "pointer");
        $("." + exceptionClass).parents('td').css("cursor", "default");
    },
    hidePginationIfOnePage: function (tableID, oSettings) {
        if (oSettings._iDisplayLength > oSettings.fnRecordsDisplay()) {
            $(oSettings.nTableWrapper).find('.dataTables_paginate').hide();
        }
    }
}