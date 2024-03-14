namespace SistemaVenta.Entity
{
    public partial class Usuario
    {
        public Usuario()
        {
            Empleados = new HashSet<Empleado>();
            Sesions = new HashSet<Sesion>();
            Venta = new HashSet<Venta>();
        }

        public int IdUsuario { get; set; }
        public string? Nombre { get; set; }
        public string? Correo { get; set; }
        public string? Telefono { get; set; }
        public int? IdRol { get; set; }
        public string? Clave { get; set; }
        public bool? EsActivo { get; set; }
        public DateTime? FechaRegistro { get; set; }

        public virtual Rol? IdRolNavigation { get; set; }
        public virtual ICollection<Empleado> Empleados { get; set; }
        public virtual ICollection<Sesion> Sesions { get; set; }
        public virtual ICollection<Venta> Venta { get; set; }
    }
}
