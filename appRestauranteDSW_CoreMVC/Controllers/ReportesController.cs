using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace appRestauranteDSW_CoreMVC.Controllers
{
    [Authorize]
    public class ReportesController : Controller
    {
        private readonly string _apiBase = "https://localhost:7296/api/Reportes";

        public IActionResult Index() => View();

        [HttpGet]
        public async Task<IActionResult> PlatosMasVendidos(int year)
        {
            using var client = new HttpClient();
            var res = await client.GetAsync($"{_apiBase}/platos-mas-vendidos/{year}");
            var json = await res.Content.ReadAsStringAsync();
            return Content(json, "application/json", Encoding.UTF8);
        }

        [HttpGet]
        public async Task<IActionResult> EmpleadosTopComandas()
        {
            using var client = new HttpClient();
            var res = await client.GetAsync($"{_apiBase}/empleados-top-comandas");
            var json = await res.Content.ReadAsStringAsync();
            return Content(json, "application/json", Encoding.UTF8);
        }

        [HttpGet]
        public async Task<IActionResult> IngresosPorAnio()
        {
            using var client = new HttpClient();
            var res = await client.GetAsync($"{_apiBase}/ingresos-por-anio");
            var json = await res.Content.ReadAsStringAsync();
            return Content(json, "application/json", Encoding.UTF8);
        }
    }
}
