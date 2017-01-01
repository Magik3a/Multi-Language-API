$.MltApi = $.MltApi || {};

$.MltApi.Interval = setTimer();

function setTimer() {
    var progressBar = $('.nextSystemInfoReloadingBar'), width = 0;
    progressBar.width(width);
    i = setInterval(function () {
        width += 1.65;
        progressBar.css('width', width + '%');
    },
        1000);
    return i;
}
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
    console.log($("#cpuPercent"));
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
            clearInterval($.MltApi.Interval);
        }

        $.MltApi.Interval = setTimer();

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

    // Start the connectio
    $.connection.hub.start().done(function () {
        vm.connected(true);
    });
};

