(function() {
  'use strict';

  angular
    .module('appsistencia')
    .controller('MainController', MainController);

  /** @ngInject */
  function MainController($timeout, webDevTec, toastr, authService, $location) {
    var vm = this;
    vm.authentication = authService.authentication;

    vm.logOut = function () {
        authService.logOut();
        $location.path('/access/login');
    }
    if (!vm.authentication.isAuth) {
        $location.path('/access/login');
    }

    // config
    vm.app = {
      name: 'TuAliado',
      version: '0.0.1',
      // for chart colors
      color: {
        primary: '#7266ba',
        info:    '#23b7e5',
        success: '#27c24c',
        warning: '#fad733',
        danger:  '#f05050',
        light:   '#e8eff0',
        dark:    '#3a3f51',
        black:   '#1c2b36'
      },
      settings: {
        themeID: 1,
        navbarHeaderColor: 'bg-black',
        navbarCollapseColor: 'bg-white-only',
        asideColor: 'bg-black',
        headerFixed: true,
        asideFixed: true,
        asideFolded: false,
        asideDock: false,
        container: false
      }
    }
  }
})();
