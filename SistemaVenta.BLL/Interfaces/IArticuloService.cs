using SistemaVenta.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.BLL.Interfaces
{
    public interface IArticuloService
    {
        Task<List<Articulo>> Lista();

        Task<Articulo> Crear(Articulo entidad);

        Task<Articulo> Editar(Articulo entidad);

        Task<bool> Eliminar(int idArticulo);
    }
}
