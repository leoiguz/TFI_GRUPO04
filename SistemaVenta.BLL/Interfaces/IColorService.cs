using SistemaVenta.Entity;

namespace SistemaVenta.BLL.Interfaces
{
    public interface IColorService
    {
        Task<List<Color>> Lista();

        Task<Color> Crear(Color entidad);

        Task<Color> Editar(Color entidad);

        Task<bool> Eliminar(int idColor);
    }
}
