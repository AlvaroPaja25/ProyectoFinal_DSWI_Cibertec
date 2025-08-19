using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

public class LoginController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public LoginController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(); // Vista con formulario de login
    }

    [HttpPost]
    public async Task<IActionResult> Index(string correo, string contrasena)
    {
        var client = _httpClientFactory.CreateClient();
        client.BaseAddress = new Uri("https://localhost:7296"); // URL de tu API

        var loginRequest = new
        {
            correo = correo,        
            contrasena = contrasena  
        };

        var content = new StringContent(JsonConvert.SerializeObject(loginRequest), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("/api/Auth/login", content);

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<LoginResponse>(json);

            // Guardar token y rol en sesión
            HttpContext.Session.SetString("JWToken", result.Token);
            HttpContext.Session.SetString("Rol", result.Rol);
            HttpContext.Session.SetString("Usuario", result.Usuario);

            return RedirectToAction("Index", "Home");
        }

        ViewBag.Error = "Credenciales inválidas";
        return View();
    }
}

public class LoginResponse
{
    public string Token { get; set; }
    public string Usuario { get; set; }
    public string Rol { get; set; }
    public DateTime Expira { get; set; }
}
