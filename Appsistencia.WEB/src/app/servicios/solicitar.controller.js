(function () {
    'use strict';

    angular.module('appsistencia')
      .controller('SolicitarController', function (ClienteSvc, ServicioSvc, TipoServicioSvc) {
          var vm = this;
          vm.servicio = {
              cliente: {},
              vehiculo: {},
              direccionInicio: {}
          }
          vm.verFormularioDatosBasicos = false;
          vm.ObtenerTiposServicios = function () {
              TipoServicioSvc.Obtener()
                .then(function (response) {
                    vm.tiposServicio = response.data;
                })
          }
          vm.ObtenerTiposServicios();

          vm.ObtenerCliente = function () {
              ClienteSvc.ObtenerPorUsuario()
                .then(function (response) {
                    vm.servicio.cliente = response.data;
                })
          }
          vm.ObtenerCliente();

          vm.solicitarDatosBasicos = function (tipoServicio) {
              vm.verFormularioDatosBasicos = true;
              vm.tipoServicioSeleccionado = tipoServicio;
              vm.servicio.tipoServicioCodigo = tipoServicio.codigo;
              if (tipoServicio.codigo == "CE")
                  vm.servicio.direccionDestino = {};
          }
          vm.seleccionarDireccionInicio = function (direccion, index) {
              vm.servicio.direccionInicio = direccion;
              vm.servicio.cliente.direcciones.splice(index);
          }
          vm.seleccionarDireccionDestino = function (direccion, index) {
              vm.servicio.direccionDestino = direccion;
              vm.servicio.cliente.direcciones.splice(index);
          }

          vm.seleccionarVehiculo = function (vehiculo, index) {
              vm.servicio.vehiculo = vehiculo;
              vm.servicio.cliente.vehiculos.splice(index);
          }

          vm.solicitarServicio = function () {
              ServicioSvc.Guardar(vm.servicio)
                  .then(function (response) {
                      alert("servicio solicitado correctamente")
                  })
                  .catch(function (response) {
                      alert(response.message);
                  });

          }
      });
})();
