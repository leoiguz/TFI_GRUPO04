using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaVenta.BLL.Interfaces;
using SistemaVenta.DAL.Interfaces;
using SistemaVenta.Entity;

namespace SistemaVenta.BLL.Implementacion
{
	public class TipoComprobanteService : ITipoComprobanteService
	{
		private readonly IGenericRepository<TipoComprobante> _repositorio;

        public TipoComprobanteService(IGenericRepository<TipoComprobante> repositorio)
        {
			_repositorio = repositorio;
        }
        public async Task<List<TipoComprobante>> Lista()
		{
			IQueryable<TipoComprobante> querry = await _repositorio.Consultar();
			return querry.ToList();
		}
	}
}
