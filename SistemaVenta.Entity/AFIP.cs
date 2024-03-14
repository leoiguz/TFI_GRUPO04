namespace SistemaVenta.Entity
{
    public partial class AFIP
    {
        public int IdAFIP { get; set; }
        public string? Token { get; set; }
        public string? Cae { get; set; }
        public DateTime? VencimientoToken { get; set; }
    }
}
