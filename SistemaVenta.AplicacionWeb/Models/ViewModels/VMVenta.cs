using SistemaVenta.Entity;

namespace SistemaVenta.AplicacionWeb.Models.ViewModels
{
    public class VMVenta
    {
        public int IdVenta { get; set; }
        public string? NumeroVenta { get; set; }
        public int? IdTipoComprobante { get; set; }
        public string? TipoComprobante { get; set; }
        public int? IdUsuario { get; set; }
        public string? Usuario { get; set; }
        public int? IdCliente { get; set; }
        public string? NombreCliente { get; set; }
        public string? DocumentoCliente { get; set; }
        public int? IdPuntoVenta { get; set; }
        public string? ImpuestoTotal { get; set; }
        public string? Total { get; set; }
        public string? SubTotal { get; set; }
        public string? FechaRegistro { get; set; }
        public virtual ICollection<VMDetalleVenta> DetalleVenta { get; set; }
    }
}
