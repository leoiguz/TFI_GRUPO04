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
    public class TalleController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ITalleService _talleServicio;

        public TalleController(IMapper mapper, ITalleService talleServicio)
        {
            _mapper = mapper;
            _talleServicio = talleServicio;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<VMTalle> vmTalleLista = _mapper.Map<List<VMTalle>>(await _talleServicio.Lista());
            return StatusCode(StatusCodes.Status200OK, new { data = vmTalleLista });
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromForm] string modelo)
        {
            GenericResponse<VMTalle> gResponse = new GenericResponse<VMTalle>();
            try
            {
                VMTalle vMTalle = JsonConvert.DeserializeObject<VMTalle>(modelo);

                Talle talle_creado = await _talleServicio.Crear(_mapper.Map<Talle>(vMTalle));

                vMTalle = _mapper.Map<VMTalle>(talle_creado);

                gResponse.Estado = true;
                gResponse.Objeto = vMTalle;
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
            GenericResponse<VMTalle> gResponse = new GenericResponse<VMTalle>();
            try
            {
                VMTalle vMTalle = JsonConvert.DeserializeObject<VMTalle>(modelo);

                Talle talle_editado = await _talleServicio.Editar(_mapper.Map<Talle>(vMTalle));

                vMTalle = _mapper.Map<VMTalle>(talle_editado);

                gResponse.Estado = true;
                gResponse.Objeto = vMTalle;
            }
            catch (Exception ex)
            {
                gResponse.Estado = false;
                gResponse.Mensaje = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, gResponse);
        }

        [HttpDelete]
        public async Task<IActionResult> Eliminar(int IdTalle)
        {
            GenericResponse<string> gResponse = new GenericResponse<string>();

            try
            {

                gResponse.Estado = await _talleServicio.Eliminar(IdTalle);
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
