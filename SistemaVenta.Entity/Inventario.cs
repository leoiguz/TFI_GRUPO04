namespace SistemaVenta.Entity
{
    public partial class Inventario
    {
        public Inventario()
        {
            DetalleVenta = new HashSet<DetalleVenta>();
        }

        public int IdInventario { get; set; }
        public int? Cantidad { get; set; }
        public int? IdArticulo { get; set; }
        public int? IdColor { get; set; }
        public int? IdTalle { get; set; }
        public bool? EsActivo { get; set; }
        public DateTime? FechaRegistro { get; set; }

        public virtual Articulo? IdArticuloNavigation { get; set; }
        public virtual Color? IdColorNavigation { get; set; }
        public virtual Talle? IdTalleNavigation { get; set; }
        public virtual ICollection<DetalleVenta> DetalleVenta { get; set; }
    }
}
