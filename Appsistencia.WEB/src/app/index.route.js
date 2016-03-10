(function () {
    'use strict';

    angular
      .module('appsistencia')
      .config(routerConfig);

    /** @ngInject */
    function routerConfig($stateProvider, $urlRouterProvider) {

        $urlRouterProvider.otherwise('/app/solicitar');
        $stateProvider
          .state('app', {
              url: '/app',
              templateUrl: 'app/main/main.html'
          })
        .state('app.solicitar', {
            url: '/solicitar',
            templateUrl: 'app/servicios/solicitar.html'
        })
          .state('access', {
              url: '/access',
              template: '<div ui-view class="fade-in-right-big smooth"></div>'
          })
          .state('access.login', {
              url: '/login',
              templateUrl: 'app/account/login.html'
          })
          .state('access.register', {
              url: '/register',
              templateUrl: 'app/account/register.html'
          })
          .state('access.forgotpwd', {
              url: '/forgotpwd',
              templateUrl: 'app/tpl/page_forgotpwd.html'
          })
          .state('access.404', {
              url: '/404',
              templateUrl: 'app/tpl/page_404.html'
          });

    }

})();
