var App = angular.module('App', []);

App.controller('ReportsCrtl', ['$scope', '$http', function ($scope, $http) {

    $scope.reports = {};
    $scope.eventId = 2;

    $http.get("api/Reports/" + $scope.eventId)
        .then(function (response) {
            $scope.reports = angular.copy(response.data);
        });
}]);