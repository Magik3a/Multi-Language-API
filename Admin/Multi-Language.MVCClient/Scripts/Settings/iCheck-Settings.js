
$.MltApi = $.MltApi || {};

$.MltApi.InitICheckBoxes = function (elem, color) {
    $(elem).iCheck({
        checkboxClass: 'icheckbox_square-' + color,
        radioClass: 'iradio_square-' + color,
        increaseArea: '20%', // optional
        ajaxSettings: {
            headers: {
                'Authorization': 'Bearer ' + "token"
            }
        }
    });

};