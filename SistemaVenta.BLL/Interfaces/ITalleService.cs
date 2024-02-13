using SistemaVenta.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.BLL.Interfaces
{
    public interface ITalleService
    {
        Task<List<Talle>> Lista();

        Task<Talle> Crear(Talle entidad);

        Task<Talle> Editar(Talle entidad);

        Task<bool> Eliminar(int idTalle);
    }
}
