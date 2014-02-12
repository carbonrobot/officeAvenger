// ***************
// Index / Home page controller
// ***************
app.controllers.controller('HomeController', ['$scope', '$http', 'teamService', 'missionService', function ($scope, $http, teamService, missionService) {

    $scope.loadingTeam = true;
    $scope.loadingMissions = true;
    $scope.missions = [];
    $scope.team = [];

    $scope.deleteMission = function (index, element) {
        missionService.scrub(element.Id, function () {
            $scope.missions.splice(index, 1);
        });
    };

    $scope.deleteAvenger = function (index, element) {
        teamService.scrub(element.Id, function () {
            $scope.team.splice(index, 1);
        });
    };

    $scope.startMission = function (index, element) {
        missionService.engage(element.Id, function (data) {
            $scope.missions[index] = data;
        });
    };

    $scope.init = function () {
        teamService.list(function (data) {
            delayPush(data, $scope.team);
            $scope.loadingTeam = false;
        });

        missionService.list(function (data) {
            delayPush(data, $scope.missions);
            $scope.loadingMissions = false;
        });
    };

    var delayPush = function (source, dest) {
        angular.forEach(source, function (value, key) {
            setTimeout(function () {
                dest.push(value);
                $scope.$apply();
            }, key * 100);
        });
    };

    $scope.init();
}]);