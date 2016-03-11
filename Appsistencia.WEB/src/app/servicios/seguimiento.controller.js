(function() {
'use strict';
    angular.module('appsistencia')
        .controller('SeguimientoController', function ($modal, $state, ServicioSvc, SeguimientoSvc, EstadoSvc) {
            var vm = this;
            vm.servicio = {};
            vm.servicios = [];
            vm.seguimiento = {};
            
            vm.ObtenerServicios = function () {
                ServicioSvc.Obtener()
                    .then(function (response) {
                        vm.servicios = response.data;
                        AgruparServiciosPorEstado();
                    })
                    .catch(function (response) {
                        alert(response.data.ExceptionMessage);
                    });
            }
            vm.ObtenerServicios();

            function AgruparServiciosPorEstado() {
                vm.estadoServiciosActivos = {};
                angular.forEach(vm.servicios, function (servicio) {
                    vm.estadoServiciosActivos[servicio.estado.descripcion] = 
                        vm.estadoServiciosActivos[servicio.estado.descripcion] ?
                        vm.estadoServiciosActivos[servicio.estado.descripcion] + 1 : 1
                });
            }

            vm.VerSeguimientos = function (servicio) {
                vm.servicio = servicio;
                if (vm.active != servicio) {
                    vm.ObtenerSeguimientos(servicio);
                    vm.active = servicio;
                }
                else {
                    angular.forEach(vm.servicios, function (servicio) {
                        servicio.Seguimientos = null;
                    });
                    vm.active = null;
                }
            }

            vm.isSaving = false;
            vm.ActualizarServicio = function () {
                ServicioSvc.Guardar(vm.servicio)
                    .then(function () {
                        toastr.success('Servicio actualizado correctamente.');
                    }, function (response) {
                        toastr.error(response.data.ExceptionMessage);
                    });
            }

            vm.GuardarSeguimiento = function () {
                vm.isSaving = true;
                if (!vm.seguimiento) {
                    vm.seguimiento = {};
                }
                vm.seguimiento.ServicioId = vm.servicio.Id;
                vm.seguimiento.NuevoEstado = vm.servicio.EstadoCodigo;
                
                SeguimientoSvc.Guardar(vm.seguimiento)
                    .then(function () {
                        vm.seguimiento = {};
                        toastr.success('Seguimiento guardado correctamente.');
                        vm.ObtenerServicios();
                        vm.isSaving = false;
                    }, function (response) {
                        vm.isSaving = false;
                        toastr.error(response.data.ExceptionMessage);
                    });
            }

            vm.ObtenerSeguimientos = function (servicio) {
                //var idServicio = parseInt(vm.servicio.Id);
                SeguimientoSvc.ObtenerPorServicio(servicio.Id)
                    .then(function (response) {
                          servicio.Seguimientos = response.data;
                    }, function (response) {
                        toastr.error(response.data.ExceptionMessage);
                    });
            }

            vm.ObtenerEstados = function () {
                EstadoSvc.Obtener()
                    .then(function (response) {
                        vm.estados = response.data;
                    })
                    .catch(function (response){
                        toastr.error(response.data.ExceptionMessage);
                    });
            }
        });
})();