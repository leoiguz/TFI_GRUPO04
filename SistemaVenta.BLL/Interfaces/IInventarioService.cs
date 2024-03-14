using SistemaVenta.Entity;

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
