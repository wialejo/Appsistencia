using Appsistencia.CORE.Modelos;
using Appsistencia.CORE.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ControlDrive.API.Controllers
{
    [Authorize]
    public class TipoServicioController : ApiController
    {
        private ITipoServicioService tipoServicioService;

        public TipoServicioController(ITipoServicioService servicioService)
        {
            tipoServicioService = servicioService;
        }
        
        [HttpGet]
        public IHttpActionResult Obtener()
        {
            var servicios = tipoServicioService.Obtener().ToList();
            return Ok(servicios);
        }

        [HttpPost]
        public IHttpActionResult Guardar(TipoServicio tipoServicio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var servicioModeloVista = tipoServicioService.Guardar(tipoServicio);

            return Ok(servicioModeloVista);
        }
    }
}
