// **************
// Team Service
// **************
(function (ns) {

    ns.services.factory('teamService', ['$scope', '$http', function ($scope, $http) {
        return {
            get: function (id, callback) {
                $http.get(ns.urls.getTeamUrl + '/' + id)
                   .success(callback);
            },
            update: function (avenger, callback) {
                $http({
                    method: 'POST',
                    url: ns.urls.editTeamUrl,
                    data: avenger
                }).success(callback);
            }
        };
    }]);

})(window.officeAvenger = window.officeAvenger || {})