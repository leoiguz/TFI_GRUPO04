﻿using SistemaVenta.BLL.Interfaces;
using SistemaVenta.DAL.Interfaces;
using SistemaVenta.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.BLL.Implementacion
{
    public class TipoPagoService :ITipoPagoService
    {
        private readonly IGenericRepository<TipoPago> _repositorio;

        public TipoPagoService(IGenericRepository<TipoPago> repositorio)
        {
            _repositorio = repositorio;

        }


        public async Task<List<TipoPago>> Lista()
        {
            IQueryable<TipoPago> query = await _repositorio.Consultar();
            return query.ToList();
        }
    }
}