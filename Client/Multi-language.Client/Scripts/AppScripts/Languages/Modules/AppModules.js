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
                element.dataTable({ "bDestroy": true });
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
appLanguages.config(['$httpProvider','$routeProvider', '$locationProvider', function ($httpProvider,$routeProvider, $locationProvider) {

     $httpProvider.defaults.headers.common = {};
  $httpProvider.defaults.headers.post = {};
  $httpProvider.defaults.headers.put = {};
  $httpProvider.defaults.headers.patch = {};
    $routeProvider.when('/Home/showLanguages',
                         {
                             templateUrl: '../Language/ShowLanguages',
                             controller: 'ShowLanguagesController'
                         });
    $routeProvider.when('/Home/addLanguage',
                           {
                               templateUrl: '../Language/AddNewLanguage',
                               controller: 'AddLanguageController'
                           });
    $routeProvider.when("/Home/editLanguage",
                        {
                            templateUrl: '../Language/EditLanguage',
                            controller: 'EditLanguageController'
                        });
    $routeProvider.when('/Home/deleteLanguage',
                        {
                            templateUrl: '../Language/DeleteLanguage',
                            controller: 'DeleteLanguageController'
                        });
    $routeProvider.otherwise(
                    {
                        redirectTo: '/Home/'
                    });

    $locationProvider.html5Mode({
        enabled: true,
        requireBase: false
    }).hashPrefix('!')
}]);