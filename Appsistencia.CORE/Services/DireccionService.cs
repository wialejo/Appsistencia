using Appsistencia.Core.Infraestructura;
using Appsistencia.CORE.Modelos;
using Appsistencia.CORE.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appsistencia.CORE.Services
{
    public class DireccionService : IDireccionService
    {

        private readonly IEntityBaseRepository<Direccion> _direccionRepositorio;
        private readonly IUnitOfWork _unitOfWork;

        public DireccionService(IEntityBaseRepository<Direccion> direccionRepositorio, IUnitOfWork unitOfWork)
        {
            _direccionRepositorio = direccionRepositorio;
            _unitOfWork = unitOfWork;
        }

        public void Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public Direccion Guardar(Direccion direccion)
        {
            var direccionRepo = new Direccion();
            if (direccion.id == 0)
            {
                direccionRepo = _direccionRepositorio.Add(direccion);
            }
            else {
                direccionRepo = _direccionRepositorio.FindBy(d => d.id == direccion.id).FirstOrDefault();
                direccionRepo.descripcion = direccion.descripcion;
                direccionRepo.ciudad = direccion.ciudad;
                direccionRepo.barrio = direccion.barrio;

                _direccionRepositorio.Edit(direccionRepo);
            }
            _unitOfWork.Commit();

            return direccionRepo;
        }


        public List<Direccion> Obtener()
        {
            throw new NotImplementedException();
        }

        public List<Direccion> ObtenerPorDescripcion(string descripcion)
        {
            throw new NotImplementedException();
        }

        public Direccion ObtenerPorId(int id)
        {
            throw new NotImplementedException();
        }
    }

    public interface IDireccionService
    {
        Direccion Guardar(Direccion direccion);

    }
}
