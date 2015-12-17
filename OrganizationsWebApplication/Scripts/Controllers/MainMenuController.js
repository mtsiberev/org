var MainMenuController = function ($scope, $http, $location, Filenames) {

    $scope.selectMenuOption = function (view) {
        $location.path(view);
    };
};

MainMenuController.$inject = ['$scope', '$http', '$location', 'Filenames'];
