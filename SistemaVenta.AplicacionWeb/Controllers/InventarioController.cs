using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SistemaVenta.AplicacionWeb.Models.ViewModels;
using SistemaVenta.AplicacionWeb.Utilidades.Response;
using SistemaVenta.BLL.Interfaces;
using SistemaVenta.Entity;

namespace SistemaVenta.AplicacionWeb.Controllers
{
    [Authorize]
    public class InventarioController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IInventarioService _inventarioServicio;

        public InventarioController(IMapper mapper, IInventarioService inventarioServicio)
        {
            _mapper = mapper;
            _inventarioServicio = inventarioServicio;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<VMInventario> vmInventarioLista = _mapper.Map<List<VMInventario>>(await _inventarioServicio.Lista());
            return StatusCode(StatusCodes.Status200OK, new { data = vmInventarioLista });
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromForm] string modelo)
        {
            GenericResponse<VMInventario> gResponse = new GenericResponse<VMInventario>();
            try
            {
                VMInventario vMInventario = JsonConvert.DeserializeObject<VMInventario>(modelo);

                Inventario inventario_creado = await _inventarioServicio.Crear(_mapper.Map<Inventario>(vMInventario));

                vMInventario = _mapper.Map<VMInventario>(inventario_creado);

                gResponse.Estado = true;
                gResponse.Objeto = vMInventario;
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
            GenericResponse<VMInventario> gResponse = new GenericResponse<VMInventario>();
            try
            {
                VMInventario vMInventario = JsonConvert.DeserializeObject<VMInventario>(modelo);

                Inventario inventario_editado = await _inventarioServicio.Editar(_mapper.Map<Inventario>(vMInventario));

                vMInventario = _mapper.Map<VMInventario>(inventario_editado);

                gResponse.Estado = true;
                gResponse.Objeto = vMInventario;
            }
            catch (Exception ex)
            {
                gResponse.Estado = false;
                gResponse.Mensaje = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, gResponse);
        }

        [HttpDelete]
        public async Task<IActionResult> Eliminar(int IdInventario)
        {
            GenericResponse<string> gResponse = new GenericResponse<string>();

            try
            {

                gResponse.Estado = await _inventarioServicio.Eliminar(IdInventario);
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
