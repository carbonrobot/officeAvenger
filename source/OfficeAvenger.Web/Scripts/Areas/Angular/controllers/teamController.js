// ***************
// Team Controller
// ***************
(function (ns) {

    ns.controllers.controller('TeamController', ['$scope', '$routeParams', '$window', 'teamService', function ($scope, $routeParams, $window, teamService) {

        $scope.avenger = {};
        $scope.loading = false;
        $scope.title = 'Edit Avenger';

        // updates the avenger
        $scope.updateAvenger = function () {
            teamService.update($scope.avenger, function (data) {
                $window.location.href = '#/';
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
                $scope.avenger = {};
                $scope.loading = false;
            }
        };

        $scope.init();
    }]);

})(window.officeAvenger = window.officeAvenger || {})

