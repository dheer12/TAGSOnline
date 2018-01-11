var App = angular.module('App', []);

App.controller('EventCheckInCrtl', ['$scope', '$http', function ($scope, $http) {
    $scope.searchMemb = { Email: '' };
    $scope.VM = {};

    $scope.searchMember = function () {
        $http.post("api/SearchEventMember", $scope.searchMemb)
        .then(function (response) {
            $scope.VM = angular.copy(response.data)
            if ($scope.VM.IsExistingMember && $scope.VM.IsRegistered) {
                debugger;
            }
            else if ($scope.VM.IsExistingMember && !$scope.VM.IsRegistered) {

            }
            else if (!$scope.VM.IsExistingMember && !$scope.VM.IsRegistered)
            {

            }
        });

        $scope.reset = function () {
            $scope.searchMemb = { Email: '' };
            $scope.VM = {};
        };
    };
}]);