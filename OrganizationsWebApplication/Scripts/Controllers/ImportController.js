var ImportController = function ($scope, $http, Filenames) {
    
    $scope.files = Filenames;
    
    $scope.getResult =
    function getResult(filename) {
        getJson(filename)
            .success(function (json) {
                $scope.data = JSON.parse(json);
                console.log($scope.data);
                Filenames.delete(filename);
                $scope.files = Filenames;
           
            })
            .error(function (error) {
                $scope.status = 'Unable to load data: ' + error.message;
                console.log($scope.status);
            });
    }

    function getJson(filename) {
        return $http({
            url: "Import",
            method: "GET",
            params: { fileName: filename }
        });
    };
};

ImportController.$inject = ['$scope', '$http', 'Filenames'];
