using appRestauranteDSW_CoreMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace appRestauranteDSW_CoreMVC.Controllers
{
    public class UsuariosController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Usuario> temporal = new List<Usuario>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7296/api/Usuarios");
                HttpResponseMessage response = await client.GetAsync(" ");
                string apiResponse = await response.Content.ReadAsStringAsync();

                temporal = JsonConvert.DeserializeObject<List<Usuario>>(apiResponse).ToList();
            }
            return View(await Task.Run(() => temporal));
        }

    }
}
