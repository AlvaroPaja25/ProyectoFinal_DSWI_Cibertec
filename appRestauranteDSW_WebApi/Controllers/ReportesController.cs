using appRestauranteDSW_WebApi.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace appRestauranteDSW_WebApi.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class ReportesController : ControllerBase
    {   
        private readonly RestauranteContext _ctx;
        public ReportesController(RestauranteContext ctx) => _ctx = ctx;

        // 1. Platos más vendidos por año
        // GET: api/reportes/platos-mas-vendidos/2024
        [HttpGet("platos-mas-vendidos/{year}")]
        public async Task<IActionResult> GetPlatosMasVendidosPorAnio(int year)
        {
            var data = await _ctx.detalle_comanda
                .Include(d => d.plato)
                .ThenInclude(p => p.categoria_plato)
                .Include(d => d.comanda)
                .Where(d => d.comanda != null &&
                            d.comanda.comprobante.Any(c => c.fecha_emision.HasValue &&
                                c.fecha_emision.Value.Year == year))
                .GroupBy(d => new {
                    PlatoId = d.plato_id,
                    PlatoNombre = d.plato!.nombre,
                    Categoria = d.plato.categoria_plato != null ? d.plato.categoria_plato.nombre : null
                })
                .Select(g => new {
                    g.Key.PlatoId,
                    g.Key.PlatoNombre,
                    g.Key.Categoria,
                    CantidadVendida = g.Sum(x => x.cantidad_pedido ?? 0)
                })
                .OrderByDescending(x => x.CantidadVendida)
                .ToListAsync();

            return Ok(data);
        }

        // 2. Empleados con más comandas
        // GET: api/reportes/empleados-top-comandas
        [HttpGet("empleados-top-comandas")]
        public async Task<IActionResult> GetEmpleadosConMasComandas()
        {
            var data = await _ctx.comanda
                .Include(c => c.empleado)
                .Where(c => c.empleado != null)
                .GroupBy(c => new {
                    EmpleadoId = c.empleado!.id,
                    Nombre = c.empleado.nombre,
                    Apellido = c.empleado.apellido
                })
                .Select(g => new {
                    g.Key.EmpleadoId,
                    Empleado = g.Key.Nombre + " " + g.Key.Apellido,
                    TotalComandas = g.Count(),
                    TotalVentas = g.Sum(c => c.precio_total ?? 0)
                })
                .OrderByDescending(x => x.TotalComandas)
                .ToListAsync();

            return Ok(data);
        }

        // 3. Ingresos e IGV por año (para Google Charts)
        // GET: api/reportes/ingresos-por-anio
        [HttpGet("ingresos-por-anio")]
        public async Task<IActionResult> GetIngresosPorAnio()
        {
            var data = await _ctx.comprobante
                .Where(c => c.fecha_emision.HasValue)
                .GroupBy(c => c.fecha_emision!.Value.Year)
                .Select(g => new {
                    Anio = g.Key,
                    Ingresos = g.Sum(x => x.precio_total_pedido ?? 0),
                    IGV = g.Sum(x => x.igv_total ?? 0)
                })
                .OrderBy(x => x.Anio)
                .ToListAsync();

            // Google Charts suele esperar un array tipo [["Año","Ingresos","IGV"],[2023,1234,567],...]
            var chartData = new List<object> { new object[] { "Año", "Ingresos", "IGV" } };
            foreach (var item in data)
            {
                chartData.Add(new object[] { item.Anio.ToString(), item.Ingresos, item.IGV });
            }

            return Ok(chartData);
        }
    }
}
