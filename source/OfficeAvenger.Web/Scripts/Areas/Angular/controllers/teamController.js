// ***************
// Team Controller
// ***************
app.controllers.controller('TeamController', ['$scope', '$routeParams', '$window', 'teamService', function ($scope, $routeParams, $window, teamService) {

    $scope.avenger = { Id: '', Name: '', Avatar: '' };
    $scope.loading = false;
    $scope.title = 'Edit Avenger';

    // updates the avenger
    $scope.updateAvenger = function () {
        teamService.update($scope.avenger, function (data) {
            $window.location.href = '/angular';
        });
    };

    // load the avenger
    $scope.init = function () {
        $scope.loading = true;

        if ($routeParams.id) {
            $scope.title = 'Edit Avenger';
            teamService.get($routeParams.id, function (data) {
                $scope.avenger = data;
                $scope.loading = false;
            });
        }
        else {
            $scope.title = 'Add Avenger';
            $scope.loading = false;
        }
    };

    $scope.init();
}]);