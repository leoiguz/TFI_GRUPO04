using System;
using System.Collections.Generic;
using System.Globalization;
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
        private readonly IGenericRepository<Inventario> _repositorioInventario;
        private readonly IVentaRepository _repositorioVenta;
        private readonly IGenericRepository<Cliente> _repositorioCliente;
        private readonly IGenericRepository<TipoComprobante> _repositorioTipoComprobante;
        IGenericRepository<CondicionTributaria> _repositorioCondicionTributaria;

        public VentaService(IGenericRepository<Inventario> repositorioInventario,
           IVentaRepository repositorioVenta,
           IGenericRepository<Cliente> repositorioCliente,
           IGenericRepository<TipoComprobante> repositorioTipoComprobante,
            IGenericRepository<CondicionTributaria> repositorioCondicionTributaria
           )
        {
            _repositorioInventario = repositorioInventario;
            _repositorioVenta = repositorioVenta;
            _repositorioCliente = repositorioCliente;
            _repositorioTipoComprobante = repositorioTipoComprobante;
            _repositorioCondicionTributaria = repositorioCondicionTributaria;
        }
        public async Task<List<Inventario>> ObtenerInventario(string busqueda)
        {
            IQueryable<Inventario> querry = await _repositorioInventario.Consultar(
                i => i.EsActivo == true &&
                i.Cantidad > 0 &&
                i.IdArticuloNavigation.CodigoBarra.Contains(busqueda)
                );

            return querry.Include(c => c.IdArticuloNavigation).
                Include(c => c.IdColorNavigation).
                Include(c => c.IdTalleNavigation).ToList();
        }

        public async Task<Venta> Registrar(Venta entidad)
        {
            try
            {
                return await _repositorioVenta.Registrar(entidad);
            }
            catch
            {
                throw;
            }
        }
        public async Task<List<Venta>> Historial(string numeroVenta, string fechaInicio, string fechaFin)
        {
            IQueryable<Venta> querry = await _repositorioVenta.Consultar();//Consultar() va sin filtro ya que queremos todas las ventas
            fechaInicio = fechaInicio is null ? "" : fechaInicio; //si es null FechaInicio = "" 
            fechaInicio = fechaFin is null ? "" : fechaFin;

            if (fechaInicio != "" && fechaFin != "")
            {
                DateTime fech_inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-AR"));
                DateTime fech_fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-AR"));

                return querry.Where(v =>
                v.FechaRegistro.Value.Date >= fech_inicio.Date &&
                v.FechaRegistro.Value.Date <= fech_fin.Date //filtramos ventas dentro del rango fecha inicio-fechafin
                    )
                    .Include(tdv => tdv.IdTipoComprobanteNavigation)
                    .Include(c => c.IdClienteNavigation)
                    .Include(u => u.IdUsuarioNavigation)
                    .Include(dv => dv.DetalleVenta)
                    .ToList();
            }
            else
            {
                return querry.Where(v =>
                v.NumeroComprobante == numeroVenta
                    )
                    .Include(tdv => tdv.IdTipoComprobanteNavigation)
                    .Include(c => c.IdClienteNavigation)
                    .Include(u => u.IdUsuarioNavigation)
                    .Include(dv => dv.DetalleVenta)
                    .ToList();
            }

        }
        public async Task<Venta> Detalle(string numeroVenta) //solo para una venta en especifico
        {
            IQueryable<Venta> querry = await _repositorioVenta.Consultar(v => v.NumeroComprobante == numeroVenta);

            return querry
                     .Include(tdv => tdv.IdTipoComprobanteNavigation)
                     .Include(c => c.IdClienteNavigation)
                     .Include(u => u.IdUsuarioNavigation)
                     .Include(dv => dv.DetalleVenta)
                     .First(); //devovemos todos estos atributos de la primera venta que se encuentre
        }
        public async Task<List<DetalleVenta>> Reporte(string fechaInicio, string fechaFin)
        {
            DateTime fech_inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-AR"));
            DateTime fech_fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-AR"));

            List<DetalleVenta> lista = await _repositorioVenta.Reporte(fech_inicio, fech_fin);

            return lista;
        }
        public async Task<List<Cliente>> ObtenerCliente(string busqueda)
        {

            IQueryable<Cliente> querry = await _repositorioCliente.Consultar(
                c => c.EsActivo == true &&
                string.Concat(c.Nombres, c.Apellidos, c.NumeroDocumento).Contains(busqueda) 
                );

            return querry.Include(c => c.IdCondicionTributariaNavigation).ToList();
        }

        public async Task<TipoComprobante> TipoComprobantePorCondicionTributaria(string busqueda)
        {
            CondicionTributaria condicionTributaria = await _repositorioCondicionTributaria.Obtener(c => c.Nombre == busqueda);

            if (condicionTributaria != null)
            {
                // Si se encontró la condición tributaria, consultar el tipo de comprobante asociado
                IQueryable<TipoComprobante> query = await _repositorioTipoComprobante.Consultar(
                    c => c.IdTipoComprobante == condicionTributaria.IdTipoComprobante
                );

                // Devuelve el primer tipo de comprobante encontrado
                return query.FirstOrDefault();
            }
            else
            {
                // Si no se encontró la condición tributaria, devuelve null o realiza algún manejo de errores apropiado
                return null;
            }
        }
    }
}
