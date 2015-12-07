var ExportController = function ($scope, $http) {

    func();

    function func() {
        return $http({
            url: "Export",
            method: "GET"
        });
    };
    
    $scope.export = 'export completed';
};

ExportController.$inject = ['$scope', '$http'];
