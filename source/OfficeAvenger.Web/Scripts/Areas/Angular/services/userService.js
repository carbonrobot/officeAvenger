// **************
// User Service
// **************
app.services.factory('userService', ['$http', function ($http) {
    return {
        login: function (usr, pwd, callback) {
            $http.post(officeAvenger.urls.loginUrl, { username: usr, password: pwd }).success(callback);
        },
        logout: function (callback) {
            $http.get(officeAvenger.urls.logoffUrl).success(callback);
        }
    };
}]);