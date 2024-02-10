namespace SistemaVenta.AplicacionWeb.Models.ViewModels
{
    public class VMArticulo
    {
        public int IdArticulo { get; set; }
        public string? CodigoBarra { get; set; }
        public string? IdMarca { get; set; }
        public string? NombreMarca { get; set; }
        public string? IdTipoTalle { get; set; }
        public string? NombreTipoTalle { get; set; }
        public string? Descripcion { get; set; }
        public int? IdCategoria { get; set; }
        public string? NombreCategoria { get; set; }
        public decimal? Costo { get; set; }
        public int? EsActivo { get; set; }
    }
}
