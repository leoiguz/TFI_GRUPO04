namespace SistemaVenta.AplicacionWeb.Models.ViewModels
{
    public class VMCae
    {
        public string? Token { get; set; }
        public DateTime? Fecha { get; set; }
        public double ImporteIva { get; set; }
        public double ImporteNeto { get; set; }
        public double ImporteTotal { get; set; }
        public int Numero { get; set; }
        public int NumeroDocumento { get; set; }
        public int TipoComprobante { get; set; }
        public int TipoDocumento { get; set; }

    }
}
