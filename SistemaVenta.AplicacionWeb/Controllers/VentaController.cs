using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using SistemaVenta.AplicacionWeb.Models.ViewModels;
using SistemaVenta.AplicacionWeb.Utilidades.Response;
using SistemaVenta.BLL.Interfaces;
using SistemaVenta.Entity;

namespace SistemaVenta.AplicacionWeb.Controllers
{
    public class VentaController : Controller
    {
        private readonly ITipoComprobanteService _tipoComprobanteServicio;
        private readonly IVentaService _ventaServicio;
        private readonly IMapper _mapper;

        public VentaController(ITipoComprobanteService tipoComprobanteServicio,
           IVentaService ventaServicio,
           IMapper mapper
           )
        {
            _tipoComprobanteServicio = tipoComprobanteServicio;
            _ventaServicio = ventaServicio;
            _mapper = mapper;
        }
        public IActionResult NuevaVenta()
        {
            return View();
        }

        public IActionResult HistorialVenta()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListaTipoComprobante()
        {
            List<VMTipoComprobante> vmListaTipoDocumentos = _mapper.Map<List<VMTipoComprobante>>(await _tipoComprobanteServicio.Lista());

            return StatusCode(StatusCodes.Status200OK, vmListaTipoDocumentos);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerInventarios(string busqueda)
        {
            List<VMInventario> vmListaInventarios = _mapper.Map<List<VMInventario>>(await _ventaServicio.ObtenerInventario(busqueda));

            return StatusCode(StatusCodes.Status200OK, vmListaInventarios);
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarVenta([FromBody] VMVenta modelo) { 
            GenericResponse<VMVenta> gResponse = new GenericResponse<VMVenta>();

            try
            {
                modelo.IdUsuario = 1;
                modelo.IdPuntoVenta = 1;
                Venta venta_creada = await _ventaServicio.Registrar(_mapper.Map<Venta>(modelo));
                modelo = _mapper.Map<VMVenta>(venta_creada);

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

        [HttpGet]
        public async Task<IActionResult> Historial(string numeroVenta, string fechaInicio, string fechaFin)
        {
            List<VMVenta> vmHistorialVenta = _mapper.Map<List<VMVenta>>(await _ventaServicio.Historial(numeroVenta, fechaInicio, fechaFin));

            return StatusCode(StatusCodes.Status200OK, vmHistorialVenta);
        }


        [HttpGet]
        public async Task<IActionResult> ObtenerClientes(string busqueda)
        {
            List<VMCliente> vmListaClientes = _mapper.Map<List<VMCliente>>(await _ventaServicio.ObtenerCliente(busqueda));

            return StatusCode(StatusCodes.Status200OK, vmListaClientes);
        }
    }
}
