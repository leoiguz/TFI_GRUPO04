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
        public int? Cantidad { get; set; }
        public decimal? Precio { get; set; }
        public decimal? Subtotal { get; set; }
        public decimal? Total { get; set; }

        public virtual Inventario? IdInventarioNavigation { get; set; }
        public virtual Venta? IdVentaNavigation { get; set; }
    }
}
