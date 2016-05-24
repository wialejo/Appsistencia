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
    public class VehiculoService : IVehiculoService
    {

        private readonly IEntityBaseRepository<Vehiculo> _vehiculoRepositorio;
        private readonly IUnitOfWork _unitOfWork;

        public VehiculoService(IEntityBaseRepository<Vehiculo> vehiculoRepositorio, IUnitOfWork unitOfWork)
        {
            _vehiculoRepositorio = vehiculoRepositorio;
            _unitOfWork = unitOfWork;
        }

        public List<Vehiculo> Obtener()
        {
            var vehiculoes = _vehiculoRepositorio.GetAll().ToList();
            return vehiculoes;
        }

        public List<Vehiculo> ObtenerPorDescripcion(string descripcion)
        {
            throw new NotImplementedException();
        }

        public Vehiculo ObtenerPorId(int id)
        {
            var vehiculo = _vehiculoRepositorio.FindBy(c => c.id == id).FirstOrDefault();
            return vehiculo;
        }

        public void Eliminar(int id)
        {
            var vehiculoRepo = _vehiculoRepositorio.FindBy(c => c.id == id).FirstOrDefault();
            _vehiculoRepositorio.Delete(vehiculoRepo);

            _unitOfWork.Commit();
        }

        public Vehiculo Guardar(Vehiculo vehiculo)
        {
            var vehiculoRepo = new Vehiculo();
            if (vehiculo.id == 0)
            {
                vehiculoRepo = _vehiculoRepositorio.Add(vehiculo);
            }
            else
            {
                vehiculoRepo = _vehiculoRepositorio.FindBy(c => c.id == vehiculo.id).FirstOrDefault();
                vehiculoRepo.placa = vehiculo.placa;
                vehiculoRepo.descripcion = vehiculo.descripcion;
                _vehiculoRepositorio.Edit(vehiculoRepo);
            }
            _unitOfWork.Commit();
            vehiculo = vehiculoRepo;
            return vehiculo;
        }
    }

    public interface IVehiculoService{
        Vehiculo Guardar(Vehiculo vehiculo);
        List<Vehiculo> Obtener();
        List<Vehiculo> ObtenerPorDescripcion(string descripcion);
    }
}
