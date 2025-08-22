namespace appRestauranteDSW_WebApi.DTOs
{
    public class ComandaRequest
    {
        public int MesaId { get; set; }
        public int EmpleadoId { get; set; }
        public int EstadoComandaId { get; set; }
        public List<DetalleComandaRequest> Detalles { get; set; } = new();
    }
}
