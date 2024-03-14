using SistemaVenta.Entity;

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
