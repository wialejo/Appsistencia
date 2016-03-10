using Appsistencia.Core.Infraestructura;
using Appsistencia.Core.Modelos;
using Appsistencia.CORE.Modelos;
using Appsistencia.CORE.Repositorios;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appsistencia.CORE.Services
{
    public class SeguimientoService : ISeguimientoService
    {

        private readonly IEntityBaseRepository<Seguimiento> _SeguimientoRepositorio;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEstadoService _estadoService;
        private readonly IServicioService _servicioServiceExt;

        public SeguimientoService(IEntityBaseRepository<Seguimiento> SeguimientoRepositorio, IUnitOfWork unitOfWork, 
            IEstadoService estadoService, IServicioService servicioServiceExt)
        {
            _SeguimientoRepositorio = SeguimientoRepositorio;
            _estadoService = estadoService;
            _servicioServiceExt = servicioServiceExt;
            _unitOfWork = unitOfWork;
        }

        public List<Seguimiento> Obtener()
        {
            var seguimientos = _SeguimientoRepositorio.GetAll().ToList();
            return seguimientos;
        }
        
        public Seguimiento ObtenerPorId(int id)
        {
            var seguimiento = _SeguimientoRepositorio.FindBy(c => c.id == id).FirstOrDefault();
            return seguimiento;
        }

        public void Eliminar(int id)
        {
            var seguimientoRepo = _SeguimientoRepositorio.FindBy(c => c.id == id).FirstOrDefault();
            _SeguimientoRepositorio.Delete(seguimientoRepo);

            _unitOfWork.Commit();
        }

        public Seguimiento Guardar(Seguimiento seguimiento)
        {
            var seguimientoRepo = new Seguimiento();
            if (seguimiento.id == 0)
            {
                var nuevoEstado = _estadoService.ObtenerPorCodigo(seguimiento.nuevoEstado);
                seguimiento.observacion  = "Nuevo estado: " + nuevoEstado.descripcion + ",  " + seguimiento.observacion;
                seguimiento.fecha = DateTime.Now;
                _servicioServiceExt.CambioEstado(seguimiento.servicioId, nuevoEstado);
                seguimientoRepo = _SeguimientoRepositorio.Add(seguimiento);
            }

            _unitOfWork.Commit();
            return seguimientoRepo;
        }

        public List<Seguimiento> ObtenerPorDescripcion(string descripcion)
        {
            throw new NotImplementedException();
        }

        public List<Seguimiento> ObtenerPorServicio(int id)
        {
            var seguimientos = _SeguimientoRepositorio.FindBy(s => s.servicioId == id).ToList();
            return seguimientos;
        }
    }

    public interface ISeguimientoService
    {
        List<Seguimiento> ObtenerPorServicio(int id);
        Seguimiento Guardar(Seguimiento seguimiento);
    }
}
