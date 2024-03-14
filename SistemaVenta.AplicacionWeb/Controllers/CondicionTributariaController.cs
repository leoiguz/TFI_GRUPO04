using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaVenta.AplicacionWeb.Models.ViewModels;
using SistemaVenta.BLL.Interfaces;

namespace SistemaVenta.AplicacionWeb.Controllers
{
    [Authorize]
    public class CondicionTributariaController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICondicionTributariaService _condicionTributariaServicio;

        public CondicionTributariaController(IMapper mapper, ICondicionTributariaService condicionTributariaServicio)
        {
            _mapper = mapper;
            _condicionTributariaServicio = condicionTributariaServicio;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<VMCondicionTributaria> vmCondicionTributariaLista = _mapper.Map<List<VMCondicionTributaria>>(await _condicionTributariaServicio.Lista());
            return StatusCode(StatusCodes.Status200OK, new { data = vmCondicionTributariaLista });
        }
    }
}
