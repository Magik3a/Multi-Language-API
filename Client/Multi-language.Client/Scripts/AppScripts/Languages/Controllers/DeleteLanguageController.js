 
appLanguages.controller('DeleteLanguageController', function ($scope, $location, ShareData, SinglePageCRUDService) {


        getLanguage();
    function getLanguage() {
        
        var promiseGetLanguage = SinglePageCRUDService.getLanguage(ShareData.value);

        
        promiseGetLanguage.then(function (pl)
        {
            $scope.Language = pl.data;
        },
              function (errorPl) {
                  $scope.error = 'Failure loading Language', errorPl;
              });
    }

    $scope.delete = function () {
        var promiseDelete = SinglePageCRUDService.delete(ShareData.value);
        promiseDelete.then(function (pl) {
            $location.path("Home/showLanguages");
        },
                 function (errorPl) {
                     $scope.error = 'Failure deleting language', errorPl;
                 });
    };

});