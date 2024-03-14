using Microsoft.EntityFrameworkCore;
using SistemaVenta.BLL.Interfaces;
using SistemaVenta.DAL.Interfaces;
using SistemaVenta.Entity;

namespace SistemaVenta.BLL.Implementacion
{
    public class ClienteService :IClienteService
    {
        private readonly IGenericRepository<Cliente> _repositorio;
        private readonly IUtilidadesService _utilidadesServicio;

        public ClienteService(IGenericRepository<Cliente> repositorio, IUtilidadesService utilidadesServicio)
        {
            _repositorio = repositorio;
            _utilidadesServicio = utilidadesServicio;
        }

        public async Task<List<Cliente>> Lista()
        {
            IQueryable<Cliente> query = await _repositorio.Consultar();
            return query.Include(c => c.IdCondicionTributariaNavigation).ToList();
        }

        public async Task<Cliente> Crear(Cliente entidad)
        {;

            try
            {

                Cliente cliente_creado = await _repositorio.Crear(entidad);

                if (cliente_creado.IdCliente == 0) throw new TaskCanceledException("No se pudo crear el cliente");

                IQueryable<Cliente> query = await _repositorio.Consultar(p => p.IdCliente == cliente_creado.IdCliente);

                cliente_creado = query.Include(c => c.IdCondicionTributariaNavigation).First();

                return cliente_creado;
            }
            catch { throw; }

        }

        public async Task<Cliente> Editar(Cliente entidad)
        {

            try
            {
                IQueryable<Cliente> queryCliente = await _repositorio.Consultar(p => p.IdCliente == entidad.IdCliente);

                Cliente cliente_editar = queryCliente.First();

                cliente_editar.NumeroDocumento = entidad.NumeroDocumento;
                cliente_editar.Nombres = entidad.Nombres;
                cliente_editar.Apellidos = entidad.Apellidos;
                cliente_editar.Telefono = entidad.Telefono;
                cliente_editar.Domicilio = entidad.Domicilio;
                cliente_editar.IdCondicionTributaria = entidad.IdCondicionTributaria;
                cliente_editar.EsActivo = entidad.EsActivo;

                bool respuesta = await _repositorio.Editar(cliente_editar);

                if (!respuesta) throw new TaskCanceledException("No se pudo editar el cliente");

                Cliente cliente_editado = queryCliente
                    .Include(c => c.IdCondicionTributariaNavigation).First();


                return cliente_editado;

            }
            catch { throw; }
        }

        public async Task<bool> Eliminar(int idCliente)
        {
            try
            {
                Cliente cliente_encontrado = await _repositorio.Obtener(p => p.IdCliente == idCliente);

                if (cliente_encontrado == null) throw new TaskCanceledException("El cliente no existe");

                bool respuesta = await _repositorio.Eliminar(cliente_encontrado);

                return true;
            }
            catch { throw; }
        }
    }
}
