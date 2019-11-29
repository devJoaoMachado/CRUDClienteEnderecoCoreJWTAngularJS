
templatingApp.controller('LogoutController', ['$scope', function ($scope) {

    window.sessionStorage.setItem('tokenApi', '');
    window.location.href = '../';
}]);
