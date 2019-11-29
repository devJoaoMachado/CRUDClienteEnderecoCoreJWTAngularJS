var templatingApp;
(
    function () {
        'use strict';
        templatingApp = angular.module('templating_app', ['ui.router']);

        angular.module('templating_app').controller('IndexController', ['$scope', function ($scope) {
            $scope.verificaPossuiTokenApi = function () {

                var tokenApi = window.sessionStorage.getItem('tokenApi');

                if (tokenApi == null || tokenApi == 'undefined' || tokenApi == '')
                    return false;
                return true;
            }
        }]);
    }
)();
