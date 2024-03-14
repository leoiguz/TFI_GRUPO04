using SistemaVenta.Entity;

namespace SistemaVenta.BLL.Interfaces
{
    public interface IMarcaService
    {
        Task<List<Marca>> Lista();

        Task<Marca> Crear(Marca entidad);

        Task<Marca> Editar(Marca entidad);

        Task<bool> Eliminar(int idMarca);
    }
}
