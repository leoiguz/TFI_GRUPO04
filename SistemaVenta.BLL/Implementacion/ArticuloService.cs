using Microsoft.EntityFrameworkCore;
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
    public class ArticuloService : IArticuloService
    {
        private readonly IGenericRepository<Articulo> _repositorio;
        private readonly IUtilidadesService _utilidadesServicio;

        public ArticuloService(IGenericRepository<Articulo> repositorio,IUtilidadesService utilidadesServicio)
        {
            _repositorio = repositorio;
            _utilidadesServicio = utilidadesServicio;
        }

        public async Task<List<Articulo>> Lista()
        {
            IQueryable<Articulo> query = await _repositorio.Consultar();
            return query
                .Include(c => c.IdCategoriaNavigation)
                .Include(c => c.IdMarcaNavigation)
                .Include(c => c.IdTipoTalleNavigation)
                .ToList();
        }

        public async Task<Articulo> Crear(Articulo entidad)
        {
            Articulo articulo_existe = await _repositorio.Obtener(p => p.CodigoBarra == entidad.CodigoBarra);

            if (articulo_existe != null) throw new TaskCanceledException("El codigo de barra ya existe");

            try
            {

                Articulo articulo_creado = await _repositorio.Crear(entidad);

                if (articulo_creado.IdArticulo == 0) throw new TaskCanceledException("No se pudo crear el articulo");

                IQueryable<Articulo> query = await _repositorio.Consultar(p => p.IdArticulo == articulo_creado.IdArticulo);

                articulo_creado = query
                    .Include(c => c.IdCategoriaNavigation)
                    .Include(c => c.IdMarcaNavigation)
                    .Include(c => c.IdTipoTalleNavigation)
                    .First();

                return articulo_creado;
            }
            catch { throw; }

        }

        public async Task<Articulo> Editar(Articulo entidad)
        {
            Articulo articulo_existe = await _repositorio.Obtener(p => p.CodigoBarra == entidad.CodigoBarra && p.IdArticulo != entidad.IdArticulo);

            if (articulo_existe != null) throw new TaskCanceledException("El codigo de barra ya existe");

            try
            {
                IQueryable<Articulo> queryArticulo = await _repositorio.Consultar(p => p.IdArticulo == entidad.IdArticulo);

                Articulo articulo_editar = queryArticulo.First();

                articulo_editar.CodigoBarra = entidad.CodigoBarra;
                articulo_editar.IdMarca = entidad.IdMarca;
                articulo_editar.Descripcion = entidad.Descripcion;
                articulo_editar.IdCategoria = entidad.IdCategoria;
                articulo_editar.IdTipoTalle = entidad.IdTipoTalle;
                articulo_editar.Costo = entidad.Costo;
                articulo_editar.Iva = entidad.Iva;
                articulo_editar.EsActivo = entidad.EsActivo;

                bool respuesta = await _repositorio.Editar(articulo_editar);

                if (!respuesta) throw new TaskCanceledException("No se pudo editar el articulo");

                Articulo articulo_editado = queryArticulo
                    .Include(c => c.IdCategoriaNavigation)
                    .Include(c => c.IdMarcaNavigation)
                    .Include(c => c.IdTipoTalleNavigation)
                    .First();


                return articulo_editado;

            }
            catch { throw; }
        }

        public async Task<bool> Eliminar(int idArticulo)
        {
            try
            {
                Articulo articulo_encontrado = await _repositorio.Obtener(p => p.IdArticulo == idArticulo);

                if (articulo_encontrado == null) throw new TaskCanceledException("El articulo no existe");

                bool respuesta = await _repositorio.Eliminar(articulo_encontrado);

                return true;
            }
            catch { throw; }
        }
    }
}
