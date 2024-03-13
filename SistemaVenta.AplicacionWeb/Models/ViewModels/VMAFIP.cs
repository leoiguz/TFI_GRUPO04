namespace SistemaVenta.AplicacionWeb.Models.ViewModels
{
    public class VMAFIP
    {
        public int IdAFIP { get; set; }
        public string? Token { get; set; }
        public string? Cae { get; set; }
        public DateTime? VencimientoToken { get; set; }
    }
}
