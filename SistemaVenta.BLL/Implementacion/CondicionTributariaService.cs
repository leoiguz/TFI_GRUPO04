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
    public class CondicionTributariaService :ICondicionTributariaService
    {
        private readonly IGenericRepository<CondicionTributaria> _repositorio;

        public CondicionTributariaService(IGenericRepository<CondicionTributaria> repositorio)
        {
            _repositorio = repositorio;

        }


        public async Task<List<CondicionTributaria>> Lista()
        {
            IQueryable<CondicionTributaria> query = await _repositorio.Consultar();
            return query.ToList();
        }
    }
}
