using System;
using System.Collections.Generic;

namespace SistemaVenta.Entity
{
    public partial class Marca
    {
        public Marca()
        {
            Articulos = new HashSet<Articulo>();
        }

        public int IdMarca { get; set; }
        public string? Descripcion { get; set; }
        public bool? EsActivo { get; set; }
        public DateTime? FechaRegistro { get; set; }

        public virtual ICollection<Articulo> Articulos { get; set; }
    }
}
