using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SistemaVenta.AplicacionWeb.Models.ViewModels;
using SistemaVenta.AplicacionWeb.Utilidades.Response;
using SistemaVenta.BLL.Interfaces;
using SistemaVenta.Entity;

namespace SistemaVenta.AplicacionWeb.Controllers
{
    public class TipoTalleController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ITipoTalleService _tipoTalleServicio;

        public TipoTalleController(IMapper mapper, ITipoTalleService tipoTalleServicio)
        {
            _mapper = mapper;
            _tipoTalleServicio = tipoTalleServicio;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<VMTipoTalle> vmTipoTalleLista = _mapper.Map<List<VMTipoTalle>>(await _tipoTalleServicio.Lista());
            return StatusCode(StatusCodes.Status200OK, new { data = vmTipoTalleLista });
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] VMTipoTalle modelo)
        {
            GenericResponse<VMTipoTalle> gResponse = new GenericResponse<VMTipoTalle>();

            try
            {
                TipoTalle tipoTalle_creada = await _tipoTalleServicio.Crear(_mapper.Map<TipoTalle>(modelo));
                modelo = _mapper.Map<VMTipoTalle>(tipoTalle_creada);

                gResponse.Estado = true;
                gResponse.Objeto = modelo;
            }
            catch (Exception ex)
            {
                gResponse.Estado = false;
                gResponse.Mensaje = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, gResponse);
        }

        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] VMTipoTalle modelo)
        {
            GenericResponse<VMTipoTalle> gResponse = new GenericResponse<VMTipoTalle>();

            try
            {
                TipoTalle tipoTalle_editado = await _tipoTalleServicio.Editar(_mapper.Map<TipoTalle>(modelo));
                modelo = _mapper.Map<VMTipoTalle>(tipoTalle_editado);

                gResponse.Estado = true;
                gResponse.Objeto = modelo;
            }
            catch (Exception ex)
            {
                gResponse.Estado = false;
                gResponse.Mensaje = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, gResponse);
        }

        [HttpDelete]
        public async Task<IActionResult> Eliminar(int IdTipoTalle)
        {
            GenericResponse<string> gResponse = new GenericResponse<string>();
            try
            {
                gResponse.Estado = await _tipoTalleServicio.Eliminar(IdTipoTalle);
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
