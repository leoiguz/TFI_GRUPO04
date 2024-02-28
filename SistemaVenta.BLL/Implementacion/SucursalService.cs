using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaVenta.BLL.Interfaces;
using SistemaVenta.DAL.Interfaces;
using SistemaVenta.Entity;

namespace SistemaVenta.BLL.Implementacion
{
    public class SucursalService : ISucursalService
    {
        private readonly IGenericRepository<Sucursal> _repositorio;

        public SucursalService(IGenericRepository<Sucursal> repositorio)
        {
            _repositorio = repositorio;
        }
        public async Task<Sucursal> Obtener()
        {
            try
            {
                Sucursal sucursal_encontrado = await _repositorio.Obtener(n => n.IdSucursal == 1); //Desde aqui se pone Id 1 porque manejamos solo 1 Sucursal
                return sucursal_encontrado;
            }
            catch
            {
                throw;
            }
        }
        public async Task<Sucursal> GuardarCambios(Sucursal entidad)
        {
            try
            {
                Sucursal sucursal_encontrado = await _repositorio.Obtener(n => n.IdSucursal == 1);

                sucursal_encontrado.NumeroDocumento = entidad.NumeroDocumento;
                sucursal_encontrado.Nombre = entidad.Nombre;
                sucursal_encontrado.Correo = entidad.Correo;
                sucursal_encontrado.Domicilio = entidad.Domicilio;
                sucursal_encontrado.Ciudad = entidad.Ciudad;
                sucursal_encontrado.Telefono = entidad.Telefono;
                sucursal_encontrado.IdCondicionTributaria = entidad.IdCondicionTributaria;

                await _repositorio.Editar(sucursal_encontrado);
                return sucursal_encontrado;
            }
            catch
            {
                throw;
            }
        }


    }
}
