var App = angular.module('App', []);

App.controller('ReportsCrtl', ['$scope', '$http', function ($scope, $http) {

    $scope.reports = {};
    $scope.events = {};
    $scope.event = {
        'EVENT_ID': 0,
        'EVENT_NAME': ""
    }

    $http.get("api/GetEvents/")
       .then(function (response) {
           $scope.events = angular.copy(response.data);
           $scope.event = $scope.events[0];
           $http.get("api/Reports/" + $scope.event.EVENT_ID)
            .then(function (response) {
             $scope.reports = angular.copy(response.data);
            });
       });

    $scope.selectEvent = function () {
        $http.get("api/Reports/" + $scope.event.EVENT_ID)
        .then(function (response) {
        $scope.reports = angular.copy(response.data);
    });
    };
}]);