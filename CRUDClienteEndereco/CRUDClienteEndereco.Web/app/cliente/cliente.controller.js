
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

    //******=========Get Single User=========******
    $scope.getUser = function (user) {
        $http({
            method: 'GET',
            url: '/api/Values/GetUserByID/' + parseInt(user.id)
        }).then(function (response) {
            $scope.userModel = response.data;
        }, function (error) {
            console.log(error);
        });
    };

    //******=========Save User=========******
    $scope.saveUser = function () {
        $http({
            method: 'POST',
            url: '/api/Values/PostUser/',
            data: $scope.userModel
        }).then(function (response) {
            $scope.reset();
            getallData();
        }, function (error) {
            console.log(error);
        });
    };

    //******=========Update User=========******
    $scope.updateUser = function () {
        $http({
            method: 'PUT',
            url: '/api/Values/PutUser/' + parseInt($scope.userModel.id),
            data: $scope.userModel
        }).then(function (response) {
            $scope.reset();
            getallData();
        }, function (error) {
            console.log(error);
        });
    };

    //******=========Delete User=========******
    $scope.deleteUser = function (user) {
        var IsConf = confirm('You are about to delete ' + user.Name + '. Are you sure?');
        if (IsConf) {
            $http({
                method: 'DELETE',
                url: '/api/Values/DeleteUserByID/' + parseInt(user.id)
            }).then(function (response) {
                $scope.reset();
                getallData();
            }, function (error) {
                console.log(error);
            });
        }
    };

    //******=========Clear Form=========******
    $scope.reset = function () {
        var msg = "Form Cleared";
        $scope.userModel = {};
        $scope.userModel.id = 0;
    };
}]);
