using Microsoft.EntityFrameworkCore;
using SistemaVenta.BLL.Interfaces;
using SistemaVenta.DAL.Interfaces;
using SistemaVenta.Entity;
using System.Globalization;

namespace SistemaVenta.BLL.Implementacion
{
    public class DashBoardService : IDashBoardService
    {
        private readonly IVentaRepository _repositorioVenta;
        private readonly IGenericRepository<DetalleVenta> _repositorioDetalleVenta;
        private readonly IGenericRepository<Articulo> _repositorioArticulo;
        private readonly IGenericRepository<Inventario> _repositorioInventario;
        private DateTime FechaInicio = DateTime.Now;

        public DashBoardService(
            IVentaRepository repositorioVenta,
            IGenericRepository<DetalleVenta> repositorioDetalleVenta,
             IGenericRepository<Articulo> repositorioArticulo,
              IGenericRepository<Inventario> repositorioInventario
            )
        {
            _repositorioVenta = repositorioVenta;
            _repositorioDetalleVenta = repositorioDetalleVenta;
            _repositorioArticulo = repositorioArticulo;
            _repositorioInventario = repositorioInventario;

            FechaInicio = FechaInicio.AddDays(-7);
        }

        public async Task<int> TotalVentasUltimaSemana()
        {
            try
            {
                IQueryable<Venta> query = await _repositorioVenta.Consultar(v => v.FechaRegistro.Value.Date >= FechaInicio.Date);
                int total = query.Count();
                return total;
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> TotalIngresosUltimaSemana()
        {
            try
            {
                IQueryable<Venta> query = await _repositorioVenta.Consultar(v => v.FechaRegistro.Value.Date >= FechaInicio.Date);

                decimal resultado = query
                    .Select(v => v.NetoGravado)
                    .Sum(v => v.Value);

                return Convert.ToString(Convert.ToDecimal(resultado)/100, new CultureInfo("es-AR"));
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> TotalInventarios()
        {
            try
            {
                IQueryable<Inventario> query = await _repositorioInventario.Consultar();
                int total = query.Count();
                return total;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> TotalArticulos()
        {
            try
            {
                IQueryable<Articulo> query = await _repositorioArticulo.Consultar();
                int total = query.Count();
                return total;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Dictionary<string, int>> VentasUltimaSemana()
        {
            try
            {

                IQueryable<Venta> query = await _repositorioVenta
                    .Consultar(v => v.FechaRegistro.Value.Date >= FechaInicio.Date);


                Dictionary<string, int> resultado = query
                    .GroupBy(v => v.FechaRegistro.Value.Date).OrderByDescending(g => g.Key)
                    .Select(dv => new { fecha = dv.Key.ToString("dd/MM/yyyy"), total = dv.Count() })
                    .ToDictionary(keySelector: r => r.fecha, elementSelector: r => r.total);

                return resultado;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Dictionary<string, int>> InventariosTopUltimaSemana()
        {
            try
            {

                IQueryable<DetalleVenta> query = await _repositorioDetalleVenta.Consultar();


                Dictionary<string, int> resultado = query
                    .Include(v => v.IdVentaNavigation)
                    .Where(dv => dv.IdVentaNavigation.FechaRegistro.Value.Date >= FechaInicio.Date)
                    .GroupBy(dv => dv.IdInventarioNavigation.IdArticuloNavigation.Descripcion).OrderByDescending(g => g.Count())
                    .Select(dv => new { inventario = dv.Key, total = dv.Count() }).Take(4)
                    .ToDictionary(keySelector: r => r.inventario, elementSelector: r => r.total);

                return resultado;
            }
            catch
            {
                throw;
            }
        }




    }
}
