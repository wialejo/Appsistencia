using Autofac;
using Autofac.Core;
using Autofac.Integration.WebApi;
using Appsistencia.API.Infraestructura;
using Appsistencia.API.Modelos;
using Appsistencia.CORE.Modelos;
using Appsistencia.CORE.Repositorios;
using Appsistencia.CORE.Services;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace Appsistencia.API.App_Start
{
    public class AutofacWebApiConfig
    {
        public static IContainer Container;
        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }

        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<ApplicationDbContext>()
                   .As<IdentityDbContext<ApplicationUser>>()
                   .InstancePerRequest();

            builder.RegisterType<DbFactory>()
                .As<IDbFactory>()
                .InstancePerRequest();

            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerRequest();

            builder.RegisterGeneric(typeof(EntityBaseRepository<>))
                   .As(typeof(IEntityBaseRepository<>))
                   .InstancePerRequest();

            //// Services
            builder.RegisterType<ServicioService>()
                .As<IServicioService>()
                .InstancePerRequest();

            builder.RegisterType<SeguimientoService>()
                .As<ISeguimientoService>()
                .InstancePerRequest();

            builder.RegisterType<EstadoService>()
                .As<IEstadoService>()
                .InstancePerRequest();

            builder.RegisterType<DireccionService>()
                .As<IDireccionService>()
                .InstancePerRequest();

            builder.RegisterType<VehiculoService>()
                .As<IVehiculoService>()
                .InstancePerRequest();

            builder.RegisterType<CorreoService>()
                .As<ICorreoService>()
                .InstancePerRequest();

            builder.RegisterType<TipoServicioService>()
                .As<ITipoServicioService>()
                .InstancePerRequest();

            builder.RegisterType<ClienteService>()
                .As<IClienteService>()
                .InstancePerRequest();

            Container = builder.Build();

            return Container;
        }
    }
}