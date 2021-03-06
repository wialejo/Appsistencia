﻿using System;
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
using System.Diagnostics;
using Appsistencia.CORE.Modelos;
using Appsistencia.CORE.Infraestructura;
using Appsistencia.CORE.Services;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http.Headers;
using System.Web;

namespace Appsistencia.API.Controllers
{
    [Authorize]
    public class ServiciosController : ApiController
    {
        private IServicioService _servicioService;

        public ServiciosController(IServicioService servicioService)
        {
            _servicioService = servicioService;
        }

        [HttpGet]
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

        [HttpPost]
        [Route("api/Servicios/{servicioId}/proveedor/{proveedorId}/tiempoEstimado/{tiempoEstimado}")]
        public IHttpActionResult AsignarProveedor([FromUri]int servicioId, [FromUri]int proveedorId, [FromUri]int tiempoEstimado) {
            _servicioService.AsignarProveedor(servicioId, proveedorId, tiempoEstimado);
            return Ok();
        }
    }
}