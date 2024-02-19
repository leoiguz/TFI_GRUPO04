using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SistemaVenta.AplicacionWeb.Models.ViewModels;
using SistemaVenta.AplicacionWeb.Utilidades.Response;
using SistemaVenta.BLL.Interfaces;

namespace SistemaVenta.AplicacionWeb.Controllers
{
    public class DashBoardController : Controller
    {

        private readonly IDashBoardService _dashboardServicio;

        public DashBoardController(IDashBoardService dashboardServicio)
        {
            _dashboardServicio = dashboardServicio;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerResumen()
        {
            GenericResponse<VMDashBoard> gResponse = new GenericResponse<VMDashBoard>();
            try
            {
                VMDashBoard vmDashBoard = new VMDashBoard();

                vmDashBoard.TotalVentas = await _dashboardServicio.TotalVentasUltimaSemana();
                vmDashBoard.TotalIngresos = await _dashboardServicio.TotalIngresosUltimaSemana();
                vmDashBoard.TotalInventarios = await _dashboardServicio.TotalInventarios();
                vmDashBoard.TotalArticulos = await _dashboardServicio.TotalArticulos();

                List<VMVentasSemana> listaVentasSemana = new List<VMVentasSemana>();
                List<VMInventariosSemana> listaInventariosSemana = new List<VMInventariosSemana>();

                foreach (KeyValuePair<string, int> item in await _dashboardServicio.VentasUltimaSemana())
                {
                    listaVentasSemana.Add(new VMVentasSemana()
                    {
                        Fecha = item.Key,
                        Total = item.Value
                    });
                }

                foreach (KeyValuePair<string, int> item in await _dashboardServicio.InventariosTopUltimaSemana())
                {
                    listaInventariosSemana.Add(new VMInventariosSemana()
                    {
                        NombreArticulo = item.Key,
                        Cantidad = item.Value
                    });
                }

                vmDashBoard.VentasUltimaSemana = listaVentasSemana;
                vmDashBoard.InventariosTopUltimaSemana = listaInventariosSemana;

                gResponse.Estado = true;
                gResponse.Objeto = vmDashBoard;

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