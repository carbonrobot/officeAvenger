// ***************
// Index / Home page controller
// ***************
app.controllers.controller('HomeController', ['$scope', '$http', 'teamService', 'missionService', function ($scope, $http, teamService, missionService) {

    $scope.loadingTeam = true;
    $scope.loadingMissions = true;
    $scope.missions = [];
    $scope.team = [];

    teamService.list(function (data) {
        $scope.team = data;
        $scope.loadingTeam = false;
    });

    missionService.list(function (data) {
        $scope.missions = data;
        $scope.loadingMissions = false;
    });

}]);