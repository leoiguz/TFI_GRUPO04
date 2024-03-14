using SistemaVenta.Entity;

namespace SistemaVenta.BLL.Interfaces
{
	public interface ITipoComprobanteService
	{
		Task<List<TipoComprobante>> Lista();
	}
}
