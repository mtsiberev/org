var UserApp = angular.module('UserApp', []);

UserApp.controller('UserController', UserController);

UserApp.factory('UserService', UserService);