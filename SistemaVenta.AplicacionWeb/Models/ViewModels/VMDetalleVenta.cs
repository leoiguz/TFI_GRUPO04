namespace SistemaVenta.AplicacionWeb.Models.ViewModels
{
    public class VMDetalleVenta
    {
        public int IdInventario { get; set; }
        public int? Cantidad { get; set; }
        public string? NombreArticulo { get; set; }
        public string? Precio { get; set; }
        public string? Subtotal { get; set; }
        public string? Total { get; set; }
    }
}
