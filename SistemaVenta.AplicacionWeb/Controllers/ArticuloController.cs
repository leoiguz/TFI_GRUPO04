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
    public class ArticuloController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IArticuloService _articuloServicio;

        public ArticuloController(IMapper mapper, IArticuloService articuloServicio)
        {
            _mapper = mapper;
            _articuloServicio = articuloServicio;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<VMArticulo> vmArticuloLista = _mapper.Map<List<VMArticulo>>(await _articuloServicio.Lista());
            return StatusCode(StatusCodes.Status200OK, new { data = vmArticuloLista });
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromForm] string modelo)
        {
            GenericResponse<VMArticulo> gResponse = new GenericResponse<VMArticulo>();
            try
            {
                VMArticulo vMArticulo = JsonConvert.DeserializeObject<VMArticulo>(modelo);

                Articulo articulo_creado = await _articuloServicio.Crear(_mapper.Map<Articulo>(vMArticulo));

                vMArticulo = _mapper.Map<VMArticulo>(articulo_creado);

                gResponse.Estado = true;
                gResponse.Objeto = vMArticulo;
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
            GenericResponse<VMArticulo> gResponse = new GenericResponse<VMArticulo>();
            try
            {
                VMArticulo vMArticulo = JsonConvert.DeserializeObject<VMArticulo>(modelo);

                Articulo articulo_editado = await _articuloServicio.Editar(_mapper.Map<Articulo>(vMArticulo));

                vMArticulo = _mapper.Map<VMArticulo>(articulo_editado);

                gResponse.Estado = true;
                gResponse.Objeto = vMArticulo;
            }
            catch (Exception ex)
            {
                gResponse.Estado = false;
                gResponse.Mensaje = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, gResponse);
        }

        [HttpDelete]
        public async Task<IActionResult> Eliminar(int IdArticulo)
        {
            GenericResponse<string> gResponse = new GenericResponse<string>();

            try
            {

                gResponse.Estado = await _articuloServicio.Eliminar(IdArticulo);
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
