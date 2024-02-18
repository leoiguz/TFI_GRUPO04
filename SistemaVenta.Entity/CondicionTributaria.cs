using System;
using System.Collections.Generic;

namespace SistemaVenta.Entity
{
    public partial class CondicionTributaria
    {
        public CondicionTributaria()
        {
            Clientes = new HashSet<Cliente>();
            //TipoComprobantes = new HashSet<TipoComprobante>();
        }

        public int IdCondicionTributaria { get; set; }
        public string? Nombre { get; set; }
        public bool? EsActivo { get; set; }
        public int? IdTipoComprobante { get; set; }
        public DateTime? FechaRegistro { get; set; }

        public virtual ICollection<Cliente> Clientes { get; set; }
        //public virtual ICollection<TipoComprobante> TipoComprobantes { get; set; }
        public virtual TipoComprobante? IdTipoComprobanteNavigation { get; set; }
    }
}
