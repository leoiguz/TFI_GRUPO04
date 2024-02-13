namespace SistemaVenta.AplicacionWeb.Models.ViewModels
{
    public class VMCliente
    {
        public int IdCliente { get; set; }
        public string? NumeroDocumento { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? Telefono { get; set; }
        public string? Domicilio { get; set; }
        public int? IdCondicionTributaria { get; set; }
        public string? NombreCondicionTributaria { get; set; }
        public int? EsActivo { get; set; }
    }
}
