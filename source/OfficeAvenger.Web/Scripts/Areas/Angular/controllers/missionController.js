// ***************
// Mission Controller
// ***************
app.controllers.controller('MissionController', ['$scope', '$routeParams', '$window', 'missionService', 'teamService', function ($scope, $routeParams, $window, missionService, teamService) {

    $scope.mission = { Id: '', Name: '', Duration: 30, Team: [] };
    $scope.loading = [];
    $scope.title = 'Edit Mission';
    $scope.team = [];
    $scope.standbyTeam = [];

    // assign an avenger to this mission
    $scope.addHero = function (index, element) {
        missionService.assign($scope.mission.Id, element.Id, function (data) {
            $scope.mission.Team.push(element);
            $scope.onTeamUpdated();
        });
    };

    // remove an avenger from this mission
    $scope.removeHero = function (index, element) {
        missionService.remove($scope.mission.Id, element.Id, function (data) {
            $scope.mission.Team.splice(index, 1);
            $scope.onTeamUpdated();
        });
    };

    // updates the mission
    $scope.updateMission = function () {
        missionService.update($scope.mission, function (data) {
            $window.location.href = '/angular';
        });
    };

    // load the mission
    $scope.init = function () {
        if ($routeParams.id) {
            $scope.title = 'Edit Mission';
            $scope.loading.push(true);

            missionService.get($routeParams.id, function (data) {
                $scope.mission = data;

                teamService.list(function (data) {
                    $scope.team = data;
                    $scope.onTeamUpdated();
                });
            });
        }
        else {
            $scope.title = 'Add Mission';
            $scope.loading = false;
        }
    };

    // updates the standby team
    $scope.onTeamUpdated = function () {
        $scope.standbyTeam = getStandbyTeam();
    };

    // get an array of all team members not selected for this mission
    var getStandbyTeam = function () {
        var team = $scope.team.slice(0);

        if ($scope.mission.Team.length > 0) {
            for (var i = 0; i < $scope.mission.Team.length; i++) {
                var av = $scope.mission.Team[i];
                for (var j = 0; j < team.length; j++) {
                    var match = team[j];
                    if (match.Id == av.Id) {
                        team.splice(j, 1);
                        break;
                    }
                }
            }
        }

        return team;
    };

    $scope.init();
}]);