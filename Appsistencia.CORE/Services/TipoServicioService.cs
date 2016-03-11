using Appsistencia.API.Infraestructura;
using Appsistencia.API.Modelos;
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
    public class TipoServicioService : ITipoServicioService
    {

        private readonly IEntityBaseRepository<TipoServicio> _tipoServicioRepositorio;
        private readonly IUnitOfWork _unitOfWork;

        public TipoServicioService(IEntityBaseRepository<TipoServicio> tipoServicioRepositorio, IUnitOfWork unitOfWork)
        {
            _tipoServicioRepositorio = tipoServicioRepositorio;
            _unitOfWork = unitOfWork;
        }

        public List<TipoServicio> Obtener()
        {
            var vehiculoes = _tipoServicioRepositorio.GetAll().ToList();
            return vehiculoes;
        }

        public List<TipoServicio> ObtenerPorDescripcion(string descripcion)
        {
            throw new NotImplementedException();
        }

        public TipoServicio ObtenerPorCodigo(string codigo)
        {
            var tipoServicio = _tipoServicioRepositorio.FindBy(c => c.codigo == codigo).FirstOrDefault();
            return tipoServicio;
        }

        public void Eliminar(string codigo)
        {
            var vehiculoRepo = _tipoServicioRepositorio.FindBy(c => c.codigo == codigo).FirstOrDefault();
            _tipoServicioRepositorio.Delete(vehiculoRepo);

            _unitOfWork.Commit();
        }

        public TipoServicio Guardar(TipoServicio tipoServicio)
        {
            var tipoServicioRepo = new TipoServicio();
            if (tipoServicio.codigo == string.Empty)
            {
                tipoServicioRepo = _tipoServicioRepositorio.Add(tipoServicio);
            }
            else
            {
                tipoServicioRepo = _tipoServicioRepositorio.FindBy(c => c.codigo == tipoServicio.codigo).FirstOrDefault();
                tipoServicioRepo.descripcion = tipoServicio.descripcion;
                _tipoServicioRepositorio.Edit(tipoServicioRepo);
            }
            _unitOfWork.Commit();
            return tipoServicioRepo;
        }
    }

    public interface ITipoServicioService
    {
        TipoServicio Guardar(TipoServicio tipoServicio);
        List<TipoServicio> Obtener();
        List<TipoServicio> ObtenerPorDescripcion(string descripcion);
    }
}
