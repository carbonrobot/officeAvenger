// ***************
// Mission Controller
// ***************
app.controllers.controller('MissionController', ['$scope', '$routeParams', '$window', 'missionService', function ($scope, $routeParams, $window, missionService) {

    $scope.mission = { Id: '', Name: '', Duration: 30 };
    $scope.loading = false;
    $scope.title = 'Edit Mission';
    $scope.standbyTeam = [];

    // updates the mission
    $scope.updateMission = function () {
        missionService.update($scope.mission, function (data) {
            $window.location.href = '/angular';
        });
    };

    // load the mission
    $scope.init = function () {
        $scope.loading = true;

        if ($routeParams.id) {
            $scope.title = 'Edit Mission';
            missionService.get($routeParams.id, function (data) {
                $scope.mission = data;
                $scope.loading = false;
            });
        }
        else {
            $scope.title = 'Add Mission';
            $scope.loading = false;
        }
    };

    $scope.init();
}]);