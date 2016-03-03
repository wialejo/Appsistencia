(function() {
  'use strict';

  angular
    .module('appsistencia')
    .config(routerConfig);

  /** @ngInject */
  function routerConfig($stateProvider, $urlRouterProvider) {
    $stateProvider
      .state('solicitarServicio', {
        url: '/',
        templateUrl: 'app/servicios/seleccionTipo.html'
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

    $urlRouterProvider.otherwise('/');
  }

})();
