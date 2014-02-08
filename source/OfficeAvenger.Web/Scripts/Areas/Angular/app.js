(function(ns) {

    var app = angular.module('officeAvengerApp', []);

    // team controller
    app.controller('TeamController', function($scope, $http) {

        $scope.addAvenger = function() {
            ns.modal.open(ns.views.editTeamModal);
        };

        $http.get(ns.urls.getTeamUrl).success(function(data) {
            $scope.team = data;
        });

    });

    // mission controller
    app.controller('MissionController', function($scope, $http) {

        $http.get(ns.urls.getMissionsUrl).success(function (data) {
            $scope.missions = data;
        });

    });

})(window.officeAvenger = window.officeAvenger || {})