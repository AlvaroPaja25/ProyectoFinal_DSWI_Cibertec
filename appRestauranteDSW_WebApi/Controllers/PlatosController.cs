using appRestauranteDSW_WebApi.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace appRestauranteDSW_WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // requiere JWT
    public class PlatosController : ControllerBase
    {
        private readonly RestauranteContext _ctx;
        public PlatosController(RestauranteContext ctx) => _ctx = ctx;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var platos = await _ctx.plato
                .Include(p => p.categoria_plato)
                .ToListAsync();
            return Ok(platos);
        }
    }
}
