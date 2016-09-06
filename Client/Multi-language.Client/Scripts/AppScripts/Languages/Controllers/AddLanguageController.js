
appLanguages.controller('AddLanguageController', function ($scope, SinglePageCRUDService) {
    $scope.LangNo = 0;
    //The Save scope method use to define the Employee object and 
    //post the Employee information to the server by making call to the Service
    $scope.save = function () {
        var Language = {
            Name : $scope.Name,
            Initials:  $scope.Initials,
            Culture: $scope.Culture,
           // Picture: $scope.Picture,
            IsActive: $scope.IsActive,
            UserName: $scope.UserName,
        };
     
        var promisePost = SinglePageCRUDService.post(Language);


        promisePost.then(function (pl) {
            $scope.LangNo = pl.data.LangNo;
           // alert("LangNo " + pl.data.LangNo);
        },
              function (errorPl) {
                  $scope.error = 'failure loading Languages', errorPl;
              });

    };

});