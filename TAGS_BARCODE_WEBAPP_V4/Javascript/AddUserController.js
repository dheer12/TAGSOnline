var App = angular.module('App', []);

App.controller('AddUserCrtl', ['$scope', '$http', function ($scope, $http) {
    $scope.user = {
        FIRST_NAME: '',
        LAST_NAME: '',
        PASSWORD: '',
        USER_ROLE: 'User'
    };

    $scope.userCreated = false;

    $scope.addUser = function () {
        $http.post("api/AddUser", $scope.user)
        .then(function (response) {
            $scope.userCreated = true;
            $scope.reset();
        });
    };

    $scope.reset = function () {
        $scope.user = {
            FIRST_NAME: '',
            LAST_NAME: '',
            PASSWORD: '',
            USER_ROLE: ''
        };
    };
}]);