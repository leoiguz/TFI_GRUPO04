using System;
using System.Collections.Generic;

namespace SistemaVenta.Entity
{
    public partial class Color
    {
        public Color()
        {
            Inventarios = new HashSet<Inventario>();
        }

        public int IdColor { get; set; }
        public string? Descripcion { get; set; }
        public bool? EsActivo { get; set; }
        public DateTime? FechaRegistro { get; set; }

        public virtual ICollection<Inventario> Inventarios { get; set; }
    }
}
