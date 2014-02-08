
var app = angular.module('officeAvengerApp', []);

// team controller
app.controller('TeamController', function ($scope, $http) {

    $http.get('/api/team').success(function(data) {
        $scope.team = data;
    });

});

// mission controller
app.controller('MissionController', function ($scope, $http) {

    $http.get('/api/mission').success(function (data) {
        $scope.missions = data;
    });

});