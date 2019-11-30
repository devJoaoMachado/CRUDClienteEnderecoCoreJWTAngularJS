
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
            if (response.status == 200)
                alert('Cliente incluído com sucesso!');
        }, function (error) {
            if (error.status == 401)
                window.location.href = '../';
        });
    };

    function deletar(clienteModel) {
        $http({
            method: 'DELETE',
            url: '/api/cliente/deletar?clienteId=' + clienteModel.id, 
        }).then(function (response) {
            if (response.status == 204)
                alert('Cliente excluído com sucesso!');
        }, function (error) {
            if (error.status == 401)
                alert('Ops! Você não tem permissão para realizar essa operação.');
        });
    };

}]);
