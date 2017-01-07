$.MltApi = $.MltApi || {};
$.MltApi.FreeSpace = $.MltApi.FreeSpace || 0;
$.MltApi.ReservedSpace = $.MltApi.ReservedSpace || 0;

$.MltApi.InitialisePlugins = function() {
    // initialise plugins if there is any
    if ($('.dataTable').children().length > 0) {
        $('.dataTable').DataTable();
    }
    if ($('input[type=checkbox]').length > 0) {
        $.MltApi.InitICheckBoxes($('input[type=checkbox]'), "red");
    }
    if ($(".select2").length > 0) {
        $(".select2").select2();
    }
    if ($("#input-backups").length > 0) {
        // TODO Add drag and drop options
        $.MltApi.InitFileInput($("#input-backups"), baseAdressApi + "/backup/upload", ['bak']);
    }

    if ($('#pieChartSystemSpace').length > 0) {
        $.MltApi.InitializeDiskSpaceChart($.MltApi.FreeSpace, $.MltApi.ReservedSpace);
    }

    if ($('#systemStabilityChart').length > 0) {
            $.MltApi.InitializeSystemStabilityChart(jsonSystemStabilityChartModel);


    }
}