using SistemaVenta.Entity;

namespace SistemaVenta.BLL.Interfaces
{
    public interface ISucursalService
    {
        Task<Sucursal> Obtener();

        Task<Sucursal> GuardarCambios(Sucursal entidad);
    }
}
