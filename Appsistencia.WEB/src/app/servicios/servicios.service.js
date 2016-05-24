(function () {
    'use strict';
    angular.module('appsistencia')
        .service('ServicioSvc', function ($http, $q, ngAuthSettings) {
        return {
	        Obtener: Obtener,
	        Guardar: Guardar,
	        ObtenerPorId: ObtenerPorId,
	        AsignarProveedor: AsignarProveedor
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
        function Guardar(servicio) {
	        var url = "Servicios/Guardar/";
	        var obj = http('POST', url, servicio);
	        return obj;
        }
        function ObtenerPorId(id) {
	        var url = "Servicios/ObtenerPorId/" + id;
	        var obj = http("GET", url);
	        return obj
        }
        function Obtener() {
	        var url = "Servicios/Obtener/";
	        var obj = http("GET", url);
	        return obj
        }

        function AsignarProveedor(servicioId, proveedorId, tiempoEstimado) {
	        var url = "Servicios/" + servicioId + "/proveedor/" + proveedorId + "/tiempoEstimado/" + tiempoEstimado;
	        var obj = http("POST", url);

	        return obj
        }
		});
})();

