namespace SistemaVenta.AplicacionWeb.Models.ViewModels
{
    public class VMPago
    {
        public int IdPago { get; set; }
        public string? Monto { get; set; }
        public string? IdTipoPago { get; set; }
        public string? NombreTipoPago { get; set; }      
        public int esActivo { get; set; }
    }
}
