﻿namespace SistemaVenta.Entity
{
    public partial class Sucursal
    {
        public Sucursal()
        {
            Empleados = new HashSet<Empleado>();
            PuntoVenta = new HashSet<PuntoVenta>();
        }

        public int IdSucursal { get; set; }
        public string? NumeroDocumento { get; set; }
        public string? Nombre { get; set; }
        public string? Correo { get; set; }
        public string? Domicilio { get; set; }
        public string? Ciudad { get; set; }
        public string? Telefono { get; set; }

        public int? IdCondicionTributaria { get; set; }


        public virtual CondicionTributaria? IdCondicionTributariaNavigation { get; set; }
        public virtual ICollection<Empleado> Empleados { get; set; }
        public virtual ICollection<PuntoVenta> PuntoVenta { get; set; }
    }
}
