using Appsistencia.Core.Infraestructura;
using Appsistencia.Core.Modelos;
using Appsistencia.CORE.Infraestructura;
using Appsistencia.CORE.Modelos;
using Appsistencia.CORE.Repositorios;
using FileHelpers;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Appsistencia.CORE.Services
{
    public class ServicioService : IServicioService
    {

        private readonly IEntityBaseRepository<Servicio> _servicioRepositorio;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICorreoService _correoService;
        private readonly IClienteService _clienteService;
        private readonly IVehiculoService _vehiculoService;
        private readonly IDireccionService _direccionService;

        public ServicioService(IEntityBaseRepository<Servicio> servicioRepositorio, IDireccionService direccionService, IVehiculoService vehiculoService, IClienteService clienteService, IUnitOfWork unitOfWork)
        {
            _servicioRepositorio = servicioRepositorio;
            _unitOfWork = unitOfWork;
            _clienteService = clienteService;
            _vehiculoService = vehiculoService;
            _direccionService = direccionService;
        }

        public Servicio Guardar(Servicio servicio)
        {
            var servicioRepo = new Servicio();

            if (servicio.id == 0)
            {
                servicioRepo.estadoCodigo = "RG";
                servicioRepo.fecha = DateTime.Now;
                servicioRepo.hora = DateTime.Now.TimeOfDay;

                _clienteService.Guardar(servicio.cliente);
                servicioRepo.clienteId = servicio.cliente.id;

                _vehiculoService.Guardar(servicio.vehiculo);
                servicioRepo.vehiculoId = servicio.vehiculo.id;

                _direccionService.Guardar(servicio.direccionInicio);
                servicioRepo.direccionInicioId = servicio.direccionInicio.id;

                _direccionService.Guardar(servicio.direccionDestino);
                servicioRepo.direccionDestinoId = servicio.direccionDestino.id;

                servicioRepo.fechaModificacion = DateTime.Now;
                servicioRepo.fechaRegistro = DateTime.Now;

                servicioRepo = _servicioRepositorio.Add(servicioRepo);
            }
            else
            {
                _servicioRepositorio.Edit(servicioRepo);
            }

            _unitOfWork.Commit();

            return servicioRepo;
        }

        public List<Servicio> Obtener()
        {
            var servicios = _servicioRepositorio.GetAll().ToList();
            return servicios;
        }

        public List<Servicio> ObtenerPorDescripcion(string descripcion)
        {
            throw new NotImplementedException();
        }

        public Servicio ObtenerPorId(int id)
        {
            var servicio = _servicioRepositorio.FindBy(s => s.id == id).FirstOrDefault();
            return servicio;
        }
        
        public void Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public void CambioEstado(int idServicio, Estado nuevoEstado)
        {
            var servicioRepo = _servicioRepositorio.FindBy(s => s.id == idServicio).FirstOrDefault();
            servicioRepo.estadoCodigo = nuevoEstado.codigo;
            _servicioRepositorio.Edit(servicioRepo);
            _unitOfWork.Commit();
        }

    }

    public partial interface IServicioService
    {
        List<Servicio> Obtener();
        Servicio ObtenerPorId(int id);
        Servicio Guardar(Servicio servicio);
        void CambioEstado(int idServicio, Estado nuevoEstado);
    }

    public class ServicioResumen
    {
        public string fecha { get; set; }
        public string hora { get; set; }
        public string aseguradora { get; set; }
        public string codigo { get; set; }
        public string asignadoPor { get; set; }
        public string asegurado { get; set; }
        public string vehiculo { get; set; }
        public string origen { get; set; }
        public string destino { get; set; }
        public string conductor { get; set; }
        public string ruta { get; set; }
        public string estado { get; set; }
    }
}
