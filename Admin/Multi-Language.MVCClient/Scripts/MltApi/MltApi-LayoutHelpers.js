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
                    $("body").removeClass("sidebar-open");
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
            function () {
                $("#project-box").append(data);
                $.get({ url: "../../Sections/ProjectBoxDropDowns", cache: false }).then(function (data2) {
                    $("#project-box").append(data2);
                });
            });

    });
};


$.MltApi.HideHomeLogoText = function () {
    // CHECK if mobile
    if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
        // Hide Header on on scroll down
        var didScroll;
        var lastScrollTop = 0;
        var delta = 5;
        var navbarHeight = $('#home-logo').outerHeight();


        $(window).scroll(function (event) {
            didScroll = true;
        });

        setInterval(function () {
            if (didScroll) {
                hasScrolled();
                didScroll = false;
            }
        },
            250);

        function hasScrolled() {
            var st = $(this).scrollTop();

            // Make sure they scroll more than delta
            if (Math.abs(lastScrollTop - st) <= delta)
                return;

            // If they scrolled down and are past the navbar, add class .nav-up.
            // This is necessary so you never see what is "behind" the navbar.
            if (st > lastScrollTop && st > navbarHeight) {
                // Scroll Down
                //$('#home-logo').removeClass('nav-down').addClass('nav-up');
                $('#home-logo').slideUp(500);
            } else {
                // Scroll Up
                if (st + $(window).height() < $(document).height()) {
                    //$('#home-logo').removeClass('nav-up').addClass('nav-down');
                    $('#home-logo').slideDown(500);
                }
            }

            lastScrollTop = st;
        }
    }
};

$.MltApi.ShowItemOnMobile = function (item) {
    $(item).fadeOut(500);
    $(item).parent().children(".hide-on-mobile").fadeIn(500,
        function () {

        });
};
$.MltApi.HideItemsOnMobile = function () {
    // CHECK if mobile
   // if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
    // Hide Header on on scroll
    $.each($(".hide-on-mobile"),function(i, item) {
        $(item).fadeOut(500,
            function() {
                $(item).parent().append("<button type='button' class='btn btn-info btn-sm' onclick=\"$.MltApi.ShowItemOnMobile(this)\">SHOW</button>");
            });
    });
    // };
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