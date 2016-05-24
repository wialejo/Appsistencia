(function () {
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

            vm.Seguimiento = function (servicio) {
                $modal.open({
                    size: 'md',
                    templateUrl: "seguimiento.html",
                    controllerAs: "vmm",
                    controller: function ($modalInstance, servicio, ProveedorSvc, ServicioSvc, toastr, SeguimientoSvc, vm) {
                        var vmm = this
                        vmm.servicio = servicio;

                        vmm.ObtenerEstados = function () {
                            EstadoSvc.Obtener()
                                .then(function (response) {
                                    vmm.estados = response.data;
                                })
                                .catch(function (response) {
                                    toastr.error(response.data.ExceptionMessage);
                                });
                        }
                        vmm.ObtenerEstados();

                        vmm.ObtenerSeguimientos = function () {
                            SeguimientoSvc.ObtenerPorServicio(vmm.servicio.id)
                                .then(function (response) {
                                    vmm.servicio.seguimientos = response.data;
                                }, function (response) {
                                    toastr.error(response.data.ExceptionMessage);
                                });
                        }
                        vmm.ObtenerSeguimientos();
                        vmm.GuardarSeguimiento = function () {
                            vmm.isSaving = true;
                            if (!vmm.seguimiento) {
                                vmm.seguimiento = {};
                            }
                            vmm.seguimiento.servicioId = vmm.servicio.id;
                            vmm.seguimiento.nuevoEstado = vmm.servicio.estadoCodigo;

                            SeguimientoSvc.Guardar(vmm.seguimiento)
                                .then(function () {
                                    vmm.seguimiento = {};
                                    vmm.ObtenerSeguimientos();
                                    toastr.success('Seguimiento guardado correctamente.');
                                    vmm.isSaving = false;
                                }, function (response) {
                                    vmm.isSaving = false;
                                    toastr.error(response.data.ExceptionMessage);
                                });
                        }
                        vmm.cancel = function () {
                            vm.ObtenerServicios();
                            $modalInstance.dismiss('cancel');
                        };
                    },
                    resolve: {
                        servicio: function () {
                            return servicio;
                        },
                        vm: function () {
                            return vm;
                        }
                    }
                });
            }

            vm.AsignarProveedor = function (servicio) {
                $modal.open({
                    size: 'md',
                    templateUrl: "AsignarProveedor.html",
                    controllerAs: "vmm",
                    controller: function ($modalInstance, servicio, ProveedorSvc, ServicioSvc, vm) {
                        var vmm = this
                        vmm.servicio = servicio;

                        vmm.ObtenerProveedores = function (descripcion) {
                            vmm.proveedores = [];
                            if (descripcion) {
                                ProveedorSvc.Obtener("descripcion=" + descripcion + "&top=10")
                                    .then(function (response) {
                                        vmm.proveedores = response.data;
                                    })
                            }
                        }
                        vmm.cancel = function () {
                            vm.ObtenerServicios();
                            $modalInstance.dismiss('cancel');
                        };
                        vmm.AsignarProveedor = function () {
                            ServicioSvc.AsignarProveedor(servicio.id, servicio.proveedor.id, servicio.tiempoEstimado)
                                .then(function (response) {
                                    alert("Proveedor asignado correctamente");
                                }).catch(function (response) {
                                   alert(response.data.ExceptionMessage);
                                });
                        }
                    },
                    resolve: {
                        servicio: function () {
                            return servicio;
                        },
                        
                        vm: function () {
                            return vm;
                        }

                        
                    }
                });
            }

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

        });
})();