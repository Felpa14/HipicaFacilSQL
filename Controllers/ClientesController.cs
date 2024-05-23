using Microsoft.AspNetCore.Mvc;

namespace HipicaFacilSQL.Controllers
{
    public class ClientesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
