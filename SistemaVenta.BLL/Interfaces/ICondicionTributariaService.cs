using SistemaVenta.Entity;

namespace SistemaVenta.BLL.Interfaces
{
    public interface ICondicionTributariaService
    {
        Task<List<CondicionTributaria>> Lista();
    }
}
