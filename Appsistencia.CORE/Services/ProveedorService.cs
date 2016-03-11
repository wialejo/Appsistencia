
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
    public class ProveedorService : IProveedorService
    {

        private readonly IEntityBaseRepository<Proveedor> _proveedorRepositorio;
        private readonly IUnitOfWork _unitOfWork;

        public ProveedorService(IEntityBaseRepository<Proveedor> proveedorRepositorio, IUnitOfWork unitOfWork)
        {
            _proveedorRepositorio = proveedorRepositorio;
            _unitOfWork = unitOfWork;
        }

        public Proveedor Guardar(Proveedor proveedor)
        {
            var proveedorRepo = new Proveedor();
            if (proveedor.id == 0)
            {
                proveedorRepo = _proveedorRepositorio.Add(proveedor);
            }
            else
            {
                proveedorRepo = _proveedorRepositorio.FindBy(c => c.id == proveedor.id).FirstOrDefault();
                proveedorRepo.nombre = proveedor.nombre;
                proveedorRepo.telefono = proveedor.telefono;
                proveedorRepo.telefono2 = proveedor.telefono2;
                proveedorRepo.email = proveedor.email;

                _proveedorRepositorio.Edit(proveedorRepo);
            }
            proveedor = proveedorRepo;
            _unitOfWork.Commit();
            return proveedor;
        }

        public List<Proveedor> Obtener()
        {
            var proveedors = _proveedorRepositorio.GetAll().ToList();
            return proveedors;
        }

        public List<Proveedor> ObtenerPorDescripcion(string descripcion)
        {
            var proveedors = _proveedorRepositorio.FindBy(p => p.nombre.ToLower().Contains(descripcion.ToLower())).ToList();
            return proveedors;
        }
       
        public Proveedor ObtenerPorId(int id)
        {
            var proveedorRepo = _proveedorRepositorio.FindBy(e => e.id == id).FirstOrDefault();
            return proveedorRepo;
        }
    }
    public interface IProveedorService
    {
        Proveedor ObtenerPorId(int id);
        List<Proveedor> ObtenerPorDescripcion(string descripcion);
        List<Proveedor> Obtener();
        Proveedor Guardar(Proveedor proveedor);
    }
}
