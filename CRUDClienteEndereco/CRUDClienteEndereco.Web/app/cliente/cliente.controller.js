
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

    $scope.inserir = function () {
        $http({
            method: 'POST',
            url: '/api/cliente/Inserir/',
            data: $scope.clientModel
        }).then(function (response) {
            if (response.status == 200) {
                alert('Cliente incluido com sucesso!');
                $scope.clientModel = null;
                window.location.href = '/cliente';
            }

        }, function (error) {
            if (error.status == 401)
                window.location.href = '../';
        });
    };

    $scope.deletar = function(id) {
        $http({
            method: 'DELETE',
            url: '/api/cliente/deletar?clienteId=' + id, 
        }).then(function (response) {
            if (response.status == 204) {
                alert('Cliente excluido com sucesso!');
                obterTodos();
            }
                
            }, function (error) {
                if (error.status == 401 || error.status == 403)
                alert('Ops! Voce nao tem permissao para realizar essa operacao.');
        });
    };

}]);
