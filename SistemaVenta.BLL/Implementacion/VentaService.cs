using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using SistemaVenta.BLL.Interfaces;
using SistemaVenta.DAL.Interfaces;
using SistemaVenta.Entity;

namespace SistemaVenta.BLL.Implementacion
{
	public class VentaService : IVentaService
	{
		public Task<Venta> Detalle(string numeroVenta)
		{
			throw new NotImplementedException();
		}

		public Task<List<Venta>> Historial(string numeroVenta, string fechaInicio, string fechaFin)
		{
			throw new NotImplementedException();
		}

		public Task<List<Articulo>> ObtenerArticulos(string busqueda)
		{
			throw new NotImplementedException();
		}

		public Task<Venta> Registrar(Venta entidad)
		{
			throw new NotImplementedException();
		}

		public Task<List<DetalleVenta>> Reporte(string fechaInicio, string fechaFin)
		{
			throw new NotImplementedException();
		}
	}
}
