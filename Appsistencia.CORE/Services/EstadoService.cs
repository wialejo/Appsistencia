﻿using Appsistencia.API.Infraestructura;
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
    public class EstadoService : IEstadoService
    {

        private readonly IEntityBaseRepository<Estado> _estadoRepositorio;
        private readonly IUnitOfWork _unitOfWork;

        public EstadoService(IEntityBaseRepository<Estado> estadoRepositorio, IUnitOfWork unitOfWork)
        {
            _estadoRepositorio = estadoRepositorio;
            _unitOfWork = unitOfWork;
        }

        public List<Estado> Obtener()
        {
            var estados = _estadoRepositorio.GetAll().ToList();
            return estados;
        }

        public List<Estado> ObtenerPorDescripcion(string descripcion)
        {
            throw new NotImplementedException();
        }

        public Estado ObtenerPorId(int id)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public Estado Guardar(Estado entidad)
        {
            throw new NotImplementedException();
        }

        public Estado ObtenerPorCodigo(string codigo)
        {
            var estadoRepo = _estadoRepositorio.FindBy(e => e.codigo == codigo).FirstOrDefault();
            return estadoRepo;
        }
    }
    public interface IEstadoService
    {
        Estado ObtenerPorCodigo(string codigo);
        List<Estado> Obtener();
    }
}
