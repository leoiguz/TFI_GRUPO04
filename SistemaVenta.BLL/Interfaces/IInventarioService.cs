using SistemaVenta.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.BLL.Interfaces
{
    public interface IInventarioService
    {
        Task<List<Inventario>> Lista();

        Task<Inventario> Crear(Inventario entidad);

        Task<Inventario> Editar(Inventario entidad);

        Task<bool> Eliminar(int idInventario);
    }
}
