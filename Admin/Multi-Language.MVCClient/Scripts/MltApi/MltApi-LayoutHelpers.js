$.MltApi = $.MltApi || {};

$.MltApi.HomeIndexLinkClicked = function (elem) {
    $(".sidebar-menu li").removeClass("active");
    var url = $(location).attr('href').split('/');
    //if (url[3] === "") {
    //    return;
    //}
    var content = $(".wrapper").html();
    window.history.pushState({ "html": content, "action": "" }, "", '../../');
    $("#page-content").fadeTo("fast",
        0,
        function () {
            $.get("../../",
                function (data) {
                    $(".content-header h1").html("Dashboard <small>Hello! This is your administration panel.</small>");
                    $("#breadcrumb-parent").html("<i class='fa fa-globe'></i>Index");
                    $("#breadcrumb-child").html("Home");
                    $("#page-content").html(data);
                    $("#page-content").fadeTo("fast", 1);
                    $("#first-row-content").fadeTo("fast", 0);
                    $("#first-row-content").hide(250);

                    $("#project-box").fadeTo("fast", 1).css("position", "");
                    $("#first-row-content").css("position", "");

                    $.MltApi.InitialisePlugins();

                    $.MltApi.SystemStabilityRefreshInterval = $.MltApi.SystemStabilityRefreshIntervalTimer();
                    $.MltApi.SystemStabilityLoggsRefreshInterval = $.MltApi.SystemStabilityLoggsRefreshIntervalTimer();
                    $.MltApi.LoadFirstRow("../../Sections/FirstRow?id=Home");
                });
        });
};


$.MltApi.ActiveProjectIsChanged = function (e) {
    $("#project-box").fadeTo("fast", 0, function () {
        $("#active-project-box").remove();
        $.get({ url: "../../Sections/ChangeActiveProject?id=" + $(e).val(), cache: false }).then(function (data) {
            $("#project-box").prepend(data);

            $("#project-box").fadeTo("fast", 100, function () {
                var url = $(location).attr('href');

                $.get({ url: url, cache: false }).then(function (data) {
                    $("#page-content").fadeTo("fast", 0, function () {
                        $("#page-content").html(" ");
                        $("#page-content").append(data);
                        $("#page-content").fadeTo("fast", 100, function () {
                            urlSplited = url.split('/');
                            $.MltApi.LoadFirstRow("../../Sections/FirstRow?id=" + urlSplited[3]);

                        });
                        $.MltApi.InitialisePlugins();

                    });
                });
            });
        });

    });

};


$.MltApi.LoadFirstRow = function (url) {
    $("#first-row-content").fadeTo("fast", 0, function () {
        var progressElem = $('#first-row-content');
        var URL = url;
        // $("#loading").hide();
        $.ajax({
            type: 'GET',
            dataType: 'html',
            url: URL,
            cache: false,
            error: function (xhr, ajaxOptions, thrownError) {
                console.log(xhr.responseText);
                console.log(thrownError);
            },
            success: function (json) {
                $("#first-row-content").html(json);
                $("#first-row-content").show(250);
                $("#first-row-content").fadeTo("fast", 100);
            }
        });
    });
};


$.MltApi.LoadProjectBox = function () {

    var url = $(location).attr('href').split('/');
    if (url[3] === "Documentation") {
        $("#project-box").fadeTo("fast",
           0,
           function () {
               $("#project-box").css("position", "absolute");
           });
        return;
    }
    $.get({ url: "../../Sections/ProjectBox", cache: false }).then(function (data) {
        $("#project-box .box").fadeOut(500,
            function() {
                $("#project-box").append(data);
                $.get({ url: "../../Sections/ProjectBoxDropDowns", cache: false }).then(function (data2) {
                    $("#project-box").append(data2);
                });
            });

    });
};

window.onpopstate = function (e) {
    if (e.state) {
        $(".wrapper").html(e.state.html);
        $("#page-content").fadeIn(200);
        document.title = e.state.action;
        $("#first-row-content").fadeTo("fast", 0);
        $("#first-row-content").hide(250);
        var url = $(location).attr('href').split('/');
        $.MltApi.LoadFirstRow("../../Sections/FirstRow?id=" + url[3]);
    }
};