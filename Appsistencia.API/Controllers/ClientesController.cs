using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Appsistencia.Core.Modelos;
using Appsistencia.CORE.Services;
using Appsistencia.CORE.Modelos;

namespace Appsistencia.Core.Controllers
{
    [Authorize]
    public class ClientesController: ApiController
    {
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService) {
            _clienteService = clienteService;

        }

        [HttpGet]
        public IHttpActionResult Obtener()
        {
            var clientes = _clienteService.Obtener();
            return Ok(clientes);
        }

        [HttpGet]
        public IHttpActionResult ObtenerPorUsuario(string id)
        {
            var cliente = _clienteService.ObtenerPorUsuario(id);
            return Ok(cliente);
        }
    }
}