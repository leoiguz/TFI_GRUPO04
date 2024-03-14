namespace SistemaVenta.Entity
{
    public partial class Tarjeta
    {
        public Tarjeta()
        {
            Pagos = new HashSet<Pago>();
        }

        public int IdTarjeta { get; set; }
        public string? NumeroTarjeta { get; set; }
        public string? MesTarjeta { get; set; }
        public string? AnioTarjeta { get; set; }
        public string? CodigoSeguridadTarjeta { get; set; }
        public string? NombreTarjeta { get; set; }
        public virtual ICollection<Pago> Pagos { get; set; }
    }
}
