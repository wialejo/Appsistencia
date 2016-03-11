(function() {
	'use strict';
		angular.module('appsistencia')
			.service('EstadoSvc', function ($http, $q, ngAuthSettings) {
				return {
					Obtener: Obtener
				}
				function http(method, urlMetodo, data) {
					var request = $http({
						method: method,
						url: ngAuthSettings.apiServiceBaseUri + 'api/' + urlMetodo,
						data: data
					});
					return request;
				}
				function Obtener() {
					var url = "estados/Obtener/";
					var obj = http("GET", url);
					return obj
				}
			});
})();