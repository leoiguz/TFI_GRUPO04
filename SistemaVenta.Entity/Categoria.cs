using System;
using System.Collections.Generic;

namespace SistemaVenta.Entity
{
    public partial class Categoria
    {
        public Categoria()
        {
            Articulos = new HashSet<Articulo>();
        }

        public int IdCategoria { get; set; }
        public string? Descripcion { get; set; }
        public bool? EsActivo { get; set; }
        public DateTime? FechaRegistro { get; set; }

        public virtual ICollection<Articulo> Articulos { get; set; }
    }
}
