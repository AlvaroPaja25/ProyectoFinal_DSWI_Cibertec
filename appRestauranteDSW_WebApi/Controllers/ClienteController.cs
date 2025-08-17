using Microsoft.AspNetCore.Mvc;

namespace appRestauranteDSW_WebApi.Controllers
{
    public class ClienteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
