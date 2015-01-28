var usersControllers = angular.module("usersControllers", []);

usersControllers.controller("usersListController", ["$scope", "$http", function ($scope, $http) {
    $scope.users = [];
  


}]);

function User(name, isOn) {
    this.name = name;
    this.isOn = isOn;
}