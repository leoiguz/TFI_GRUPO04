﻿namespace SistemaVenta.AplicacionWeb.Models.ViewModels
{
    public class VMSucursal
    {
        public int IdSucursal { get; set; }
        public string? NumeroDocumento { get; set; }
        public string? Nombre { get; set; }
        public string? Correo { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public decimal? PorcentajeImpuesto { get; set; }
        public string? SimboloMoneda { get; set; }
    }
}
