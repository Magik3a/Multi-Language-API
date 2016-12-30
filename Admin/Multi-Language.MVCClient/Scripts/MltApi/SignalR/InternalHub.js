$.MltApi = $.MltApi || {};

$.MltApi.InitInternalHub = function(signalrAddress) {

    $.connection.hub.url = signalrAddress;

var internalHubProxy = $.connection.internalHub;
//$.connection.hub.qs = { 'access_token': token };
//internalHubProxy.on("Hello", function () {
//});

$.connection.hub.start()
    .done(function(){ console.log('Now connected, connection ID=' + $.connection.hub.id); })
    .fail(function(){ console.log('Could not Connect!'); });

};