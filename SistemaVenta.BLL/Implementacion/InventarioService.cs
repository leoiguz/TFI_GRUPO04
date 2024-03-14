using Microsoft.EntityFrameworkCore;
using SistemaVenta.BLL.Interfaces;
using SistemaVenta.DAL.Interfaces;
using SistemaVenta.Entity;

namespace SistemaVenta.BLL.Implementacion
{
    public class InventarioService :IInventarioService
    {
        private readonly IGenericRepository<Inventario> _repositorio;
        private readonly IUtilidadesService _utilidadesServicio;

        public InventarioService(IGenericRepository<Inventario> repositorio, IUtilidadesService utilidadesServicio)
        {
            _repositorio = repositorio;
            _utilidadesServicio = utilidadesServicio;
        }

        public async Task<List<Inventario>> Lista()
        {
            IQueryable<Inventario> query = await _repositorio.Consultar();
            return query
                .Include(c => c.IdArticuloNavigation)
                .Include(c => c.IdColorNavigation)
                .Include(c => c.IdTalleNavigation)
                .ToList();
        }

        public async Task<Inventario> Crear(Inventario entidad)
        {
            try
            {

                Inventario inventario_creado = await _repositorio.Crear(entidad);

                if (inventario_creado.IdInventario == 0) throw new TaskCanceledException("No se pudo crear el inventario");

                IQueryable<Inventario> query = await _repositorio.Consultar(p => p.IdInventario == inventario_creado.IdInventario);

                inventario_creado = query
                    .Include(c => c.IdArticuloNavigation)
                    .Include(c => c.IdColorNavigation)
                    .Include(c => c.IdTalleNavigation)
                    .First();

                return inventario_creado;
            }
            catch { throw; }

        }

        public async Task<Inventario> Editar(Inventario entidad)
        {
            try
            {
                IQueryable<Inventario> queryInventario = await _repositorio.Consultar(p => p.IdInventario == entidad.IdInventario);

                Inventario inventario_editar = queryInventario.First();

                inventario_editar.IdArticulo = entidad.IdArticulo;
                inventario_editar.IdColor = entidad.IdColor;
                inventario_editar.IdTalle = entidad.IdTalle;
                inventario_editar.Cantidad = entidad.Cantidad;
                inventario_editar.EsActivo = entidad.EsActivo;

                bool respuesta = await _repositorio.Editar(inventario_editar);

                if (!respuesta) throw new TaskCanceledException("No se pudo editar el inventario");

                Inventario inventario_editado = queryInventario
                    .Include(c => c.IdArticuloNavigation)
                    .Include(c => c.IdColorNavigation)
                    .Include(c => c.IdTalleNavigation)
                    .First();


                return inventario_editado;

            }
            catch { throw; }
        }

        public async Task<bool> Eliminar(int idInventario)
        {
            try
            {
                Inventario inventario_encontrado = await _repositorio.Obtener(p => p.IdInventario == idInventario);

                if (inventario_encontrado == null) throw new TaskCanceledException("El inventario no existe");

                bool respuesta = await _repositorio.Eliminar(inventario_encontrado);

                return true;
            }
            catch { throw; }
        }
    }
}
