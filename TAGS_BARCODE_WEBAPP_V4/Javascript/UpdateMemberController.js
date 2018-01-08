var App = angular.module('App', []);

App.controller('UpdateMemberCrtl', ['$scope', '$http', function ($scope, $http) {
    $scope.searchMemb = { Email: '' };

    $scope.member = {
        CELL_PHONE: null,
        COMPANY_NAME:null,
        EMAIL_ID: null,
        FIRST_NAME: null,       
        HOME_PHONE: null,
        IS_VOLUNTEER: null,
        LAST_NAME: null,
        MEMBER_ID: null,
        MemberNotFound: null,
        IsNewMember: null,
        NewMember: null
    };

    $scope.ShowUpdateMemberForm = false;
    $scope.ShowNewMemberForm = false;

    $scope.alert = '';
        
    $scope.searchMember = function () {
        $http.post("api/SearchMember", $scope.searchMemb)
        .then(function (response) {
            if (response.data.MemberNotFound) {
                $scope.ShowNewMemberForm = true;
                $scope.ShowUpdateMemberForm = false;
                $scope.member.EMAIL_ID = angular.copy($scope.searchMemb.Email);
                $scope.alert = '';
            }
            else {
                $scope.member = response.data;
                $scope.ShowNewMemberForm = false;
                $scope.ShowUpdateMemberForm = true;
                $scope.alert = '';
            }
        });
    };

    $scope.updateMember = function () {
        $http.post("api/UpdateMember", $scope.member)
        .then(function (response) {
            response.data = $scope.member;
            $scope.ShowNewMemberForm = false;
            $scope.ShowUpdateMemberForm = true;
            $scope.alert = 'Successfully Updated';
        });
    };

    $scope.AddMember = function () {
        $http.post("api/AddMember", $scope.member)
        .then(function (response) {
            response.data = $scope.member;
                $scope.ShowNewMemberForm = false;
                $scope.ShowUpdateMemberForm = true;
                $scope.alert = 'Successfully Created';
        });
    };

    $scope.reset = function () {
        $scope.ShowUpdateMemberForm = false;
        $scope.ShowNewMemberForm = false;

        $scope.searchMemb = { Email: '' };

        $scope.member = {
            CELL_PHONE: null,
            COMPANY_NAME: null,
            EMAIL_ID: null,
            FIRST_NAME: null,
            HOME_PHONE: null,
            IS_VOLUNTEER: null,
            LAST_NAME: null,
            MEMBER_ID: null,
            MemberNotFound: null,
            IsNewMember: null,
            NewMember: null
        };
        $scope.alert = '';
    };

}]);