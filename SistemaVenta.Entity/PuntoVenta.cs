using System;
using System.Collections.Generic;

namespace SistemaVenta.Entity
{
    public partial class PuntoVenta
    {
        public PuntoVenta()
        {
            Sucursals = new HashSet<Sucursal>();
            Venta = new HashSet<Venta>();
        }

        public int IdPuntoVenta { get; set; }
        public int? Numero { get; set; }
        public int? IdSesion { get; set; }
        public bool? EsActivo { get; set; }
        public DateTime? FechaRegistro { get; set; }

        public virtual Sesion? IdSesionNavigation { get; set; }
        public virtual ICollection<Sucursal> Sucursals { get; set; }
        public virtual ICollection<Venta> Venta { get; set; }
    }
}
