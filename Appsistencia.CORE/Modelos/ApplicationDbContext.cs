using Appsistencia.API.Migrations.ApplicationDbContext;
using Appsistencia.CORE.Modelos;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Appsistencia.API.Modelos
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public ApplicationDbContext() : base("name=AppsistenciaDBConnectionString")
        {
            Database.SetInitializer<ApplicationDbContext>(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Servicio> Servicios { get; set; }

        public DbSet<Estado> Estados { get; set; }

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Proveedor> Proveedores { get; set; }

        public DbSet<TipoServicio> TiposServicio { get; set; }

        public DbSet<Vehiculo> Vehiculos { get; set; }

        public DbSet<Seguimiento> Seguimientos { get; set; }

        public DbSet<Correo> Correos { get; set; }

        public DbSet<Cuenta> Cuentas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });



            //modelBuilder.Entity<Servicio>()
            //    .HasOptional(A => A.Asegurado);

            //modelBuilder.Entity<Servicio>()
            //    .HasOptional(C => C.Conductor);
            
            

            //modelBuilder.Entity<Direccion>()
            //    .HasRequired(C => C.Ciudad)
            //    .WithMany(d => d.Direcciones)
            //    .HasForeignKey(d => d.CiudadId);


            // modelBuilder.Entity<Servicio>()
            //.Property(l => l.Id)
            //.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }

    }
}
