
templatingApp.controller('LoginController', ['$scope', '$http', function ($scope, $http) {
    
    $scope.submit = function () {
        $http({
            method: 'POST',
            url: '/api/token/request',
            data: $scope.loginModel
        }).then(function (response) {
            window.sessionStorage.setItem('tokenApi', response.data.token);
            console.log(response.data.token);
            window.location.href = "/cliente";

            }, function (error) {
                if (error.status == 400) alert(error.data);
            console.log(error);
        });
    };


}]);
