var UserController = function ($scope, $http) {

    getUser();
    
    function getUser() {
        GetUserById()
            .success(function (userById) {
                $scope.user = userById;
                console.log($scope.user);
            })
            .error(function (error) {
                $scope.status = 'Unable to load data: ' + error.message;
                console.log($scope.status);
            });
    }
    
    function GetUserById() {
        var userId = userIdVar;
        var getUserAction = getUserActionVar;
        return $http({
            url: getUserAction,
            method: "GET",
            params: { id: userId }
        });
    };
};

UserController.$inject = ['$scope', '$http'];
