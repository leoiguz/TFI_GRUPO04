using System;
using System.Collections.Generic;

namespace SistemaVenta.Entity
{
    public partial class Cliente
    {
        public Cliente()
        {
            Venta = new HashSet<Venta>();
        }

        public int IdCliente { get; set; }
        public string? NumeroDocumento { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? Telefono { get; set; }
        public string? Domicilio { get; set; }
        public string? Descripcion { get; set; }
        public int? IdCondicionTributaria { get; set; }
        public bool? EsActivo { get; set; }
        public DateTime? FechaRegistro { get; set; }

        public virtual CondicionTributaria? IdCondicionTributariaNavigation { get; set; }
        public virtual ICollection<Venta> Venta { get; set; }
    }
}
