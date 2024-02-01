using System;
using System.Collections.Generic;

namespace SistemaVenta.Entity
{
    public partial class Empleado
    {
        public Empleado()
        {
            Sucursals = new HashSet<Sucursal>();
        }

        public int IdEmpleado { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? Telefono { get; set; }
        public int? IdUsuario { get; set; }
        public bool? EsActivo { get; set; }
        public DateTime? FechaRegistro { get; set; }

        public virtual Usuario? IdUsuarioNavigation { get; set; }
        public virtual ICollection<Sucursal> Sucursals { get; set; }
    }
}
