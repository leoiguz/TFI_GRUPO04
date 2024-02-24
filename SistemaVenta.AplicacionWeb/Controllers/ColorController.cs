using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaVenta.AplicacionWeb.Models.ViewModels;
using SistemaVenta.AplicacionWeb.Utilidades.Response;
using SistemaVenta.BLL.Interfaces;
using SistemaVenta.Entity;

namespace SistemaVenta.AplicacionWeb.Controllers
{
    [Authorize]
    public class ColorController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IColorService _colorServicio;

        public ColorController(IMapper mapper, IColorService colorServicio)
        {
            _mapper = mapper;
            _colorServicio = colorServicio;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<VMColor> vmColorLista = _mapper.Map<List<VMColor>>(await _colorServicio.Lista());
            return StatusCode(StatusCodes.Status200OK, new { data = vmColorLista });
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] VMColor modelo)
        {
            GenericResponse<VMColor> gResponse = new GenericResponse<VMColor>();

            try
            {
                Color color_creada = await _colorServicio.Crear(_mapper.Map<Color>(modelo));
                modelo = _mapper.Map<VMColor>(color_creada);

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
        public async Task<IActionResult> Editar([FromBody] VMColor modelo)
        {
            GenericResponse<VMColor> gResponse = new GenericResponse<VMColor>();

            try
            {
                Color color_editado = await _colorServicio.Editar(_mapper.Map<Color>(modelo));
                modelo = _mapper.Map<VMColor>(color_editado);

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
        public async Task<IActionResult> Eliminar(int IdColor)
        {
            GenericResponse<string> gResponse = new GenericResponse<string>();
            try
            {
                gResponse.Estado = await _colorServicio.Eliminar(IdColor);
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
