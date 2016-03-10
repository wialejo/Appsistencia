(function() {
'use strict';
	angular.module('appsistencia')
		.service('ClienteSvc', function ($http, $q, ngAuthSettings, authService) {
			return {
				Guardar: Guardar,
				ObtenerPorUsuario: ObtenerPorUsuario,
				Obtener: Obtener
			}
			function http(method, urlMetodo, data) {
				var request = $http({
				    method: method,
				    url: ngAuthSettings.apiServiceBaseUri + 'api/' + urlMetodo,
					headers: { 
						'Access-Control-Allow-Origin':true	
					},
					data: data
				});
				return request;
			}

			function Guardar(cliente) {
				var url = "Clientes/Guardar/";
				var obj = http('POST', url, cliente );
				return obj;
			}
			function ObtenerPorUsuario() {
			    var url = "Clientes/ObtenerPorUsuario/" + authService.authentication.userName;
				var obj = http("GET", url);
				return obj
			}
			function Obtener() {
			    var url = "Clientes/Obtener/";
				var obj = http("GET", url);
				return obj
			}
		});
})();

