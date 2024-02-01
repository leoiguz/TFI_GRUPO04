using System;
using System.Collections.Generic;

namespace SistemaVenta.Entity
{
    public partial class TipoComprobante
    {
        public TipoComprobante()
        {
            Venta = new HashSet<Venta>();
        }

        public int IdTipoComprobante { get; set; }
        public string? Descripcion { get; set; }
        public int? IdCondicionTributaria { get; set; }
        public bool? EsActivo { get; set; }
        public DateTime? FechaRegistro { get; set; }

        public virtual CondicionTributaria? IdCondicionTributariaNavigation { get; set; }
        public virtual ICollection<Venta> Venta { get; set; }
    }
}
