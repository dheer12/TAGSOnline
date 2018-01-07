var App = angular.module('App', []);

App.controller('qrCrtl', ['$scope', '$http', function ($scope, $http) {
    $scope.CheckInVM = {
        TicketNo: '',
        IsCheckedIn: false,
        TicketNotFound: false,
        AlreadyCheckedIn: false
    };

    $scope.checkIn = function () {
        $http.post("api/Station1", $scope.CheckInVM)
        .then(function (response) {
            $scope.CheckInVM = response.data;
        });
    };

    $scope.reset = function () {
        $scope.CheckInVM = {
            TicketNo: '',
            IsCheckedIn: false,
            TicketNotFound: false,
            AlreadyCheckedIn: false
        };
    };

}]);