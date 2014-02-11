(function (ns) {
    
    // ***************
    // Application
    // ***************

    ns.app = angular.module('app', ['ngRoute', 'appControllers', 'appServices'])
        .config(function ($routeProvider) {
            $routeProvider.
              when('/', {
                  controller: 'HomeController',
                  templateUrl: '/areas/angular/views/home/_index.html'
              }).
              when('/team/edit/:id', {
                  controller: 'TeamController',
                  templateUrl: '/areas/angular/views/team/_edit.html'
              }).
              otherwise({
                  redirectTo: '/',
              });
        });

})(window.officeAvenger = window.officeAvenger || {})