using SistemaVenta.Entity;

namespace SistemaVenta.BLL.Interfaces
{
    public interface IPagoService
    {
        Task<Pago> Crear(Pago entidad);
    }
}
