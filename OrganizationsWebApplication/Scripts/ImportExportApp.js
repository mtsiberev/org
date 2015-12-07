var ImportExportApp = angular.module('ImportExportApp', ["ngRoute"]);

ImportExportApp.controller('ImportController', ImportController);
ImportExportApp.controller('ExportController', ExportController);

var configFunction = function ($routeProvider) {
    $routeProvider
        .when('/import',
    {
        templateUrl: '/Scripts/Views/import.html',
        controller: 'ImportController'
    });
    $routeProvider
        .when('/export',
    {
        templateUrl: '/Scripts/Views/export.html',
        controller: 'ExportController'
    });
   
};

configFunction.$inject = ['$routeProvider', '$httpProvider'];
ImportExportApp.config(configFunction);