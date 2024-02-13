using SistemaVenta.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.BLL.Interfaces
{
    public interface ITipoTalleService
    {
        Task<List<TipoTalle>> Lista();

        Task<TipoTalle> Crear(TipoTalle entidad);

        Task<TipoTalle> Editar(TipoTalle entidad);

        Task<bool> Eliminar(int idTipoTalle);
    }
}
