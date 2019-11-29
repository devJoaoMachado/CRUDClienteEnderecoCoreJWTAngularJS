
templatingApp.controller('ClienteController', ['$scope', '$http', function ($scope, $http) {
    
    var tokenApi = window.sessionStorage.getItem('tokenApi');
    $http.defaults.headers.common['Authorization'] = 'bearer ' + tokenApi;

    obterTodos();

    function obterTodos() {
        $http({
            method: 'GET',
            url: '/api/cliente/obtertodos/'
        }).then(function (response) {
            console.log(response);
            $scope.clienteList = response.data;
            }, function (error) {
                if (error.status == 401)
                    window.location.href = '../';
            console.log(error);
        });
    };

}]);
