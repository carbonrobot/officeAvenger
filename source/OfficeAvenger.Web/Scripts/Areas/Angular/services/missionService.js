// **************
// Mission Service
// **************
app.services.factory('missionService', ['$http', '$q', function ($http) {
    return {
        get: function (id, callback) {
            $http.get(officeAvenger.urls.getMissionsUrl + '/' + id)
                .success(callback);
        },
        list: function (callback) {
            $http.get(officeAvenger.urls.getMissionsUrl).success(callback);
        },
        update: function (mission, callback) {
            $http({
                method: 'POST',
                url: officeAvenger.urls.editMissionUrl,
                data: mission
            }).success(callback);
        }
    };
}]);