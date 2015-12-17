var Filenames = function ($http) {
    var messages = {};
    messages.list = [];

    messages.delete = function (message) {
        messages.list.pop({ name: message });
    };

    messages.add = function (message) {
        messages.list.push({ name: message });
    };

    return messages;
};

Filenames.$inject = ['$http'];
