var gridHelper = {
    updateContainerHeight: function (containerId, gridId, minHeight) {
        var newHeight = minHeight + $("#" + gridId).height();
        $("#" + containerId).height(newHeight);
    },
    changeTdCursorToPointer: function (tableId, exceptionClass) {
        $("#" + tableId).find('td').css("cursor", "pointer");
        $("." + exceptionClass).parents('td').css("cursor", "default");
    }
}