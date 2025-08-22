using appRestauranteDSW_WebApi.Data;
using appRestauranteDSW_WebApi.Data.Entities;
using appRestauranteDSW_WebApi.Data.Services;
using appRestauranteDSW_WebApi.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace appRestauranteDSW_WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComandaController : ControllerBase
    {
        private readonly ComandaService _comandaService;

        public ComandaController(ComandaService comandaService)
        {
            _comandaService = comandaService;
        }

        [HttpPost]
        public async Task<ActionResult<ComandaResponse>> CrearComanda([FromBody] ComandaRequest request)
        {
            var response = await _comandaService.CrearComandaAsync(request);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ComandaResponse>> ObtenerComanda(int id)
        {
            var comanda = await _comandaService.ObtenerComandaPorIdAsync(id);
            if (comanda == null) return NotFound();

            return Ok(comanda);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComandaResponse>>> ListarComandas()
        {
            return Ok(await _comandaService.ListarComandasAsync());
        }

        [HttpPut("{id}/estado")]
        public async Task<ActionResult> ActualizarEstado(int id, [FromBody] string nuevoEstado)
        {
            var actualizado = await _comandaService.ActualizarEstadoComandaAsync(id, nuevoEstado);
            if (!actualizado) return BadRequest("No se pudo actualizar el estado");

            return Ok("Estado actualizado correctamente");
        }
    }
}
