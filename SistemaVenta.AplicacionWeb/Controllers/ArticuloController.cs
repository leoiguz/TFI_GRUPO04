using Microsoft.AspNetCore.Mvc;

namespace SistemaVenta.AplicacionWeb.Controllers
{
    public class ArticuloController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
