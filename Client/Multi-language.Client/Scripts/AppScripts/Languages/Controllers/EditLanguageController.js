
appLanguages.controller('EditLanguageController', function ($scope, $location, ShareData, SinglePageCRUDService) {


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

    $scope.save = function () {
        var Language = {
            Name: $scope.Language.Name,
            Initials: $scope.Language.Initials,
            Culture: $scope.Language.Culture,
            //Picture: $scope.Language.Picture,
            IsActive: $scope.Language.IsActive,
            UserName: $scope.Language.UserName,
        };
        console.log(Language);
        var promisePut = SinglePageCRUDService.put($scope.Language.IdLanguage, Language);
        promisePut.then(function (pl) {
            $location.path("Home/showLanguages");
        },
                 function (errorPl) {
                     $scope.error = 'Failure saving language', errorPl;
                 });
    };

});