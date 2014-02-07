
var officeAvengerApp = angular.module('officeAvengerApp', []);
officeAvengerApp.controller('TeamController', function ($scope) {

    $scope.team = [
        {
            Name: 'Iron Man',
            Avatar: ''
        },
        {
            Name: 'Thor',
            Avatar: ''
        },
        {
            Name: 'Captain America',
            Avatar: ''
        }
    ];

});

$(function () {

});