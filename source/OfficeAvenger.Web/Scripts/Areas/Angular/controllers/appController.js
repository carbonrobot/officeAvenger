// *******************
// Application controller
// *******************
app.controller('AppController', ['$scope', '$http', 'userService', function ($scope, $http, userService) {

    $scope.isLoading = true;
    $scope.isLoggedIn = false;
    $scope.loggedInUser = {};
    $scope.loginFormData = {};

    $scope.login = function () {
        var usr = $scope.loginFormData.username;
        var pwd = $scope.loginFormData.password;

        userService.login(usr, pwd, function (response) {
            if (response.HasError) {
                $scope.loginFormData.password = '';
                $scope.loginFormData.error = response.Error;
            }
            else {
                $scope.isLoggedIn = true;
                $scope.loggedInUser = response;
            }
        });
    };

    $scope.logout = function () {
        userService.logout(function () {
            $scope.isLoggedIn = false;
            $scope.loggedInUser = {};
        });
    };

    $scope.init = function () {
        $http.get(officeAvenger.urls.agentUrl)
            .success(function (data) {
                $scope.isLoading = false;
                $scope.isLoggedIn = true;
                $scope.loggedInUser = data;
            })
            .error(function(){
                $scope.isLoading = false;
                $scope.isLoggedIn = false;
            });
    };

    $scope.init();

}]);