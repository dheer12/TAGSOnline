var App = angular.module('App', []);

App.controller('HomeCrtl', ['$scope', '$http', function ($scope, $http) {

    $scope.IsUserAdmin = false;

    $http.get("api/IsUserAdmin")
        .then(function (response) {
            $scope.IsUserAdmin = response.data;
        });
}]);