// ***************
// Index / Home page controller
// ***************
(function (ns) {

    ns.controllers.controller('HomeController', ['$scope', '$http', function ($scope, $http) {

        $http.get(ns.urls.getMissionsUrl).success(function (data) {
            $scope.missions = data;
        });
        $http.get(ns.urls.getTeamUrl).success(function (data) {
            $scope.team = data;
        });

    }]);

})(window.officeAvenger = window.officeAvenger || {})