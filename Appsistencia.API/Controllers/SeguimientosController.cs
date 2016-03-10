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
using System.Web;
using Microsoft.AspNet.Identity;
using Appsistencia.CORE.Services;
using Appsistencia.CORE.Modelos;

namespace Appsistencia.Core.Controllers
{
    [Authorize]
    public class SeguimientosController : ApiController
    {
        private readonly ISeguimientoService _seguimientoService;
        private readonly ISeguimientoService _seguimientoServiceExtend;

        public SeguimientosController(ISeguimientoService seguimientoService, ISeguimientoService seguimientoServiceExtend) {
            _seguimientoService = seguimientoService;
            _seguimientoServiceExtend = seguimientoServiceExtend;
        }

        
        [HttpGet]
        public IHttpActionResult ObtenerPorServicio(int id)
        {
            var seguimientos = _seguimientoServiceExtend.ObtenerPorServicio(id);

            return Ok(seguimientos);
        }

        [HttpPost]
        public IHttpActionResult Guardar(Seguimiento seguimiento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            seguimiento.usuarioRegistroId = HttpContext.Current.User.Identity.GetUserId();
            var seguimientoRepo = _seguimientoService.Guardar(seguimiento);

            return Ok(seguimientoRepo);
        }
    }
}