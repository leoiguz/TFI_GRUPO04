using SistemaVenta.BLL.Interfaces;
using SistemaVenta.DAL.Interfaces;
using SistemaVenta.Entity;

namespace SistemaVenta.BLL.Implementacion
{
    public class TipoPagoService :ITipoPagoService
    {
        private readonly IGenericRepository<TipoPago> _repositorio;

        public TipoPagoService(IGenericRepository<TipoPago> repositorio)
        {
            _repositorio = repositorio;

        }


        public async Task<List<TipoPago>> Lista()
        {
            IQueryable<TipoPago> query = await _repositorio.Consultar();
            return query.ToList();
        }
    }
}
