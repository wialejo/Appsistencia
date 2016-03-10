using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Security.Principal;
using Appsistencia.Core.Modelos;

namespace Appsistencia.CORE.Modelos
{
    public class Servicio
    {
        [Key]
        public int id { get; set; }

        public string tipoServicioCodigo { get; set; }
        public virtual TipoServicio tipoServicio { get; set; }

        public string estadoCodigo { get; set; }
        public virtual Estado estado { get; set; }
        
        public DateTime fecha { get; set; }

        public TimeSpan hora { get; set; }

        public int? vehiculoId { get; set; }
        public virtual Vehiculo vehiculo { get; set; }

        public int? proveedorId { get; set; }
        public virtual Proveedor proveedor { get; set; }

        public int clienteId { get; set; }
        public virtual Cliente cliente { get; set; }


        public int? direccionInicioId { get; set; }
        public virtual Direccion direccionInicio { get; set; }

        public int? direccionDestinoId { get; set; }
        public virtual Direccion direccionDestino { get; set; }

        public DateTime fechaRegistro { get; internal set; }
        public DateTime fechaModificacion { get; internal set; }

        public int? usuarioRegistroId { get; set; }
        public virtual ApplicationUser usuarioRegistro { get; set; }

        public int? usuarioModificacionId { get; set; }
        public virtual ApplicationUser usuarioModificacion { get; set; }

        public ICollection<Seguimiento> seguimientos { get; set; }
    }

    [Table("Seguimientos")]
    public class Seguimiento
    {
        [Key]
        public int id { get; set; }
        public int servicioId { get; set; }
        public DateTime fecha { get; set; }
        public string observacion { get; set; }
        public string usuarioRegistroId { get; set; }
        public virtual ApplicationUser usuarioRegistro { get; set; }
        public string nuevoEstado { get; set; }
    }
    
    public class Estado
    {
        [Key]
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public int orden { get; set; }
    }

    public class Vehiculo
    {
        [Key]
        public int id { get; set; }
        public string placa { get; set; }
        public string marca { get; set; }
        public string referencia { get; set; }
        public string observaciones { get; set; }
    }

    public class Direccion
    {
        [Key]
        public int id { get; set; }
        public string descripcion { get; set; }
        public string ciudad { get; set; }
        public string barrio { get; set; }
    }
    public class TipoServicio
    {
        [Key]
        public string codigo { get; set; }
        public string descripcion { get; set; }
    }

    public class Proveedor {
        [Key]
        public int id { get; set; }
        public string nombre { get; set; }
        public string email { get; set; }
    }

    public class Cliente {
        [Key]
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string identificacion { get; set; }
        public string email { get; set; }
        public string telefono { get; set; }
        public string telefonoAux { get; set; }
        public virtual List<Vehiculo> vehiculos { get; set; }
        public virtual List<Direccion> direcciones { get; set; }
        public string usuario { get; set; }
    }
}