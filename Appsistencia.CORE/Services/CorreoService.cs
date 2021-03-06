﻿using Appsistencia.CORE.Infraestructura;
using Appsistencia.CORE.Modelos;
using Appsistencia.CORE.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appsistencia.CORE.Services
{
    public class CorreoService: ICorreoService
    {
        private readonly IEntityBaseRepository<Cuenta> _cuentaRepositorio;

        public CorreoService(IEntityBaseRepository<Cuenta> cuentaRepositorio)
        {
            _cuentaRepositorio = cuentaRepositorio;
        }

        public void Enviar(List<Correo> correos)
        {
            var cuenta =  _cuentaRepositorio.GetAll().FirstOrDefault();
            var SmtpClient = Smtp.IniciarSmtpClient(cuenta);
            Smtp.EnviarMensajesAClienteSMTP(correos, cuenta, SmtpClient);
        }
        public void Enviar(Correo correo)
        {
            var cuenta = _cuentaRepositorio.GetAll().FirstOrDefault();
            var SmtpClient = Smtp.IniciarSmtpClient(cuenta);
            var correos = new List<Correo>();
            correos.Add(correo);
            Smtp.EnviarMensajesAClienteSMTP(correos, cuenta, SmtpClient);
        }

    }

    public interface ICorreoService
    {
        void Enviar(List<Correo> correos);
        void Enviar(Correo correo);
    }
}
