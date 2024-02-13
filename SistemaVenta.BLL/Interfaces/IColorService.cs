using SistemaVenta.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.BLL.Interfaces
{
    public interface IColorService
    {
        Task<List<Color>> Lista();

        Task<Color> Crear(Color entidad);

        Task<Color> Editar(Color entidad);

        Task<bool> Eliminar(int idColor);
    }
}
