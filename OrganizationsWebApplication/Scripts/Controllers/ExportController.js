var ExportController = function ($scope, $http, Filenames) {

    $scope.files = Filenames;
    
    $scope.getResult =
        function getResult() {
            exportFile()
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
        };

    function exportFile() {
        return $http({
            url: "Export",
            method: "GET"
        });
    };
    /////////////////////////////////////////
    $scope.downloadFile =
     function downloadFile(filename) {

         $scope.filename = filename;

         getFile(filename)
             .success(function (file) {
                 var a = document.createElement("a");
                 document.body.appendChild(a);
                 a.style = "display: none";
                 
                 var json = file,
                     blob = new Blob([json], { type: "octet/stream" }),
                     url = window.URL.createObjectURL(blob);
                 a.href = url;
                 a.download = $scope.filename;
                 a.click();
                 window.URL.revokeObjectURL(url);

             })
             .error(function (error) {
                 $scope.status = 'Unable to save file: ' + error.message;
                 console.log($scope.status);
             });
     };
    
    function getFile(filename) {
        return $http({
            url: "GetFile",
            method: "GET",
            params: { fileName: filename }
        });
    };

};

ExportController.$inject = ['$scope', '$http', 'Filenames'];



