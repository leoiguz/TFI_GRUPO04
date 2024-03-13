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
using Newtonsoft.Json;

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
        public async Task<IActionResult> SolicitarAutorizacion([FromBody] string codigo)
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
                return Json(new { Error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> SolicitarUltimosComprobantes([FromBody] string token)
        {

            try
            {
                var cliente = new LoginServiceClient();
                var ultimosComprobantes = await cliente.SolicitarUltimosComprobantesAsync(token);
                cliente.Close();

                return StatusCode(StatusCodes.Status200OK, ultimosComprobantes);
            }
            catch (Exception ex)
            {
                return Json(new { Error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> SolicitarCae([FromBody] VMCae ventaCae)
        {

            try
            {

                int numeroTipoComprobante = ventaCae.TipoComprobante;
                int numeroTipoDocumento = ventaCae.TipoDocumento;

                AFIPService.TipoComprobante tipoComprobante = AFIPService.TipoComprobante.FacturaB; ;
                AFIPService.TipoDocumento tipoDocumento = AFIPService.TipoDocumento.ConsumidorFinal; ;

                switch (numeroTipoComprobante)
                {
                    case 1:
                        tipoComprobante = AFIPService.TipoComprobante.FacturaA;
                        break;
                    case 6:
                        tipoComprobante = AFIPService.TipoComprobante.FacturaB;
                        break;
                }

                switch (numeroTipoDocumento)
                {
                    case 80:
                        tipoDocumento = AFIPService.TipoDocumento.Cuit;
                        break;
                    case 86:
                        tipoDocumento = AFIPService.TipoDocumento.Cuil;
                        break;
                    case 96:
                        tipoDocumento = AFIPService.TipoDocumento.Dni;
                        break;
                    case 99:
                        tipoDocumento = AFIPService.TipoDocumento.ConsumidorFinal;
                        break;
                }

                if (tipoDocumento == AFIPService.TipoDocumento.ConsumidorFinal) ventaCae.NumeroDocumento = 0;

                SolicitudAutorizacion solicitud = new SolicitudAutorizacion
                {
                    Fecha = ventaCae.Fecha,
                    ImporteIva = ventaCae.ImporteIva,
                    ImporteNeto = ventaCae.ImporteNeto,
                    ImporteTotal = ventaCae.ImporteTotal,
                    Numero = ventaCae.Numero + 1,
                    NumeroDocumento = ventaCae.NumeroDocumento,
                    TipoComprobante = tipoComprobante,
                    TipoDocumento = tipoDocumento
                };
                var cliente = new LoginServiceClient();
                var cae = await cliente.SolicitarCaeAsync(ventaCae.Token, solicitud);
                cliente.Close();

                return StatusCode(StatusCodes.Status200OK, cae);
            }
            catch (Exception ex)
            {
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

        [HttpGet]
        public async Task<IActionResult> ObtenerAFIP()
        {
            VMAFIP vmAFIP = _mapper.Map<VMAFIP>(await _ventaServicio.ObtenerDatosAFIP());

            return StatusCode(StatusCodes.Status200OK, vmAFIP);
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

        [HttpPut]
        public async Task<IActionResult> EditarAFIP([FromBody] VMAFIP modelo)
        {
            GenericResponse<VMAFIP> gResponse = new GenericResponse<VMAFIP>();

            try
            {
                AFIP afip_editado = await _ventaServicio.EditarAfip(_mapper.Map<AFIP>(modelo));
                modelo = _mapper.Map<VMAFIP>(afip_editado);

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

    }
}
