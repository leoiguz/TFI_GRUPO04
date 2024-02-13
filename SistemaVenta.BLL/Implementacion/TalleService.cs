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
    public class TalleService :ITalleService
    {
        private readonly IGenericRepository<Talle> _repositorio;
        private readonly IUtilidadesService _utilidadesServicio;

        public TalleService(IGenericRepository<Talle> repositorio, IUtilidadesService utilidadesServicio)
        {
            _repositorio = repositorio;
            _utilidadesServicio = utilidadesServicio;
        }

        public async Task<List<Talle>> Lista()
        {
            IQueryable<Talle> query = await _repositorio.Consultar();
            return query
                .Include(c => c.IdTipoTalleNavigation)
                .ToList();
        }

        public async Task<Talle> Crear(Talle entidad)
        {
            //Talle talle_existe = await _repositorio.Obtener(p => p.IdTalle == entidad.IdTalle);

            //if (talle_existe != null) throw new TaskCanceledException("El codigo de barra ya existe");

            try
            {

                Talle talle_creado = await _repositorio.Crear(entidad);

                if (talle_creado.IdTalle == 0) throw new TaskCanceledException("No se pudo crear el talle");

                IQueryable<Talle> query = await _repositorio.Consultar(p => p.IdTalle == talle_creado.IdTalle);

                talle_creado = query
                    .Include(c => c.IdTipoTalleNavigation)
                    .First();

                return talle_creado;
            }
            catch { throw; }

        }

        public async Task<Talle> Editar(Talle entidad)
        {
            //Talle talle_existe = await _repositorio.Obtener(p => p.CodigoBarra == entidad.CodigoBarra && p.IdTalle != entidad.IdTalle);

            //if (talle_existe != null) throw new TaskCanceledException("El codigo de barra ya existe");

            try
            {
                IQueryable<Talle> queryTalle = await _repositorio.Consultar(p => p.IdTalle == entidad.IdTalle);

                Talle talle_editar = queryTalle.First();

                talle_editar.Descripcion = entidad.Descripcion;
                talle_editar.IdTipoTalle = entidad.IdTipoTalle;
                talle_editar.EsActivo = entidad.EsActivo;

                bool respuesta = await _repositorio.Editar(talle_editar);

                if (!respuesta) throw new TaskCanceledException("No se pudo editar el talle");

                Talle talle_editado = queryTalle
                    .Include(c => c.IdTipoTalleNavigation)
                    .First();


                return talle_editado;

            }
            catch { throw; }
        }

        public async Task<bool> Eliminar(int idTalle)
        {
            try
            {
                Talle talle_encontrado = await _repositorio.Obtener(p => p.IdTalle == idTalle);

                if (talle_encontrado == null) throw new TaskCanceledException("El talle no existe");

                bool respuesta = await _repositorio.Eliminar(talle_encontrado);

                return true;
            }
            catch { throw; }
        }
    }
}
