using SistemaVenta.BLL.Interfaces;
using SistemaVenta.DAL.Interfaces;
using SistemaVenta.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.BLL.Implementacion
{
    public class ColorService :IColorService
    {
        private readonly IGenericRepository<Color> _repositorio;

        public ColorService(IGenericRepository<Color> repositorio)
        {
            _repositorio = repositorio;

        }


        public async Task<List<Color>> Lista()
        {
            IQueryable<Color> query = await _repositorio.Consultar();
            return query.ToList();
        }

        public async Task<Color> Crear(Color entidad)
        {
            try
            {
                Color color_creado = await _repositorio.Crear(entidad);
                if (color_creado.IdColor == 0) throw new TaskCanceledException("No se pudo crear el color");

                return color_creado;
            }
            catch { throw; }
        }

        public async Task<Color> Editar(Color entidad)
        {
            try
            {
                Color color_encontrado = await _repositorio.Obtener(c => c.IdColor == entidad.IdColor);
                color_encontrado.Descripcion = entidad.Descripcion;
                color_encontrado.EsActivo = entidad.EsActivo;

                bool respuesta = await _repositorio.Editar(color_encontrado);

                if (!respuesta) throw new TaskCanceledException("No se pudo modificar el color");

                return color_encontrado;
            }
            catch { throw; }
        }

        public async Task<bool> Eliminar(int idColor)
        {
            try
            {
                Color color_encontrado = await _repositorio.Obtener(c => c.IdColor == idColor);
                if (color_encontrado == null) throw new TaskCanceledException("El color no existe");

                bool respuesta = await _repositorio.Eliminar(color_encontrado);

                return respuesta;
            }
            catch { throw; }
        }
    }
}
