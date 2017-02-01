$.MltApi = $.MltApi || {};


$.MltApi.AjaxClickBegin = function (elem) {
    var target = $(elem).parent();
    if (target.is("li")) {
        $(".sidebar-menu li").removeClass("active");

    }
    $("#page-content").fadeTo(0, 0, function () {
        if (target.is("li")) {
            $(elem).append("<span id='mainSidebarLoader'><i class='fa fa-refresh fa-spin pull-right'></i></span>");
            target.addClass("active");
        }
    });

};
$.MltApi.AjaxClickComplete = function (action, controller, icon, clearPage) {

    if (clearPage) {
        $("#project-box").fadeTo("fast",
            0,
            function () {
                $("#project-box").css("position", "absolute");
            });
        $("#first-row-content").fadeTo("fast", 0, function () {
            $("#first-row-content").css("position", "absolute");
        });
    } else {

        $("#project-box").fadeTo("fast", 1).css("position", "");

        $("#first-row-content").css("position", "");
    }

    $("#page-content").fadeTo("fast", 1, function () {
        $("#mainSidebarLoader").remove();
        $("#breadcrumb-parent").html("<i class='fa " + icon + "'></i>" + controller);
        $("#breadcrumb-child").html(action);
        var url = $(location).attr('href').split('/');
        if (url[3] != controller && action === "Index") {

            $("#first-row-content").fadeTo("fast", 0);
            $("#first-row-content").hide(250);
            $.MltApi.LoadFirstRow("../../Sections/FirstRow?id=" + controller);
        }
        $.MltApi.InitialisePlugins();
        var content = $(".wrapper").html();
        window.history.pushState({ "html": content, "action": action }, action, '../../' + controller + '/' + action);
    });

};

$.MltApi.AjaxClickSuccess = function (data, status, xhr) {
    $("body").removeClass("sidebar-open");

    $.MltApi.HideItemsOnMobile();

    $(".content-header h1").html(xhr.getResponseHeader("ContentHeader") + " <small>" + xhr.getResponseHeader("ContentDescription") + "</small>");
    if (xhr.getResponseHeader("ProjectIsChanged") != null) {
        $("#project-box").fadeTo("fast", 0, function () {
            $("#project-box").html("");
            $.get({ url: "../../Sections/ProjectBox?id=" + xhr.getResponseHeader("ProjectIsChanged"), cache: false }).then(function (data) {
                $("#project-box").append(data);
            });
            $.get({ url: "../../Sections/ProjectBoxDropDowns", cache: false }).then(function (data2) {
                $("#project-box").append(data2);
            });
            $("#project-box").fadeTo("fast", 100);
        });


    }
    if (xhr.getResponseHeader("ProjectDropDownIsChanged") != null) {
        $("#change-active-project-box").hide(300);
        console.log("changed");
        $.get({ url: "../../Sections/ProjectBoxDropDowns", cache: false }).then(function (data2) {
            $("#change-active-project-box").remove();

            $("#project-box").append(data2);
        });
    }
};

$.MltApi.AjaxLoginFromLockScreenSuccess = function (data, status, xhr) {
    if (data.status === "Error") {
        $("#externalLoginError").text(data.message);
        $("#externalLoginError").slideDown(300,
            function () {
            });
        return;
    }
    $("#externalLoginError").slideUp(300,
         function () {
             $(".wrapper").slideDown(300, function () {
                 $("body").removeClass();
                 $("body").addClass("skin-red sidebar-mini");
             });
             $(".lockscreen-wrapper").slideUp(300);
         });
};