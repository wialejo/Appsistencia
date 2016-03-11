using Appsistencia.API.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appsistencia.API.Infraestructura
{
    public interface IDbFactory : IDisposable
    {
        ApplicationDbContext Init();
    }
}
