(function (ns) {

    // ***************
    // Application
    // ***************

    ns.app = angular.module('officeAvengerApp', ['ngRoute', 'officeAvengerControllers']);
    ns.app.config(['$routeProvider',
      function ($routeProvider) {
          $routeProvider.
            when('/', {
                templateUrl: '/areas/angular/views/home/_Dashboard.html'
            }).
            when('/team/add', {
                templateUrl: '/areas/angular/views/team/_edit.html',
                controller: 'EditTeamController'
            }).
            when('/team/edit/:id', {
                templateUrl: '/areas/angular/views/team/_edit.html',
                controller: 'EditTeamController'
            }).
            otherwise({
                redirectTo: '/',
            });
      }]);

    // ***************
    // Controllers
    // ***************

    // mission controller
    ns.controllers = angular.module('officeAvengerControllers', []);
    ns.controllers.controller('MissionController', ['$scope', '$http', function ($scope, $http) {

        $http.get(ns.urls.getMissionsUrl).success(function (data) {
            $scope.missions = data;
        });

    }]);

    // team controller
    ns.controllers.controller('TeamController', ['$scope', '$http', function ($scope, $http) {

        $scope.addAvenger = function () {
            ns.modal.open(ns.views.editTeamModal);
        };

        $http.get(ns.urls.getTeamUrl).success(function (data) {
            $scope.team = data;
        });

    }]);

    // edit avenger
    ns.controllers.controller('EditTeamController', ['$scope', '$http', '$routeParams', '$window', function ($scope, $http, $routeParams, $window) {

        // updates the avenger
        $scope.updateAvenger = function () {
            $http({
                method: 'POST',
                url: ns.urls.editTeamUrl,
                data: $scope.avenger
            }).success(function (data) {
                $window.location.href = '#/';
            });
        };

        // load the avenger
        if ($routeParams.id) {
            $http.get(ns.urls.getTeamUrl + '/' + $routeParams.id).success(function (data) {
                $scope.avenger = data;
            });
        }
        else {
            $scope.avenger = {};
        }

    }]);

})(window.officeAvenger = window.officeAvenger || {})