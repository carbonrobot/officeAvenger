// **************
// Team Service
// **************
app.services.factory('teamService', ['$http', function ($http) {
    return {
        get: function (id, callback) {
            $http.get(officeAvenger.urls.getTeamUrl + '/' + id)
                .success(callback);
        },
        list: function (callback) {
            $http.get(officeAvenger.urls.getTeamUrl).success(callback);
        },
        update: function (avenger, callback) {
            $http({
                method: 'POST',
                url: officeAvenger.urls.editTeamUrl,
                data: avenger
            }).success(callback);
        },
        scrub: function (id, callback) {
            $http({
                method: 'DELETE',
                url: officeAvenger.urls.deleteHeroUrl + '/' + id,
            }).success(callback);
        }
    };
}]);