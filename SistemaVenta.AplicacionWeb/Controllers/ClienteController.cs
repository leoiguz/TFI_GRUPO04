using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SistemaVenta.AplicacionWeb.Models.ViewModels;
using SistemaVenta.AplicacionWeb.Utilidades.Response;
using SistemaVenta.BLL.Interfaces;
using SistemaVenta.Entity;

namespace SistemaVenta.AplicacionWeb.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IClienteService _clienteServicio;

        public ClienteController(IMapper mapper, IClienteService clienteServicio)
        {
            _mapper = mapper;
            _clienteServicio = clienteServicio;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<VMCliente> vmClienteLista = _mapper.Map<List<VMCliente>>(await _clienteServicio.Lista());
            return StatusCode(StatusCodes.Status200OK, new { data = vmClienteLista });
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromForm] string modelo)
        {
            GenericResponse<VMCliente> gResponse = new GenericResponse<VMCliente>();
            try
            {
                VMCliente vMCliente = JsonConvert.DeserializeObject<VMCliente>(modelo);

                Cliente cliente_creado = await _clienteServicio.Crear(_mapper.Map<Cliente>(vMCliente));

                vMCliente = _mapper.Map<VMCliente>(cliente_creado);

                gResponse.Estado = true;
                gResponse.Objeto = vMCliente;
            }
            catch (Exception ex)
            {
                gResponse.Estado = false;
                gResponse.Mensaje = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, gResponse);
        }


        [HttpPut]
        public async Task<IActionResult> Editar([FromForm] string modelo)
        {
            GenericResponse<VMCliente> gResponse = new GenericResponse<VMCliente>();
            try
            {
                VMCliente vMCliente = JsonConvert.DeserializeObject<VMCliente>(modelo);

                Cliente cliente_editado = await _clienteServicio.Editar(_mapper.Map<Cliente>(vMCliente));

                vMCliente = _mapper.Map<VMCliente>(cliente_editado);

                gResponse.Estado = true;
                gResponse.Objeto = vMCliente;
            }
            catch (Exception ex)
            {
                gResponse.Estado = false;
                gResponse.Mensaje = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, gResponse);
        }

        [HttpDelete]
        public async Task<IActionResult> Eliminar(int IdCliente)
        {
            GenericResponse<string> gResponse = new GenericResponse<string>();

            try
            {

                gResponse.Estado = await _clienteServicio.Eliminar(IdCliente);
            }
            catch (Exception ex)
            {
                gResponse.Estado = false;
                gResponse.Mensaje = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, gResponse);
        }
    }
}
