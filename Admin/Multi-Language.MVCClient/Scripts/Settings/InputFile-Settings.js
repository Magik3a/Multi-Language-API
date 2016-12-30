
$.MltApi = $.MltApi || {};

$.MltApi.InitFileInput = function (elem, url, allowedExtensions) {
    $(elem)
              .fileinput({
                  uploadUrl: url, // server upload action
                  uploadAsync: true,
                  allowedPreviewTypes: false,
                  allowedFileExtensions: allowedExtensions,
                  showUploadedThumbs: false,
                  browseOnZoneClick: true,
                  maxFileCount: 5
              });
}