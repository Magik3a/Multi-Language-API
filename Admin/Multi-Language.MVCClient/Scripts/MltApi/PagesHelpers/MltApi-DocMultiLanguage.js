$.MltApi = $.MltApi || {};
$.MltApi.DocMultilanguage = $.MltApi.DocMultilanguage || {};

$.MltApi.DocMultilanguage.FormSubmitedBegin = function (elem) {
    var parrentBox = $(elem).parents(".box").eq(0);
    parrentBox.find(".field-validation-error").remove();
    parrentBox.append(
    "<div class='overlay overlay-documentation' style='display: none'><i class='fa fa-refresh fa-spin'></i></div>"
        );
    $(".overlay").slideDown(100);
    console.log("begin");
};
$.MltApi.DocMultilanguage.FormSubmitedSuccessfully = function (data, status, xhr) {
    var isModelValid = xhr.getResponseHeader("InvalidModel");
    var box = $(".documentation-getting-started .overlay-documentation").parent();

    if (isModelValid != null && isModelValid !== "true") {
        $(box).find(".box-body").prepend($(data).find(".field-validation-error"));
        $(".overlay-documentation").slideUp(300);
    } else {
        if (xhr.getResponseHeader("ProjectDropDownIsChanged") != null) {
            $.get({ url: "../../Sections/ProjectBoxDropDowns", cache: false }).then(function (data2) {
                $("#change-active-project-box").remove();
                $("#project-box").append(data2);
            });
        }
        $(box).slideUp(300);
        $(box).parent().find(".box-ajax-success").slideDown(300);

    }
};


$.MltApi.DocMultilanguage.ToggleCollapse = function (elem) {
    var a = $(elem).attr('href');
    $(a).slideToggle();
    $(a).css("height", "");
};