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
    public class MarcaController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMarcaService _marcaServicio;

        public MarcaController(IMapper mapper, IMarcaService marcaServicio)
        {
            _mapper = mapper;
            _marcaServicio = marcaServicio;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<VMMarca> vmMarcaLista = _mapper.Map<List<VMMarca>>(await _marcaServicio.Lista());
            return StatusCode(StatusCodes.Status200OK, new { data = vmMarcaLista });
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] VMMarca modelo)
        {
            GenericResponse<VMMarca> gResponse = new GenericResponse<VMMarca>();

            try
            {
                Marca marca_creada = await _marcaServicio.Crear(_mapper.Map<Marca>(modelo));
                modelo = _mapper.Map<VMMarca>(marca_creada);

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
        public async Task<IActionResult> Editar([FromBody] VMMarca modelo)
        {
            GenericResponse<VMMarca> gResponse = new GenericResponse<VMMarca>();

            try
            {
                Marca marca_editado = await _marcaServicio.Editar(_mapper.Map<Marca>(modelo));
                modelo = _mapper.Map<VMMarca>(marca_editado);

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
        public async Task<IActionResult> Eliminar(int IdMarca)
        {
            GenericResponse<string> gResponse = new GenericResponse<string>();
            try
            {
                gResponse.Estado = await _marcaServicio.Eliminar(IdMarca);
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
