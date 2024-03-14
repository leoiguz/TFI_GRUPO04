namespace SistemaVenta.BLL.Interfaces
{
    public interface ICorreoService
    {
        Task<bool> EnviarCorreo(string correoDestino, string Asunto, string Mensaje);
    }
}
