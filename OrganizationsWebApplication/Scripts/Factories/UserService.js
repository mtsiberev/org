var UserService = function ($http) {
    var userService = {};

    userService.GetUserById = function () {
        var userId = id;
        var getUserAction = '@Url.Action("GetUser")';
        return $http({
            url: getUserAction,
            method: "GET",
            params: { id: userId }
        });
    };
    return userService;
 };

UserService.$inject = ['$http'];
