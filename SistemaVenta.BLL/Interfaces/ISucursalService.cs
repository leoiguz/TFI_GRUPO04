using SistemaVenta.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.BLL.Interfaces
{
    public interface ISucursalService
    {
        Task<Sucursal> Obtener();

        Task<Sucursal> GuardarCambios(Sucursal entidad);
    }
}
