using System;
using System.Collections.Generic;

namespace SistemaVenta.Entity
{
    public partial class Sucursal
    {
        public int IdSucursal { get; set; }
        public string? NumeroDocumento { get; set; }
        public string? Nombre { get; set; }
        public string? Correo { get; set; }
        public string? Domicilio { get; set; }
        public string? Ciudad { get; set; }
        public string? Telefono { get; set; }
        public decimal? PorcentajeImpuesto { get; set; }
        public string? SimboloMoneda { get; set; }
        public int? IdEmpleado { get; set; }
        public int? IdPuntoVenta { get; set; }

        public virtual Empleado? IdEmpleadoNavigation { get; set; }
        public virtual PuntoVenta? IdPuntoVentaNavigation { get; set; }
    }
}
