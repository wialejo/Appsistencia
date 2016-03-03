(function() {
  'use strict';

  angular
    .module('appsistencia')
    .run(runBlock);

  /** @ngInject */
  function runBlock($log) {

    $log.debug('runBlock end');
  }

})();
