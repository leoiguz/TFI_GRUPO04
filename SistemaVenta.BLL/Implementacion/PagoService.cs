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
    public class PagoService : IPagoService
    {
        private readonly IGenericRepository<Pago> _repositorio;

        public PagoService(IGenericRepository<Pago> repositorio)
        {
            _repositorio = repositorio;
        }
        public async Task<Pago> Crear(Pago entidad)
        {
            try
            {

                Pago pago_creado = await _repositorio.Crear(entidad);

                if (pago_creado.IdPago == 0) throw new TaskCanceledException("No se pudo crear el pago");

                IQueryable<Pago> query = await _repositorio.Consultar(p => p.IdPago == pago_creado.IdPago);

                pago_creado = query
                    .Include(c => c.IdTipoPagoNavigation)
                    .First();

                return pago_creado;
            }
            catch { throw; }

        }
    }
}