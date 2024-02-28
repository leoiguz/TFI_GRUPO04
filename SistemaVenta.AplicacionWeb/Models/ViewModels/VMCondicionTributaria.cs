namespace SistemaVenta.AplicacionWeb.Models.ViewModels
{
    public class VMCondicionTributaria
    {
        public int IdCondicionTributaria { get; set; }
        public string? Nombre { get; set; }
        public string? IdTipoComprobante { get; set; }
        public string? Codigo { get; set; }
        public int esActivo { get; set; }
    }
}
