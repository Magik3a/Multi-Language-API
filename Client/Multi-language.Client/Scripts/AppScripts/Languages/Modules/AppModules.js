var appLanguages = angular.module("LanguagesDataTableModule", ["ngRoute"]);
    //The Factory used to define the value to
    //Communicate and pass data across controllers

    appLanguages.factory("ShareData", function () {
        return { value: 0 }
    });
appLanguages.directive("data-table", function () {
    return function (scope, element, attrs) {
        scope.$watch("assignments", function (value) {//I change here
            var val = value || null;            
            if (val)
                element.dataTable({"bDestroy": true});
        });
    };
});

function AssignmentCtrl($scope, $http, $timeout) {
    //I change here
    //$scope.assignments = [];

    $scope.fetch = function () {
        // -- Mock server data..
        var data = SinglePageCRUDService.getLanguages();
        
        
        // -- This works, since it does not have an async call:
        //$scope.assignmentsLoaded(data);
        
                
         // -- Add an async call, no more worky (the table stays empty)
        $timeout(function () { $scope.assignmentsLoaded(data); }, 1000);
    };

    $scope.assignmentsLoaded = function (data, status) {
        $scope.assignments = data;
    }

    $scope.fetch();
}
appLanguages.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
    $routeProvider.when('/showLanguages',
                         {
                             templateUrl: 'Language/ShowLanguages',
                             controller: 'ShowLanguagesController'
                         });
    $routeProvider.when('/addLanguage',
                           {
                               templateUrl: 'Language/AddNewLanguage',
                               controller: 'AddLanguageController'
                           });
    $routeProvider.when("/editLanguage",
                        {
                            templateUrl: 'Language/EditLanguage',
                            controller: 'EditLanguageController'
                        });
    $routeProvider.when('/deleteLanguage',
                        {
                            templateUrl: 'Language/DeleteLanguage',
                            controller: 'DeleteLanguageController'
                        });
    $routeProvider.otherwise(
                    {
                        redirectTo: '/'
                    });

    $locationProvider.html5Mode({
  enabled: true,
  requireBase: false
}).hashPrefix('!')
}]);