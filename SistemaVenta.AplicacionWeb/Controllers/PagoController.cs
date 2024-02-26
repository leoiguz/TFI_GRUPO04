using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SistemaVenta.AplicacionWeb.Models.ViewModels;
using SistemaVenta.AplicacionWeb.Utilidades.Response;
using SistemaVenta.BLL.Interfaces;
using SistemaVenta.Entity;

namespace SistemaVenta.AplicacionWeb.Controllers
{
    public class PagoController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPagoService _pagoServicio;

        public PagoController(IMapper mapper, IPagoService pagoServicio)
        {
            _mapper = mapper;
            _pagoServicio = pagoServicio;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromForm] string modelo)
        {
            GenericResponse<VMPago> gResponse = new GenericResponse<VMPago>();
            try
            {
                VMPago vMPago = JsonConvert.DeserializeObject<VMPago>(modelo);

                Pago pago_creado = await _pagoServicio.Crear(_mapper.Map<Pago>(vMPago));

                vMPago = _mapper.Map<VMPago>(pago_creado);

                gResponse.Estado = true;
                gResponse.Objeto = vMPago;
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
