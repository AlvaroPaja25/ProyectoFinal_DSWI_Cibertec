using appRestauranteDSW_WebApi.Data;
using appRestauranteDSW_WebApi.Data.Entities;
using appRestauranteDSW_WebApi.DTOs;
using Microsoft.EntityFrameworkCore;

namespace appRestauranteDSW_WebApi.Data.Services
{
    public class ComandaService
    {
        private readonly RestauranteContext _context;

        public ComandaService(RestauranteContext context)
        {
            _context = context;
        }

        public async Task<ComandaResponse> CrearComandaAsync(ComandaRequest request)
        {
            var comanda = new comanda
            {
                mesa_id = request.MesaId,
                estado_comanda_id = 1, // Por defecto "Pendiente"
                fecha_emision = DateTime.Now.ToString  ("yyyy-MM-dd HH:mm:ss")
            };

            _context.comanda.Add(comanda);
            await _context.SaveChangesAsync();

            // Insertar detalles
            foreach (var det in request.Detalles)
            {
                var detalle = new detalle_comanda
                {
                    comanda_id = comanda.id,
                    plato_id = det.PlatoId,
                    cantidad_pedido = det.CantidadPedido
                };
                _context.detalle_comanda.Add(detalle);
            }
            await _context.SaveChangesAsync();

            return new ComandaResponse
            {
                Id = comanda.id,
                MesaId = comanda.mesa_id,
                Estado = "Pendiente",
                FechaEmision  = comanda.fecha_emision,
                Detalles = request.Detalles.Select(d => new DetalleComandaResponse
                {
                    PlatoId = d.PlatoId,
                    CantidadPedido = d.CantidadPedido
                }).ToList()
            };
        }

        public async Task<ComandaResponse?> ObtenerComandaPorIdAsync(int id)
        {
            var comanda = await _context.comanda
                .Include(c => c.detalle_comanda)
                .FirstOrDefaultAsync(c => c.id == id);

            if (comanda == null) return null;

            return new ComandaResponse
            {
                Id = comanda.id,
                MesaId = comanda.mesa_id,
                Estado = comanda.estado_comanda?.estado,
                FechaEmision = comanda.fecha_emision,
                Detalles = comanda.detalle_comanda.Select(d => new DetalleComandaResponse
                {
                    PlatoId = d.plato_id,
                    CantidadPedido = d.cantidad_pedido
                }).ToList()
            };
        }

        public async Task<List<ComandaResponse>> ListarComandasAsync()
        {
            var comandas = await _context.comanda
                .Include(c => c.detalle_comanda)
                .ToListAsync();

            return comandas.Select(c => new ComandaResponse
            {
                Id = c.id,
                MesaId = c.mesa_id,
                Estado = c.estado_comanda?.estado,
                FechaEmision = c.fecha_emision,
                Detalles = c.detalle_comanda.Select(d => new DetalleComandaResponse
                {
                    PlatoId = d.plato_id,
                    CantidadPedido = d.cantidad_pedido
                }).ToList()
            }).ToList();
        }

        public async Task<bool> ActualizarEstadoComandaAsync(int id, string nuevoEstado)
        {
            var comanda = await _context.comanda.FindAsync(id);
            if (comanda == null) return false;

            var estado = await _context.estado_comanda
                .FirstOrDefaultAsync(e => e.estado == nuevoEstado);

            if (estado == null) return false;

            comanda.estado_comanda_id = estado.id;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
