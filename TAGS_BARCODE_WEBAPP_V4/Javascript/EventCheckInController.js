var App = angular.module('App', []);

App.controller('EventCheckInCrtl', ['$scope', '$http', function ($scope, $http) {
    $scope.searchMemb = { Email: '' };

    $scope.searchMember = function () {
        $http.post("api/SearchEventMember", $scope.searchMemb)
        .then(function (response) {
            if (response.data.MemberNotFound) {

            }
            else {

            }
        });

        $scope.reset = function () {

        };
    };
}]);