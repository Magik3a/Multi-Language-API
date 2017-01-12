$.MltApi = $.MltApi || {};
$.MltApi.InternalHub = $.MltApi.InternalHub || $.connection.InternalHub;


$.MltApi.SystemStabilityRefreshIntervalTimer = function () {
    var d = new Date();
    var seconds = d.getSeconds();
    var progressBar = $('.nextSystemInfoReloadingBar'), width = seconds * 1.65;
    progressBar.width(width);
    i = setInterval(function () {
        width += 0.415;
        progressBar.css('width', width + '%');
    },
        250);
    return i;
};
$.MltApi.SystemStabilityLoggsRefreshIntervalTimer = function () {
    var d = new Date();
    var minutes = d.getMinutes();
    var progressBar = $('.nextSystemInfoLoggsReloadingBar'), width = minutes * 1.65;
    progressBar.width(width);
    i = setInterval(function () {
        width += 0.0825;
        progressBar.css('width', width + '%');
    },
        3000);
    return i;
};
$.MltApi.SystemStabilityRefreshInterval = $.MltApi.SystemStabilityRefreshIntervalTimer();
$.MltApi.SystemStabilityLoggsRefreshInterval = $.MltApi.SystemStabilityLoggsRefreshIntervalTimer();

// Add a handler to receive updates from the server
$.MltApi.InternalHub.CpuInfoMessage = function (machineName, cpu, memUsage, memTotal, vm) {
    if ($('.nextSystemInfoReloadingBar').width() > 0) {
        clearInterval($.MltApi.SystemStabilityRefreshInterval);
    }

    $.MltApi.SystemStabilityRefreshInterval = $.MltApi.SystemStabilityRefreshIntervalTimer();

    var machine = {
        machineName: machineName,
        cpu: cpu.toFixed(0) + "%",
        memUsage: (memUsage / 1024).toFixed(2),
        memTotal: (memTotal / 1024).toFixed(2),
        memPercent: ((memUsage / memTotal) * 100).toFixed(1) + "%"
    };

    var machineModel = ko.mapping.fromJS(machine);

    // Check if we already have it:
    var match = ko.utils.arrayFirst(vm.machines(), function (item) {
        return item.machineName() == machineName;
    });

    if (!match) {
        vm.machines.removeAll();
        vm.machines.push(machineModel);
    } else {
        var index = vm.machines.indexOf(match);
        vm.machines.replace(vm.machines()[index], machineModel);


    }
};

$.MltApi.InternalHub.TokenIsExpired = function (bool) {
    console
        .log("The whole thing from hangfire > tokenTask > web api > signalR > client is working! Dont know how, but it is a good thing");

    $(".wrapper").slideUp(500, function () {
        $("body").removeClass();
        $("body").addClass("hold-transition lockscreen");
    });
    $(".lockscreen-wrapper").slideDown(500);
};
// Start the connection