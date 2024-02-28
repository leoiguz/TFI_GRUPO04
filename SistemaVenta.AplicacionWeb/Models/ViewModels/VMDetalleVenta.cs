namespace SistemaVenta.AplicacionWeb.Models.ViewModels
{
    public class VMDetalleVenta
    {
        public int IdInventario { get; set; }
        public int? Cantidad { get; set; }
        public string? NombreArticulo { get; set; }
        public string? MontoIva { get; set; }
        public string? NetoGravado { get; set; }
        public string? PorcentajeIva { get; set; }
        public string? Total { get; set; }
    }
}
