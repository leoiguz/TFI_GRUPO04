namespace SistemaVenta.AplicacionWeb.Models.ViewModels
{
    public class VMDetalleVenta
    {
        public int? IdArticulo { get; set; }
        public string? MarcaArticulo { get; set; }
        public string? DescripcionArticulo { get; set; }
        public string? CategoriaArticulo { get; set; }
        public int? Cantidad { get; set; }
        public string? Precio { get; set; }
        public string? Subtotal { get; set; }
        public string? Total { get; set; }
    }
}
