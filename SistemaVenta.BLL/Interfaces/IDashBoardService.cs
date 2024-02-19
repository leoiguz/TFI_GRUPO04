using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.BLL.Interfaces
{
    public interface IDashBoardService
    {
        Task<int> TotalVentasUltimaSemana();
        Task<string> TotalIngresosUltimaSemana();
        Task<int> TotalInventarios();
        Task<int> TotalArticulos();
        Task<Dictionary<string, int>> VentasUltimaSemana();
        Task<Dictionary<string, int>> InventariosTopUltimaSemana();

    }
}