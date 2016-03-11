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
                    Nombre = "Admininstrador del sistema",
                    UserName = "admin",
                    Email = "wi_alejo@hotmail.com",
                    PasswordHash = new PasswordHasher().HashPassword("admin"),
                    PhoneNumber = "3175104254",
                    SecurityStamp = Guid.NewGuid().ToString()

                });

            context.Estados.AddOrUpdate(
                new Estado() { codigo = "RG", descripcion = "Registrado", orden = 1 },
                new Estado() { codigo = "ES", descripcion = "En sitio", orden = 2 },
                new Estado() { codigo = "EC", descripcion = "En curso", orden = 3 },
                new Estado() { codigo = "TE", descripcion = "Terminado", orden = 4 },
                new Estado() { codigo = "FL", descripcion = "Fallido", orden = 5 },
                new Estado() { codigo = "CN", descripcion = "Cancelado", orden = 6 },
                new Estado() { codigo = "AN", descripcion = "Anulado", orden = 7 },
                new Estado() { codigo = "RE", descripcion = "Recibido", orden = 10 },
                new Estado() { codigo = "EN", descripcion = "Enviado", orden = 9 }
            );

            context.TiposServicio.AddOrUpdate(
                new TipoServicio() { codigo = "CE", descripcion = "Conductor elegido"},
                new TipoServicio() { codigo = "GR", descripcion = "Grúa" },
                new TipoServicio() { codigo = "CT", descripcion = "Carro taller" },
                new TipoServicio() { codigo = "CR", descripcion = "Cerrajero" }
            );

            //context.Conductores.AddOrUpdate(
            //    new Conductor()
            //    {
            //        Id = 1,
            //        Nombre = "Javier conductor martinez",
            //        Email = "wi_alejo@hotmail.com",
            //        TipoIdentificacion = "CC",
            //        Identificacion = 1018421359,
            //        Telefono1 = "3112150087",
            //        Direccion = "Cra 51 # 66a 22 Br San miguel",
            //        Activo = true
            //    });

            //context.Asegurados.AddOrUpdate(
            //    new Asegurado() { Id = 1, Nombre = "Asegurado 1" }
            //);

            //context.Cuentas.AddOrUpdate(new Cuenta
            //{
            //    Id = 1,
            //    Descripcion = "ControlDrive",
            //    CorreoSalida = "Notificaciones@controldrive.com.co",
            //    NombreMostrar = "ControlDrive",
            //    CorreoRespuesta = "Notificaciones@controldrive.com.co",
            //    NombreServidor = "controldrive.com.co",
            //    NombreServidorIMAP = "controldrive.com.co",
            //    NombreServidorPOP = "controldrive.com.co",
            //    NombreServidorSMPT = "controldrive.com.co",
            //    Puerto = 25,
            //    Ssl = false,
            //    Usuario = "Notificaciones@controldrive.com.co",
            //    Contrasena = "Loreka8812"
            //});

            context.Cuentas.AddOrUpdate(new Cuenta
            {
                Id = 1,
                Descripcion = "ControlDrive",
                CorreoSalida = "arhcontroldrive@gmail.com",
                NombreMostrar = "ARH - ControlDrive",
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
