using System;
using System.Collections.Generic;

namespace SistemaVenta.Entity
{
    public partial class Pago
    {
        public Pago()
        {
            Venta = new HashSet<Venta>();
        }

        public int IdPago { get; set; }
        public decimal? Monto { get; set; }
        public bool? EsActivo { get; set; }
        public int? IdTipoPago { get; set; }
        public DateTime? FechaRegistro { get; set; }

        public virtual TipoPago? IdTipoPagoNavigation { get; set; }

        public virtual ICollection<Venta> Venta { get; set; }
    }
}
