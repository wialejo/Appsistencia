namespace Appsistencia.API.Migrations.ApplicationDbContext
{
    using API.Modelos;
    using CORE.Modelos;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Appsistencia.API.Modelos.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;

        }

        protected override void Seed(Appsistencia.API.Modelos.ApplicationDbContext context)
        {
            context.Users.AddOrUpdate(u => u.UserName,
                new ApplicationUser
                {
                    nombre = "Admininstrador del sistema",
                    UserName = "admin",
                    Email = "wi_alejo@hotmail.com",
                    PasswordHash = new PasswordHasher().HashPassword("admin"),
                    PhoneNumber = "3175104254",
                    SecurityStamp = Guid.NewGuid().ToString()

                });

            context.Clientes.AddOrUpdate(new Cliente
            {
                nombre = "Pepito",
                apellido = "Pelaz",
                email = "pepito.pelaez@tualiado.com",
                identificacion = "88888888",
                telefono = "3156789012",
                usuario = "admin"
            });

            context.Estados.AddOrUpdate(
                new Estado() { codigo = "RG", descripcion = "Registrado", orden = 10 },
                new Estado() { codigo = "ES", descripcion = "En sitio", orden = 20 },
                new Estado() { codigo = "EC", descripcion = "En curso", orden = 30 },
                new Estado() { codigo = "TE", descripcion = "Terminado", orden = 40 },
                new Estado() { codigo = "FL", descripcion = "Fallido", orden = 50 },
                new Estado() { codigo = "CN", descripcion = "Cancelado", orden = 60 },
                new Estado() { codigo = "AN", descripcion = "Anulado", orden = 70 },
                new Estado() { codigo = "EN", descripcion = "Enviado", orden = 90 },
                new Estado() { codigo = "AS", descripcion = "Asignado", orden = 110 },
                new Estado() { codigo = "RE", descripcion = "Recibido", orden = 10 }
            );

            context.TiposServicio.AddOrUpdate(
                new TipoServicio() { codigo = "CE", descripcion = "Conductor elegido", iconoRuta= "assets/iconos_web/conductorElegido.svg" },
                new TipoServicio() { codigo = "GR", descripcion = "Grúa", iconoRuta = "assets/iconos_web/grua.svg" },
                new TipoServicio() { codigo = "CT", descripcion = "Carro taller", iconoRuta = "assets/iconos_web/taller.svg" },
                new TipoServicio() { codigo = "CR", descripcion = "Cerrajero", iconoRuta = "assets/iconos_web/cerrajero.svg" }
            );


            context.Cuentas.AddOrUpdate(new Cuenta
            {
                Id = 1,
                Descripcion = "TuAliado",
                CorreoSalida = "arhcontroldrive@gmail.com",
                NombreMostrar = "TuAliado",
                CorreoRespuesta = "arhcontroldrive@gmail.com",
                NombreServidor = "smtp.gmail.com",
                NombreServidorIMAP = "smtp.gmail.com",
                NombreServidorPOP = "smtp.gmail.com",
                NombreServidorSMPT = "smtp.gmail.com",
                Puerto = 587,
                Ssl = true,
                Usuario = "arhcontroldrive@gmail.com",
                Contrasena = "Loreka8812"
            });
        }
    }
}
