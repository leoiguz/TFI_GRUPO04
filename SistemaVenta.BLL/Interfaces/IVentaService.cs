﻿using SistemaVenta.Entity;

namespace SistemaVenta.BLL.Interfaces
{
	public interface IVentaService
	{
		Task<List<Inventario>> ObtenerInventario(string busqueda);
		Task<List<Cliente>> ObtenerCliente(string busqueda);
		Task<Venta> Registrar(Venta entidad);
		Task<List<Venta>> Historial(string numeroVenta, string fechaInicio, string fechaFin);
		Task<Venta> Detalle(string numeroVenta);
		Task<List<DetalleVenta>> Reporte(string fechaInicio, string fechaFin);
		Task<TipoComprobante> TipoComprobantePorCondicionTributaria(string busqueda);
		Task<AFIP> ObtenerDatosAFIP();
		Task<AFIP> EditarAfip(AFIP entidad);

    }
}
