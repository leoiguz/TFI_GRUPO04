using SistemaVenta.BLL.Interfaces;
using SistemaVenta.DAL.Interfaces;
using SistemaVenta.Entity;

namespace SistemaVenta.BLL.Implementacion
{
    public class TipoTalleService : ITipoTalleService
    {
        private readonly IGenericRepository<TipoTalle> _repositorio;

        public TipoTalleService(IGenericRepository<TipoTalle> repositorio)
        {
            _repositorio = repositorio;

        }


        public async Task<List<TipoTalle>> Lista()
        {
            IQueryable<TipoTalle> query = await _repositorio.Consultar();
            return query.ToList();
        }

        public async Task<TipoTalle> Crear(TipoTalle entidad)
        {
            try
            {
                TipoTalle tipoTalle_creado = await _repositorio.Crear(entidad);
                if (tipoTalle_creado.IdTipoTalle == 0) throw new TaskCanceledException("No se pudo crear el tipo talle");

                return tipoTalle_creado;
            }
            catch { throw; }
        }

        public async Task<TipoTalle> Editar(TipoTalle entidad)
        {
            try
            {
                TipoTalle tipoTalle_encontrado = await _repositorio.Obtener(c => c.IdTipoTalle == entidad.IdTipoTalle);
                tipoTalle_encontrado.Descripcion = entidad.Descripcion;
                tipoTalle_encontrado.EsActivo = entidad.EsActivo;

                bool respuesta = await _repositorio.Editar(tipoTalle_encontrado);

                if (!respuesta) throw new TaskCanceledException("No se pudo modificar el tipo talle");

                return tipoTalle_encontrado;
            }
            catch { throw; }
        }

        public async Task<bool> Eliminar(int idTipoTalle)
        {
            try
            {
                TipoTalle tipoTalle_encontrado = await _repositorio.Obtener(c => c.IdTipoTalle == idTipoTalle);
                if (tipoTalle_encontrado == null) throw new TaskCanceledException("El tipo talle no existe");

                bool respuesta = await _repositorio.Eliminar(tipoTalle_encontrado);

                return respuesta;
            }
            catch { throw; }
        }
    }
}
