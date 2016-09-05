appLanguages.controller('ShowLanguagesController', function ($scope, $location, SinglePageCRUDService, ShareData) {

    loadRecords();

//Function to Load all Languages Records.   
    function loadRecords()
    {
        var promiseGetLanguages = SinglePageCRUDService.getLanguages();

        promiseGetLanguages.then(function (pl) { $scope.Languages = pl.data },
              function (errorPl) {
                  $scope.error = 'failure loading Languages', errorPl;
              });
    }


    //Method to route to the addLanguage
    $scope.addLanguage = function () {
        $location.path("/addLanguage");
    }

    //Method to route to the editLanguage
    //The LangNo passed to this method is further set to the ShareData
    //This value can then be used to communicate across the Controllers
    $scope.editLanguage = function (LangNo) {
        ShareData.value = LangNo;
        $location.path("/editLanguage");
    }

    //Method to route to the deleteLanguage
    //The LangNo passed to this method is further set to the ShareData
    //This value can then be used to communicate across the Controllers
    $scope.deleteLanguage = function (LangNo) {
        ShareData.value = LangNo;
        $location.path("/deleteLanguage");
    }
});