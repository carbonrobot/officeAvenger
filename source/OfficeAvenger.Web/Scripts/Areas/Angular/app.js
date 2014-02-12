
// ***************
// application
// ***************
var app = angular.module('app', ['ngRoute', 'app.services', 'app.controllers', 'ngAnimate'])
        .config(function ($routeProvider, $locationProvider) {
            $routeProvider.
                when('/angular', {
                    controller: 'HomeController',
                    templateUrl: '/areas/angular/views/home/_index.html'
                }).
                when('/angular/team/add', {
                    controller: 'TeamController',
                    templateUrl: '/areas/angular/views/team/_edit.html'
                }).
                when('/angular/team/edit/:id', {
                    controller: 'TeamController',
                    templateUrl: '/areas/angular/views/team/_edit.html'
                }).
                when('/angular/mission/add', {
                    controller: 'MissionController',
                    templateUrl: '/areas/angular/views/mission/_edit.html'
                }).
                when('/angular/mission/edit/:id', {
                    controller: 'MissionController',
                    templateUrl: '/areas/angular/views/mission/_edit.html'
                }).
                otherwise({
                    redirectTo: '/angular',
                });

            $locationProvider.html5Mode(true)
                             .hashPrefix('!');
        });

// ***************
// modules
// ***************
app.services = angular.module('app.services', []);
app.controllers = angular.module('app.controllers', ['app.services']);