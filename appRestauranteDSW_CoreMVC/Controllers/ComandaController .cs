using appRestauranteDSW_CoreMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace appRestauranteDSW_CoreMVC.Controllers
{
    public class ComandaController : Controller
    {
        private readonly ComandaApiService _apiService;

        public ComandaController(ComandaApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var comandas = await _apiService.GetComandasAsync();
            return View(comandas);
        }

    }
}
