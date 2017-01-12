$.MltApi = $.MltApi || {};
$.MltApi.InternalHub = $.MltApi.InternalHub || {};

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
        $.MltApi.InternalHub.CpuInfoMessage(machineName, cpu, memUsage, memTotal, vm);
    };

    hub.client.tokenIsExpired = function (bool) {
        $.MltApi.InternalHub.TokenIsExpired(true);
    };
    // Start the connection
    $.connection.hub.start().done(function () {
        vm.connected(true);
    });
};

