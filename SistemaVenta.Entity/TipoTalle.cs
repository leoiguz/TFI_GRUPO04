namespace SistemaVenta.Entity
{
    public partial class TipoTalle
    {
        public TipoTalle()
        {
            Articulos = new HashSet<Articulo>();
            Talles = new HashSet<Talle>();
        }

        public int IdTipoTalle { get; set; }
        public string? Descripcion { get; set; }
        public bool? EsActivo { get; set; }
        public DateTime? FechaRegistro { get; set; }

        public virtual ICollection<Articulo> Articulos { get; set; }
        public virtual ICollection<Talle> Talles { get; set; }
    }
}
