var ImportController = function ($scope, $http) {
    
    getResult();

    function getResult() {
        getJson()
            .success(function (json) {
                $scope.import = json;
                console.log($scope.import);
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

ImportController.$inject = ['$scope', '$http'];
