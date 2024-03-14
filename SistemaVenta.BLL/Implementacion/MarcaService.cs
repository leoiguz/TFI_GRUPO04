using SistemaVenta.BLL.Interfaces;
using SistemaVenta.DAL.Interfaces;
using SistemaVenta.Entity;

namespace SistemaVenta.BLL.Implementacion
{
    public class MarcaService : IMarcaService
    {
        private readonly IGenericRepository<Marca> _repositorio;

        public MarcaService(IGenericRepository<Marca> repositorio)
        {
            _repositorio = repositorio;

        }


        public async Task<List<Marca>> Lista()
        {
            IQueryable<Marca> query = await _repositorio.Consultar();
            return query.ToList();
        }

        public async Task<Marca> Crear(Marca entidad)
        {
            try
            {
                Marca marca_creado = await _repositorio.Crear(entidad);
                if (marca_creado.IdMarca == 0) throw new TaskCanceledException("No se pudo crear el marca");

                return marca_creado;
            }
            catch { throw; }
        }

        public async Task<Marca> Editar(Marca entidad)
        {
            try
            {
                Marca marca_encontrado = await _repositorio.Obtener(c => c.IdMarca == entidad.IdMarca);
                marca_encontrado.Descripcion = entidad.Descripcion;
                marca_encontrado.EsActivo = entidad.EsActivo;

                bool respuesta = await _repositorio.Editar(marca_encontrado);

                if (!respuesta) throw new TaskCanceledException("No se pudo modificar el marca");

                return marca_encontrado;
            }
            catch { throw; }
        }

        public async Task<bool> Eliminar(int idMarca)
        {
            try
            {
                Marca marca_encontrado = await _repositorio.Obtener(c => c.IdMarca == idMarca);
                if (marca_encontrado == null) throw new TaskCanceledException("El marca no existe");

                bool respuesta = await _repositorio.Eliminar(marca_encontrado);

                return respuesta;
            }
            catch { throw; }
        }
    }
}
