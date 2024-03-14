using SistemaVenta.Entity;

namespace SistemaVenta.BLL.Interfaces
{
    public interface ITipoPagoService
    {
        Task<List<TipoPago>> Lista();
    }
}
