var App = angular.module('App', []);

App.controller('EventCheckInCrtl', ['$scope', '$http', function ($scope, $http) {
    $scope.searchMemb = { Email: '' };
    $scope.VM = {
        IsExistingMember : false,
        IsRegistered: false,
        newMember: {
        },
        eventCheckInVM: {

        }
    };
    $scope.IsExistingMember = false;
    $scope.IsRegistered = false;
    $scope.hasSubmitted = false;

    $scope.searchMember = function () {
        $http.post("api/SearchEventMember", $scope.searchMemb)
        .then(function (response) {
            $scope.hasSubmitted = true;
            $scope.VM = angular.copy(response.data);
            if ($scope.VM.IsExistingMember && $scope.VM.IsRegistered) {
                $scope.IsExistingMember = true;
                $scope.IsRegistered = true;
                debugger;
            }
            else if ($scope.VM.IsExistingMember && !$scope.VM.IsRegistered) {
                $scope.IsExistingMember = true;
                $scope.IsRegistered = false;
                debugger;
            }
            else if (!$scope.VM.IsExistingMember && !$scope.VM.IsRegistered)
            {
                $scope.VM.newMember.EMAIL_ID = angular.copy($scope.searchMemb.Email);
                $scope.IsExistingMember = false;
                $scope.IsRegistered = false;
                debugger;
            }
        });

        $scope.reset = function () {
            $scope.searchMemb = { Email: '' };
            $scope.VM = {
                IsExistingMember: false,
                IsRegistered: false,
                newMember: {}
            };
            $scope.IsExistingMember = false;
            $scope.IsRegistered = false;
            $scope.hasSubmitted = false;
        };
    };

    $scope.CheckInUser = function () {
        $http.post("api/UpdateEventMember", $scope.VM)
       .then(function (response) {
           $scope.VM = angular.copy(response.data);
           if ($scope.VM.IsExistingMember && $scope.VM.IsRegistered) {
               $scope.IsExistingMember = true;
               $scope.IsRegistered = true;
           }
           else if ($scope.VM.IsExistingMember && !$scope.VM.IsRegistered) {
               $scope.IsExistingMember = true;
               $scope.IsRegistered = false;
           }
           else if (!$scope.VM.IsExistingMember && !$scope.VM.IsRegistered) {
               $scope.IsExistingMember = false;
               $scope.IsRegistered = false;
           }
       });
    };
}]);