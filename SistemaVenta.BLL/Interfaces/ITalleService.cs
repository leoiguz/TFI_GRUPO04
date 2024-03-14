using SistemaVenta.Entity;

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
