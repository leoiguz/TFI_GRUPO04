using SistemaVenta.Entity;

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
