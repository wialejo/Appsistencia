(function () {
    'use strict';
    angular.module('appsistencia')
        .service('ProveedorSvc', function ($http, $q, ngAuthSettings) {
            return {
                Obtener: Obtener
            }
            function http(method, urlMetodo, data) {
                var request = $http({
                    method: method,
                    url: ngAuthSettings.apiServiceBaseUri + 'api/' + urlMetodo,
                    headers: {
                        'Access-Control-Allow-Origin': true
                    },
                    data: data
                });
                return request;
            }
            function Obtener(query) {
                var url = "Proveedores?" + query;
                var obj = http("GET", url);
                return obj
            }
        });
})();

