namespace SistemaVenta.Entity
{
    public partial class TipoPago
    {
        public TipoPago()
        {
            Pagos = new HashSet<Pago>();
        }

        public int IdTipoPago { get; set; }
        public string? Descripcion { get; set; }
        public virtual ICollection<Pago> Pagos { get; set; }
    }
}
