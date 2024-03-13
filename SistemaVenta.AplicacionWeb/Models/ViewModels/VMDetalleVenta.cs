namespace SistemaVenta.AplicacionWeb.Models.ViewModels
{
    public class VMDetalleVenta
    {
        public int? IdInventario { get; set; }       
        public string? NombreArticulo { get; set; }
        public string? ColorInventario { get; set; }
        public string? TalleInventario { get; set; }
        public int? Cantidad { get; set; }
        public string? NetoGravado { get; set; }
        public string? Iva { get; set; }
        public string? MargenGanancia { get; set; }
    }
}
