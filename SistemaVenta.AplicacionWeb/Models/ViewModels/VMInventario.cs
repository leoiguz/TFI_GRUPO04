namespace SistemaVenta.AplicacionWeb.Models.ViewModels
{
    public class VMInventario
    {
        public int IdInventario { get; set; }
        public int? Cantidad { get; set; }
        public string? IdArticulo { get; set; }
        public string? NombreArticulo { get; set; }
        public string? IdColor { get; set; }
        public string? NombreColor { get; set; }
        public string? IdTalle { get; set; }
        public string? NombreTalle { get; set; }
        public int? EsActivo { get; set; }
        public decimal? PrecioArticulo { get; set; }
    }
}
