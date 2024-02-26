using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SistemaVenta.AplicacionWeb.Models.ViewModels;
using SistemaVenta.BLL.Interfaces;

namespace SistemaVenta.AplicacionWeb.Controllers
{
    public class TipoPagoController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ITipoPagoService _tipoPagoServicio;

        public TipoPagoController(IMapper mapper, ITipoPagoService tipoPagoServicio)
        {
            _mapper = mapper;
            _tipoPagoServicio = tipoPagoServicio;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<VMTipoPago> vmTipoPagoLista = _mapper.Map<List<VMTipoPago>>(await _tipoPagoServicio.Lista());
            return StatusCode(StatusCodes.Status200OK, new { data = vmTipoPagoLista });
        }
    }
}
