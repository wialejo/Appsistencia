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
using Appsistencia.API.Modelos;
using Appsistencia.CORE.Services;
using Appsistencia.CORE.Modelos;

namespace Appsistencia.API.Controllers
{
    [Authorize]
    public class EstadosController : ApiController
    {
        private readonly IEstadoService _estadoService;

        public EstadosController(IEstadoService estadoService) {
            _estadoService = estadoService;

        }

        [HttpGet]
        public IHttpActionResult Obtener()
        {
            var estados = _estadoService.Obtener();
            return Ok(estados);
        }
    }
}