var ImportExportApp = angular.module('ImportExportApp', ["ngRoute"]);


ImportExportApp.controller('MainMenuController', MainMenuController);
ImportExportApp.controller('ImportController', ImportController);
ImportExportApp.controller('ExportController', ExportController);
ImportExportApp.factory('Filenames', Filenames);

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