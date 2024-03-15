namespace SistemaVenta.Entity
{
    public partial class Color
    {
        public Color()
        {
            Inventarios = new HashSet<Inventario>();
        }

        public Color(int id, string descripcion, bool esActivo, DateTime fechaRegistro)
        {
            IdColor = id;
            Descripcion = descripcion;
            EsActivo = esActivo;
            FechaRegistro = fechaRegistro;
        }

        public int IdColor { get; set; }
        public string? Descripcion { get; set; }
        public bool? EsActivo { get; set; }
        public DateTime? FechaRegistro { get; set; }

        public virtual ICollection<Inventario> Inventarios { get; set; }
    }
}
