using System;
using System.Collections.Generic;

namespace SistemaVenta.Entity
{
    public partial class Venta
    {
        public Venta()
        {
            DetalleVenta = new HashSet<DetalleVenta>();
        }

        public int IdVenta { get; set; }
        public string? NumeroComprobante { get; set; }
        public int? IdTipoComprobante { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdCliente { get; set; }
        public int? IdPago { get; set; }
        public decimal? NetoGravado { get; set; }
        public decimal? ImporteIva { get; set; }
        public decimal? Monto { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public int? IdPuntoVenta { get; set; }
        public string? Observacion { get; set; }
        public string? Estado { get; set; }
        public virtual Cliente? IdClienteNavigation { get; set; }
        public virtual Pago? IdPagoNavigation { get; set; }
        public virtual PuntoVenta? IdPuntoVentaNavigation { get; set; }
        public virtual TipoComprobante? IdTipoComprobanteNavigation { get; set; }
        public virtual Usuario? IdUsuarioNavigation { get; set; }
        public virtual ICollection<DetalleVenta> DetalleVenta { get; set; }
    }
}
