var ExportController = function ($scope, $http, Filenames) {

    $scope.getResult =

    function getResult() {
        getFilename()
            .success(function (filename) {
                console.log(filename);
                $scope.resultFile = filename;
                Filenames.add(filename);

                $scope.files = Filenames;
            })
            .error(function (error) {
                $scope.status = 'Unable to save file: ' + error.message;
                console.log($scope.status);
            });
    }

    function getFilename() {
        return $http({
            url: "Export",
            method: "GET"
        });
    };
};

ExportController.$inject = ['$scope', '$http', 'Filenames'];



