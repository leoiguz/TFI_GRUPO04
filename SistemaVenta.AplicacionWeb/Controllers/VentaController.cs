using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using SistemaVenta.AplicacionWeb.Models.ViewModels;
using SistemaVenta.AplicacionWeb.Utilidades.Response;
using SistemaVenta.BLL.Interfaces;
using SistemaVenta.Entity;

using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using AFIPService;
using NuGet.Protocol.Plugins;

namespace SistemaVenta.AplicacionWeb.Controllers
{
    [Authorize]
    public class VentaController : Controller
    {
        private readonly ITipoComprobanteService _tipoComprobanteServicio;
        private readonly IVentaService _ventaServicio;
        private readonly IMapper _mapper;
        private readonly IConverter _converter;

        public VentaController(ITipoComprobanteService tipoComprobanteServicio,
           IVentaService ventaServicio,
           IMapper mapper,
           IConverter converter
           )
        {
            _tipoComprobanteServicio = tipoComprobanteServicio;
            _ventaServicio = ventaServicio;
            _mapper = mapper;
            _converter = converter;
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
        public async Task<ActionResult> SolicitarAutorizacion(string codigo)
        {
            try
            {
                var cliente = new LoginServiceClient();

                var token = await cliente.SolicitarAutorizacionAsync(codigo);
                cliente.Close();

                return StatusCode(StatusCodes.Status200OK, token);
            }
            catch (Exception ex)
            {
                // Maneja cualquier excepción que ocurra durante la solicitud
                return Json(new { Error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarVenta([FromBody] VMVenta modelo) { 
            GenericResponse<VMVenta> gResponse = new GenericResponse<VMVenta>();

            try
            {

                ClaimsPrincipal claimUser = HttpContext.User;
                string idUsuario = claimUser.Claims
                    .Where(c => c.Type == ClaimTypes.NameIdentifier)
                    .Select(c => c.Value).SingleOrDefault();

                modelo.IdUsuario = int.Parse(idUsuario);
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

        [HttpGet]
        public async Task<IActionResult> ObtenerTipoComprobante(string busqueda)
        {
            VMTipoComprobante vmTipoComprobante = _mapper.Map<VMTipoComprobante>(await _ventaServicio.TipoComprobantePorCondicionTributaria(busqueda));

            return StatusCode(StatusCodes.Status200OK, vmTipoComprobante);
        }

        public IActionResult MostrarPDFVenta(string numeroVenta)
        {

            string urlPlantillaVista = $"{this.Request.Scheme}://{this.Request.Host}/Plantilla/PDFVenta?numeroVenta={numeroVenta}";

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = new GlobalSettings()
                {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait,
                },
                Objects = {
                    new ObjectSettings(){
                        Page = urlPlantillaVista
                    }
                }
            };

            var archivoPDF = _converter.Convert(pdf);

            return File(archivoPDF, "application/pdf");

        }


    }
}
