namespace appRestauranteDSW_WebApi.DTOs
{
    public class DetalleComandaResponse
    {
        public int Id { get; set; }
        public string? PlatoId { get; set; }
        public string? Plato { get; set; } = string.Empty;
        public int? CantidadPedido { get; set; }
        public string? Observacion { get; set; }
        public decimal? PrecioUnitario { get; set; }
    }
    }
