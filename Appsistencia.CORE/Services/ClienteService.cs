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
    public class ClienteService : IClienteService
    {

        private readonly IEntityBaseRepository<Cliente> _clienteRepositorio;
        private readonly IDireccionService _direccionService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVehiculoService _vehiculosService;

        public ClienteService(IEntityBaseRepository<Cliente> clienteRepositorio, IVehiculoService vehiculosService, IDireccionService direccionService, IUnitOfWork unitOfWork)
        {
            _clienteRepositorio = clienteRepositorio;
            _direccionService = direccionService;
            _unitOfWork = unitOfWork;
            _vehiculosService = vehiculosService;
        }

        public Cliente Guardar(Cliente cliente)
        {
            var clienteRepo = new Cliente();
            if (cliente.id == 0)
            {
                clienteRepo = _clienteRepositorio.Add(cliente);
            }
            else
            {
                clienteRepo = _clienteRepositorio.FindBy(c => c.id == cliente.id).FirstOrDefault();
                clienteRepo.nombre = cliente.nombre;
                clienteRepo.apellido = cliente.apellido;
                clienteRepo.telefono = cliente.telefono;
                clienteRepo.email = cliente.email;
                clienteRepo.identificacion = cliente.identificacion;

                cliente.direcciones.ForEach(direccion => {
                    clienteRepo.direcciones.Add(_direccionService.Guardar(direccion));
                });

                cliente.vehiculos.ForEach(vehiculo => {
                    clienteRepo.vehiculos.Add(_vehiculosService.Guardar(vehiculo));
                });

                _clienteRepositorio.Edit(clienteRepo);
            }
            cliente = clienteRepo;
            _unitOfWork.Commit();
            return cliente;
        }

        public List<Cliente> Obtener()
        {
            var clientes = _clienteRepositorio.GetAll().ToList();
            return clientes;
        }

        public List<Cliente> ObtenerPorDescripcion(string descripcion)
        {
            throw new NotImplementedException();
        }
       
        public Cliente ObtenerPorId(int id)
        {
            var clienteRepo = _clienteRepositorio.FindBy(e => e.id == id).FirstOrDefault();
            return clienteRepo;
        }
        public Cliente ObtenerPorUsuario(string usuario)
        {
            var clienteRepo = _clienteRepositorio.FindBy(e => e.usuario == usuario).FirstOrDefault();
            return clienteRepo;
        }
    }
    public interface IClienteService
    {
        Cliente ObtenerPorId(int id);
        Cliente ObtenerPorUsuario(string usuario);
        List<Cliente> Obtener();
        Cliente Guardar(Cliente cliente);
    }
}
