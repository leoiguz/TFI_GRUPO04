namespace SistemaVenta.AplicacionWeb.Models.ViewModels
{
    public class VMDashBoard
    {
        public int TotalVentas { get; set; }
        public string? TotalIngresos { get; set; }
        public int TotalArticulos { get; set; }
        public int TotalInventarios { get; set; }
        public List<VMVentasSemana> VentasUltimaSemana { get; set; }
        public List<VMInventariosSemana> InventariosTopUltimaSemana { get; set; }
    }
}
