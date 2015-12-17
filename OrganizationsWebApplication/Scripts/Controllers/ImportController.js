var ImportController = function ($scope, $http, Filenames) {

    $scope.getResult =

    function getResult() {
        getJson()
            .success(function (json) {
                $scope.data = JSON.parse(json);
                console.log($scope.data);

            })
            .error(function (error) {
                $scope.status = 'Unable to load data: ' + error.message;
                console.log($scope.status);
            });
    }

    function getJson() {
        return $http({
            url: "Import",
            method: "GET"
        });
    };
};

ImportController.$inject = ['$scope', '$http', 'Filenames'];
