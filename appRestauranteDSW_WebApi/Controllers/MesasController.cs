using appRestauranteDSW_WebApi.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace appRestauranteDSW_WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize] // requiere JWT
    public class MesasController : ControllerBase
    {
        private readonly RestauranteContext _ctx;
        public MesasController(RestauranteContext ctx) => _ctx = ctx;

        // GET: api/mesas
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var mesas = await _ctx.mesa.ToListAsync();
            return Ok(mesas);
        }

        // GET: api/mesas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var mesa = await _ctx.mesa.FindAsync(id);
            if (mesa == null) return NotFound(new { message = $"No se encontró la mesa con id {id}" });
            return Ok(mesa);
        }

        // POST: api/mesas
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] mesa mesa)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Datos inválidos", errors = ModelState });

            _ctx.mesa.Add(mesa);
            await _ctx.SaveChangesAsync();

            return Ok(new { message = "Mesa creada exitosamente", data = mesa });
        }

        // PUT: api/mesas/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] mesa mesa)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Datos inválidos", errors = ModelState });

            var existingMesa = await _ctx.mesa.FindAsync(id);
            if (existingMesa == null)
                return NotFound(new { message = $"No se encontró la mesa con id {id}" });

            // Actualiza los campos
            existingMesa.cantidad_asientos = mesa.cantidad_asientos;
            existingMesa.estado = mesa.estado;

            await _ctx.SaveChangesAsync();
            return Ok(new { message = "Mesa actualizada correctamente", data = existingMesa });
        }

        // DELETE: api/mesas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var mesa = await _ctx.mesa.FindAsync(id);
            if (mesa == null)
                return NotFound(new { message = $"No se encontró la mesa con id {id}" });

            _ctx.mesa.Remove(mesa);
            await _ctx.SaveChangesAsync();

            return Ok(new { message = "Mesa eliminada correctamente" });
        }
    }
}
