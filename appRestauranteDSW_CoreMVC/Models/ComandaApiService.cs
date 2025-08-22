using System.Text.Json;

namespace appRestauranteDSW_CoreMVC.Models
{
    public class ComandaApiService
    {
        private readonly HttpClient _httpClient;

        public ComandaApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7296/api"); // 👈 tu Web API
        }

        public async Task<List<ComandaResponse>> GetComandasAsync()
        {
            var response = await _httpClient.GetAsync("Comanda");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ComandaResponse>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
