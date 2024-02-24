using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using Newtonsoft.Json;
using SistemaVenta.AplicacionWeb.Models.ViewModels;
using SistemaVenta.AplicacionWeb.Utilidades.Response;
using SistemaVenta.BLL.Interfaces;
using SistemaVenta.Entity;
using Microsoft.AspNetCore.Authorization;

namespace SistemaVenta.AplicacionWeb.Controllers
{
    [Authorize]
    public class SucursalController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ISucursalService _sucursalServicio;

        public SucursalController(IMapper mapper, ISucursalService sucursalService)
        {
            _mapper = mapper;
            _sucursalServicio = sucursalService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            GenericResponse<VMSucursal> gResponse = new GenericResponse<VMSucursal>();
            try
            {
                VMSucursal vmSucursal = _mapper.Map<VMSucursal>(await _sucursalServicio.Obtener());
                gResponse.Estado = true;
                gResponse.Objeto = vmSucursal;
            }
            catch (Exception ex)
            {
                gResponse.Estado = false;
                gResponse.Mensaje = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, gResponse);

        }

        [HttpPost]
        public async Task<IActionResult> GuardarCambios([FromForm] string modelo)
        {
            GenericResponse<VMSucursal> gResponse = new GenericResponse<VMSucursal>();
            try
            {
                VMSucursal vmSucursal = JsonConvert.DeserializeObject<VMSucursal>(modelo);



                Sucursal sucursal_editado = await _sucursalServicio.GuardarCambios(_mapper.Map<Sucursal>(vmSucursal));

                vmSucursal = _mapper.Map<VMSucursal>(sucursal_editado);

                gResponse.Estado = true;
                gResponse.Objeto = vmSucursal;
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
