$.MltApi = $.MltApi || {};

$.MltApi.SystemStabilityRefreshIntervalTimer = function() {
    var d = new Date();
    var seconds = d.getSeconds();
    var progressBar = $('.nextSystemInfoReloadingBar'), width = seconds * 1.65;
    progressBar.width(width);
    i = setInterval(function() {
            width += 0.415;
            progressBar.css('width', width + '%');
        },
        250);
    return i;
};
$.MltApi.SystemStabilityLoggsRefreshIntervalTimer = function() {
    var d = new Date();
    var minutes = d.getMinutes();
    var progressBar = $('.nextSystemInfoLoggsReloadingBar'), width = minutes * 1.65;
    progressBar.width(width);
    i = setInterval(function() {
            width += 0.0825;
            progressBar.css('width', width + '%');
        },
        3000);
    return i;
};
$.MltApi.SystemStabilityRefreshInterval = $.MltApi.SystemStabilityRefreshIntervalTimer();
$.MltApi.SystemStabilityLoggsRefreshInterval = $.MltApi.SystemStabilityLoggsRefreshIntervalTimer();
$.MltApi.InitInternalHub = function (signalrAddress) {

    // The view model that is bound to our view
    var ViewModel = function () {
        var self = this;

        // Whether we're connected or not
        self.connected = ko.observable(false);

        // Collection of machines that are connected
        self.machines = ko.observableArray();
    };

    // Instantiate the viewmodel..
    var vm = new ViewModel();
    // .. and bind it to the view
    ko.applyBindings(vm, $(".computerInfo")[0]);

    if ($('.computerInfoBox').length > 0) {
        var machine = {
            machineName: $("#machineName").text(),
            cpu: $("#cpuPercent").text(),
            memUsage: $("#memoryAvailable").text(),
            memTotal: $("#memoryTotal").text(),
            memPercent: $("#memPercent").text()
        };
        var machineModel = ko.mapping.fromJS(machine);
        vm.machines.push(machineModel);
        ko.applyBindings(vm, $(".computerInfoBox")[0]);
    }
    // Get a reference to our hub
    var hub = $.connection.InternalHub;

    $.connection.hub.url = signalrAddress;
    // Add a handler to receive updates from the server
    hub.client.cpuInfoMessage = function (machineName, cpu, memUsage, memTotal) {
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

    hub.client.tokenIsExpired = function (bool) {
        console
            .log("The whole thing from hangfire > tokenTask > web api > signalR > client is working! Dont know how, but it is a good thing");

        $(".wrapper").slideUp(500, function () {
            $("body").removeClass();
            $("body").addClass("hold-transition lockscreen");
        });
        $(".lockscreen-wrapper").slideDown(500);
    };
    // Start the connection
    $.connection.hub.start().done(function () {
        vm.connected(true);
    });
};

