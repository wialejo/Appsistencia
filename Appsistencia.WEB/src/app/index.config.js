(function () {
    'use strict';

    angular
      .module('appsistencia')
      .config(config);

    /** @ngInject */
    function config($logProvider, toastrConfig) {
        // Enable log
        $logProvider.debugEnabled(true);

        // Set options third-party lib
        toastrConfig.allowHtml = true;
        toastrConfig.timeOut = 3000;
        toastrConfig.positionClass = 'toast-top-right';
        toastrConfig.preventDuplicates = true;
        toastrConfig.progressBar = true;
    }


    angular.module('appsistencia')
      .constant('ngAuthSettings', {
          apiServiceBaseUri: "http://" + location.hostname + "/" + location.pathname.split("/")[1] + "/API/",
          clientId: 'ngAuthApp'
      });

    angular.module('appsistencia')
      .config(function ($httpProvider) {
          $httpProvider.defaults.headers.common['Access-Control-Allow-Headers'] = '*';
          $httpProvider.defaults.headers.common['Access-Control-Allow-Methods'] = '*';
          $httpProvider.interceptors.push('authInterceptorService');
          delete $httpProvider.defaults.headers.common['X-Requested-With'];
      });

    angular.module('appsistencia')
      .run(function (authService) {
          authService.fillAuthData();
      });

})();
