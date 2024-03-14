namespace SistemaVenta.Entity
{
    public partial class Talle
    {
        public Talle()
        {
            Inventarios = new HashSet<Inventario>();
        }

        public int IdTalle { get; set; }
        public string? Descripcion { get; set; }
        public int? IdTipoTalle { get; set; }
        public bool? EsActivo { get; set; }
        public DateTime? FechaRegistro { get; set; }

        public virtual TipoTalle? IdTipoTalleNavigation { get; set; }
        public virtual ICollection<Inventario> Inventarios { get; set; }
    }
}
