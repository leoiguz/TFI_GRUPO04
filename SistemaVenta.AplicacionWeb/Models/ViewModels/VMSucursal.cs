namespace SistemaVenta.AplicacionWeb.Models.ViewModels
{
    public class VMSucursal
    {
        public int IdSucursal { get; set; }
        public string? NumeroDocumento { get; set; }
        public string? Nombre { get; set; }
        public string? Correo { get; set; }
        public string? Domicilio { get; set; }
        public string? Ciudad { get; set; }
        public string? Telefono { get; set; }
        public int? IdCondicionTributaria { get; set; }
        public string? NombreCondicionTributaria { get; set; }
    }
}
