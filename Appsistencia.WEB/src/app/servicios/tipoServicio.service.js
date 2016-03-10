(function () {
    'use strict';
    angular.module('appsistencia')
		.service('TipoServicioSvc', function ($http, $q, ngAuthSettings) {
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
		        })
		        return request;
		    }
		    
		    function Obtener() {
		        var url = "TipoServicio/Obtener";
		        var obj = http("GET", url);
		        return obj
		    }
		});
})();

