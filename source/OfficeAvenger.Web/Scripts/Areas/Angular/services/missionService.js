// **************
// Mission Service
// **************
app.services.factory('missionService', ['$http', function ($http) {

    var assignHero = function (url, id, avengerId, callback) {
        $http({
            method: 'POST',
            url: url,
            data: { missionId: id, avengerId: avengerId }
        }).success(callback);
    };

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
        },
        assign: function (id, avengerId, callback) {
            assignHero(officeAvenger.urls.addHeroUrl, id, avengerId, callback);
        },
        remove: function (id, avengerId, callback) {
            assignHero(officeAvenger.urls.removeHeroUrl, id, avengerId, callback);
        },
        scrub: function (id, callback) {
            $http({
                method: 'DELETE',
                url: officeAvenger.urls.deleteMissionUrl + '/' + id,
            }).success(callback);
        },
        engage: function (id, callback) {
            $http({
                method: 'POST',
                url: officeAvenger.urls.startMissionUrl + '/' + id,
            }).success(callback);
        }
    };
}]);