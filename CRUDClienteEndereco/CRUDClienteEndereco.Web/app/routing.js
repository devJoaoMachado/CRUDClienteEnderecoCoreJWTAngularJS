templatingApp.config(['$locationProvider', '$stateProvider', '$urlRouterProvider', '$urlMatcherFactoryProvider', '$compileProvider',
    function ($locationProvider, $stateProvider, $urlRouterProvider, $urlMatcherFactoryProvider, $compileProvider) {

        //console.log('Appt.Main is now running')
        if (window.history && window.history.pushState) {
            $locationProvider.html5Mode({
                enabled: true,
                requireBase: true
            }).hashPrefix('!');
        };
        $urlMatcherFactoryProvider.strictMode(false);
        $compileProvider.debugInfoEnabled(false);

        $stateProvider
            .state('login', {
                url: '/',
                templateUrl: './views/home/login.html',
                controller: 'LoginController'
            })
            .state('cliente', {
                url: '/cliente',
                templateUrl: './views/cliente/cliente.html',
                controller: 'ClienteController'
            })
            .state('endereco', {
                url: '/endereco',
                templateUrl: './views/endereco/endereco.html',
                controller: 'EnderecoController'
            })
            .state('logout', {
                url: '/',
                templateUrl: './views/home/login.html',
                controller: 'LogoutController'
            }); 

        $urlRouterProvider.otherwise('/');
    }]); 
