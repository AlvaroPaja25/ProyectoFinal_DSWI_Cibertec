namespace appRestauranteDSW_WebApi.DTOs
{
    public class ComandaResponse
    {
        public int Id { get; set; }
        public int? MesaId { get; set; }
        public string? FechaEmision { get; set; }
        public decimal? PrecioTotal { get; set; }
        public string? Estado { get; set; } = string.Empty;
        public string? Mesa { get; set; } = string.Empty;

        public List<DetalleComandaResponse> Detalles { get; set; } = new();
    }
}

