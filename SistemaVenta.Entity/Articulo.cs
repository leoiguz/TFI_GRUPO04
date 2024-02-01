using System;
using System.Collections.Generic;

namespace SistemaVenta.Entity
{
    public partial class Articulo
    {
        public Articulo()
        {
            Inventarios = new HashSet<Inventario>();
        }

        public int IdArticulo { get; set; }
        public string? CodigoBarra { get; set; }
        public string? Descripcion { get; set; }
        public int? IdCategoria { get; set; }
        public int? IdMarca { get; set; }
        public int? IdTipoTalle { get; set; }
        public decimal? Costo { get; set; }
        public decimal? MargenGanancia { get; set; }
        public decimal? Iva { get; set; }
        public bool? EsActivo { get; set; }
        public DateTime? FechaRegistro { get; set; }

        public virtual Categoria? IdCategoriaNavigation { get; set; }
        public virtual Marca? IdMarcaNavigation { get; set; }
        public virtual TipoTalle? IdTipoTalleNavigation { get; set; }
        public virtual ICollection<Inventario> Inventarios { get; set; }
    }
}
