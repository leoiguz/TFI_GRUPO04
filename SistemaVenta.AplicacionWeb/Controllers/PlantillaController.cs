using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using SistemaVenta.AplicacionWeb.Models.ViewModels;
using SistemaVenta.BLL.Interfaces;

namespace SistemaVenta.AplicacionWeb.Controllers
{
    public class PlantillaController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ISucursalService _sucursalServicio;
        private readonly IVentaService _ventaServicio;

        public PlantillaController(IMapper mapper, ISucursalService sucursalServicio, IVentaService ventaServicio)
        {
            _mapper = mapper;
            _sucursalServicio = sucursalServicio;
            _ventaServicio = ventaServicio;
        }

        public IActionResult EnviarClave(string correo,string clave)
        {
            ViewData["Correo"]= correo;
            ViewData["Clave"] = clave;
            ViewData["Url"] = $"{this.Request.Scheme}://{this.Request.Host}";

            return View();
        }

        public async Task<IActionResult> PDFVenta(string numeroVenta)
        {

            VMVenta vmVenta = _mapper.Map<VMVenta>(await _ventaServicio.Detalle(numeroVenta));
            VMSucursal vmSucursal = _mapper.Map<VMSucursal>(await _sucursalServicio.Obtener());

            VMPDFVenta modelo = new VMPDFVenta();

            modelo.sucursal = vmSucursal;
            modelo.venta = vmVenta;

            return View(modelo);
        }

        public IActionResult RestablecerClave(string clave)
        {
            ViewData["Clave"] = clave;
            
            return View();
        }


    }
}
