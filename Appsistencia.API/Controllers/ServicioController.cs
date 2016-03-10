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
using System.Diagnostics;
using Appsistencia.CORE.Modelos;
using Appsistencia.CORE.Infraestructura;
using Appsistencia.CORE.Services;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http.Headers;
using System.Web;

namespace Appsistencia.Core.Controllers
{
    [Authorize]
    public class ServicioController : ApiController
    {
        private IServicioService _servicioService;

        public ServicioController(IServicioService servicioService)
        {
            _servicioService = servicioService;
        }

        [HttpPost]
        public IHttpActionResult Obtener()
        {   
            var servicios = _servicioService.Obtener().ToList();
            return Ok(servicios);
        }
        
        [HttpGet]
        public IHttpActionResult ObtenerPorId(int id)
        {
            var servicio = _servicioService.ObtenerPorId(id);
            return Ok(servicio);
        }

        [HttpPost]
        public IHttpActionResult Guardar(Servicio servicio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var servicioModeloVista = _servicioService.Guardar(servicio);

            return Ok(servicioModeloVista);
        }
    }
}