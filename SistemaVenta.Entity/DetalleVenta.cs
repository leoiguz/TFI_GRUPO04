using System;
using System.Collections.Generic;

namespace SistemaVenta.Entity
{
    public partial class DetalleVenta
    {
        public int IdDetalleVenta { get; set; }
        public int? IdVenta { get; set; }
        public int? IdInventario { get; set; }
        public string? NombreArticulo { get; set; }
        public string? ColorInventario { get; set; }
        public string? TalleInventario { get; set; }
        public int? Cantidad { get; set; }
        public decimal? Iva { get; set; }
        public decimal? MargenGanancia { get; set; }
        public decimal? NetoGravado { get; set; }
        public decimal? Total { get; set; }
        public virtual Inventario? IdInventarioNavigation { get; set; }
        public virtual Venta? IdVentaNavigation { get; set; }
    }
}
