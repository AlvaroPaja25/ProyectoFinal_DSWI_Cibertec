namespace appRestauranteDSW_WebApi.DTOs
{
    public class DetalleComandaRequest
    {
        public string PlatoId { get; set; } = string.Empty;
        public int CantidadPedido { get; set; }
        public string? Observacion { get; set; }
    }
}
