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
    public class ProveedoresController : ApiController
    {
        private readonly IProveedorService _proveeedorService;

        public ProveedoresController(IProveedorService proveeedorService) {
            _proveeedorService = proveeedorService;

        }

        //[HttpGet]
        //public IHttpActionResult Obtener()
        //{
        //    var proveeedores = _proveeedorService.Obtener();
        //    return Ok(proveeedores);
        //}

        [HttpPost]
        public IHttpActionResult Guardar(Proveedor proveedor)
        {
            var proveedorRepo = _proveeedorService.Guardar(proveedor);
            return Ok(proveedorRepo);
        }

        [HttpGet]
        [Route("api/Proveedores")]
        public IHttpActionResult Obtener([FromUri]int top, [FromUri] string descripcion)
        {
            var proveeedor = _proveeedorService.ObtenerPorDescripcion(descripcion, top);
            return Ok(proveeedor);
        }
    }
}