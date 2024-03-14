namespace SistemaVenta.Entity
{
    public partial class Sesion
    {
        public Sesion()
        {
            PuntoVenta = new HashSet<PuntoVenta>();
        }

        public int IdSesion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int? IdUsuario { get; set; }

        public virtual Usuario? IdUsuarioNavigation { get; set; }
        public virtual ICollection<PuntoVenta> PuntoVenta { get; set; }
    }
}
